using System;
using System.Collections;
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
    public class DragInfo
    {
        public int PanelID { get; set; }
        public Border DragFrom { get; set; }
    }

    public class DragInsertEventArgs : EventArgs
    {
        public object DragFrom;
        public object DragTo;
        public bool IsDropFront;
    }

    /// <summary>
    /// BaseListPanel.xaml 的交互逻辑
    /// </summary>
    public partial class BaseListPanel : UserControl
    {
        #region id

        //静态ID，用于标识不同panel
        private static int _GlobleID = 0;

        //id
        public static readonly DependencyProperty PanelIDProperty = DependencyProperty.Register("PanelID", typeof(int), typeof(BaseListPanel), new PropertyMetadata(PanelIDChanged));
        private static void PanelIDChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            
        }
        public int PanelID
        {
            get { return (int)GetValue(PanelIDProperty); }
            set { SetValue(PanelIDProperty, value); }
        }

        public BaseListPanel()
        {
            InitializeComponent();
            PanelID = _GlobleID++;
        }

        #endregion

        #region 属性

        //高度
        public static readonly new DependencyProperty HeightProperty = DependencyProperty.Register("Height", typeof(double), typeof(BaseListPanel), new PropertyMetadata(OnHeightChanged));
        private static void OnHeightChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((BaseListPanel)o)._ListPanel.Height = (double)e.NewValue;
            ((BaseListPanel)o)._ScrollViewer.Height = (double)e.NewValue;
        }
        public new double Height
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        //数据源
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(object), typeof(BaseListPanel), new PropertyMetadata(ItemsSourceChanged));
        private static void ItemsSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((BaseListPanel)o)._LinkList.ItemsSource = (IEnumerable)e.NewValue;
        }
        public object ItemsSource
        {
            get { return GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        //选中项
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(BaseListPanel), new PropertyMetadata(SelectedItemChanged));
        private static void SelectedItemChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {

        }
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        //选中事件
        public static readonly DependencyProperty SelectEventProperty = DependencyProperty.Register("SelectEvent", typeof(EventHandler<SelectionChangedEventArgs>), typeof(BaseListPanel), new PropertyMetadata(OnSelectEventChanged));
        private static void OnSelectEventChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {

        }
        public EventHandler<SelectionChangedEventArgs> SelectEvent
        {
            get { return (EventHandler<SelectionChangedEventArgs>)GetValue(SelectEventProperty); }
            set { SetValue(SelectEventProperty, value); }
        }

        //删除事件
        public static readonly DependencyProperty RemoveEventProperty = DependencyProperty.Register("RemoveEvent", typeof(EventHandler<RoutedEventArgs>), typeof(BaseListPanel), new PropertyMetadata(OnRemoveEventChanged));
        private static void OnRemoveEventChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {

        }
        public EventHandler<RoutedEventArgs> RemoveEvent
        {
            get { return (EventHandler<RoutedEventArgs>)GetValue(RemoveEventProperty); }
            set { SetValue(RemoveEventProperty, value); }
        }

        //改名事件
        public static readonly DependencyProperty RenameEventProperty = DependencyProperty.Register("RenameEvent", typeof(EventHandler<RoutedEventArgs>), typeof(BaseListPanel), new PropertyMetadata(OnRenameEventChanged));
        private static void OnRenameEventChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {

        }
        public EventHandler<RoutedEventArgs> RenameEvent
        {
            get { return (EventHandler<RoutedEventArgs>)GetValue(RenameEventProperty); }
            set { SetValue(SelectEventProperty, value); }
        }

        //位置修改事件
        public static readonly DependencyProperty DragInsertEventProperty = DependencyProperty.Register("DragInsertEvent", typeof(EventHandler<DragInsertEventArgs>), typeof(BaseListPanel), new PropertyMetadata(OnDragInsertEventChanged));
        private static void OnDragInsertEventChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            
        }
        public EventHandler<DragInsertEventArgs> DragInsertEvent
        {
            get { return (EventHandler<DragInsertEventArgs>)GetValue(DragInsertEventProperty); }
            set { SetValue(DragInsertEventProperty, value); }
        }

        #endregion

        #region 逻辑属性

        private Border _DragOverItem = null;
        private bool _IsButtom = false;

        #endregion


        #region 事件


        private void LinkList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItem = _LinkList.SelectedItem;
            if (SelectEvent != null)
            {
                SelectEvent(this, e);
            }
        }

        private void MenuItem_Remove(object sender, RoutedEventArgs e)
        {
            if (RemoveEvent != null)
            {
                RemoveEvent(this, e);
            }
        }

        private void MenuItem_Rename(object sender, RoutedEventArgs e)
        {
            if (RenameEvent != null)
            {
                RenameEvent(this, e);
            }
        }

        private void Border_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (DragInsertEvent == null)
                return;

            Border panel = GetBorderFromMousePos(e);
            if (panel == null)
                return;

            if (e.LeftButton == MouseButtonState.Pressed && _LinkList.SelectedItem != null)
            {
                DragInfo dragInfo = new DragInfo();
                dragInfo.PanelID = PanelID;
                dragInfo.DragFrom = panel;

                DataObject data = new DataObject(typeof(DragInfo), dragInfo);
                DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
            }
        }

        private void Border_Drop(object sender, DragEventArgs e)
        {
            if (DragInsertEvent == null)
                return;

            if (!e.Data.GetDataPresent(typeof(DragInfo)))
                return;

            DragInfo data = e.Data.GetData(typeof(DragInfo)) as DragInfo;
            Border panel = _DragOverItem;


            if (panel != null && panel != data.DragFrom && PanelID == data.PanelID)
            {
                panel.BorderThickness = new Thickness(0);

                DragInsertEventArgs insertEvent = new DragInsertEventArgs();
                insertEvent.DragFrom = data.DragFrom.DataContext;
                insertEvent.DragTo = panel.DataContext;

                if (!_IsButtom)
                {
                    insertEvent.IsDropFront = true;
                }
                else
                {
                    insertEvent.IsDropFront = false;
                }

                DragInsertEvent(this, insertEvent);
            }
            else
            {
                if (_DragOverItem != null)
                {
                    _DragOverItem.BorderThickness = new Thickness(0);
                }
            }
        }

        private void LinkList_DragEnter_1(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(DragInfo)))
                return;

            DragInfo data = e.Data.GetData(typeof(DragInfo)) as DragInfo;
            if (data == null)
                return;

            if (data.PanelID != PanelID)
                return;

            Border panel = GetBorderFromMousePos(e);
            if (panel == null)
                return;

            if (panel != null && PanelID == data.PanelID)
            {
                //清理之前的
                if (_DragOverItem != null)
                {
                    _DragOverItem.BorderThickness = new Thickness(0);
                }
                _DragOverItem = panel;

                if (_LinkList.Items[_LinkList.Items.Count - 1] == panel.DataContext)
                {
                    Point point = e.GetPosition(panel);
                    if (point.Y < panel.ActualHeight * 0.5d)
                    {
                        _IsButtom = false;
                        panel.BorderThickness = new Thickness(0, 5, 0, 0);
                    }
                    else
                    {
                        _IsButtom = true;
                        panel.BorderThickness = new Thickness(0, 0, 0, 5);
                    }
                }
                else
                {
                    _IsButtom = false;
                    panel.BorderThickness = new Thickness(0, 5, 0, 0);
                }
            }
            else
            {
                //e.Effects = DragDropEffects.None;
            }

            //是否需要滚动
            Point panelPos = e.GetPosition(_ListPanel);
            if (panelPos.Y < panel.ActualHeight)
            {
                _ScrollViewer.ScrollToVerticalOffset(_ScrollViewer.VerticalOffset - panel.ActualHeight);
            }
            else if (panelPos.Y > (this.ActualHeight - panel.ActualHeight))
            {
                _ScrollViewer.ScrollToVerticalOffset(_ScrollViewer.VerticalOffset + panel.ActualHeight);
            }
        }

        private void LinkList_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(DragInfo)))
                return;

            DragInfo data = e.Data.GetData(typeof(DragInfo)) as DragInfo;
            if (data == null)
                return;

            if (data.PanelID != PanelID)
                return;

            Border panel = GetBorderFromMousePos(e);
            if (panel != null && panel != data.DragFrom && PanelID == data.PanelID)
            {
                if (_LinkList.Items[_LinkList.Items.Count - 1] == panel.DataContext)
                {
                    Point point = e.GetPosition(panel);
                    if (point.Y < panel.ActualHeight * 0.5d)
                    {
                        _IsButtom = false;
                        panel.BorderThickness = new Thickness(0, 5, 0, 0);
                    }
                    else
                    {
                        _IsButtom = true;
                        panel.BorderThickness = new Thickness(0, 0, 0, 5);
                    }
                }
            }
        }
        
        private void LinkList_DragLeave_1(object sender, DragEventArgs e)
        {
            if (sender is StackPanel)
                return;

            if (!e.Data.GetDataPresent(typeof(DragInfo)))
                return;

            DragInfo data = e.Data.GetData(typeof(DragInfo)) as DragInfo;
            if (data == null)
                return;

            if (data.PanelID != PanelID)
                return;

            Border panel = GetBorderFromMousePos(e);
            if (panel == null)
            {
                if (_DragOverItem != null)
                {
                    _DragOverItem.BorderThickness = new Thickness(0);
                }
                _DragOverItem = null;
            }
            
        }

        private void LinkList_MouseWheel_1(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (_ScrollViewer != null)
            {
                _ScrollViewer.ScrollToVerticalOffset(_ScrollViewer.VerticalOffset - e.Delta);
            }
        }

        #endregion 

        
        #region 

        private void SetAllowDrag(bool allowDrag)
        {
            foreach (object item in _LinkList.Items)
            {
                ListBoxItem myListBoxItem =
                    (ListBoxItem)(_LinkList.ItemContainerGenerator.ContainerFromItem(item));

                // Getting the ContentPresenter of myListBoxItem
                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);

                // Finding textBlock from the DataTemplate that is set on that ContentPresenter
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                Border myBorder = (Border)myDataTemplate.FindName("listItemBorder", myContentPresenter);
                myBorder.AllowDrop = allowDrag;
            }
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        private Border GetBorderFromMousePos(DragEventArgs e)
        {
            Point pos = e.GetPosition(_LinkList);
            HitTestResult result = VisualTreeHelper.HitTest(_LinkList, pos);
            if (result == null)
                return null;

            Border panel = VisualTreeHelper.GetParent(result.VisualHit) as Border;
            if (panel == null)
                panel = result.VisualHit as Border;

            if (panel == null)
                return null;

            return panel;
        }

        private Border GetBorderFromMousePos(MouseEventArgs e)
        {
            Point pos = e.GetPosition(_LinkList);
            HitTestResult result = VisualTreeHelper.HitTest(_LinkList, pos);
            if (result == null)
                return null;

            Border panel = VisualTreeHelper.GetParent(result.VisualHit) as Border;
            if(panel == null)
                panel = result.VisualHit as Border;

            if (panel == null)
                return null;

            return panel;
        }
        #endregion

        #region 方法
        //private Dictionary<string, EventHandler<RoutedEventArgs>> _MenuDictionary = new Dictionary<string, EventHandler<RoutedEventArgs>>();

        public void AddContexMenu(string menuName, RoutedEventHandler contexMenu)
        {
            //_MenuDictionary.Add(menuName, contexMenu);
            MenuItem menuItem = new MenuItem();
            menuItem.Header = menuName;
            menuItem.Click += contexMenu;

            _LinkList.ContextMenu.Items.Add(menuItem);
        }

        #endregion
    }
}
