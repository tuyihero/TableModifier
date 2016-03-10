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
    public partial class ItemVector3Control : UserControl
    {
        public ItemVector3Control()
        {
            InitializeComponent();
        }

        private void ValidValue()
        {
            string value = _VectorX + ";" + _VectorY + ";" + _VectorZ;
            _ContentItem.Value = value;
        }

        #region 

        private ContentItem _ContentItem = null;

        private string _VectorX = "";
        private string _VectorY = "";
        private string _VectorZ = "";
        
        #endregion

        #region

        private void Item_GotFocus_1(object sender, RoutedEventArgs e)
        {
            ItemBorder.BorderThickness = new Thickness(0, 0, 0, 2);
        }

        private void Item_LostFocus_1(object sender, RoutedEventArgs e)
        {
            ItemBorder.BorderThickness = new Thickness(0, 0, 0, 0);

            _VectorX = VectorX.Text;
            _VectorY = VectorY.Text;
            _VectorZ = VectorZ.Text;
            ValidValue();
        }

        private void Grid_DataContextChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            _ContentItem = null;
            _ContentItem = e.NewValue as ContentItem;
            if (_ContentItem != null 
                && _ContentItem.ItemConstruct.ItemType1 == TableConstruct.ConstructConfig.CONSTRUCT_ITEM_TYPE_BASE
                && _ContentItem.ItemConstruct.ItemType2[0].Name == TableConstruct.ConstructConfig.ITEM_TYPE_BASE_VECTOR3)
            {
                var splitValues = ContentItem.GetSplitValue(_ContentItem.Value);
                if (splitValues.Count == 3)
                {
                    _VectorX = splitValues[0];
                    _VectorY = splitValues[1];
                    _VectorZ = splitValues[2];

                    VectorX.Text = _VectorX;
                    VectorY.Text = _VectorY;
                    VectorZ.Text = _VectorZ;
                }

                ValidValue();
            }
        }

        #endregion
    }
}
