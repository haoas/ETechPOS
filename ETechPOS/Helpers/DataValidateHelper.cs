using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ETech.Helpers
{
    public static class DataValidator
    {
        public static bool HasInputData(Control control, string message)
        {
            if (control.Text.Length == 0)
            {
                DialogHelper.ShowDialog(message);
                if (!(control is CheckBox || control is RadioButton))
                    control.Select();
                return false;
            }
            return true;
        }
        public static bool HasInputData(Control control, bool condition, string message)
        {
            if (condition)
            {
                DialogHelper.ShowDialog(message);
                if (!(control is CheckBox || control is RadioButton))
                    control.Select();
                return false;
            }
            return true;
        }
    }
}
