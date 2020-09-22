using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.String;

namespace Financeiro.Scripts {

    public class CustomMaskedTextBox {

        public CustomMaskedTextBox (string mask) {
            Mask = mask;
        }

        bool IsEditing = false;
        string Mask { get; }

        public void ApplyMask (TextBox tb) {
            tb.TextChanged += Tb_TextChanged;
        }

        private void Tb_TextChanged(object sender, TextChangedEventArgs e) {
            if (Mask.Length == 0) return;
            if (IsEditing) return;
            IsEditing = true;
            var tb = sender as TextBox;
            var caret_position = tb.CaretIndex;
            string original_text = tb.Text;
            for (int i = 0; i < original_text.Length && i < Mask.Length; i++) {
                var mask_char = Mask[i];
                var text_char = original_text[i];
                if (mask_char == '0') {
                    if (text_char < '0' || text_char > '9') {
                        original_text = original_text.Remove(i--, 1);
                        if (i+1 < caret_position) 
                            caret_position--;
                    }
                } else if (mask_char != text_char) {
                    original_text = original_text.Insert(i, mask_char+"");
                    if (i < caret_position) 
                        caret_position++;
                }
            }
            if (original_text.Length > Mask.Length) original_text = Join("", original_text.Take(Mask.Length));
            tb.Text = original_text;
            tb.CaretIndex = caret_position;
            IsEditing = false;
        }

    }
}
