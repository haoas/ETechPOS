using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using ETech.cls;
namespace ETech.cls
{
    public class cls_POSTransDiscount
    {

        private List<cls_memberdiscount> memberrates;
        private decimal memberDisc;
        private DataTable dt;

        private decimal memberDiscount_value;
        private decimal adjustments_value;
        private decimal posPromo_value;
        private decimal customDiscount_value;

        private int customdiscounttype = cls_globalvariables.dchead_customdiscounttype;
        private int adjusttype = cls_globalvariables.dchead_adjusttype;
        private int discounttype = cls_globalvariables.dchead_discounttype;
        private int membertype = cls_globalvariables.dchead_membertype;
        private int pospromotype = cls_globalvariables.dchead_pospromotype;

        public cls_POSTransDiscount()
        {
            this.memberrates = new List<cls_memberdiscount>();
            this.memberDisc = 0;
            this.dt = new DataTable();

            this.memberDiscount_value = 0;
            this.adjustments_value = 0;
            this.posPromo_value = 0;
            this.customDiscount_value = 0;
        }

        public DataTable getHierarchyList() { return this.dt; }
        public decimal getMemberDiscountValue() { return this.memberDiscount_value; }
        public decimal getAdjustDiscountValue() { return this.adjustments_value; }
        public decimal getPosPromoDiscountValue() { return this.posPromo_value; }
        public decimal getCustomDiscountValue() { return this.customDiscount_value; }

        public void setMemberDiscountValue(decimal val) { this.memberDiscount_value = val; }
        public void setAdjustDiscountValue(decimal val) { this.adjustments_value = val; }
        public void setPosPromoDiscountValue(decimal val) { this.posPromo_value = val; }
        public void setMemberRates(List<cls_memberdiscount> val) { this.memberrates = val; }
        public void setCustomDiscountValue(decimal val) { this.customDiscount_value = val; }

        public void setDiscountAmounts(int type, decimal amount)
        {
            if(type == this.discounttype || type == this.adjusttype){
                setAdjustDiscountValue(getAdjustDiscountValue() + (amount * -1));
            }
            else if(type == this.membertype){
                setMemberDiscountValue(getMemberDiscountValue() + (amount * -1));
            }
            else if(type == this.pospromotype){
                setPosPromoDiscountValue(getPosPromoDiscountValue() + (amount * -1));
            }
            else if (type == customdiscounttype)
            {
                setCustomDiscountValue(getCustomDiscountValue() + (amount * -1));
            }
            else{}
        }

        public void activateDiscount(int type, decimal value, bool isMultiple)
        {
            foreach (DataRow dr in this.dt.Rows)
            {
                if (type == Convert.ToInt32(dr["type"]) && !Convert.ToBoolean(dr["status"]))
                {
                    dr["status"] = true;
                    dr["value"] = value;
                    dr["ismultiple"] = isMultiple;
                    break;
                }
            }
        }

        public void activateDiscount(int wid, bool isMultiple)
        {
            foreach (DataRow dr in this.dt.Rows)
            {
                if (wid == Convert.ToInt32(dr["wid"]) && !Convert.ToBoolean(dr["status"]))
                {
                    dr["status"] = true;
                    dr["ismultiple"] = isMultiple;
                    break;
                }
                else if (wid == Convert.ToInt32(dr["wid"]) && Convert.ToBoolean(dr["status"]))
                {
                    if (MessageBox.Show("This discount is already being used. Do you want to remove the discount?", "Confirm Box",
                                MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        dr["status"] = false;
                        break;
                    }
                }
                else{}
            }
        }

        public int search_discount_row(int type, bool isActive)
        {
            int row_number = 0;
            int temp_row_number = -1;

            foreach (DataRow dr in this.dt.Rows)
            {
                if (type == Convert.ToInt32(dr["type"]) && Convert.ToBoolean(dr["status"]) == isActive)
                {
                    temp_row_number = row_number;
                }
                row_number++;
            }
            return temp_row_number;
        }

        public void add_new_default_discount(int type, int basis, decimal value, bool ismultiple, bool status, int position)
        {
            DataRow dr = this.dt.NewRow();
            dr["wid"] = 0;
            dr["type"] = type;
            dr["basis"] = basis;
            dr["value"] = value;
            dr["ismultiple"] = ismultiple;
            dr["status"] = status;
            this.dt.Rows.InsertAt(dr, position);

            foreach (DataRow datarow in this.dt.Rows)
            {
                int basis_temp = Convert.ToInt32(datarow["basis"]);
                if (basis_temp >= position)
                {
                    datarow["basis"] = basis_temp + 1;
                }
            }
        }

        public void appendDiscount(int type, decimal value, bool isMultiple)
        {
            for (int i = this.dt.Rows.Count - 1; i >= 0; i--)
            {
                if(Convert.ToBoolean(this.dt.Rows[i]["status"])){
                    add_new_default_discount(type, i, value, isMultiple, true, i + 1);
                    return;
                }
            }
            add_new_default_discount(type, -1, value, isMultiple, true, 0);
        }

        public void setPosPromo(decimal val, bool ismultiple)
        {

            int discRow = search_discount_row(this.pospromotype, false);

            //if pos promo active, add another promo row
            if (discRow == -1)
            {
                //get position of last active 
                int rownum = search_discount_row(this.pospromotype, true);
                add_new_default_discount(this.pospromotype, rownum, val, ismultiple, true, rownum + 1);
            }
            else
            {
                activateDiscount(this.pospromotype, val, ismultiple); 
            }
        }
        
        public void setMemberDisc(decimal totalAmt) {
            if(this.memberrates.Count == 0){
                return;
            }

            decimal default_discount = 0;
            decimal memberDiscount = 0;
            foreach (cls_memberdiscount i in this.memberrates)
            {
                if (i.getAmtTo() == 0 && i.getAmtFrom() == 0)
                {
                    default_discount = i.getPercent();
                }
                if (totalAmt <= i.getAmtTo() && totalAmt >= i.getAmtFrom())
                {
                    memberDiscount = i.getPercent();
                }
            }
            if(memberDiscount == 0){
                memberDiscount = default_discount;
            }
            this.memberDisc = memberDiscount / 100;

            if(this.memberDisc != 0){
                activateDiscount(this.membertype, this.memberDisc, true);
            }
        }

        public void getHierarchy()
        {
            DataTable discs = mySQLFunc.getdb(@"select T.wid, T.`type`, T.particular, H.position, H.basis, T.`value` 
                                            from discounttype as T, discounthierarchy as H
                                            where `headdetail` = 0 and `status` = 1 
                                                and branchid = " + cls_globalvariables.branchid_v.ToString() + @"
	                                            and H.discountid = T.wid
                                            order by H.`position`");
            this.dt = new DataTable();
            this.dt.Columns.Add("wid", typeof(int));
            this.dt.Columns.Add("type", typeof(int));
            this.dt.Columns.Add("basis", typeof(int));
            this.dt.Columns.Add("value", typeof(decimal));
            this.dt.Columns.Add("ismultiple", typeof(bool));
            this.dt.Columns.Add("status", typeof(bool));

            foreach (DataRow dr in discs.Rows)
            {
                this.dt.Rows.Add(Convert.ToInt32(dr["wid"]), Convert.ToInt32(dr["type"]), Convert.ToInt32(dr["basis"]), Convert.ToDecimal(dr["value"]), false, false);
            }
        }

        //can be used to get last value in amts list
        public decimal getBasis(List<decimal> amts, int basis)
        {
            for (int i = basis; i >= 0; i--)
            {
                if (amts[i] != 0)
                {
                    return amts[i];
                }
            }
            return 0;
        }
        public decimal getTotalWithDiscount(decimal totalAmt)
        {
            this.setMemberDisc(totalAmt);

            List<decimal> amts = new List<decimal>();

            this.adjustments_value = 0;
            this.memberDiscount_value = 0;
            this.posPromo_value = 0;
            this.customDiscount_value = 0;

            foreach (DataRow dr in this.dt.Rows)
            {
                int type = Convert.ToInt32(dr["type"]);
                int basisValue = Convert.ToInt32(dr["basis"]);
                decimal value = Convert.ToDecimal(dr["value"]);
                bool isMultiple = Convert.ToBoolean(dr["ismultiple"]);
                bool status = Convert.ToBoolean(dr["status"]);
                decimal lastAmtInArr = getBasis(amts, amts.Count - 1);
                decimal basisAmount = getBasis(amts, basisValue);
                decimal newValue = 0;

                if(!status){
                    amts.Add(0);
                    continue;
                }

                if (isMultiple)
                {
                    if (basisAmount == 0)
                    {
                        if (lastAmtInArr == 0)
                        {
                            newValue = totalAmt - (totalAmt * value);
                        }
                        else
                        {
                            newValue = lastAmtInArr - (totalAmt * value);
                        }
                        this.setDiscountAmounts(type, -1 * totalAmt * value);
                    }
                    else
                    {
                        newValue = lastAmtInArr - (basisAmount * value);
                        this.setDiscountAmounts(type, -1 * basisAmount * value);
                    }
                }
                else
                {
                    if (lastAmtInArr == 0)
                    {
                        newValue = totalAmt + value;
                    }
                    else
                    {
                        newValue = lastAmtInArr + value;
                    }
                    this.setDiscountAmounts(type, value);
                }
                
                amts.Add(newValue);
                /*MessageBox.Show("particular: " + dr["particular"] + " \n" +
                                "type: " + dr["type"] + " \n" +
                                "basis: " + dr["basis"] + " \n" +
                                "value: " + dr["value"] + " \n" +
                                "ismultiple: " + dr["ismultiple"] + " \n" +
                                "status: " + dr["status"] + " \n" );*/
            }
            return (getBasis(amts, amts.Count - 1) == 0) ? totalAmt : getBasis(amts, amts.Count - 1);
        }
    }
    
}
