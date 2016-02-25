namespace ETech
{
    partial class frmGiftChequeInfo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblGiftChequeInfo = new System.Windows.Forms.Label();
            this.lblScan = new System.Windows.Forms.Label();
            this.txtScan = new System.Windows.Forms.TextBox();
            this.dgvGiftChequeInfo = new System.Windows.Forms.DataGridView();
            this.colGiftChequeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colwid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTotalAmount_d = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnESC = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiftChequeInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblGiftChequeInfo
            // 
            this.lblGiftChequeInfo.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiftChequeInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblGiftChequeInfo.Location = new System.Drawing.Point(-1, 9);
            this.lblGiftChequeInfo.Name = "lblGiftChequeInfo";
            this.lblGiftChequeInfo.Size = new System.Drawing.Size(497, 55);
            this.lblGiftChequeInfo.TabIndex = 2;
            this.lblGiftChequeInfo.Text = "GIFT CHEQUE INFO";
            this.lblGiftChequeInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScan
            // 
            this.lblScan.AutoSize = true;
            this.lblScan.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblScan.Location = new System.Drawing.Point(9, 73);
            this.lblScan.Name = "lblScan";
            this.lblScan.Size = new System.Drawing.Size(100, 38);
            this.lblScan.TabIndex = 3;
            this.lblScan.Text = "Scan:";
            // 
            // txtScan
            // 
            this.txtScan.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScan.Location = new System.Drawing.Point(115, 70);
            this.txtScan.MaxLength = 30;
            this.txtScan.Name = "txtScan";
            this.txtScan.Size = new System.Drawing.Size(372, 44);
            this.txtScan.TabIndex = 7;
            this.txtScan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtScan_KeyPress);
            // 
            // dgvGiftChequeInfo
            // 
            this.dgvGiftChequeInfo.AllowUserToAddRows = false;
            this.dgvGiftChequeInfo.AllowUserToDeleteRows = false;
            this.dgvGiftChequeInfo.AllowUserToResizeColumns = false;
            this.dgvGiftChequeInfo.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGiftChequeInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGiftChequeInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGiftChequeInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colGiftChequeNo,
            this.colAmount,
            this.colwid});
            this.dgvGiftChequeInfo.Location = new System.Drawing.Point(9, 120);
            this.dgvGiftChequeInfo.Name = "dgvGiftChequeInfo";
            this.dgvGiftChequeInfo.Size = new System.Drawing.Size(478, 162);
            this.dgvGiftChequeInfo.TabIndex = 12;
            // 
            // colGiftChequeNo
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colGiftChequeNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.colGiftChequeNo.HeaderText = "Gift Cheque No.";
            this.colGiftChequeNo.Name = "colGiftChequeNo";
            this.colGiftChequeNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colGiftChequeNo.Width = 270;
            // 
            // colAmount
            // 
            this.colAmount.HeaderText = "Amount";
            this.colAmount.MinimumWidth = 160;
            this.colAmount.Name = "colAmount";
            this.colAmount.Width = 165;
            // 
            // colwid
            // 
            this.colwid.HeaderText = "wid";
            this.colwid.Name = "colwid";
            this.colwid.Visible = false;
            // 
            // lblTotalAmount_d
            // 
            this.lblTotalAmount_d.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount_d.Location = new System.Drawing.Point(275, 327);
            this.lblTotalAmount_d.Name = "lblTotalAmount_d";
            this.lblTotalAmount_d.Size = new System.Drawing.Size(212, 43);
            this.lblTotalAmount_d.TabIndex = 17;
            this.lblTotalAmount_d.Text = "0.00";
            this.lblTotalAmount_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.Location = new System.Drawing.Point(8, 329);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(211, 38);
            this.lblTotalAmount.TabIndex = 16;
            this.lblTotalAmount.Text = "Total Amount:";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(217, 375);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 38);
            this.btnOK.TabIndex = 104;
            this.btnOK.Text = "Okay (F1)";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnESC
            // 
            this.btnESC.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESC.Location = new System.Drawing.Point(343, 375);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(139, 38);
            this.btnESC.TabIndex = 105;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(9, 288);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 38);
            this.btnDelete.TabIndex = 106;
            this.btnDelete.Text = "Delete (F7)";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmGiftChequeInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 418);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.lblTotalAmount_d);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.dgvGiftChequeInfo);
            this.Controls.Add(this.txtScan);
            this.Controls.Add(this.lblScan);
            this.Controls.Add(this.lblGiftChequeInfo);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmGiftChequeInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gift Cheque Information";
            this.Load += new System.EventHandler(this.frmGiftChequeInfo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmGiftChequeInfo_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiftChequeInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGiftChequeInfo;
        private System.Windows.Forms.Label lblScan;
        private System.Windows.Forms.DataGridView dgvGiftChequeInfo;
        private System.Windows.Forms.Label lblTotalAmount_d;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiftChequeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colwid;
        public System.Windows.Forms.TextBox txtScan;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnESC;
        private System.Windows.Forms.Button btnDelete;
    }
}