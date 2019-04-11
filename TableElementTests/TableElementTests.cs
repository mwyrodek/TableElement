using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TableElement;
using TableElement.Exception;
using Xunit;

namespace TableElementTests
{
    public class TableElementTests
    {
        //check if add header when there is header
        //check if it wont add header when there isn't header
        //check if rows are filled
        
        private string[][] expectedBody={
            new[] { "c0r0", "c1r0","c2r0" },
            new[]{ "c0r1", "c1r1","c2r1" },
            new[] { "c0r2", "c1r2","c2r2" }
        };

        private string[] expectedHeader = {"h1", "h2", "h3"};
        [Fact]
        public void CreateElement_ValidInput_NoHeader()
        {
            var realTableNoHeader = new MockedTable().CreateTableNoHeader(expectedBody);
            var tableElement = new TableElement.TableElement(realTableNoHeader);
            
            Assert.Equal(3, tableElement.Rows.Count);
            Assert.Equal(9, tableElement.Cells.Count);
            Assert.Equal(0, tableElement.ColumnHeaders.Count);
        }
        
        [Fact]
        public void CreateElement_ValidInput_Headers()
        {
            var realTableWithHeader =  new MockedTable().CreateTableWithHeader(expectedBody, expectedHeader);
            var tableElement = new TableElement.TableElement(realTableWithHeader);
            
            Assert.Equal(2, tableElement.Rows.Count);
            Assert.Equal(6, tableElement.Cells.Count);
            Assert.Equal(3, tableElement.ColumnHeaders.Count);
        }

        [Fact]
        public void CreateElement_InvalidInput_ThrowsException()
        {

            var notATable =  new MockedTable().CreateNotATable();
            Exception ex =
                Assert.Throws<TableNotFoundException>(() => new TableElement.TableElement(notATable));
        }

        [Fact]
        public void GetCell_NoHeaderProperCell()
        {
            var realTableNoHeader = new MockedTable().CreateTableNoHeader(expectedBody);
            var tableElement = new TableElement.TableElement(realTableNoHeader);

            var actualCellText = tableElement.GetCell(0, 1).Text;
            var expectedCellText = expectedBody[1][0];
            Assert.Equal(expectedCellText,actualCellText);
        }

        /*public IWebElement NotATable()
        {
            var mockTableRootElement = new Mock<IWebElement>();
            mockTableRootElement.Setup(foo => foo.FindElement(By.CssSelector("tr")))
                .Throws(new NotFoundException());


            var trList = new List<IWebElement>() { };
            var trCollection = new ReadOnlyCollection<IWebElement>(trList);

            mockTableRootElement.Setup(foo => foo.FindElements(By.CssSelector("tr")))
                .Returns(trCollection);
            return mockTableRootElement.Object;
        }


        public IWebElement RealTableNoHeader()
        {
            var td1Element = new Mock<IWebElement>();
            var td2Element = new Mock<IWebElement>();
            var td3Element = new Mock<IWebElement>();
            var tdList = new List<IWebElement>() {td1Element.Object, td2Element.Object, td3Element.Object};
            var tdCollection = new ReadOnlyCollection<IWebElement>(tdList);

            var trElement = new Mock<IWebElement>();
            trElement.Setup(foo => foo.FindElements(By.CssSelector("td"))).Returns(tdCollection);

            var trList = new List<IWebElement>() {trElement.Object, trElement.Object, trElement.Object};
            var trCollection = new ReadOnlyCollection<IWebElement>(trList);


            var mockTableRootElement = new Mock<IWebElement>();
            mockTableRootElement.Setup(foo => foo.FindElement(By.CssSelector("tr")))
                
                .Returns(trElement.Object);
            mockTableRootElement.Setup(foo => foo.FindElements(By.CssSelector("tr")))
                .Returns(trCollection);

            mockTableRootElement.Setup(foo => foo.FindElements(By.CssSelector("th")))
                .Returns(new ReadOnlyCollection<IWebElement>(new List<IWebElement>()));

            return mockTableRootElement.Object;
        }
        
        public IWebElement RealTableWithHeader()
        {
            var td1Element = new Mock<IWebElement>();
            var td2Element = new Mock<IWebElement>();
            var td3Element = new Mock<IWebElement>();
            var tdList = new List<IWebElement>() {td1Element.Object, td2Element.Object, td3Element.Object};
            var tdCollection = new ReadOnlyCollection<IWebElement>(tdList);

            var trElement = new Mock<IWebElement>();
            trElement.Setup(foo => foo.FindElements(By.CssSelector("td"))).Returns(tdCollection);

            var trList = new List<IWebElement>() {trElement.Object, trElement.Object, trElement.Object};
            var trCollection = new ReadOnlyCollection<IWebElement>(trList);


            var mockTableRootElement = new Mock<IWebElement>();
            mockTableRootElement.Setup(foo => foo.FindElement(By.CssSelector("tr")))
                
                .Returns(trElement.Object);
            mockTableRootElement.Setup(foo => foo.FindElements(By.CssSelector("tr")))
                .Returns(trCollection);

            mockTableRootElement.Setup(foo => foo.FindElements(By.CssSelector("th")))
                .Returns(tdCollection);
            
            
            
            return mockTableRootElement.Object;
        }*/
    }

    internal class MockedTable
    {
        Mock<IWebElement> mockTableRootElement = new Mock<IWebElement>();
        internal IWebElement CreateTableNoHeader(string[][] body)
        {
            CreateBody(body);                
            mockTableRootElement.Setup(foo => foo.FindElements(By.CssSelector("th")))
                .Returns(new ReadOnlyCollection<IWebElement>(new List<IWebElement>()));
            
            return mockTableRootElement.Object;
        }

        internal IWebElement CreateTableWithHeader(string[][] body, string[] header)
        {
                CreateBody(body);                
                var thList = new List<IWebElement>();
                foreach (var text in header)
                {
                    thList.Add(CreateMockElementWithText(text));
                }
                var thCollection = new ReadOnlyCollection<IWebElement>(thList);
                mockTableRootElement.Setup(foo => foo.FindElements(By.CssSelector("th")))
                    .Returns(thCollection);
             
                return mockTableRootElement.Object;
        }
        
        internal IWebElement CreateNotATable()
        {

            mockTableRootElement.Setup(foo => foo.FindElement(By.CssSelector("tr")))
                .Throws(new NotFoundException());

            var trList = new List<IWebElement>() { };
            var trCollection = new ReadOnlyCollection<IWebElement>(trList);

            mockTableRootElement.Setup(foo => foo.FindElements(By.CssSelector("tr")))
                .Returns(trCollection);
            return mockTableRootElement.Object;
        }
        private IWebElement CreateMockElementWithText(string text)
        {
            var tdElement = new Mock<IWebElement>();
            tdElement.Setup(foo => foo.Text).Returns(text);
            return tdElement.Object;
        }
        
        private IWebElement CreateMockElementReturnsList(string[] texts)
        {
            var tdList = new List<IWebElement>();
            foreach (var text in texts)
            {
                tdList.Add(CreateMockElementWithText(text));
            }
            var tdCollection = new ReadOnlyCollection<IWebElement>(tdList);
            var trElement = new Mock<IWebElement>();
            trElement.Setup(foo => foo.FindElements(By.CssSelector("td"))).Returns(tdCollection);
            return trElement.Object;
        }

        private void CreateBody(string[][] body)
        {
            var trList = new List<IWebElement>();
            foreach (var text in body)
            {
                trList.Add(CreateMockElementReturnsList(text));
            }
            var trCollection = new ReadOnlyCollection<IWebElement>(trList);
                
            mockTableRootElement.Setup(foo => foo.FindElement(By.CssSelector("tr")))
                .Returns(trCollection.First());
            mockTableRootElement.Setup(foo => foo.FindElements(By.CssSelector("tr")))
                .Returns(trCollection);
        }
    }
}