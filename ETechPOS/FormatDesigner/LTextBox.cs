using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ETech.FormatDesigner
{
    public static class LTextBox
    {
        public static void AsSigned2DecimalTextBox(this TextBox TB)
        {
            TB.KeyPress += OnSigned2DecimalTextBox_KeyPress;
        }

        private static void OnSigned2DecimalTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Regex.IsMatch((sender as TextBox).Text, @"\.\d\d") && e.KeyChar != 8)
            {
                e.Handled = true;
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
            if (e.KeyChar == '-' && (sender as TextBox).Text.Contains('-'))
            {
                e.Handled = true;
            }
        }
        
        public static void AsUnsigned2DecimalTextBox(this TextBox TB)
        {
            TB.KeyPress += OnUnsigned2DecimalTextBox_KeyPress;
        }

        private static void OnUnsigned2DecimalTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Regex.IsMatch((sender as TextBox).Text, @"\.\d\d") && e.KeyChar != 8)
            {
                e.Handled = true;
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        public static void AsInteger(this TextBox TB)
        {
            TB.KeyPress += OnIntegerTextBox_KeyPress;
        }

        private static void OnIntegerTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


    }

}
