﻿#pragma checksum "..\..\..\..\Pages\ConstructItemInfo.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4E07049F90E79E5B6054701A15223594"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
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


namespace TableConstruct.Pages {
    
    
    /// <summary>
    /// ConstructItemInfo
    /// </summary>
    public partial class ConstructItemInfo : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\..\Pages\ConstructItemInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ItemField;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Pages\ConstructItemInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ItemName;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Pages\ConstructItemInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ItemCode;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Pages\ConstructItemInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ItemType;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\Pages\ConstructItemInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ItemDefault;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\Pages\ConstructItemInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ItemRepeat;
        
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
            System.Uri resourceLocater = new System.Uri("/TableConstruct;component/pages/constructiteminfo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\ConstructItemInfo.xaml"
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
            this.ItemField = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.ItemName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.ItemCode = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.ItemType = ((System.Windows.Controls.ComboBox)(target));
            
            #line 40 "..\..\..\..\Pages\ConstructItemInfo.xaml"
            this.ItemType.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ItemType_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ItemDefault = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.ItemRepeat = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

