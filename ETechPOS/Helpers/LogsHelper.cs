using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ETech.cls;

namespace ETech.Helpers
{
    public static class LogsHelper
    {
        public static void ClearTLog()
        {
            try
            {
                FileInfo fileInfo = new FileInfo(cls_globalvariables.PosTLogsFilePath);
                if (!File.Exists(fileInfo.FullName))
                    File.Create(fileInfo.FullName).Dispose();
                File.WriteAllText(fileInfo.FullName, String.Empty);
            }
            catch (Exception ex)
            {
                DialogHelper.ShowDialog(ex.ToString());
            }
        }
        public static void WriteToTLog(string message)
        {
            try
            {
                if (message == "")
                    return;
                FileInfo fileInfo = new FileInfo(cls_globalvariables.PosTLogsFilePath);
                if (!File.Exists(fileInfo.FullName))
                    File.Create(fileInfo.FullName).Dispose();
                using (StreamWriter w = File.AppendText(fileInfo.FullName))
                {
                    string dataTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
                    string logMessage = string.Format("[{0}] {1}", dataTimeNow, message);
                    w.WriteLine(logMessage);
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowDialog(ex.ToString());
            }
        }

        public static void WriteToExceptionLog(string exceptionMessage)
        {
            WriteToExceptionLog(exceptionMessage, "");
        }
        public static void WriteToExceptionLog(string exceptionMessage, int retryNumber)
        {
            WriteToExceptionLog(exceptionMessage, retryNumber, "");
        }
        public static void WriteToExceptionLog(string exceptionMessage, string otherInformation)
        {
            WriteToExceptionLog(exceptionMessage, 0, otherInformation);
        }
        public static void WriteToExceptionLog(string exceptionMessage, int retryNumber, string otherInformation)
        {
            try
            {
                if (exceptionMessage == "")
                    return;
                FileInfo fileInfo = new FileInfo(cls_globalvariables.ErrorExceptionLogsFilePath);
                if (!File.Exists(fileInfo.FullName))
                    File.Create(fileInfo.FullName).Dispose();
                string dataTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
                string terminalNumber = cls_globalvariables.TerminalNumber.ToString();
                using (StreamWriter w = File.AppendText(fileInfo.FullName))
                {
                    string logMessage = string.Format("Date: {0}\nTerminal Number: {1}", dataTimeNow, terminalNumber);
                    logMessage = (retryNumber > 0) ? logMessage + string.Format("\nRetry Number: {0}", retryNumber) : logMessage;
                    logMessage += string.Format("\nException:\n{0}", exceptionMessage);
                    logMessage = (otherInformation != string.Empty) ? logMessage + string.Format("\nOther Information:\n{0}", otherInformation) : logMessage;
                    logMessage += "\n";
                    w.WriteLine(logMessage);
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowDialog(ex.ToString());
            }
        }
    }
}