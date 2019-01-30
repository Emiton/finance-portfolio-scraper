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

        // PROCESSES NEEDED
        // Get table
        // Get row
        // Process row
        // Store in StockObject
        // Send to database

        public IWebElement GetStockTable(IWebDriver driver)
        {
            IWebElement stockTable = driver.FindElement(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody"));
        }

        public List<StockObject> ParseStockTable(IList<IWebElement> stocks)
        {
            List<StockObject> parsedStocks = new List<StockObject>();
            int numberOfStocks = stocks.Count;
            Console.WriteLine(numberOfStocks);
            return parsedStocks;
        }

        // TODO
        // If this is kept, put it in it's own static class
        public void WriteVisibleConsoleMessage(string message)
        {
            int messageLength = message.Length;
            string symbolBuffer = "$%$%$$%$%$%$%$%$%$%$%$%$%$%$%$%$%";
            Console.WriteLine(symbolBuffer);
            Console.WriteLine( "     " );

            for (int i = 0; i < messageLength; i++)
            {
                string currentCharacter = message[i].ToString();
                Console.WriteLine(currentCharacter.ToUpper());
                Console.WriteLine("  ");
            }

            Console.WriteLine( "     " );
            Console.WriteLine(symbolBuffer);
        }

        // TODO: Repeated code, refactor in order to be overloaded for ints and strings
        public void WriteVisibleConsoleMessage(int numberToPrint)
        {
            string symbolBuffer = "$%$%$$%$%$%$%$%$%$%$%$%$%$%$%$%$%";
            Console.WriteLine(symbolBuffer);
            Console.WriteLine("     ");

            Console.WriteLine(numberToPrint);

            Console.WriteLine("     ");
            Console.WriteLine(symbolBuffer);
        }
    }
}
