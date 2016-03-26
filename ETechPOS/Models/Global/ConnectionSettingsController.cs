using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ETech.cls;
using ETech.Variables;
using ETech.Helpers;
using ETech.fnc;

namespace ETech.Models.Global
{
    public static class ConnectionSettingsController
    {
        public static ConnectionSettings GetData()
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(cls_globalvariables.ConnectionSettingsXmlPath);
                XElement settingsElem = xmlDoc.Element("Settings");

                ConnectionSettings connectionSettings = new ConnectionSettings();
                connectionSettings.Server = settingsElem.Element("Server").Value;
                connectionSettings.Database = settingsElem.Element("Database").Value;
                connectionSettings.UserId = settingsElem.Element("UserId").Value;
                connectionSettings.Password = SaltEncryptionFunction.Decrypt(settingsElem.Element("Password").Value);

                return connectionSettings;
            }
            catch (Exception ex)
            {
                DialogHelper.ShowDialogWithPrintLogs(MessagesVariable.FailedLoadingSettings, ex.ToString());
                return null;
            }
        }

        public static bool TryGetData(out ConnectionSettings connectionSettings)
        {
            connectionSettings = GetData();
            if (connectionSettings == null)
                return false;
            return true;
        }
    }
}
