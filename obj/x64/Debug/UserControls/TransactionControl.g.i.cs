﻿#pragma checksum "..\..\..\..\UserControls\TransactionControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "736659AED44525A64ACB9CBFEB282F83DEF366F6E67E06389E8B8D4BC91CC6A3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Financeiro.Managers.UI;
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
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Converters;
using Xceed.Wpf.Toolkit.Core;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Mag.Converters;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace Financeiro.UserControls {
    
    
    /// <summary>
    /// TransactionControl
    /// </summary>
    public partial class TransactionControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\..\UserControls\TransactionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ConteudoTB;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\UserControls\TransactionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox IncluirDescricaoTB;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\UserControls\TransactionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ValorMinimoMTB;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\UserControls\TransactionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ValorMaximoMTB;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\UserControls\TransactionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Financeiro.UserControls.ChooseEntity ProvedorCE;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\UserControls\TransactionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Financeiro.UserControls.ChooseEntity RecebedorCE;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\UserControls\TransactionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Financeiro.UserControls.DateInterval DateIntervalDI;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\UserControls\TransactionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PesquisarB;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\..\UserControls\TransactionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid TransactionsDG;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\..\UserControls\TransactionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContextMenu TransactionsCM;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\..\UserControls\TransactionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem EditarMI;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\..\UserControls\TransactionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem RemoverMI;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\..\UserControls\TransactionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ItemsCountTBL;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\..\..\UserControls\TransactionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ResumoSP;
        
        #line default
        #line hidden
        
        
        #line 146 "..\..\..\..\UserControls\TransactionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExportarB;
        
        #line default
        #line hidden
        
        
        #line 149 "..\..\..\..\UserControls\TransactionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button InserirB;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\..\..\UserControls\TransactionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EditarB;
        
        #line default
        #line hidden
        
        
        #line 151 "..\..\..\..\UserControls\TransactionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RemoveB;
        
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
            System.Uri resourceLocater = new System.Uri("/Financeiro;component/usercontrols/transactioncontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UserControls\TransactionControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.ConteudoTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.IncluirDescricaoTB = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 3:
            this.ValorMinimoMTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.ValorMaximoMTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.ProvedorCE = ((Financeiro.UserControls.ChooseEntity)(target));
            return;
            case 6:
            this.RecebedorCE = ((Financeiro.UserControls.ChooseEntity)(target));
            return;
            case 7:
            this.DateIntervalDI = ((Financeiro.UserControls.DateInterval)(target));
            return;
            case 8:
            this.PesquisarB = ((System.Windows.Controls.Button)(target));
            
            #line 80 "..\..\..\..\UserControls\TransactionControl.xaml"
            this.PesquisarB.Click += new System.Windows.RoutedEventHandler(this.PesquisarB_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.TransactionsDG = ((System.Windows.Controls.DataGrid)(target));
            
            #line 93 "..\..\..\..\UserControls\TransactionControl.xaml"
            this.TransactionsDG.ContextMenuOpening += new System.Windows.Controls.ContextMenuEventHandler(this.TransactionsDG_ContextMenuOpening);
            
            #line default
            #line hidden
            
            #line 93 "..\..\..\..\UserControls\TransactionControl.xaml"
            this.TransactionsDG.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TransactionsDG_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.TransactionsCM = ((System.Windows.Controls.ContextMenu)(target));
            return;
            case 11:
            this.EditarMI = ((System.Windows.Controls.MenuItem)(target));
            
            #line 104 "..\..\..\..\UserControls\TransactionControl.xaml"
            this.EditarMI.Click += new System.Windows.RoutedEventHandler(this.EditarB_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.RemoverMI = ((System.Windows.Controls.MenuItem)(target));
            
            #line 105 "..\..\..\..\UserControls\TransactionControl.xaml"
            this.RemoverMI.Click += new System.Windows.RoutedEventHandler(this.EditarB_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.ItemsCountTBL = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 14:
            this.ResumoSP = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 15:
            this.ExportarB = ((System.Windows.Controls.Button)(target));
            
            #line 146 "..\..\..\..\UserControls\TransactionControl.xaml"
            this.ExportarB.Click += new System.Windows.RoutedEventHandler(this.ExportarB_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.InserirB = ((System.Windows.Controls.Button)(target));
            
            #line 149 "..\..\..\..\UserControls\TransactionControl.xaml"
            this.InserirB.Click += new System.Windows.RoutedEventHandler(this.InserirB_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            this.EditarB = ((System.Windows.Controls.Button)(target));
            
            #line 150 "..\..\..\..\UserControls\TransactionControl.xaml"
            this.EditarB.Click += new System.Windows.RoutedEventHandler(this.EditarB_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            this.RemoveB = ((System.Windows.Controls.Button)(target));
            
            #line 151 "..\..\..\..\UserControls\TransactionControl.xaml"
            this.RemoveB.Click += new System.Windows.RoutedEventHandler(this.RemoveB_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

