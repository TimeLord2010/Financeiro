using DeviceId;
using Financeiro.DB.Entities.Enums;
using SqliteHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.DB.Entities.Sqlite {

    public class Transaction {

        public Transaction(SqliteHandler handler) {
            Handler = handler;
            TransactionPayment = new TransactionPayment(Handler);
        }

        SqliteHandler Handler { get; }
        TransactionPayment TransactionPayment { get; }

        public string RegistrationDate { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public bool IsEnabled { get; set; }

        long date;
        public long Date { 
            get => date; 
            set {
                if (value < 1) throw new Exception("O campo 'Data' recebeu um valor negativo.");
                if (value > DateTimeHelper.ToUnixTimestamp(new DateTime(2030, 1, 1))) throw new Exception("Esta data não é válida.");
                date = value;
            }
        }

        public long? Origin { get; set; }
        public long? Destination { get; set; }
        public long Source { get; set; }
        public long? Category { get; set; }
        //public Entity Origin { get; set; }
        //public Entity Destination { get; set; }
        //public Source Source { get; set; }
        //public Category Category { get; set; }

        public string Insert(double money, double debit, double credit, int installments) {
            var deviceId = new DeviceIdBuilder()
                .AddMotherboardSerialNumber()
                .ToString();
            var rd = $"{DateTime.UtcNow:yyyy/MM/dd HH:mm:ss.fff}";
            //Handler.NonQuery("insert into [Transaction] ([RegistrationDate], [Title], [Description], [Amount], [Date], [Origin], [Destination], [Source], [Category]) " +
            //    "values " +
            //        $"(@rd, @t,'{Description}', @a, @dt, @ori, @des, @sou, @cat)",
            //("@rd", rd),
            //("@t", Title),
            //("@des", Description),
            //("@a", Amount),
            //("@dt", Date),
            //("@ori", Origin),
            //("@des", Destination),
            //("@sou", Source),
            //("@cat", Category));
            Handler.NonQuery("insert into [Transaction] ([RegistrationDate], [Title], [Description], [Amount], [Date], [Origin], [Destination], [Source], [Category]) " +
                "values " +
                    "(@rd, @title, @description, @a, @dt, @ori, @des, @sou, @cat)",
            ("@rd", rd),
            ("@title", Title),
            ("@description", Description),
            ("@a", Amount),
            ("@dt", Date),
            ("@ori", Origin),
            ("@des", Destination),
            ("@sou", Source),
            ("@cat", Category));
            TransactionPayment.Insert(rd, PaymentMethods.Dinheiro, money);
            TransactionPayment.Insert(rd, PaymentMethods.Debito, debit);
            if (installments > 0) credit /= installments;
            for (int i = 0; i < installments; i++) {
                TransactionPayment.Insert(rd, PaymentMethods.Credito, credit);
            }
            return rd;
        }

        public void UpdateTransaction(string registration_date, double money, double debit, double credit) {
            Handler.NonQuery(
                $"update [Transaction] set " +
                $"  Title = @title," +
                $"  Description = @des," +
                $"  Amount = @amount," +
                $"  Date = @dt," +
                $"  Origin = @origin," +
                $"  Destination = @destination, " +
                $"  Category = @category " +
                $"where RegistrationDate = @rd;",
                ("@title", Title),
                ("@des", Description),
                ("@amount", Amount),
                ("@dt", Date),
                ("@origin", Origin),
                ("@destination", Destination),
                ("@category", Category),
                ("@rd", registration_date)
                );
            Handler.NonQuery($"update TransactionPayment set Amount = @amount where [Transaction] = @rd and PaymentMethod = {(long)PaymentMethods.Dinheiro};", ("@amount", money), ("@rd", registration_date));
            Handler.NonQuery($"update TransactionPayment set Amount = @amount where [Transaction] = @rd and PaymentMethod = {(long)PaymentMethods.Debito};", ("@amount", debit), ("@rd", registration_date));
            Handler.NonQuery($"update TransactionPayment set Amount = @amount where [Transaction] = @rd and PaymentMethod = {(long)PaymentMethods.Credito};", ("@amount", credit), ("@rd", registration_date));
        }

        public void UpdateTransaction(double money, double debit, double credit) {
            UpdateTransaction(RegistrationDate, money, debit, credit);
        }

        public void Delete() {
            Delete(RegistrationDate);
        }

        public void Delete(string registration_date) {
            //Handler.NonQuery($"delete from [Transaction] where RegistrationDate = @rd;", ("@rd", registration_date));
            Handler.NonQuery($"update [Transaction] set IsEnabled = 0 where Registration = @rd;", 
                ("@rd", registration_date));
        }

    }
}
