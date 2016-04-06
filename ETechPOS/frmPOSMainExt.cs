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
    public partial class frmPOSMainExt : Form
    {
        POSMain mainform;
        fncFullScreen fncfullscreen;

        public frmPOSMainExt(POSMain mainform)
        {
            InitializeComponent();
            this.mainform = mainform;
        }

        private void frmPOSMainExt_Load(object sender, EventArgs e)
        {
            fncfullscreen = new fncFullScreen(this);
            fncfullscreen.FullScreen(false, false);
            fncFilter.set_theme_color(this);

            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width + 1, 0);
            lblStoreName.Text = cls_globalvariables.BusinessName_v.ToUpper();
            wbAds.Navigate(cls_globalvariables.ads_url_v);
            tmrUpdateTime.Enabled = true;
        }

        private void dgvProduct_SelectionChanged(object sender, EventArgs e)
        {
            dgvProduct.ClearSelection();
        }

        private void wbAds_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            this.Text = "Navigating";
        }
        private void wbAds_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.Text = e.Url.ToString() + " loaded";
            float zoom = 72;
            zoom = zoom * fncfullscreen.factor;
            wbAds.Document.Body.Style = "zoom: " + zoom.ToString() + "%";
        }

        private void tmrUpdateTime_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("MMMMMMMMMMMMM dd, yyyy hh:mm:ss tt");
        }

        private void dgvProduct_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dgvProduct.RowCount > 0)
                dgvProduct.FirstDisplayedScrollingRowIndex = dgvProduct.RowCount - 1;
        }
        private void dgvProduct_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (dgvProduct.RowCount > 0)
                dgvProduct.FirstDisplayedScrollingRowIndex = dgvProduct.RowCount - 1;
        }

        public void UpdateDGV(cls_POSTransaction tran)
        {
            if (cls_globalvariables.ads_url_v == "")
                return;
            
            dgvProduct.DataSource = null;
            lblTender.Text = "P 0.00";
            lblChange.Text = "P 0.00";
            lblORNumber_d.Text = tran.OfficialReceiptNumber.ToString();
            ctrl_productgrid ctrlprod = new ctrl_productgrid(dgvProduct);
            ctrlprod.set_databinding(tran.get_productlist().get_dtproduct());
            dgvProduct.DataSource = ctrlprod.get_productgrid().DataSource;
            lblTotalAmount.Text = "P " + tran.TotalAmount.ToString("N2");
            lblItemCount.Text = tran.get_productlist().get_productlist().Count.ToString();
        }
        public void AfterTran()
        {
            dgvProduct.DataSource = null;
            lblTotalAmount.Text = "P 0.00";
            lblTender.Text = "P 0.00";
            lblChange.Text = "P 0.00";
            lblORNumber_d.Text = "";
            lblItemCount.Text = "0";
        }
        public void UpdateTenderChange(cls_POSTransaction tran)
        {
            lblTender.Text = "P " + tran.Payments.get_totalamount().ToString("N2");
            lblChange.Text = "P " + tran.get_changeamount().ToString("N2");
        }
    }
}