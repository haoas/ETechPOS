using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;

namespace ETECHPOS.Helpers
{
    public static class MessageBoxHelper
    {
        public static DialogResult ShowAsteriskMessage(string message)
        {
            DialogResult dialogResult = MessageBox.Show(message, cls_globalvariables.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            return dialogResult;
        }
        public static DialogResult ShowErrorMessage(string message)
        {
            DialogResult dialogResult = MessageBox.Show(message, cls_globalvariables.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            return dialogResult;
        }
        public static DialogResult ShowExclamationMessage(string message)
        {
            DialogResult dialogResult = MessageBox.Show(message, cls_globalvariables.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            return dialogResult;
        }
        public static DialogResult ShowHandMessage(string message)
        {
            DialogResult dialogResult = MessageBox.Show(message, cls_globalvariables.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            return dialogResult;
        }
        public static DialogResult ShowInformationMessage(string message)
        {
            DialogResult dialogResult = MessageBox.Show(message, cls_globalvariables.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            return dialogResult;
        }
        public static DialogResult ShowMessage(string message)
        {
            DialogResult dialogResult = MessageBox.Show(message, cls_globalvariables.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            return dialogResult;
        }
        public static DialogResult ShowQuestionMessage(string message)
        {
            DialogResult dialogResult = MessageBox.Show(message, cls_globalvariables.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            return dialogResult;
        }
        public static DialogResult ShowQuestionMessage(string message, MessageBoxButtons messageBoxButtons)
        {
            DialogResult dialogResult = MessageBox.Show(message, cls_globalvariables.ApplicationName, messageBoxButtons, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            return dialogResult;
        }
        public static DialogResult ShowStopMessage(string message)
        {
            DialogResult dialogResult = MessageBox.Show(message, cls_globalvariables.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
            return dialogResult;
        }
        public static DialogResult ShowWarningMessage(string message)
        {
            DialogResult dialogResult = MessageBox.Show(message, cls_globalvariables.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            return dialogResult;
        }

        public static void ShowExceptionMessageWithPrintLogs(string message, string errorMessage)
        {
            MessageBoxHelper.ShowErrorMessage(message);
            LogsHelper.Print(message + "\n" + errorMessage);
        }
    }
}
