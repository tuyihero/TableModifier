using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// BaseTreePanel.xaml 的交互逻辑
    /// </summary>
    public partial class BaseTreePanel : UserControl
    {
        #region id

        //静态ID，用于标识不同panel
        private static int _GlobleID = 0;

        //id
        public static readonly DependencyProperty TreeIDProperty = DependencyProperty.Register("TreeID", typeof(int), typeof(BaseTreePanel), new PropertyMetadata(TreeIDChanged));
        private static void TreeIDChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            
        }
        public int TreeID
        {
            get { return (int)GetValue(TreeIDProperty); }
            set { SetValue(TreeIDProperty, value); }
        }

        public BaseTreePanel()
        {
            InitializeComponent();
            TreeID = _GlobleID++;
        }

        #endregion

        #region 属性

        //高度
        public static readonly new DependencyProperty HeightProperty = DependencyProperty.Register("TreeHeight", typeof(double), typeof(BaseTreePanel), new PropertyMetadata(OnHeightChanged));
        private static void OnHeightChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((BaseTreePanel)o)._TreeViewPanel.Height = (double)e.NewValue;
            ((BaseTreePanel)o)._ScrollViewer.Height = (double)e.NewValue;
        }
        public new double Height
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        //数据源
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("TreeItemsSource", typeof(object), typeof(BaseTreePanel), new PropertyMetadata(ItemsSourceChanged));
        private static void ItemsSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            //((BaseTreePanel)o)._TreeView.ItemsSource = (IEnumerable)e.NewValue;
            ((BaseTreePanel)o).SetTreeDataContex((IEnumerable)e.NewValue);
        }
        public object ItemsSource
        {
            get { return GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        //选中项
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("TreeSelectedItem", typeof(object), typeof(BaseTreePanel), new PropertyMetadata(SelectedItemChanged));
        private static void SelectedItemChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {

        }
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        //选中事件
        public static readonly DependencyProperty SelectEventProperty = DependencyProperty.Register("TreeSelectEvent", typeof(EventHandler<RoutedPropertyChangedEventArgs<object>>), typeof(BaseTreePanel), new PropertyMetadata(OnSelectEventChanged));
        private static void OnSelectEventChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {

        }
        public EventHandler<RoutedPropertyChangedEventArgs<object>> SelectEvent
        {
            get { return (EventHandler<RoutedPropertyChangedEventArgs<object>>)GetValue(SelectEventProperty); }
            set { SetValue(SelectEventProperty, value); }
        }

        //新建事件
        public static readonly DependencyProperty NewItemEventProperty = DependencyProperty.Register("TreeNewItemEvent", typeof(EventHandler<RoutedEventArgs>), typeof(BaseTreePanel), new PropertyMetadata(OnNewItemEventChanged));
        private static void OnNewItemEventChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {

        }
        public EventHandler<RoutedEventArgs> NewItemEvent
        {
            get { return (EventHandler<RoutedEventArgs>)GetValue(NewItemEventProperty); }
            set { SetValue(NewItemEventProperty, value); }
        }

        //删除事件
        public static readonly DependencyProperty RemoveEventProperty = DependencyProperty.Register("TreeRemoveEvent", typeof(EventHandler<RoutedEventArgs>), typeof(BaseTreePanel), new PropertyMetadata(OnRemoveEventChanged));
        private static void OnRemoveEventChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {

        }
        public EventHandler<RoutedEventArgs> RemoveEvent
        {
            get { return (EventHandler<RoutedEventArgs>)GetValue(RemoveEventProperty); }
            set { SetValue(RemoveEventProperty, value); }
        }

        //改名事件
        public static readonly DependencyProperty RenameEventProperty = DependencyProperty.Register("TreeRenameEvent", typeof(EventHandler<RoutedEventArgs>), typeof(BaseTreePanel), new PropertyMetadata(OnRenameEventChanged));
        private static void OnRenameEventChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {

        }
        public EventHandler<RoutedEventArgs> RenameEvent
        {
            get { return (EventHandler<RoutedEventArgs>)GetValue(RenameEventProperty); }
            set { SetValue(RenameEventProperty, value); }
        }

        //位置修改事件
        public static readonly DependencyProperty DragInsertEventProperty = DependencyProperty.Register("TreeDragInsertEvent", typeof(EventHandler<DragInsertEventArgs>), typeof(BaseTreePanel), new PropertyMetadata(OnDragInsertEventChanged));
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

        public class TreeData
        {
            public ObservableCollection<Object> Children { get; set; }
            public string Name {get;set;}
            public string Path { get; set; }

            public TreeData()
            {
                Children = new ObservableCollection<object>();
            }
        }
        private ObservableCollection<Object> _TreeDataCollection = new ObservableCollection<Object>();
        public ObservableCollection<Object> TreeDataCollection { get { return _TreeDataCollection; } }
        #endregion


        #region 事件


        private void TreeView_SelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SelectedItem = _TreeView.SelectedItem;
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

        private void MenuItem_NewItem(object sender, RoutedEventArgs e)
        {
            if (NewItemEvent != null)
            {
                TreeData pathNode = null;
                if (SelectedItem != null)
                {
                    if (SelectedItem is TreeData)
                    {
                        pathNode = SelectedItem as TreeData;
                    }
                    else
                    {
                        var field = SelectedItem.GetType().GetProperty("Path");
                        string pathValue = "";
                        if (field != null)
                        {
                            pathValue = field.GetValue(SelectedItem) as string;
                        }
                        pathNode = FindTreeNode(pathValue);
                    }
                }
                else
                {
                    pathNode = FindTreeNode("");
                }

                NewItemEvent(pathNode, e);
            }
        }

        private void MenuItem_NewDirect(object sender, RoutedEventArgs e)
        {
            //SelectedItem = _TreeView.SelectedItem;

            string newName = DialogMessage.DialogString();
            if (string.IsNullOrEmpty(newName))
            {
                return;
            }

            TreeData newNode = new TreeData();
            newNode.Name = newName;

            if (SelectedItem == null)
            {
                newNode.Path = "";
                _TreeDataCollection.Add(newNode);
                return;
            }

            if (SelectedItem is TreeData)
            {
                TreeData selectTreeNode = SelectedItem as TreeData;
                newNode.Path = selectTreeNode.Name;
                selectTreeNode.Children.Add(newNode);
                return;
            }

            var field = SelectedItem.GetType().GetProperty("Path");

            string pathValue = "";
            if (field != null)
            {
                pathValue = field.GetValue(SelectedItem) as string;
            }

            TreeData treeNode = FindTreeNode(pathValue);
            
            

            if (treeNode != null)
            {
                newNode.Path = treeNode.Path + "\\" + treeNode.Name;
                treeNode.Children.Add(newNode);
            }
            else
            {
                newNode.Path = treeNode.Name;
                _TreeDataCollection.Add(newNode);
            }

            _TreeView.ItemsSource = _TreeDataCollection;
        }

        private void Border_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (DragInsertEvent == null)
                return;

            Border panel = GetBorderFromMousePos(e);
            if (panel == null)
                return;

            if (e.LeftButton == MouseButtonState.Pressed && _TreeView.SelectedItem != null)
            {
                DragInfo dragInfo = new DragInfo();
                dragInfo.PanelID = TreeID;
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


            if (panel != null && panel != data.DragFrom && TreeID == data.PanelID)
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

        private void TreeView_DragEnter_1(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(DragInfo)))
                return;

            DragInfo data = e.Data.GetData(typeof(DragInfo)) as DragInfo;
            if (data == null)
                return;

            if (data.PanelID != TreeID)
                return;

            Border panel = GetBorderFromMousePos(e);
            if (panel == null)
                return;

            if (panel != null && TreeID == data.PanelID)
            {
                //清理之前的
                if (_DragOverItem != null)
                {
                    _DragOverItem.BorderThickness = new Thickness(0);
                }
                _DragOverItem = panel;

                if (_TreeView.Items[_TreeView.Items.Count - 1] == panel.DataContext)
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
            Point panelPos = e.GetPosition(_TreeViewPanel);
            if (panelPos.Y < panel.ActualHeight)
            {
                _ScrollViewer.ScrollToVerticalOffset(_ScrollViewer.VerticalOffset - panel.ActualHeight);
            }
            else if (panelPos.Y > (this.ActualHeight - panel.ActualHeight))
            {
                _ScrollViewer.ScrollToVerticalOffset(_ScrollViewer.VerticalOffset + panel.ActualHeight);
            }
        }

        private void TreeView_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(DragInfo)))
                return;

            DragInfo data = e.Data.GetData(typeof(DragInfo)) as DragInfo;
            if (data == null)
                return;

            if (data.PanelID != TreeID)
                return;

            Border panel = GetBorderFromMousePos(e);
            if (panel != null && panel != data.DragFrom && TreeID == data.PanelID)
            {
                if (_TreeView.Items[_TreeView.Items.Count - 1] == panel.DataContext)
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
        
        private void TreeView_DragLeave_1(object sender, DragEventArgs e)
        {
            if (sender is StackPanel)
                return;

            if (!e.Data.GetDataPresent(typeof(DragInfo)))
                return;

            DragInfo data = e.Data.GetData(typeof(DragInfo)) as DragInfo;
            if (data == null)
                return;

            if (data.PanelID != TreeID)
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

        private void TreeView_MouseWheel_1(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (_ScrollViewer != null)
            {
                _ScrollViewer.ScrollToVerticalOffset(_ScrollViewer.VerticalOffset - e.Delta);
            }
        }

        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
                source = VisualTreeHelper.GetParent(source);

            return source;
        }
        #endregion 

        
        #region 

        private void SetAllowDrag(bool allowDrag)
        {
            foreach (object item in _TreeView.Items)
            {
                ListBoxItem myListBoxItem =
                    (ListBoxItem)(_TreeView.ItemContainerGenerator.ContainerFromItem(item));

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
            Point pos = e.GetPosition(_TreeView);
            HitTestResult result = VisualTreeHelper.HitTest(_TreeView, pos);
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
            Point pos = e.GetPosition(_TreeView);
            HitTestResult result = VisualTreeHelper.HitTest(_TreeView, pos);
            if (result == null)
                return null;

            Border panel = VisualTreeHelper.GetParent(result.VisualHit) as Border;
            if(panel == null)
                panel = result.VisualHit as Border;

            if (panel == null)
                return null;

            return panel;
        }

        private void SetTreeDataContex(IEnumerable itemSource)
        {
            foreach (var obj in itemSource)
            {
                //var obj = itemSource.GetEnumerator().Current;
                var field = obj.GetType().GetProperty("Path");

                string pathValue = "";
                if (field != null)
                {
                    pathValue = field.GetValue(obj) as string;
                }

                var nameField = obj.GetType().GetProperty("Name");

                string nameValue = "";
                if (nameField != null)
                {
                    nameValue = nameField.GetValue(obj) as string;
                }
                if (string.IsNullOrEmpty(nameValue))
                {
                    continue;
                }

                TreeData treeNode = FindTreeNode(pathValue);

                if (treeNode != null)
                {
                    treeNode.Children.Add(obj);
                }
                else
                {
                    _TreeDataCollection.Add(obj);
                }
            }
            

            _TreeView.ItemsSource = _TreeDataCollection;
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

            _TreeView.ContextMenu.Items.Add(menuItem);
        }

        private TreeData FindTreeNode(string path)
        {
            string[] nodeNames = path.Split('\\');
            ObservableCollection<Object> treeNodeDatas = _TreeDataCollection;
            TreeData findedTree = null;
            for (int i = 0; i < nodeNames.Length; ++i)
            {
                if (string.IsNullOrEmpty(nodeNames[i]))
                {
                    continue;
                }
                TreeData treeNodeData =
                treeNodeDatas.SingleOrDefault((treeNode) =>
                {
                    TreeData treeData = treeNode as TreeData;
                    if (treeData == null)
                        return false;

                    if (nodeNames[i] == treeData.Name)
                        return true;

                    return false;
                }) as TreeData;

                if (treeNodeData != null)
                {
                    treeNodeDatas = treeNodeData.Children;
                    findedTree = treeNodeData;
                    continue;
                }
                else
                {
                    TreeData newNode = new TreeData();
                    newNode.Name = nodeNames[i];
                    for (int j = 0; j < i; j++)
                    {
                        newNode.Path += nodeNames[j] + "\\";
                    }

                    treeNodeDatas.Add(newNode);
                    treeNodeDatas = newNode.Children = new ObservableCollection<Object>();
                    findedTree = newNode;
                }
            }
            return findedTree;
        }
        #endregion


        
    }
}
