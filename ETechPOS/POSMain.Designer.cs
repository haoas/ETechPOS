namespace ETech
{
    partial class POSMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvProduct = new System.Windows.Forms.DataGridView();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblORNumber = new System.Windows.Forms.Label();
            this.lblORNumber_d = new System.Windows.Forms.Label();
            this.btnF1 = new System.Windows.Forms.Button();
            this.btnF2 = new System.Windows.Forms.Button();
            this.btnF3 = new System.Windows.Forms.Button();
            this.btnF4 = new System.Windows.Forms.Button();
            this.btnF5 = new System.Windows.Forms.Button();
            this.btnF6 = new System.Windows.Forms.Button();
            this.btnF7 = new System.Windows.Forms.Button();
            this.btnF8 = new System.Windows.Forms.Button();
            this.btnF9 = new System.Windows.Forms.Button();
            this.btnF10 = new System.Windows.Forms.Button();
            this.btnF11 = new System.Windows.Forms.Button();
            this.btnF12 = new System.Windows.Forms.Button();
            this.pnlOtherInfo = new System.Windows.Forms.Panel();
            this.lbl_smemo = new System.Windows.Forms.Label();
            this.lblSalesMemo = new System.Windows.Forms.Label();
            this.lblCustomermemo_d = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            this.lblMember_d = new System.Windows.Forms.Label();
            this.lblCustomer_d = new System.Windows.Forms.Label();
            this.lblMember = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblMode_d = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.lblChecker_d = new System.Windows.Forms.Label();
            this.lblClerk_d = new System.Windows.Forms.Label();
            this.lblChecker = new System.Windows.Forms.Label();
            this.lblClerk = new System.Windows.Forms.Label();
            this.lblTerminal_d = new System.Windows.Forms.Label();
            this.lblTerminal = new System.Windows.Forms.Label();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.lblQty_d = new System.Windows.Forms.Label();
            this.gbTotal = new System.Windows.Forms.GroupBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.gbTendered = new System.Windows.Forms.GroupBox();
            this.lblTendered = new System.Windows.Forms.Label();
            this.gbRemaining = new System.Windows.Forms.GroupBox();
            this.lblRemaining = new System.Windows.Forms.Label();
            this.lblTransaction = new System.Windows.Forms.Label();
            this.lbltransaction_d = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbltransaction_total = new System.Windows.Forms.Label();
            this.gbProductInfo = new System.Windows.Forms.GroupBox();
            this.lblSwitchPack = new System.Windows.Forms.Label();
            this.lblAmount_d = new System.Windows.Forms.Label();
            this.lblProductPrice_d = new System.Windows.Forms.Label();
            this.lblProductPrice = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblProductDesc_d = new System.Windows.Forms.Label();
            this.lblProductDesc = new System.Windows.Forms.Label();
            this.lblQuantity_d = new System.Windows.Forms.Label();
            this.lblProductBarcode_d = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.lblProductBarcode = new System.Windows.Forms.Label();
            this.spcustdisp = new System.IO.Ports.SerialPort(this.components);
            this.gbDiscounts = new System.Windows.Forms.GroupBox();
            this.lbl_pospromodiscount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_adjustments = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_regdisc = new System.Windows.Forms.Label();
            this.lbl_memberdisc = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tmrcheckposdsetting = new System.Windows.Forms.Timer(this.components);
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            this.pnlOtherInfo.SuspendLayout();
            this.gbTotal.SuspendLayout();
            this.gbTendered.SuspendLayout();
            this.gbRemaining.SuspendLayout();
            this.gbProductInfo.SuspendLayout();
            this.gbDiscounts.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvProduct
            // 
            this.dgvProduct.AllowUserToAddRows = false;
            this.dgvProduct.AllowUserToDeleteRows = false;
            this.dgvProduct.AllowUserToResizeColumns = false;
            this.dgvProduct.AllowUserToResizeRows = false;
            this.dgvProduct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.description,
            this.qty,
            this.price,
            this.amount});
            this.dgvProduct.Enabled = false;
            this.dgvProduct.Location = new System.Drawing.Point(9, 127);
            this.dgvProduct.Name = "dgvProduct";
            this.dgvProduct.ReadOnly = true;
            this.dgvProduct.RowHeadersVisible = false;
            this.dgvProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProduct.Size = new System.Drawing.Size(662, 554);
            this.dgvProduct.TabIndex = 1;
            this.dgvProduct.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProduct_CellClick);
            // 
            // txtBarcode
            // 
            this.txtBarcode.Enabled = false;
            this.txtBarcode.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcode.Location = new System.Drawing.Point(9, 82);
            this.txtBarcode.MaxLength = 50;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(662, 35);
            this.txtBarcode.TabIndex = 0;
            this.txtBarcode.Tag = "";
            this.txtBarcode.Leave += new System.EventHandler(this.txtBarcode_Leave);
            this.txtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarcode_KeyPress);
            // 
            // lblORNumber
            // 
            this.lblORNumber.AutoSize = true;
            this.lblORNumber.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblORNumber.Location = new System.Drawing.Point(2, 9);
            this.lblORNumber.Name = "lblORNumber";
            this.lblORNumber.Size = new System.Drawing.Size(94, 38);
            this.lblORNumber.TabIndex = 4;
            this.lblORNumber.Text = "OR#:";
            // 
            // lblORNumber_d
            // 
            this.lblORNumber_d.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblORNumber_d.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblORNumber_d.Location = new System.Drawing.Point(102, 9);
            this.lblORNumber_d.Name = "lblORNumber_d";
            this.lblORNumber_d.Size = new System.Drawing.Size(366, 39);
            this.lblORNumber_d.TabIndex = 5;
            this.lblORNumber_d.Text = "00000000000";
            this.lblORNumber_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnF1
            // 
            this.btnF1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF1.ForeColor = System.Drawing.Color.White;
            this.btnF1.Location = new System.Drawing.Point(9, 687);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(99, 73);
            this.btnF1.TabIndex = 6;
            this.btnF1.Text = "F1\r\nNew";
            this.btnF1.UseVisualStyleBackColor = false;
            // 
            // btnF2
            // 
            this.btnF2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF2.Enabled = false;
            this.btnF2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF2.ForeColor = System.Drawing.Color.White;
            this.btnF2.Location = new System.Drawing.Point(114, 687);
            this.btnF2.Name = "btnF2";
            this.btnF2.Size = new System.Drawing.Size(99, 73);
            this.btnF2.TabIndex = 7;
            this.btnF2.Text = "F2\r\nSwitch";
            this.btnF2.UseVisualStyleBackColor = false;
            // 
            // btnF3
            // 
            this.btnF3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF3.ForeColor = System.Drawing.Color.White;
            this.btnF3.Location = new System.Drawing.Point(219, 687);
            this.btnF3.Name = "btnF3";
            this.btnF3.Size = new System.Drawing.Size(99, 73);
            this.btnF3.TabIndex = 8;
            this.btnF3.Text = "F3\r\nExit";
            this.btnF3.UseVisualStyleBackColor = false;
            // 
            // btnF4
            // 
            this.btnF4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF4.Enabled = false;
            this.btnF4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF4.ForeColor = System.Drawing.Color.White;
            this.btnF4.Location = new System.Drawing.Point(324, 687);
            this.btnF4.Name = "btnF4";
            this.btnF4.Size = new System.Drawing.Size(99, 73);
            this.btnF4.TabIndex = 9;
            this.btnF4.Text = "F4\r\nSearch";
            this.btnF4.UseVisualStyleBackColor = false;
            // 
            // btnF5
            // 
            this.btnF5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF5.Enabled = false;
            this.btnF5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF5.ForeColor = System.Drawing.Color.White;
            this.btnF5.Location = new System.Drawing.Point(429, 687);
            this.btnF5.Name = "btnF5";
            this.btnF5.Size = new System.Drawing.Size(99, 73);
            this.btnF5.TabIndex = 10;
            this.btnF5.Text = "F5\r\nQty";
            this.btnF5.UseVisualStyleBackColor = false;
            // 
            // btnF6
            // 
            this.btnF6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF6.Enabled = false;
            this.btnF6.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF6.ForeColor = System.Drawing.Color.White;
            this.btnF6.Location = new System.Drawing.Point(534, 687);
            this.btnF6.Name = "btnF6";
            this.btnF6.Size = new System.Drawing.Size(99, 73);
            this.btnF6.TabIndex = 11;
            this.btnF6.Text = "F6\r\nVoid";
            this.btnF6.UseVisualStyleBackColor = false;
            // 
            // btnF7
            // 
            this.btnF7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF7.Enabled = false;
            this.btnF7.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF7.ForeColor = System.Drawing.Color.White;
            this.btnF7.Location = new System.Drawing.Point(639, 687);
            this.btnF7.Name = "btnF7";
            this.btnF7.Size = new System.Drawing.Size(99, 73);
            this.btnF7.TabIndex = 12;
            this.btnF7.Text = "F7\r\nDelete";
            this.btnF7.UseVisualStyleBackColor = false;
            // 
            // btnF8
            // 
            this.btnF8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF8.Enabled = false;
            this.btnF8.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF8.ForeColor = System.Drawing.Color.White;
            this.btnF8.Location = new System.Drawing.Point(744, 687);
            this.btnF8.Name = "btnF8";
            this.btnF8.Size = new System.Drawing.Size(99, 73);
            this.btnF8.TabIndex = 13;
            this.btnF8.Text = "F8\r\nPayment";
            this.btnF8.UseVisualStyleBackColor = false;
            // 
            // btnF9
            // 
            this.btnF9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF9.Enabled = false;
            this.btnF9.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF9.ForeColor = System.Drawing.Color.White;
            this.btnF9.Location = new System.Drawing.Point(849, 687);
            this.btnF9.Name = "btnF9";
            this.btnF9.Size = new System.Drawing.Size(99, 73);
            this.btnF9.TabIndex = 14;
            this.btnF9.Text = "F9\r\nRetail";
            this.btnF9.UseVisualStyleBackColor = false;
            // 
            // btnF10
            // 
            this.btnF10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF10.Enabled = false;
            this.btnF10.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF10.ForeColor = System.Drawing.Color.White;
            this.btnF10.Location = new System.Drawing.Point(954, 687);
            this.btnF10.Name = "btnF10";
            this.btnF10.Size = new System.Drawing.Size(99, 73);
            this.btnF10.TabIndex = 15;
            this.btnF10.Text = "F10\r\nPackage";
            this.btnF10.UseVisualStyleBackColor = false;
            // 
            // btnF11
            // 
            this.btnF11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF11.Enabled = false;
            this.btnF11.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF11.ForeColor = System.Drawing.Color.White;
            this.btnF11.Location = new System.Drawing.Point(1059, 687);
            this.btnF11.Name = "btnF11";
            this.btnF11.Size = new System.Drawing.Size(99, 73);
            this.btnF11.TabIndex = 16;
            this.btnF11.Text = "F11 \r\nAdjust /\r\nDiscount";
            this.btnF11.UseVisualStyleBackColor = false;
            // 
            // btnF12
            // 
            this.btnF12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF12.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF12.ForeColor = System.Drawing.Color.White;
            this.btnF12.Location = new System.Drawing.Point(1164, 687);
            this.btnF12.Name = "btnF12";
            this.btnF12.Size = new System.Drawing.Size(105, 73);
            this.btnF12.TabIndex = 17;
            this.btnF12.Text = "F12\r\nMenu";
            this.btnF12.UseVisualStyleBackColor = false;
            // 
            // pnlOtherInfo
            // 
            this.pnlOtherInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.pnlOtherInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlOtherInfo.Controls.Add(this.lbl_smemo);
            this.pnlOtherInfo.Controls.Add(this.lblSalesMemo);
            this.pnlOtherInfo.Controls.Add(this.lblCustomermemo_d);
            this.pnlOtherInfo.Controls.Add(this.lblWarning);
            this.pnlOtherInfo.Controls.Add(this.lblMember_d);
            this.pnlOtherInfo.Controls.Add(this.lblCustomer_d);
            this.pnlOtherInfo.Controls.Add(this.lblMember);
            this.pnlOtherInfo.Controls.Add(this.lblCustomer);
            this.pnlOtherInfo.Controls.Add(this.lblMode_d);
            this.pnlOtherInfo.Controls.Add(this.lblMode);
            this.pnlOtherInfo.Controls.Add(this.lblChecker_d);
            this.pnlOtherInfo.Controls.Add(this.lblClerk_d);
            this.pnlOtherInfo.Controls.Add(this.lblChecker);
            this.pnlOtherInfo.Controls.Add(this.lblClerk);
            this.pnlOtherInfo.Location = new System.Drawing.Point(685, 478);
            this.pnlOtherInfo.Name = "pnlOtherInfo";
            this.pnlOtherInfo.Size = new System.Drawing.Size(584, 203);
            this.pnlOtherInfo.TabIndex = 18;
            // 
            // lbl_smemo
            // 
            this.lbl_smemo.AutoSize = true;
            this.lbl_smemo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_smemo.ForeColor = System.Drawing.Color.Black;
            this.lbl_smemo.Location = new System.Drawing.Point(303, 72);
            this.lbl_smemo.Name = "lbl_smemo";
            this.lbl_smemo.Size = new System.Drawing.Size(69, 24);
            this.lbl_smemo.TabIndex = 15;
            this.lbl_smemo.Text = "Memo:";
            // 
            // lblSalesMemo
            // 
            this.lblSalesMemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSalesMemo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalesMemo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblSalesMemo.Location = new System.Drawing.Point(401, 72);
            this.lblSalesMemo.Name = "lblSalesMemo";
            this.lblSalesMemo.Size = new System.Drawing.Size(169, 24);
            this.lblSalesMemo.TabIndex = 14;
            // 
            // lblCustomermemo_d
            // 
            this.lblCustomermemo_d.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCustomermemo_d.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCustomermemo_d.ForeColor = System.Drawing.Color.Black;
            this.lblCustomermemo_d.Location = new System.Drawing.Point(13, 103);
            this.lblCustomermemo_d.Name = "lblCustomermemo_d";
            this.lblCustomermemo_d.Size = new System.Drawing.Size(557, 40);
            this.lblCustomermemo_d.TabIndex = 13;
            // 
            // lblWarning
            // 
            this.lblWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarning.ForeColor = System.Drawing.Color.Red;
            this.lblWarning.Location = new System.Drawing.Point(13, 149);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(557, 40);
            this.lblWarning.TabIndex = 12;
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMember_d
            // 
            this.lblMember_d.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMember_d.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMember_d.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblMember_d.Location = new System.Drawing.Point(401, 12);
            this.lblMember_d.Name = "lblMember_d";
            this.lblMember_d.Size = new System.Drawing.Size(169, 24);
            this.lblMember_d.TabIndex = 11;
            // 
            // lblCustomer_d
            // 
            this.lblCustomer_d.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCustomer_d.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer_d.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblCustomer_d.Location = new System.Drawing.Point(117, 72);
            this.lblCustomer_d.Name = "lblCustomer_d";
            this.lblCustomer_d.Size = new System.Drawing.Size(169, 24);
            this.lblCustomer_d.TabIndex = 10;
            // 
            // lblMember
            // 
            this.lblMember.AutoSize = true;
            this.lblMember.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMember.ForeColor = System.Drawing.Color.Black;
            this.lblMember.Location = new System.Drawing.Point(303, 12);
            this.lblMember.Name = "lblMember";
            this.lblMember.Size = new System.Drawing.Size(86, 24);
            this.lblMember.TabIndex = 9;
            this.lblMember.Text = "Member:";
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer.ForeColor = System.Drawing.Color.Black;
            this.lblCustomer.Location = new System.Drawing.Point(14, 70);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(96, 24);
            this.lblCustomer.TabIndex = 8;
            this.lblCustomer.Text = "Customer:";
            // 
            // lblMode_d
            // 
            this.lblMode_d.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMode_d.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode_d.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblMode_d.Location = new System.Drawing.Point(401, 42);
            this.lblMode_d.Name = "lblMode_d";
            this.lblMode_d.Size = new System.Drawing.Size(169, 24);
            this.lblMode_d.TabIndex = 5;
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.Black;
            this.lblMode.Location = new System.Drawing.Point(303, 42);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(64, 24);
            this.lblMode.TabIndex = 4;
            this.lblMode.Text = "Mode:";
            // 
            // lblChecker_d
            // 
            this.lblChecker_d.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblChecker_d.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChecker_d.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblChecker_d.Location = new System.Drawing.Point(117, 42);
            this.lblChecker_d.Name = "lblChecker_d";
            this.lblChecker_d.Size = new System.Drawing.Size(169, 24);
            this.lblChecker_d.TabIndex = 3;
            // 
            // lblClerk_d
            // 
            this.lblClerk_d.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblClerk_d.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClerk_d.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblClerk_d.Location = new System.Drawing.Point(117, 12);
            this.lblClerk_d.Name = "lblClerk_d";
            this.lblClerk_d.Size = new System.Drawing.Size(169, 24);
            this.lblClerk_d.TabIndex = 2;
            // 
            // lblChecker
            // 
            this.lblChecker.AutoSize = true;
            this.lblChecker.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChecker.ForeColor = System.Drawing.Color.Black;
            this.lblChecker.Location = new System.Drawing.Point(15, 41);
            this.lblChecker.Name = "lblChecker";
            this.lblChecker.Size = new System.Drawing.Size(98, 24);
            this.lblChecker.TabIndex = 1;
            this.lblChecker.Text = "Salesman:";
            // 
            // lblClerk
            // 
            this.lblClerk.AutoSize = true;
            this.lblClerk.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClerk.ForeColor = System.Drawing.Color.Black;
            this.lblClerk.Location = new System.Drawing.Point(16, 13);
            this.lblClerk.Name = "lblClerk";
            this.lblClerk.Size = new System.Drawing.Size(58, 24);
            this.lblClerk.TabIndex = 0;
            this.lblClerk.Text = "Clerk:";
            // 
            // lblTerminal_d
            // 
            this.lblTerminal_d.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerminal_d.ForeColor = System.Drawing.Color.Navy;
            this.lblTerminal_d.Location = new System.Drawing.Point(550, -5);
            this.lblTerminal_d.Name = "lblTerminal_d";
            this.lblTerminal_d.Size = new System.Drawing.Size(34, 24);
            this.lblTerminal_d.TabIndex = 7;
            this.lblTerminal_d.Text = "0";
            this.lblTerminal_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTerminal
            // 
            this.lblTerminal.AutoSize = true;
            this.lblTerminal.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerminal.ForeColor = System.Drawing.Color.Navy;
            this.lblTerminal.Location = new System.Drawing.Point(471, -3);
            this.lblTerminal.Name = "lblTerminal";
            this.lblTerminal.Size = new System.Drawing.Size(70, 18);
            this.lblTerminal.TabIndex = 6;
            this.lblTerminal.Text = "Terminal:";
            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBarcode.Location = new System.Drawing.Point(5, 57);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(87, 22);
            this.lblBarcode.TabIndex = 20;
            this.lblBarcode.Text = "Barcode:";
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.Location = new System.Drawing.Point(474, 9);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(76, 38);
            this.lblQty.TabIndex = 21;
            this.lblQty.Text = "Qty:";
            // 
            // lblQty_d
            // 
            this.lblQty_d.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblQty_d.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty_d.Location = new System.Drawing.Point(556, 9);
            this.lblQty_d.Name = "lblQty_d";
            this.lblQty_d.Size = new System.Drawing.Size(115, 39);
            this.lblQty_d.TabIndex = 22;
            this.lblQty_d.Text = "0";
            this.lblQty_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbTotal
            // 
            this.gbTotal.Controls.Add(this.lblTotal);
            this.gbTotal.Controls.Add(this.lblTerminal);
            this.gbTotal.Controls.Add(this.lblTerminal_d);
            this.gbTotal.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTotal.Location = new System.Drawing.Point(685, 6);
            this.gbTotal.Name = "gbTotal";
            this.gbTotal.Size = new System.Drawing.Size(584, 144);
            this.gbTotal.TabIndex = 23;
            this.gbTotal.TabStop = false;
            this.gbTotal.Text = "TOTAL";
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Arial Black", 55F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTotal.Location = new System.Drawing.Point(13, 36);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(566, 103);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // gbTendered
            // 
            this.gbTendered.Controls.Add(this.lblTendered);
            this.gbTendered.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTendered.Location = new System.Drawing.Point(685, 180);
            this.gbTendered.Name = "gbTendered";
            this.gbTendered.Size = new System.Drawing.Size(584, 144);
            this.gbTendered.TabIndex = 24;
            this.gbTendered.TabStop = false;
            this.gbTendered.Text = "TENDERED";
            // 
            // lblTendered
            // 
            this.lblTendered.Font = new System.Drawing.Font("Arial Black", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTendered.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTendered.Location = new System.Drawing.Point(72, 52);
            this.lblTendered.Name = "lblTendered";
            this.lblTendered.Size = new System.Drawing.Size(505, 84);
            this.lblTendered.TabIndex = 1;
            this.lblTendered.Text = "0.00";
            this.lblTendered.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // gbRemaining
            // 
            this.gbRemaining.Controls.Add(this.lblRemaining);
            this.gbRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbRemaining.Location = new System.Drawing.Point(685, 322);
            this.gbRemaining.Name = "gbRemaining";
            this.gbRemaining.Size = new System.Drawing.Size(584, 144);
            this.gbRemaining.TabIndex = 24;
            this.gbRemaining.TabStop = false;
            this.gbRemaining.Text = "REMAINING";
            // 
            // lblRemaining
            // 
            this.lblRemaining.Font = new System.Drawing.Font("Arial Black", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemaining.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblRemaining.Location = new System.Drawing.Point(91, 43);
            this.lblRemaining.Name = "lblRemaining";
            this.lblRemaining.Size = new System.Drawing.Size(488, 96);
            this.lblRemaining.TabIndex = 2;
            this.lblRemaining.Text = "0.00";
            this.lblRemaining.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblTransaction
            // 
            this.lblTransaction.AutoSize = true;
            this.lblTransaction.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransaction.Location = new System.Drawing.Point(498, 58);
            this.lblTransaction.Name = "lblTransaction";
            this.lblTransaction.Size = new System.Drawing.Size(112, 22);
            this.lblTransaction.TabIndex = 25;
            this.lblTransaction.Text = "Transaction:";
            // 
            // lbltransaction_d
            // 
            this.lbltransaction_d.AutoSize = true;
            this.lbltransaction_d.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltransaction_d.Location = new System.Drawing.Point(616, 58);
            this.lbltransaction_d.Name = "lbltransaction_d";
            this.lbltransaction_d.Size = new System.Drawing.Size(16, 22);
            this.lbltransaction_d.TabIndex = 26;
            this.lbltransaction_d.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(636, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 22);
            this.label2.TabIndex = 27;
            this.label2.Text = "/";
            // 
            // lbltransaction_total
            // 
            this.lbltransaction_total.AutoSize = true;
            this.lbltransaction_total.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltransaction_total.Location = new System.Drawing.Point(655, 57);
            this.lbltransaction_total.Name = "lbltransaction_total";
            this.lbltransaction_total.Size = new System.Drawing.Size(16, 22);
            this.lbltransaction_total.TabIndex = 28;
            this.lbltransaction_total.Text = "-";
            // 
            // gbProductInfo
            // 
            this.gbProductInfo.Controls.Add(this.lblSwitchPack);
            this.gbProductInfo.Controls.Add(this.lblAmount_d);
            this.gbProductInfo.Controls.Add(this.lblProductPrice_d);
            this.gbProductInfo.Controls.Add(this.lblProductPrice);
            this.gbProductInfo.Controls.Add(this.lblAmount);
            this.gbProductInfo.Controls.Add(this.lblProductDesc_d);
            this.gbProductInfo.Controls.Add(this.lblProductDesc);
            this.gbProductInfo.Controls.Add(this.lblQuantity_d);
            this.gbProductInfo.Controls.Add(this.lblProductBarcode_d);
            this.gbProductInfo.Controls.Add(this.lblQuantity);
            this.gbProductInfo.Controls.Add(this.lblProductBarcode);
            this.gbProductInfo.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbProductInfo.Location = new System.Drawing.Point(685, 156);
            this.gbProductInfo.Name = "gbProductInfo";
            this.gbProductInfo.Size = new System.Drawing.Size(584, 229);
            this.gbProductInfo.TabIndex = 2;
            this.gbProductInfo.TabStop = false;
            this.gbProductInfo.Text = "PRODUCT INFO";
            // 
            // lblSwitchPack
            // 
            this.lblSwitchPack.Font = new System.Drawing.Font("Arial", 14.25F);
            this.lblSwitchPack.ForeColor = System.Drawing.Color.Black;
            this.lblSwitchPack.Location = new System.Drawing.Point(18, 189);
            this.lblSwitchPack.Name = "lblSwitchPack";
            this.lblSwitchPack.Size = new System.Drawing.Size(554, 30);
            this.lblSwitchPack.TabIndex = 13;
            this.lblSwitchPack.Text = "F10 - Switch to per Package";
            this.lblSwitchPack.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAmount_d
            // 
            this.lblAmount_d.Font = new System.Drawing.Font("Arial", 14.25F);
            this.lblAmount_d.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblAmount_d.Location = new System.Drawing.Point(395, 160);
            this.lblAmount_d.Name = "lblAmount_d";
            this.lblAmount_d.Size = new System.Drawing.Size(177, 24);
            this.lblAmount_d.TabIndex = 12;
            // 
            // lblProductPrice_d
            // 
            this.lblProductPrice_d.Font = new System.Drawing.Font("Arial", 14.25F);
            this.lblProductPrice_d.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblProductPrice_d.Location = new System.Drawing.Point(124, 102);
            this.lblProductPrice_d.Name = "lblProductPrice_d";
            this.lblProductPrice_d.Size = new System.Drawing.Size(449, 58);
            this.lblProductPrice_d.TabIndex = 6;
            // 
            // lblProductPrice
            // 
            this.lblProductPrice.AutoSize = true;
            this.lblProductPrice.Font = new System.Drawing.Font("Arial", 14.25F);
            this.lblProductPrice.ForeColor = System.Drawing.Color.Black;
            this.lblProductPrice.Location = new System.Drawing.Point(8, 102);
            this.lblProductPrice.Name = "lblProductPrice";
            this.lblProductPrice.Size = new System.Drawing.Size(59, 22);
            this.lblProductPrice.TabIndex = 5;
            this.lblProductPrice.Text = "Price:";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Arial", 14.25F);
            this.lblAmount.ForeColor = System.Drawing.Color.Black;
            this.lblAmount.Location = new System.Drawing.Point(309, 160);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(80, 22);
            this.lblAmount.TabIndex = 11;
            this.lblAmount.Text = "Amount:";
            // 
            // lblProductDesc_d
            // 
            this.lblProductDesc_d.Font = new System.Drawing.Font("Arial", 14.25F);
            this.lblProductDesc_d.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblProductDesc_d.Location = new System.Drawing.Point(124, 70);
            this.lblProductDesc_d.Name = "lblProductDesc_d";
            this.lblProductDesc_d.Size = new System.Drawing.Size(448, 24);
            this.lblProductDesc_d.TabIndex = 4;
            // 
            // lblProductDesc
            // 
            this.lblProductDesc.AutoSize = true;
            this.lblProductDesc.Font = new System.Drawing.Font("Arial", 14.25F);
            this.lblProductDesc.ForeColor = System.Drawing.Color.Black;
            this.lblProductDesc.Location = new System.Drawing.Point(7, 70);
            this.lblProductDesc.Name = "lblProductDesc";
            this.lblProductDesc.Size = new System.Drawing.Size(111, 22);
            this.lblProductDesc.TabIndex = 3;
            this.lblProductDesc.Text = "Description:";
            // 
            // lblQuantity_d
            // 
            this.lblQuantity_d.Font = new System.Drawing.Font("Arial", 14.25F);
            this.lblQuantity_d.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblQuantity_d.Location = new System.Drawing.Point(118, 160);
            this.lblQuantity_d.Name = "lblQuantity_d";
            this.lblQuantity_d.Size = new System.Drawing.Size(178, 24);
            this.lblQuantity_d.TabIndex = 10;
            // 
            // lblProductBarcode_d
            // 
            this.lblProductBarcode_d.Font = new System.Drawing.Font("Arial", 14.25F);
            this.lblProductBarcode_d.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblProductBarcode_d.Location = new System.Drawing.Point(124, 40);
            this.lblProductBarcode_d.Name = "lblProductBarcode_d";
            this.lblProductBarcode_d.Size = new System.Drawing.Size(310, 25);
            this.lblProductBarcode_d.TabIndex = 8;
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Arial", 14.25F);
            this.lblQuantity.ForeColor = System.Drawing.Color.Black;
            this.lblQuantity.Location = new System.Drawing.Point(8, 160);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(83, 22);
            this.lblQuantity.TabIndex = 9;
            this.lblQuantity.Text = "Quantity:";
            // 
            // lblProductBarcode
            // 
            this.lblProductBarcode.AutoSize = true;
            this.lblProductBarcode.Font = new System.Drawing.Font("Arial", 14.25F);
            this.lblProductBarcode.ForeColor = System.Drawing.Color.Black;
            this.lblProductBarcode.Location = new System.Drawing.Point(8, 41);
            this.lblProductBarcode.Name = "lblProductBarcode";
            this.lblProductBarcode.Size = new System.Drawing.Size(87, 22);
            this.lblProductBarcode.TabIndex = 7;
            this.lblProductBarcode.Text = "Barcode:";
            // 
            // spcustdisp
            // 
            this.spcustdisp.PortName = "COM3";
            // 
            // gbDiscounts
            // 
            this.gbDiscounts.Controls.Add(this.lbl_pospromodiscount);
            this.gbDiscounts.Controls.Add(this.label5);
            this.gbDiscounts.Controls.Add(this.lbl_adjustments);
            this.gbDiscounts.Controls.Add(this.label6);
            this.gbDiscounts.Controls.Add(this.lbl_regdisc);
            this.gbDiscounts.Controls.Add(this.lbl_memberdisc);
            this.gbDiscounts.Controls.Add(this.label3);
            this.gbDiscounts.Controls.Add(this.label1);
            this.gbDiscounts.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDiscounts.Location = new System.Drawing.Point(684, 388);
            this.gbDiscounts.Name = "gbDiscounts";
            this.gbDiscounts.Size = new System.Drawing.Size(584, 84);
            this.gbDiscounts.TabIndex = 14;
            this.gbDiscounts.TabStop = false;
            this.gbDiscounts.Text = "Transaction Discounts";
            // 
            // lbl_pospromodiscount
            // 
            this.lbl_pospromodiscount.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pospromodiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lbl_pospromodiscount.Location = new System.Drawing.Point(160, 53);
            this.lbl_pospromodiscount.Name = "lbl_pospromodiscount";
            this.lbl_pospromodiscount.Size = new System.Drawing.Size(128, 20);
            this.lbl_pospromodiscount.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 22);
            this.label5.TabIndex = 18;
            this.label5.Text = "Pos Promo:";
            // 
            // lbl_adjustments
            // 
            this.lbl_adjustments.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_adjustments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lbl_adjustments.Location = new System.Drawing.Point(463, 24);
            this.lbl_adjustments.Name = "lbl_adjustments";
            this.lbl_adjustments.Size = new System.Drawing.Size(116, 25);
            this.lbl_adjustments.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(303, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 22);
            this.label6.TabIndex = 16;
            this.label6.Text = "Adjustments:";
            // 
            // lbl_regdisc
            // 
            this.lbl_regdisc.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_regdisc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lbl_regdisc.Location = new System.Drawing.Point(469, 49);
            this.lbl_regdisc.Name = "lbl_regdisc";
            this.lbl_regdisc.Size = new System.Drawing.Size(109, 26);
            this.lbl_regdisc.TabIndex = 15;
            // 
            // lbl_memberdisc
            // 
            this.lbl_memberdisc.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_memberdisc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lbl_memberdisc.Location = new System.Drawing.Point(201, 27);
            this.lbl_memberdisc.Name = "lbl_memberdisc";
            this.lbl_memberdisc.Size = new System.Drawing.Size(87, 20);
            this.lbl_memberdisc.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(303, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 22);
            this.label3.TabIndex = 1;
            this.label3.Text = "Custom Discounts:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Member Discount:";
            // 
            // tmrcheckposdsetting
            // 
            this.tmrcheckposdsetting.Enabled = true;
            this.tmrcheckposdsetting.Interval = 5000;
            this.tmrcheckposdsetting.Tick += new System.EventHandler(this.tmrcheckposdsetting_Tick);
            // 
            // description
            // 
            this.description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.description.DataPropertyName = "productname";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.description.DefaultCellStyle = dataGridViewCellStyle1;
            this.description.FillWeight = 200F;
            this.description.HeaderText = "Description";
            this.description.MinimumWidth = 300;
            this.description.Name = "description";
            this.description.ReadOnly = true;
            // 
            // qty
            // 
            this.qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.qty.DataPropertyName = "qty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.qty.DefaultCellStyle = dataGridViewCellStyle2;
            this.qty.FillWeight = 46.81124F;
            this.qty.HeaderText = "Qty";
            this.qty.MinimumWidth = 100;
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            // 
            // price
            // 
            this.price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.price.DataPropertyName = "price";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.price.DefaultCellStyle = dataGridViewCellStyle3;
            this.price.FillWeight = 61.91541F;
            this.price.HeaderText = "Price";
            this.price.MinimumWidth = 100;
            this.price.Name = "price";
            this.price.ReadOnly = true;
            // 
            // amount
            // 
            this.amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.amount.DataPropertyName = "amount";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.amount.DefaultCellStyle = dataGridViewCellStyle4;
            this.amount.HeaderText = "Amount";
            this.amount.MinimumWidth = 100;
            this.amount.Name = "amount";
            this.amount.ReadOnly = true;
            // 
            // POSMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1020, 730);
            this.Controls.Add(this.gbProductInfo);
            this.Controls.Add(this.dgvProduct);
            this.Controls.Add(this.gbDiscounts);
            this.Controls.Add(this.lbltransaction_total);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbltransaction_d);
            this.Controls.Add(this.lblTransaction);
            this.Controls.Add(this.gbTendered);
            this.Controls.Add(this.gbTotal);
            this.Controls.Add(this.lblQty_d);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.lblBarcode);
            this.Controls.Add(this.pnlOtherInfo);
            this.Controls.Add(this.btnF12);
            this.Controls.Add(this.btnF11);
            this.Controls.Add(this.btnF10);
            this.Controls.Add(this.btnF9);
            this.Controls.Add(this.btnF8);
            this.Controls.Add(this.btnF7);
            this.Controls.Add(this.btnF6);
            this.Controls.Add(this.btnF5);
            this.Controls.Add(this.btnF4);
            this.Controls.Add(this.btnF3);
            this.Controls.Add(this.btnF2);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.lblORNumber_d);
            this.Controls.Add(this.lblORNumber);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.gbRemaining);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "POSMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ETECH POS SYSTEM Version 1.0.0";
            this.Load += new System.EventHandler(this.POSMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.POSMain_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.POSMain_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).EndInit();
            this.pnlOtherInfo.ResumeLayout(false);
            this.pnlOtherInfo.PerformLayout();
            this.gbTotal.ResumeLayout(false);
            this.gbTotal.PerformLayout();
            this.gbTendered.ResumeLayout(false);
            this.gbRemaining.ResumeLayout(false);
            this.gbProductInfo.ResumeLayout(false);
            this.gbProductInfo.PerformLayout();
            this.gbDiscounts.ResumeLayout(false);
            this.gbDiscounts.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblORNumber;
        private System.Windows.Forms.Label lblORNumber_d;
        private System.Windows.Forms.Button btnF1;
        private System.Windows.Forms.Button btnF2;
        private System.Windows.Forms.Button btnF3;
        private System.Windows.Forms.Button btnF4;
        private System.Windows.Forms.Button btnF5;
        private System.Windows.Forms.Button btnF6;
        private System.Windows.Forms.Button btnF7;
        private System.Windows.Forms.Button btnF8;
        private System.Windows.Forms.Button btnF9;
        private System.Windows.Forms.Button btnF10;
        private System.Windows.Forms.Button btnF11;
        private System.Windows.Forms.Button btnF12;
        private System.Windows.Forms.Panel pnlOtherInfo;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Label lblQty_d;
        private System.Windows.Forms.GroupBox gbTotal;
        private System.Windows.Forms.GroupBox gbTendered;
        private System.Windows.Forms.GroupBox gbRemaining;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTendered;
        private System.Windows.Forms.Label lblRemaining;
        private System.Windows.Forms.Label lblChecker_d;
        private System.Windows.Forms.Label lblClerk_d;
        private System.Windows.Forms.Label lblChecker;
        private System.Windows.Forms.Label lblClerk;
        private System.Windows.Forms.Label lblTerminal_d;
        private System.Windows.Forms.Label lblTerminal;
        private System.Windows.Forms.Label lblMode_d;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Label lblMember_d;
        private System.Windows.Forms.Label lblCustomer_d;
        private System.Windows.Forms.Label lblMember;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Label lblTransaction;
        private System.Windows.Forms.Label lbltransaction_d;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbltransaction_total;
        private System.Windows.Forms.GroupBox gbProductInfo;
        private System.Windows.Forms.Label lblProductDesc_d;
        private System.Windows.Forms.Label lblProductDesc;
        private System.Windows.Forms.Label lblProductPrice_d;
        private System.Windows.Forms.Label lblProductPrice;
        private System.Windows.Forms.Label lblProductBarcode_d;
        private System.Windows.Forms.Label lblProductBarcode;
        private System.IO.Ports.SerialPort spcustdisp;
        private System.Windows.Forms.Label lblAmount_d;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblQuantity_d;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblSwitchPack;
        private System.Windows.Forms.GroupBox gbDiscounts;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_regdisc;
        private System.Windows.Forms.Label lbl_memberdisc;
        private System.Windows.Forms.Label lbl_adjustments;
        private System.Windows.Forms.Label lbl_pospromodiscount;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.DataGridView dgvProduct;
        public System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label lblCustomermemo_d;
        private System.Windows.Forms.Timer tmrcheckposdsetting;
        private System.Windows.Forms.Label lblSalesMemo;
        private System.Windows.Forms.Label lbl_smemo;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
    }
}