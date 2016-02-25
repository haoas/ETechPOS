namespace ETech
{
    partial class frmInventory
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
            this.btnF2 = new System.Windows.Forms.Button();
            this.btnF1 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnF4 = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // btnF2
            // 
            this.btnF2.BackColor = System.Drawing.Color.Navy;
            this.btnF2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF2.ForeColor = System.Drawing.Color.White;
            this.btnF2.Location = new System.Drawing.Point(120, 12);
            this.btnF2.Name = "btnF2";
            this.btnF2.Size = new System.Drawing.Size(93, 73);
            this.btnF2.TabIndex = 33;
            this.btnF2.Text = "F2\r\nZ Read";
            this.btnF2.UseVisualStyleBackColor = false;
            this.btnF2.Click += new System.EventHandler(this.btnF2_Click);
            // 
            // btnF1
            // 
            this.btnF1.BackColor = System.Drawing.Color.Navy;
            this.btnF1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF1.ForeColor = System.Drawing.Color.White;
            this.btnF1.Location = new System.Drawing.Point(21, 12);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(93, 73);
            this.btnF1.TabIndex = 32;
            this.btnF1.Text = "F1\r\nX Read";
            this.btnF1.UseVisualStyleBackColor = false;
            this.btnF1.Click += new System.EventHandler(this.btnF1_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(18, 125);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 34;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // btnF4
            // 
            this.btnF4.AutoSize = true;
            this.btnF4.BackColor = System.Drawing.Color.Navy;
            this.btnF4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF4.ForeColor = System.Drawing.Color.White;
            this.btnF4.Location = new System.Drawing.Point(56, 184);
            this.btnF4.Name = "btnF4";
            this.btnF4.Size = new System.Drawing.Size(122, 73);
            this.btnF4.TabIndex = 36;
            this.btnF4.Text = "F4\r\nTerminal\r\nAccountability";
            this.btnF4.UseVisualStyleBackColor = false;
            this.btnF4.Click += new System.EventHandler(this.btnF4_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(17, 158);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 38;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // frmInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(234, 92);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.btnF4);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btnF2);
            this.Controls.Add(this.btnF1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmInventory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Inventory_";
            this.Load += new System.EventHandler(this.frmInventory_Load);
            this.Leave += new System.EventHandler(this.frmInventory_Leave);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmInventory_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnF2;
        private System.Windows.Forms.Button btnF1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnF4;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;

    }
}