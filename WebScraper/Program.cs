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
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            string test = "This is ";
            string test2 = "a test";
            Console.WriteLine(test + test2);

            // Create instance of web scraper
            using (IWebDriver webDriver = new ChromeDriver())
            {
                webDriver.Navigate().GoToUrl("https://finance.yahoo.com");

                webDriver.FindElement(By.Id("uh-signedin")).Click();
            }

        }

    }
}
