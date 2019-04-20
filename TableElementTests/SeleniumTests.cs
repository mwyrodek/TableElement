using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TableElement;
using Xunit;

namespace TableElementTests
{
    public class SeleniumTests : IDisposable
    {
        public SeleniumTests()
        {
            var path = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);
            chromeDriver = new ChromeDriver(path);
            chromeDriver.Navigate().GoToUrl("http://the-internet.herokuapp.com/tables");
        }

        public void Dispose()
        {
            chromeDriver.Quit();
        }

        private readonly ChromeDriver chromeDriver;

        [Fact]
        public void BasicTable_has_Header()
        {
            var table = chromeDriver.FindTable(By.Id("table1"));
            var headerNames = table.GetHeaderNames();
            var webElement = table.GetCell(3, 1);
            Assert.Contains(headerNames, x => x.Contains("Due"));
            Assert.Equal("$51.00", webElement.Text);
        }

        [Fact]
        public void Table_MapToObject()
        {
            var table = chromeDriver.FindTableWithHeader(By.Id("table1"));
            var mapper = new Mapper();
            var map = new Dictionary<string, int>
            {
                ["LastName"] = 0,
                ["FirstName"] = 1,
                ["Email"] = 2,
                ["Due"] = 3,
                ["WebSite"] = 4
            };

            var dataTableExampleOnes = mapper.MapTableToObjectList<DataTableExampleOne>(table, map);

            Assert.Equal("Smith", dataTableExampleOnes.First().LastName);
            Assert.Equal("John", dataTableExampleOnes.First().FirstName);
            Assert.Equal("http://www.timconway.com", dataTableExampleOnes.Last().WebSite);
        }

        [Fact]
        public void TableWithHeader_MapToObject()
        {
            var table = chromeDriver.FindTableWithHeader(By.Id("table1"));
            var mapper = new Mapper();
            var dataTableExampleOnes = mapper.MapTableToObjectList<DataTableExampleOne>(table);

            Assert.Equal("Smith", dataTableExampleOnes.First().LastName);
            Assert.Equal("John", dataTableExampleOnes.First().FirstName);
            Assert.Equal("http://www.timconway.com", dataTableExampleOnes.Last().WebSite);
        }

        [Fact]
        public void TableWithHeader_returns_Value()
        {
            var table = chromeDriver.FindTableWithHeader(By.Id("table1"));
            var webElement = table.GetCell("Email", 1);
            Assert.Equal("fbach@yahoo.com", webElement.Text);
        }
    }
}