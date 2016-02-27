using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Logging;
using System.Windows.Forms;
using Stocks.BusinessRules;
using Stocks.Entity;

namespace Stocks
{
    public partial class PurchasesForm : Form
    {
        public PurchasesForm()
        {
            InitializeComponent();
            this.dataGridView1.Columns["PurchaseDate"].DefaultCellStyle.Format = "d";
            this.dataGridView1.Columns["TotalPrice"].DefaultCellStyle.Format = "c";
            this.dataGridView1.Columns["R"].DefaultCellStyle.Format = "c";
            this.dataGridView1.Columns["TrailingStop"].DefaultCellStyle.Format = "P0";
        }

        public PurchasesList Purchases { get; set; }

        /// <summary>
        /// Data repository.
        /// </summary>
        public static Stocks.DataAccess.IRepository Repository;

        /// <summary>
        /// Data grid view. 
        /// </summary>
        public DataGridView DGView { get { return this.dataGridView1; } }

        /// <summary>
        /// Saves the purchases. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            LogHelper.LogInfo("Entering PurchasesForm.btnOK_Click.");

            try
            {
                BR br = new BR(Repository);

                foreach (Purchase purchase in (List<Purchase>)this.dataGridView1.DataSource)
                {
                    if ((purchase.ID == 0) && !string.IsNullOrEmpty(purchase.Symbol))
                    {
                        purchase.Symbol = purchase.Symbol.ToUpper();
                        br.PurchaseShares(purchase);
                    }
                }

                this.Close();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }

            LogHelper.LogInfo("Exiting PurchasesForm.btnOK_Click.");
        }

        /// <summary>
        /// Closes the form. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            LogHelper.LogInfo("Entering PurchasesForm.btnCancel_Click.");
            this.Close();
            LogHelper.LogInfo("Exiting PurchasesForm.btnCancel_Click.");
        }
    }
}
