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

namespace KeyChanger
{
    /// <summary>
    /// WinHookPage.xaml 的交互逻辑
    /// </summary>
    public partial class WinHookPage : UserControl
    {
        public WinHookPage()
        {
            InitializeComponent();

            WinList.ItemsSource = WinManager.Instance.GetSysWinTitles();
        }

        public void ItemType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count < 1)
                return;

            if (!(e.AddedItems[0] is WinInfoItem))
                return;

            string typeStr = ((WinInfoItem)e.AddedItems[0]).Title;
        }

        private void StartJoyStickHook(object sender, RoutedEventArgs e)
        {
            KeyChangeManager.Instance.StartHookInit();

            WinManager.Instance.HookJoyStick();

        }

        private void StartKeyboardHook(object sender, RoutedEventArgs e)
        {
            KeyChangeManager.Instance.StartHookInit();

            WinManager.Instance.HookKeyboard();
            
        }

        private void TestKeySend(object sender, RoutedEventArgs e)
        {
            user32.keybd_event((byte)System.Windows.Forms.Keys.W, 0, 0, 0);

        }
    }
}
