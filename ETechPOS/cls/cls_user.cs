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
        public long syncid;
        private string code;
        private string fullname;
        public string username { get; private set; }
        public string password { get; private set; }
        public string position { get; private set; }
        public List<string> AuthorizationList { get; private set; }
        public int status { get; private set; }

        public void init()
        {
            this.code = "";
            this.fullname = "";
            this.username = "";
            this.password = "";
            this.position = "";
            this.AuthorizationList = new List<string>();
            this.syncid = 0;
            this.status = 0;
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

        public void setcls_user(string code_d, string fullname_d, List<string> authorization_d, long syncId_d)
        {
            this.code = code_d;
            this.fullname = fullname_d;
            this.AuthorizationList = authorization_d;
            this.syncid = syncId_d;
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

        public bool CheckAuth(string auth)
        {
            if (AuthorizationList.Contains("ALL") || 
                AuthorizationList.Contains(auth))
                return true;
            else
                return false;
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
                sSQL += " AND `status` = 1 ";
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
            this.username = dr["username"].ToString();
            this.password = dr["password"].ToString();
            this.position = dr["position"].ToString();
            this.status = int.Parse(dr["status"].ToString());

            sSQL = "SELECT * FROM `userauth` WHERE `userid` = " + SyncId;
            dt = mySQLFunc.getdb(sSQL);
            this.AuthorizationList.Clear();
            foreach (DataRow dr_d in dt.Rows)
            {
                this.AuthorizationList.Add(dr_d["authorization"].ToString());
            }
        }

        public void setcls_user_by_wid(long wid_d)
        {
            setcls_user_by_wid(wid_d, false);
        }

    }
}
