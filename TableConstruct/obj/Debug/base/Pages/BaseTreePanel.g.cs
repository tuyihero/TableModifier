﻿#pragma checksum "..\..\..\..\base\Pages\BaseTreePanel.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E570C1110BFA50B5386661E0F0F5A1FD"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace TableConstruct {
    
    
    /// <summary>
    /// BaseTreePanel
    /// </summary>
    public partial class BaseTreePanel : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 110 "..\..\..\..\base\Pages\BaseTreePanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid _TreeViewPanel;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\..\base\Pages\BaseTreePanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer _ScrollViewer;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\..\base\Pages\BaseTreePanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView _TreeView;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TableConstruct;component/base/pages/basetreepanel.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\base\Pages\BaseTreePanel.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 2:
            this._TreeViewPanel = ((System.Windows.Controls.Grid)(target));
            
            #line 110 "..\..\..\..\base\Pages\BaseTreePanel.xaml"
            this._TreeViewPanel.DragLeave += new System.Windows.DragEventHandler(this.TreeView_DragLeave_1);
            
            #line default
            #line hidden
            return;
            case 3:
            this._ScrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 4:
            this._TreeView = ((System.Windows.Controls.TreeView)(target));
            
            #line 113 "..\..\..\..\base\Pages\BaseTreePanel.xaml"
            this._TreeView.PreviewMouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.TreeView_MouseWheel_1);
            
            #line default
            #line hidden
            
            #line 114 "..\..\..\..\base\Pages\BaseTreePanel.xaml"
            this._TreeView.SelectedItemChanged += new System.Windows.RoutedPropertyChangedEventHandler<object>(this.TreeView_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 116 "..\..\..\..\base\Pages\BaseTreePanel.xaml"
            this._TreeView.PreviewMouseMove += new System.Windows.Input.MouseEventHandler(this.Border_PreviewMouseMove);
            
            #line default
            #line hidden
            
            #line 117 "..\..\..\..\base\Pages\BaseTreePanel.xaml"
            this._TreeView.DragEnter += new System.Windows.DragEventHandler(this.TreeView_DragEnter_1);
            
            #line default
            #line hidden
            
            #line 119 "..\..\..\..\base\Pages\BaseTreePanel.xaml"
            this._TreeView.DragOver += new System.Windows.DragEventHandler(this.TreeView_DragOver);
            
            #line default
            #line hidden
            
            #line 120 "..\..\..\..\base\Pages\BaseTreePanel.xaml"
            this._TreeView.Drop += new System.Windows.DragEventHandler(this.Border_Drop);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 125 "..\..\..\..\base\Pages\BaseTreePanel.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Remove);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 126 "..\..\..\..\base\Pages\BaseTreePanel.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Rename);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 127 "..\..\..\..\base\Pages\BaseTreePanel.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_NewItem);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 128 "..\..\..\..\base\Pages\BaseTreePanel.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_NewDirect);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 1:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.PreviewMouseRightButtonDownEvent;
            
            #line 50 "..\..\..\..\base\Pages\BaseTreePanel.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.TreeViewItem_PreviewMouseRightButtonDown);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}

