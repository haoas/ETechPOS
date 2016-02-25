using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace ETech.cls
{
    public class cls_memberdiscount
    {
        private decimal amt_from;
        private decimal amt_to;
        private decimal percent;
        private bool ismultiple;
        private DateTime datefrom;
        private DateTime dateto;

        public decimal getAmtFrom() { return this.amt_from; }
        public decimal getAmtTo() { return this.amt_to; }
        public decimal getPercent() { return this.percent; }
        public bool getIsMultiple() { return this.ismultiple; }

        public cls_memberdiscount()
        {
            this.amt_from = 0;
            this.amt_to = 0;
            this.percent = 0;
            this.ismultiple = false;
            this.datefrom = new DateTime(0001, 1, 1, 0, 0, 0);
            this.dateto = new DateTime(3000, 1, 1, 0, 0, 0);
        }

        public cls_memberdiscount(decimal amt_from_d, decimal amt_to_d, decimal percent_d, bool ismultiple_d,
            DateTime date_from_d, DateTime date_to_d)
        {
            this.amt_from = amt_from_d;
            this.amt_to = amt_to_d;
            this.percent = percent_d;
            this.ismultiple = ismultiple_d;
            this.datefrom = date_from_d;
            this.dateto = date_to_d;
        }

        public decimal compute_for_discount(decimal totalamt)
        {
            if (totalamt < 0)
                totalamt *= -1;
            if (this.percent == 0) return 0;
            if (totalamt <= 0) return 0;

            if (this.amt_from > 0 && totalamt < amt_from) return 0;
            if (this.amt_to > 0 && totalamt > amt_to) return 0;

            if (this.datefrom > DateTime.Now.Date) return 0;
            if (this.dateto < DateTime.Now.Date) return 0;

            return this.percent / 100;
        }


    }
}
