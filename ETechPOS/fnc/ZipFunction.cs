using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;
using ETech.Helpers;

namespace ETech.fnc
{
    public static class ZipFunction
    {
        public static bool ZipFile(string zipFilePath, string fileNamePath)
        {
            try
            {
                ZipFile zip = new ZipFile();
                zip.UseZip64WhenSaving = Zip64Option.Always;
                zip.AddItem(fileNamePath, "");
                zip.Save(zipFilePath);
                LogsHelper.WriteToTLog("Zip file created in \"" + zipFilePath + "\"");
                return true;
            }
            catch (Exception ex)
            {
                LogsHelper.WriteToTLog("Zip file failed: " + ex.ToString());
                return false;
            }
        }
        public static bool ZipFile(string zipFilePath, List<string> fileNamePathList)
        {
            try
            {
                ZipFile zip = new ZipFile();
                zip.UseZip64WhenSaving = Zip64Option.Always;
                foreach (string fileNamePath in fileNamePathList)
                    zip.AddItem(fileNamePath, "");
                zip.Save(zipFilePath);
                LogsHelper.WriteToTLog("Zip file created in \"" + zipFilePath + "\"");
                return true;
            }
            catch (Exception ex)
            {
                LogsHelper.WriteToTLog("Zip file failed: " + ex.ToString());
                return false;
            }
        }
    }
}