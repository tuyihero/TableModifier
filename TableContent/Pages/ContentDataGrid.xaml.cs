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
    /// ContentDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class ContentDataGrid : UserControl
    {
        public ContentDataGrid(ContentFile contentFile)
        {
            InitializeComponent();

            _ContentFile = contentFile;

            if (_ContentFile != null)
                ShowContent();
        }

        #region 属性

        private ContentFile _ContentFile = null;

        #endregion

        #region 

        private void ShowContent()
        {
            if (_ContentFile.ContentRow.Count == 0)
                return;

            DG1.DataContext = _ContentFile.ContentRow;
            ContentRow row = _ContentFile.ContentRow[0] as ContentRow;
            if (row == null)
                return;

            for (int i = 0; i < row.ContentItems.Count; ++i)
            {
                var itemConstrucst = row.ContentItems[i] as ContentItem;
                DataGridTextColumn textColumn = new DataGridTextColumn();
                textColumn.Header = itemConstrucst.ItemConstructName;
                Binding binding = new Binding(string.Format("ContentItems[{0}].Value", i)) { Mode = BindingMode.TwoWay, ValidatesOnDataErrors = true };
                //binding.ValidationRules.Add(new ContentValidationRule() { ConstructItem = itemConstrucst.ItemConstruct });
                textColumn.Binding = binding;
                DG1.Columns.Add(textColumn);
            }
            
        }

        #endregion

        private void DG1_MouseRightButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            ContentRow row = DG1.SelectedItem as ContentRow;
            if (row == null)
                return;

            if (ContentFileList.Instance != null)
            {
                ContentFileList.Instance.ShowFileRow(_ContentFile, row);
            }
        }
    }
}
