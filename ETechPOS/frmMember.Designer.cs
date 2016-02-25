namespace ETech
{
    partial class frmMember
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
            this.lblCardID = new System.Windows.Forms.Label();
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnESC = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lblPoints = new System.Windows.Forms.Label();
            this.lblMemberType = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCardID
            // 
            this.lblCardID.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardID.ForeColor = System.Drawing.Color.Black;
            this.lblCardID.Location = new System.Drawing.Point(9, 9);
            this.lblCardID.Name = "lblCardID";
            this.lblCardID.Size = new System.Drawing.Size(600, 55);
            this.lblCardID.TabIndex = 4;
            this.lblCardID.Text = "Card ID / Barcode:";
            this.lblCardID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCardID
            // 
            this.txtCardID.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCardID.Location = new System.Drawing.Point(10, 67);
            this.txtCardID.MaxLength = 16;
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Size = new System.Drawing.Size(604, 63);
            this.txtCardID.TabIndex = 25;
            this.txtCardID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCardID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCardID_KeyPress);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(187, 338);
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
            this.btnESC.Location = new System.Drawing.Point(313, 338);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(139, 38);
            this.btnESC.TabIndex = 105;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(19, 338);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(162, 38);
            this.btnRemove.TabIndex = 107;
            this.btnRemove.Text = "Remove (F7)";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lblPoints
            // 
            this.lblPoints.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblPoints.ForeColor = System.Drawing.Color.Maroon;
            this.lblPoints.Location = new System.Drawing.Point(12, 266);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(602, 55);
            this.lblPoints.TabIndex = 29;
            this.lblPoints.Text = "0.00";
            this.lblPoints.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMemberType
            // 
            this.lblMemberType.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblMemberType.ForeColor = System.Drawing.Color.Black;
            this.lblMemberType.Location = new System.Drawing.Point(12, 217);
            this.lblMemberType.Name = "lblMemberType";
            this.lblMemberType.Size = new System.Drawing.Size(602, 42);
            this.lblMemberType.TabIndex = 30;
            this.lblMemberType.Text = "Type";
            this.lblMemberType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.Location = new System.Drawing.Point(12, 142);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(602, 68);
            this.lblName.TabIndex = 28;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 388);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.lblMemberType);
            this.Controls.Add(this.lblPoints);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtCardID);
            this.Controls.Add(this.lblCardID);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmMember";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Member";
            this.Load += new System.EventHandler(this.frmMember_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMember_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCardID;
        private System.Windows.Forms.TextBox txtCardID;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnESC;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.Label lblMemberType;
        private System.Windows.Forms.Label lblName;
    }
}