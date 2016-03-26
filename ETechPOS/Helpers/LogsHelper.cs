using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ETech.cls;

namespace ETECHPOS.Helpers
{
    public static class LogsHelper
    {
        public static void Clear()
        {
            while (IsFileInUse(new FileInfo(cls_globalvariables.PosTLogsFilePath)) == true) { }
            File.WriteAllText(cls_globalvariables.PosTLogsFilePath, "");
        }
        public static void Print(string message)
        {
            try
            {
                if (message == "") return;
                using (StreamWriter w = File.AppendText(cls_globalvariables.PosTLogsFilePath))
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

        private static bool IsFileInUse(FileInfo fileInfo)
        {
            FileStream stream = null;
            try
            {
                if (!File.Exists(fileInfo.FullName))
                    File.Create(fileInfo.FullName);
                stream = fileInfo.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return false;
        }
    }
}