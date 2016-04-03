namespace ETech
{
    partial class frmProductAdjust
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
            this.lblOrigPrice = new System.Windows.Forms.Label();
            this.lblAdjustTo = new System.Windows.Forms.Label();
            this.lblNewPrice = new System.Windows.Forms.Label();
            this.txtAdjustTo = new System.Windows.Forms.TextBox();
            this.lblOrigPrice_d = new System.Windows.Forms.Label();
            this.lblNewPrice_d = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnF2 = new System.Windows.Forms.Button();
            this.BtnF1 = new System.Windows.Forms.Button();
            this.BtnF4 = new System.Windows.Forms.Button();
            this.BtnF3 = new System.Windows.Forms.Button();
            this.BtnProceed = new System.Windows.Forms.Button();
            this.BtnESC = new System.Windows.Forms.Button();
            this.BtnF11 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblProductName
            // 
            this.lblProductName.BackColor = System.Drawing.Color.White;
            this.lblProductName.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblProductName.Location = new System.Drawing.Point(12, 64);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(475, 114);
            this.lblProductName.TabIndex = 3;
            this.lblProductName.Text = "Product Name";
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOrigPrice
            // 
            this.lblOrigPrice.BackColor = System.Drawing.Color.White;
            this.lblOrigPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrigPrice.Location = new System.Drawing.Point(12, 193);
            this.lblOrigPrice.Name = "lblOrigPrice";
            this.lblOrigPrice.Size = new System.Drawing.Size(250, 51);
            this.lblOrigPrice.TabIndex = 3;
            this.lblOrigPrice.Text = "Original Price:";
            this.lblOrigPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAdjustTo
            // 
            this.lblAdjustTo.BackColor = System.Drawing.Color.White;
            this.lblAdjustTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdjustTo.Location = new System.Drawing.Point(13, 294);
            this.lblAdjustTo.Name = "lblAdjustTo";
            this.lblAdjustTo.Size = new System.Drawing.Size(249, 51);
            this.lblAdjustTo.TabIndex = 4;
            this.lblAdjustTo.Text = "Set Fixed Price:";
            this.lblAdjustTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNewPrice
            // 
            this.lblNewPrice.BackColor = System.Drawing.Color.White;
            this.lblNewPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewPrice.ForeColor = System.Drawing.Color.Black;
            this.lblNewPrice.Location = new System.Drawing.Point(12, 370);
            this.lblNewPrice.Name = "lblNewPrice";
            this.lblNewPrice.Size = new System.Drawing.Size(250, 51);
            this.lblNewPrice.TabIndex = 0;
            this.lblNewPrice.Text = "Adjusted Price:";
            this.lblNewPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAdjustTo
            // 
            this.txtAdjustTo.BackColor = System.Drawing.Color.White;
            this.txtAdjustTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdjustTo.Location = new System.Drawing.Point(285, 301);
            this.txtAdjustTo.MaxLength = 20;
            this.txtAdjustTo.Name = "txtAdjustTo";
            this.txtAdjustTo.Size = new System.Drawing.Size(203, 44);
            this.txtAdjustTo.TabIndex = 1;
            this.txtAdjustTo.Tag = "num";
            this.txtAdjustTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAdjustTo.TextChanged += new System.EventHandler(this.txtAdjustTo_TextChanged);
            this.txtAdjustTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAdjustTo_KeyPress);
            // 
            // lblOrigPrice_d
            // 
            this.lblOrigPrice_d.BackColor = System.Drawing.Color.White;
            this.lblOrigPrice_d.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrigPrice_d.Location = new System.Drawing.Point(284, 193);
            this.lblOrigPrice_d.Name = "lblOrigPrice_d";
            this.lblOrigPrice_d.Size = new System.Drawing.Size(203, 51);
            this.lblOrigPrice_d.TabIndex = 0;
            this.lblOrigPrice_d.Text = "000,000.00";
            this.lblOrigPrice_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNewPrice_d
            // 
            this.lblNewPrice_d.BackColor = System.Drawing.Color.White;
            this.lblNewPrice_d.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewPrice_d.ForeColor = System.Drawing.Color.Black;
            this.lblNewPrice_d.Location = new System.Drawing.Point(284, 370);
            this.lblNewPrice_d.Name = "lblNewPrice_d";
            this.lblNewPrice_d.Size = new System.Drawing.Size(203, 51);
            this.lblNewPrice_d.TabIndex = 0;
            this.lblNewPrice_d.Text = "0,000,000.00";
            this.lblNewPrice_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(129, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(458, 51);
            this.label1.TabIndex = 104;
            this.label1.Text = "Product Discount/Adjustment";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnF2
            // 
            this.BtnF2.BackColor = System.Drawing.Color.White;
            this.BtnF2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnF2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnF2.ForeColor = System.Drawing.Color.Black;
            this.BtnF2.Location = new System.Drawing.Point(493, 169);
            this.BtnF2.Name = "BtnF2";
            this.BtnF2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.BtnF2.Size = new System.Drawing.Size(100, 80);
            this.BtnF2.TabIndex = 111;
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
            this.BtnF1.Location = new System.Drawing.Point(493, 83);
            this.BtnF1.Name = "BtnF1";
            this.BtnF1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.BtnF1.Size = new System.Drawing.Size(100, 80);
            this.BtnF1.TabIndex = 110;
            this.BtnF1.Text = "[ F1 ]\r\n5%\r\nDISCOUNT\r\n";
            this.BtnF1.UseVisualStyleBackColor = false;
            this.BtnF1.Click += new System.EventHandler(this.BtnF1_Click);
            // 
            // BtnF4
            // 
            this.BtnF4.BackColor = System.Drawing.Color.White;
            this.BtnF4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnF4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnF4.ForeColor = System.Drawing.Color.Black;
            this.BtnF4.Location = new System.Drawing.Point(493, 341);
            this.BtnF4.Name = "BtnF4";
            this.BtnF4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.BtnF4.Size = new System.Drawing.Size(100, 80);
            this.BtnF4.TabIndex = 113;
            this.BtnF4.Text = "[ F4 ]\r\n20%\r\nDISCOUNT";
            this.BtnF4.UseVisualStyleBackColor = false;
            this.BtnF4.Click += new System.EventHandler(this.BtnF4_Click);
            // 
            // BtnF3
            // 
            this.BtnF3.BackColor = System.Drawing.Color.White;
            this.BtnF3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnF3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnF3.ForeColor = System.Drawing.Color.Black;
            this.BtnF3.Location = new System.Drawing.Point(493, 255);
            this.BtnF3.Name = "BtnF3";
            this.BtnF3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.BtnF3.Size = new System.Drawing.Size(100, 80);
            this.BtnF3.TabIndex = 112;
            this.BtnF3.Text = "[ F3 ]\r\n15%\r\nDISCOUNT";
            this.BtnF3.UseVisualStyleBackColor = false;
            this.BtnF3.Click += new System.EventHandler(this.BtnF3_Click);
            // 
            // BtnProceed
            // 
            this.BtnProceed.BackColor = System.Drawing.Color.White;
            this.BtnProceed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnProceed.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnProceed.ForeColor = System.Drawing.Color.Black;
            this.BtnProceed.Location = new System.Drawing.Point(599, 341);
            this.BtnProceed.Name = "BtnProceed";
            this.BtnProceed.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.BtnProceed.Size = new System.Drawing.Size(100, 80);
            this.BtnProceed.TabIndex = 115;
            this.BtnProceed.Text = "[ F12 ]\r\nPROCEED";
            this.BtnProceed.UseVisualStyleBackColor = false;
            this.BtnProceed.Click += new System.EventHandler(this.BtnProceed_Click);
            // 
            // BtnESC
            // 
            this.BtnESC.BackColor = System.Drawing.Color.White;
            this.BtnESC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnESC.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnESC.ForeColor = System.Drawing.Color.Black;
            this.BtnESC.Location = new System.Drawing.Point(599, 169);
            this.BtnESC.Name = "BtnESC";
            this.BtnESC.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.BtnESC.Size = new System.Drawing.Size(100, 80);
            this.BtnESC.TabIndex = 114;
            this.BtnESC.Text = "[ ESC ]\r\nCANCEL";
            this.BtnESC.UseVisualStyleBackColor = false;
            this.BtnESC.Click += new System.EventHandler(this.button4_Click);
            // 
            // BtnF11
            // 
            this.BtnF11.BackColor = System.Drawing.Color.White;
            this.BtnF11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnF11.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnF11.ForeColor = System.Drawing.Color.Black;
            this.BtnF11.Location = new System.Drawing.Point(599, 255);
            this.BtnF11.Name = "BtnF11";
            this.BtnF11.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.BtnF11.Size = new System.Drawing.Size(100, 80);
            this.BtnF11.TabIndex = 116;
            this.BtnF11.Text = "[ F11 ]\r\nREMOVE\r\nDISCOUNT";
            this.BtnF11.UseVisualStyleBackColor = false;
            this.BtnF11.Click += new System.EventHandler(this.Btn11_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 24F);
            this.label2.Location = new System.Drawing.Point(448, 247);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 51);
            this.label2.TabIndex = 119;
            this.label2.Text = "%";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDiscount
            // 
            this.txtDiscount.BackColor = System.Drawing.Color.White;
            this.txtDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscount.Location = new System.Drawing.Point(285, 251);
            this.txtDiscount.MaxLength = 10;
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(151, 44);
            this.txtDiscount.TabIndex = 0;
            this.txtDiscount.Tag = "num";
            this.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiscount.TextChanged += new System.EventHandler(this.txtDiscount_TextChanged);
            // 
            // lblDiscount
            // 
            this.lblDiscount.BackColor = System.Drawing.Color.White;
            this.lblDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscount.Location = new System.Drawing.Point(13, 244);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(249, 51);
            this.lblDiscount.TabIndex = 118;
            this.lblDiscount.Text = "Discount by:";
            this.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmProductAdjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(714, 432);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDiscount);
            this.Controls.Add(this.lblDiscount);
            this.Controls.Add(this.BtnF11);
            this.Controls.Add(this.BtnProceed);
            this.Controls.Add(this.BtnESC);
            this.Controls.Add(this.BtnF4);
            this.Controls.Add(this.BtnF3);
            this.Controls.Add(this.BtnF2);
            this.Controls.Add(this.BtnF1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.lblNewPrice_d);
            this.Controls.Add(this.lblOrigPrice_d);
            this.Controls.Add(this.txtAdjustTo);
            this.Controls.Add(this.lblNewPrice);
            this.Controls.Add(this.lblAdjustTo);
            this.Controls.Add(this.lblOrigPrice);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmProductAdjust";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProductAdjust";
            this.Load += new System.EventHandler(this.frmProductAdjust_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmProductAdjust_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblOrigPrice;
        private System.Windows.Forms.Label lblAdjustTo;
        private System.Windows.Forms.Label lblNewPrice;
        private System.Windows.Forms.TextBox txtAdjustTo;
        private System.Windows.Forms.Label lblOrigPrice_d;
        private System.Windows.Forms.Label lblNewPrice_d;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnF2;
        private System.Windows.Forms.Button BtnF1;
        private System.Windows.Forms.Button BtnF4;
        private System.Windows.Forms.Button BtnF3;
        private System.Windows.Forms.Button BtnProceed;
        private System.Windows.Forms.Button BtnESC;
        private System.Windows.Forms.Button BtnF11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Label lblDiscount;
    }
}