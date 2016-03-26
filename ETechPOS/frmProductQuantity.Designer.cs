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
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_Proceed = new System.Windows.Forms.Button();
            this.Btn_ESC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblProductName
            // 
            this.lblProductName.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.ForeColor = System.Drawing.Color.Black;
            this.lblProductName.Location = new System.Drawing.Point(12, 58);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(384, 79);
            this.lblProductName.TabIndex = 0;
            this.lblProductName.Text = "Product Name";
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOldQty
            // 
            this.lblOldQty.AutoSize = true;
            this.lblOldQty.Font = new System.Drawing.Font("Arial", 25F);
            this.lblOldQty.Location = new System.Drawing.Point(124, 9);
            this.lblOldQty.Name = "lblOldQty";
            this.lblOldQty.Size = new System.Drawing.Size(267, 39);
            this.lblOldQty.TabIndex = 1;
            this.lblOldQty.Text = "Change Quantity";
            // 
            // lblNewQty
            // 
            this.lblNewQty.AutoSize = true;
            this.lblNewQty.Font = new System.Drawing.Font("Arial", 25F);
            this.lblNewQty.Location = new System.Drawing.Point(62, 205);
            this.lblNewQty.Name = "lblNewQty";
            this.lblNewQty.Size = new System.Drawing.Size(61, 39);
            this.lblNewQty.TabIndex = 2;
            this.lblNewQty.Text = "To:";
            // 
            // lblOldQty_d
            // 
            this.lblOldQty_d.Font = new System.Drawing.Font("Arial", 25F);
            this.lblOldQty_d.Location = new System.Drawing.Point(171, 147);
            this.lblOldQty_d.Name = "lblOldQty_d";
            this.lblOldQty_d.Size = new System.Drawing.Size(179, 46);
            this.lblOldQty_d.TabIndex = 3;
            this.lblOldQty_d.Text = "0";
            this.lblOldQty_d.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNewQty_d
            // 
            this.txtNewQty_d.Font = new System.Drawing.Font("Arial", 25F);
            this.txtNewQty_d.Location = new System.Drawing.Point(171, 202);
            this.txtNewQty_d.MaxLength = 10;
            this.txtNewQty_d.Name = "txtNewQty_d";
            this.txtNewQty_d.Size = new System.Drawing.Size(179, 46);
            this.txtNewQty_d.TabIndex = 4;
            this.txtNewQty_d.Tag = "num";
            this.txtNewQty_d.Text = "0";
            this.txtNewQty_d.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 25F);
            this.label1.Location = new System.Drawing.Point(62, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 39);
            this.label1.TabIndex = 35;
            this.label1.Text = "From:";
            // 
            // Btn_Proceed
            // 
            this.Btn_Proceed.BackColor = System.Drawing.Color.White;
            this.Btn_Proceed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Proceed.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Proceed.ForeColor = System.Drawing.Color.Black;
            this.Btn_Proceed.Location = new System.Drawing.Point(402, 166);
            this.Btn_Proceed.Name = "Btn_Proceed";
            this.Btn_Proceed.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.Btn_Proceed.Size = new System.Drawing.Size(100, 80);
            this.Btn_Proceed.TabIndex = 111;
            this.Btn_Proceed.Text = "[ F12 ]\r\nPROCEED";
            this.Btn_Proceed.UseVisualStyleBackColor = false;
            this.Btn_Proceed.Click += new System.EventHandler(this.Btn_Proceed_Click);
            // 
            // Btn_ESC
            // 
            this.Btn_ESC.BackColor = System.Drawing.Color.White;
            this.Btn_ESC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_ESC.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_ESC.ForeColor = System.Drawing.Color.Black;
            this.Btn_ESC.Location = new System.Drawing.Point(402, 80);
            this.Btn_ESC.Name = "Btn_ESC";
            this.Btn_ESC.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.Btn_ESC.Size = new System.Drawing.Size(100, 80);
            this.Btn_ESC.TabIndex = 110;
            this.Btn_ESC.Text = "[ ESC ]\r\nCANCEL";
            this.Btn_ESC.UseVisualStyleBackColor = false;
            this.Btn_ESC.Click += new System.EventHandler(this.Btn_ESC_Click);
            // 
            // frmProductQuantity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 263);
            this.Controls.Add(this.Btn_Proceed);
            this.Controls.Add(this.Btn_ESC);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_Proceed;
        private System.Windows.Forms.Button Btn_ESC;
    }
}