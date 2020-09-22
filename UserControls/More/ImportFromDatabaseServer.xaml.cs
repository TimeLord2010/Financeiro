using Financeiro.Managers;
using Financeiro.Scripts;
using SqliteHelper;
using System;
using System.Collections.Generic;
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
using WPFClinica.Scripts.DB;

namespace Financeiro.UserControls.More {

    public partial class ImportFromDatabaseServer : UserControl {

        public ImportFromDatabaseServer() {
            InitializeComponent();
            
        }

        public SqliteFinanceiro SqliteFinanceiro { get; set; } = new SqliteFinanceiro();
        public MysqlFinanceiro MysqlFinanceiro { get; set; }
        public WindowMaskHelper WindowMaskHelper { get; set; }

        private void ImportB_Click(object sender, RoutedEventArgs e) {
            MysqlFinanceiro = new MysqlFinanceiro(SqliteFinanceiro);
            var begin = IntervalDI.BeginDT;
            var end = IntervalDI.EndDT;
            var mysql = MysqlFinanceiro.MySqlH;
            var import_consulta = ConsultasChB.IsChecked ?? false;
            var import_proc = ProcedimentosChB.IsChecked ?? false;
            var import_proc_lab = ProcedimentosLaboratoriaisChB.IsChecked ?? false;
            var t = new Thread(() => {
                if (import_consulta) {
                    ImportSection($"Log : Importando consultas", () => {
                        var historico_consultas = new Historico_Consultas(mysql);
                        historico_consultas.GetConsultasByRealizadoEm(begin, end , (consulta) => {
                            SqliteFinanceiro.InsertTransaction(consulta);
                            AddMessage($"{consulta.RealizadoEm:yyyy-MM-dd HH:mm:ss} {consulta.Paciente}");
                        });
                    });
                }
                if (import_proc) {
                    ImportSection($"Log : Importando procedimentos médicos", () => {
                        var historico_procedimentos = new Historico_Procedimento(mysql);
                        historico_procedimentos.Select_Realizado("", "", begin, end, (cpf, realizado, valor, nome_paciente, procedimento, tipo, pm) => {
                            SqliteFinanceiro.InsertTransaction(cpf, realizado, valor, nome_paciente, procedimento, tipo, pm);
                            AddMessage($"{realizado:yyyy-MM-dd HH:mm:ss} {nome_paciente} | {procedimento}, {tipo}");
                        });
                    });
                }
                if (import_proc_lab) {
                    ImportSection($"Log : Importando procedimentos laboratoriais", () => {
                        var historico_procedimento_lab = new Historico_ProcedimentosLab(mysql);
                        historico_procedimento_lab.SelectLike("", "", begin, end, "", true, (cpf, realizado, pago, procedimento, convenio, paciente, pm) => {
                            SqliteFinanceiro.InsertTransaction(cpf, realizado, pago, procedimento, convenio, paciente, pm);
                            AddMessage($"{realizado:yyyy-MM-dd HH:mm:ss} {paciente} | {procedimento}, {convenio}");
                        });
                    });
                }
                App.Current.Dispatcher.Invoke(() => {
                    WindowMaskHelper.ClearMask();
                });
            });
            t.Start();
        }

        void ImportSection(string log, Action action) {
            App.Current.Dispatcher.Invoke(() => {
                LogTBL.Text = log;
            });
            action();
            Thread.Sleep(5000);
            ClearMessages();
        }

        void AddMessage(string message) {
            App.Current.Dispatcher.Invoke(() => {
                ItemsLB.Items.Add(new TextBlock() {
                    Text = message
                });
            });
            Thread.Sleep(50);
        }

        void ClearMessages() {
            App.Current.Dispatcher.Invoke(() => {
                ItemsLB.Items.Clear();
            });
        }

        private void CancelB_Click(object sender, RoutedEventArgs e) {
            WindowMaskHelper.Previous();
        }
    }
}
