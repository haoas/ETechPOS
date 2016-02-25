using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace ETech
{
    class POSMainGridview
    {
        private DataTable GridViewDT;
        private DataGridView POSGridView;

        public POSMainGridview(DataGridView dgv)
        {
            this.GridViewDT = new DataTable();
            this.GridViewDT.Columns.Add("row_num");
            this.GridViewDT.Columns.Add("productbarcode");
            this.GridViewDT.Columns.Add("description");
            this.GridViewDT.Columns.Add("qty");
            this.GridViewDT.Columns.Add("price");
            this.GridViewDT.Columns.Add("amount");
            this.POSGridView = dgv;
            this.POSGridView.AutoGenerateColumns = false;
            this.POSGridView.DataSource = this.GridViewDT;
        }

        public void add_new_product(DataTable dt)
        {
            bool isfound = false;
            for (int i = 0; i < GridViewDT.Rows.Count; i++)
            {
                if (dt.Rows[0]["productbarcode"].ToString() ==
                        GridViewDT.Rows[i]["productbarcode"].ToString())
                {
                    POSGridView.ClearSelection();
                    decimal qty = Convert.ToDecimal(GridViewDT.Rows[i]["qty"]);
                    qty++;
                    GridViewDT.Rows[i]["qty"] = qty;
                    decimal price = Convert.ToDecimal(GridViewDT.Rows[i]["price"]);
                    decimal amt = qty * price;
                    GridViewDT.Rows[i]["amount"] = amt;
                    POSGridView.Rows[i].Selected = true;
                    isfound = true;
                    break;
                }
            }
            if (!isfound)
            {
                POSGridView.ClearSelection();
                DataRow dr = this.GridViewDT.NewRow();
                dr["row_num"] = GridViewDT.Rows.Count+1;
                dr["productbarcode"] = dt.Rows[0]["productbarcode"].ToString();
                dr["description"] = dt.Rows[0]["description"].ToString();
                dr["price"] = dt.Rows[0]["price"];
                dr["qty"] = 1;
                decimal qty = Convert.ToDecimal(dr["qty"]);
                decimal price = Convert.ToDecimal(dr["price"]);
                decimal amt = qty * price;
                dr["amount"] = amt;
                this.GridViewDT.Rows.Add(dr);
                POSGridView.Rows[this.GridViewDT.Rows.IndexOf(dr)].Selected = true;
            }
        }



    }
}
