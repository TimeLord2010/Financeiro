using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Scripts.Exceptions {

    public class SessionTimeout : Exception {

        public SessionTimeout (string message) : base(message) {

        }

    }
}
