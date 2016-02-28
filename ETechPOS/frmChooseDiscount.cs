using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;
using ETech.fnc;

namespace ETech
{
    public partial class frmChooseDiscount : Form
    {
        private cls_productlist prodList;

        public frmChooseDiscount()
        {
            InitializeComponent();

            fncFilter.set_theme_color(this);

            this.prodList = new cls_productlist();
        }

        public void passProductList(cls_productlist val) { this.prodList = val; }

        private void f1()
        {
            frmTransactionDiscountList tDisc = new frmTransactionDiscountList();
            tDisc.setTotalAmt(this.prodList.get_totalamount_no_head_discount());
            tDisc.setDiscList(this.prodList.getTransDisc());
            tDisc.ShowDialog();
        }
        private void f2()
        {
            frmProductDiscountList pDisc = new frmProductDiscountList();
            pDisc.setProductList(this.prodList);
            pDisc.ShowDialog();
        }

        private void frmChooseDiscount_Load(object sender, EventArgs e)
        {
            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }
        private void frmChooseDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.F1)
            {
                f1();
            }
            else if (e.KeyCode == Keys.F2)
            {
                f2();
            }
            else { }
        }

        private void btnF1_Click(object sender, EventArgs e)
        {
            f1();
        }
        private void btnF2_Click(object sender, EventArgs e)
        {
            f2();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
