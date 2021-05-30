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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Financeiro.UserControls.More {

    public partial class FeedbackControl : Window {

        public FeedbackControl() {
            InitializeComponent();
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        readonly DispatcherTimer Timer = new DispatcherTimer() { 
            Interval = TimeSpan.FromSeconds(2)
        };

        private void Timer_Tick(object sender, EventArgs e) {
            if (DescriptionTB.Text.Length != 0) {
                TitleTBL.Text = "Título (opcional)";
            } else {
                TitleTBL.Text = "Título";
            }
            if (TitleTB.Text.Length != 0) {
                DescriptionTBL.Text = "Descrição (opcional)";
            } else {
                DescriptionTBL.Text = "Descrição";
            }
        }

        private void EnviarB_Click(object sender, RoutedEventArgs e) {
            try {
                ShouldSend();
                var selected = TypeCB.SelectedItem as ComboBoxItem;
                var type = selected.Content.ToString();
                Close();
            } catch (Exception ex) {
                MessageBox.Show(
                    ex.Message, 
                    "Erro", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }

        void ShouldSend () {
            if (TitleTB.Text.Length == 0 && DescriptionTB.Text.Length == 0) {
                throw new Exception($"Mensagem vazia.");
            }
        }


    }
}
