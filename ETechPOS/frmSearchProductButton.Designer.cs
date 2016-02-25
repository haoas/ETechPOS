namespace ETech
{
    partial class frmSearchProductButton
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
            this.gbox_dept = new System.Windows.Forms.GroupBox();
            this.gbox_products = new System.Windows.Forms.GroupBox();
            this.txtPage = new System.Windows.Forms.TextBox();
            this.lblTotalPage = new System.Windows.Forms.Label();
            this.DGVTempProd = new System.Windows.Forms.DataGridView();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnESC = new System.Windows.Forms.Button();
            this.gbox_products.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVTempProd)).BeginInit();
            this.SuspendLayout();
            // 
            // gbox_dept
            // 
            this.gbox_dept.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbox_dept.Location = new System.Drawing.Point(12, 12);
            this.gbox_dept.Name = "gbox_dept";
            this.gbox_dept.Size = new System.Drawing.Size(213, 511);
            this.gbox_dept.TabIndex = 0;
            this.gbox_dept.TabStop = false;
            this.gbox_dept.Text = "Department";
            // 
            // gbox_products
            // 
            this.gbox_products.Controls.Add(this.txtPage);
            this.gbox_products.Controls.Add(this.lblTotalPage);
            this.gbox_products.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbox_products.Location = new System.Drawing.Point(231, 12);
            this.gbox_products.Name = "gbox_products";
            this.gbox_products.Size = new System.Drawing.Size(409, 511);
            this.gbox_products.TabIndex = 21;
            this.gbox_products.TabStop = false;
            this.gbox_products.Text = "Products";
            // 
            // txtPage
            // 
            this.txtPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPage.Location = new System.Drawing.Point(110, 445);
            this.txtPage.Name = "txtPage";
            this.txtPage.Size = new System.Drawing.Size(90, 47);
            this.txtPage.TabIndex = 2;
            this.txtPage.Tag = "integer";
            this.txtPage.Text = "1000";
            this.txtPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPage_KeyDown);
            // 
            // lblTotalPage
            // 
            this.lblTotalPage.AutoSize = true;
            this.lblTotalPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPage.Location = new System.Drawing.Point(197, 448);
            this.lblTotalPage.Name = "lblTotalPage";
            this.lblTotalPage.Size = new System.Drawing.Size(108, 39);
            this.lblTotalPage.TabIndex = 1;
            this.lblTotalPage.Text = "/1000";
            // 
            // DGVTempProd
            // 
            this.DGVTempProd.AllowUserToAddRows = false;
            this.DGVTempProd.AllowUserToDeleteRows = false;
            this.DGVTempProd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVTempProd.Location = new System.Drawing.Point(646, 23);
            this.DGVTempProd.Name = "DGVTempProd";
            this.DGVTempProd.ReadOnly = true;
            this.DGVTempProd.Size = new System.Drawing.Size(361, 419);
            this.DGVTempProd.TabIndex = 22;
            this.DGVTempProd.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVTempProd_CellClick);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(646, 448);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(93, 75);
            this.btnOK.TabIndex = 106;
            this.btnOK.Text = "Okay (F1)";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnESC
            // 
            this.btnESC.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESC.Location = new System.Drawing.Point(914, 448);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(93, 75);
            this.btnESC.TabIndex = 107;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // frmSearchProductButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 535);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.DGVTempProd);
            this.Controls.Add(this.gbox_products);
            this.Controls.Add(this.gbox_dept);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "frmSearchProductButton";
            this.Text = "Product Search";
            this.Load += new System.EventHandler(this.frmSearchProductButton_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSearchProductButton_KeyDown);
            this.gbox_products.ResumeLayout(false);
            this.gbox_products.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVTempProd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbox_dept;
        private System.Windows.Forms.GroupBox gbox_products;
        private System.Windows.Forms.DataGridView DGVTempProd;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnESC;
        private System.Windows.Forms.TextBox txtPage;
        private System.Windows.Forms.Label lblTotalPage;
    }
}