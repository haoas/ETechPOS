namespace ETech
{
    partial class frmProductQuantity
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
            this.lblOldQty = new System.Windows.Forms.Label();
            this.lblNewQty = new System.Windows.Forms.Label();
            this.lblOldQty_d = new System.Windows.Forms.Label();
            this.txtNewQty_d = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnESC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblProductName
            // 
            this.lblProductName.Font = new System.Drawing.Font("Arial", 25F);
            this.lblProductName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblProductName.Location = new System.Drawing.Point(3, 9);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(427, 79);
            this.lblProductName.TabIndex = 0;
            this.lblProductName.Text = "Product Name";
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOldQty
            // 
            this.lblOldQty.AutoSize = true;
            this.lblOldQty.Font = new System.Drawing.Font("Arial", 25F);
            this.lblOldQty.Location = new System.Drawing.Point(3, 99);
            this.lblOldQty.Name = "lblOldQty";
            this.lblOldQty.Size = new System.Drawing.Size(201, 39);
            this.lblOldQty.TabIndex = 1;
            this.lblOldQty.Text = "Old Quantity";
            // 
            // lblNewQty
            // 
            this.lblNewQty.AutoSize = true;
            this.lblNewQty.Font = new System.Drawing.Font("Arial", 25F);
            this.lblNewQty.Location = new System.Drawing.Point(210, 99);
            this.lblNewQty.Name = "lblNewQty";
            this.lblNewQty.Size = new System.Drawing.Size(216, 39);
            this.lblNewQty.TabIndex = 2;
            this.lblNewQty.Text = "New Quantity";
            // 
            // lblOldQty_d
            // 
            this.lblOldQty_d.AutoSize = true;
            this.lblOldQty_d.Font = new System.Drawing.Font("Arial", 25F);
            this.lblOldQty_d.Location = new System.Drawing.Point(93, 148);
            this.lblOldQty_d.Name = "lblOldQty_d";
            this.lblOldQty_d.Size = new System.Drawing.Size(36, 39);
            this.lblOldQty_d.TabIndex = 3;
            this.lblOldQty_d.Text = "0";
            // 
            // txtNewQty_d
            // 
            this.txtNewQty_d.Font = new System.Drawing.Font("Arial", 25F);
            this.txtNewQty_d.Location = new System.Drawing.Point(273, 145);
            this.txtNewQty_d.MaxLength = 10;
            this.txtNewQty_d.Name = "txtNewQty_d";
            this.txtNewQty_d.Size = new System.Drawing.Size(100, 46);
            this.txtNewQty_d.TabIndex = 4;
            this.txtNewQty_d.Tag = "num";
            this.txtNewQty_d.Text = "0";
            this.txtNewQty_d.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNewQty_d.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNewQty_d_KeyPress);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(80, 197);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 38);
            this.btnOK.TabIndex = 33;
            this.btnOK.Text = "Okay (F1)";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnESC
            // 
            this.btnESC.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESC.Location = new System.Drawing.Point(206, 197);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(139, 38);
            this.btnESC.TabIndex = 34;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // frmProductQuantity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 240);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.txtNewQty_d);
            this.Controls.Add(this.lblOldQty_d);
            this.Controls.Add(this.lblNewQty);
            this.Controls.Add(this.lblOldQty);
            this.Controls.Add(this.lblProductName);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmProductQuantity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Product Quantity";
            this.Load += new System.EventHandler(this.frmProductQuantity_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmProductQuantity_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblOldQty;
        private System.Windows.Forms.Label lblNewQty;
        private System.Windows.Forms.Label lblOldQty_d;
        private System.Windows.Forms.TextBox txtNewQty_d;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnESC;
    }
}