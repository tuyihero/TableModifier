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
    /// ConstructItem.xaml 的交互逻辑
    /// </summary>
    public partial class EnumerateItems : UserControl, PathChildFrame
    {
        public EnumerateItems()
        {
            InitializeComponent();

            EnumList.ItemsSource = EnumManager.Instance.EnumInfoCollection;
        }

        #region 属性

        private EnumInfo _CurEnum = null;
        #endregion

        #region 接口

        public PathItem PathItem { get; set; }

        public object ParentBaseFrame { get; set; }

        public double GetWidth()
        {
            return 500;
        }

        public void ShowContent(object param) 
        {
            EnumList.ItemsSource = EnumManager.Instance.EnumInfoCollection;
        }

        #endregion

        #region 事件
        private void Button_Save(object sender, RoutedEventArgs e)
        {
            WriteEnuminfos.WriteAll();
        }

        private void Button_NewInfo(object sender, RoutedEventArgs e)
        {
            string newName = DialogMessage.DialogString();
            if (!string.IsNullOrEmpty(newName))
            {
                EnumManager.Instance.CreateNewEnum(newName);
            }
        }

        private void EnumList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            EnumInfo info = e.AddedItems[0] as EnumInfo;
            if (info != null)
            {
                _CurEnum = info;
                EnumItems.ItemsSource = _CurEnum._EnumItemCollection;
            }
        }

        private void Button_NewItem(object sender, RoutedEventArgs e)
        {
            if (_CurEnum == null)
                return;

            _CurEnum.CreateEnumItem();
        }

        private void MenuItem_Remove(object sender, RoutedEventArgs e)
        {
            EnumInfo fileInfo = EnumList.SelectedItem as EnumInfo;
            if (fileInfo == null)
                return;

            EnumManager.Instance.RemoveFile(fileInfo.Name);
        }

        private void MenuItem_Rename(object sender, RoutedEventArgs e)
        {
            EnumInfo fileInfo = EnumList.SelectedItem as EnumInfo;
            if (fileInfo == null)
                return;

            string newName = DialogMessage.DialogString();
            if (!string.IsNullOrEmpty(newName))
            {
                EnumManager.Instance.RenameFile(fileInfo.Name, newName);
            }
        }
        #endregion
    }
}
