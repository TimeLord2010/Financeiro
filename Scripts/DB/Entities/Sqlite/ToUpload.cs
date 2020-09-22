using SqliteHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.DB.Entities.Sqlite {
    
    public class ToUpload {

        public ToUpload(SqliteHandler handler) {
            Handler = handler;
        }

        SqliteHandler Handler { get; }

        public string TableName { get; set; }
        public string RegistrationDate { get; set; }

        public void SelectToUpload (Action<string, string> for_each) {
            Handler.QueryLoop($"select * from ToUpload", (r) => {
                for_each(r.GetString(0), r.GetString(1));
            });
        }

        public void Clear () {
            Handler.NonQuery($"delete from ToUpload;");
        }

        public void Delete (string table_name, string registration_date) {
            Handler.NonQuery($"delete from ToUpload where TableName = @tn and RegistrationDate = @rd;",
                ("@tn", table_name),
                ("@rd", registration_date));
        }

        internal long SelectCount() {
            long count = -1;
            Handler.QueryLoop($"select Count(*) from ToUpload;", (r) => {
                count = r.GetInt64(0);
            });
            return count;
        }
    }
}
