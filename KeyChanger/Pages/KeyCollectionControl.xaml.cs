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

namespace KeyChanger
{
    /// <summary>
    /// KeyCollectionControl.xaml 的交互逻辑
    /// </summary>
    public partial class KeyCollectionControl : UserControl
    {
        #region 属性

        public static readonly DependencyProperty AlignmentProperty = DependencyProperty.Register("Alignment", typeof(HorizontalAlignment), typeof(KeyCollectionControl), new PropertyMetadata(OnAlignment));
        public HorizontalAlignment Alignment
        {
            get { return (HorizontalAlignment)GetValue(AlignmentProperty); }
            set { SetValue(AlignmentProperty, value); }
        }
        private static void OnAlignment(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((KeyCollectionControl)o).OrderList(((HorizontalAlignment)e.NewValue));
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(KeyStoreInfoCollection), typeof(KeyCollectionControl), new PropertyMetadata(OnSource));
        public KeyStoreInfoCollection Source
        {
            get { return (KeyStoreInfoCollection)GetValue(SourceProperty); }
            set 
            {
                SetValue(SourceProperty, value);
                ItemList.ItemsSource = value;
            }
        }
        private static void OnSource(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((KeyCollectionControl)o).ItemList.ItemsSource = (KeyStoreInfoCollection)e.NewValue;
        }

        private StackPanel _ItemPanel = null;
        #endregion

        public KeyCollectionControl()
        {
            InitializeComponent();
            
        }

        #region 事件
        //获得焦点是注册输入事件
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //获得焦点时，清空内容
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.Text = "";
            }

            //注册手柄事件
            WinManager.Instance.AddJoystickClick(JoyStickClick);

            //注册键盘事件
            WinManager.Instance.AddKeyDown(KeyboardClick);
        }

        //屏蔽输入
        private void TextBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        //添加
        private void AddStoreInfo(object sender, RoutedEventArgs e)
        {
            Source.Add(new KeyStoreInfo());
        }

        //删除 最后一个
        private void DecStoreInfo(object sender, RoutedEventArgs e)
        {
            Source.RemoveAt(Source.Count - 1);
        }

        //加载，只会执行一次 获取ItemList的Panel
        private void ItemPanel_Loaded(object sender, RoutedEventArgs e)
        {
            _ItemPanel = sender as StackPanel;

            OrderList(Alignment);
        }
        #endregion

        #region 按键回调

        public void JoyStickClick(object sender, JoystickEventArgs e)
        {
            TextBox textBox = Keyboard.FocusedElement as TextBox;
            if (textBox == null)
                return;

            KeyStoreInfo keyStore = textBox.DataContext as KeyStoreInfo;
            if (keyStore != null)
            {
                keyStore.SetStoreValue(WinManager.GetClickButton(e.Buttons));
                //ItemList.Focus();
            }

            FinishInput();
        }

        public void KeyboardClick(object sender, KeyboardHookArgs e)
        {
            TextBox textBox = (TextBox)Keyboard.FocusedElement;
            if (textBox == null)
                return;

            KeyStoreInfo keyStore = textBox.DataContext as KeyStoreInfo;
            if (keyStore != null)
            {
                keyStore.SetStoreValue(e.Key);
                //ItemList.Focus();
            }

            FinishInput();
        }

        private void FinishInput()
        {
            ItemList.Focus();
            WinManager.Instance.RemoveJoystickClick(JoyStickClick);
            WinManager.Instance.RemoveKeyDown(KeyboardClick);
        }

        #endregion

        #region 逻辑

        public void OrderList(HorizontalAlignment direct)
        {
            if (direct == HorizontalAlignment.Left)
            {
                if (_ItemPanel != null)
                {
                    _ItemPanel.FlowDirection = FlowDirection.LeftToRight;
                }
                BtnPanelLeft.Visibility = System.Windows.Visibility.Visible;
                BtnPanelRight.Visibility = System.Windows.Visibility.Hidden;

                BasePanel.HorizontalAlignment = HorizontalAlignment.Right;
            }
            else if (direct == HorizontalAlignment.Right)
            {
                if (_ItemPanel != null)
                {
                    _ItemPanel.FlowDirection = FlowDirection.RightToLeft;
                }
                BtnPanelLeft.Visibility = System.Windows.Visibility.Hidden;
                BtnPanelRight.Visibility = System.Windows.Visibility.Visible;

                BasePanel.HorizontalAlignment = HorizontalAlignment.Left;
            }
        }

        #endregion

    }
}
