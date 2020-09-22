using DB;
using MySql.Data.MySqlClient;
using System;

public class MySqlH {

    public string CS;
    public bool ThrowExceptions { get; set; } = false;

    public MySqlH(MySqlConnectionStringBuilder sb) {
        CS = sb.ToString();
    }

    public MySqlH(string cs) {
        CS = cs;
    }

    public void NonQuery(string a, params (string, object)[] parameters) {
        Command(a, (command) => {
            command.ExecuteNonQuery();
        }, parameters);
    }

    public void QueryR(string q, Action<MysqlReader> action, params (string, object)[] parameters) {
        Command(q, (c) => {
            using var reader = c.ExecuteReader();
            action(reader);
        }, parameters);
    }

    public void QueryRLoop(string c, Action<MysqlReader> action, params (string, object)[] parameters) {
        QueryR(c, (r) => {
            while (r.Read()) {
                action(r);
            }
        }, parameters);
    }

    private void Connection(Action<MySqlConnection> action) {
        using var connection = new MySqlConnection(CS);
        connection.Open();
        action(connection);
    }

    private void Command(string b, Action<MySqlCommand> a, params (string, object)[] parameters) {
        Connection((c) => {
            using var command = c.CreateCommand();
            command.CommandText = b;
            foreach (var (name, value) in parameters) {
                object v = value;
                if (value is DateTime dt) {
                    v = dt.ToString("yyyy-MM-dd HH:mm:ss");
                }
                command.Parameters.AddWithValue(name, v);
            }
            a(command);
        });
    }

}