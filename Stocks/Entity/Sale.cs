using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stocks.Entity
{
    public class Sale
    {
        public int ID { get; set; }
        public int PositionID { get; set; }
        public DateTime SaleDate { get; set; }
        public double NumberOfShares { get; set; }
        public decimal TotalPrice { get; set; }
        public string Symbol { get; set; }
        public bool IsClose { get; set; }
    }
}
