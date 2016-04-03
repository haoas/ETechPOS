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
            this.lblAdjustTo = new System.Windows.Forms.Label();
            this.lblOrigPrice = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnF11 = new System.Windows.Forms.Button();
            this.BtnProceed = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.BtnF3 = new System.Windows.Forms.Button();
            this.BtnF2 = new System.Windows.Forms.Button();
            this.BtnF1 = new System.Windows.Forms.Button();
            this.lblNewPrice = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 24.75F);
            this.label2.Location = new System.Drawing.Point(445, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 51);
            this.label2.TabIndex = 26;
            this.label2.Text = "%";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNewPrice_d
            // 
            this.lblNewPrice_d.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewPrice_d.ForeColor = System.Drawing.Color.Black;
            this.lblNewPrice_d.Location = new System.Drawing.Point(290, 283);
            this.lblNewPrice_d.Name = "lblNewPrice_d";
            this.lblNewPrice_d.Size = new System.Drawing.Size(201, 51);
            this.lblNewPrice_d.TabIndex = 16;
            this.lblNewPrice_d.Text = "0.00";
            this.lblNewPrice_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOrigPrice_d
            // 
            this.lblOrigPrice_d.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrigPrice_d.Location = new System.Drawing.Point(296, 94);
            this.lblOrigPrice_d.Name = "lblOrigPrice_d";
            this.lblOrigPrice_d.Size = new System.Drawing.Size(195, 51);
            this.lblOrigPrice_d.TabIndex = 15;
            this.lblOrigPrice_d.Text = "000,000.00";
            this.lblOrigPrice_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDiscount
            // 
            this.txtDiscount.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscount.Location = new System.Drawing.Point(296, 152);
            this.txtDiscount.MaxLength = 10;
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(151, 41);
            this.txtDiscount.TabIndex = 19;
            this.txtDiscount.Tag = "num";
            this.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiscount.TextChanged += new System.EventHandler(this.txtDiscount_TextChanged);
            this.txtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDiscount_KeyPress);
            // 
            // txtAdjustTo
            // 
            this.txtAdjustTo.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdjustTo.Location = new System.Drawing.Point(296, 207);
            this.txtAdjustTo.MaxLength = 20;
            this.txtAdjustTo.Name = "txtAdjustTo";
            this.txtAdjustTo.Size = new System.Drawing.Size(195, 41);
            this.txtAdjustTo.TabIndex = 17;
            this.txtAdjustTo.Tag = "num";
            this.txtAdjustTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAdjustTo.TextChanged += new System.EventHandler(this.txtAdjustTo_TextChanged);
            this.txtAdjustTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAdjustTo_KeyPress);
            // 
            // lblAdjustTo
            // 
            this.lblAdjustTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdjustTo.Location = new System.Drawing.Point(13, 197);
            this.lblAdjustTo.Name = "lblAdjustTo";
            this.lblAdjustTo.Size = new System.Drawing.Size(252, 51);
            this.lblAdjustTo.TabIndex = 22;
            this.lblAdjustTo.Text = "Set Fixed Amount:";
            this.lblAdjustTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOrigPrice
            // 
            this.lblOrigPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrigPrice.Location = new System.Drawing.Point(13, 91);
            this.lblOrigPrice.Name = "lblOrigPrice";
            this.lblOrigPrice.Size = new System.Drawing.Size(252, 51);
            this.lblOrigPrice.TabIndex = 21;
            this.lblOrigPrice.Text = "Original Amount:";
            this.lblOrigPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(112, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(498, 51);
            this.label1.TabIndex = 108;
            this.label1.Text = "Transaction Discount/Adjustment";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnF11
            // 
            this.BtnF11.BackColor = System.Drawing.Color.White;
            this.BtnF11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnF11.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnF11.ForeColor = System.Drawing.Color.Black;
            this.BtnF11.Location = new System.Drawing.Point(615, 168);
            this.BtnF11.Name = "BtnF11";
            this.BtnF11.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.BtnF11.Size = new System.Drawing.Size(100, 80);
            this.BtnF11.TabIndex = 123;
            this.BtnF11.Text = "[ F11 ]\r\nREMOVE\r\nDISCOUNT";
            this.BtnF11.UseVisualStyleBackColor = false;
            this.BtnF11.Click += new System.EventHandler(this.BtnF11_Click);
            // 
            // BtnProceed
            // 
            this.BtnProceed.BackColor = System.Drawing.Color.White;
            this.BtnProceed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnProceed.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnProceed.ForeColor = System.Drawing.Color.Black;
            this.BtnProceed.Location = new System.Drawing.Point(615, 254);
            this.BtnProceed.Name = "BtnProceed";
            this.BtnProceed.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.BtnProceed.Size = new System.Drawing.Size(100, 80);
            this.BtnProceed.TabIndex = 122;
            this.BtnProceed.Text = "[ F12 ]\r\nPROCEED";
            this.BtnProceed.UseVisualStyleBackColor = false;
            this.BtnProceed.Click += new System.EventHandler(this.BtnProceed_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(615, 82);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.button1.Size = new System.Drawing.Size(100, 80);
            this.button1.TabIndex = 121;
            this.button1.Text = "[ ESC ]\r\nCANCEL";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BtnF3
            // 
            this.BtnF3.BackColor = System.Drawing.Color.White;
            this.BtnF3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnF3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnF3.ForeColor = System.Drawing.Color.Black;
            this.BtnF3.Location = new System.Drawing.Point(509, 254);
            this.BtnF3.Name = "BtnF3";
            this.BtnF3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.BtnF3.Size = new System.Drawing.Size(100, 80);
            this.BtnF3.TabIndex = 119;
            this.BtnF3.Text = "[ F3 ]\r\n15%\r\nDISCOUNT";
            this.BtnF3.UseVisualStyleBackColor = false;
            this.BtnF3.Click += new System.EventHandler(this.BtnF3_Click);
            // 
            // BtnF2
            // 
            this.BtnF2.BackColor = System.Drawing.Color.White;
            this.BtnF2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnF2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnF2.ForeColor = System.Drawing.Color.Black;
            this.BtnF2.Location = new System.Drawing.Point(509, 168);
            this.BtnF2.Name = "BtnF2";
            this.BtnF2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.BtnF2.Size = new System.Drawing.Size(100, 80);
            this.BtnF2.TabIndex = 118;
            this.BtnF2.Text = "[ F2 ]\r\n10%\r\nDISCOUNT";
            this.BtnF2.UseVisualStyleBackColor = false;
            this.BtnF2.Click += new System.EventHandler(this.BtnF2_Click);
            // 
            // BtnF1
            // 
            this.BtnF1.BackColor = System.Drawing.Color.White;
            this.BtnF1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnF1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnF1.ForeColor = System.Drawing.Color.Black;
            this.BtnF1.Location = new System.Drawing.Point(509, 82);
            this.BtnF1.Name = "BtnF1";
            this.BtnF1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.BtnF1.Size = new System.Drawing.Size(100, 80);
            this.BtnF1.TabIndex = 117;
            this.BtnF1.Text = "[ F1 ]\r\n5%\r\nDISCOUNT\r\n";
            this.BtnF1.UseVisualStyleBackColor = false;
            this.BtnF1.Click += new System.EventHandler(this.BtnF1_Click);
            // 
            // lblNewPrice
            // 
            this.lblNewPrice.BackColor = System.Drawing.Color.White;
            this.lblNewPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewPrice.ForeColor = System.Drawing.Color.Black;
            this.lblNewPrice.Location = new System.Drawing.Point(13, 283);
            this.lblNewPrice.Name = "lblNewPrice";
            this.lblNewPrice.Size = new System.Drawing.Size(252, 51);
            this.lblNewPrice.TabIndex = 127;
            this.lblNewPrice.Text = "Adjusted Amount:";
            this.lblNewPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(249, 51);
            this.label3.TabIndex = 120;
            this.label3.Text = "Discount by:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmTransactionAdjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 353);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblNewPrice);
            this.Controls.Add(this.BtnF11);
            this.Controls.Add(this.BtnProceed);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BtnF3);
            this.Controls.Add(this.BtnF2);
            this.Controls.Add(this.BtnF1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNewPrice_d);
            this.Controls.Add(this.lblOrigPrice_d);
            this.Controls.Add(this.txtDiscount);
            this.Controls.Add(this.txtAdjustTo);
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
        private System.Windows.Forms.Label lblAdjustTo;
        private System.Windows.Forms.Label lblOrigPrice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnF11;
        private System.Windows.Forms.Button BtnProceed;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BtnF3;
        private System.Windows.Forms.Button BtnF2;
        private System.Windows.Forms.Button BtnF1;
        private System.Windows.Forms.Label lblNewPrice;
        private System.Windows.Forms.Label label3;
    }
}