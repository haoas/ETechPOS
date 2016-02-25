using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETech.cls
{
    public class cls_nonvat
    {
        private string idnumber;
        private string fullname;
        private string address;
        private string telno;

        public cls_nonvat()
        {
            this.idnumber = "";
            this.fullname = "";
            this.address = "";
            this.telno = "";
        }

        public void set_nonvat(string idnumber_d, string fullname_d,
            string address_d, string telno_d)
        {
            this.idnumber = idnumber_d;
            this.fullname = fullname_d;
            this.address = address_d;
            this.telno = telno_d;
        }

        public string get_idnumber() { return this.idnumber; }
        public string get_fullname() { return this.fullname; }
        public string get_address() { return this.address; }
        public string get_telno() { return this.telno; }


    }
}
