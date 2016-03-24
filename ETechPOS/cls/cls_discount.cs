using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ETech.cls
{
    public class cls_discount
    {
        private long syncid;
        private int type;
        private int basis;
        private decimal value;
        private bool ismultiple;
        private bool status;
        private decimal discounted_amount;
        private string discount_name;

        public cls_discount()
        {
            this.syncid = 0;
            this.type = 0;
            this.basis = 0;
            this.value = 0;
            this.ismultiple = false;
            this.status = false;
            this.discounted_amount = 0;
        }
        
        public cls_discount(long syncid_val, int type_val, int basis_val, decimal value_val, bool ismultiple_val, bool status_val){
            this.syncid = syncid_val;
            this.type = type_val;
            this.basis = basis_val;
            this.value = value_val;
            this.ismultiple = ismultiple_val;
            this.status = status_val;
            this.discounted_amount = 0;
        }
        public cls_discount(string discount_name_val, int wid_val, int type_val, int basis_val, decimal value_val, bool ismultiple_val, bool status_val)
        {
            this.syncid = wid_val;
            this.type = type_val;
            this.basis = basis_val;
            this.value = value_val;
            this.ismultiple = ismultiple_val;
            this.status = status_val;
            this.discounted_amount = 0;
            this.discount_name = discount_name_val;
        }

        public void set_wid(int val){ this.syncid = val; }
        public void set_type(int val){ this.type = val; }
        public void set_basis(int val){ this.basis = val; }
        public void set_value(decimal val){ this.value = val; }
        public void set_ismultiple(bool val){ this.ismultiple = val; }
        public void set_status(bool val){ this.status = val; }
        public void set_discounted_amount(decimal val) { this.discounted_amount = val; }
        public void add_discounted_amount(decimal val) { this.discounted_amount += val; }
        public void set_name(string val) { this.discount_name = val; }

        public long get_SyncId(){ return this.syncid; }
        public int get_type(){ return this.type; }
        public int get_basis(){ return this.basis; }
        public decimal get_value()
        {
            if (this.type == cls_globalvariables.dcdetail_nonvat && ismultiple)
                return this.value;
            else if(ismultiple)
                return Math.Round(this.value, 4, MidpointRounding.AwayFromZero);
            else
                return Math.Round(this.value, 2, MidpointRounding.AwayFromZero);
        }
        public bool get_ismultiple(){ return this.ismultiple; }
        public bool get_status(){ return this.status; }
        public decimal get_discounted_amount() { return (this.status) ? Math.Round(this.discounted_amount, 2, MidpointRounding.AwayFromZero) : 0; }
        public string get_name() { return this.discount_name; }
    }

}
