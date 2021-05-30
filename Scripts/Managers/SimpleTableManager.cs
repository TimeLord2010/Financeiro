using SqliteHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Managers {

    public class SimpleTableManager {

        public SimpleTableManager(SqliteHandler handler, string table_name = null) {
            Handler = handler;
            TableName = table_name;
        }

        SqliteHandler Handler { get; }

        public string TableName { get; set; }

        //public long RegistrationDate { get; private set; }
        //public string Name { get; set; }

        public void SelectLikeName(string name, Action<long,string> for_each_row) {
            Handler.QueryLoop($"select * from [{TableName}] where [Name] like '%{name}%';", (r) => {
                for_each_row(r.GetInt64(0), r.GetString(1));
            });
        }

        public long Insert(string name, long? rd = null) {
            var dt = rd ?? DateTimeHelper.CurrentUnixTimeStamp;
            Handler.NonQuery($"insert into [{TableName}] values (@rd, @n);", ("@rd", dt), ("@n", name));
            return dt;
        }

        public string Get(long? registrationDate) {
            if (registrationDate.HasValue) {
                string name = null;
                Handler.QueryLoop($"select [Name] from [{TableName}] where RegistrationDate = {registrationDate};", (r) => {
                    name = r.GetString(0);
                });
                return name;
            } else {
                return null;
            }
        }

        internal void Update(long value, string text) {
            Handler.NonQuery($"update [{TableName}] set [Name] = @nome where RegistrationDate = {value};", ("@nome", text));
        }

        internal void Delete(long registrationDate) {
            Handler.NonQuery($"delete from {TableName} where RegistrationDate = {registrationDate};");
        }
    }
}
