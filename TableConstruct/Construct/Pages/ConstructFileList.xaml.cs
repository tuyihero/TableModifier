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

namespace TableConstruct
{
    /// <summary>
    /// ConstructList.xaml 的交互逻辑
    /// </summary>
    public partial class ConstructFileList : UserControl, PathChildFrame
    {
        public ConstructFileList()
        {
            InitializeComponent();

            ConstructFold.Instance.InitFold();
            //顺便初始化内容
            LinkList.ItemsSource = ConstructFold.Instance.ConstructFiles; 
        }

        #region 接口

        public PathItem PathItem { get; set; }

        public object ParentBaseFrame { get; set; }

        public double GetWidth()
        { 
            return 250;
        }

        public void ShowContent(object param) { }

        #endregion

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            ConstructFold.Instance.Save();
        }

        private void Button_New(object sender, RoutedEventArgs e)
        {
            string newName = DialogMessage.DialogString();
            if (!string.IsNullOrEmpty(newName))
            {
                ConstructFold.Instance.CreateNewFile(newName);
            }
        }

        private void LinkList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConstructFile fileInfo = LinkList.SelectedItem as ConstructFile;
            if (fileInfo == null)
                return;

            PathBaseFrame pathFrame = ParentBaseFrame as PathBaseFrame;
            if (pathFrame == null)
                return;

            pathFrame.PushPage(PathItem, fileInfo.Name, typeof(ConstructItemInfo), fileInfo);
        }

        private void MenuItem_Remove(object sender, RoutedEventArgs e)
        {
            ConstructFile fileInfo = LinkList.SelectedItem as ConstructFile;
            if (fileInfo == null)
                return;

            ConstructFold.Instance.RemoveFile(fileInfo.Name);
        }

        private void MenuItem_Rename(object sender, RoutedEventArgs e)
        {
            ConstructFile fileInfo = LinkList.SelectedItem as ConstructFile;
            if (fileInfo == null)
                return;

            string newName = DialogMessage.DialogString(fileInfo.Name);
            if (!string.IsNullOrEmpty(newName))
            {
                ConstructFold.Instance.RenameFile(fileInfo.Name, newName);
            }
        }

    }
}
