using Financeiro.DB.Entities.Enums;
using Financeiro.DB.Entities.Sqlite;
using Financeiro.Managers;
using SqliteHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.DB.Entities.Sqlite {

    public class TransactionV {

        public TransactionV(SqliteHandler handler) {
            Handler = handler;
        }

        //SqliteFinanceiro Financeiro = new SqliteFinanceiro();
        SqliteHandler Handler { get; }

        public string RegistrationDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public long Date { get; set; }
        public string DateStr { get; set; }
        public long? Origin { get; set; }
        public string OriginName { get; set; }
        public long? Destination { get; set; }
        public string DestinationName { get; set; }
        public long? Source { get; set; }
        public string SourceName { get; set; }
        public long? Category { get; set; }
        public string CategoryName { get; set; }

        public void Select(Action<TransactionV> for_each_row, string where_statement = "", uint limit = 2000, params (string, object)[] parameters) {
            string ws = "";
            if (where_statement.Length > 0) {
                ws = $"where {where_statement}";
            }
            Handler.QueryLoop($"Select * from TransactionV {ws} order by Date asc limit {limit}", (r) => {
                var dt = DateTimeHelper.UnixTimeStampToDateTime((long)r["Date"]).ToString("dd/MM/yy HH:mm");
                if (dt.EndsWith(" 00:00")) {
                    dt = dt.Substring(0, 8);
                }
                var t = new TransactionV(null) {
                    RegistrationDate = r["RegistrationDate"] as string,
                    Title = r["Title"] as string,
                    Description = r["Description"] as string,
                    Amount = (double)r["Amount"],
                    Date = (long)r["Date"],
                    DateStr = dt,
                    Origin = Converter.TryToNullInt64(r["Origin"]),
                    OriginName = r["OriginName"] as string,
                    Destination = Converter.TryToNullInt64(r["Destination"]),
                    DestinationName = r["DestinationName"] as string,
                    Source = Converter.TryToNullInt64(r["Source"]),
                    SourceName = r["SourceName"] as string,
                    Category = Converter.TryToNullInt64(r["Category"]),
                    CategoryName = r["CategoryName"] as string
                };
                for_each_row(t);
            }, parameters);
        }


    }
}
