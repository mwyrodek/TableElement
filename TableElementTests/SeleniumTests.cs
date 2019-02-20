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
            
            var table = chromeDriver.FindTable(By.Id("table1"));
            var headerNames = table.GetHeaderNames();
            var webElement = table.GetCell(3,1);
            Assert.Contains(headerNames, x => x.Contains("Due"));
            Assert.Equal("$51.00", webElement.Text);           
        }
        
        [Fact]
        public void TableWithHeader_returns_Value()
        {
            
            
            var table = chromeDriver.FindTableWithHeader(By.Id("table1"));
            var headerNames = table.GetHeaderNames();
            var webElement = table.GetCell("Email",1);
            Assert.Equal("fbach@yahoo.com", webElement.Text);           
        }
        
        [Fact]
        public void Table_MapToObject()
        {
            
            var table = chromeDriver.FindTableWithHeader(By.Id("table1"));
            var mapper = new Mapper();
            var map = new Dictionary<string, int>();
            map.Add("LastName",0);
            map.Add("FirstName",1);
            map.Add("Email",2);
            map.Add("Due",3);
            map.Add("WebSite",4);
            var dataTableExampleOnes = mapper.MapTableToObjectList<DataTableExampleOne>(table, map);
            
            Assert.Equal(dataTableExampleOnes.First().LastName, "Smith");
            Assert.Equal(dataTableExampleOnes.First().FirstName, "John");
            Assert.Equal(dataTableExampleOnes.Last().WebSite, "http://www.timconway.com");
                       
        }
        
        [Fact]
        public void TableWithHeader_MapToObject()
        {
            
            var table = chromeDriver.FindTableWithHeader(By.Id("table1"));
            var mapper = new Mapper();
            var dataTableExampleOnes = mapper.MapTableToObjectList<DataTableExampleOne>(table);
            
            Assert.Equal(dataTableExampleOnes.First().LastName, "Smith");
            Assert.Equal(dataTableExampleOnes.First().FirstName, "John");
            Assert.Equal(dataTableExampleOnes.Last().WebSite, "http://www.timconway.com");
                       
        }

        public SeleniumTests()
        {
            chromeDriver = new ChromeDriver(@"C:\git\chromedriver");
            chromeDriver.Navigate().GoToUrl("http://the-internet.herokuapp.com/tables");
        }

        public void Dispose()
        {
            chromeDriver.Quit();
        }
    }
}