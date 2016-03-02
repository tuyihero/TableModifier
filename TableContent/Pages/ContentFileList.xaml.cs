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

using TableConstruct;
using UITemplate.Controls;

namespace TableContent
{
    /// <summary>
    /// ConstructList.xaml 的交互逻辑
    /// </summary>
    public partial class ContentFileList : UserControl
    {
        #region 静态
        private static ContentFileList _Instance = null;
        public static ContentFileList Instance
        {
            get
            {
                return _Instance;
            }
        }
        #endregion

        public ContentFileList()
        {
            InitializeComponent();

            TableContentManager.Instance.InitFiles();
            LinkList.ItemsSource = TableContentManager.Instance.ContentFiles;

            _Instance = this;
        }

        #region 方法
        public void ShowFileRow(ContentFile fileInfo, ContentRow rowInfo)
        {
            ListFrame.PushPage(null, fileInfo.Name, typeof(ContentDataTab), new ShowRowParam() { ShowContentFile = fileInfo, ShowContentRow = rowInfo });
        }

        public void Refresh()
        {
            ListFrame.Clear();
        }
        #endregion

        #region 事件

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            TableContentManager.Instance.SaveFiles();
        }

        private void Button_New(object sender, RoutedEventArgs e)
        {
            //string newName = DialogMessage.DialogString();
            //if (newName != null)
            //{
            //    ConstructFold.Instance.CreateNewFile(newName);
            //}
            WriteContent.CreateFileNotExist();
        }

        private void LinkList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ContentFile fileInfo = LinkList.SelectedItem as ContentFile;
            if (fileInfo == null)
                return;

            ListFrame.PushPage(null, fileInfo.Name, typeof(ContentDataTab), new ShowRowParam() { ShowContentFile = fileInfo });
            //PathBaseFrame pathFrame = ParentBaseFrame as PathBaseFrame;
            //if (pathFrame == null)
            //    return;

            //pathFrame.PushPage(PathItem, fileInfo.Name, typeof(ContentDataTab), fileInfo);
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

            string newName = DialogMessage.DialogString();
            if (!string.IsNullOrEmpty(newName))
            {
                ConstructFold.Instance.RenameFile(fileInfo.Name, newName);
            }
        }

        private void BaseList_DragInsert(object sender, DragInsertEventArgs e)
        {
            MessageBox.Show("drop here");
        }

        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            ReadContent.ReLoadNeed();
        }
        #endregion

       

    }
}
