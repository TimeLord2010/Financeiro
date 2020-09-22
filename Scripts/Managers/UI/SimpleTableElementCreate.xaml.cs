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

    public partial class SimpleTableElementCreate : Window {

        public SimpleTableElementCreate(string table_name, long? rd = null) {
            InitializeComponent();
            var f = new SqliteFinanceiro();
            Manager = new SimpleTableManager(f.Handler, table_name);
            RegistrationDate = rd;
            string a = table_name switch {
                "Entity" => "entidade",
                "Source" => "fonte",
                _ => "?"
            };
            Title = $"Criar {a}";
            if (rd.HasValue) {
                NomeTB.Text = Manager.Get(rd);
            }
        }

        SimpleTableManager Manager { get; }
        long? RegistrationDate { get; }

        //public static readonly DependencyProperty TableNameProperty = DependencyProperty.RegisterAttached("TableName", typeof(string), typeof(SimpleTableElementCreate), new PropertyMetadata(null));

        //public string TableName {
        //    get => (string)GetValue(TableNameProperty);
        //    set => SetValue(TableNameProperty, value);
        //}

        private void CriarB_Click(object sender, RoutedEventArgs e) {
            if (RegistrationDate.HasValue) {
                Manager.Update(RegistrationDate.Value, NomeTB.Text);
            } else {
                Manager.Insert(NomeTB.Text);
            }
            Close();
        }
    }
}
