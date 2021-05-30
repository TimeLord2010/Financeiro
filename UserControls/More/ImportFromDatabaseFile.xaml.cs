using Financeiro.Scripts;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Financeiro.UserControls.More {

    public partial class ImportFromDatabaseFile : UserControl {

        public ImportFromDatabaseFile(string fn) {
            InitializeComponent();
        }

        string FileName;
        WindowMaskHelper MaskHelper;

        private async void StartB_Click(object sender, RoutedEventArgs e) {
            using var sr = new StreamReader(FileName);
            while (!sr.EndOfStream) {
                var line = await sr.ReadLineAsync();
                ProcessLine(line);
            }
            MaskHelper.ClearMask();
        }

        void ProcessLine (string line) {
            ListInfoLV.Items.Add(line);
            if (ListInfoLV.Items.Count > 20) {
                ListInfoLV.Items.RemoveAt(0);
            }
            // Nome, Data, Categoria, Categorias, Fonte, Destino
            var parts = line.Split(',');
        }
    }
}
