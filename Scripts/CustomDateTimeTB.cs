using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Financeiro.Scripts {

    public class CustomDateTimeTB : CustomMaskedTextBox {

        public CustomDateTimeTB() : base("00/00/00 00:00") {

        }
    }
}
