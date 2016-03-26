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
        public static void Clear()
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
        public static void Print(string message)
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
                    w.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "] " + message);
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