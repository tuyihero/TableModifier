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
    public partial class ConstructItemInfo : UserControl, PathChildFrame
    {
        public ConstructItemInfo()
        {
            InitializeComponent();

            LinkList.AddContexMenu("新建", MenuItem_New);
        }

        #region 属性

        private ConstructFile _ConstructFile = null;

        private ConstructItem _CurItem = null;
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
            ConstructFile fileInfo = param as ConstructFile;
            if (fileInfo == null)
                return;

            _ConstructFile = fileInfo;

            //ItemAttrPanel.Visibility = System.Windows.Visibility.Collapsed;

            LinkList.ItemsSource = _ConstructFile.ConstructItems; 
            //InitItemType1();
            ContentTransition.ChangeContent(null, PathContentTranstitionType.NO_ANIM);
        }

        #endregion

        private const string TAB_CONSTRUCT_ITEM_STR = "结构项";
        
        #region 事件
        
        public void ConItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ItemAttrPanel.Visibility = System.Windows.Visibility.Visible;

            if (e.AddedItems.Count > 0)
            {
                _CurItem = e.AddedItems[0] as ConstructItem;
                SetDataContent(e.AddedItems[0] as ConstructItem);
            }

            //InitItemType1();
        }

        private void Button_TableInfo(object sender, RoutedEventArgs e)
        {
            ConstructTableInfoPanel infoPanel = new ConstructTableInfoPanel();
            infoPanel.DataContext = _ConstructFile;
            ContentTransition.ChangeContent(infoPanel, PathContentTranstitionType.NO_ANIM);
        }

        private void Button_New(object sender, RoutedEventArgs e)
        {
            if (_ConstructFile != null)
            {
                ConstructItem item = _ConstructFile.CreateNewItem();

                SetDataContent(item);
            }
        }

        private void MenuItem_Remove(object sender, RoutedEventArgs e)
        {
            ConstructItem itemInfo = LinkList.SelectedItem as ConstructItem;
            if (itemInfo == null)
                return;

            _ConstructFile.ConstructItems.RemoveByName(itemInfo.Name);
        }

        private void MenuItem_Rename(object sender, RoutedEventArgs e)
        {
            ConstructItem itemInfo = LinkList.SelectedItem as ConstructItem;
            if (itemInfo == null)
                return;

            string newName = DialogMessage.DialogString();
            if (!string.IsNullOrEmpty(newName))
            {
                itemInfo.Name = newName;
            }
        }

        private void MenuItem_New(object sender, RoutedEventArgs e)
        {
            ConstructItem itemInfo = LinkList.SelectedItem as ConstructItem;
            if (itemInfo == null)
                return;

            int pos = _ConstructFile.ConstructItems.IndexOf(itemInfo);

            if (_ConstructFile != null)
            {
                ConstructItem item = _ConstructFile.CreateNewItem();
                SetDataContent(item);

                _ConstructFile.ConstructItems.MovePosToPos(_ConstructFile.ConstructItems.IndexOf(item), pos);
            }
        } 

        private void BaseList_DragInsert(object sender, DragInsertEventArgs e)
        {
            TableBaseItem dragFrom = e.DragFrom as TableBaseItem;
            TableBaseItem dragTo = e.DragTo as TableBaseItem;
            if (dragFrom == null || dragTo == null)
                return;

            int itemIdx = _ConstructFile.ConstructItems.IndexOf(dragFrom);
            int pos = _ConstructFile.ConstructItems.IndexOf(dragTo);
            if (itemIdx < pos)
                --pos;

            if (!e.IsDropFront)
                ++pos;

            _ConstructFile.ConstructItems.MovePosToPos(itemIdx, pos);
        }
        #endregion

        #region 类型

        

        private void SetDataContent(ConstructItem item)
        {
            if (item == null)
                return;

            ConstructItemInfoPanel infoPanel = new ConstructItemInfoPanel();
            infoPanel.DataContext = item;
            ContentTransition.ChangeContent(infoPanel, PathContentTranstitionType.NO_ANIM);
            //ItemAttrPanel.Visibility = System.Windows.Visibility.Visible;
            //ItemAttrPanel.DataContext = item;

            //if (item.ItemReadOnly)
            //{
            //    this.Name.IsEnabled = false;
            //    this.ItemCode.IsEnabled = false;
            //    this.ItemDefault.IsEnabled = false;
            //    this.ItemRepeat.IsEnabled = false;
            //}
            //else
            {
                //this.Name.IsEnabled = true;
                //this.ItemCode.IsEnabled = true;
                //this.ItemDefault.IsEnabled = true;
                //this.ItemRepeat.IsEnabled = true;
            }
        }

        
        #endregion
    }
}
