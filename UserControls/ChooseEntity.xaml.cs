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

    public partial class ChooseEntity : UserControl {

        public ChooseEntity () {
            InitializeComponent();
            //TableName = table_name;
        }

        SqliteFinanceiro Financeiro = new SqliteFinanceiro();

        public static readonly DependencyProperty TableNameProperty = DependencyProperty.RegisterAttached("TableName", typeof(string), typeof(ChooseEntity), new PropertyMetadata(null));
        
        public string TableName {
            get => (string)GetValue(TableNameProperty);
            set => SetValue(TableNameProperty, value);
        }

        long? registrationDate = null;
        public long? RegistrationDate {
            get => registrationDate;
            set {
                registrationDate = value;
                if (registrationDate.HasValue) {
                    var manager = new SimpleTableManager(Financeiro.Handler, TableName ?? throw new NullReferenceException("TableName was null"));
                    var name = manager.Get(registrationDate);
                    EntityNameTB.Text = name;
                }
            }
        }

        private void ProcurarB_Click(object sender, RoutedEventArgs e) {
            (long, string) selection = (0,null);
            var chooser = new TableElementChooser(TableName);
            chooser.Closing += delegate {
                selection = chooser.Selection;
            };
            chooser.ShowDialog();
            RegistrationDate = selection.Item1;
            EntityNameTB.Text = selection.Item2;
        }
    }
}
