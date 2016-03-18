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
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblNewPrice = new System.Windows.Forms.Label();
            this.txtAdjustTo = new System.Windows.Forms.TextBox();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.lblOrigPrice_d = new System.Windows.Forms.Label();
            this.lblNewPrice_d = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCustomDiscount = new System.Windows.Forms.Label();
            this.btnPriceA = new System.Windows.Forms.Button();
            this.btnPriceB = new System.Windows.Forms.Button();
            this.btnPriceC = new System.Windows.Forms.Button();
            this.btnPriceD = new System.Windows.Forms.Button();
            this.btnPriceE = new System.Windows.Forms.Button();
            this.btnCustom = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnESC = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblProductName
            // 
            this.lblProductName.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblProductName.Location = new System.Drawing.Point(4, 9);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(475, 114);
            this.lblProductName.TabIndex = 3;
            this.lblProductName.Text = "Product Name";
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOrigPrice
            // 
            this.lblOrigPrice.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrigPrice.Location = new System.Drawing.Point(4, 114);
            this.lblOrigPrice.Name = "lblOrigPrice";
            this.lblOrigPrice.Size = new System.Drawing.Size(185, 51);
            this.lblOrigPrice.TabIndex = 3;
            this.lblOrigPrice.Text = "Orig. Price:";
            this.lblOrigPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAdjustTo
            // 
            this.lblAdjustTo.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdjustTo.Location = new System.Drawing.Point(5, 174);
            this.lblAdjustTo.Name = "lblAdjustTo";
            this.lblAdjustTo.Size = new System.Drawing.Size(159, 51);
            this.lblAdjustTo.TabIndex = 4;
            this.lblAdjustTo.Text = "Adjust to:";
            this.lblAdjustTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDiscount
            // 
            this.lblDiscount.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscount.Location = new System.Drawing.Point(5, 234);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(159, 51);
            this.lblDiscount.TabIndex = 5;
            this.lblDiscount.Text = "Discount:";
            this.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNewPrice
            // 
            this.lblNewPrice.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblNewPrice.Location = new System.Drawing.Point(5, 300);
            this.lblNewPrice.Name = "lblNewPrice";
            this.lblNewPrice.Size = new System.Drawing.Size(272, 51);
            this.lblNewPrice.TabIndex = 0;
            this.lblNewPrice.Text = "Adjusted Price:";
            this.lblNewPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAdjustTo
            // 
            this.txtAdjustTo.Font = new System.Drawing.Font("Arial", 24F);
            this.txtAdjustTo.Location = new System.Drawing.Point(276, 180);
            this.txtAdjustTo.MaxLength = 20;
            this.txtAdjustTo.Name = "txtAdjustTo";
            this.txtAdjustTo.Size = new System.Drawing.Size(203, 44);
            this.txtAdjustTo.TabIndex = 0;
            this.txtAdjustTo.Tag = "num";
            this.txtAdjustTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAdjustTo.TextChanged += new System.EventHandler(this.txtAdjustTo_TextChanged);
            this.txtAdjustTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAdjustTo_KeyPress);
            // 
            // txtDiscount
            // 
            this.txtDiscount.Font = new System.Drawing.Font("Arial", 24F);
            this.txtDiscount.Location = new System.Drawing.Point(276, 234);
            this.txtDiscount.MaxLength = 10;
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(151, 44);
            this.txtDiscount.TabIndex = 1;
            this.txtDiscount.Tag = "num";
            this.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiscount.TextChanged += new System.EventHandler(this.txtDiscount_TextChanged);
            this.txtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDiscount_KeyPress);
            // 
            // lblOrigPrice_d
            // 
            this.lblOrigPrice_d.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrigPrice_d.Location = new System.Drawing.Point(276, 114);
            this.lblOrigPrice_d.Name = "lblOrigPrice_d";
            this.lblOrigPrice_d.Size = new System.Drawing.Size(203, 51);
            this.lblOrigPrice_d.TabIndex = 0;
            this.lblOrigPrice_d.Text = "0.00";
            this.lblOrigPrice_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNewPrice_d
            // 
            this.lblNewPrice_d.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewPrice_d.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblNewPrice_d.Location = new System.Drawing.Point(276, 300);
            this.lblNewPrice_d.Name = "lblNewPrice_d";
            this.lblNewPrice_d.Size = new System.Drawing.Size(203, 51);
            this.lblNewPrice_d.TabIndex = 0;
            this.lblNewPrice_d.Text = "0.00";
            this.lblNewPrice_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 24F);
            this.label2.Location = new System.Drawing.Point(439, 228);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 51);
            this.label2.TabIndex = 14;
            this.label2.Text = "%";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCustomDiscount
            // 
            this.lblCustomDiscount.AutoSize = true;
            this.lblCustomDiscount.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomDiscount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCustomDiscount.Location = new System.Drawing.Point(174, 259);
            this.lblCustomDiscount.Name = "lblCustomDiscount";
            this.lblCustomDiscount.Size = new System.Drawing.Size(103, 15);
            this.lblCustomDiscount.TabIndex = 34;
            this.lblCustomDiscount.Text = "Regular Discount";
            this.lblCustomDiscount.Visible = false;
            // 
            // btnPriceA
            // 
            this.btnPriceA.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPriceA.Location = new System.Drawing.Point(498, 33);
            this.btnPriceA.Name = "btnPriceA";
            this.btnPriceA.Size = new System.Drawing.Size(115, 75);
            this.btnPriceA.TabIndex = 104;
            this.btnPriceA.Text = "Price A (F6)";
            this.btnPriceA.UseVisualStyleBackColor = false;
            this.btnPriceA.Click += new System.EventHandler(this.btnPriceA_Click);
            // 
            // btnPriceB
            // 
            this.btnPriceB.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPriceB.Location = new System.Drawing.Point(498, 114);
            this.btnPriceB.Name = "btnPriceB";
            this.btnPriceB.Size = new System.Drawing.Size(115, 75);
            this.btnPriceB.TabIndex = 105;
            this.btnPriceB.Text = "Price B (F7)";
            this.btnPriceB.UseVisualStyleBackColor = false;
            this.btnPriceB.Click += new System.EventHandler(this.btnPriceB_Click);
            // 
            // btnPriceC
            // 
            this.btnPriceC.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPriceC.Location = new System.Drawing.Point(498, 195);
            this.btnPriceC.Name = "btnPriceC";
            this.btnPriceC.Size = new System.Drawing.Size(115, 75);
            this.btnPriceC.TabIndex = 106;
            this.btnPriceC.Text = "Price C (F8)";
            this.btnPriceC.UseVisualStyleBackColor = false;
            this.btnPriceC.Click += new System.EventHandler(this.btnPriceC_Click);
            // 
            // btnPriceD
            // 
            this.btnPriceD.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPriceD.Location = new System.Drawing.Point(498, 276);
            this.btnPriceD.Name = "btnPriceD";
            this.btnPriceD.Size = new System.Drawing.Size(115, 75);
            this.btnPriceD.TabIndex = 107;
            this.btnPriceD.Text = "Price D (F9)";
            this.btnPriceD.UseVisualStyleBackColor = false;
            this.btnPriceD.Click += new System.EventHandler(this.btnPriceD_Click);
            // 
            // btnPriceE
            // 
            this.btnPriceE.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPriceE.Location = new System.Drawing.Point(498, 357);
            this.btnPriceE.Name = "btnPriceE";
            this.btnPriceE.Size = new System.Drawing.Size(115, 75);
            this.btnPriceE.TabIndex = 108;
            this.btnPriceE.Text = "Price E (F10)";
            this.btnPriceE.UseVisualStyleBackColor = false;
            this.btnPriceE.Click += new System.EventHandler(this.btnPriceE_Click);
            // 
            // btnCustom
            // 
            this.btnCustom.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustom.Location = new System.Drawing.Point(1, 360);
            this.btnCustom.Name = "btnCustom";
            this.btnCustom.Size = new System.Drawing.Size(115, 75);
            this.btnCustom.TabIndex = 100;
            this.btnCustom.Text = "Custom (F11)";
            this.btnCustom.UseVisualStyleBackColor = false;
            this.btnCustom.Visible = false;
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(122, 360);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(115, 75);
            this.btnRemove.TabIndex = 101;
            this.btnRemove.Text = "Remove (F12)";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Visible = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnESC
            // 
            this.btnESC.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESC.Location = new System.Drawing.Point(364, 360);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(115, 75);
            this.btnESC.TabIndex = 103;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Visible = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(243, 360);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(115, 75);
            this.btnOK.TabIndex = 102;
            this.btnOK.Text = "Okay (F1)";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Visible = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmProductAdjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(625, 356);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnPriceE);
            this.Controls.Add(this.btnCustom);
            this.Controls.Add(this.btnPriceC);
            this.Controls.Add(this.btnPriceD);
            this.Controls.Add(this.btnPriceA);
            this.Controls.Add(this.btnPriceB);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.lblCustomDiscount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNewPrice_d);
            this.Controls.Add(this.lblOrigPrice_d);
            this.Controls.Add(this.txtDiscount);
            this.Controls.Add(this.txtAdjustTo);
            this.Controls.Add(this.lblNewPrice);
            this.Controls.Add(this.lblDiscount);
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
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblNewPrice;
        private System.Windows.Forms.TextBox txtAdjustTo;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Label lblOrigPrice_d;
        private System.Windows.Forms.Label lblNewPrice_d;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCustomDiscount;
        private System.Windows.Forms.Button btnPriceA;
        private System.Windows.Forms.Button btnPriceB;
        private System.Windows.Forms.Button btnPriceC;
        private System.Windows.Forms.Button btnPriceD;
        private System.Windows.Forms.Button btnPriceE;
        private System.Windows.Forms.Button btnCustom;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnESC;
        private System.Windows.Forms.Button btnOK;
    }
}