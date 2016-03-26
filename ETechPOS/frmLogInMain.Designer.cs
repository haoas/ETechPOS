namespace ETech
{
    partial class frmLogInMain
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
            this.components = new System.ComponentModel.Container();
            this.LBL_Username = new System.Windows.Forms.Label();
            this.LBL_Password = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.tmrConnecting = new System.Windows.Forms.Timer(this.components);
            this.lblServerDateTime = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.bgwConnecting = new System.ComponentModel.BackgroundWorker();
            this.lbl_Terminalno = new System.Windows.Forms.Label();
            this.lbl_BranchCode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LBL_Username
            // 
            this.LBL_Username.AutoSize = true;
            this.LBL_Username.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_Username.Location = new System.Drawing.Point(20, 115);
            this.LBL_Username.Name = "LBL_Username";
            this.LBL_Username.Size = new System.Drawing.Size(102, 22);
            this.LBL_Username.TabIndex = 1;
            this.LBL_Username.Text = "Username:";
            // 
            // LBL_Password
            // 
            this.LBL_Password.AutoSize = true;
            this.LBL_Password.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_Password.Location = new System.Drawing.Point(20, 152);
            this.LBL_Password.Name = "LBL_Password";
            this.LBL_Password.Size = new System.Drawing.Size(99, 22);
            this.LBL_Password.TabIndex = 2;
            this.LBL_Password.Text = "Password:";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(125, 112);
            this.txtUsername.MaxLength = 30;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(297, 29);
            this.txtUsername.TabIndex = 4;
            this.txtUsername.Tag = "";
            this.txtUsername.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUsername.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUsername_KeyPress);
            this.txtUsername.Enter += new System.EventHandler(this.txtUsername_Enter);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(125, 149);
            this.txtPassword.MaxLength = 30;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(297, 29);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.Tag = "";
            this.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            this.txtPassword.Enter += new System.EventHandler(this.txtPassword_Enter);
            // 
            // tmrConnecting
            // 
            this.tmrConnecting.Interval = 1000;
            this.tmrConnecting.Tick += new System.EventHandler(this.tmrConnecting_Tick);
            // 
            // lblServerDateTime
            // 
            this.lblServerDateTime.AutoSize = true;
            this.lblServerDateTime.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerDateTime.ForeColor = System.Drawing.Color.Black;
            this.lblServerDateTime.Location = new System.Drawing.Point(16, 71);
            this.lblServerDateTime.Name = "lblServerDateTime";
            this.lblServerDateTime.Size = new System.Drawing.Size(368, 27);
            this.lblServerDateTime.TabIndex = 12;
            this.lblServerDateTime.Text = "MMM dd, yyyy hh:mm tt, wwwww";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnCancel.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancel.Location = new System.Drawing.Point(21, 184);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(159, 60);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // btnLogIn
            // 
            this.btnLogIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnLogIn.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogIn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLogIn.Location = new System.Drawing.Point(263, 184);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(159, 60);
            this.btnLogIn.TabIndex = 31;
            this.btnLogIn.Text = "Log In";
            this.btnLogIn.UseVisualStyleBackColor = false;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // bgwConnecting
            // 
            this.bgwConnecting.WorkerReportsProgress = true;
            this.bgwConnecting.WorkerSupportsCancellation = true;
            this.bgwConnecting.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwConnecting_DoWork);
            this.bgwConnecting.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwConnecting_RunWorkerCompleted);
            // 
            // lbl_Terminalno
            // 
            this.lbl_Terminalno.AutoSize = true;
            this.lbl_Terminalno.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Terminalno.ForeColor = System.Drawing.Color.Black;
            this.lbl_Terminalno.Location = new System.Drawing.Point(16, 42);
            this.lbl_Terminalno.Name = "lbl_Terminalno";
            this.lbl_Terminalno.Size = new System.Drawing.Size(164, 29);
            this.lbl_Terminalno.TabIndex = 33;
            this.lbl_Terminalno.Text = "Terminal#: 00";
            // 
            // lbl_BranchCode
            // 
            this.lbl_BranchCode.AutoSize = true;
            this.lbl_BranchCode.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BranchCode.ForeColor = System.Drawing.Color.Black;
            this.lbl_BranchCode.Location = new System.Drawing.Point(16, 13);
            this.lbl_BranchCode.Name = "lbl_BranchCode";
            this.lbl_BranchCode.Size = new System.Drawing.Size(314, 29);
            this.lbl_BranchCode.TabIndex = 34;
            this.lbl_BranchCode.Text = "Branch: 1000-Main Branch";
            // 
            // frmLogInMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 256);
            this.Controls.Add(this.lbl_BranchCode);
            this.Controls.Add(this.lbl_Terminalno);
            this.Controls.Add(this.btnLogIn);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblServerDateTime);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.LBL_Password);
            this.Controls.Add(this.LBL_Username);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmLogInMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log In";
            this.Load += new System.EventHandler(this.frmLogInMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLogInMain_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LBL_Username;
        private System.Windows.Forms.Label LBL_Password;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Timer tmrConnecting;
        private System.Windows.Forms.Label lblServerDateTime;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnLogIn;
        private System.ComponentModel.BackgroundWorker bgwConnecting;
        private System.Windows.Forms.Label lbl_Terminalno;
        private System.Windows.Forms.Label lbl_BranchCode;
    }
}