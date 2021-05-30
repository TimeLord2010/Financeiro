using Financeiro.Managers;
using Financeiro.Scripts;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Financeiro.UserControls.More {

    public partial class EspecifyDatabaseImport : UserControl {

        public EspecifyDatabaseImport() {
            InitializeComponent();
            var financeiro = new SqliteFinanceiro();
            Mysql = new MysqlFinanceiro(financeiro);
            var (db, user, pwd, server) = Mysql.Parameters;
            DatabaseTB.Text = db;
            UserTB.Text = user;
            PasswordPB.Password = pwd;
            ServerTB.Text = server;
        }

        public WindowMaskHelper WindowMaskHelper { get; set; }
        MysqlFinanceiro Mysql { get; }

        private void CancelarB_Click(object sender, RoutedEventArgs e) {
            WindowMaskHelper.ClearMask();
        }

        private void DatabaseFileRB_Checked(object sender, RoutedEventArgs e) {
            IsDatabaseFile = true;
        }

        private void DatabaseFileRB_Unchecked(object sender, RoutedEventArgs e) {
            IsDatabaseFile = false;
        }

        bool IsDatabaseFile {
            get => DatabaseFileRB.IsChecked ?? false;
            set {
                DatabaseFileSP.IsEnabled = value;
                ServerDatabaseSP.IsEnabled = !value;
            }
        }

        private void NextB_Click(object sender, RoutedEventArgs e) {
            if (IsDatabaseFile) {
                if (!File.Exists(FileNameTB.Text)) {
                    MessageBox.Show("Arquivo não existe.");
                    return;
                }
                WindowMaskHelper.Next(new ImportFromDatabaseFile(FileNameTB.Text));
            } else {
                Mysql.Parameters = (DatabaseTB.Text, UserTB.Text, PasswordPB.Password, ServerTB.Text);
                WindowMaskHelper.Next(new ImportFromDatabaseServer());
            }
        }

        private void BrowseB_Click(object sender, RoutedEventArgs e) {
            if (WindowsSO.ChooseFile(out string fn)) {
                FileNameTB.Text = fn;
            }
        }
    }
}
