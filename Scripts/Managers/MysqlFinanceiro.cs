using Financeiro.Managers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Managers {

    public class MysqlFinanceiro {

        public MysqlFinanceiro(SqliteFinanceiro financeiro) {
            SqliteFinanceiro = financeiro;
        }

        SqliteFinanceiro SqliteFinanceiro { get; }

        MySqlH mySqlH;
        public MySqlH MySqlH {
            get {
                if (mySqlH == null) mySqlH = new MySqlH(GetServerConnectionString);
                return mySqlH;
            }
            set => mySqlH = value;
        }

        public string GetServerConnectionString {
            get {
                var connection_string_builder = new MySqlConnectionStringBuilder {
                    ConnectionTimeout = 10,
                    ConnectionLifeTime = 0
                };
                SqliteFinanceiro.LoopSettings((name, value) => {
                    switch (name) {
                        case "Database":
                            connection_string_builder.Database = value;
                            break;
                        case "UserID":
                            connection_string_builder.UserID = value;
                            break;
                        case "Password":
                            connection_string_builder.Password = value;
                            break;
                        case "Server":
                            connection_string_builder.Server = value;
                            break;
                    }
                });
                return connection_string_builder.ToString();
            }
        }

        public (string db, string user, string pwd, string server) Parameters {
            get {
                string db = "", user = "", password = "", server = "";
                SqliteFinanceiro.LoopSettings((name, value) => {
                    switch (name) {
                        case "Database":
                            db = value;
                            break;
                        case "UserID":
                            user = value;
                            break;
                        case "Password":
                            password = value;
                            break;
                        case "Server":
                            server = value;
                            break;
                    }
                });
                return (db, user, password, server);
            }
            set {
                var (db, user, password, server) = value;
                SqliteFinanceiro.SetSettingsConnection(db, user, password, server);
            }
        }

    }
}
