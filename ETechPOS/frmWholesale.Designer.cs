namespace ETech
{
    partial class frmWholesale
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
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblQty = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblProduct_d = new System.Windows.Forms.Label();
            this.lblUnitPrice_d = new System.Windows.Forms.Label();
            this.lblUnitPrice = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnESC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtQty
            // 
            this.txtQty.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQty.Location = new System.Drawing.Point(31, 188);
            this.txtQty.MaxLength = 10;
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(125, 48);
            this.txtQty.TabIndex = 1;
            this.txtQty.Tag = "num";
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            // 
            // txtPrice
            // 
            this.txtPrice.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrice.Location = new System.Drawing.Point(189, 188);
            this.txtPrice.MaxLength = 20;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(216, 48);
            this.txtPrice.TabIndex = 2;
            this.txtPrice.Tag = "num";
            this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrice_KeyPress);
            // 
            // lblQty
            // 
            this.lblQty.Font = new System.Drawing.Font("Arial Narrow", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.Location = new System.Drawing.Point(31, 134);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(125, 51);
            this.lblQty.TabIndex = 12;
            this.lblQty.Text = "Qty";
            this.lblQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPrice
            // 
            this.lblPrice.Font = new System.Drawing.Font("Arial Narrow", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.Location = new System.Drawing.Point(189, 134);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(216, 51);
            this.lblPrice.TabIndex = 11;
            this.lblPrice.Text = "Price";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProduct
            // 
            this.lblProduct.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.Location = new System.Drawing.Point(12, 9);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(136, 29);
            this.lblProduct.TabIndex = 17;
            this.lblProduct.Text = "Description:";
            // 
            // lblProduct_d
            // 
            this.lblProduct_d.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct_d.Location = new System.Drawing.Point(154, 9);
            this.lblProduct_d.Name = "lblProduct_d";
            this.lblProduct_d.Size = new System.Drawing.Size(271, 78);
            this.lblProduct_d.TabIndex = 18;
            this.lblProduct_d.Text = "Product Name";
            // 
            // lblUnitPrice_d
            // 
            this.lblUnitPrice_d.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnitPrice_d.Location = new System.Drawing.Point(159, 96);
            this.lblUnitPrice_d.Name = "lblUnitPrice_d";
            this.lblUnitPrice_d.Size = new System.Drawing.Size(266, 29);
            this.lblUnitPrice_d.TabIndex = 20;
            this.lblUnitPrice_d.Text = "0.00";
            // 
            // lblUnitPrice
            // 
            this.lblUnitPrice.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnitPrice.Location = new System.Drawing.Point(12, 96);
            this.lblUnitPrice.Name = "lblUnitPrice";
            this.lblUnitPrice.Size = new System.Drawing.Size(141, 29);
            this.lblUnitPrice.TabIndex = 19;
            this.lblUnitPrice.Text = "Unit Price:";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(86, 242);
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
            this.btnESC.Location = new System.Drawing.Point(212, 242);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(139, 38);
            this.btnESC.TabIndex = 105;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // frmWholesale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 288);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.lblUnitPrice_d);
            this.Controls.Add(this.lblUnitPrice);
            this.Controls.Add(this.lblProduct_d);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.lblPrice);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmWholesale";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wholesale";
            this.Load += new System.EventHandler(this.frmWholesale_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmWholesale_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblProduct_d;
        private System.Windows.Forms.Label lblUnitPrice_d;
        private System.Windows.Forms.Label lblUnitPrice;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnESC;
    }
}