using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

using UITemplate.Controls;

namespace PictureShower
{
    /// <summary>
    /// PictureList.xaml 的交互逻辑
    /// </summary>
    public partial class PictureList : UserControl
    {
        #region 属性

        private bool _IsAutoWidth = false;
        private double _ScrollViewerHeight = 0;
        private double _ScrollViewerWidth = 0;

        #endregion

        public PictureList()
        {
            InitializeComponent();
        }

        public void ShowDirPictures()
        {
            imageList.Items.Clear();

            foreach (string picName in PictureManager.Instance.PictureShowNames)
            {
                AddPicToList(picName);
            }

            CulScrollViewerSize();
            scrollViewer.Height = _ScrollViewerHeight;
            scrollViewer.Width = _ScrollViewerWidth;
        }

        public void ClearImage()
        {
            imageList.Items.Clear();
        }

        private const string TAB_PATH_STR = "路径列表";
        public void SelectedPath()
        {
            if (TabContent.GetTabItem(TAB_PATH_STR) == null)
            {
                if (PictureManager.Instance.PictureDirectories.Count != 0)
                {
                    PicturePathList pathList = new PicturePathList();
                    pathList.InitPathEvent();
                    pathList.SelectChanged += DirList_SelectionChanged;

                    TabContent.AddTabItem(TAB_PATH_STR, pathList, TabPopType.POP_CLICK);
                    TabContent.ShowTabContent(TAB_PATH_STR);
                }
            }
            else
            {
                PicturePathList pathList = TabContent.GetTabItem(TAB_PATH_STR).TabControl as PicturePathList;
                pathList.InitPathEvent();
            }
        }

        #region event

        public void lbMatFile_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
            }
        }

        public void SetAutoWidth(bool isAuto)
        {
            if (_IsAutoWidth != isAuto)
            {
                _IsAutoWidth = isAuto;
                //ShowDirPictures();
                foreach (ImageControl imageControl in _ImageList)
                {
                    BitmapImage image = imageControl.Control.Source as BitmapImage;
                    if (image.PixelWidth > _ScrollViewerWidth)
                    {
                        imageControl.Control.Width = _ScrollViewerWidth - 30;
                    }
                }
            }
        }

        private void DirList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowDirPictures();
            TabContent.HideTabContent();
            imageList.Focus();

            scrollViewer.ScrollToVerticalOffset(0);
        }

        public void SetWidthRate(int width)
        {
            SetAllImageWidth(width);
        }
        #endregion

        #region show picture

        private class ImageControl
        {
            public Image Control;
            public double BaseWidth;
        }

        private List<ImageControl> _ImageList = new List<ImageControl>();
        private double _WidthRate;

        private void SetAllImageWidth(int width)
        {
            _WidthRate = width * 0.05d + 1;
            foreach (ImageControl imageControl in _ImageList)
            {
                imageControl.Control.Width = _WidthRate * imageControl.BaseWidth;
            }
        }

        private void CulScrollViewerSize()
        {
            Window mainwin = Application.Current.MainWindow;
            _ScrollViewerHeight = mainwin.ActualHeight - 180;
            _ScrollViewerWidth = mainwin.ActualWidth - 100;
        }

        public delegate void LoadImageDelegate(string picName, StackPanel panel);
        private void AddPicToList(string picName)
        {
            StackPanel panel = new StackPanel();
            panel.Height = 100;
            panel.Width = 100;
            panel.Margin = new Thickness(5);

            TextBlock textBlock = new TextBlock();
            textBlock.Text = "加载中...";
            panel.Children.Add(textBlock);

            panel.Dispatcher.BeginInvoke(
                new LoadImageDelegate(this.LoadImage), 
                System.Windows.Threading.DispatcherPriority.SystemIdle,
                new object[] { picName, panel});
            
            imageList.Items.Add(panel);
            //if (picName.Contains(".gif"))
            //{
            //    AddGif(picName);
            //}
            //else
            //{
            //    AddOther(picName);
            //}
        }

        private void LoadImage(string picName, StackPanel panel)
        {
            UIElement imageElement = null;
            if (picName.Contains(".gif"))
            {
                imageElement = CreateGif(picName);
            }
            else
            {
                imageElement = CreateOther(picName);
                ImageControl imageControl = new ImageControl();
                imageControl.Control = imageElement as Image;
                imageControl.BaseWidth = imageControl.Control.Width;
                _ImageList.Add(imageControl);
            }

            panel.Children.Clear();
            if (imageElement == null)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = "加载失败";
                panel.Children.Add(textBlock);
            }
            else
            {
                panel.Width = double.NaN;
                panel.Height = double.NaN;
                panel.Children.Add(imageElement);
            }
        }

        private UIElement CreateGif(string picName)
        {
            try
            {
                GifImageLib.GifImage gif = new GifImageLib.GifImage();
                gif.Source = picName;
                //gif.Margin = new Thickness(5, 5, 5, 5);

                //imageList.Items.Add(gif);

                //gif.MouseWheel += lbMatFile_MouseWheel;
                return gif;
            }
            catch (Exception e)
            {
                TextBox msg = new TextBox();
                msg.Text = "error:" + e.ToString();
                msg.Margin = new Thickness(5, 5, 5, 5);

                return null;
            }
        }

        private UIElement CreateOther(string picName)
        {
            try
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(picName, UriKind.Absolute);
                image.EndInit();
                Image imageControl = new Image();
                imageControl.Source = image;

                if (_WidthRate != 0)
                {
                    if (_IsAutoWidth)
                    {
                        imageControl.Width = _WidthRate * (_ScrollViewerWidth - 30);
                    }
                    else
                    {
                        imageControl.Width = _WidthRate * image.PixelWidth;
                    }
                }
                else if (_IsAutoWidth && image.PixelWidth > _ScrollViewerWidth)
                {
                    imageControl.Width = _ScrollViewerWidth - 30;
                }
                else
                {
                    imageControl.Width = image.PixelWidth;
                }
                //imageControl.Margin = new Thickness(5, 5, 5, 5);

                //imageList.Items.Add(imageControl);
                //imageControl.MouseWheel += lbMatFile_MouseWheel;
                return imageControl;
            }
            catch (Exception e)
            {
                TextBox msg = new TextBox();
                msg.Text = "error:" + e.ToString();
                msg.Margin = new Thickness(5, 5, 5, 5);

                //imageList.Items.Add(msg);
                return null;
            }
        }

        #endregion
    }
}
