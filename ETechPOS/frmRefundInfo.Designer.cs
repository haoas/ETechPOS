namespace ETech
{
    partial class frmRefundInfo
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
            this.lblProductName = new System.Windows.Forms.Label();
            this.lbl_refundReason = new System.Windows.Forms.Label();
            this.txtORno = new System.Windows.Forms.TextBox();
            this.lblORno = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnESC = new System.Windows.Forms.Button();
            this.cboxRefundReason = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblProductName
            // 
            this.lblProductName.Font = new System.Drawing.Font("Arial", 25F);
            this.lblProductName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblProductName.Location = new System.Drawing.Point(17, 9);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(551, 79);
            this.lblProductName.TabIndex = 1;
            this.lblProductName.Text = "Product Name";
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_refundReason
            // 
            this.lbl_refundReason.AutoSize = true;
            this.lbl_refundReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_refundReason.Location = new System.Drawing.Point(8, 105);
            this.lbl_refundReason.Name = "lbl_refundReason";
            this.lbl_refundReason.Size = new System.Drawing.Size(199, 29);
            this.lbl_refundReason.TabIndex = 2;
            this.lbl_refundReason.Text = "Refund Reason:";
            // 
            // txtORno
            // 
            this.txtORno.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtORno.Location = new System.Drawing.Point(213, 159);
            this.txtORno.Name = "txtORno";
            this.txtORno.Size = new System.Drawing.Size(355, 40);
            this.txtORno.TabIndex = 2;
            this.txtORno.Text = "111110000000";
            this.txtORno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtORno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtORno_KeyDown);
            this.txtORno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtORno_KeyPress);
            // 
            // lblORno
            // 
            this.lblORno.AutoSize = true;
            this.lblORno.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblORno.Location = new System.Drawing.Point(62, 166);
            this.lblORno.Name = "lblORno";
            this.lblORno.Size = new System.Drawing.Size(145, 29);
            this.lblORno.TabIndex = 5;
            this.lblORno.Text = "Sales OR#:";
            // 
            // txtRemark
            // 
            this.txtRemark.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemark.Location = new System.Drawing.Point(213, 215);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(355, 40);
            this.txtRemark.TabIndex = 3;
            this.txtRemark.Text = "remarks";
            this.txtRemark.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemark.Location = new System.Drawing.Point(12, 222);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(195, 29);
            this.lblRemark.TabIndex = 10;
            this.lblRemark.Text = "Other Remarks:";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(139, 271);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 38);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "Okay (F1)";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnESC
            // 
            this.btnESC.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESC.Location = new System.Drawing.Point(284, 271);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(139, 38);
            this.btnESC.TabIndex = 5;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // cboxRefundReason
            // 
            this.cboxRefundReason.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboxRefundReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxRefundReason.Items.AddRange(new object[] {
            "DAMAGE",
            "EXCHANGE"});
            this.cboxRefundReason.Location = new System.Drawing.Point(213, 98);
            this.cboxRefundReason.Name = "cboxRefundReason";
            this.cboxRefundReason.Size = new System.Drawing.Size(355, 41);
            this.cboxRefundReason.Sorted = true;
            this.cboxRefundReason.TabIndex = 1;
            this.cboxRefundReason.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboxRefundReason_KeyPress);
            this.cboxRefundReason.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboxRefundReason_KeyDown);
            // 
            // frmRefundInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 321);
            this.Controls.Add(this.cboxRefundReason);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.lblRemark);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.lblORno);
            this.Controls.Add(this.txtORno);
            this.Controls.Add(this.lbl_refundReason);
            this.Controls.Add(this.lblProductName);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "frmRefundInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Refund";
            this.Load += new System.EventHandler(this.frmRefundInfo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmRefundInfo_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lbl_refundReason;
        private System.Windows.Forms.TextBox txtORno;
        private System.Windows.Forms.Label lblORno;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnESC;
        private System.Windows.Forms.ComboBox cboxRefundReason;
    }
}