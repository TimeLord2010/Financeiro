using Financeiro.Scripts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Financeiro.UserControls {

    public partial class DateInterval : UserControl {

        public DateInterval() {
            InitializeComponent();
            var mask_tb = new CustomMaskedTextBox("00/00/00");
            mask_tb.ApplyMask(BeginMTB);
            mask_tb.ApplyMask(EndMTB);
        }

        public DateTime BeginDT {
            get {
                try {
                    return DateTime.ParseExact(BeginMTB.Text, "dd/MM/yy", CultureInfo.InvariantCulture);
                } catch (Exception) {
                    return new DateTime(1970,1,1);
                }
            }
        }

        public long BeginTimestamp {
            get => DateTimeHelper.ToUnixTimestamp(BeginDT);
        }

        public DateTime EndDT {
            get {
                try {
                    return DateTime.ParseExact(EndMTB.Text, "dd/MM/yy", CultureInfo.InvariantCulture);
                } catch (Exception) {
                    return new DateTime(2030,1,1);
                }
            }
        }

        public long EndTimestamp {
            get => DateTimeHelper.ToUnixTimestamp(EndDT);
        }

    }
}