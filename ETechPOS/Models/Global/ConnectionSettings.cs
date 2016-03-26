using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETech.Models.Global
{
    [Serializable]
    public class ConnectionSettings
    {
        public string Server
        {
            get;
            set;
        }
        public string Database
        {
            get;
            set;
        }
        public string UserId
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }

        public ConnectionSettings()
            : this("", "", "", "")
        {
        }
        public ConnectionSettings(string server, string database, string userId, string password)
        {
            Server = server;
            Database = database;
            UserId = userId;
            Password = password;
        }
    }
}
