namespace ETech
{
    partial class frmOpenItem
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
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_ESC = new System.Windows.Forms.Button();
            this.Btn_Proceed = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPrice
            // 
            this.lblPrice.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.Location = new System.Drawing.Point(13, 129);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(187, 51);
            this.lblPrice.TabIndex = 2;
            this.lblPrice.Text = "Price:";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblQty
            // 
            this.lblQty.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.Location = new System.Drawing.Point(13, 177);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(187, 51);
            this.lblQty.TabIndex = 3;
            this.lblQty.Text = "Quantity:";
            this.lblQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPrice
            // 
            this.txtPrice.Font = new System.Drawing.Font("Arial", 24.75F);
            this.txtPrice.Location = new System.Drawing.Point(206, 132);
            this.txtPrice.MaxLength = 20;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(270, 45);
            this.txtPrice.TabIndex = 2;
            this.txtPrice.Tag = "";
            this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrice_KeyPress);
            // 
            // txtQty
            // 
            this.txtQty.Font = new System.Drawing.Font("Arial", 24.75F);
            this.txtQty.Location = new System.Drawing.Point(337, 180);
            this.txtQty.MaxLength = 10;
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(139, 45);
            this.txtQty.TabIndex = 3;
            this.txtQty.Tag = "";
            this.txtQty.Text = "1";
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            // 
            // txtMemo
            // 
            this.txtMemo.Font = new System.Drawing.Font("Arial", 24.75F);
            this.txtMemo.Location = new System.Drawing.Point(206, 84);
            this.txtMemo.MaxLength = 20;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(270, 45);
            this.txtMemo.TabIndex = 1;
            this.txtMemo.Tag = "";
            this.txtMemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 51);
            this.label1.TabIndex = 106;
            this.label1.Text = "Memo:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Btn_ESC
            // 
            this.Btn_ESC.BackColor = System.Drawing.Color.White;
            this.Btn_ESC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_ESC.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_ESC.ForeColor = System.Drawing.Color.Black;
            this.Btn_ESC.Location = new System.Drawing.Point(487, 73);
            this.Btn_ESC.Name = "Btn_ESC";
            this.Btn_ESC.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.Btn_ESC.Size = new System.Drawing.Size(100, 80);
            this.Btn_ESC.TabIndex = 108;
            this.Btn_ESC.Text = "[ ESC ]\r\nCANCEL";
            this.Btn_ESC.UseVisualStyleBackColor = false;
            this.Btn_ESC.Click += new System.EventHandler(this.Btn_ESC_Click);
            // 
            // Btn_Proceed
            // 
            this.Btn_Proceed.BackColor = System.Drawing.Color.White;
            this.Btn_Proceed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Proceed.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Proceed.ForeColor = System.Drawing.Color.Black;
            this.Btn_Proceed.Location = new System.Drawing.Point(487, 159);
            this.Btn_Proceed.Name = "Btn_Proceed";
            this.Btn_Proceed.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.Btn_Proceed.Size = new System.Drawing.Size(100, 80);
            this.Btn_Proceed.TabIndex = 109;
            this.Btn_Proceed.Text = "[ F12 ]\r\nPROCEED";
            this.Btn_Proceed.UseVisualStyleBackColor = false;
            this.Btn_Proceed.Click += new System.EventHandler(this.Btn_Proceed_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(207, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 51);
            this.label2.TabIndex = 110;
            this.label2.Text = "Open Item";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmOpenItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 255);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Btn_Proceed);
            this.Controls.Add(this.Btn_ESC);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.lblPrice);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmOpenItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Open Item";
            this.Load += new System.EventHandler(this.frmOpenItem_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOpenItem_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_ESC;
        private System.Windows.Forms.Button Btn_Proceed;
        private System.Windows.Forms.Label label2;
    }
}