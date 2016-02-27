using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stocks.Entity
{
    public class Purchase
    {
        public int ID { get; set; }
        public int PositionID { get; set; }
        public int NumberOfShares { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal R { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Symbol { get; set; }
        public decimal TrailingStop { get; set; }
    }
}
