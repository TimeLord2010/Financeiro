using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Scripts.Exceptions {

    public class NotLoggedIn : Exception {

        public NotLoggedIn (string message) : base(message) {

        }

    }
}
