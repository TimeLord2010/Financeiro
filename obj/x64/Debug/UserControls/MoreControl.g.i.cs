﻿#pragma checksum "..\..\..\..\UserControls\MoreControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DE66C3B1FA5B9BB721F548E0AF6EF32351DDD6F0B0D81F5D5623561183E2D178"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Financeiro.UserControls;
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


namespace Financeiro.UserControls {
    
    
    /// <summary>
    /// MoreControl
    /// </summary>
    public partial class MoreControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\..\UserControls\MoreControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MyGrid;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\UserControls\MoreControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EspecificarB;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\UserControls\MoreControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ComecarExportarB;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\UserControls\MoreControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ReportarB;
        
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
            System.Uri resourceLocater = new System.Uri("/Financeiro;component/usercontrols/morecontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UserControls\MoreControl.xaml"
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
            this.MyGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.EspecificarB = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\..\UserControls\MoreControl.xaml"
            this.EspecificarB.Click += new System.Windows.RoutedEventHandler(this.EspecificarB_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ComecarExportarB = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\..\UserControls\MoreControl.xaml"
            this.ComecarExportarB.Click += new System.Windows.RoutedEventHandler(this.ComecarExportarB_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ReportarB = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\..\UserControls\MoreControl.xaml"
            this.ReportarB.Click += new System.Windows.RoutedEventHandler(this.ReportarB_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

