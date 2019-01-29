using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebScraper
{
    class StockProcesser
    {
        public List<StockObject> ParseStockTable(IList<IWebElement> stocks)
        {
            List<StockObject> parsedStocks = new List<StockObject>();
            int numberOfStocks = stocks.Count;
            Console.WriteLine(numberOfStocks);
            return parsedStocks;
        }
    }
}
