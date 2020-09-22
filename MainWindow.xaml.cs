using Financeiro.Scripts.DB.DynamoDB;
using Financeiro.Scripts.Managers;
using Financeiro.UserControls;
using System;
using System.Collections.Generic;
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
using System.Net.NetworkInformation;
using System.Net;

namespace Financeiro {

    public partial class MainWindow : Window {

        public MainWindow() {
            ErrorReporter.ShowMessage = (ex, title) => {
                MessageBox.Show(ex.Message, title ?? $"{ex.GetType()}");
            };
            ErrorReporter.OnAfterReport = (ex) => {
                var dt = DateTime.UtcNow;
                var net_interfaces = NetworkInterface.GetAllNetworkInterfaces();
                if (net_interfaces.Count() == 0) return;
                var net_interface = net_interfaces.First();
                var result = UploadToDynamoDBTable.Upload("financeiro_errors", new { 
                    User = net_interface.GetPhysicalAddress().ToString() + " - " + Environment.UserName,
                    DateTime = DateTimeHelper.ToUnixTimestamp(dt),
                    DateTimeStr = $"{dt:yyyy/MM/dd HH:mm:ss}",
                    Message = ex.Message,
                    Type = ex.GetType().ToString()
                }).Result;
            };
            InitializeComponent();
        }
    }
}
