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
    /// ConstructList.xaml 的交互逻辑
    /// </summary>
    public partial class ConstructItemList : UserControl
    {
        public ConstructItemList()
        {
            InitializeComponent();

            //ConstructFold.Instance.InitFold();
            //LinkList.ItemsSource = ConstructFold.Instance.ConstructFiles; 
        }

        #region 属性

        private ConstructFile _ConstructFile = null;

        

        #endregion


        #region logic

        public event EventHandler<SelectionChangedEventArgs> SelectChanged;

        private void DirList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectChanged != null)
            {
                SelectChanged(sender, e);
            }
        }

        public void lbMatFile_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
            }
        }

        public void SetListDataContent(TableBaseCollection dataContent)
        {
            LinkList.ItemsSource = dataContent;

            if (dataContent.Count == 0)
            {
                NoRecordText.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                NoRecordText.Visibility = System.Windows.Visibility.Collapsed;
            }

            LinkList.SelectedItem = null;
        }

        public void HideNoRecord()
        {
            NoRecordText.Visibility = System.Windows.Visibility.Collapsed;
        }
        #endregion
    }
}
