using Financeiro.DB.Entities.Enums;
using SqliteHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.DB.Entities.Sqlite {

    public class TransactionPayment {

        public TransactionPayment (SqliteHandler handler) {
            Handler = handler;
        }

        SqliteHandler Handler { get; }

        public long Transaction { get; set; }
        public long PaymentMethod { get; set; }
        public double Amount { get; set; }
        public string RegistrationDate { get; set; }

        public void Insert (string transaction, long payment_method, double amount) {
            Handler.NonQuery($"insert into TransactionPayment " +
                $"([Transaction], [PaymentMethod], [Amount], [RegistrationDate]) " +
                $"values " +
                $"(@tran, {payment_method}, @amount, @rd);",
                ("@amount", amount), 
                ("@tran", transaction),
                ("@rd", $"{DateTime.UtcNow:yyyy/MM/dd HH:mm:ss.fff}"));
        }

        public void Insert(string transaction, PaymentMethods payment, double money) {
            Insert(transaction, (long)payment, money);
        }

        public List<(PaymentMethods, double)> Select (string transaction) {
            var list = new List<(PaymentMethods, double)>();
            Handler.QueryLoop($"select [PaymentMethod], [Amount] from TransactionPayment where [Transaction] = @tran;", (r) => {
                list.Add(((PaymentMethods)r.GetInt64(0) ,r.GetDouble(1)));
            }, ("@tran", transaction));
            return list;
        }

        public void Update (string transaction, long payment_method, double amount) {
            Handler.NonQuery($"update TransactionPayment set Amount = @amount " +
                $"where Transaction = @tran and PaymentMethod = {payment_method};", 
                ("@amount", amount), 
                ("@tran", transaction));
        }

        public void Update (string transaction, PaymentMethods payment_method, double amount) {
            Update(transaction, (long)payment_method, amount);
        }

        internal void Delete(string transaction) {
            Handler.NonQuery($"delete from TransactionPayment where [Transaction] = @tran;", ("@tran", transaction));
        }
    }
}
