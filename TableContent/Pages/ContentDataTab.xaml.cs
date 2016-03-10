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
    /// ContentDataTab.xaml 的交互逻辑
    /// </summary>
    public partial class ContentDataTab : UserControl, PathChildFrame
    {
        public ContentDataTab()
        {
            InitializeComponent();
        }

        #region 属性

        private ContentItemInfo _ListShow = null;
        private ContentDataGrid _GridShow = null;

        private ContentFile _ContentFile = null;

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
            ShowRowParam showParam = param as ShowRowParam;
            if (showParam == null)
                return;

            _ContentFile = showParam.ShowContentFile;
            ShowList();

            if (showParam.ShowContentRow != null)
            {
                _ListShow.ShowRow(showParam.ShowContentRow);
            }

            //ItemAttrPanel.Visibility = System.Windows.Visibility.Collapsed;

            //LinkList.ItemsSource = _ContentFile.ContentColumn;
            //InitItemType1();
            //InitItemType1();
        }

        private void ShowList()
        {
            if (_ListShow == null)
            {
                _ListShow = new ContentItemInfo(_ContentFile);
            }
            if (_ListShow == null)
                return;

            contentFrame.ChangeContent(_ListShow, PathContentTranstitionType.RIGHT_IN);
        }

        #endregion

        private void ButtonShowList(object sender, RoutedEventArgs e)
        {
            ShowList();
        }

        private void ButtonShowGrid(object sender, RoutedEventArgs e)
        {
            if (_GridShow == null)
            {
                _GridShow = new ContentDataGrid(_ContentFile);
            }
            if (_GridShow == null)
                return;


            contentFrame.ChangeContent(_GridShow, PathContentTranstitionType.RIGHT_IN);
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            //ConstructFold.Instance.Save();
        }

        private void ButtonRefresh(object sender, RoutedEventArgs e)
        {
            if (_GridShow != null)
            {
                contentFrame.ChangeContent(_GridShow, PathContentTranstitionType.RIGHT_OUT);
                _GridShow = null;
            }
            if (_ListShow != null)
            {
                contentFrame.ChangeContent(_ListShow, PathContentTranstitionType.RIGHT_OUT);
                _ListShow = null;
            }

            ReadContent.ReadContentFile(_ContentFile.ConstructFile, ref _ContentFile);
        }

        private void Button_New(object sender, RoutedEventArgs e)
        {
            string newName = DialogMessage.DialogString();
            if (!string.IsNullOrEmpty(newName))
            {
                _ContentFile.GreateRow(newName);
            }
        }
    }
}
