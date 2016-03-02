using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows;

namespace UITemplate.Controls
{
    public enum TabPopType
    {
        POP_MOUSE_OVER = 1,
        POP_CLICK = 2
    }

    public class TabContentFrame : Control
    {
        #region 属性
        public static readonly DependencyProperty TabsProperty = DependencyProperty.Register("Tabs", typeof(TabItemCollection), typeof(TabContentFrame), new PropertyMetadata(OnTabsChanged));
        private static void OnTabsChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            
        }
        public TabItemCollection Tabs
        {
            get { return (TabItemCollection)GetValue(TabsProperty); }
            set { SetValue(TabsProperty, value); }
        }


        //主内容
        public static readonly DependencyProperty RightContentProperty = DependencyProperty.Register("RightContent", typeof(object), typeof(TabContentFrame), new PropertyMetadata(OnRightContentChanged));
        private static void OnRightContentChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((TabContentFrame)o).RightContent = e.NewValue;
            ((TabContentFrame)o).ShowRightContent();
        }
        public object RightContent
        {
            get { return GetValue(RightContentProperty); }
            set { SetValue(RightContentProperty, value); }
        }

        //控件
        private ListBox _TabList;
        private PathContentTransition _RightContent;
        private PathContentTransition _TabContent;
        #endregion

        public TabContentFrame()
        {
            this.DefaultStyleKey = typeof(TabContentFrame);

            SetValue(TabsProperty, new TabItemCollection());
        }

        #region UI

        private bool _IsTabContentShow = false;
        private string _TabDefaultShow = "";

        public void HideTabContent()
        {
            _IsTabContentShow = false;
            //_TabContent.ChangeContent(null, PathContentTranstitionType.LEFT_OUT, () => { _TabContent.Visibility = System.Windows.Visibility.Hidden; _TabContent.IsEnabled = false; });
            _TabContent.Visibility = System.Windows.Visibility.Hidden;
            _TabList.SelectedItem = null;
        }

        public void HideTabContent(object sender, EventArgs e)
        {
            if (!_IsTabContentShow)
                return;

            HideTabContent();
        }

        public void ShowTabContent(string tabName)
        {
            //未初始化完成，记录并直接返回
            if (_TabContent == null)
            {
                _TabDefaultShow = tabName;
                return;
            }

            var tabItem = GetTabItem(tabName);
            if (tabItem != null)
            {
                if (_IsTabContentShow && _TabContent.Content == tabItem.TabControl)
                    return;

                _IsTabContentShow = true;
                _TabContent.Visibility = System.Windows.Visibility.Visible;
                _TabContent.IsEnabled = true;
                _TabContent.ChangeContent(tabItem.TabControl, PathContentTranstitionType.LEFT_IN);
                _TabContent.Focus();

                var tabShow = tabItem.TabControl as ISelectTabItem;
                if (tabShow != null)
                {
                    tabShow.SelectedShow();
                }

                this._TabList.SelectedItem = null;
            }
        }

        private void ShowTabContent(object sender, EventArgs e)
        {
            Border tabBorder = sender as Border;
            if (tabBorder == null)
                return;

            ShowTabContent(tabBorder.Name);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._RightContent = GetTemplateChild("RightContent") as PathContentTransition;
            if (_RightContent != null)
            {
                ShowRightContent();
            }

            this._TabContent = GetTemplateChild("TabContent") as PathContentTransition;

            this._TabList = GetTemplateChild("TabList") as ListBox;
            if (this._TabList != null)
            {
                this._TabList.SelectedItem = null;
                //this._TabList.SelectionChanged += ShowTabContent;

                foreach (TabItem tabItem in Tabs)
                {
                    AddUITabShowList(tabItem);
                }
            }

            //显示预订的tab
            if (!string.IsNullOrEmpty(_TabDefaultShow))
            {
                ShowTabContent(_TabDefaultShow);
                _TabDefaultShow = "";
            }
        }


        public void ShowRightContent()
        {
            if (_RightContent != null)
            {
                _RightContent.ChangeContent(RightContent, PathContentTranstitionType.NO_ANIM);
            }
        }

        private void AddUITabShowList(TabItem tabItem)
        {
            if (_TabContent == null)
                return;

            Border border = new Border();
            border.Name = tabItem.DisplayName;
            border.SetResourceReference(Border.StyleProperty, "TabItemBorderWhite");

            TextBlock textBlock = new TextBlock();
            textBlock.Text = tabItem.DisplayName;
            textBlock.SetResourceReference(TextBlock.StyleProperty, "TabItemWhite");

            border.Child = textBlock;

            //设置显示事件
            if (tabItem.PopType == TabPopType.POP_CLICK)
            {//点击显示，选择列表show，失去焦点hide
                border.MouseUp -= ShowTabContent;
                border.MouseUp += ShowTabContent;

                _TabContent.LostFocus -= HideTabContent;
                _TabContent.LostFocus += HideTabContent;
            }
            else if (tabItem.PopType == TabPopType.POP_MOUSE_OVER)
            {
                border.MouseEnter -= ShowTabContent;
                border.MouseEnter += ShowTabContent;

                _TabContent.MouseLeave -= HideTabContent;
                _TabContent.MouseLeave += HideTabContent;
            }
          
            _TabList.Items.Add(border);
        }

        #endregion

        #region 接口

        public void AddTabItem(string tabName, UIElement tabContent, TabPopType popStyle = TabPopType.POP_CLICK)
        {
            UITemplate.Controls.TabItem tabItem = new UITemplate.Controls.TabItem();
            tabItem.DisplayName = tabName;
            tabItem.TabControl = tabContent;
            tabItem.PopType = popStyle;
            Tabs.Add(tabItem);

            if (_TabList != null)
            {
                AddUITabShowList(tabItem);
            }
        }

        public void RemoveTabItem(string tabName)
        {
            Tabs.Remove(Tabs.FindTabByName(tabName));
        }

        public TabItem GetTabItem(string tabName)
        {
            return Tabs.FindTabByName(tabName);
        }

        #endregion
    }
}
