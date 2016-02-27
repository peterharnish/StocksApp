﻿namespace Stocks
{
    partial class PurchasesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchasesForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.purchaseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.Symbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurchaseDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumberOfShares = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrailingStop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Symbol,
            this.PurchaseDate,
            this.TotalPrice,
            this.NumberOfShares,
            this.R,
            this.TrailingStop});
            this.dataGridView1.DataSource = this.purchaseBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(644, 260);
            this.dataGridView1.TabIndex = 0;
            // 
            // purchaseBindingSource
            // 
            this.purchaseBindingSource.DataSource = typeof(Stocks.Entity.Purchase);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(228, 297);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(335, 297);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Symbol
            // 
            this.Symbol.DataPropertyName = "Symbol";
            this.Symbol.HeaderText = "Symbol";
            this.Symbol.Name = "Symbol";
            // 
            // PurchaseDate
            // 
            this.PurchaseDate.DataPropertyName = "PurchaseDate";
            this.PurchaseDate.HeaderText = "Purchase Date";
            this.PurchaseDate.Name = "PurchaseDate";
            // 
            // TotalPrice
            // 
            this.TotalPrice.DataPropertyName = "TotalPrice";
            this.TotalPrice.HeaderText = "Total Price";
            this.TotalPrice.Name = "TotalPrice";
            // 
            // NumberOfShares
            // 
            this.NumberOfShares.DataPropertyName = "NumberOfShares";
            this.NumberOfShares.HeaderText = "Number Of Shares";
            this.NumberOfShares.Name = "NumberOfShares";
            // 
            // R
            // 
            this.R.DataPropertyName = "R";
            this.R.HeaderText = "R";
            this.R.Name = "R";
            // 
            // TrailingStop
            // 
            this.TrailingStop.DataPropertyName = "TrailingStop";
            this.TrailingStop.HeaderText = "Trailing Stop";
            this.TrailingStop.Name = "TrailingStop";
            // 
            // PurchasesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 333);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PurchasesForm";
            this.Text = "Purchases";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource purchaseBindingSource;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Symbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurchaseDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberOfShares;
        private System.Windows.Forms.DataGridViewTextBoxColumn R;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrailingStop;
    }
}