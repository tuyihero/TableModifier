using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UITemplate.Controls
{
    public class ListBaseFrame : Control
    {
        #region 属性

        protected ListContentFrame _ListFrame;
        protected List<PathChildFrame> _PathChildren;

        protected PathChildFrame FindChild(string name)
        {
            return _PathChildren.Find((pathItem) => pathItem.PathItem.DisplayName == name);
        }

        protected PathChildFrame FindLevel(int level)
        {
            return _PathChildren.Find((pathItem) => pathItem.PathItem.PathLevel == level);
        } 

        #endregion

        public ListBaseFrame()
        {
            this.DefaultStyleKey = typeof(ListBaseFrame);

            _PathChildren = new List<PathChildFrame>();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            object test = GetTemplateChild("ListFrame");
            this._ListFrame = GetTemplateChild("ListFrame") as ListContentFrame;

            OnApplyTemplateFinish();
        }

        #region toChild

        public virtual void OnApplyTemplateFinish()
        {
 
        }

        #endregion

        #region 固有

        public void PushPage(PathItem parentPath, string name, Type pageType, object param = null)
        {
            PathItem pathItem = CreateOrModify(parentPath, name, pageType, param);
            if (pathItem != null)
            {
                _ListFrame.PushContent(pathItem);
            }
        }

        public void Clear()
        {
            foreach (var child in _PathChildren)
            {
                _ListFrame.RemovePathItem(child.PathItem);
                child.PathItem.PathControl.Visibility = System.Windows.Visibility.Collapsed;
            }
            _PathChildren.Clear();
        }
        #endregion

        #region 私有

        private PathItem CreateOrModify(PathItem parentPath, string name, Type pageType, object param)
        {
            //int nextLevel = 0;
            //if (parentPath != null)
            //    nextLevel = parentPath.PathLevel + 1;

            //去掉更低级的项
            //foreach (PathChildFrame childFrame in _PathChildren)
            //{
            //    if (childFrame.PathItem.PathLevel >= nextLevel)
            //    {
            //        _PathFrame.RemovePathItem(childFrame.PathItem);
            //    }
            //}

            //PathChildFrame pathChild = FindLevel(nextLevel);
            //if (pathChild == null || pathChild.PathItem.PathControl.GetType() != pageType)
            //{
            //    object page = Activator.CreateInstance(pageType);
            //    UIElement pageUI = page as UIElement;
            //    if (pageUI == null)
            //        return null;

            //    PathChildFrame pageChild = page as PathChildFrame;
            //    if (pageChild == null)
            //        return null;

            //    PathItem pathItem = new PathItem { DisplayName = name, PathControl = pageUI, PathLevel = nextLevel };
            //    pageChild.PathBaseFrame = this;
            //    pageChild.PathItem = pathItem;
            //    pathItem.Width = pageChild.GetWidth();
            //    pageChild.ShowContent(param);

            //    _PathChildren.Add(pageChild);
            //    return pathItem;
            //}
            //else 
            //{
            //    pathChild.PathItem.DisplayName = name;
            //    pathChild.ShowContent(param);
            //    return pathChild.PathItem;
            //}

            PathChildFrame pathChild = FindChild(name);
            if (pathChild == null)
            {
                object page = Activator.CreateInstance(pageType);
                UIElement pageUI = page as UIElement;
                if (pageUI == null)
                    return null;

                PathChildFrame pageChild = page as PathChildFrame;
                if (pageChild == null)
                    return null;

                PathItem pathItem = new PathItem { DisplayName = name, PathControl = pageUI, PathLevel = 0 };
                pageChild.ParentBaseFrame = null;
                pageChild.PathItem = pathItem;
                pathItem.Width = pageChild.GetWidth();
                pageChild.ShowContent(param);

                _PathChildren.Add(pageChild);
                return pathItem;
            }
            else
            {
                pathChild.ShowContent(param);
                return pathChild.PathItem;
            }
        }

        #endregion
    }
}
