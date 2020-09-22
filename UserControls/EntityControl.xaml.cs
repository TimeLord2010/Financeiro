using Financeiro.Managers;
using Financeiro.Managers.UI;
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

    public partial class EntityControl : UserControl {

        public EntityControl() {
            InitializeComponent();
            Pesquisar();
        }

        SqliteFinanceiro SqliteFinanceiro = new SqliteFinanceiro();

        private void InserirB_Click(object sender, RoutedEventArgs e) {
            var a = new SimpleTableElementCreate("Entity");
            a.ShowDialog();
            Pesquisar();
        }

        private void EditarB_Click(object sender, RoutedEventArgs e) {
            dynamic item = ResultDG.SelectedItem;
            var a = new SimpleTableElementCreate("Entity", item.RegistrationDate);
            a.ShowDialog();
            Pesquisar();
        }

        private void RemoverB_Click(object sender, RoutedEventArgs e) {
            dynamic item = ResultDG.SelectedItem;
            SqliteFinanceiro.DeleteEntity(item.RegistrationDate);
            Pesquisar();
        }

        private void ResultDG_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            EditarB.IsEnabled = RemoverB.IsEnabled = ResultDG.SelectedIndex >= 0;
        }

        private void PesquisarB_Click(object sender, RoutedEventArgs e) {
            Pesquisar();
        }

        void Pesquisar () {
            ResultDG.Items.Clear();
            SqliteFinanceiro.SelectEntity(NameTB.Text, (rd, name) => {
                ResultDG.Items.Add(new { 
                    RegistrationDate = rd,
                    Name = name
                });
            });
        }
    }
}
