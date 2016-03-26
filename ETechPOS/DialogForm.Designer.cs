namespace ETech
{
    partial class DialogForm
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
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnCommand2 = new System.Windows.Forms.Button();
            this.btnCommand3 = new System.Windows.Forms.Button();
            this.btnCommand1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(12, 9);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(428, 215);
            this.lblMessage.TabIndex = 21;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCommand2
            // 
            this.btnCommand2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCommand2.Location = new System.Drawing.Point(156, 227);
            this.btnCommand2.Name = "btnCommand2";
            this.btnCommand2.Size = new System.Drawing.Size(139, 38);
            this.btnCommand2.TabIndex = 1;
            this.btnCommand2.Text = "Command2";
            this.btnCommand2.UseVisualStyleBackColor = false;
            this.btnCommand2.Visible = false;
            this.btnCommand2.Click += new System.EventHandler(this.btnCommand2_Click);
            // 
            // btnCommand3
            // 
            this.btnCommand3.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCommand3.Location = new System.Drawing.Point(11, 227);
            this.btnCommand3.Name = "btnCommand3";
            this.btnCommand3.Size = new System.Drawing.Size(139, 38);
            this.btnCommand3.TabIndex = 2;
            this.btnCommand3.Text = "Command3";
            this.btnCommand3.UseVisualStyleBackColor = false;
            this.btnCommand3.Visible = false;
            this.btnCommand3.Click += new System.EventHandler(this.btnCommand3_Click);
            // 
            // btnCommand1
            // 
            this.btnCommand1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCommand1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCommand1.Location = new System.Drawing.Point(301, 227);
            this.btnCommand1.Name = "btnCommand1";
            this.btnCommand1.Size = new System.Drawing.Size(139, 38);
            this.btnCommand1.TabIndex = 0;
            this.btnCommand1.Text = "Command1";
            this.btnCommand1.UseVisualStyleBackColor = false;
            this.btnCommand1.Visible = false;
            this.btnCommand1.Click += new System.EventHandler(this.btnCommand1_Click);
            // 
            // DialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCommand1;
            this.ClientSize = new System.Drawing.Size(451, 271);
            this.Controls.Add(this.btnCommand1);
            this.Controls.Add(this.btnCommand3);
            this.Controls.Add(this.btnCommand2);
            this.Controls.Add(this.lblMessage);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "DialogForm";
            this.Text = "Dialog";
            this.Load += new System.EventHandler(this.DialogForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnCommand2;
        private System.Windows.Forms.Button btnCommand3;
        private System.Windows.Forms.Button btnCommand1;
    }
}