﻿#pragma checksum "..\..\..\base\ItemVector3Control.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BDED16107313DFE44C25779760A13FFA"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34209
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


namespace TableContent {
    
    
    /// <summary>
    /// ItemVector3Control
    /// </summary>
    public partial class ItemVector3Control : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\base\ItemVector3Control.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ItemBorder;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\base\ItemVector3Control.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ItemIDMultiValue;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\base\ItemVector3Control.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox VectorX;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\base\ItemVector3Control.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox VectorY;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\base\ItemVector3Control.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox VectorZ;
        
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
            System.Uri resourceLocater = new System.Uri("/TableContent;component/base/itemvector3control.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\base\ItemVector3Control.xaml"
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
            case 1:
            
            #line 8 "..\..\..\base\ItemVector3Control.xaml"
            ((System.Windows.Controls.Grid)(target)).DataContextChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.Grid_DataContextChanged_1);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ItemBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.ItemIDMultiValue = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.VectorX = ((System.Windows.Controls.TextBox)(target));
            
            #line 18 "..\..\..\base\ItemVector3Control.xaml"
            this.VectorX.GotFocus += new System.Windows.RoutedEventHandler(this.Item_GotFocus_1);
            
            #line default
            #line hidden
            
            #line 19 "..\..\..\base\ItemVector3Control.xaml"
            this.VectorX.LostFocus += new System.Windows.RoutedEventHandler(this.Item_LostFocus_1);
            
            #line default
            #line hidden
            return;
            case 5:
            this.VectorY = ((System.Windows.Controls.TextBox)(target));
            
            #line 24 "..\..\..\base\ItemVector3Control.xaml"
            this.VectorY.GotFocus += new System.Windows.RoutedEventHandler(this.Item_GotFocus_1);
            
            #line default
            #line hidden
            
            #line 25 "..\..\..\base\ItemVector3Control.xaml"
            this.VectorY.LostFocus += new System.Windows.RoutedEventHandler(this.Item_LostFocus_1);
            
            #line default
            #line hidden
            return;
            case 6:
            this.VectorZ = ((System.Windows.Controls.TextBox)(target));
            
            #line 30 "..\..\..\base\ItemVector3Control.xaml"
            this.VectorZ.GotFocus += new System.Windows.RoutedEventHandler(this.Item_GotFocus_1);
            
            #line default
            #line hidden
            
            #line 31 "..\..\..\base\ItemVector3Control.xaml"
            this.VectorZ.LostFocus += new System.Windows.RoutedEventHandler(this.Item_LostFocus_1);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
