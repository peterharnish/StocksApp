using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Logging;
using Stocks.BusinessRules;
using Stocks.DataAccess;
using Stocks.Entity;

namespace Stocks.UpdateStockPrice
{
    public class Program
    {
        static void Main(string[] args)
        {
            LogHelper.LogInfo("Entering Main.");

            try
            {
                FetchCurrentStockPrices();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message, ex);
            }

            LogHelper.LogInfo("Exiting Main.");
        }

        /// <summary>
        /// Fetches the current stock prices. 
        /// </summary>
        private static void FetchCurrentStockPrices()
        {
            LogHelper.LogInfo("Entering FetchCurrentStockPrices.");

            BusinessRules.BR br = new BR(new Stocks.DataAccess.Repository());
            List<Position> positions = br.GetCurrent().Where(x => x.ID > 0).ToList<Position>();

            foreach (var position in positions)
            {
                try
                {
                    decimal price = FetchCurrentPrice(position.Symbol);

                    if (price > 0)
                    {
                        position.CurrentPrice = price;
                    }

                    br.UpdateStockPrice(position);
                }
                catch (Exception ex)
                {
                    LogHelper.LogError(ex.Message, ex);
                }
            }

            LogHelper.LogInfo("Exiting FetchCurrentStockPrices.");
        }

        /// <summary>
        /// Fetches the current stock price. 
        /// </summary>
        /// <param name="symbol"> Stock symbol. </param>
        /// <returns> Stock price. </returns>
        private static decimal FetchCurrentPrice(string symbol)
        {
            LogHelper.LogInfo(string.Format("Entering FetchCurrentPrice with symbol = {0}.", symbol));

            string url = string.Format(System.Configuration.ConfigurationManager.AppSettings["url"], symbol);
            decimal price = 0;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            WebResponse response = request.GetResponse();
            XmlDocument document = new XmlDocument();
            document.Load(response.GetResponseStream());
            XmlNodeList list = document.GetElementsByTagName("price");

            if (list.Count > 0)
            {
                price = decimal.Parse(list[0].InnerText);
            }

            LogHelper.LogInfo(string.Format("Exiting FetchCurrentPrice with price = {0}.", price.ToString()));
            return price;
        }
    }
}
