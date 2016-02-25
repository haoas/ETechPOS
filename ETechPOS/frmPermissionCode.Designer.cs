namespace ETech
{
    partial class frmPermissionCode
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
            this.lblPermissionCode = new System.Windows.Forms.Label();
            this.txtPermissionCode = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnESC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPermissionCode
            // 
            this.lblPermissionCode.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPermissionCode.Location = new System.Drawing.Point(22, 9);
            this.lblPermissionCode.Name = "lblPermissionCode";
            this.lblPermissionCode.Size = new System.Drawing.Size(290, 51);
            this.lblPermissionCode.TabIndex = 1;
            this.lblPermissionCode.Text = "Permission Code";
            this.lblPermissionCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPermissionCode
            // 
            this.txtPermissionCode.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPermissionCode.Location = new System.Drawing.Point(12, 63);
            this.txtPermissionCode.MaxLength = 20;
            this.txtPermissionCode.Name = "txtPermissionCode";
            this.txtPermissionCode.Size = new System.Drawing.Size(309, 44);
            this.txtPermissionCode.TabIndex = 2;
            this.txtPermissionCode.UseSystemPasswordChar = true;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(34, 113);
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
            this.btnESC.Location = new System.Drawing.Point(160, 113);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(139, 38);
            this.btnESC.TabIndex = 105;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // frmPermissionCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 161);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.txtPermissionCode);
            this.Controls.Add(this.lblPermissionCode);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmPermissionCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Permission Code";
            this.Load += new System.EventHandler(this.frmPermissionCode_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPermissionCode_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPermissionCode;
        private System.Windows.Forms.TextBox txtPermissionCode;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnESC;
    }
}