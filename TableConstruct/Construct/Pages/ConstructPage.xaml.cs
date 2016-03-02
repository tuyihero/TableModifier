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

using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using UITemplate.Controls;

namespace TableConstruct
{
    /// <summary>
    /// ConstructPage.xaml 的交互逻辑
    /// </summary>
    public partial class ConstructPage : PathBaseFrame
    {
        public ConstructPage()
        {
            InitializeComponent();
        }

        #region 继承

        public override void OnApplyTemplateFinish()
        {
            base.OnApplyTemplateFinish();

            PushPage(null, "结构文件列表", typeof(ConstructFileList));
        }

        #endregion

    }
}
