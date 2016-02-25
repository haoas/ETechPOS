namespace ETech
{
    partial class frmCustomDiscount
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dg_discounts = new System.Windows.Forms.DataGridView();
            this.wid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbl_origPrice = new System.Windows.Forms.Label();
            this.lbl_newPrice = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnESC = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dg_discounts)).BeginInit();
            this.SuspendLayout();
            // 
            // dg_discounts
            // 
            this.dg_discounts.AllowUserToAddRows = false;
            this.dg_discounts.AllowUserToDeleteRows = false;
            this.dg_discounts.AllowUserToResizeColumns = false;
            this.dg_discounts.AllowUserToResizeRows = false;
            this.dg_discounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_discounts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.wid,
            this.name,
            this.value});
            this.dg_discounts.Location = new System.Drawing.Point(17, 94);
            this.dg_discounts.MultiSelect = false;
            this.dg_discounts.Name = "dg_discounts";
            this.dg_discounts.RowHeadersVisible = false;
            this.dg_discounts.RowTemplate.ReadOnly = true;
            this.dg_discounts.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_discounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_discounts.Size = new System.Drawing.Size(405, 169);
            this.dg_discounts.TabIndex = 2;
            this.dg_discounts.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_discounts_CellEnter);
            // 
            // wid
            // 
            this.wid.DataPropertyName = "wid";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wid.DefaultCellStyle = dataGridViewCellStyle1;
            this.wid.HeaderText = "wid";
            this.wid.Name = "wid";
            this.wid.ReadOnly = true;
            this.wid.Visible = false;
            this.wid.Width = 125;
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.name.DataPropertyName = "particular";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.DefaultCellStyle = dataGridViewCellStyle2;
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // value
            // 
            this.value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.value.DataPropertyName = "value";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value.DefaultCellStyle = dataGridViewCellStyle3;
            this.value.HeaderText = "Value (%)";
            this.value.Name = "value";
            this.value.ReadOnly = true;
            // 
            // lbl_origPrice
            // 
            this.lbl_origPrice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_origPrice.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_origPrice.Location = new System.Drawing.Point(17, 21);
            this.lbl_origPrice.Name = "lbl_origPrice";
            this.lbl_origPrice.Size = new System.Drawing.Size(193, 54);
            this.lbl_origPrice.TabIndex = 30;
            this.lbl_origPrice.Text = "P0.00";
            this.lbl_origPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_newPrice
            // 
            this.lbl_newPrice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_newPrice.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_newPrice.Location = new System.Drawing.Point(229, 21);
            this.lbl_newPrice.Name = "lbl_newPrice";
            this.lbl_newPrice.Size = new System.Drawing.Size(193, 54);
            this.lbl_newPrice.TabIndex = 31;
            this.lbl_newPrice.Text = "P0.00";
            this.lbl_newPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(157, 269);
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
            this.btnESC.Location = new System.Drawing.Point(283, 269);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(139, 38);
            this.btnESC.TabIndex = 105;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // frmCustomDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 313);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.lbl_newPrice);
            this.Controls.Add(this.lbl_origPrice);
            this.Controls.Add(this.dg_discounts);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmCustomDiscount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Product Discounts";
            this.Load += new System.EventHandler(this.frmCustomDetailDiscount_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCustomDetailDiscount_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dg_discounts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dg_discounts;
        private System.Windows.Forms.Label lbl_origPrice;
        private System.Windows.Forms.Label lbl_newPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn wid;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnESC;
    }
}