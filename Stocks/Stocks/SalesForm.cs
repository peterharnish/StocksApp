using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Logging;
using Stocks.BusinessRules;
using Stocks.Entity;

namespace Stocks
{
    public partial class SalesForm : Form
    {
        public SalesForm()
        {
            InitializeComponent();
            this.dataGridView1.Columns["SaleDate"].DefaultCellStyle.Format = "d";
            this.dataGridView1.Columns["TotalPrice"].DefaultCellStyle.Format = "c";
        }

        public SalesList Sales { get; set; }

        /// <summary>
        /// Data grid view. 
        /// </summary>
        public DataGridView DGView { get { return this.dataGridView1; } }

        /// <summary>
        /// Data repository. 
        /// </summary>
        public static Stocks.DataAccess.IRepository Repository;

        /// <summary>
        /// Saves the sales. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            LogHelper.LogInfo("Entering SalesForm.btnOK_Click.");

            try
            {
                BR br = new BR(Repository);

                foreach (Sale sale in (List<Sale>)this.dataGridView1.DataSource)
                {
                    if ((sale.ID == 0) && !string.IsNullOrEmpty(sale.Symbol))
                    {
                        sale.Symbol = sale.Symbol.ToUpper();
                        br.SellStock(sale);
                    }
                }

                this.Close();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }

            LogHelper.LogInfo("Exiting SalesForm.btnOK_Click.");
        }

        /// <summary>
        /// Closes the form. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            LogHelper.LogInfo("Entering SalesForm.btnCancel_Click.");
            this.Close();

            LogHelper.LogInfo("Exiting SalesForm.btnCancel_Click.");
        }
    }
}
