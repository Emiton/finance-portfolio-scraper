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

            }



        }

    }
}
