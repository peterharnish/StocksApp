using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stocks.Entity
{
    public class Dividend
    {
        public int ID { get; set; }
        public int PositionID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string Symbol { get; set; }
    }
}
