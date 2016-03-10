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
    /// KeyChangerPage.xaml 的交互逻辑
    /// </summary>
    public partial class KeyChangerPage : UserControl
    {
        public KeyChangerPage()
        {
            InitializeComponent();

            RWFile.ReadFile();
            ItemList.ItemsSource = KeyChangeManager.Instance.KeyChangeItemCollection;

            Window mainwin = Application.Current.MainWindow;
            ItemList.Height = mainwin.ActualHeight - 180;
            ItemList.Width = mainwin.ActualWidth - 100;
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            RWFile.WriteFile();
        }

        private void Button_New(object sender, RoutedEventArgs e)
        {
            KeyChangeManager.Instance.NewChange();
        }

        private void Button_Remove(object sender, RoutedEventArgs e)
        {
            KeyChangeItem changeItem = ItemList.SelectedItem as KeyChangeItem;
            if (changeItem == null)
                return;

            KeyChangeManager.Instance.RemoveChange(changeItem);
        }
        //private void TextBox_KeyUp(object sender, KeyEventArgs e)
        //{

        //    KeyChangeItem keyItem = sender as KeyChangeItem;
        //    if (keyItem != null)
        //    {
                
        //        ItemList.Focus();
        //    }

        //    //无论是否成功，去掉事件
        //    WinManager.Instance.Joystick.ButtonDown -= JoyStickClick;
        //}

        //private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    //获得焦点时，清空内容
        //    TextBox textBox = sender as TextBox;
        //    if (textBox != null)
        //    {
        //        textBox.Text = "";
        //    }

        //    //注册手柄事件
        //    WinManager.Instance.Joystick.Click += JoyStickClick;
        //}

        //#region 手柄触发

        //public void JoyStickClick(object sender, JoystickEventArgs e)
        //{
        //    KeyChangeItem keyItem = sender as KeyChangeItem;
        //    if (keyItem != null)
        //    {
        //        //keyItem.SetFromKey(WinManager.GetClickButton(e.Buttons));
        //        ItemList.Focus();
        //    }

        //    //无论是否成功，去掉事件
        //    WinManager.Instance.Joystick.ButtonDown -= JoyStickClick;
        //}

        //#endregion

    }
}
