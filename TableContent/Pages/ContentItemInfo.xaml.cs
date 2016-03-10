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
    /// ConstructItem.xaml 的交互逻辑
    /// </summary>
    public partial class ContentItemInfo : UserControl
    {
        public ContentItemInfo(ContentFile contentFile, ContentRow selectedRow = null)
        {
            InitializeComponent();

            _ContentFile = contentFile;
            _SelectedRow = selectedRow;
            SetContex();
        }

        #region 属性

        private ContentFile _ContentFile = null;
        private ContentRow _SelectedRow = null;

        #endregion

        #region 方法

        private void SetContex()
        {
            RowList.ItemsSource = _ContentFile.ContentRow;
            if (_SelectedRow != null)
            {
                RowList.SelectedItem = _SelectedRow;
            }
        }

        public void ShowRow(ContentRow contentRow)
        {
            ItemValueList.ItemsSource = contentRow.ContentItems;
        }

        #endregion

        #region 事件

        private void Row_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count == 0)
                return;

            _SelectedRow = e.AddedItems[0] as ContentRow;
            if (_SelectedRow == null)
                return;

            ItemValueList.ItemsSource = _SelectedRow._ContentItems;
        }

        private void MenuItem_Remove(object sender, RoutedEventArgs e)
        {
            ContentRow fileInfo = RowList.SelectedItem as ContentRow;
            if (fileInfo == null)
                return;

            _ContentFile.ContentRow.Remove(fileInfo);
        }

        private void MenuItem_Rename(object sender, RoutedEventArgs e)
        {

        }

        private void BaseList_DragInsert(object sender, DragInsertEventArgs e)
        {
            TableBaseItem dragFrom = e.DragFrom as TableBaseItem;
            TableBaseItem dragTo = e.DragTo as TableBaseItem;
            if (dragFrom == null || dragTo == null)
                return;

            int itemIdx = _ContentFile.ContentRow.IndexOf(dragFrom);
            int pos = _ContentFile.ContentRow.IndexOf(dragTo);
            if (itemIdx < pos)
                --pos;

            if (!e.IsDropFront)
                ++pos;

            _ContentFile.ContentRow.MovePosToPos(itemIdx, pos);
        }

        
        #endregion
    }
}
