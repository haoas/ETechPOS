using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ETech.cls
{
    
    public class cls_CustomPayments
    {
        private string paymentwid;
        private string name;
        //private decimal amount;
        private string field1;
        private string field2;
        private string field3;
        private string field4;
        private string field5;
        private string field6;
        
        public cls_CustomPayments()
        {
            this.paymentwid = "";
            this.name = "";
            this.field1 = "";
            this.field2 = "";
            this.field3 = "";
            this.field4 = "";
            this.field5 = "";
            this.field6 = "";
        }

        public void set_custompayments_by_DataRow(DataRow DR)
        {
            this.paymentwid = DR["Payment Id"].ToString();
            this.name = DR["Payment Name"].ToString();
            this.field1 = DR["Field 1"].ToString();
            this.field2 = DR["Field 2"].ToString();
            this.field3 = DR["Field 3"].ToString();
            this.field4 = DR["Field 4"].ToString();
            this.field5 = DR["Field 5"].ToString();
            this.field6 = DR["Field 6"].ToString();
        }

        public string get_paymentwid()
        {
            return this.paymentwid;
        }
        public string get_name()
        {
            return this.name;
        }
        public string get_field1()
        {
            return this.field1;
        }
        public string get_field2()
        {
            return this.field2;
        }
        public string get_field3()
        {
            return this.field3;
        }
        public string get_field4()
        {
            return this.field4;
        }
        public string get_field5()
        {
            return this.field5;
        }
        public string get_field6()
        {
            return this.field6;
        }
    }
    
}
