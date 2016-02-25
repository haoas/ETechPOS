namespace ETech
{
    partial class frmTransactionDiscountList
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
            this.dgDiscounts = new System.Windows.Forms.DataGridView();
            this.discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.basis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ismultiple = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actualValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblAmtFrom = new System.Windows.Forms.Label();
            this.lblAmtTo = new System.Windows.Forms.Label();
            this.lblAmtToTxt = new System.Windows.Forms.Label();
            this.lblAmtFromTxt = new System.Windows.Forms.Label();
            this.btnESC = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgDiscounts)).BeginInit();
            this.SuspendLayout();
            // 
            // dgDiscounts
            // 
            this.dgDiscounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDiscounts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.discount,
            this.basis,
            this.value,
            this.wid,
            this.status,
            this.ismultiple,
            this.actualValue});
            this.dgDiscounts.Location = new System.Drawing.Point(12, 12);
            this.dgDiscounts.MultiSelect = false;
            this.dgDiscounts.Name = "dgDiscounts";
            this.dgDiscounts.RowHeadersVisible = false;
            this.dgDiscounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDiscounts.Size = new System.Drawing.Size(305, 150);
            this.dgDiscounts.TabIndex = 0;
            // 
            // discount
            // 
            this.discount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.discount.HeaderText = "Discount";
            this.discount.Name = "discount";
            this.discount.ReadOnly = true;
            this.discount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // basis
            // 
            this.basis.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.basis.HeaderText = "Basis";
            this.basis.Name = "basis";
            this.basis.ReadOnly = true;
            this.basis.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // value
            // 
            this.value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.value.HeaderText = "Value";
            this.value.Name = "value";
            this.value.ReadOnly = true;
            this.value.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // wid
            // 
            this.wid.HeaderText = "wid";
            this.wid.Name = "wid";
            this.wid.Visible = false;
            // 
            // status
            // 
            this.status.HeaderText = "status";
            this.status.Name = "status";
            this.status.Visible = false;
            // 
            // ismultiple
            // 
            this.ismultiple.HeaderText = "ismultiple";
            this.ismultiple.Name = "ismultiple";
            this.ismultiple.Visible = false;
            // 
            // actualValue
            // 
            this.actualValue.HeaderText = "value";
            this.actualValue.Name = "actualValue";
            this.actualValue.Visible = false;
            // 
            // lblAmtFrom
            // 
            this.lblAmtFrom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAmtFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmtFrom.Location = new System.Drawing.Point(364, 12);
            this.lblAmtFrom.Name = "lblAmtFrom";
            this.lblAmtFrom.Size = new System.Drawing.Size(226, 44);
            this.lblAmtFrom.TabIndex = 17;
            // 
            // lblAmtTo
            // 
            this.lblAmtTo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAmtTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmtTo.Location = new System.Drawing.Point(364, 65);
            this.lblAmtTo.Name = "lblAmtTo";
            this.lblAmtTo.Size = new System.Drawing.Size(226, 44);
            this.lblAmtTo.TabIndex = 18;
            // 
            // lblAmtToTxt
            // 
            this.lblAmtToTxt.AutoSize = true;
            this.lblAmtToTxt.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmtToTxt.Location = new System.Drawing.Point(339, 80);
            this.lblAmtToTxt.Name = "lblAmtToTxt";
            this.lblAmtToTxt.Size = new System.Drawing.Size(21, 14);
            this.lblAmtToTxt.TabIndex = 19;
            this.lblAmtToTxt.Text = "To:";
            // 
            // lblAmtFromTxt
            // 
            this.lblAmtFromTxt.AutoSize = true;
            this.lblAmtFromTxt.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmtFromTxt.Location = new System.Drawing.Point(325, 27);
            this.lblAmtFromTxt.Name = "lblAmtFromTxt";
            this.lblAmtFromTxt.Size = new System.Drawing.Size(34, 14);
            this.lblAmtFromTxt.TabIndex = 20;
            this.lblAmtFromTxt.Text = "From:";
            // 
            // btnESC
            // 
            this.btnESC.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESC.Location = new System.Drawing.Point(451, 124);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(139, 38);
            this.btnESC.TabIndex = 104;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // frmTransactionDiscountList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 169);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.lblAmtFromTxt);
            this.Controls.Add(this.lblAmtToTxt);
            this.Controls.Add(this.lblAmtTo);
            this.Controls.Add(this.lblAmtFrom);
            this.Controls.Add(this.dgDiscounts);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmTransactionDiscountList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transaction Discounts";
            this.Load += new System.EventHandler(this.frmTransactionDiscountList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTransactionDiscountList_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgDiscounts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgDiscounts;
        private System.Windows.Forms.Label lblAmtFrom;
        private System.Windows.Forms.Label lblAmtTo;
        private System.Windows.Forms.Label lblAmtToTxt;
        private System.Windows.Forms.Label lblAmtFromTxt;
        private System.Windows.Forms.DataGridViewTextBoxColumn discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn basis;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
        private System.Windows.Forms.DataGridViewTextBoxColumn wid;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn ismultiple;
        private System.Windows.Forms.DataGridViewTextBoxColumn actualValue;
        private System.Windows.Forms.Button btnESC;
    }
}