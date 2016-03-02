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

namespace TableContent
{
    /// <summary>
    /// MultiTableControl.xaml 的交互逻辑
    /// </summary>
    public partial class MultiTableControl : UserControl
    {
        public MultiTableControl()
        {
            InitializeComponent();
        }

        private void ValidValue()
        {
            string value = _TableName + ";" + _TableID;
            _ContentItem.Value = value;
        }

        #region 

        private ContentItem _ContentItem = null;

        private string _TableName = "";
        private string _TableID = "";
        
        #endregion

        #region

        private void ItemTextValue_GotFocus_1(object sender, RoutedEventArgs e)
        {
            ItemBorder.BorderThickness = new Thickness(0, 0, 0, 2);
        }

        private void ItemTextValue_LostFocus_1(object sender, RoutedEventArgs e)
        {
            ItemBorder.BorderThickness = new Thickness(0, 0, 0, 0);
        }

        private void Grid_DataContextChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            _ContentItem = null;
            _ContentItem = e.NewValue as ContentItem;

            Init();
        }

        private void ItemIDMultiValue1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            string selectedTable = e.AddedItems[0] as string;
            if (string.IsNullOrEmpty(selectedTable))
                return;

            _TableName = selectedTable;

            ContentFile contentFile = TableContentManager.Instance.GetFileByName(selectedTable);
            if (contentFile != null)
            {
                ItemIDMultiValue2.Items.Clear();
                foreach (ContentRow contentRow in contentFile.ContentRow)
                {
                    if (ContentConfig.IsContentIDInvalid(contentRow.ID))
                    {
                        ItemIDMultiValue2.Items.Add(contentRow.ID);
                    }
                }
            }
            else if (selectedTable == "None")
            {
                _TableName = "";
                _TableID = "";
                ItemIDMultiValue1.Text = _TableName;
                ItemIDMultiValue2.Text = _TableID;
                ValidValue();
            }

            //ItemIDMultiValue2.DisplayMemberPath = "DisplayName";
        }

        private void ItemIDMultiValue2_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            string selectedID = e.AddedItems[0] as string;

            _TableID = selectedID;

            ValidValue();
        }

        private void ItemIDMultiValue2_MouseRightButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            ContentFile contentFile = TableContentManager.Instance.GetFileByName(_TableName);
            if (contentFile == null)
                return;

            ContentRow contentRow = contentFile.ContentRow.GetRowByID(_TableID);
            if (contentRow == null)
                return;

            if (ContentFileList.Instance != null)
            {
                ContentFileList.Instance.ShowFileRow(contentFile, contentRow);
            }
        }

        private void ItemBorder_Loaded_1(object sender, RoutedEventArgs e)
        {
            Init();
        }
        #endregion


        private void Init()
        {
            if (_ContentItem != null
                && _ContentItem.ItemConstruct.ItemType1 == TableConstruct.ConstructConfig.CONSTRUCT_ITEM_TYPE_TABLE_ID
                && _ContentItem.ItemConstruct.ItemType2.Count > 1)
            {
                ItemIDMultiValue1.Items.Clear();
                foreach (var typeTable in _ContentItem.ItemConstruct.ItemType2)
                {
                    //if (contentRow.ID != ContentConfig.CONTENT_INVALID_ID)
                    {
                        ItemIDMultiValue1.Items.Add(typeTable.Name);
                    }
                }

                ItemIDMultiValue1.Items.Add("None");

                var splitValues = ContentItem.GetSplitValue(_ContentItem.Value);
                if (splitValues.Count == 2)
                {
                    _TableName = splitValues[0];
                    ItemIDMultiValue1.Text = splitValues[0];
                    _TableID = splitValues[1];
                    ItemIDMultiValue2.Text = splitValues[1];
                }

                ValidValue();
            }
        }
        
    }
}
