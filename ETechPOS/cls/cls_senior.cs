using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETech.cls
{
    public class cls_senior
    {
        private string idnumber;
        private string fullname;

        public cls_senior()
        {
            this.idnumber = "";
            this.fullname = "";
        }

        public void set_senior(string idnumber_d, string fullname_d)
        {
            this.idnumber = idnumber_d;
            this.fullname = fullname_d;
        }

        public string get_idnumber()
        {
            return this.idnumber;
        }

        public string get_fullname()
        {
            return this.fullname;
        }



    }
}
