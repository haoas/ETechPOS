namespace ETech
{
    partial class AddUserForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ViewUsersPanel = new System.Windows.Forms.Panel();
            this.cbxActive = new System.Windows.Forms.CheckBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.DGVUsers = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbx_Usercode = new System.Windows.Forms.TextBox();
            this.tbx_Fullname = new System.Windows.Forms.TextBox();
            this.tbx_Password = new System.Windows.Forms.TextBox();
            this.tbx_Username = new System.Windows.Forms.TextBox();
            this.AddOrEditUsersPanel = new System.Windows.Forms.Panel();
            this.btn_UpdateUser = new System.Windows.Forms.Button();
            this.GB_Authorization = new System.Windows.Forms.GroupBox();
            this.cbx_zread = new System.Windows.Forms.CheckBox();
            this.cbx_xread = new System.Windows.Forms.CheckBox();
            this.cbx_pickupcash = new System.Windows.Forms.CheckBox();
            this.cbx_opendrawer = new System.Windows.Forms.CheckBox();
            this.cbx_wholesale = new System.Windows.Forms.CheckBox();
            this.cbx_reprintor = new System.Windows.Forms.CheckBox();
            this.cbx_voidtrans = new System.Windows.Forms.CheckBox();
            this.cbx_seniortrans = new System.Windows.Forms.CheckBox();
            this.cbx_membertrans = new System.Windows.Forms.CheckBox();
            this.cbx_Refunditem = new System.Windows.Forms.CheckBox();
            this.cbx_nonvattrans = new System.Windows.Forms.CheckBox();
            this.cbx_discount = new System.Windows.Forms.CheckBox();
            this.cbx_Removeitem = new System.Windows.Forms.CheckBox();
            this.cbx_ChangeQuantity = new System.Windows.Forms.CheckBox();
            this.cbx_Openitem = new System.Windows.Forms.CheckBox();
            this.cbx_All = new System.Windows.Forms.CheckBox();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ViewUsersPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVUsers)).BeginInit();
            this.AddOrEditUsersPanel.SuspendLayout();
            this.GB_Authorization.SuspendLayout();
            this.SuspendLayout();
            // 
            // ViewUsersPanel
            // 
            this.ViewUsersPanel.Controls.Add(this.cbxActive);
            this.ViewUsersPanel.Controls.Add(this.txtUser);
            this.ViewUsersPanel.Controls.Add(this.lblUser);
            this.ViewUsersPanel.Controls.Add(this.DGVUsers);
            this.ViewUsersPanel.Location = new System.Drawing.Point(12, 12);
            this.ViewUsersPanel.Name = "ViewUsersPanel";
            this.ViewUsersPanel.Size = new System.Drawing.Size(419, 297);
            this.ViewUsersPanel.TabIndex = 9;
            // 
            // cbxActive
            // 
            this.cbxActive.AutoSize = true;
            this.cbxActive.Checked = true;
            this.cbxActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxActive.Location = new System.Drawing.Point(320, 12);
            this.cbxActive.Name = "cbxActive";
            this.cbxActive.Size = new System.Drawing.Size(86, 28);
            this.cbxActive.TabIndex = 12;
            this.cbxActive.Text = "Active";
            this.cbxActive.UseVisualStyleBackColor = true;
            this.cbxActive.CheckedChanged += new System.EventHandler(this.cbxActive_CheckedChanged_1);
            // 
            // txtUser
            // 
            this.txtUser.Font = new System.Drawing.Font("Arial", 14.25F);
            this.txtUser.Location = new System.Drawing.Point(76, 9);
            this.txtUser.MaxLength = 20;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(238, 29);
            this.txtUser.TabIndex = 1;
            this.txtUser.TextChanged += new System.EventHandler(this.txtUser_TextChanged);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(10, 14);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(60, 22);
            this.lblUser.TabIndex = 10;
            this.lblUser.Text = "User:";
            // 
            // DGVUsers
            // 
            this.DGVUsers.AllowUserToAddRows = false;
            this.DGVUsers.AllowUserToDeleteRows = false;
            this.DGVUsers.AllowUserToResizeColumns = false;
            this.DGVUsers.AllowUserToResizeRows = false;
            this.DGVUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.qty,
            this.price,
            this.amount});
            this.DGVUsers.Enabled = false;
            this.DGVUsers.Location = new System.Drawing.Point(14, 44);
            this.DGVUsers.Name = "DGVUsers";
            this.DGVUsers.ReadOnly = true;
            this.DGVUsers.RowHeadersVisible = false;
            this.DGVUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVUsers.Size = new System.Drawing.Size(392, 236);
            this.DGVUsers.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 22);
            this.label2.TabIndex = 11;
            this.label2.Text = "UserCode:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 22);
            this.label1.TabIndex = 12;
            this.label1.Text = "Full Name:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(328, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 22);
            this.label3.TabIndex = 13;
            this.label3.Text = "Password:";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(328, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 22);
            this.label4.TabIndex = 14;
            this.label4.Text = "Username:";
            // 
            // tbx_Usercode
            // 
            this.tbx_Usercode.Font = new System.Drawing.Font("Arial", 14.25F);
            this.tbx_Usercode.Location = new System.Drawing.Point(133, 10);
            this.tbx_Usercode.MaxLength = 20;
            this.tbx_Usercode.Name = "tbx_Usercode";
            this.tbx_Usercode.Size = new System.Drawing.Size(183, 29);
            this.tbx_Usercode.TabIndex = 2;
            // 
            // tbx_Fullname
            // 
            this.tbx_Fullname.Font = new System.Drawing.Font("Arial", 14.25F);
            this.tbx_Fullname.Location = new System.Drawing.Point(133, 45);
            this.tbx_Fullname.MaxLength = 20;
            this.tbx_Fullname.Name = "tbx_Fullname";
            this.tbx_Fullname.Size = new System.Drawing.Size(183, 29);
            this.tbx_Fullname.TabIndex = 3;
            // 
            // tbx_Password
            // 
            this.tbx_Password.Font = new System.Drawing.Font("Arial", 14.25F);
            this.tbx_Password.Location = new System.Drawing.Point(449, 49);
            this.tbx_Password.MaxLength = 20;
            this.tbx_Password.Name = "tbx_Password";
            this.tbx_Password.PasswordChar = '*';
            this.tbx_Password.Size = new System.Drawing.Size(183, 29);
            this.tbx_Password.TabIndex = 5;
            // 
            // tbx_Username
            // 
            this.tbx_Username.Font = new System.Drawing.Font("Arial", 14.25F);
            this.tbx_Username.Location = new System.Drawing.Point(449, 9);
            this.tbx_Username.MaxLength = 20;
            this.tbx_Username.Name = "tbx_Username";
            this.tbx_Username.Size = new System.Drawing.Size(183, 29);
            this.tbx_Username.TabIndex = 4;
            // 
            // AddOrEditUsersPanel
            // 
            this.AddOrEditUsersPanel.Controls.Add(this.btn_UpdateUser);
            this.AddOrEditUsersPanel.Controls.Add(this.GB_Authorization);
            this.AddOrEditUsersPanel.Controls.Add(this.label3);
            this.AddOrEditUsersPanel.Controls.Add(this.tbx_Password);
            this.AddOrEditUsersPanel.Controls.Add(this.label2);
            this.AddOrEditUsersPanel.Controls.Add(this.tbx_Username);
            this.AddOrEditUsersPanel.Controls.Add(this.label1);
            this.AddOrEditUsersPanel.Controls.Add(this.tbx_Fullname);
            this.AddOrEditUsersPanel.Controls.Add(this.label4);
            this.AddOrEditUsersPanel.Controls.Add(this.tbx_Usercode);
            this.AddOrEditUsersPanel.Enabled = false;
            this.AddOrEditUsersPanel.Location = new System.Drawing.Point(437, 12);
            this.AddOrEditUsersPanel.Name = "AddOrEditUsersPanel";
            this.AddOrEditUsersPanel.Size = new System.Drawing.Size(644, 361);
            this.AddOrEditUsersPanel.TabIndex = 18;
            // 
            // btn_UpdateUser
            // 
            this.btn_UpdateUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_UpdateUser.Location = new System.Drawing.Point(489, 303);
            this.btn_UpdateUser.Name = "btn_UpdateUser";
            this.btn_UpdateUser.Size = new System.Drawing.Size(143, 44);
            this.btn_UpdateUser.TabIndex = 16;
            this.btn_UpdateUser.Text = "Update";
            this.btn_UpdateUser.UseVisualStyleBackColor = true;
            this.btn_UpdateUser.Click += new System.EventHandler(this.btn_UpdateUser_Click);
            // 
            // GB_Authorization
            // 
            this.GB_Authorization.Controls.Add(this.cbx_zread);
            this.GB_Authorization.Controls.Add(this.cbx_xread);
            this.GB_Authorization.Controls.Add(this.cbx_pickupcash);
            this.GB_Authorization.Controls.Add(this.cbx_opendrawer);
            this.GB_Authorization.Controls.Add(this.cbx_wholesale);
            this.GB_Authorization.Controls.Add(this.cbx_reprintor);
            this.GB_Authorization.Controls.Add(this.cbx_voidtrans);
            this.GB_Authorization.Controls.Add(this.cbx_seniortrans);
            this.GB_Authorization.Controls.Add(this.cbx_membertrans);
            this.GB_Authorization.Controls.Add(this.cbx_Refunditem);
            this.GB_Authorization.Controls.Add(this.cbx_nonvattrans);
            this.GB_Authorization.Controls.Add(this.cbx_discount);
            this.GB_Authorization.Controls.Add(this.cbx_Removeitem);
            this.GB_Authorization.Controls.Add(this.cbx_ChangeQuantity);
            this.GB_Authorization.Controls.Add(this.cbx_Openitem);
            this.GB_Authorization.Controls.Add(this.cbx_All);
            this.GB_Authorization.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GB_Authorization.Location = new System.Drawing.Point(16, 80);
            this.GB_Authorization.Name = "GB_Authorization";
            this.GB_Authorization.Size = new System.Drawing.Size(619, 217);
            this.GB_Authorization.TabIndex = 15;
            this.GB_Authorization.TabStop = false;
            this.GB_Authorization.Text = "Authorizations";
            // 
            // cbx_zread
            // 
            this.cbx_zread.AutoSize = true;
            this.cbx_zread.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cbx_zread.Location = new System.Drawing.Point(391, 144);
            this.cbx_zread.Name = "cbx_zread";
            this.cbx_zread.Size = new System.Drawing.Size(153, 24);
            this.cbx_zread.TabIndex = 15;
            this.cbx_zread.Text = "Print Z Reading";
            this.cbx_zread.UseVisualStyleBackColor = false;
            // 
            // cbx_xread
            // 
            this.cbx_xread.AutoSize = true;
            this.cbx_xread.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cbx_xread.Location = new System.Drawing.Point(391, 115);
            this.cbx_xread.Name = "cbx_xread";
            this.cbx_xread.Size = new System.Drawing.Size(154, 24);
            this.cbx_xread.TabIndex = 14;
            this.cbx_xread.Text = "Print X Reading";
            this.cbx_xread.UseVisualStyleBackColor = false;
            // 
            // cbx_pickupcash
            // 
            this.cbx_pickupcash.AutoSize = true;
            this.cbx_pickupcash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cbx_pickupcash.Location = new System.Drawing.Point(195, 175);
            this.cbx_pickupcash.Name = "cbx_pickupcash";
            this.cbx_pickupcash.Size = new System.Drawing.Size(127, 24);
            this.cbx_pickupcash.TabIndex = 13;
            this.cbx_pickupcash.Text = "Pickup Cash";
            this.cbx_pickupcash.UseVisualStyleBackColor = false;
            // 
            // cbx_opendrawer
            // 
            this.cbx_opendrawer.AutoSize = true;
            this.cbx_opendrawer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cbx_opendrawer.Location = new System.Drawing.Point(195, 145);
            this.cbx_opendrawer.Name = "cbx_opendrawer";
            this.cbx_opendrawer.Size = new System.Drawing.Size(133, 24);
            this.cbx_opendrawer.TabIndex = 12;
            this.cbx_opendrawer.Text = "Open Drawer";
            this.cbx_opendrawer.UseVisualStyleBackColor = false;
            // 
            // cbx_wholesale
            // 
            this.cbx_wholesale.AutoSize = true;
            this.cbx_wholesale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cbx_wholesale.Location = new System.Drawing.Point(195, 115);
            this.cbx_wholesale.Name = "cbx_wholesale";
            this.cbx_wholesale.Size = new System.Drawing.Size(189, 24);
            this.cbx_wholesale.TabIndex = 11;
            this.cbx_wholesale.Text = "Set Wholesale Price";
            this.cbx_wholesale.UseVisualStyleBackColor = false;
            // 
            // cbx_reprintor
            // 
            this.cbx_reprintor.AutoSize = true;
            this.cbx_reprintor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cbx_reprintor.Location = new System.Drawing.Point(195, 55);
            this.cbx_reprintor.Name = "cbx_reprintor";
            this.cbx_reprintor.Size = new System.Drawing.Size(154, 24);
            this.cbx_reprintor.TabIndex = 10;
            this.cbx_reprintor.Text = "Reprint Receipt";
            this.cbx_reprintor.UseVisualStyleBackColor = false;
            // 
            // cbx_voidtrans
            // 
            this.cbx_voidtrans.AutoSize = true;
            this.cbx_voidtrans.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cbx_voidtrans.Location = new System.Drawing.Point(195, 25);
            this.cbx_voidtrans.Name = "cbx_voidtrans";
            this.cbx_voidtrans.Size = new System.Drawing.Size(163, 24);
            this.cbx_voidtrans.TabIndex = 9;
            this.cbx_voidtrans.Text = "Void Transaction";
            this.cbx_voidtrans.UseVisualStyleBackColor = false;
            // 
            // cbx_seniortrans
            // 
            this.cbx_seniortrans.AutoSize = true;
            this.cbx_seniortrans.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cbx_seniortrans.Location = new System.Drawing.Point(391, 85);
            this.cbx_seniortrans.Name = "cbx_seniortrans";
            this.cbx_seniortrans.Size = new System.Drawing.Size(163, 24);
            this.cbx_seniortrans.TabIndex = 8;
            this.cbx_seniortrans.Text = "Set Senior Trans";
            this.cbx_seniortrans.UseVisualStyleBackColor = false;
            // 
            // cbx_membertrans
            // 
            this.cbx_membertrans.AutoSize = true;
            this.cbx_membertrans.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cbx_membertrans.Location = new System.Drawing.Point(391, 55);
            this.cbx_membertrans.Name = "cbx_membertrans";
            this.cbx_membertrans.Size = new System.Drawing.Size(175, 24);
            this.cbx_membertrans.TabIndex = 7;
            this.cbx_membertrans.Text = "Set Member Trans";
            this.cbx_membertrans.UseVisualStyleBackColor = false;
            // 
            // cbx_Refunditem
            // 
            this.cbx_Refunditem.AutoSize = true;
            this.cbx_Refunditem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cbx_Refunditem.Location = new System.Drawing.Point(6, 115);
            this.cbx_Refunditem.Name = "cbx_Refunditem";
            this.cbx_Refunditem.Size = new System.Drawing.Size(137, 24);
            this.cbx_Refunditem.TabIndex = 6;
            this.cbx_Refunditem.Text = "Refund Items";
            this.cbx_Refunditem.UseVisualStyleBackColor = false;
            // 
            // cbx_nonvattrans
            // 
            this.cbx_nonvattrans.AutoSize = true;
            this.cbx_nonvattrans.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cbx_nonvattrans.Location = new System.Drawing.Point(391, 25);
            this.cbx_nonvattrans.Name = "cbx_nonvattrans";
            this.cbx_nonvattrans.Size = new System.Drawing.Size(171, 24);
            this.cbx_nonvattrans.TabIndex = 5;
            this.cbx_nonvattrans.Text = "Set NonVat Trans";
            this.cbx_nonvattrans.UseVisualStyleBackColor = false;
            // 
            // cbx_discount
            // 
            this.cbx_discount.AutoSize = true;
            this.cbx_discount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cbx_discount.Location = new System.Drawing.Point(195, 85);
            this.cbx_discount.Name = "cbx_discount";
            this.cbx_discount.Size = new System.Drawing.Size(156, 24);
            this.cbx_discount.TabIndex = 4;
            this.cbx_discount.Text = "Make Discounts";
            this.cbx_discount.UseVisualStyleBackColor = false;
            // 
            // cbx_Removeitem
            // 
            this.cbx_Removeitem.AutoSize = true;
            this.cbx_Removeitem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cbx_Removeitem.Location = new System.Drawing.Point(6, 144);
            this.cbx_Removeitem.Name = "cbx_Removeitem";
            this.cbx_Removeitem.Size = new System.Drawing.Size(134, 24);
            this.cbx_Removeitem.TabIndex = 3;
            this.cbx_Removeitem.Text = "Remove Item";
            this.cbx_Removeitem.UseVisualStyleBackColor = false;
            // 
            // cbx_ChangeQuantity
            // 
            this.cbx_ChangeQuantity.AutoSize = true;
            this.cbx_ChangeQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cbx_ChangeQuantity.Location = new System.Drawing.Point(6, 85);
            this.cbx_ChangeQuantity.Name = "cbx_ChangeQuantity";
            this.cbx_ChangeQuantity.Size = new System.Drawing.Size(162, 24);
            this.cbx_ChangeQuantity.TabIndex = 2;
            this.cbx_ChangeQuantity.Text = "Change Quantity";
            this.cbx_ChangeQuantity.UseVisualStyleBackColor = false;
            // 
            // cbx_Openitem
            // 
            this.cbx_Openitem.AutoSize = true;
            this.cbx_Openitem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cbx_Openitem.Location = new System.Drawing.Point(6, 55);
            this.cbx_Openitem.Name = "cbx_Openitem";
            this.cbx_Openitem.Size = new System.Drawing.Size(144, 24);
            this.cbx_Openitem.TabIndex = 1;
            this.cbx_Openitem.Text = "Add OpenItem";
            this.cbx_Openitem.UseVisualStyleBackColor = false;
            // 
            // cbx_All
            // 
            this.cbx_All.AutoSize = true;
            this.cbx_All.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cbx_All.Location = new System.Drawing.Point(6, 25);
            this.cbx_All.Name = "cbx_All";
            this.cbx_All.Size = new System.Drawing.Size(111, 24);
            this.cbx_All.TabIndex = 0;
            this.cbx_All.Text = "All Access";
            this.cbx_All.UseVisualStyleBackColor = false;
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.id.DataPropertyName = "syncid";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.id.DefaultCellStyle = dataGridViewCellStyle5;
            this.id.FillWeight = 200F;
            this.id.HeaderText = "Id";
            this.id.MinimumWidth = 10;
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 41;
            // 
            // qty
            // 
            this.qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.qty.DataPropertyName = "usercode";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.qty.DefaultCellStyle = dataGridViewCellStyle6;
            this.qty.FillWeight = 46.81124F;
            this.qty.HeaderText = "Code";
            this.qty.MinimumWidth = 10;
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            this.qty.Width = 57;
            // 
            // price
            // 
            this.price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.price.DataPropertyName = "fullname";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.price.DefaultCellStyle = dataGridViewCellStyle7;
            this.price.FillWeight = 61.91541F;
            this.price.HeaderText = "Full Name";
            this.price.MinimumWidth = 10;
            this.price.Name = "price";
            this.price.ReadOnly = true;
            // 
            // amount
            // 
            this.amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.amount.DataPropertyName = "username";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.amount.DefaultCellStyle = dataGridViewCellStyle8;
            this.amount.HeaderText = "Username";
            this.amount.MinimumWidth = 10;
            this.amount.Name = "amount";
            this.amount.ReadOnly = true;
            this.amount.Width = 80;
            // 
            // AddUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 425);
            this.Controls.Add(this.AddOrEditUsersPanel);
            this.Controls.Add(this.ViewUsersPanel);
            this.KeyPreview = true;
            this.Name = "AddUserForm";
            this.Text = "AddUserForm";
            this.Load += new System.EventHandler(this.AddUserForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddUserForm_KeyDown);
            this.ViewUsersPanel.ResumeLayout(false);
            this.ViewUsersPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVUsers)).EndInit();
            this.AddOrEditUsersPanel.ResumeLayout(false);
            this.AddOrEditUsersPanel.PerformLayout();
            this.GB_Authorization.ResumeLayout(false);
            this.GB_Authorization.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ViewUsersPanel;
        private System.Windows.Forms.CheckBox cbxActive;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblUser;
        public System.Windows.Forms.DataGridView DGVUsers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbx_Usercode;
        private System.Windows.Forms.TextBox tbx_Fullname;
        private System.Windows.Forms.TextBox tbx_Password;
        private System.Windows.Forms.TextBox tbx_Username;
        private System.Windows.Forms.Panel AddOrEditUsersPanel;
        private System.Windows.Forms.GroupBox GB_Authorization;
        private System.Windows.Forms.CheckBox cbx_All;
        private System.Windows.Forms.CheckBox cbx_Removeitem;
        private System.Windows.Forms.CheckBox cbx_ChangeQuantity;
        private System.Windows.Forms.CheckBox cbx_Openitem;
        private System.Windows.Forms.CheckBox cbx_discount;
        private System.Windows.Forms.CheckBox cbx_nonvattrans;
        private System.Windows.Forms.CheckBox cbx_membertrans;
        private System.Windows.Forms.CheckBox cbx_Refunditem;
        private System.Windows.Forms.CheckBox cbx_reprintor;
        private System.Windows.Forms.CheckBox cbx_voidtrans;
        private System.Windows.Forms.CheckBox cbx_seniortrans;
        private System.Windows.Forms.CheckBox cbx_pickupcash;
        private System.Windows.Forms.CheckBox cbx_opendrawer;
        private System.Windows.Forms.CheckBox cbx_wholesale;
        private System.Windows.Forms.CheckBox cbx_zread;
        private System.Windows.Forms.CheckBox cbx_xread;
        private System.Windows.Forms.Button btn_UpdateUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;

    }
}