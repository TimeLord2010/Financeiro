using Financeiro.DB.Entities.Enums;
using Financeiro.DB.Entities.Sqlite;
using MySql.Data.MySqlClient;
using SqliteHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClinica.Scripts.DB;

namespace Financeiro.Managers {

    public class SqliteFinanceiro {

        public SqliteFinanceiro(string db_path = "db.db") {
            if (!db_path.Contains(@"\")) {
                db_path = AppDomain.CurrentDomain.BaseDirectory + @"\" + db_path;
            }
            Handler = new SqliteHandler(db_path);
            if (!File.Exists(db_path)) {
                Handler.NonQuery(Properties.Resources.DatabaseCreation);
                Handler.NonQuery(Properties.Resources.InitialData);
            }
            Transaction = new Transaction(Handler);
            TransactionPayment = new TransactionPayment(Handler);
            SimpleTableManager = new SimpleTableManager(Handler);
            ToUpload = new ToUpload(Handler);
            TransactionV = new TransactionV(Handler);
        }

        public void SelectToUpload(Action<string, string> for_each) {
            ToUpload.SelectToUpload(for_each);
        }

        public long SelectCountToUpload () {
            return ToUpload.SelectCount();
        }

        public SqliteHandler Handler { get; }
        Transaction Transaction { get; }
        TransactionV TransactionV { get; }
        TransactionPayment TransactionPayment { get; }
        ToUpload ToUpload { get; }
        SimpleTableManager SimpleTableManager { get; }

        public TransactionV SelectTransactionV(string rd) {
            TransactionV t = null;
            TransactionV.Select((_t) => {
                t = _t;
            }, "RegistrationDate = @rd", 1, ("@rd", rd));
            return t;
        }

        public void LoopSettings(Action<string, string> for_each_row) {
            Handler.QueryLoop("select * from [Settings];", (r) => {
                var setting_name = r.GetString(0);
                var setting_value = r.GetString(1);
                for_each_row(setting_name, setting_value);
            });
        }

        internal void DeleteEntity(long registrationDate) {
            SimpleTableManager.TableName = "Entity";
            SimpleTableManager.Delete(registrationDate);
        }

        public string InsertTransaction(Historico_Consultas consulta) {
            Transaction.Title = $"Consulta - {consulta.Paciente}";
            Transaction.Description = $"Funcionário: {consulta.Funcionario}\nEspecilização: {consulta.Especializacao}\nProfissão: {consulta.Profissao}\nRetorno: {consulta.Retorno}";
            Transaction.Amount = consulta.Pago;
            Transaction.Date = DateTimeHelper.ToUnixTimestamp(consulta.RealizadoEm);
            Transaction.Source = (long)Sources.MySql;
            Transaction.Origin = (long)Entities.Paciente;
            Transaction.Destination = (long)Entities.Clinica;
            var payments = consulta.GetPayment(consulta.ID);
            var money = payments.FirstOrDefault(x => x.Item1 == PaymentMethods.Dinheiro);
            var debit = payments.FirstOrDefault(x => x.Item1 == PaymentMethods.Debito);
            var credit = payments.FirstOrDefault(x => x.Item1 == PaymentMethods.Credito);
            return Transaction.Insert(money.Item2, debit.Item2, credit.Item2, 1);
        }

        public string InsertTransaction(TransactionV tranV) {
            Transaction.Title = tranV.Title;
            Transaction.Description = tranV.Description;
            Transaction.Date = tranV.Date;
            Transaction.Origin = tranV.Origin;
            Transaction.Destination = tranV.Destination;
            Transaction.Amount = tranV.Amount;
            Transaction.Category = tranV.Category;
            var pm = SelectTransactionPayment(tranV.RegistrationDate);
            var money = pm.FirstOrDefault(x => x.Item1 == PaymentMethods.Dinheiro);
            var debit = pm.FirstOrDefault(x => x.Item1 == PaymentMethods.Debito);
            var credit = pm.FirstOrDefault(x => x.Item1 == PaymentMethods.Credito);
            return Transaction.Insert(money.Item2, debit.Item2, credit.Item2, 1);
        }

        internal void NonQuery(string nq, params (string, object)[] pa) {
            Handler.NonQuery(nq, pa);
        }

        internal string InsertTransaction(string cpf, DateTime realizado, double valor, string nome_paciente, string procedimento, string tipo, List<(PaymentMethods, double)> pm) {
            Transaction.Title = $"Procedimento - {nome_paciente}";
            Transaction.Description = $"Paciente: {cpf}\nProcedimento: {procedimento}\nTipo: {tipo}";
            Transaction.Amount = valor;
            Transaction.Date = DateTimeHelper.ToUnixTimestamp(realizado);
            Transaction.Source = (long)Sources.MySql;
            Transaction.Origin = (long)Entities.Paciente;
            Transaction.Destination = (long)Entities.Clinica;
            var money = pm.FirstOrDefault(x => x.Item1 == PaymentMethods.Dinheiro);
            var debit = pm.FirstOrDefault(x => x.Item1 == PaymentMethods.Debito);
            var credit = pm.FirstOrDefault(x => x.Item1 == PaymentMethods.Credito);
            return Transaction.Insert(money.Item2, debit.Item2, credit.Item2, 1);
        }

        internal void DeleteToUpload(string tn, string rd) {
            ToUpload.Delete(tn, rd);
        }

        public string InsertTransaction(string title, string description, double amount, long date, long? origin, long? destination, long? category, double money, double debit, double credit, int installments) {
            Transaction.Title = title;
            Transaction.Description = description;
            Transaction.Amount = amount;
            Transaction.Date = date;
            Transaction.Source = (long)Sources.Sqlite;
            Transaction.Origin = origin;
            Transaction.Destination = destination;
            Transaction.Category = category;
            return Transaction.Insert(money, debit, credit, installments);
        }

        internal void SetSettingsConnection(string db, string user, string password, string server) {
            Handler.NonQuery($"insert or replace into Settings ([Key], [Value]) values ('Database', @db);", ("@db", db));
            Handler.NonQuery($"insert or replace into Settings ([Key], [Value]) values ('UserID', @user);", ("@user", user));
            Handler.NonQuery($"insert or replace into Settings ([Key], [Value]) values ('Password', @pwd);", ("@pwd", password));
            Handler.NonQuery($"insert or replace into Settings ([Key], [Value]) values ('Server', @server);", ("@server", server));
        }

        public string InsertTransaction(string cpf, DateTime? realizado, double pago, string procedimento, string convenio, string paciente, List<(PaymentMethods, double)> pm) {
            Transaction.Title = $"Procedimento Lab - {paciente}";
            Transaction.Description = $"Paciente: {cpf}\nProcedimento: {procedimento}\nConvênio: {convenio}";
            Transaction.Amount = pago;
            Transaction.Date = realizado.HasValue? DateTimeHelper.ToUnixTimestamp(realizado.Value) : 0;
            Transaction.Source = (long)Sources.MySql;
            Transaction.Origin = (long)Entities.Paciente;
            Transaction.Destination = (long)Entities.Clinica;
            var money = pm.FirstOrDefault(x => x.Item1 == PaymentMethods.Dinheiro);
            var debit = pm.FirstOrDefault(x => x.Item1 == PaymentMethods.Debito);
            var credit = pm.FirstOrDefault(x => x.Item1 == PaymentMethods.Credito);
            return Transaction.Insert(money.Item2, debit.Item2, credit.Item2, 1);
        }

        public List<(PaymentMethods, double)> SelectTransactionPayment (string transaction) {
            return TransactionPayment.Select(transaction);
        }

        public void UpdateTransactionPayment(string transaction, PaymentMethods method, double amount) {
            TransactionPayment.Update(transaction, method, amount);
        }

        internal void UpdateTransaction(string registrationDate, string titulo, string descricao, double total, long date, long? provedor, long? destinatário, long? category, double dinheiro, double debito, double credito) {
            Transaction.Title = titulo;
            Transaction.Description = descricao;
            Transaction.Amount = total;
            Transaction.Date = date;
            Transaction.Source = (long)Sources.Sqlite;
            Transaction.Origin = provedor;
            Transaction.Destination = destinatário;
            Transaction.Category = category;
            Transaction.UpdateTransaction(registrationDate, dinheiro, debito, credito);
        }

        public void DeleteTransaction (string registration_date) {
            Transaction.Delete(registration_date);
            TransactionPayment.Delete(registration_date);
        }

        public void SelectEntity (string name, Action<long, string> for_each_row) {
            SimpleTableManager.TableName = "Entity";
            SimpleTableManager.SelectLikeName(name, for_each_row);
        }

        public void EnsureEntity (long? rd, string desc) {
            if (rd == null) return;
            SimpleTableManager.TableName = "Entity";
            var _desc = SimpleTableManager.Get(rd);
            if (_desc == null) {
                SimpleTableManager.Insert(desc, rd);
            }
        }

        //public void UpdateTransaction () {
        //    Transaction.UpdateTransaction();
        //}

    }
}
