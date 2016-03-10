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
using System.Collections.ObjectModel;

namespace TableConstruct
{
    class Type1Items : ObservableCollection<string>
    {
        public Type1Items()
        {

            Add(ConstructConfig.CONSTRUCT_ITEM_TYPE_BASE);
            Add(ConstructConfig.CONSTRUCT_ITEM_TYPE_ENUM);
            Add(ConstructConfig.CONSTRUCT_ITEM_TYPE_TABLE_ID);
        }
    }

    class Type2Items : ObservableCollection<string>
    {
        private string _CurType1 = "";

        public Type2Items()
        {
        }

        public void SetType2Items(string itemType1)
        {
            ClearType2();
            _CurType1 = itemType1;
            switch (itemType1)
            {
                case ConstructConfig.CONSTRUCT_ITEM_TYPE_BASE:
                    Add(ConstructConfig.ITEM_TYPE_BASE_INT);
                    Add(ConstructConfig.ITEM_TYPE_BASE_FLOAT);
                    Add(ConstructConfig.ITEM_TYPE_BASE_STRING);
                    Add(ConstructConfig.ITEM_TYPE_BASE_BOOL);
                    Add(ConstructConfig.ITEM_TYPE_BASE_VECTOR3);
                    break;
                case ConstructConfig.CONSTRUCT_ITEM_TYPE_ENUM:
                    //BtnNewEnum.Visibility = System.Windows.Visibility.Visible;
                    EnumManager.Instance.EnumInfoCollection.ForEach((file) =>
                    {
                        Add(file.Name);
                    });
                    break;
                case ConstructConfig.CONSTRUCT_ITEM_TYPE_TABLE_ID:
                    ConstructFold.Instance.ConstructFiles.ForEach((file) =>
                    {
                        Add(file.Name);
                    });
                    break;
                default:
                    break;
            }
        }

        public void ClearType2()
        {
            switch (_CurType1)
            {
                case ConstructConfig.CONSTRUCT_ITEM_TYPE_BASE:
                    Remove(ConstructConfig.ITEM_TYPE_BASE_INT);
                    Remove(ConstructConfig.ITEM_TYPE_BASE_FLOAT);
                    Remove(ConstructConfig.ITEM_TYPE_BASE_STRING);
                    Remove(ConstructConfig.ITEM_TYPE_BASE_BOOL);
                    Remove(ConstructConfig.ITEM_TYPE_BASE_VECTOR3);
                    break;
                case ConstructConfig.CONSTRUCT_ITEM_TYPE_ENUM:
                    //BtnNewEnum.Visibility = System.Windows.Visibility.Visible;
                    EnumManager.Instance.EnumInfoCollection.ForEach((file) =>
                    {
                        Remove(file.Name);
                    });
                    break;
                case ConstructConfig.CONSTRUCT_ITEM_TYPE_TABLE_ID:
                    ConstructFold.Instance.ConstructFiles.ForEach((file) =>
                    {
                        Remove(file.Name);
                    });
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// TypePanel.xaml 的交互逻辑
    /// </summary>
    public partial class TypePanel : UserControl
    {
        public TypePanel()
        {
            InitializeComponent();
            //InitItemType1();

            BtnNewType2.Visibility = System.Windows.Visibility.Collapsed;
            BtnAddTableRelate.Visibility = System.Windows.Visibility.Collapsed;
            BtnDecTableRelate.Visibility = System.Windows.Visibility.Collapsed;
        }

        private ObservableCollection<string> _ItemType1Items = new ObservableCollection<string>() 
        { 
            ConstructConfig.CONSTRUCT_ITEM_TYPE_BASE,
            ConstructConfig.CONSTRUCT_ITEM_TYPE_ENUM,
            ConstructConfig.CONSTRUCT_ITEM_TYPE_TABLE_ID,
        };

        private ObservableCollection<string> _ItemType2Items = new ObservableCollection<string>();

        private void InitItemType1()
        {
            ItemType1.ItemsSource = _ItemType1Items;
        }

        private void InitItemType2()
        {
            //如果类型1未定义，保持清空即可
            if (ItemType1.SelectedItem == null)
            {
                return;
            }

            Type2Items type2Items = this.Resources["type2Items"] as Type2Items;
            if (type2Items != null)
            {
                type2Items.SetType2Items(ItemType1.SelectedItem.ToString());
            }
        }

        private void InitItemType2(string itemType1)
        {
            Type2Items type2Items = this.Resources["type2Items"] as Type2Items;
            if (type2Items != null)
            {
                type2Items.SetType2Items(itemType1);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ConstructItem contentItem = this.DataContext as ConstructItem;
            //if (contentItem != null)
            //{
            //    contentItem.ItemType2.AddNewItem(new TableBaseItem());
            //}

            //InnerStructItem innerStructItem = this.DataContext as InnerStructItem;
            //if (innerStructItem != null)
            //{
            //    innerStructItem.ItemType2.AddNewItem(new TableBaseItem());
            //}
        }

        public void ItemType1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                InitItemType2();
            }

            BtnNewType2.Visibility = System.Windows.Visibility.Collapsed;
            BtnAddTableRelate.Visibility = System.Windows.Visibility.Collapsed;
            BtnDecTableRelate.Visibility = System.Windows.Visibility.Collapsed;

            string selectedType1 = ItemType1.SelectedItem.ToString();
            if (selectedType1 == ConstructConfig.CONSTRUCT_ITEM_TYPE_ENUM)
            {
                BtnNewType2.Visibility = System.Windows.Visibility.Visible;
            }
            else if (selectedType1 == ConstructConfig.CONSTRUCT_ITEM_TYPE_TABLE_ID)
            {
                BtnAddTableRelate.Visibility = System.Windows.Visibility.Visible;
                BtnDecTableRelate.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void Button_AddRelate(object sender, RoutedEventArgs e)
        {
            ConstructItem constructItem = this.DataContext as ConstructItem;
            if (constructItem == null)
                return;

            constructItem.ItemType2.AddNewItem(new TableBaseItem());
            constructItem.WriteFlag = true;
        }

        private void Button_DecRelate(object sender, RoutedEventArgs e)
        {
            ConstructItem constructItem = this.DataContext as ConstructItem;
            if (constructItem == null)
                return;

            constructItem.ItemType2.Remove(constructItem.ItemType2.Last<TableBaseItem>());
        }

        private void StackPanel_DataContextChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            ConstructItem constructItem = e.NewValue as ConstructItem;
            if (constructItem == null)
                return;
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ConstructItem constructItem = this.DataContext as ConstructItem;
            if (constructItem == null)
                return;

            constructItem.SetDefaultByType(e.AddedItems[0].ToString());
        }

        public void RefreshType()
        {
            InitItemType2();
        }
    }
}
