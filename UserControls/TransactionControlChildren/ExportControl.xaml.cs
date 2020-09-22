using Financeiro.DB.Entities.Sqlite;
using Financeiro.Managers;
using Financeiro.Scripts;
using SqliteHelper;
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
using static System.Convert;
using static System.String;

namespace Financeiro.UserControls.TransactionControlChildren {

    public partial class ExportControl : UserControl {

        public ExportControl() {
            InitializeComponent();
            //DatabaseSmoothExpandColapse = new SmoothExpandColapse(DatabaseFileSP, Orientation.Vertical, 0, 40, 1, 2);
            ExcelSmoothExpandColapse = new SmoothExpandColapse(ExcelSP, Orientation.Vertical, 0, 40, 1, 2);
        }

        public WindowMaskHelper WindowMaskHelper { get; set; }

        //SmoothExpandColapse DatabaseSmoothExpandColapse { get; }
        SmoothExpandColapse ExcelSmoothExpandColapse { get; }

        private void DatabaseFileRB_Checked(object sender, RoutedEventArgs e) {
            //DatabaseSmoothExpandColapse.Expand();
            ExportarB.IsEnabled = true;
        }

        private void DatabaseFileRB_Unchecked(object sender, RoutedEventArgs e) {
            //DatabaseSmoothExpandColapse.Colapse();
        }

        private void ExcelFileRB_Checked(object sender, RoutedEventArgs e) {
            ExportarB.IsEnabled = true;
            ExcelSmoothExpandColapse.Expand();
        }

        private void ExcelFileRB_Unchecked(object sender, RoutedEventArgs e) {
            ExcelSmoothExpandColapse.Colapse();
        }

        private void ExportarB_Click(object sender, RoutedEventArgs e) {
            if (!Directory.Exists(FolderPathTB.Text)) {
                MessageBox.Show("Pasta não existe ou não é accessível.");
            }
            var mw = (MainWindow)App.Current.MainWindow;
            var tran = mw.TransactionC;
            if (DatabaseFileRB.IsChecked ?? false) {
                var external_sqlite = new SqliteFinanceiro(FolderPathTB.Text + $"\\[{DateTime.Now:dd/MM/yyyy}] db.db");
                external_sqlite.NonQuery(Properties.Resources.DatabaseCreation);
                external_sqlite.NonQuery(Properties.Resources.InitialData);
                foreach (var item in tran.TransactionsDG.Items) {
                    var tranV = item as TransactionV;
                    external_sqlite.InsertTransaction(tranV);
                }
                WindowMaskHelper.ClearMask();
            } else if (ExcelFileRB.IsChecked ?? false) {
                using var sw = new StreamWriter(FolderPathTB.Text + $"\\[{DateTime.Now:dd-MM-yyyy}] Relatório Geral.csv", false, Encoding.UTF8);
                using var relatorio_gastos = new StreamWriter($"{FolderPathTB.Text}\\[{DateTime.Now:dd-MM-yyyy}] Relatório de Gastos.csv", false, Encoding.UTF8);
                var offset = Converter.TryToInt32(LinhaOffsetTB.Text, 0);
                for (var i = 0; i < offset; i++) {
                    sw.WriteLine();
                    relatorio_gastos.WriteLine();
                }
                var coluns = new HashSet<string>();
                var dt_columnValue = new Dictionary<string, Dictionary<string, double>>();
                sw.WriteLine($"Título,Data,Valor,Provedor,Destinatário,Descrição");
                foreach (var item in tran.TransactionsDG.Items) {
                    var tranV = item as TransactionV;
                    sw.WriteLine($"\"{tranV.Title}\",\"{tranV.DateStr}\",\"{tranV.Amount}\",\"{tranV.OriginName}\",\"{tranV.DestinationName}\",\"{tranV.Description}\"");
                    if (tranV.Origin == 1) {
                        coluns.Add(tranV.DestinationName ?? "Outros");
                        var dt = DateTimeHelper.UnixTimeStampToDateTime(tranV.Date);
                        var current_dt = $"{dt.Year}/{dt.Month}/{dt.Day}";
                        if (!dt_columnValue.ContainsKey(current_dt)) dt_columnValue.Add(current_dt, new Dictionary<string, double>());
                        var columnValue = dt_columnValue[current_dt];
                        var destination_name = String.IsNullOrEmpty(tranV.DestinationName) ? "Outros" : tranV.DestinationName;
                        if (!columnValue.ContainsKey(destination_name)) columnValue.Add(destination_name, 0);
                        columnValue[destination_name] += tranV.Amount;
                    }
                }
                var ordered_rows = coluns.ToArray();
                relatorio_gastos.WriteLine($"Data,{Join(",", ordered_rows)}");
                foreach (var item in dt_columnValue) {
                    var dt = item.Key;
                    var columnValue = item.Value;
                    relatorio_gastos.WriteLine($"{dt},{Join(",", ordered_rows.Select(x => $"\"{columnValue.FirstOrDefault(y => y.Key == x).Value}\""))}");
                }
                WindowMaskHelper.ClearMask();
            }
        }

        private void CancelarB_Click(object sender, RoutedEventArgs e) {
            WindowMaskHelper.ClearMask();
        }

        private void ProcurarB_Click(object sender, RoutedEventArgs e) {
            if (WindowsSO.ChooseFolder(out string path)) {
                FolderPathTB.Text = path;
            }
        }
    }
}
