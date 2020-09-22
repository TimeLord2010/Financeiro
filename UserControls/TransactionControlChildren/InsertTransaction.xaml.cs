using Financeiro.DB.Entities.Enums;
using Financeiro.DB.Entities.Sqlite;
using Financeiro.Managers;
using Financeiro.Scripts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Financeiro.UserControls.TransactionControlChildren {

    public partial class InsertTransaction : UserControl {

        public InsertTransaction(TransactionV selection = null) {
            InitializeComponent();
            var dt_tb = new CustomDateTimeTB();
            dt_tb.ApplyMask(DateMTB);
            ProvedorCE.RegistrationDate = 1;
            if (selection != null) {
                TransactionV = selection;
                TitleTB.Text = selection.Title;
                //DateMTB.Mask = "################";
                DateMTB.Text = selection.DateStr;
                //DateMTB.Mask = "00/00/00 00:00";
                var payments = Financeiro.SelectTransactionPayment(selection.RegistrationDate);
                foreach (var (payment, amount) in payments) {
                    switch (payment) {
                        case PaymentMethods.Dinheiro:
                            DinheiroTB.Text = amount + "";
                            break;
                        case PaymentMethods.Debito:
                            DebitoTB.Text = amount + "";
                            break;
                        case PaymentMethods.Credito:
                            CreditoTB.Text = amount + "";
                            break;
                    }
                }
                ParcelasTB.Text = "1";
                ProvedorCE.RegistrationDate = selection.Origin;
                DestinatarioCE.RegistrationDate = selection.Destination;
                DescriptionTB.Text = selection.Description;
                InserirB.Content = "Editar";
            }
        }

        public WindowMaskHelper MaskHelper { get; set; }
        SqliteFinanceiro Financeiro = new SqliteFinanceiro();
        //Source SourceTable { get; }

        TransactionV TransactionV {
            get;
        }

        #region Vars

        string Titulo {
            get => TitleTB.Text;
        }

        long Date {
            get {
                try {
                    var match = Regex.Match(DateMTB.Text, @"\s*(\d{1,2})/(\d{1,2})/(\d{2,4})\s+(\d{1,2}):(\d{1,2})\s*", RegexOptions.CultureInvariant);
                    if (!match.Success) {
                        match = Regex.Match(DateMTB.Text, @"\s*(\d{1,2})/(\d{1,2})/(\d{2,4})\s*", RegexOptions.CultureInvariant);
                        if (!match.Success) {
                            throw new Exception("O campo 'Data' não possui uma data no formato desejado (dd/MM/yy HH:mm).");
                        }
                        var (day, month, year) = GetDate(match);
                        var dt = new DateTime(year, month, day, 00, 00, 00);
                        return DateTimeHelper.ToUnixTimestamp(dt);
                    } else {
                        var (day, month, year) = GetDate(match);
                        var hour = ToInt32(match.Groups[4].Value);
                        var minute = ToInt32(match.Groups[5].Value);
                        var second = DateTime.Now.Second;
                        var dt = new DateTime(year, month, day, hour, minute, second);
                        return DateTimeHelper.ToUnixTimestamp(dt);
                    }
                } catch (Exception ex) {
                    throw new Exception($"Erro no campo 'Data': {ex.Message}.", ex);
                }
            }
        }

        double Dinheiro {
            get {
                var value = Converter.TryToDouble(DinheiroTB.Text, 0);
                if (value < 0) throw new ArgumentException("'Dinheiro' não pode ser negativo.");
                return value;
            }
        }

        double Debito {
            get {
                var value = Converter.TryToDouble(DebitoTB.Text, 0);
                if (value < 0) throw new ArgumentException("'Debito' não pode ser negativo.");
                return value;
            }
        }

        double Credito {
            get {
                var value = Converter.TryToDouble(CreditoTB.Text, 0);
                if (value < 0) throw new ArgumentException("'Crédito' não pode ser negativo.");
                return value;
            }
        }

        int Parcelas {
            get {
                var value = Converter.TryToInt32(ParcelasTB.Text, 1);
                if (value < 1 || value > 12) throw new ArgumentException("'Parcelas não possui um valor válido.'");
                return value;
            }
        }

        double Total {
            get {
                double paid_credito = 0;
                double parcelas = Parcelas;
                if (parcelas != 0)
                    paid_credito = Credito / parcelas;
                return Dinheiro + Debito + paid_credito;
            }
        }

        long? Provedor {
            get => ProvedorCE.RegistrationDate;
        }

        long? Destinatário {
            get => DestinatarioCE.RegistrationDate;
        }

        string Descricao {
            get => DescriptionTB.Text;
        }

        #endregion

        private void InserirB_Click(object sender, RoutedEventArgs e) {
            try {
                var mw = (MainWindow)App.Current.MainWindow;
                var title = Titulo;
                var date = Date;
                var description = Descricao;
                var total = Total;
                var des_rd = DestinatarioCE.RegistrationDate;
                var des = DestinatarioCE.EntityNameTB.Text;
                var origin_rd = ProvedorCE.RegistrationDate;
                var origin = ProvedorCE.EntityNameTB.Text;
                var transactionV = new TransactionV (Financeiro.Handler) {
                    Amount = total,
                    Date = date,
                    DateStr = DateTimeHelper.UnixTimeStampToDateTime(date).ToString("dd/MM/yy HH:mm"),
                    Description = description,
                    Title = title,
                    Source = (long)Sources.Sqlite,
                    SourceName = "Sqlite",
                    Destination = des_rd,
                    DestinationName = des,
                    Origin = origin_rd,
                    OriginName = origin
                };
                if (date <= 0) {
                    throw new Exception("O campo 'Data' não está no formato correto.");
                }
                if (TransactionV != null) {
                    Financeiro.UpdateTransaction(TransactionV.RegistrationDate, Titulo, Descricao, Total, date, Provedor, Destinatário, null, Dinheiro, Debito, Credito);
                    if (mw.TransactionControlTI.Content is TransactionControl tc) {
                        transactionV.RegistrationDate = TransactionV.RegistrationDate;
                        tc.UpdateTransaction(TransactionV.RegistrationDate, transactionV);
                    }
                } else {
                    var rd = Financeiro.InsertTransaction(Titulo, description, Total, date, Provedor, Destinatário, null, Dinheiro, Debito, Credito, Parcelas);
                    if (mw.TransactionControlTI.Content is TransactionControl tc) {
                        transactionV.RegistrationDate = rd;
                        tc.InsertOrdered(transactionV);
                    }
                }
                MaskHelper.ClearMask();
            } catch (Exception ex) {
                string message = ex.Message;
                while (ex.InnerException != null) {
                    ex = ex.InnerException;
                    message = $"{message}\n{ex.Message}";
                } 
                MessageBox.Show(message, "Erro ao inserir transação", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelarB_Click(object sender, RoutedEventArgs e) {
            MaskHelper.ClearMask();
        }

        (int, int, int) GetDate(Match match) {
            var day = ToInt32(match.Groups[1].Value);
            var month = ToInt32(match.Groups[2].Value);
            var y = match.Groups[3].Value;
            if (y.Length == 2) y = $"20{y}";
            var year = ToInt32(y);
            return (day, month, year);
        }

    }
}
