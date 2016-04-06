using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;

namespace ETech.Views.Controls
{
    public partial class TransactionControl : UserControl
    {
        public long OfficialReceiptNumber
        {
            set { lblTransaction.Text = value.ToString(); }
        }
        public decimal TotalAmount
        {
            set { lblTotalAmount.Text = value.ToString("N2"); }
        }

        public TransactionControl()
        {
            InitializeComponent();
        }
    }
}
