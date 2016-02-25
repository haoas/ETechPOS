namespace ETech
{
    partial class frmReprintReceipt
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
            this.txtORNumber_d = new System.Windows.Forms.TextBox();
            this.lblORNumber = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnESC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtORNumber_d
            // 
            this.txtORNumber_d.Font = new System.Drawing.Font("Arial Narrow", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtORNumber_d.Location = new System.Drawing.Point(112, 10);
            this.txtORNumber_d.MaxLength = 20;
            this.txtORNumber_d.Name = "txtORNumber_d";
            this.txtORNumber_d.Size = new System.Drawing.Size(309, 41);
            this.txtORNumber_d.TabIndex = 6;
            this.txtORNumber_d.Tag = "num";
            this.txtORNumber_d.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtORNumber_d_KeyPress);
            // 
            // lblORNumber
            // 
            this.lblORNumber.AutoSize = true;
            this.lblORNumber.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblORNumber.Location = new System.Drawing.Point(12, 9);
            this.lblORNumber.Name = "lblORNumber";
            this.lblORNumber.Size = new System.Drawing.Size(94, 38);
            this.lblORNumber.TabIndex = 5;
            this.lblORNumber.Text = "OR#:";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(156, 57);
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
            this.btnESC.Location = new System.Drawing.Point(282, 57);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(139, 38);
            this.btnESC.TabIndex = 105;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // frmReprintReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 105);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.txtORNumber_d);
            this.Controls.Add(this.lblORNumber);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmReprintReceipt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reprint Receipt_";
            this.Load += new System.EventHandler(this.frmReprintReceipt_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmReprintReceipt_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblORNumber;
        public System.Windows.Forms.TextBox txtORNumber_d;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnESC;
    }
}