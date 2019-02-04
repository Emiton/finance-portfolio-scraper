using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Data.Entity;
using System.Data.SqlClient;

namespace WebScraper
{
    class Program
    {

        /// <summary>
        ///     Entry point for the scraper.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ChromeOptions options = new ChromeOptions();


            // Create instance of web scraper
            using (var db = new StockDatabase())
            using (IWebDriver webDriver = new ChromeDriver(options))
            {
                // TODO: Suppress not working
                // Suppress certificate errors
               //options.AddArgument("--ignore-certificate-errors");
               //options.AddArgument("--ignore-ssl-errors");
               webDriver.Navigate().GoToUrl("https://finance.yahoo.com");

                // define an explicit wait
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));

                // SIGN IN PROCESS
                wait.Until<IWebElement>(d => d.FindElement(By.Id("uh-signedin")));
                webDriver.FindElement(By.Id("uh-signedin")).Click();

                // Sign in using email and password

                wait.Until<IWebElement>(d => d.FindElement(By.Id("login-username")));
                IWebElement email = webDriver.FindElement(By.Id("login-username"));
                email.SendKeys("financetester321@gmail.com");
                webDriver.FindElement(By.XPath("//*[@id=\"login-signin\"]")).Click();

                wait.Until<IWebElement>(d => d.FindElement(By.Id("login-passwd")));
                IWebElement password = webDriver.FindElement(By.Id("login-passwd"));
                password.SendKeys("thisisnew1234");
                webDriver.FindElement(By.Id("login-signin")).Click();


                // Deal with potential pop-up
                webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                IList<IWebElement> popUps =  webDriver.FindElements(By.XPath("//*[@id=\"__dialog\"]/section"));

                if (popUps.Count != 0)
                {
                    IWebElement popUpExit = webDriver.FindElement(By.XPath("//*[@id=\"__dialog\"]/section"));
                    popUpExit.Click();
                }

                // Navigate to Portfolio
                webDriver.FindElement(By.XPath("//*[@id=\"Nav-0-DesktopNav\"]/div/div[3]/div/div[1]/ul/li[2]/a")).Click();
                webDriver.FindElement(By.XPath("//*[@id=\"Col1-0-Portfolios-Proxy\"]/main/table/tbody/tr[1]/td[1]/a")).Click();

                webDriver.Manage().Window.Maximize();

                // TODO : Get rid of this line
                webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

                // GRABBING STOCKS
                // Grab all stocks
                IList<IWebElement> tempTable = webDriver.FindElements(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody")); // cannot find this same Xpath again for some reason

                IWebElement stockTable =
                    webDriver.FindElement(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody"));

                IList<IWebElement> stockList = stockTable.FindElements(By.TagName("tr"));

                int numberOfRows = stockList.Count;

                IWebElement firstRow = webDriver.FindElement(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody/tr[1]"));

                // TODO : Get rid of this line
                webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                int numberOfColumns = firstRow.FindElements(By.TagName("td")).Count;
                IList<IWebElement> currentRows = firstRow.FindElements(By.TagName("td"));

                var messagePrinter = new StockProcesser();

                messagePrinter.WriteVisibleConsoleMessage("stock table");
                messagePrinter.WriteVisibleConsoleMessage(numberOfRows);
                messagePrinter.WriteVisibleConsoleMessage(numberOfColumns);

                // PROCESS STOCKS

                StockProcesser stockProcessor = new StockProcesser();

                // XPath elements start at 1 index
                Console.BackgroundColor = ConsoleColor.DarkRed;
                for (int i = 1; i < numberOfRows; i++)
                {
                    var tempStock = new StockObject();
                    tempStock.ScrapeTime = DateTime.Now.ToString("G");

                    messagePrinter.WriteVisibleConsoleMessage("new stock");
                    // Last two columns are not needed, subtract 1
                    for (int j = 1; j < numberOfColumns - 1; j++)
                    {
                        //string currentText = webDriver.FindElement( By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody/tr[1]") ).Text;
                        string currentText = webDriver.FindElement( By.XPath($"//*[@id=\"pf-detail-table\"]/div[1]/table/tbody/tr[{i}]/td[{j}]") ).Text;
                        stockProcessor.AddEntry(ref tempStock, currentText, j);
                        Console.WriteLine($"{j} --> {currentText}");
                    }

                    stockProcessor.listOfStocks.Add(tempStock);
                }

                Console.ResetColor();

                messagePrinter.WriteVisibleConsoleMessage("check stock objects");

                foreach (var stock in stockProcessor.listOfStocks)
                {
                    Console.WriteLine(stock.Symbol);
                    Console.WriteLine(stock.LastPrice);
                    Console.WriteLine(stock.PercentChange);
                    Console.WriteLine(stock.Currency);
                    Console.WriteLine(stock.MarketTime);
                    Console.WriteLine(stock.MarketCap);
                }


                // Put stocks in database
                foreach (var stock in stockProcessor.listOfStocks)
                {
                    db.StockObjects.Add(stock);
                    db.SaveChanges();
                }

                // Get stocks from database and print
                var query = from stock in db.StockObjects
                    orderby stock.Symbol
                    select stock;

                messagePrinter.WriteVisibleConsoleMessage("all stock messages");

                foreach (var stock in query)
                {
                    Console.WriteLine("SYMBOL " + stock.Symbol);
                    Console.WriteLine("LAST PRICE " + stock.LastPrice);
                    Console.WriteLine("AVERAGE VOLUME 3M " + stock.AverageVolume3M);
                }
            }
        }
    }
}
