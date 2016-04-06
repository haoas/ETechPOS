namespace ETech.Views.Controls
{
    partial class TransactionControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblTotalAmountTxt = new System.Windows.Forms.Label();
            this.lblTransaction = new System.Windows.Forms.Label();
            this.lblTransactionTxt = new System.Windows.Forms.Label();
            this.lblFunction = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.Location = new System.Drawing.Point(149, 57);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(76, 24);
            this.lblTotalAmount.TabIndex = 8;
            this.lblTotalAmount.Text = "Amount";
            // 
            // lblTotalAmountTxt
            // 
            this.lblTotalAmountTxt.AutoSize = true;
            this.lblTotalAmountTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmountTxt.Location = new System.Drawing.Point(16, 57);
            this.lblTotalAmountTxt.Name = "lblTotalAmountTxt";
            this.lblTotalAmountTxt.Size = new System.Drawing.Size(127, 24);
            this.lblTotalAmountTxt.TabIndex = 7;
            this.lblTotalAmountTxt.Text = "Total Amount:";
            // 
            // lblTransaction
            // 
            this.lblTransaction.AutoSize = true;
            this.lblTransaction.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransaction.Location = new System.Drawing.Point(149, 33);
            this.lblTransaction.Name = "lblTransaction";
            this.lblTransaction.Size = new System.Drawing.Size(108, 24);
            this.lblTransaction.TabIndex = 6;
            this.lblTransaction.Text = "Transaction";
            // 
            // lblTransactionTxt
            // 
            this.lblTransactionTxt.AutoSize = true;
            this.lblTransactionTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransactionTxt.Location = new System.Drawing.Point(16, 33);
            this.lblTransactionTxt.Name = "lblTransactionTxt";
            this.lblTransactionTxt.Size = new System.Drawing.Size(113, 24);
            this.lblTransactionTxt.TabIndex = 5;
            this.lblTransactionTxt.Text = "Transaction:";
            // 
            // lblFunction
            // 
            this.lblFunction.AutoSize = true;
            this.lblFunction.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFunction.Location = new System.Drawing.Point(128, 9);
            this.lblFunction.Name = "lblFunction";
            this.lblFunction.Size = new System.Drawing.Size(51, 24);
            this.lblFunction.TabIndex = 9;
            this.lblFunction.Text = "[ FN]";
            // 
            // TransactionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblFunction);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.lblTotalAmountTxt);
            this.Controls.Add(this.lblTransaction);
            this.Controls.Add(this.lblTransactionTxt);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "TransactionControl";
            this.Size = new System.Drawing.Size(309, 101);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblTotalAmountTxt;
        private System.Windows.Forms.Label lblTransaction;
        private System.Windows.Forms.Label lblTransactionTxt;
        private System.Windows.Forms.Label lblFunction;
    }
}
