using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace ETech.cls
{
    public class cls_discountlist
    {
        private List<cls_discount> disclist;
        private int headdetail;
        private cls_member member_obj;

        private int dcdetail_customdiscounttype = cls_globalvariables.dcdetail_customdiscounttype;
        private int dcdetail_adjusttype = cls_globalvariables.dcdetail_adjusttype;
        private int dcdetail_discounttype = cls_globalvariables.dcdetail_discounttype;
        private int dcdetail_senior = cls_globalvariables.dcdetail_senior;
        private int dcdetail_nonvat = cls_globalvariables.dcdetail_nonvat;
        private int dcdetail_promoqty = cls_globalvariables.dcdetail_promoqty;

        private int dchead_customdiscounttype = cls_globalvariables.dchead_customdiscounttype;
        private int dchead_adjusttype = cls_globalvariables.dchead_adjusttype;
        private int dchead_discounttype = cls_globalvariables.dchead_discounttype;
        private int dchead_membertype = cls_globalvariables.dchead_membertype;
        private int dchead_pospromotype = cls_globalvariables.dchead_pospromotype;

        public int get_headdetail() { return this.headdetail; }


        public cls_discountlist(int headdetail_val)
        {
            this.disclist = new List<cls_discount>();
            this.headdetail = headdetail_val;
            this.member_obj = new cls_member();
            this.getHierarchy();
        }

        public void setMember(cls_member val, decimal dc, decimal amount, bool isHistory)
        {
            this.member_obj = val;
            this.activateDiscount(this.dchead_membertype, dc, amount, true, true);
        }
        public void setHeadDetail(int val) { this.headdetail = val; }

        public List<cls_discount> get_discount_list() { return this.disclist; }


        public cls_discount get_discount_using_wid(int wid)
        {
            foreach (cls_discount disc in this.disclist)
            {
                if (disc.get_wid() == wid)
                {
                    return disc;
                }
            }
            return new cls_discount();
        }

        public cls_discount get_discount(int type)
        {
            foreach (cls_discount disc in this.disclist)
            {
                if (disc.get_type() == type && disc.get_status() == true)
                {
                    return disc;
                }
            }
            return new cls_discount();
        }

        public decimal get_all_discount_amount_of_type(int type)
        {
            decimal total_discount = 0;
            foreach (cls_discount disc in this.disclist)
            {
                if (disc.get_type() == type && disc.get_status() == true)
                {
                    total_discount += disc.get_discounted_amount();
                }
            }
            return total_discount;
        }

        public void getHierarchy()
        {
            if (cls_globalvariables.POSmode == 0)
                return;

            DataTable discs = mySQLFunc.getdb(@"select T.wid, T.`type`, T.particular, H.position, H.basis, T.`value` 
                                            from discounttype as T, discounthierarchy as H
                                            where `headdetail` = " + this.headdetail.ToString() + @" and `status` = 1 
                                                and branchid = " + cls_globalvariables.branchid_v.ToString() + @"
	                                            and H.discountid = T.wid
                                            order by H.`position`");
            if (discs == null)
                return;

            if (discs.Rows.Count <= 0)
                return;

            this.disclist = new List<cls_discount>();

            foreach (DataRow dr in discs.Rows)
            {
                cls_discount disc = new cls_discount(dr["particular"].ToString(),
                                                     Convert.ToInt32(dr["wid"]),
                                                     Convert.ToInt32(dr["type"]),
                                                     Convert.ToInt32(dr["basis"]),
                                                     1 - Convert.ToDecimal(dr["value"]),
                                                     false,
                                                     false);
                this.disclist.Add(disc);
            }
        }

        private void set_discount_name(cls_discount disc, int type)
        {
            if (type == dchead_adjusttype || type == dcdetail_adjusttype)
            {
                disc.set_name("Adjust");
            }
            else if (type == dchead_discounttype || type == dcdetail_discounttype)
            {
                disc.set_name("Regular Discount");
            }
            else { }
        }

        private int search_discount_row(int type, bool isActive)
        {
            int row_number = 0;
            int temp_row_number = -1;

            foreach (cls_discount disc in this.disclist)
            {
                if (type == disc.get_type() && disc.get_status() == isActive)
                {
                    temp_row_number = row_number;
                }
                row_number++;
            }
            return temp_row_number;
        }

        public int getDynamicWID()
        {
            int wid = 0;
            foreach (cls_discount disc in this.disclist)
            {
                if (disc.get_wid() > wid)
                {
                    wid = disc.get_wid();
                }
            }
            return wid + 1000;
        }

        public void setPosPromo(decimal val, bool ismultiple)
        {
            setPosPromo(val, ismultiple, 0, false);
        }

        public void setPosPromo(decimal val, bool ismultiple, decimal amount, bool isHistory)
        {
            int discRow = search_discount_row(this.dchead_pospromotype, false);
            //if pos promo active, add another promo row
            if (discRow == -1)
            {
                //get position of last active 
                int rownum = search_discount_row(this.dchead_pospromotype, true);
                add_new_discount(getDynamicWID(), this.dchead_pospromotype, rownum, val, amount, ismultiple, isHistory, true, rownum + 1);
            }
            else
            {
                activateDiscount(this.dchead_pospromotype, val, amount, ismultiple, isHistory);
            }
        }

        public void add_new_discount(int wid, int type, int basis, decimal value, bool ismultiple, bool status, int position)
        {
            add_new_discount(wid, type, basis, value, 0, ismultiple, false, status, position);
        }

        public void add_new_discount(int wid, int type, int basis, decimal value, decimal amount, bool ismultiple, bool ishistory, bool status, int position)
        {
            cls_discount disc = new cls_discount(wid, type, basis, value, ismultiple, status);
            set_discount_name(disc, type);
            if (ishistory)
                disc.set_discounted_amount(amount);
            this.disclist.Insert(position, disc);

            foreach (cls_discount d in this.disclist)
            {
                if (d.get_basis() >= position)
                {
                    d.set_basis(d.get_basis() + 1);
                }
            }
        }

        public void appendDiscount(int type, decimal value, bool isMultiple)
        {
            appendDiscount(type, value, 0, isMultiple, false);
        }
        public void appendDiscount(int type, decimal value, decimal amount, bool isMultiple, bool ishistory)
        {
            for (int i = this.disclist.Count - 1; i >= 0; i--)
            {
                if (this.disclist[i].get_status())
                {
                    add_new_discount(getDynamicWID(), type, i, value, amount, isMultiple, ishistory, true, i + 1);
                    return;
                }
            }
            //if no discount is activated yet, make basis -1.
            add_new_discount(getDynamicWID(), type, -1, value, amount, isMultiple, ishistory, true, 0);
        }

        public void activateDiscount(int type, decimal value, bool isMultiple)
        {
            activateDiscount(type, value, 0, isMultiple, false, 0);
        }
        public void activateDiscount(int type, decimal value, bool isMultiple, bool isHistory)
        {
            activateDiscount(type, value, 0, isMultiple, isHistory, 0);
        }
        public void activateDiscount(int type, decimal value, bool isMultiple, int discountWid)
        {
            activateDiscount(type, value, 0, isMultiple, (discountWid != 0), discountWid);
        }
        public void activateDiscount(int type, decimal value, decimal amount, bool isMultiple, int discountWid)
        {
            activateDiscount(type, value, amount, isMultiple, (discountWid != 0), discountWid);
        }
        public void activateDiscount(int type, decimal value, decimal amount, bool isMultiple, bool isHistory)
        {
            activateDiscount(type, value, amount, isMultiple, isHistory, 0);
        }
        private void activateDiscount(int type, decimal value, decimal amount, bool isMultiple, bool isHistory, int discountWid)
        {
            bool flag = false;
            if (!isHistory)
                discountWid = 0;
            if ((type == dcdetail_customdiscounttype && discountWid != 0) || type != dcdetail_customdiscounttype)
            {
                foreach (cls_discount disc in this.disclist)
                {
                    if (type == disc.get_type() && !disc.get_status())
                    {
                        if (type == dcdetail_customdiscounttype && discountWid != disc.get_wid())
                            continue;
                        if (type == cls_globalvariables.dcdetail_promoqty && isMultiple)
                            disc.set_name("Item Discount");

                        set_discount_name(disc, type);
                        disc.set_status(true);
                        disc.set_ismultiple(isMultiple);
                        disc.set_value(value);
                        if (amount == 0 && !isMultiple && type == dcdetail_promoqty)
                            amount = value * -1;
                        if (isHistory)
                            disc.set_discounted_amount(amount);
                        flag = true;
                        break;
                    }
                }
            }
            if (!flag && type == dcdetail_customdiscounttype)
            {
                cls_discount discountTemp = new cls_discount();
                discountTemp.set_status(true);
                discountTemp.set_ismultiple(isMultiple);
                discountTemp.set_value(value);
                discountTemp.set_name("Custom Discount");
                if (isHistory)
                    discountTemp.set_discounted_amount(amount);
                this.disclist.Add(discountTemp);
            }
        }

        public void activateDiscount(int wid, bool isMultiple)
        {
            foreach (cls_discount disc in this.disclist)
            {
                if (wid == disc.get_wid() && !disc.get_status())
                {
                    set_discount_name(disc, disc.get_type());
                    disc.set_status(true);
                    disc.set_ismultiple(isMultiple);
                    break;
                }
                else if (wid == disc.get_wid() && disc.get_status())
                {
                    if (MessageBox.Show("This discount is already being used. Do you want to remove the discount?", "Confirm Box",
                                MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        disc.set_status(false);
                        break;
                    }

                }
                else { }
            }
        }

        public void activateDiscount_using_wid(int wid, decimal value, bool isMultiple)
        {
            cls_discount disc = get_discount_using_wid(wid);

            if (wid == disc.get_wid() && !disc.get_status())
            {
                disc.set_status(true);
                disc.set_value(value);
                disc.set_ismultiple(isMultiple);
            }
            else if (wid == disc.get_wid() && disc.get_status())
            {
                if (MessageBox.Show("This discount is already being used. Do you want to remove the discount?", "Confirm Box",
                            MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    disc.set_status(false);
                }

            }
            else { }
        }


        public void deactivateDiscount(int type)
        {
            foreach (cls_discount disc in this.disclist)
            {
                if (type == disc.get_type() && disc.get_status())
                {
                    disc.set_status(false);
                    break;
                }
            }
        }

        private decimal getBasis(List<decimal> amts, int basis)
        {
            for (int i = basis; i >= 0; i--)
            {
                //if basis is found, check if there are adjustments after the basis.
                if (amts[i] != 0 && i == basis)
                {
                    decimal temp_amt = amts[i];
                    for (int x = basis + 1; x < amts.Count; x++)
                    {
                        if (((this.headdetail == 0 && this.disclist[i].get_type() == dchead_pospromotype && this.disclist[x].get_type() == dchead_pospromotype) ||
                             this.disclist[x].get_type() == dchead_adjusttype || this.disclist[x].get_type() == dchead_discounttype ||
                             this.disclist[x].get_type() == dcdetail_adjusttype || this.disclist[x].get_type() == dcdetail_discounttype)
                                && this.disclist[x].get_status() == true)
                        {
                            temp_amt = amts[x];
                        }
                        else if (this.disclist[x].get_type() != dchead_adjusttype && this.disclist[x].get_type() != dchead_discounttype &&
                             this.disclist[x].get_type() != dcdetail_adjusttype && this.disclist[x].get_type() != dcdetail_discounttype)
                        {
                            break;
                        }
                    }
                    return temp_amt;
                }
                else if (amts[i] != 0 && i != basis)
                {
                    return amts[i];
                }
                else { }
            }
            return 0;
        }

        private void add_to_discount_amount(int type, decimal amount)
        {
            foreach (cls_discount disc in this.disclist)
            {
                if (type == disc.get_type())
                {
                    //special case for pospromo
                    if (type == dchead_pospromotype && disc.get_discounted_amount() != 0)
                    {
                        continue;
                    }
                    else
                    {
                        disc.add_discounted_amount(amount);
                    }

                }
            }
        }

        private void add_to_discount_amount_using_wid(int wid, decimal amount)
        {
            foreach (cls_discount disc in this.disclist)
            {
                if (wid == disc.get_wid())
                {
                    disc.add_discounted_amount(amount);
                }
            }
        }

        private void set_discount_amount(int type, decimal amount)
        {
            foreach (cls_discount disc in this.disclist)
            {
                if (type == disc.get_type())
                {
                    disc.set_discounted_amount(amount);
                }
            }
        }

        public void disable_all_discounts()
        {
            foreach (cls_discount disc in this.disclist)
            {
                disc.set_status(false);
            }
        }

        private void clear_discount_amounts()
        {
            foreach (cls_discount disc in this.disclist)
            {
                disc.set_discounted_amount(0);
            }
        }

        /*private void setDiscountAmounts(int wid, decimal amount)
        {
            int type = 0;
            foreach(cls_discount disc in this.disclist){
                if(wid == disc.get_wid()){
                    type = disc.get_type();
                    break;
                }
            }
            if (this.headdetail == 1)
            {
                if (type == this.dcdetail_adjusttype || type == this.dcdetail_discounttype)
                {
                    this.add_to_discount_amount_using_wid(wid, amount);
                }
                else if (type == this.dcdetail_nonvat || type == this.dcdetail_promoqty)
                {
                    this.set_discount_amount(type, amount);
                }
                else if (type == this.dcdetail_senior)
                {
                    this.set_discount_amount(this.dcdetail_senior, amount);
                }
                else if(type == dcdetail_customdiscounttype){
                    this.add_to_discount_amount_using_wid(wid, amount);
                }
                else { }
            }
            else
            {
                if (type == dchead_customdiscounttype || type == dchead_pospromotype || 
                    type == dchead_adjusttype || type == dchead_discounttype)
                {
                    this.add_to_discount_amount_using_wid(wid, amount);
                }
                else
                {
                    this.add_to_discount_amount(type, amount);
                }
            }
        }*/

        public decimal get_last_amt_before_discount(int filter, decimal totalAmt) { return getValueFromDiscount(totalAmt, filter, 2); }
        public decimal get_basis_before_discount(int filter, decimal totalAmt) { return getValueFromDiscount(totalAmt, filter, 3); }
        public decimal get_amount_after_discount(decimal totalAmt) { return getValueFromDiscount(totalAmt, 0, 1); }
        public decimal get_discounts_percentage(decimal totalAmt) { return getValueFromDiscount(totalAmt, 0, 4); }
        public void refresh_discountlist(decimal totalAmt) { getValueFromDiscount(totalAmt, 0, 1); }

        //choice: 1 = amount; 2 = last amount; 3 = basis amount; 4 get total % of discounts
        private decimal getValueFromDiscount(decimal totalAmt, int filter, int choice)
        {
            //has no discount value if no total Amount
            if (totalAmt == 0)
                return 0;
            //----------------------------------------

            List<decimal> amts = new List<decimal>();
            int type = 0;
            int wid = 0;
            int basisValue = 0;
            decimal value = 0;
            bool isMultiple = false;
            bool status = false;
            decimal lastAmtInArr = 0;
            decimal basisAmount = 0;
            decimal discAmount = 0;
            decimal discPercentage = 1;
            if (choice == 1 || choice == 4) { this.clear_discount_amounts(); }

            foreach (cls_discount disc in this.disclist)
            {


                type = disc.get_type();
                wid = disc.get_wid();
                basisValue = disc.get_basis();
                value = disc.get_value();
                isMultiple = disc.get_ismultiple();
                status = disc.get_status();
                lastAmtInArr = this.getBasis(amts, amts.Count - 1);
                basisAmount = this.getBasis(amts, basisValue);

                decimal newValue = 0;

                if ((filter == type || filter == wid) && (choice == 2 || choice == 3))
                    break;

                if (!status)
                {
                    amts.Add(0);
                    continue;
                }

                if (headdetail == 1)
                {
                    //when senior already, skip nonvat.
                    /*if (type == 2) { isSenior = true; }
                    if (isSenior && type == dcdetail_nonvat)
                    {
                        amts.Add(0);
                        continue;
                    }*/
                }
                else
                {
                    //compute for member discount
                    if (choice == 1 || choice == 4)
                    {
                        decimal temp_value = ((lastAmtInArr == 0) ? totalAmt : lastAmtInArr);
                        if (disc.get_type() == this.dchead_membertype)
                        {
                            value = 1 - (this.member_obj.get_member_discount_amount(temp_value) / temp_value);
                            disc.set_value(value);
                        }
                    }
                }
                if (isMultiple)
                {
                    if (basisAmount == 0)
                    {
                        if (lastAmtInArr == 0)
                        {
                            newValue = totalAmt - (totalAmt * (1 - value));
                            discPercentage *= newValue / totalAmt;
                        }
                        else
                        {
                            newValue = lastAmtInArr - (totalAmt * (1 - value));
                            discPercentage *= newValue / lastAmtInArr;
                        }
                        discAmount = totalAmt * (1 - value);
                    }
                    else
                    {
                        newValue = lastAmtInArr - (basisAmount * (1 - value));
                        discPercentage *= newValue / lastAmtInArr;
                        discAmount = basisAmount * (1 - value);
                    }
                }
                else
                {
                    if (lastAmtInArr == 0)
                    {
                        newValue = totalAmt + value;
                        discPercentage *= 1 - ((totalAmt == 0) ? 1 : (value * -1 / totalAmt));
                    }
                    else
                    {
                        newValue = lastAmtInArr + value;
                        discPercentage *= 1 - (value * -1 / lastAmtInArr);
                    }
                    discAmount = -1 * value;

                }
                if (choice == 1 || choice == 4) { this.add_to_discount_amount_using_wid(wid, discAmount); }

                amts.Add(newValue);
            }

            if (amts.Count > 0)
            {
                lastAmtInArr = this.getBasis(amts, amts.Count - 1);
                basisAmount = this.getBasis(amts, basisValue);
            }
            if (choice == 3)
            {
                return (basisAmount == 0) ? totalAmt : basisAmount;
            }
            else if (choice == 4)
            {
                return 1 - discPercentage;
            }
            else
            {
                return (lastAmtInArr == 0) ? totalAmt : lastAmtInArr;
            }
        }
    }
}
