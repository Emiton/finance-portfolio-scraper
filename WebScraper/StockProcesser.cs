using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebScraper
{
    /// <summary>
    ///     This class will process stocks by taking a stockTable and turning
    ///     each row into its own stockObject
    /// </summary>
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
            return stockTable;
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
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
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
            Console.ResetColor();
        }

        // TODO: Repeated code, refactor in order to be overloaded for ints and strings
        public void WriteVisibleConsoleMessage(int numberToPrint)
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string symbolBuffer = "$%$%$$%$%$%$%$%$%$%$%$%$%$%$%$%$%";
            Console.WriteLine(symbolBuffer);
            Console.WriteLine("     ");

            Console.WriteLine(numberToPrint);

            Console.WriteLine("     ");
            Console.WriteLine(symbolBuffer);
            Console.ResetColor();
        }

        // TODO: May not need ref since modifying attribute not actual object
        // ref keyword was used in function call
        public void AddEntry(ref StockObject currentStock, string information, int columnId)
        {
            switch (columnId)
            {
                case 1:
                    currentStock.Symbol = information;
                    break;

                case 2:
                    currentStock.LastPrice = information;
                    break;

                case 3:
                    currentStock.ValueChange = information;
                    break;

                case 4:
                    currentStock.PercentChange = information;
                    break;

                case 5:
                    currentStock.Currency = information;
                    break;

                case 6:
                    currentStock.MarketTime = information;
                    break;

                case 7:
                    currentStock.Volume = information;
                    break;

                case 9:
                    currentStock.AverageVolume3M = information;
                    break;

                case 13:
                    currentStock.MarketCap = information;
                    break;

                default:
                    break;
            }
        }

        public List<StockObject> listOfStocks = new List<StockObject>();

    }
}
