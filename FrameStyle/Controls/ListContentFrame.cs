using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows;

namespace UITemplate.Controls
{
    /// <summary>
    /// Represents a control that contains multiple pages that share the same space on screen.
    /// </summary>
    public class ListContentFrame
        : Control
    {
        #region 属性

        private PathItemCollection _PathItemCollection = null;
        private PathItem _StackRightData = null;

        //UI
        private ListBox _LinkList;
        private PathContentTransition _StackLeft;
        private PathContentTransition _StackRight;
        private Grid _Grid;

        #endregion

        #region UI部分
        /// <summary>
        /// Initializes a new instance of the <see cref="ModernTab"/> control.
        /// </summary>
        public ListContentFrame()
        {
            this.DefaultStyleKey = typeof(PathContentFrame);

            _PathItemCollection = new PathItemCollection();
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call System.Windows.FrameworkElement.ApplyTemplate().
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _LinkList = GetTemplateChild("LinkList") as ListBox;
            if (_LinkList != null)
            {
                _LinkList.ItemsSource = _PathItemCollection;
                _LinkList.SelectionChanged += OnLinkListSelectionChanged;
            }

            this._StackLeft = GetTemplateChild("ContentFrame") as PathContentTransition;

            //列表中如果有信息，显示在左边
            ShowLeftContent();
        }

        private void OnLinkListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var link = this._LinkList.SelectedItem as PathItem;
            if (link != null)
            {
                ShowLeftContent(link);
            }
        }

        #endregion

        #region 逻辑部分
        public void PushContent(PathItem pathItem)
        {
            if (!_PathItemCollection.Contains(pathItem))
            {
                _PathItemCollection.Add(pathItem);
                ShowLeftContent();
            }
            else
            {
                ShowLeftContent(pathItem);
            }

        }

        //private void ShowRightContent()
        //{
        //    if (_StackRight!= null && _StackRightData != null)
        //    {
        //        _StackRight.ChangeContent(_StackRightData.PathControl, PathContentTranstitionType.RIGHT_IN);
        //        _Grid.ColumnDefinitions[0].BringIntoView();
        //    }
        //}

        private void ShowLeftContent()
        {
            if (_StackLeft!= null && _PathItemCollection.Count > 0)
            {
                _LinkList.SelectedIndex = _PathItemCollection.Count -1;
                _StackLeft.ChangeContent(_PathItemCollection.Last<PathItem>().PathControl, PathContentTranstitionType.LEFT_IN);
            }
        }

        private void ShowLeftContent(PathContentTranstitionType animType)
        {
            if (_StackLeft != null && _PathItemCollection.Count > 0)
            {
                _LinkList.SelectedIndex = _PathItemCollection.Count - 1;
                _StackLeft.ChangeContent(_PathItemCollection.Last<PathItem>().PathControl, animType);
            }
        }

        private void ShowLeftContent(PathItem pathItem)
        {
            if (_StackLeft != null)
            {
                int index = _PathItemCollection.IndexOf(pathItem);
                _LinkList.SelectedIndex = index;
                _StackLeft.ChangeContent(pathItem.PathControl, PathContentTranstitionType.LEFT_IN);
            }
        }

        public void RemovePathItem(PathItem pathItem)
        {
            if (_StackRightData == pathItem)
            {
                _StackRightData = null;
            }
            else
            {
                _PathItemCollection.Remove(pathItem);
            }
        }
        #endregion
    }
}
