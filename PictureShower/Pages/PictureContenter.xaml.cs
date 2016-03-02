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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PictureShower
{
    /// <summary>
    /// PictureContenter.xaml 的交互逻辑
    /// </summary>
    public partial class PictureContenter : UserControl, INotifyPropertyChanged
    {
        public PictureContenter()
        {
            InitializeComponent();

            widthSlider.DataContext = this;
        }

        #region 接口 INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

        #region 属性

        private int _WidthSlide = 0;
        public int WidthSlide
        {
            get { return _WidthSlide; }
            set
            {
                _WidthSlide = value;
                PicList.SetWidthRate(value);
                OnPropertyChanged("WidthSlide");
            }
        }

        #endregion

        #region 事件

        private void ShowFileSelect_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
            {
                PictureManager.Instance.FindDirectFromPath(fbd.SelectedPath);
                FolderPath.Text = fbd.SelectedPath;

                PicList.SelectedPath();
            }
        }

        

        private void AutoWidth_Check(object sender, RoutedEventArgs e)
        {
            PicList.SetAutoWidth(true);
        }

        private void UnAutoWidth_Check(object sender, RoutedEventArgs e)
        {
            PicList.SetAutoWidth(false);
        }
        #endregion

        

        

        

        
    }
}
