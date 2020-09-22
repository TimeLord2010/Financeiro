using Financeiro.DB.Entities.Enums;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClinica.Scripts;
using WPFClinica.Scripts.DB;
//using static WPFClinica.Scripts.DB.SQLOperations;

namespace WPFClinica.Scripts.DB {

    class Historico_Procedimento {

        public Historico_Procedimento (MySqlH handler) {
            Handler = handler;
        }

        public const string Name = nameof(Historico_Procedimento);
        MySqlH Handler { get; }

        public int ID { get; set; }
        public int Proc_ID { get; set; }
        public string Paciente { get; set; }
        public DateTime RealizadoEm { get; set; }
        public DateTime? PagoEm { get; set; }
        public double Valor { get; set; }

        //public static void CreateTable() {
        //    NonQuery("Erro ao criar tabela de histórico de procedimento.",
        //        $"create table if not exists {Name} (" +
        //        $"  {nameof(ID)} int auto_increment," +
        //        $"  {nameof(Proc_ID)} int," +
        //        $"  {nameof(Paciente)} varchar(15)," +
        //        $"  {nameof(RealizadoEm)} datetime," +
        //        $"  {nameof(PagoEm)} datetime null," +
        //        $"  {nameof(Valor)} double," +
        //        $"  primary key ({nameof(ID)})," +
        //        $"  foreign key ({nameof(Paciente)}) references {Pacientes.Name} ({nameof(Pacientes.CPF)}) on delete no action on update cascade," +
        //        $"  foreign key ({nameof(Proc_ID)}) references {Procedimentos.Name} ({nameof(Procedimentos.ID)}) on delete no action on update cascade" +
        //        $");");
        //}

        //public static void Insert(int proc, string cpf, DateTime realizacao, DateTime? pago, double valor) {
        //    NonQuery($"Erro ao inserir histórico de procedimentos. ({ErrorCodes.DB0034})", (c) => {
        //        c.CommandText = $"insert into {Name} (Proc_ID, Paciente, RealizadoEm, PagoEm, Valor) values (@proc, @pac, @rea, {(pago.HasValue ? "@pag" : "null")}, @val);";
        //        c.Parameters.AddWithValue("@proc", proc);
        //        c.Parameters.AddWithValue("@pac", cpf);
        //        c.Parameters.AddWithValue("@rea", realizacao.ToString("yyyy-MM-dd HH:mm:ss"));
        //        if (pago != null) c.Parameters.AddWithValue("@pag", pago.Value.ToString("yyyy-MM-dd HH:mm:ss"));
        //        c.Parameters.AddWithValue("@val", valor);
        //        return c;
        //    }, (ex) => {
        //        if (ex.Message.Contains("doesn't exist")) {
        //            CreateTable();
        //        }
        //    }, true);
        //}

        //public static void Update_Realizacao(int id, DateTime realizacao) {
        //    NonQuery("Erro ao atualizar data de realizacao de procedimento médico", (c) => {
        //        c.CommandText = $"update {Name} set {nameof(RealizadoEm)} = @rea where {nameof(ID)} = @id;";
        //        c.Parameters.AddWithValue("@id", id);
        //        c.Parameters.AddWithValue("@rea", realizacao.ToString("yyyy-MM-dd HH:mm:ss"));
        //        return c;
        //    });
        //}

        //public static void Delete(int id) {
        //    NonQuery("Erro ao deletar histórico procedimento médico.", (c) => {
        //        c.CommandText = $"delete from {Name} where {nameof(ID)} = @id;";
        //        c.Parameters.AddWithValue("@id", id);
        //        return c;
        //    });
        //}

        //public static List<Historico_Procedimento> Select(int proc, string paciente, int limit = 1) {
        //    var c = new MySqlCommand(
        //        $"select" +
        //        $"  a.{nameof(ID)}, " +
        //        $"  a.{nameof(Proc_ID)}, " +
        //        $"  a.{nameof(Paciente)}, " +
        //        $"  a.{nameof(RealizadoEm)}," +
        //        $"  a.{nameof(PagoEm)}," +
        //        $"  a.{nameof(Valor)} " +
        //        $"from {Name} as a " +
        //        $"where a.{nameof(Paciente)} = @cpf and a.{nameof(Proc_ID)} = {proc} " +
        //        $"order by {nameof(ID)} desc " +
        //        $"limit {limit};");
        //    c.Parameters.AddWithValue("@cpf", paciente);
        //    var lista = new List<Historico_Procedimento>();
        //    QueryRLoop($"Erro em historico-procedimento. ({ErrorCodes.DB0055})", c, (r) => {
        //        fillList(lista, r);
        //    });
        //    return lista;
        //}

        public List<(PaymentMethods, double)> GetPayment(int ID) {
            var list = new List<(PaymentMethods, double)>();
            Handler.QueryRLoop($"select pagamento, valor from procedimentoslab_formapagamento where Historico = {ID};", (re) => {
                list.Add((re.GetInt32(0) switch
                {
                    1 => PaymentMethods.Dinheiro,
                    2 => PaymentMethods.Credito,
                    3 => PaymentMethods.Debito,
                    _ => throw new Exception($"Invalid Payment method.")
                }, re.GetDouble(1)));
            });
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procedimento"></param>
        /// <param name="tipo"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="foreach_row">CPF, RealizadoEm, Valor, NomePaciente, Procedimento, Tipo</param>
        public void Select_Realizado(string procedimento, string tipo, DateTime begin, DateTime end, Action<string, DateTime, double, string, string, string, List<(PaymentMethods, double)>> foreach_row) {
            string c = 
                $"select " +
                $"  a.{nameof(ID)}, " +
                $"  a.{nameof(Proc_ID)}, " +
                $"  a.{nameof(Paciente)}, " +
                $"  a.{nameof(RealizadoEm)}," +
                $"  a.{nameof(PagoEm)}," +
                $"  a.{nameof(Valor)}," +
                $"  b.Nome," +
                $"  c.Tipo," +
                $"  c.Descricao," +
                $"  d.Tipo " +
                $"from {Name} as a " +
                $"inner join Pessoas as b on a.{nameof(Paciente)} = b.CPF " +
                $"inner join Procedimentos as c on a.Proc_ID = c.ID " +
                $"inner join TipoProcedimento as d on c.Tipo = d.ID " +
                $"where " +
                $"  c.Descricao like concat(@proc, '%') and " +
                $"  d.Tipo like concat(@tipo, '%') and " +
                $"  (RealizadoEm between @begin and @end) " +
                $"limit 3000;";
            Handler.QueryRLoop(c, (r) => {
                var ID = r.GetInt32(0);
                var Proc_ID = r.GetInt32(1);
                var CPF = r.GetString(2);
                var RealizadoEm = r.GetDateTime(3);
                var PagoEm = r.GetNullDateTime(4);
                var Valor = r.GetDouble(5);
                var nome_paciente = r.GetString(6);
                var Tipo_ID = r.GetInt32(7);
                var Procedimento = r.GetString(8);
                var Tipo = r.GetString(9);
                foreach_row(CPF, RealizadoEm, Valor, nome_paciente, Procedimento, Tipo, GetPayment(ID));
            }, 
                ("@proc", procedimento),
                ("@tipo", tipo),
                ("@begin", begin),
                ("@end", end)
            );
        }

        //public static void fillList(List<Historico_Procedimento> lista, MySqlDataReader r) {
        //    lista.Add(new Historico_Procedimento() { 
        //        ID = r.GetInt32(0),
        //        Proc_ID = r.GetInt32(1),
        //        Paciente = r.GetString(2),
        //        RealizadoEm = r.GetMySqlDateTime(3).GetDateTime(),
        //        PagoEm = r.IsDBNull(4) ? (DateTime?)null : r.GetMySqlDateTime(4).GetDateTime(),
        //        Valor = r.GetDouble(5)
        //    });
        //}

    }
}
