using Financeiro.Managers;
using Financeiro.Scripts;
using Financeiro.UserControls.More;
using System;
using System.Collections.Generic;
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

namespace Financeiro.UserControls {

    public partial class MoreControl : UserControl {

        public MoreControl() {
            InitializeComponent();
        }

        WindowMaskHelper WindowMaskHelper = new WindowMaskHelper();
        SqliteFinanceiro Financeiro = new SqliteFinanceiro();

        private void EspecificarB_Click(object sender, RoutedEventArgs e) {
            WindowMaskHelper.CreateMask();
            WindowMaskHelper.SetView(new EspecifyDatabaseImport());
        }

        private void ComecarExportarB_Click(object sender, RoutedEventArgs e) {
            var export = new ExportDataControl(Financeiro);
            export.Show();
        }

        private void ReportarB_Click(object sender, RoutedEventArgs e) {

        }
    }
}
