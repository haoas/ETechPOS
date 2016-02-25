namespace ETech
{
    partial class frmPOSMainExt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblItemCountTxt = new System.Windows.Forms.Label();
            this.lblItemCount = new System.Windows.Forms.Label();
            this.lblTender = new System.Windows.Forms.Label();
            this.lblTenderTxt = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.tmrUpdateTime = new System.Windows.Forms.Timer(this.components);
            this.lblChangeTxt = new System.Windows.Forms.Label();
            this.lblChange = new System.Windows.Forms.Label();
            this.pnlAds = new System.Windows.Forms.Panel();
            this.wbAds = new System.Windows.Forms.WebBrowser();
            this.lblStoreName = new System.Windows.Forms.Label();
            this.dgvProduct = new System.Windows.Forms.DataGridView();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblORNumber_d = new System.Windows.Forms.Label();
            this.lblORNumber = new System.Windows.Forms.Label();
            this.lblTotalAmountTxt = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.pnlAds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // lblItemCountTxt
            // 
            this.lblItemCountTxt.AutoSize = true;
            this.lblItemCountTxt.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCountTxt.ForeColor = System.Drawing.Color.DarkRed;
            this.lblItemCountTxt.Location = new System.Drawing.Point(464, 96);
            this.lblItemCountTxt.Name = "lblItemCountTxt";
            this.lblItemCountTxt.Size = new System.Drawing.Size(131, 22);
            this.lblItemCountTxt.TabIndex = 5;
            this.lblItemCountTxt.Text = "ITEM COUNT:";
            // 
            // lblItemCount
            // 
            this.lblItemCount.AutoSize = true;
            this.lblItemCount.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCount.ForeColor = System.Drawing.Color.DarkRed;
            this.lblItemCount.Location = new System.Drawing.Point(601, 95);
            this.lblItemCount.Name = "lblItemCount";
            this.lblItemCount.Size = new System.Drawing.Size(19, 23);
            this.lblItemCount.TabIndex = 6;
            this.lblItemCount.Text = "0";
            // 
            // lblTender
            // 
            this.lblTender.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTender.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTender.Location = new System.Drawing.Point(285, 570);
            this.lblTender.Name = "lblTender";
            this.lblTender.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTender.Size = new System.Drawing.Size(352, 56);
            this.lblTender.TabIndex = 9;
            this.lblTender.Text = "P 0.00";
            // 
            // lblTenderTxt
            // 
            this.lblTenderTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenderTxt.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTenderTxt.Location = new System.Drawing.Point(25, 570);
            this.lblTenderTxt.Name = "lblTenderTxt";
            this.lblTenderTxt.Size = new System.Drawing.Size(254, 56);
            this.lblTenderTxt.TabIndex = 10;
            this.lblTenderTxt.Text = "TENDER:";
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTime.ForeColor = System.Drawing.Color.DarkRed;
            this.lblDateTime.Location = new System.Drawing.Point(20, 96);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(282, 22);
            this.lblDateTime.TabIndex = 7;
            this.lblDateTime.Text = "MMM. DD, YYYY HH:MM:SS TT ";
            this.lblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmrUpdateTime
            // 
            this.tmrUpdateTime.Interval = 1000;
            this.tmrUpdateTime.Tick += new System.EventHandler(this.tmrUpdateTime_Tick);
            // 
            // lblChangeTxt
            // 
            this.lblChangeTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangeTxt.ForeColor = System.Drawing.Color.DarkRed;
            this.lblChangeTxt.Location = new System.Drawing.Point(25, 682);
            this.lblChangeTxt.Name = "lblChangeTxt";
            this.lblChangeTxt.Size = new System.Drawing.Size(254, 56);
            this.lblChangeTxt.TabIndex = 15;
            this.lblChangeTxt.Text = "CHANGE:";
            // 
            // lblChange
            // 
            this.lblChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChange.ForeColor = System.Drawing.Color.DarkRed;
            this.lblChange.Location = new System.Drawing.Point(285, 682);
            this.lblChange.Name = "lblChange";
            this.lblChange.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblChange.Size = new System.Drawing.Size(352, 56);
            this.lblChange.TabIndex = 14;
            this.lblChange.Text = "P 0.00";
            // 
            // pnlAds
            // 
            this.pnlAds.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlAds.Controls.Add(this.wbAds);
            this.pnlAds.Location = new System.Drawing.Point(656, 0);
            this.pnlAds.Name = "pnlAds";
            this.pnlAds.Size = new System.Drawing.Size(708, 790);
            this.pnlAds.TabIndex = 0;
            // 
            // wbAds
            // 
            this.wbAds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbAds.Location = new System.Drawing.Point(0, 0);
            this.wbAds.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbAds.Name = "wbAds";
            this.wbAds.ScrollBarsEnabled = false;
            this.wbAds.Size = new System.Drawing.Size(704, 786);
            this.wbAds.TabIndex = 1;
            this.wbAds.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.wbAds_Navigating);
            this.wbAds.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbAds_DocumentCompleted);
            // 
            // lblStoreName
            // 
            this.lblStoreName.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoreName.ForeColor = System.Drawing.Color.DarkRed;
            this.lblStoreName.Location = new System.Drawing.Point(37, -1);
            this.lblStoreName.Name = "lblStoreName";
            this.lblStoreName.Size = new System.Drawing.Size(611, 56);
            this.lblStoreName.TabIndex = 16;
            this.lblStoreName.Text = "STORE NAME";
            this.lblStoreName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvProduct
            // 
            this.dgvProduct.AllowUserToAddRows = false;
            this.dgvProduct.AllowUserToResizeColumns = false;
            this.dgvProduct.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgvProduct.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProduct.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvProduct.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.dgvProduct.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProduct.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.description,
            this.qty,
            this.price,
            this.amount});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Cyan;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProduct.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvProduct.Enabled = false;
            this.dgvProduct.Location = new System.Drawing.Point(24, 136);
            this.dgvProduct.MultiSelect = false;
            this.dgvProduct.Name = "dgvProduct";
            this.dgvProduct.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProduct.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvProduct.RowHeadersVisible = false;
            this.dgvProduct.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProduct.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProduct.Size = new System.Drawing.Size(612, 431);
            this.dgvProduct.TabIndex = 13;
            this.dgvProduct.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvProduct_RowsAdded);
            this.dgvProduct.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvProduct_RowsRemoved);
            this.dgvProduct.SelectionChanged += new System.EventHandler(this.dgvProduct_SelectionChanged);
            // 
            // description
            // 
            this.description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.description.DataPropertyName = "productname";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.description.DefaultCellStyle = dataGridViewCellStyle3;
            this.description.FillWeight = 128.8368F;
            this.description.HeaderText = "Description";
            this.description.Name = "description";
            this.description.ReadOnly = true;
            // 
            // qty
            // 
            this.qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.qty.DataPropertyName = "qty";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.qty.DefaultCellStyle = dataGridViewCellStyle4;
            this.qty.FillWeight = 46.81124F;
            this.qty.HeaderText = "Qty";
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            // 
            // price
            // 
            this.price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.price.DataPropertyName = "price";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.price.DefaultCellStyle = dataGridViewCellStyle5;
            this.price.FillWeight = 61.91541F;
            this.price.HeaderText = "Price";
            this.price.Name = "price";
            this.price.ReadOnly = true;
            // 
            // amount
            // 
            this.amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.amount.DataPropertyName = "amount";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.amount.DefaultCellStyle = dataGridViewCellStyle6;
            this.amount.FillWeight = 162.4366F;
            this.amount.HeaderText = "Amount";
            this.amount.Name = "amount";
            this.amount.ReadOnly = true;
            // 
            // lblORNumber_d
            // 
            this.lblORNumber_d.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblORNumber_d.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblORNumber_d.ForeColor = System.Drawing.Color.DarkRed;
            this.lblORNumber_d.Location = new System.Drawing.Point(121, 55);
            this.lblORNumber_d.Name = "lblORNumber_d";
            this.lblORNumber_d.Size = new System.Drawing.Size(375, 39);
            this.lblORNumber_d.TabIndex = 18;
            this.lblORNumber_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblORNumber
            // 
            this.lblORNumber.AutoSize = true;
            this.lblORNumber.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblORNumber.ForeColor = System.Drawing.Color.DarkRed;
            this.lblORNumber.Location = new System.Drawing.Point(17, 55);
            this.lblORNumber.Name = "lblORNumber";
            this.lblORNumber.Size = new System.Drawing.Size(98, 40);
            this.lblORNumber.TabIndex = 17;
            this.lblORNumber.Text = "OR#:";
            // 
            // lblTotalAmountTxt
            // 
            this.lblTotalAmountTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmountTxt.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTotalAmountTxt.Location = new System.Drawing.Point(25, 626);
            this.lblTotalAmountTxt.Name = "lblTotalAmountTxt";
            this.lblTotalAmountTxt.Size = new System.Drawing.Size(206, 56);
            this.lblTotalAmountTxt.TabIndex = 20;
            this.lblTotalAmountTxt.Text = "TOTAL:";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTotalAmount.Location = new System.Drawing.Point(237, 626);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTotalAmount.Size = new System.Drawing.Size(400, 56);
            this.lblTotalAmount.TabIndex = 19;
            this.lblTotalAmount.Text = "P 0.00";
            // 
            // frmPOSMainExt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.lblTotalAmountTxt);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.lblTenderTxt);
            this.Controls.Add(this.lblTender);
            this.Controls.Add(this.lblDateTime);
            this.Controls.Add(this.lblORNumber_d);
            this.Controls.Add(this.lblORNumber);
            this.Controls.Add(this.lblStoreName);
            this.Controls.Add(this.pnlAds);
            this.Controls.Add(this.lblChangeTxt);
            this.Controls.Add(this.lblChange);
            this.Controls.Add(this.dgvProduct);
            this.Controls.Add(this.lblItemCount);
            this.Controls.Add(this.lblItemCountTxt);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(1367, 0);
            this.MaximizeBox = false;
            this.Name = "frmPOSMainExt";
            this.Text = "frmPOSMainExt";
            this.Load += new System.EventHandler(this.frmPOSMainExt_Load);
            this.pnlAds.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblItemCountTxt;
        private System.Windows.Forms.Label lblItemCount;
        private System.Windows.Forms.Label lblTender;
        private System.Windows.Forms.Label lblTenderTxt;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Timer tmrUpdateTime;
        private System.Windows.Forms.Label lblChangeTxt;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Panel pnlAds;
        private System.Windows.Forms.WebBrowser wbAds;
        private System.Windows.Forms.Label lblStoreName;
        public System.Windows.Forms.DataGridView dgvProduct;
        private System.Windows.Forms.Label lblORNumber_d;
        private System.Windows.Forms.Label lblORNumber;
        private System.Windows.Forms.Label lblTotalAmountTxt;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;

    }
}