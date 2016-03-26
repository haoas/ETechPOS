using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;

namespace ETECHPOS.Helpers
{
    public static class DialogHelper
    {
        public static DialogResult ShowDialog(string message)
        {
            return ShowDialog(message, MessageBoxButtons.OK);
        }
        public static DialogResult ShowDialog(string message, MessageBoxButtons messageBoxButtons)
        {
            DialogForm dialogForm = new DialogForm(message, messageBoxButtons);
            dialogForm.ShowDialog();
            return dialogForm.DialogResult;
        }

        public static void ShowDialogWithPrintLogs(string message, string errorMessage)
        {
            ShowDialog(message);
            LogsHelper.Print(message + "\n" + errorMessage);
        }
    }
}
