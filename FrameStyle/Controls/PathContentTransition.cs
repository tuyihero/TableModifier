using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace UITemplate.Controls
{
    public enum PathContentTranstitionType
    {
        NO_ANIM = 0,
        LEFT_IN = 1,
        LEFT_OUT = 2,
        RIGHT_IN = 3,
        RIGHT_OUT = 4,
    }

    public class PathContentTransition : ContentControl
    {

        private const string ContentPresentationSiteName = "ContentPresentationSite";
        private ContentPresenter ContentPresentationSite { get; set; }

        private const string ContentLeftInName = "ContentLeftIn";
        private const string ContentLeftOutName = "ContentLeftOut";
        private const string ContentRightInName = "ContentRightIn";
        private const string ContentRightOutName = "ContentRightOut";
        private Storyboard StoryboardLeftIn { get; set; }
        private Storyboard StoryboardLeftOut { get; set; }
        private Storyboard StoryboardRightIn { get; set; }
        private Storyboard StoryboardRightOut { get; set; }

        private Action _ComplateCallBack = null;
        #region 

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ContentPresentationSite = GetTemplateChild(ContentPresentationSiteName) as ContentPresenter;

            StoryboardLeftIn = FindResource(ContentLeftInName) as Storyboard;
            if (StoryboardLeftIn != null)
            {
                if (StoryboardLeftIn.IsFrozen)
                {
                    StoryboardLeftIn = StoryboardLeftIn.Clone();
                    StoryboardLeftIn.Completed += OnTransitionCompleted;
                }
            }

            StoryboardLeftOut = FindResource(ContentLeftOutName) as Storyboard;
            if (StoryboardLeftOut != null)
            {
                if (StoryboardLeftOut.IsFrozen)
                {
                    StoryboardLeftOut = StoryboardLeftOut.Clone();
                    StoryboardLeftOut.Completed += OnTransitionCompleted;
                }
            }

            StoryboardRightIn = FindResource(ContentRightInName) as Storyboard;
            if (StoryboardRightIn != null)
            {
                if (StoryboardRightIn.IsFrozen)
                {
                    StoryboardRightIn = StoryboardRightIn.Clone();
                    StoryboardRightIn.Completed += OnTransitionCompleted;
                }
            }

            StoryboardRightOut = FindResource(ContentRightOutName) as Storyboard;
            if (StoryboardRightOut != null)
            {
                if (StoryboardRightOut.IsFrozen)
                {
                    StoryboardRightOut = StoryboardRightOut.Clone();
                    StoryboardRightOut.Completed += OnTransitionCompleted;
                }
            }

            if (ContentPresentationSite != null)
            {
                ContentPresentationSite.Content = Content;
            }

            //VisualStateManager.GoToState(this, NormalName, true);
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            if (ContentPresentationSite != null)
                ContentPresentationSite.Content = newContent;

            //if (StoryboardLeftIn != null)
            //    StoryboardLeftIn.Begin(this.ContentPresentationSite);
            //VisualStateManager.GoToState(this, ContentLeftInName, true);
        }

        #endregion

        #region 接口
        private object nextContext = null;
        private void OnTransitionCompleted(object sender, EventArgs e)
        {            
            Storyboard storyBoard = sender as Storyboard;
            if (storyBoard == null)
                return;

            //对部分先播动画在换内容的效果，此时替换内容
            if (storyBoard == StoryboardRightOut || storyBoard == StoryboardLeftOut)
            {
                this.Content = nextContext;
            }

            if (_ComplateCallBack != null)
                _ComplateCallBack();
        }


        public void ChangeContent(object content, PathContentTranstitionType tranType, Action callBack = null)
        {
            _ComplateCallBack = callBack;
            switch (tranType)
            {
                case PathContentTranstitionType.LEFT_IN:
                    this.Content = content;
                    if (StoryboardLeftIn != null)
                        StoryboardLeftIn.Begin(this.ContentPresentationSite);
                    break;
                case PathContentTranstitionType.LEFT_OUT:
                    nextContext = content;
                    if (StoryboardLeftOut != null)
                        StoryboardLeftOut.Begin(this.ContentPresentationSite);
                    break;
                case PathContentTranstitionType.RIGHT_IN:
                    this.Content = content;
                    if (StoryboardRightIn != null)
                        StoryboardRightIn.Begin(this.ContentPresentationSite);
                    break;
                case PathContentTranstitionType.RIGHT_OUT:
                    nextContext = content;
                    if (StoryboardRightOut != null)
                        StoryboardRightOut.Begin(this.ContentPresentationSite);
                    break;
                default:
                    this.Content = content;
                    break;
            }
        }

        #endregion
    }
}
