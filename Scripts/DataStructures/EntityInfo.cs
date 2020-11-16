using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Scripts.DataStructures {
    
    public class EntityInfo {

        public EntityInfo (string entity_name) {
            EntityName = entity_name;
        }

        public string EntityName { get; }

        public double Expenses { get; private set; }

        public double Income { get; private set; }

        public double Total {
            get => Income - Expenses;
        }

        public double Operation {
            set {
                if (value < 0) {
                    Expenses += -value;
                } else {
                    Income += value;
                }
            }
        }

    }
}
