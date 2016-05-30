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

namespace TableConstruct
{
    /// <summary>
    /// ShowTableCode.xaml 的交互逻辑
    /// </summary>
    public partial class ShowTableConfig : UserControl
    {
        public ShowTableConfig()
        {
            InitializeComponent();

            this.DataContext = TableGlobalConfig.Instance;
            TableGlobalConfig.Instance.InitProject();
        }

        private void TemplatePath_MouseDown_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
            {
                TableGlobalConfig.Instance.TemplatePath = fbd.SelectedPath;
            }
        }

        private void CodePath_MouseDown_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
            {
                TableGlobalConfig.Instance.CodePath = fbd.SelectedPath;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TableGlobalConfig.Instance.SavePath();
        }

        private void Button_SellectTargetPath(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
            {
                TableGlobalConfig.Instance.ProjectPath = fbd.SelectedPath;
                TableGlobalConfig.Instance.ConstructTablePath = fbd.SelectedPath + TableGlobalDefine.ELEMENT_DEFAULT_CONSTRUCT_PATH;
                TableGlobalConfig.Instance.ResTablePath = fbd.SelectedPath + TableGlobalDefine.ELEMENT_DEFAULT_RESOURCE_PATH;
                TableGlobalConfig.Instance.CodePath = fbd.SelectedPath + TableGlobalDefine.ELEMENT_DEFAULT_CODE_PATH;
            }
        }

        private void Button_SellectResPath(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
            {
                TableGlobalConfig.Instance.ResTablePath = fbd.SelectedPath;
            }
        }
    }
}
