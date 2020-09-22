using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB {

    public class MysqlReader {

        public MysqlReader (MySqlDataReader reader) {
            Reader = reader;
        }

        MySqlDataReader Reader { get; }

        public string GetString(int i, string @default = default) => Get(i, Reader.GetString, @default);

        public int GetInt32(int i, int @default = default) => Get(i, Reader.GetInt32, @default);

        public double GetDouble (int i, double @default = default) => Get(i, Reader.GetDouble, @default);

        public DateTime GetDateTime (int i, DateTime @default = default) => Get(i, (i) => Reader.GetMySqlDateTime(i).GetDateTime(), @default);

        public DateTime? GetNullDateTime (int i, DateTime? @default = default) => Get(i, (i) => Reader.GetMySqlDateTime(i).GetDateTime(), @default);

        public long GetInt64(int i, long @default = default) => Get(i, Reader.GetInt64, @default);

        public bool GetBool(int i, bool @default = default) => Get(i, Reader.GetBoolean, @default);

        public bool Read() {
            return Reader.Read();
        }

        T Get <T> (int i, Func<int, T> func, T @default) {
            return Reader.IsDBNull(i) ? @default : func(i);
        }

        public static implicit operator MysqlReader (MySqlDataReader r) {
            return new MysqlReader(r);
        }

    }
}
