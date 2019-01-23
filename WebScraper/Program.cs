using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebScraper
{
    class Program
    {
        /// <summary>
        /// Entry point for the scraper.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("*****STARTING*****");

            // Create instance of web scraper
            using (IWebDriver webDriver = new ChromeDriver())
            {
                webDriver.Navigate().GoToUrl("https://finance.yahoo.com");

                //webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                //webDriver.FindElement(By.Id("uh-signedin")).Click();

                try
                {
                    webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                    webDriver.FindElement(By.Id("uh-signedin")).Click();
                }
                catch (OpenQA.Selenium.NoSuchElementException ex)
                {
                    Console.WriteLine(ex.Message);
                    // throw new OpenQA.Selenium.NoSuchElementException("ID not found");
                }


                try
                {
                    webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                    IWebElement email = webDriver.FindElement(By.Id("login-username"));
                    email.SendKeys("financetester321@gmail.com");
                    webDriver.FindElement(By.XPath("//*[@id=\"login-signin\"]")).Click();
                    Console.WriteLine("GOT THE EMAIL BABAYYYY!");
                }
                catch (OpenQA.Selenium.NoSuchElementException ex)
                {
                    Console.WriteLine(ex.Message);
                }


                try
                {
                    webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                    IWebElement password = webDriver.FindElement(By.Id("login-passwd"));
                    password.SendKeys("thisisnew1234");
                    webDriver.FindElement(By.Id("login-signin")).Click();
                    Console.WriteLine("WE LOGGED IN!");
                }
                catch (OpenQA.Selenium.NoSuchElementException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                // Navigate to Portfolio
                webDriver.FindElement(By.XPath("//*[@id=\"Nav-0-DesktopNav\"]/div/div[3]/div/div[1]/ul/li[2]/a")).Click();
                webDriver.FindElement(By.XPath("//*[@id=\"Col1-0-Portfolios-Proxy\"]/main/table/tbody/tr[1]/td[1]/a")).Click();

                webDriver.Manage().Window.Maximize();
                Console.WriteLine("READY TO EXTRACT DATA");


                // Grab info
                var stocks = webDriver.FindElements(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody"));
                foreach (var stock in stocks)
                    Console.WriteLine(stock.Text);


                // Test click
                webDriver.FindElement(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody/tr[1]/td[1]/a")).Click();






                // TODO: ****Next steps****
                // Create a stock object that will be sent to database
                // Rewrite function to access stock data
                // Manipulate stock objects somehow (by printing or whatever)
                // Create database connection
                // Break up code into methods
                // Add explicit waits for all calls
                // Add pop-up dodger




                // Might need this a popup handler
                //var alert = webDriver.FindElement(By.XPath("//dialog[@id = '__dialog']/section/button"));
                //alert.Click();

            }


        }

    }
}
