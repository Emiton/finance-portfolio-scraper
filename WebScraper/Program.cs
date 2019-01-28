using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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
            // Create instance of web scraper
            using (IWebDriver webDriver = new ChromeDriver())
            {
                webDriver.Navigate().GoToUrl("https://finance.yahoo.com");

                // define an explicit wait
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));

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
                    IWebElement popUp = webDriver.FindElement(By.XPath("//*[@id=\"__dialog\"]/section"));
                    popUp.Click();
                }

                // Navigate to Portfolio
                webDriver.FindElement(By.XPath("//*[@id=\"Nav-0-DesktopNav\"]/div/div[3]/div/div[1]/ul/li[2]/a")).Click();
                webDriver.FindElement(By.XPath("//*[@id=\"Col1-0-Portfolios-Proxy\"]/main/table/tbody/tr[1]/td[1]/a")).Click();

                webDriver.Manage().Window.Maximize();


                // Grab info
                IList<IWebElement> stockTable = webDriver.FindElements(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody"));
                foreach (var stock in stockTable)
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
