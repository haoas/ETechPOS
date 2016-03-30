using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using ETech.cls;
using ETech.Helpers;

namespace ETech.fnc
{
    public static class DatabaseFunction
    {
        public static bool BackupDatabase(string fileNamePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(fileNamePath);
                string userId = cls_globalvariables.ConnectionSettings.UserId;
                string password = cls_globalvariables.ConnectionSettings.Password;
                string server = cls_globalvariables.ConnectionSettings.Server;
                string database = cls_globalvariables.ConnectionSettings.Database;
                string folderPath = fileInfo.DirectoryName;
                string fullFileNamePath = fileInfo.FullName;
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                string cmdstr = "";

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = cls_globalvariables.MySqlDumpApplicationPath;
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                cmdstr = string.Format("-r \"" + "{0}\"" + " -u{1} -p{2} -h{3} {4} --routines --max_allowed_packet=1G --default-character-set=utf8 --single-transaction --quick", fullFileNamePath, userId, password, server, database);
                psi.Arguments = cmdstr;
                psi.UseShellExecute = false;
                Process process = Process.Start(psi);
                process.WaitForExit();
                process.Close();
                LogsHelper.WriteToTLog("Backup created in \"" + fullFileNamePath + "\"");
                return true;
            }
            catch (Exception ex)
            {
                LogsHelper.WriteToTLog("Backup database failed: " + ex.ToString());
                return false;
            }
        }
    }
}
