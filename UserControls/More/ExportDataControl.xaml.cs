using Financeiro.DB.Entities.Sqlite;
using Financeiro.Managers;
using Financeiro.Scripts.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Financeiro.UserControls.More {

    public partial class ExportDataControl : Window {

        public ExportDataControl(SqliteFinanceiro financeiro) {
            InitializeComponent();
            Financeiro = financeiro;
            Process();
        }

        SqliteFinanceiro Financeiro { get; }

        public void Process() {
            var count = Financeiro.SelectCountToUpload();
            MyProgressBar.Maximum = count * 2;
            var counter = 0;
            var registrations = new Dictionary<string, List<string>>();
            registrations.Add("Entity", new List<string>());
            registrations.Add("Transaction", new List<string>());
            var t = new Thread(() => {
                try {
                    Financeiro.SelectToUpload((tn, rd) => {
                        var list = registrations[tn];
                        list.Add(rd);
                        if (tn == "Entity") {

                        } else if (tn == "Transaction") {
                            var t = Financeiro.SelectTransactionV(rd);
                            Log($"Transaction - {t.Title}...");
                            throw new NotImplementedException("Este recurso ainda não está devidamente implementado.");
                            //var tran = new DynamoTransaction {
                            //    Amount = t.Amount,
                            //    Date = t.Date,
                            //    DateStr = t.DateStr,
                            //    Description = t.Description,
                            //    Destination = t.Destination,
                            //    DestinationName = t.DestinationName,
                            //    Origin = t.Origin,
                            //    OriginName = t.OriginName,
                            //    RegistrationDate = t.RegistrationDate,
                            //    Source = t.Source,
                            //    SourceName = t.SourceName,
                            //    Category = t.Category,
                            //    CategoryName = t.CategoryName,
                            //    Title = t.Title
                            //};
                            //var ps = Financeiro.SelectTransactionPayment(t.RegistrationDate);
                            //foreach (var item in ps) {
                            //    tran.Payments.Add(new Payments() {
                            //        PaymentName = item.Item1.ToString(),
                            //        Amount = item.Item2
                            //    });
                            //}
                            //string url = ;
                            //var serializer = new JavaScriptSerializer();
                            ////string json = serializer.Serialize(tran);
                            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(new {
                            //    TableName = tran.TableName,
                            //    Content = tran
                            //});
                            //var response = WebMethods.Request(HttpMethod.Post, url, json).Result;
                            //Log($"{response.StatusCode} - {response.Content.ReadAsStringAsync().Result}");
                            //if (response.StatusCode != HttpStatusCode.OK) {
                            //    return;
                            //}
                        }
                        UpdateProgress(++counter);
                    });
                    foreach (var item in registrations) {
                        var tn = item.Key;
                        var list = item.Value;
                        foreach (var rd in list) {
                            Financeiro.DeleteToUpload(tn, rd);
                            Log($"Deletando '{rd}' de '{tn}' do registro temporário.");
                            UpdateProgress(++counter);
                        }
                    }
                    App.Current.Dispatcher.Invoke(() => {
                        Title = "Exportação Concluída!";
                        FecharB.IsEnabled = true;
                    });
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message, $"Erro ao exportar.");
                }
            });
            t.Start();
        }

        void Log(string message) {
            App.Current.Dispatcher.Invoke(() => {
                LogSP.Children.Add(new TextBlock() {
                    Text = message
                });
                while (LogSP.Children.Count > 500) LogSP.Children.RemoveAt(0);

            });
        }

        void UpdateProgress(double progress) {
            App.Current.Dispatcher.Invoke(() => {
                MyProgressBar.Value = progress;
            });
        }

        private void FecharB_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
