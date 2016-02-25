using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETech.cls
{
    class cls_qtypromo
    {
        private decimal quantity;
        private decimal price;

        public cls_qtypromo()
        {
            this.quantity = 0;
            this.price = 0;
        }

        public cls_qtypromo(decimal quantity_d, decimal price_d)
        {
            this.set_qtypromo(quantity_d, price_d);
        }

        public void set_qtypromo(decimal quantity_d, decimal price_d)
        {
            this.quantity = quantity_d;
            this.price = price_d;
        }

        public decimal get_price()
        {
            return this.price;
        }
        public decimal get_qty()
        {
            return this.quantity;
        }

    }
}
