using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;
using ETech.ctrl;
using ETech.fnc;

namespace ETech
{
    public partial class frmSearchProductButton : Form
    {
        public cls_buttonGrid buttongridDepartment = new cls_buttonGrid();
        public cls_buttonGrid buttongridProduct = new cls_buttonGrid();
        public cls_productlist tempProductlist = new cls_productlist();
        private decimal amountBefore;
        private int lastaddedrownumber = -1;
        private cls_product lastaddedproduct;

        public frmSearchProductButton(cls_POSTransaction transaction, cls_product lastprod)
        {
            InitializeComponent();
            fncFilter.set_theme_color(this);
            amountBefore = transaction.get_productlist().get_totalamount();
            lastaddedproduct = lastprod;
        }

        private void frmSearchProductButton_Load(object sender, EventArgs e)
        {
            cls_globalfunc.formaddkbkpevent(this);

            buttongridDepartment.setTable("Department");
            buttongridDepartment.setColumnShown("name");
            buttongridDepartment.setCondition("WHERE `SyncId` IN( SELECT DISTINCT(departmentid) FROM product WHERE `show` = 1 AND `status` = 1 )");
            buttongridDepartment.setColumnCntPerPage(2);
            buttongridDepartment.setRowCntPerPage(5);
            buttongridDepartment.setCurrentPage(1);
            generateButtonsFromDataTable(gbox_dept, buttongridDepartment);

            buttongridProduct.setTable("Product");
            buttongridProduct.setColumnShown("product");
            buttongridProduct.setCondition("WHERE `show`=1 AND `status`=1 AND departmentid=1");
            buttongridProduct.setColumnCntPerPage(4);
            buttongridProduct.setRowCntPerPage(5);
            buttongridProduct.setCurrentPage(1);
            generateButtonsFromDataTable(gbox_products, buttongridProduct);

            //txtPage.Text = buttongridProduct.currentpage.ToString();
            //lblTotalPage.Text = "/" + buttongridProduct.getTotalPages();
        }

        public void generateButtonsFromDataTable(Control cntlParent, cls_buttonGrid buttongrid_d)
        {
            if (cntlParent == null)
                cntlParent = this;

            for (int i = cntlParent.Controls.Count - 1; i >= 0; i--)
            {
                if (cntlParent.Controls[i] is Button)
                    cntlParent.Controls[i].Dispose();
            }

            for (int I = 0; I < buttongrid_d.DT.Rows.Count; I++)
            {
                Button btn = new Button();
                btn.Parent = cntlParent;
                btn.Size = new Size(buttongrid_d.buttonwidth, buttongrid_d.buttonheight);
                btn.TextAlign = ContentAlignment.MiddleCenter;
                int btnX = buttongrid_d.initLeftSpacing + (buttongrid_d.buttonwidth + buttongrid_d.LeftSpacing) * (I % buttongrid_d.columnCntPerPage);
                int btnY = buttongrid_d.initTopSpacing + (buttongrid_d.buttonheight + buttongrid_d.RightSpacing) * (I / buttongrid_d.columnCntPerPage);
                btn.Text = buttongrid_d.DT.Rows[I][buttongrid_d.columnshown].ToString();
                btn.Tag = buttongrid_d.DT.Rows[I]["SyncId"].ToString();
                btn.Location = new Point(btnX, btnY);
                btn.Click += new EventHandler((sender, e) => MyEvent(sender, e, cntlParent));
            }

            if (buttongrid_d.currentpage > 1)
            {
                Button btnPrev = new Button();
                btnPrev.Parent = cntlParent;
                btnPrev.Size = new Size(buttongrid_d.buttonwidth, buttongrid_d.buttonheight);
                int btnXPrev = buttongrid_d.initLeftSpacing + (buttongrid_d.buttonwidth + buttongrid_d.LeftSpacing) * (buttongrid_d.getDataPerPage() % buttongrid_d.columnCntPerPage);
                int btnYPrev = buttongrid_d.initTopSpacing + (buttongrid_d.buttonheight + buttongrid_d.RightSpacing) * (buttongrid_d.getDataPerPage() / buttongrid_d.columnCntPerPage);
                btnPrev.BackgroundImage = Properties.Resources.arrow_left;
                btnPrev.Tag = "BtnPrevious";
                btnPrev.Location = new Point(btnXPrev, btnYPrev);
                btnPrev.Click += new EventHandler((sender, e) => MyEvent(sender, e, cntlParent));
            }

            if (buttongrid_d.currentpage < buttongrid_d.getTotalPages())
            {
                Button btnNext = new Button();
                btnNext.Parent = cntlParent;
                btnNext.Size = new Size(buttongrid_d.buttonwidth, buttongrid_d.buttonheight);
                int btnXNext = buttongrid_d.initLeftSpacing + (buttongrid_d.buttonwidth + buttongrid_d.LeftSpacing) * (buttongrid_d.columnCntPerPage - 1);//((buttongrid_d.getDataPerPage() + 1) % buttongrid_d.columnCntPerPage);
                int btnYNext = buttongrid_d.initTopSpacing + (buttongrid_d.buttonheight + buttongrid_d.RightSpacing) * ((buttongrid_d.getDataPerPage() + 1) / buttongrid_d.columnCntPerPage);
                btnNext.BackgroundImage = Properties.Resources.arrow_right;
                btnNext.Tag = "BtnNext";
                btnNext.Location = new Point(btnXNext, btnYNext);
                btnNext.Click += new EventHandler((sender, e) => MyEvent(sender, e, cntlParent));
            }

            txtPage.Text = buttongridProduct.currentpage.ToString();
            lblTotalPage.Text = "/" + buttongridProduct.getTotalPages();
        }

        public void MyEvent(object sender, EventArgs e, Control cntlParent)
        {
            Button btn = (Button)sender;
            if (cntlParent == gbox_dept)
            {
                if (btn.Tag.ToString() == "BtnPrevious")
                {
                    buttongridDepartment.GoToPreivousPage();
                    generateButtonsFromDataTable(gbox_dept, buttongridDepartment);
                }
                else if (btn.Tag.ToString() == "BtnNext")
                {
                    buttongridDepartment.GoToNextPage();
                    generateButtonsFromDataTable(gbox_dept, buttongridDepartment);
                }
                else
                {
                    //foreach (Control cntrl in cntlParent.Controls)
                    //{
                    //    if (cntrl.GetType() == typeof(Button))
                    //    {
                    //        Button btn2 = (Button)cntrl;
                    //        btn2.UseVisualStyleBackColor = true;
                    //    }
                    //}

                    //btn.BackColor = Color.Aqua;
                    buttongridProduct.setTable("Product");
                    buttongridProduct.setColumnShown("product");
                    buttongridProduct.setCondition("WHERE `show`=1 AND `status`=1 AND departmentid=" + btn.Tag.ToString());
                    buttongridProduct.setColumnCntPerPage(4);
                    buttongridProduct.setRowCntPerPage(5);
                    buttongridProduct.setCurrentPage(1);
                    generateButtonsFromDataTable(gbox_products, buttongridProduct);
                }
            }
            else if (cntlParent == gbox_products)
            {
                if (btn.Tag.ToString() == "BtnPrevious")
                {
                    buttongridProduct.GoToPreivousPage();
                    generateButtonsFromDataTable(gbox_products, buttongridProduct);
                }
                else if (btn.Tag.ToString() == "BtnNext")
                {
                    buttongridProduct.GoToNextPage();
                    generateButtonsFromDataTable(gbox_products, buttongridProduct);
                }
                else
                {
                    lastaddedrownumber = tempProductlist.add_product(new cls_product(Convert.ToInt32(btn.Tag)));
                    Refresh_TempProduct_DGV();
                }
            }
            txtPage.Text = buttongridProduct.currentpage.ToString();
            lblTotalPage.Text = "/" + buttongridProduct.getTotalPages();
        }

        public void Refresh_TempProduct_DGV()
        {
            DGVTempProd.Columns.Clear();
            DGVTempProd.Columns.Add("Name", "Name");
            DGVTempProd.Columns.Add("Qty", "Qty");

            //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            //btn.UseColumnTextForButtonValue = true;
            //btn.HeaderText = " ";
            //btn.Text = "X";
            //DGVTempProd.Columns.Add(btn);
            DataGridViewImageColumn img = new DataGridViewImageColumn();
            img.Width = 100;
            img.Image = Properties.Resources.close_bttn;
            DGVTempProd.Columns.Add(img);

            foreach (cls_product prod in tempProductlist.get_productlist())
            {
                DGVTempProd.Rows.Add(prod.getProductName(), prod.Quantity);
            }

            fncFilter.set_dgv_controls(DGVTempProd);

            if (DGVTempProd.RowCount > 1)
                DGVTempProd.Rows[DGVTempProd.RowCount - 1].Selected = true;

            DGVTempProd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            DGVTempProd.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            DGVTempProd.Columns[0].MinimumWidth = DGVTempProd.Width - 20 - DGVTempProd.Columns[1].Width - DGVTempProd.Columns[2].Width;
            DGVTempProd.Columns[0].Width = DGVTempProd.Columns[0].MinimumWidth;

            if (tempProductlist.get_productlist().Count == 0)
            {
                if (lastaddedproduct == null)
                {
                    ctrl_CustomerDisplay.initial_display();
                    return;
                }
                else
                {
                    ctrl_CustomerDisplay.refresh_display_addproduct_and_totalamount(
                        lastaddedproduct,
                        amountBefore + tempProductlist.get_totalamount());
                }
            }
            if (lastaddedrownumber > tempProductlist.get_productlist().Count - 1)
                lastaddedrownumber = tempProductlist.get_productlist().Count - 1;
            if (lastaddedrownumber < 0)
                return;
            ctrl_CustomerDisplay.refresh_display_addproduct_and_totalamount(
                tempProductlist.get_productlist()[lastaddedrownumber],
                amountBefore + tempProductlist.get_totalamount());
        }

        private void DGVTempProd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                cls_product prod = tempProductlist.get_productlist()[e.RowIndex];
                prod.Quantity = prod.Quantity - 1;

                if (prod.Quantity <= 0)
                    tempProductlist.get_productlist().Remove(prod);

                Refresh_TempProduct_DGV();
            }
        }

        private void btnESC_Click(object sender, EventArgs e)
        {
            tempProductlist.get_productlist().Clear();
            this.Close();
        }

        private void frmSearchProductButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                this.Close();
            else if (e.KeyCode == Keys.Escape)
            {
                tempProductlist.get_productlist().Clear();
                this.Close();
                return;
            }
            else
                return;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int changetopage;
                if (!int.TryParse(txtPage.Text, out changetopage))
                    return;

                int totalpages = buttongridProduct.getTotalPages();

                if (changetopage > totalpages)
                    buttongridProduct.setCurrentPage(totalpages);
                else if (changetopage < 1)
                    buttongridProduct.setCurrentPage(1);
                else
                    buttongridProduct.setCurrentPage(changetopage);

                generateButtonsFromDataTable(gbox_products, buttongridProduct);

                DGVTempProd.Focus();
                txtPage.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
            else
                return;
        }
    }
}
