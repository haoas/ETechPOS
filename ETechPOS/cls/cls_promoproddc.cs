using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETech.cls
{
    class cls_promoproddc
    {
        private decimal oprice;
        private decimal discount;
        private decimal adjust;
        private DateTime datefrom;
        private DateTime dateto;
        private DateTime datebasis;

        public cls_promoproddc()
        {
            this.oprice = 0;
            this.discount = 0;
            this.adjust = 0;
            this.datefrom = new DateTime(0001, 1, 1, 0, 0, 0);
            this.dateto=  new DateTime(3000, 1, 1, 0, 0, 0);
        }

        public cls_promoproddc(decimal oprice_d, decimal discount_d, decimal adjust_d,
            DateTime datefrom_d, DateTime dateto_d, DateTime datebasis_d)
        {
            this.set_promoproddc(oprice_d, discount_d, adjust_d, datefrom_d, dateto_d, datebasis_d);
        }

        public void set_promoproddc(decimal oprice_d, decimal discount_d, decimal adjust_d,
             DateTime datefrom_d, DateTime dateto_d, DateTime datebasis_d)
        {
            this.oprice = oprice_d;
            this.discount = discount_d;
            this.adjust = adjust_d;
            this.datefrom = datefrom_d;
            this.dateto = dateto_d;
            this.datebasis = datebasis_d;
        }

        public decimal get_discountedprice()
        {
            if ((datebasis >= datefrom) &&
                (datebasis <= dateto))
            {
                if (this.discount > 0)
                    return this.oprice * (1 - this.discount / 100);
                else if (this.adjust != 0)
                    return this.oprice + this.adjust;
                else
                    return this.oprice;
            }
            else
                return this.oprice;
        }
    }
}
