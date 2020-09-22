using Financeiro.DB.Entities.Sqlite;
using Financeiro.Managers;
using Financeiro.Scripts;
using Financeiro.Scripts.DataStructures;
using Financeiro.Scripts.Managers;
using Financeiro.UserControls.TransactionControlChildren;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

    public partial class TransactionControl : UserControl {

        public TransactionControl() {
            InitializeComponent();
            HTransactionV = new TransactionV(Financeiro.Handler);
            var dt = DateTime.Now.AddMonths(-1);
            DateIntervalDI.BeginMTB.Text = $"{dt:dd/MM/yy}";
            var dt2 = DateTime.Now.AddDays(1);
            DateIntervalDI.EndMTB.Text = $"{dt2:dd/MM/yy}";
            PesquisarB_Click(null, null);
        }

        WindowMaskHelper MaskHelper = new WindowMaskHelper();
        TransactionV HTransactionV { get; }
        SqliteFinanceiro Financeiro = new SqliteFinanceiro();

        CultureInfo Brasil = new CultureInfo("pt-br");

        public bool IncluirDescricao {
            get => IncluirDescricaoTB.IsChecked ?? false;
        }

        public double ValorMinimo {
            get => Converter.TryConvert(() => Convert.ToDouble(ValorMinimoMTB.Text, Brasil), -10000);
        }

        public double ValorMaximo {
            get => Converter.TryConvert(() => Convert.ToDouble(ValorMaximoMTB.Text, Brasil), 10000);
        }

        private void InserirB_Click(object sender, RoutedEventArgs e) {
            MaskHelper.CreateMask();
            MaskHelper.SetView(new InsertTransaction());
        }

        private void ExportarB_Click(object sender, RoutedEventArgs e) {
            MaskHelper.CreateMask();
            MaskHelper.SetView(new ExportControl());
        }

        private void PesquisarB_Click(object sender, RoutedEventArgs e) {
            string ws = $"Title like '%{ConteudoTB.Text}%' ";
            if (IncluirDescricaoTB.IsChecked ?? false) {
                ws += $"and Description like '%{ConteudoTB.Text}%' ";
            }
            ws += $"AND (Amount between @min_amount and @max_amount) ";
            if (ProvedorCE.RegistrationDate.HasValue) ws += $"AND Origin = {ProvedorCE.RegistrationDate} ";
            if (RecebedorCE.RegistrationDate.HasValue) ws += $"AND Destination = {RecebedorCE.RegistrationDate} ";
            ws += $"AND ([Date] between {DateIntervalDI.BeginTimestamp} and {DateIntervalDI.EndTimestamp}) ";
            TransactionsDG.Items.Clear();
            int count = 0;
            HTransactionV.Select((row) => {
                count++;
                TransactionsDG.Items.Add(row);
            }, ws, 4000,
                ("@min_amount", Converter.TryToDouble(ValorMinimoMTB.Text, -10000)),
                ("@max_amount", Converter.TryToDouble(ValorMaximoMTB.Text, 10000))
            );
            ItemsCountTBL.Text = $"{count}";
            UpdateSumary();
        }

        private void TransactionsDG_ContextMenuOpening(object sender, ContextMenuEventArgs e) {
            TransactionsCM.IsEnabled = TransactionsDG.SelectedIndex >= 0;
        }

        private void EditarB_Click(object sender, RoutedEventArgs e) {
            var selection = (TransactionsDG.SelectedItem as TransactionV);
            MaskHelper.CreateMask();
            MaskHelper.SetView(new InsertTransaction(selection));
        }

        private void RemoveB_Click(object sender, RoutedEventArgs e) {
            var selection = (TransactionsDG.SelectedItem as TransactionV);
            Financeiro.DeleteTransaction(selection.RegistrationDate);
            TransactionsDG.Items.Remove(selection);
        }

        private void TransactionsDG_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            EditarB.IsEnabled = RemoveB.IsEnabled = TransactionsDG.SelectedIndex >= 0;
        }

        public void InsertOrdered(TransactionV t) {
            try {
                if (!ShouldBeInResult(t)) return;
                var children = TransactionsDG.Items;
                for (var i = 0; i < children.Count; i++) {
                    var child = children[i];
                    if (child is TransactionV transactionV) {
                        if (t.Date < transactionV.Date) {
                            TransactionsDG.Items.Insert(i, t);
                            return;
                        }
                    }
                }
                TransactionsDG.Items.Add(t);
            } finally {
                ItemsCountTBL.Text = $"{TransactionsDG.Items.Count}";
                UpdateSumary();
            }
        }

        public void UpdateTransaction(string rd, TransactionV t) {
            var children = TransactionsDG.Items;
            for (var i = 0; i < children.Count; i++) {
                var child = children[i];
                if (child is TransactionV transactionV) {
                    if (rd == transactionV.RegistrationDate) {
                        TransactionsDG.Items.Remove(child);
                        break;
                    }
                }
            }
            InsertOrdered(t);
        }

        public void UpdateSumary() {
            ResumoSP.Children.Clear();
            var eim = new EntityInfoManager();
            foreach (var item in TransactionsDG.Items) {
                if (item is TransactionV t) {
                    eim.Process(t.Origin, t.OriginName, t.Destination, t.DestinationName, t.Amount);
                }
            }
            var sorted = eim.Infos.ToList();
            sorted.Sort((x, y) => {
                return GetLength(y.Value) - GetLength(x.Value);
            });
            foreach (var item in sorted) {
                var eic = new EntityInfoControl();
                eic.Nome.Text = item.Value.EntityName;
                eic.GastosTBL.Text = $"{item.Value.Expenses:0.00}";
                eic.GanhosTBL.Text = $"{item.Value.Income:0.00}";
                ResumoSP.Children.Add(eic);
            }
        }

        int GetLength(EntityInfo y) {
            var value = y;
            if (value == null) return 0;
            var name = value.EntityName;
            if (name == null) return 0;
            return name.Length;
        }

        public bool ShouldBeInResult(TransactionV t) {
            if (!t.Title.Contains(ConteudoTB.Text)) return false;
            if (IncluirDescricao && !t.Description.Contains(ConteudoTB.Text)) return false;
            if (t.Date > DateIntervalDI.EndTimestamp || t.Date < DateIntervalDI.BeginTimestamp) return false;
            if (ProvedorCE.RegistrationDate.HasValue && t.Origin != ProvedorCE.RegistrationDate) return false;
            if (RecebedorCE.RegistrationDate.HasValue && t.Destination != RecebedorCE.RegistrationDate) return false;
            if (t.Amount < ValorMinimo || t.Amount > ValorMaximo) return false;
            return true;
        }

    }
}
