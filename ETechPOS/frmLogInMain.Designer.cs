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
            this.lblusername = new System.Windows.Forms.Label();
            this.lblpassword = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblStatus_d = new System.Windows.Forms.Label();
            this.tmrConnecting = new System.Windows.Forms.Timer(this.components);
            this.lblServerDateTime = new System.Windows.Forms.Label();
            this.btnESC = new System.Windows.Forms.Button();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.lblBranchNameTerminalNumber = new System.Windows.Forms.Label();
            this.bgwConnecting = new System.ComponentModel.BackgroundWorker();
            this.lblApplicationVersion = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblusername
            // 
            this.lblusername.AutoSize = true;
            this.lblusername.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblusername.Location = new System.Drawing.Point(153, 87);
            this.lblusername.Name = "lblusername";
            this.lblusername.Size = new System.Drawing.Size(102, 22);
            this.lblusername.TabIndex = 1;
            this.lblusername.Text = "Username:";
            // 
            // lblpassword
            // 
            this.lblpassword.AutoSize = true;
            this.lblpassword.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpassword.Location = new System.Drawing.Point(153, 127);
            this.lblpassword.Name = "lblpassword";
            this.lblpassword.Size = new System.Drawing.Size(99, 22);
            this.lblpassword.TabIndex = 2;
            this.lblpassword.Text = "Password:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(153, 160);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(68, 22);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Status:";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(253, 80);
            this.txtUsername.MaxLength = 30;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(156, 29);
            this.txtUsername.TabIndex = 4;
            this.txtUsername.Tag = "";
            this.txtUsername.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUsername_KeyPress);
            this.txtUsername.Enter += new System.EventHandler(this.txtUsername_Enter);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(253, 120);
            this.txtPassword.MaxLength = 30;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(156, 29);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.Tag = "";
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            this.txtPassword.Enter += new System.EventHandler(this.txtPassword_Enter);
            // 
            // lblStatus_d
            // 
            this.lblStatus_d.AutoSize = true;
            this.lblStatus_d.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus_d.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblStatus_d.Location = new System.Drawing.Point(249, 160);
            this.lblStatus_d.Name = "lblStatus_d";
            this.lblStatus_d.Size = new System.Drawing.Size(64, 22);
            this.lblStatus_d.TabIndex = 6;
            this.lblStatus_d.Text = "Online";
            // 
            // tmrConnecting
            // 
            this.tmrConnecting.Enabled = true;
            this.tmrConnecting.Interval = 1000;
            this.tmrConnecting.Tick += new System.EventHandler(this.tmrConnecting_Tick);
            // 
            // lblServerDateTime
            // 
            this.lblServerDateTime.AutoSize = true;
            this.lblServerDateTime.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerDateTime.ForeColor = System.Drawing.Color.Black;
            this.lblServerDateTime.Location = new System.Drawing.Point(11, 9);
            this.lblServerDateTime.Name = "lblServerDateTime";
            this.lblServerDateTime.Size = new System.Drawing.Size(368, 27);
            this.lblServerDateTime.TabIndex = 12;
            this.lblServerDateTime.Text = "MMM dd, yyyy hh:mm tt, wwwww";
            // 
            // btnESC
            // 
            this.btnESC.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESC.Location = new System.Drawing.Point(289, 194);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(120, 38);
            this.btnESC.TabIndex = 32;
            this.btnESC.Text = "Exit (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // btnLogIn
            // 
            this.btnLogIn.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogIn.Location = new System.Drawing.Point(157, 194);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(120, 38);
            this.btnLogIn.TabIndex = 31;
            this.btnLogIn.Text = "Log In (F1)";
            this.btnLogIn.UseVisualStyleBackColor = false;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // lblBranchNameTerminalNumber
            // 
            this.lblBranchNameTerminalNumber.AutoSize = true;
            this.lblBranchNameTerminalNumber.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBranchNameTerminalNumber.ForeColor = System.Drawing.Color.Black;
            this.lblBranchNameTerminalNumber.Location = new System.Drawing.Point(12, 38);
            this.lblBranchNameTerminalNumber.Name = "lblBranchNameTerminalNumber";
            this.lblBranchNameTerminalNumber.Size = new System.Drawing.Size(186, 29);
            this.lblBranchNameTerminalNumber.TabIndex = 13;
            this.lblBranchNameTerminalNumber.Text = "BRANCH / POS";
            // 
            // bgwConnecting
            // 
            this.bgwConnecting.WorkerReportsProgress = true;
            this.bgwConnecting.WorkerSupportsCancellation = true;
            this.bgwConnecting.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwConnecting_DoWork);
            this.bgwConnecting.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwConnecting_RunWorkerCompleted);
            // 
            // lblApplicationVersion
            // 
            this.lblApplicationVersion.AutoSize = true;
            this.lblApplicationVersion.Location = new System.Drawing.Point(363, 167);
            this.lblApplicationVersion.Name = "lblApplicationVersion";
            this.lblApplicationVersion.Size = new System.Drawing.Size(46, 13);
            this.lblApplicationVersion.TabIndex = 35;
            this.lblApplicationVersion.Text = "v4.3.0.0";
            this.lblApplicationVersion.Visible = false;
            // 
            // pbLogo
            // 
            this.pbLogo.BackColor = System.Drawing.Color.Transparent;
            this.pbLogo.Location = new System.Drawing.Point(17, 87);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(131, 102);
            this.pbLogo.TabIndex = 34;
            this.pbLogo.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 218);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "ab";
            // 
            // frmLogInMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 244);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblApplicationVersion);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.btnLogIn);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.lblBranchNameTerminalNumber);
            this.Controls.Add(this.lblServerDateTime);
            this.Controls.Add(this.lblStatus_d);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblpassword);
            this.Controls.Add(this.lblusername);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmLogInMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Log In";
            this.Load += new System.EventHandler(this.frmLogInMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLogInMain_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblusername;
        private System.Windows.Forms.Label lblpassword;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblStatus_d;
        private System.Windows.Forms.Timer tmrConnecting;
        private System.Windows.Forms.Label lblServerDateTime;
        private System.Windows.Forms.Button btnESC;
        private System.Windows.Forms.Button btnLogIn;
        private System.Windows.Forms.Label lblBranchNameTerminalNumber;
        private System.ComponentModel.BackgroundWorker bgwConnecting;
        private System.Windows.Forms.Label lblApplicationVersion;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label label1;
    }
}