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

using UITemplate.Controls;

namespace PictureShower
{
    /// <summary>
    /// PicturePathList.xaml 的交互逻辑
    /// </summary>
    public partial class PicturePathList : UserControl
    {
        public PicturePathList()
        {
            InitializeComponent();
        }

        #region logic

        public event EventHandler<SelectionChangedEventArgs> SelectChanged;
        
        private void DirList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem selectedItem = DirList.SelectedItem as ListBoxItem;
            if (selectedItem == null)
                return;

            string selectPath = selectedItem.Content.ToString();
            PictureManager.Instance.FindPictureFromDir(selectPath);

            if (SelectChanged != null)
            {
                SelectChanged(sender, e);
            }
        }

        public void InitPathEvent()
        {
            DirList.Items.Clear();
            foreach (string dirPath in PictureManager.Instance.PictureDirectories)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = dirPath;
                DirList.Items.Add(item);
            }
        }


        #endregion

        #region UI event

        public void lbMatFile_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
            }
        }

        #endregion

    }
}
