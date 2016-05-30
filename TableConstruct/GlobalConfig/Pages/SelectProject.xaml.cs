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

using FirstFloor.ModernUI.Windows.Controls; 

using UITemplate.Controls;

namespace TableConstruct.GlobalConfig.Pages
{
    /// <summary>
    /// SelectProject.xaml 的交互逻辑
    /// </summary>
    public partial class SelectProject : UserControl
    {
        public SelectProject()
        {
            InitializeComponent();

            InitProjectList();
        }

        private void InitProjectList()
        {
            TableGlobalConfig.Instance.Init();
            foreach (var projectName in TableGlobalConfig.Instance.ProjectFileNames)
            {
                _ProjectList.Items.Add(projectName);
            }
        }

        private void Button_Click_OK(object sender, RoutedEventArgs e)
        {
            if(_ProjectList.SelectedItem == null)
                return;

            string projectName = _ProjectList.SelectedItem as string;
            TableGlobalConfig.Instance.SelectedProject = projectName;
            TableGlobalConfig.Instance.InitProject();

            ChangePage(PAGE_NAME_CONSTRUCT, ITEM_NAME_CONSTRUCT_TABLE);
        }

        private void Button_Click_New(object sender, RoutedEventArgs e)
        {
            string newName = DialogMessage.DialogString();
            if (!string.IsNullOrEmpty(newName))
            {
                _ProjectList.Items.Add(newName);
                TableGlobalConfig.Instance.ProjectFileNames.Add(newName);
                WriteConfig.WriteProjectConfig(newName);

                ChangePage(PAGE_NAME_CONSTRUCT, ITEM_NAME_CONSTRUCT_SETTING);
            }
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {

        }

        public string PAGE_NAME_CONSTRUCT = "表格结构";
        public string ITEM_NAME_CONSTRUCT_SETTING = "配置";
        public string ITEM_NAME_CONSTRUCT_TABLE = "表格";

        public void ChangePage(string pageName, string itemName)
        {
            ModernWindow mainWin = Application.Current.MainWindow as ModernWindow;
            foreach (var linkGroup in mainWin.MenuLinkGroups)
            {
                if (linkGroup.DisplayName != pageName)
                    continue;

                foreach (var link in linkGroup.Links)
                {
                    if (link.DisplayName != itemName)
                        continue;

                    mainWin.ContentSource = link.Source;
                }
            }
        }
    }
}
