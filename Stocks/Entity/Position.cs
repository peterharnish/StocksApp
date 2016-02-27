using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stocks.Entity
{
    public class Position
    {
        public int ID { get; set; }
        public string Symbol { get; set; }
        public DateTime? DateOpened { get; set; }
        public decimal? CurrentPrice { get; set; }
        public decimal? High { get; set; }
        public decimal TrailingStop { get; set; }
        public DateTime? DateClosed { get; set; }
        public decimal? TargetSalePrice { get; set; }
        public decimal? TotalProfit { get; set; }
        public double? TotalSharesOwned { get; set; }
        public decimal TotalInvested { get; set; }
        public decimal TotalR { get; set; }
        public bool IsOpen { get; set; }
        public decimal TotalDividends { get; set; }
        public double ProfitOverR { get; set; }
        public List<Purchase> Purchases { get; set; }
        public List<Dividend> Dividends { get; set; }
        public List<Sale> Sales { get; set; }
        public bool IsTrailingStopHigh { get; set; }
        public bool IsCurrentPriceLow { get; set; }
    }
}
