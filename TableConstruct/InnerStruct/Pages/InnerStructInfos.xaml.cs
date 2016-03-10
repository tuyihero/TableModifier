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
    public partial class InnerStructInfos : UserControl, PathChildFrame
    {
        public InnerStructInfos()
        {
            InitializeComponent();

            InfoList.ItemsSource = InnerStructManager.Instance.InnerStructInfoCollection;
        }

        #region 属性

        private InnerStructItemList _ItemList = null;

        private InnerStructInfo _CurInfo = null;

        private InnerStructItem _CurItem = null;
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
            InfoList.ItemsSource = InnerStructManager.Instance.InnerStructInfoCollection;
        }

        #endregion

        #region logic

        private const string TAB_CONSTRUCT_ITEM_STR = "结构项";
        private void AddTab()
        {
            if (_ItemList == null)
            {
                _ItemList = new InnerStructItemList();
                _ItemList.SelectChanged += ConItem_SelectionChanged;
                TabContent.AddTabItem(TAB_CONSTRUCT_ITEM_STR, _ItemList, TabPopType.POP_CLICK);
            }

            _ItemList.SetListDataContent(_CurInfo._InnerStructItemCollection);
            TabContent.ShowTabContent(TAB_CONSTRUCT_ITEM_STR);
        }

        private void SetDataContent(InnerStructItem item)
        {
            if (item == null)
                return;

            ItemAttrPanel.Visibility = System.Windows.Visibility.Visible;
            ItemAttrPanel.DataContext = item;

            if (item.ItemReadOnly)
            {
                this.ItemName.IsEnabled = false;
                this.ItemCode.IsEnabled = false;
                this.ItemDefault.IsEnabled = false;
                this.ItemRepeat.IsEnabled = false;
            }
            else
            {
                this.ItemName.IsEnabled = true;
                this.ItemCode.IsEnabled = true;
                this.ItemDefault.IsEnabled = true;
                this.ItemRepeat.IsEnabled = true;
            }
        }

        #endregion

        #region 事件
        public void ConItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemAttrPanel.Visibility = System.Windows.Visibility.Visible;

            if (e.AddedItems.Count > 0)
            {
                _CurItem = e.AddedItems[0] as InnerStructItem;
                ItemAttrPanel.DataContext = _CurItem;
            }

            TabContent.HideTabContent();

            //InitItemType1();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            WriteInnerStruct.WriteAll();
        }

        private void Button_NewInfo(object sender, RoutedEventArgs e)
        {
            string newName = DialogMessage.DialogString();
            if (!string.IsNullOrEmpty(newName))
            {
                InnerStructManager.Instance.CreateNewEnum(newName);
            }
        }

        private void EnumList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InnerStructInfo info = e.AddedItems[0] as InnerStructInfo;
            if (info != null)
            {
                _CurInfo = info;
            }
            AddTab();

            ItemAttrPanel.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Button_NewItem(object sender, RoutedEventArgs e)
        {
            InnerStructItem item = _CurInfo.CreateItem();

            _ItemList.HideNoRecord();
            TabContent.HideTabContent();

            SetDataContent(item);
        }

        private void MenuItem_Remove(object sender, RoutedEventArgs e)
        {
            InnerStructInfo fileInfo = InfoList.SelectedItem as InnerStructInfo;
            if (fileInfo == null)
                return;

            InnerStructManager.Instance.RemoveFile(fileInfo.Name);
        }

        private void MenuItem_Rename(object sender, RoutedEventArgs e)
        {
            InnerStructInfo fileInfo = InfoList.SelectedItem as InnerStructInfo;
            if (fileInfo == null)
                return;

            string newName = DialogMessage.DialogString();
            if (!string.IsNullOrEmpty(newName))
            {
                InnerStructManager.Instance.RenameFile(fileInfo.Name, newName);
            }
        }
        #endregion
    }
}
