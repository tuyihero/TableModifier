﻿#pragma checksum "..\..\..\base\ContentItemBase.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "99069E23C0D08326E5692E6043B4F7C6"
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
using TableContent;


namespace TableContent {
    
    
    /// <summary>
    /// ContentItemBase
    /// </summary>
    public partial class ContentItemBase : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\base\ContentItemBase.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ItemBorder;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\base\ContentItemBase.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ItemLabel;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\base\ContentItemBase.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ItemTextValue;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\base\ContentItemBase.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ItemBoolValue;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\base\ContentItemBase.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TableContent.ItemVector3Control ItemVector3Value;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\base\ContentItemBase.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ItemEnmuValue;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\base\ContentItemBase.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ItemIDSingleValue;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\base\ContentItemBase.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TableContent.MultiTableControl ItemIDMultiValue;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\base\ContentItemBase.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border errorBorder;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\base\ContentItemBase.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock errorText;
        
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
            System.Uri resourceLocater = new System.Uri("/TableContent;component/base/contentitembase.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\base\ContentItemBase.xaml"
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
            
            #line 9 "..\..\..\base\ContentItemBase.xaml"
            ((System.Windows.Controls.Grid)(target)).DataContextChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.Grid_DataContextChanged_1);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ItemBorder = ((System.Windows.Controls.Border)(target));
            
            #line 15 "..\..\..\base\ContentItemBase.xaml"
            this.ItemBorder.Loaded += new System.Windows.RoutedEventHandler(this.ItemBorder_Loaded_1);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ItemLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.ItemTextValue = ((System.Windows.Controls.TextBox)(target));
            
            #line 28 "..\..\..\base\ContentItemBase.xaml"
            this.ItemTextValue.GotFocus += new System.Windows.RoutedEventHandler(this.Item_GotFocus_1);
            
            #line default
            #line hidden
            
            #line 29 "..\..\..\base\ContentItemBase.xaml"
            this.ItemTextValue.LostFocus += new System.Windows.RoutedEventHandler(this.Item_LostFocus_1);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ItemBoolValue = ((System.Windows.Controls.CheckBox)(target));
            
            #line 37 "..\..\..\base\ContentItemBase.xaml"
            this.ItemBoolValue.GotFocus += new System.Windows.RoutedEventHandler(this.Item_GotFocus_1);
            
            #line default
            #line hidden
            
            #line 38 "..\..\..\base\ContentItemBase.xaml"
            this.ItemBoolValue.LostFocus += new System.Windows.RoutedEventHandler(this.Item_LostFocus_1);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ItemVector3Value = ((TableContent.ItemVector3Control)(target));
            return;
            case 7:
            this.ItemEnmuValue = ((System.Windows.Controls.ComboBox)(target));
            
            #line 49 "..\..\..\base\ContentItemBase.xaml"
            this.ItemEnmuValue.GotFocus += new System.Windows.RoutedEventHandler(this.Item_GotFocus_1);
            
            #line default
            #line hidden
            
            #line 50 "..\..\..\base\ContentItemBase.xaml"
            this.ItemEnmuValue.LostFocus += new System.Windows.RoutedEventHandler(this.Item_LostFocus_1);
            
            #line default
            #line hidden
            
            #line 51 "..\..\..\base\ContentItemBase.xaml"
            this.ItemEnmuValue.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ItemEnmuValue_SelectionChanged_1);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ItemIDSingleValue = ((System.Windows.Controls.ComboBox)(target));
            
            #line 61 "..\..\..\base\ContentItemBase.xaml"
            this.ItemIDSingleValue.GotFocus += new System.Windows.RoutedEventHandler(this.Item_GotFocus_1);
            
            #line default
            #line hidden
            
            #line 62 "..\..\..\base\ContentItemBase.xaml"
            this.ItemIDSingleValue.LostFocus += new System.Windows.RoutedEventHandler(this.Item_LostFocus_1);
            
            #line default
            #line hidden
            
            #line 63 "..\..\..\base\ContentItemBase.xaml"
            this.ItemIDSingleValue.MouseRightButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.ItemIDSingleValue_MouseRightButtonDown_1);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ItemIDMultiValue = ((TableContent.MultiTableControl)(target));
            return;
            case 10:
            this.errorBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 11:
            this.errorText = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

