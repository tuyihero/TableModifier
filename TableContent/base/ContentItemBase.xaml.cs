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
namespace TableContent
{
    /// <summary>
    /// ContentItemBase.xaml 的交互逻辑
    /// </summary>
    public partial class ContentItemBase : UserControl
    {
        public ContentItemBase()
        {
            InitializeComponent();
        }

        #region 属性

        private ContentItem _ContentItem = null;

        #endregion

        #region 逻辑

        private void InitValue()
        {
            switch (_ContentItem.ItemConstruct.ItemType1)
            {
                case ConstructConfig.CONSTRUCT_ITEM_TYPE_BASE:
                    InitBaseType();
                    break;
                case ConstructConfig.CONSTRUCT_ITEM_TYPE_ENUM:
                    InitEnumType();
                    break;
                case ConstructConfig.CONSTRUCT_ITEM_TYPE_TABLE_ID:
                    InitTableIdType();
                    break;
                    
            }
        }

        private void InitBaseType()
        {
            ItemTextValue.Visibility = System.Windows.Visibility.Collapsed;
            ItemEnmuValue.Visibility = System.Windows.Visibility.Collapsed;
            ItemIDSingleValue.Visibility = System.Windows.Visibility.Collapsed;
            ItemIDMultiValue.Visibility = System.Windows.Visibility.Collapsed;
            ItemBoolValue.Visibility = System.Windows.Visibility.Collapsed;
            ItemVector3Value.Visibility = System.Windows.Visibility.Collapsed;

            if (_ContentItem.ItemConstruct.ItemType2[0].Name == ConstructConfig.ITEM_TYPE_BASE_BOOL)
            {
                ItemBoolValue.Visibility = System.Windows.Visibility.Visible;
            }
            else if (_ContentItem.ItemConstruct.ItemType2[0].Name == ConstructConfig.ITEM_TYPE_BASE_VECTOR3)
            {
                ItemVector3Value.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                ItemTextValue.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void InitEnumType()
        {
            ItemTextValue.Visibility = System.Windows.Visibility.Collapsed;
            //ItemEnmuValue.Visibility = System.Windows.Visibility.Collapsed;
            ItemIDSingleValue.Visibility = System.Windows.Visibility.Collapsed;
            ItemIDMultiValue.Visibility = System.Windows.Visibility.Collapsed;
            ItemBoolValue.Visibility = System.Windows.Visibility.Collapsed;
            ItemVector3Value.Visibility = System.Windows.Visibility.Collapsed;

            ItemEnmuValue.Visibility = System.Windows.Visibility.Visible;
            //ItemEnmuValue.Items.Clear();

            EnumInfo enumInfo = EnumManager.Instance.GetEnum(_ContentItem.ItemConstruct.ItemType2[0].Name);
            if (enumInfo == null)
                return;

            foreach (TableBaseItem tableItem in enumInfo._EnumItemCollection)
            {
                ITableDisplay enumItem = tableItem as ITableDisplay;
                if (enumItem != null)
                {
                    if (ItemEnmuValue.Items.Contains(enumItem))
                        continue;

                    ItemEnmuValue.Items.Add(enumItem);
                }
            }

            ItemEnmuValue.SelectedValuePath = "WriteValue";
            ItemEnmuValue.DisplayMemberPath = "DisplayName";
        }

        private void InitTableIdType()
        {
            ItemTextValue.Visibility = System.Windows.Visibility.Collapsed;
            ItemEnmuValue.Visibility = System.Windows.Visibility.Collapsed;
            ItemIDSingleValue.Visibility = System.Windows.Visibility.Collapsed;
            ItemIDMultiValue.Visibility = System.Windows.Visibility.Collapsed;
            ItemBoolValue.Visibility = System.Windows.Visibility.Collapsed;
            ItemVector3Value.Visibility = System.Windows.Visibility.Collapsed;

            if (_ContentItem.ItemConstruct.ItemType2.Count > 1)
            {
                ItemIDMultiValue.Visibility = System.Windows.Visibility.Visible;

                //foreach (var typeTable in _ContentItem.ItemConstruct.ItemType2)
                //{
                //    //if (contentRow.ID != ContentConfig.CONTENT_INVALID_ID)
                //    {
                //        ItemIDMultiValue1.Items.Add(typeTable.Name);
                //    }
                //}

                //_ContentItem.SplitValue();
                //ItemIDMultiValue1.Text = _ContentItem.GetSplitValue(0);
                //ItemIDMultiValue2.Text = _ContentItem.GetSplitValue(1);
                

            }
            else if (_ContentItem.ItemConstruct.ItemType2.Count == 1)
            {
                ItemIDSingleValue.Visibility = System.Windows.Visibility.Visible;

                ContentFile contentFile = TableContentManager.Instance.GetFileByName(_ContentItem.ItemConstruct.ItemType2[0].Name);
                if (contentFile == null)
                    return;

                ItemIDSingleValue.Items.Clear();
                foreach (ContentRow contentRow in contentFile.ContentRow)
                {
                    if (ContentConfig.IsContentIDInvalid(contentRow.ID))
                    {
                        ItemIDSingleValue.Items.Add(contentRow);
                    }
                }

                //空选择
                ItemIDSingleValue.Items.Add("None");

                ContentRow selectRow = contentFile.GetRowByID(_ContentItem.Value);
                if (selectRow != null)
                {
                    ItemIDSingleValue.Text = selectRow.DisplayName;
                }
                ItemIDSingleValue.SelectedValuePath = "WriteValue";
                ItemIDSingleValue.DisplayMemberPath = "DisplayName";
            }


        }
        #endregion

        private void Item_GotFocus_1(object sender, RoutedEventArgs e)
        {
            ItemBorder.BorderThickness = new Thickness(0, 0, 0, 2);
        }

        private void Item_LostFocus_1(object sender, RoutedEventArgs e)
        {
            ItemBorder.BorderThickness = new Thickness(0, 0, 0, 0);
        }

        private void Grid_DataContextChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            _ContentItem = null;
            _ContentItem = e.NewValue as ContentItem;
            if (_ContentItem != null)
            {
                InitValue();
            }
        }

        //private void ItemIDMultiValue1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    if (e.AddedItems.Count == 0)
        //        return;

        //    string selectedTable = e.AddedItems[0] as string;
        //    if (string.IsNullOrEmpty(selectedTable))
        //        return;

        //    ContentFile contentFile = TableContentManager.Instance.GetFileByName(selectedTable);
        //    if (contentFile == null)
        //        return;

        //    ItemIDMultiValue2.Items.Clear();
        //    foreach (ContentRow contentRow in contentFile.ContentRow)
        //    {
        //        if (ContentConfig.IsContentIDInvalid(contentRow.ID))
        //        {
        //            ItemIDMultiValue2.Items.Add(contentRow.ID);
        //        }
        //    }
        //}

        //private void ItemIDMultiValue2_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    if (e.AddedItems.Count == 0)
        //        return;

        //    string selectedID = e.AddedItems[0] as string;

        //    _ContentItem.SetSplitValue(ItemIDMultiValue1.Text, 0);
        //    _ContentItem.SetSplitValue(selectedID, 1);

        //    _ContentItem.CombineSplitValue();
        //}

        private void ItemEnmuValue_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            ITableDisplay selectedTable = e.AddedItems[0] as ITableDisplay;
            if (selectedTable == null)
                return;

            _ContentItem.DisplayInfo = selectedTable;

            //_ContentItem.SetSplitValue(ItemIDMultiValue1.Text, 0);
            //_ContentItem.SetSplitValue("-1", 1);

            //_ContentItem.CombineSplitValue();
        }

        private void ItemIDSingleValue_MouseRightButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            ContentFile contentFile = TableContentManager.Instance.GetFileByName(_ContentItem.ItemConstruct.ItemType2[0].Name);
            if (contentFile == null)
                return;

            ContentRow contentRow = contentFile.ContentRow.GetRowByID(_ContentItem.Value);
            if (contentRow == null)
                return;

            if (ContentFileList.Instance != null)
            {
                ContentFileList.Instance.ShowFileRow(contentFile, contentRow);
            }
        }

        private void ItemBorder_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (_ContentItem != null)
            {
                InitValue();
            }
        }

        private void ItemIDSingleValue_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            ITableDisplay displayItem = e.AddedItems[0] as ITableDisplay;
            if (displayItem != null)
            {
                _ContentItem.Value = displayItem.WriteValue;
                ItemIDSingleValue.Text = displayItem.DisplayName;
            }
            else
            {
                _ContentItem.Value = "";
                ItemIDSingleValue.Text = "";
            }
        }
        
    }
}
