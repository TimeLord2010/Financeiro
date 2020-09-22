using SqliteHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.DB.Entities.Sqlite {

    public class Source : SqliteTable<Source> {

        public long RegistrationDate { get; set; }
        public string Name { get; set; }

    }
}
