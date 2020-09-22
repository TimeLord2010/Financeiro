using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Scripts.DataStructures {
    
    public class EntityInfoManager {

        public EntityInfoManager () { }

        public Dictionary<long, EntityInfo> Infos = new Dictionary<long, EntityInfo>();

        public void Process (long? origin, string originName, long? destination, string destination_name, double amount) {
            Process(origin, originName, -amount);
            Process(destination, destination_name, amount);
        }

        void Process (long? rd, string name, double amount) {
            if (!rd.HasValue) return;
            long _rd = rd.Value;
            if (!Infos.ContainsKey(_rd)) Infos.Add(_rd, new EntityInfo(name));
            var ei = Infos[_rd];
            ei.Operation = amount;
        }


    }
}
