using ETech.Standard_Views;
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
using ETech.Views.Generic_Controls;
using ETech.Helpers;

namespace ETech.Views.Forms
{
    public partial class TransactionsForm : StandardForm
    {
        public List<cls_POSTransaction> Transactions;
        public cls_POSTransaction CurrentTransaction;

        public TransactionsForm(List<cls_POSTransaction> transactions, cls_POSTransaction currentTransaction)
        {
            Transactions = transactions;
            CurrentTransaction = currentTransaction;

            InitializeComponent();
        }
        
        private void TransactionsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
        private void TransactionsForm_Load(object sender, EventArgs e)
        {
            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();

            lblCurrentTransaction.Text = CurrentTransaction.OfficialReceiptNumber.ToString();
            lblTotalAmount.Text = CurrentTransaction.TotalAmount.ToString("N2");
        }

        private void btnESC_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cgTransactions_Initialize(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;
            cgTransactions.DataSource = DataTableHelper.ConvertToDataTable<cls_POSTransaction>(Transactions);
        }
        private void cgTransactions_Resize(object sender, EventArgs e)
        {
            ControlGrid controlGrid = (ControlGrid)sender;
            if (controlGrid.Size.Width + controlGrid.GridSpacing.Width > 423 * 2)
                controlGrid.GridDimensions = new Size(2, controlGrid.GridDimensions.Height);
            else
                controlGrid.GridDimensions = new Size(1, controlGrid.GridDimensions.Height);
        }
    }
}
