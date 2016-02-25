namespace ETechPOS
{
    partial class frmReprintReceipt_posd
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
            this.lblF3 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtORNumber_d = new System.Windows.Forms.TextBox();
            this.lblORNumber = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblF3
            // 
            this.lblF3.AutoSize = true;
            this.lblF3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblF3.Location = new System.Drawing.Point(12, 84);
            this.lblF3.Name = "lblF3";
            this.lblF3.Size = new System.Drawing.Size(131, 18);
            this.lblF3.TabIndex = 12;
            this.lblF3.Text = "Press \"F3\" to Exit";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Arial Narrow", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(240, 57);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(181, 45);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "F1 Print";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtORNumber_d
            // 
            this.txtORNumber_d.Font = new System.Drawing.Font("Arial Narrow", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtORNumber_d.Location = new System.Drawing.Point(98, 5);
            this.txtORNumber_d.Name = "txtORNumber_d";
            this.txtORNumber_d.Size = new System.Drawing.Size(323, 41);
            this.txtORNumber_d.TabIndex = 10;
            // 
            // lblORNumber
            // 
            this.lblORNumber.AutoSize = true;
            this.lblORNumber.Font = new System.Drawing.Font("Arial Narrow", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblORNumber.Location = new System.Drawing.Point(12, 9);
            this.lblORNumber.Name = "lblORNumber";
            this.lblORNumber.Size = new System.Drawing.Size(80, 37);
            this.lblORNumber.TabIndex = 9;
            this.lblORNumber.Text = "OR#:";
            // 
            // frmReprintReceipt_posd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 114);
            this.Controls.Add(this.lblF3);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtORNumber_d);
            this.Controls.Add(this.lblORNumber);
            this.KeyPreview = true;
            this.Name = "frmReprintReceipt_posd";
            this.Text = "Reprint Receipt";
            this.Load += new System.EventHandler(this.frmReprintReceipt_posd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmReprintReceipt_posd_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblF3;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtORNumber_d;
        private System.Windows.Forms.Label lblORNumber;
    }
}