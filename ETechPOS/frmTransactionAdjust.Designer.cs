namespace ETech
{
    partial class frmTransactionAdjust
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
            this.label2 = new System.Windows.Forms.Label();
            this.lblNewPrice_d = new System.Windows.Forms.Label();
            this.lblOrigPrice_d = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.txtAdjustTo = new System.Windows.Forms.TextBox();
            this.lblNewPrice = new System.Windows.Forms.Label();
            this.lblOr = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblAdjustTo = new System.Windows.Forms.Label();
            this.lblOrigPrice = new System.Windows.Forms.Label();
            this.lblCustomDiscount = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnESC = new System.Windows.Forms.Button();
            this.btnCustom = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 24.75F);
            this.label2.Location = new System.Drawing.Point(268, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 51);
            this.label2.TabIndex = 26;
            this.label2.Text = "%";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNewPrice_d
            // 
            this.lblNewPrice_d.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblNewPrice_d.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblNewPrice_d.Location = new System.Drawing.Point(229, 197);
            this.lblNewPrice_d.Name = "lblNewPrice_d";
            this.lblNewPrice_d.Size = new System.Drawing.Size(268, 51);
            this.lblNewPrice_d.TabIndex = 16;
            this.lblNewPrice_d.Text = "0.00";
            this.lblNewPrice_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOrigPrice_d
            // 
            this.lblOrigPrice_d.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblOrigPrice_d.Location = new System.Drawing.Point(240, 9);
            this.lblOrigPrice_d.Name = "lblOrigPrice_d";
            this.lblOrigPrice_d.Size = new System.Drawing.Size(257, 51);
            this.lblOrigPrice_d.TabIndex = 15;
            this.lblOrigPrice_d.Text = "0.00";
            this.lblOrigPrice_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDiscount
            // 
            this.txtDiscount.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscount.Location = new System.Drawing.Point(174, 142);
            this.txtDiscount.MaxLength = 10;
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(88, 41);
            this.txtDiscount.TabIndex = 19;
            this.txtDiscount.Tag = "num";
            this.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiscount.TextChanged += new System.EventHandler(this.txtDiscount_TextChanged);
            this.txtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDiscount_KeyPress);
            // 
            // txtAdjustTo
            // 
            this.txtAdjustTo.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdjustTo.Location = new System.Drawing.Point(174, 66);
            this.txtAdjustTo.MaxLength = 20;
            this.txtAdjustTo.Name = "txtAdjustTo";
            this.txtAdjustTo.Size = new System.Drawing.Size(323, 41);
            this.txtAdjustTo.TabIndex = 17;
            this.txtAdjustTo.Tag = "num";
            this.txtAdjustTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAdjustTo.TextChanged += new System.EventHandler(this.txtAdjustTo_TextChanged);
            this.txtAdjustTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAdjustTo_KeyPress);
            // 
            // lblNewPrice
            // 
            this.lblNewPrice.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblNewPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblNewPrice.Location = new System.Drawing.Point(10, 197);
            this.lblNewPrice.Name = "lblNewPrice";
            this.lblNewPrice.Size = new System.Drawing.Size(213, 51);
            this.lblNewPrice.TabIndex = 18;
            this.lblNewPrice.Text = "New Amount:";
            this.lblNewPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOr
            // 
            this.lblOr.AutoSize = true;
            this.lblOr.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOr.Location = new System.Drawing.Point(62, 111);
            this.lblOr.Name = "lblOr";
            this.lblOr.Size = new System.Drawing.Size(29, 24);
            this.lblOr.TabIndex = 24;
            this.lblOr.Text = "or";
            // 
            // lblDiscount
            // 
            this.lblDiscount.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblDiscount.Location = new System.Drawing.Point(10, 136);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(159, 51);
            this.lblDiscount.TabIndex = 23;
            this.lblDiscount.Text = "Discount:";
            this.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAdjustTo
            // 
            this.lblAdjustTo.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblAdjustTo.Location = new System.Drawing.Point(10, 60);
            this.lblAdjustTo.Name = "lblAdjustTo";
            this.lblAdjustTo.Size = new System.Drawing.Size(159, 51);
            this.lblAdjustTo.TabIndex = 22;
            this.lblAdjustTo.Text = "Adjust To:";
            this.lblAdjustTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOrigPrice
            // 
            this.lblOrigPrice.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblOrigPrice.Location = new System.Drawing.Point(10, 9);
            this.lblOrigPrice.Name = "lblOrigPrice";
            this.lblOrigPrice.Size = new System.Drawing.Size(224, 51);
            this.lblOrigPrice.TabIndex = 21;
            this.lblOrigPrice.Text = "Orig. Amount:";
            this.lblOrigPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCustomDiscount
            // 
            this.lblCustomDiscount.AutoSize = true;
            this.lblCustomDiscount.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomDiscount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCustomDiscount.Location = new System.Drawing.Point(171, 184);
            this.lblCustomDiscount.Name = "lblCustomDiscount";
            this.lblCustomDiscount.Size = new System.Drawing.Size(103, 15);
            this.lblCustomDiscount.TabIndex = 33;
            this.lblCustomDiscount.Text = "Regular Discount";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(257, 251);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(115, 75);
            this.btnOK.TabIndex = 104;
            this.btnOK.Text = "Okay\r\n(F1)";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnESC
            // 
            this.btnESC.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESC.Location = new System.Drawing.Point(383, 251);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(115, 75);
            this.btnESC.TabIndex = 105;
            this.btnESC.Text = "Close\r\n(ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // btnCustom
            // 
            this.btnCustom.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustom.Location = new System.Drawing.Point(8, 251);
            this.btnCustom.Name = "btnCustom";
            this.btnCustom.Size = new System.Drawing.Size(115, 75);
            this.btnCustom.TabIndex = 106;
            this.btnCustom.Text = "Custom\r\n(F11)";
            this.btnCustom.UseVisualStyleBackColor = false;
            this.btnCustom.Click += new System.EventHandler(this.btnCustom_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(134, 251);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(115, 75);
            this.btnRemove.TabIndex = 107;
            this.btnRemove.Text = "Remove\r\n(F12)";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // frmTransactionAdjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 333);
            this.Controls.Add(this.btnCustom);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.lblCustomDiscount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNewPrice_d);
            this.Controls.Add(this.lblOrigPrice_d);
            this.Controls.Add(this.txtDiscount);
            this.Controls.Add(this.txtAdjustTo);
            this.Controls.Add(this.lblNewPrice);
            this.Controls.Add(this.lblOr);
            this.Controls.Add(this.lblDiscount);
            this.Controls.Add(this.lblAdjustTo);
            this.Controls.Add(this.lblOrigPrice);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmTransactionAdjust";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Transaction Adjust";
            this.Load += new System.EventHandler(this.frmTransactionAdjust_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTransactionAdjust_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNewPrice_d;
        private System.Windows.Forms.Label lblOrigPrice_d;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.TextBox txtAdjustTo;
        private System.Windows.Forms.Label lblNewPrice;
        private System.Windows.Forms.Label lblOr;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblAdjustTo;
        private System.Windows.Forms.Label lblOrigPrice;
        private System.Windows.Forms.Label lblCustomDiscount;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnESC;
        private System.Windows.Forms.Button btnCustom;
        private System.Windows.Forms.Button btnRemove;
    }
}