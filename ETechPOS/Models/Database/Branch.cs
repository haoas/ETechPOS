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
    public class Branch
    {
        public long Id
        {
            get;
            set;
        }
        public string Code
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }

        public Branch()
            : this(0, "", "")
        {
        }
        public Branch(long wid, string code, string name)
        {
            Id = wid;
            Code = code;
            Name = name;
        }
    }

    [Serializable]
    public class BranchList : BindingList<Branch>
    {
    }
}
