using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace ETech.cls
{
    public class cls_user
    {
        //attributes
        private string code;
        private string fullname;
        private List<int> permission;
        private long syncid;

        public void init()
        {
            this.code = "";
            this.fullname = "";
            this.permission = new List<int>();
            this.syncid = 0;
        }

        //constructor
        public cls_user()
        {
            init();
        }

        public cls_user(long wid_d)
        {
            init();
            setcls_user_by_wid(wid_d);
        }

        public cls_user(long wid_d, bool is_history)
        {
            init();
            setcls_user_by_wid(wid_d, is_history);
        }

        public void setcls_user(string code_d, string fullname_d, List<int> permission_d, int wid_d)
        {
            this.code = code_d;
            this.fullname = fullname_d;
            this.permission = permission_d;
            this.syncid = wid_d;
        }

        public string getusercode()
        {
            return this.code;
        }

        //getfullname - return fullname
        public string getfullname()
        {
            return this.fullname;
        }
        //checkpermission - return true or false if permission is included in the list
        public bool checkpermission(int perm)
        {
            return permission.Contains(perm);
        }

        public List<int> getpermission()
        {
            return this.permission;
        }

        public long getsyncid()
        {
            return this.syncid;
        }

        public void setcls_user_by_wid(long SyncId, bool is_history)
        {
            this.syncid = SyncId;
            string sSQL = "SELECT * FROM `user` WHERE `SyncId` = " + SyncId;
            
            if (!is_history)
            {
                sSQL += " AND `show` = 1 AND `status` = 1 ";
            }

            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
            {
                this.syncid = 0;
                return;
            }
            DataRow dr = dt.Rows[0];
            this.code = dr["usercode"].ToString();
            this.fullname = dr["fullname"].ToString();

            sSQL = "SELECT * FROM `userpermission` WHERE `userid` = " + SyncId;
            dt = mySQLFunc.getdb(sSQL);
            this.permission.Clear();
            foreach (DataRow dr_d in dt.Rows)
            {
                this.permission.Add(Convert.ToInt32(dr_d["code"]));
            }
        }

        public void setcls_user_by_wid(long wid_d)
        {
            setcls_user_by_wid(wid_d, false);
        }

    }
}
