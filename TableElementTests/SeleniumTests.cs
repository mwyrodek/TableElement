using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TableElement;
using Xunit;

namespace TableElementTests
{
    public class SeleniumTests : IDisposable
    {
        private ChromeDriver chromeDriver;

        //todo make it proper test
        [Fact]
        public void BasicTable_has_Header()
        {
            
            chromeDriver.Navigate().GoToUrl("http://the-internet.herokuapp.com/tables");
            var table = chromeDriver.FindTable(By.Id("table1"));
            var headerNames = table.GetHeaderNames();
            var webElement = table.GetCell(3,1);
            Assert.Contains(headerNames, x => x.Contains("Due"));
            Assert.Equal("$51.00", webElement.Text);           
        }
        
        [Fact]
        public void TableWithHeader_returns_Value()
        {
            
            chromeDriver.Navigate().GoToUrl("http://the-internet.herokuapp.com/tables");
            var table = chromeDriver.FindTableWithHeader(By.Id("table1"));
            var headerNames = table.GetHeaderNames();
            var webElement = table.GetCell("Email",1);
            Assert.Equal("fbach@yahoo.com", webElement.Text);           
        }

        public SeleniumTests()
        {
            chromeDriver = new ChromeDriver(@"C:\git\chromedriver");
        }

        public void Dispose()
        {
            chromeDriver.Quit();
        }
    }
}