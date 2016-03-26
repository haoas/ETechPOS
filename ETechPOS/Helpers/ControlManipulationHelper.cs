using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ETech.Helpers
{
    public static class ControlManipulationHelper
    {
        public static void HighLightTextInControl(Control control)
        {
            if (control is TextBox)
            {
                TextBox textBox = (TextBox)control;
                textBox.Select(0, textBox.Text.Length);
            }
            else if (control is ComboBox)
            {
                ComboBox comboBox = (ComboBox)control;
                comboBox.Select(0, comboBox.Text.Length);
            }
            else if (control is RichTextBox)
            {
                RichTextBox richTextBox = (RichTextBox)control;
                richTextBox.Select(0, richTextBox.Text.Length);
            }
            else if (control is NumericUpDown)
            {
                NumericUpDown numericUpDown = (NumericUpDown)control;
                numericUpDown.Select(0, numericUpDown.Text.Length);
            }
        }
        public static void RemoveAllUserControlInContainer(Control container)
        {
            for (int i = 0; i < container.Controls.Count; )
            {
                Control control = container.Controls[i];
                if (control is UserControl)
                    container.Controls.Remove(control);
                else
                    i++;
            }
        }
        public static void HideAllUserControlInContainer(Control container)
        {
            for (int i = 0; i < container.Controls.Count; i++)
            {
                Control control = container.Controls[i];
                if (!(control is UserControl))
                    continue;
                control.Visible = false;
            }
        }

        public static void TrimTextInControl(Control control)
        {
            control.Text = control.Text.Trim();
        }

        public static bool IsControlAtFront(Control control)
        {
            return control.Parent.Controls.GetChildIndex(control) == 0;
        }

        public static void AddRowNumberInDataGridView(DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
                dataGridView["dgvcRowNumber", i].Value = i + 1;
        }
    }
}
