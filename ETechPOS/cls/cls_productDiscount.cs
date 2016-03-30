using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using ETech.Helpers;
namespace ETech.cls
{
    public class cls_productDiscount
    {

        private DataTable dt;
        private decimal adjustments;
        private decimal seniordisc_value;
        private decimal nonvatdisc_value;
        private decimal customDisc_value;

        private int dcdetail_customdiscounttype = cls_globalvariables.dcdetail_customdiscounttype;
        private int dcdetail_adjusttype = cls_globalvariables.dcdetail_adjusttype;
        private int dcdetail_discounttype = cls_globalvariables.dcdetail_discounttype;
        private int dcdetail_senior = cls_globalvariables.dcdetail_senior;
        private int dcdetail_nonvat = cls_globalvariables.dcdetail_nonvat;

        public cls_productDiscount()
        {
            this.dt = new DataTable();
            this.adjustments = 0;
            this.seniordisc_value = 0;
            this.nonvatdisc_value = 0;
            this.customDisc_value = 0;
        }

        public DataTable getHierarchyList() { return this.dt; }
        public decimal getAdjustments() { return this.adjustments; }
        public decimal getDiscounts() { return this.seniordisc_value + this.nonvatdisc_value + this.customDisc_value; }
        public decimal getSeniorDisc() { return this.seniordisc_value; }
        public decimal getNonVatDisc() { return this.nonvatdisc_value; }
        public decimal getCustomDisc() { return this.customDisc_value; }

        public void setAdjustments(decimal val) { this.adjustments = val; }
        public void setSeniorDisc(decimal val) { this.seniordisc_value = val; }
        public void setNonVatDisc(decimal val) { this.nonvatdisc_value = val; }
        public void setCustomDisc(decimal val) { this.customDisc_value = val; }

        public void setDiscountAmounts(int type, decimal amount)
        {
            if (type == this.dcdetail_adjusttype || type == this.dcdetail_discounttype)
            {
                setAdjustments(getAdjustments() + (amount * -1));
            }
            else if (type == this.dcdetail_senior)
            {
                //get amount
                decimal a = (amount * -1) / (1 - ((1 - cls_globalvariables.senior) / (1 + cls_globalvariables.vat)));
                //recompute vat
                decimal v = a - (a / (1 + cls_globalvariables.vat));
                //recompute senior
                decimal s = (amount * -1) - v;
                //add nonvat from senior to total nonvat disc of the product
                setNonVatDisc(getNonVatDisc() + v);
                setSeniorDisc(s);
            }
            else if (type == this.dcdetail_nonvat)
            {
                setNonVatDisc(amount * -1);
            }
            else if (type == dcdetail_customdiscounttype)
            {
                setCustomDisc(getCustomDisc() + (amount * -1));
            }
            else { }
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

        public void activateDiscount(long SyncId, bool isMultiple)
        {
            foreach (DataRow dr in this.dt.Rows)
            {
                if (SyncId == Convert.ToInt32(dr["SyncId"]) && !Convert.ToBoolean(dr["status"]))
                {
                    dr["status"] = true;
                    dr["ismultiple"] = isMultiple;
                    break;
                }
                else if (SyncId == Convert.ToInt32(dr["SyncId"]) && Convert.ToBoolean(dr["status"]))
                {
                    if (DialogHelper.ShowDialog("This discount is already being used. Do you want to remove the discount?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        dr["status"] = false;
                        break;
                    }

                }
                else { }
            }
        }

        public void deactivateDiscount(int type)
        {
            foreach (DataRow dr in this.dt.Rows)
            {
                if (type == Convert.ToInt32(dr["type"]) && Convert.ToBoolean(dr["status"]))
                {
                    dr["status"] = false;
                    break;
                }
            }
        }

        public void add_new_default_discount(int type, int basis, decimal value, bool ismultiple, bool status, int position)
        {
            DataRow dr = this.dt.NewRow();
            dr["SyncId"] = 0;
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
                if (Convert.ToBoolean(this.dt.Rows[i]["status"]))
                {
                    add_new_default_discount(type, i, value, isMultiple, true, i + 1);
                    return;
                }
            }
            add_new_default_discount(type, -1, value, isMultiple, true, 0);
        }

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
            List<decimal> amts = new List<decimal>();
            bool isSenior = false;
            this.adjustments = 0;
            this.seniordisc_value = 0;
            this.nonvatdisc_value = 0;
            this.customDisc_value = 0;

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

                if (!status)
                {
                    amts.Add(0);
                    continue;
                }

                if (type == 2)
                {
                    isSenior = true;
                }
                if (isSenior && type == dcdetail_nonvat)
                {
                    amts.Add(0);
                    continue;
                }
                if (isMultiple)
                {
                    if (basisAmount == 0)
                    {
                        if (lastAmtInArr == 0)
                        {
                            newValue = totalAmt - (totalAmt * (1 - value));
                        }
                        else
                        {
                            newValue = lastAmtInArr - (totalAmt * (1 - value));
                        }
                        setDiscountAmounts(type, -1 * totalAmt * (1 - value));
                    }
                    else
                    {
                        newValue = lastAmtInArr - (basisAmount * (1 - value));
                        setDiscountAmounts(type, -1 * basisAmount * (1 - value));
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
                    setDiscountAmounts(type, value);
                }
                amts.Add(newValue);
            }
            return (getBasis(amts, amts.Count - 1) == 0) ? totalAmt : getBasis(amts, amts.Count - 1);
        }
    }
}
