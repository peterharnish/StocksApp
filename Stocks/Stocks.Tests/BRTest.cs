using Stocks.BusinessRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Stocks.DataAccess;
using Stocks.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Stocks.Tests
{
    
    
    /// <summary>
    ///This is a test class for BRTest and is intended
    ///to contain all BRTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BRTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GetHistory
        ///</summary>
        [TestMethod()]
        public void GetHistoryTest()
        {
            TestRepository rep = new TestRepository();
            rep.Positions = new List<Position>(new Position[] { new Position() { Purchases = new List<Purchase>(), Sales = new List<Sale>(), Dividends = new List<Dividend>() } });
            BR target = new BR(rep);
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            List<Position> actual;
            actual = target.GetHistory(startDate, endDate);
            Assert.AreNotEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for GetCurrent
        ///</summary>
        [TestMethod()]
        public void GetCurrentTest()
        {
            TestRepository rep = new TestRepository();
            rep.Positions = new List<Position>(new Position[] { new Position() { Purchases = new List<Purchase>(), Sales = new List<Sale>(), Dividends = new List<Dividend>() } });
            BR target = new BR(rep);
            List<Position> actual = target.GetCurrent();
            Assert.AreNotEqual(0, actual.Count);
        }

        [TestMethod()]
        public void CalcTotalSharesOwnedTest()
        {
            TestRepository rep = new TestRepository();
            int numShares = 1;
            Purchase purchase = new Purchase(){NumberOfShares = numShares} ;
            rep.Positions = new List<Position>(new Position[] { new Position() { Purchases = new List<Purchase>(new Purchase[] { purchase}), Sales = new List<Sale>(), Dividends = new List<Dividend>() } });
            BR target = new BR(rep);
            List<Position> actual = target.GetCurrent();
            Assert.AreEqual(numShares, actual.First().TotalSharesOwned);
        }

        [TestMethod()]
        public void CalcTotalInvestedTest()
        {
            TestRepository rep = new TestRepository();
            decimal totalPrice = 1m;
            Purchase purchase = new Purchase() {TotalPrice = totalPrice, NumberOfShares = 1 };
            rep.Positions = new List<Position>(new Position[] { new Position() { Purchases = new List<Purchase>(new Purchase[] { purchase }), Sales = new List<Sale>(), Dividends = new List<Dividend>() } });
            BR target = new BR(rep);
            List<Position> actual = target.GetCurrent();
            Assert.AreEqual(totalPrice, actual.First().TotalInvested);
        }

        [TestMethod()]
        public void CalcTotalRTest()
        {
            TestRepository rep = new TestRepository();
            decimal totalR = 1m;
            Purchase purchase = new Purchase() { R = totalR, NumberOfShares = 1 };
            rep.Positions = new List<Position>(new Position[] { new Position() { Purchases = new List<Purchase>(new Purchase[] { purchase }), Sales = new List<Sale>(), Dividends = new List<Dividend>() } });
            BR target = new BR(rep);
            List<Position> actual = target.GetCurrent();
            Assert.AreEqual(totalR, actual.First().TotalR);
        }

        [TestMethod()]
        public void CalcTotalDividendsTest()
        {
            TestRepository rep = new TestRepository();
            decimal totalDividends = 1m;
            Dividend dividend = new Dividend() { Amount = totalDividends};
            Purchase purchase = new Purchase() {NumberOfShares = 1 };
            rep.Positions = new List<Position>(new Position[] { new Position() { Purchases = new List<Purchase>(new Purchase[] { purchase }), Sales = new List<Sale>(), Dividends = new List<Dividend>(new Dividend[] { dividend }) } });
            BR target = new BR(rep);
            List<Position> actual = target.GetCurrent();
            Assert.AreEqual(totalDividends, actual.First().TotalDividends);
        }

        [TestMethod()]
        public void CalcTotalProfitTest()
        {
            TestRepository rep = new TestRepository();
            decimal totalProfit = 1m;
            Sale sale = new Sale() { TotalPrice = totalProfit };
            Purchase purchase = new Purchase() { NumberOfShares = 1};
            rep.Positions = new List<Position>(new Position[] { new Position() { Purchases = new List<Purchase>(new Purchase[] { purchase }), Sales = new List<Sale>(new Sale[] { sale}), Dividends = new List<Dividend>() } });
            BR target = new BR(rep);
            List<Position> actual = target.GetCurrent();
            Assert.AreEqual(totalProfit, actual.First().TotalProfit);
        }

        [TestMethod()]
        public void CalcProfitOverRTest()
        {
            TestRepository rep = new TestRepository();
            double profitOverR = 1;
            Sale sale = new Sale() { TotalPrice = 1m };
            Purchase purchase = new Purchase() { NumberOfShares = 1, R = 1 };
            rep.Positions = new List<Position>(new Position[] { new Position() { Purchases = new List<Purchase>(new Purchase[] { purchase }), Sales = new List<Sale>(new Sale[] { sale }), Dividends = new List<Dividend>() } });
            BR target = new BR(rep);
            List<Position> actual = target.GetCurrent();
            Assert.AreEqual(profitOverR, actual.First().ProfitOverR);
        }

        [TestMethod()]
        public void CalcTargetSalePriceTest()
        {
            TestRepository rep = new TestRepository();
            decimal targetSalePrice = 0.75m;
            Purchase purchase = new Purchase() { NumberOfShares = 1 };
            rep.Positions = new List<Position>(new Position[] { new Position() {High = 1m, TrailingStop = 0.25m, Purchases = new List<Purchase>(new Purchase[] { purchase }), Sales = new List<Sale>(), Dividends = new List<Dividend>() } });
            BR target = new BR(rep);
            List<Position> actual = target.GetCurrent();
            Assert.AreEqual(targetSalePrice, actual.First().TargetSalePrice);
        }

        /// <summary>
        ///A test for PurchaseShares
        ///</summary>
        [TestMethod()]
        public void PurchaseSharesTest()
        {
            TestRepository rep = new TestRepository();
            BR target = new BR(rep);
            Purchase purchase = new Purchase() { Symbol = "XXX", TrailingStop = 0.25m };
            int expected = 1;
            int actual;
            actual = target.PurchaseShares(purchase);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AddDividend
        ///</summary>
        [TestMethod()]
        public void AddDividendTest()
        {
            TestRepository rep = new TestRepository();
            BR target = new BR(rep);
            Dividend dividend = new Dividend() { Symbol = "XXX" };
            int expected = 1;
            int actual;
            actual = target.AddDividend(dividend);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetPositionBySymbol
        ///</summary>
        [TestMethod()]
        public void GetPositionBySymbolTest()
        {
            TestRepository rep = new TestRepository();
            BR target = new BR(rep);
            string symbol = "XXX";
            Position actual;
            actual = target.GetPositionBySymbol(symbol);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for SellStock
        ///</summary>
        [TestMethod()]
        public void SellStockTest()
        {
            TestRepository rep = new TestRepository();
            BR target = new BR(rep);
            Sale sale = new Sale() { Symbol = "XXX" };
            int expected = 1;
            int actual;
            actual = target.SellStock(sale);
            Assert.AreEqual(expected, actual);
        }
    }
}
