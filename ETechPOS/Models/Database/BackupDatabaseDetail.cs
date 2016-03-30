using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel;
using System.Linq.Expressions;

namespace ETech.Models.Database
{
    [Serializable]
    public class BackupDatabaseDetail
    {
        public string Name
        {
            get;
            set;
        }
        public string FileLocation
        {
            get;
            set;
        }
        public string Time
        {
            get;
            set;
        }

        public BackupDatabaseDetail()
            : this("", "", "")
        {
        }
        public BackupDatabaseDetail(string name, string fileLocation, string time)
        {
            Name = name;
            FileLocation = fileLocation;
            Time = time;
        }
    }
}
