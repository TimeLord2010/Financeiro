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
using System.Windows.Shapes;

namespace Financeiro.Managers.UI {

    public partial class TableElementChooser : Window {

        public TableElementChooser(string table_name) {
            InitializeComponent();
            TableName = table_name;
            TableManager = new SimpleTableManager(SqliteFinanceiro.Handler ,table_name);
            PesquisarB_Click(null, null);
        }

        SqliteFinanceiro SqliteFinanceiro = new SqliteFinanceiro();
        string TableName { get; }
        SimpleTableManager TableManager { get; }

        public (long, string) Selection;

        private void PesquisarB_Click(object sender, RoutedEventArgs e) {
            ResultadosDG.Items.Clear();
            TableManager.SelectLikeName(EntityNameTB.Text, (rd, n) => {
                ResultadosDG.Items.Add(new {
                    RegistrationDate = rd,
                    Nome = n
                });
            });
        }

        private void EscolherB_Click(object sender, RoutedEventArgs e) {
            if (ResultadosDG.SelectedIndex < 0) return;
            dynamic selection = ResultadosDG.SelectedItem;
            Selection = (selection.RegistrationDate, selection.Nome);
            Close();
        }

        private void CriarB_Click(object sender, RoutedEventArgs e) {
            var creator = new SimpleTableElementCreate(TableName);
            creator.ShowDialog();
            PesquisarB_Click(null,null);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            EscolherB.IsEnabled = ResultadosDG.SelectedIndex >= 0;
        }

        private void ResultadosDG_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            EscolherB_Click(null,null);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (ResultadosDG.SelectedIndex < 0) {
                Selection = (-1,null);
            }
        }
    }
}
