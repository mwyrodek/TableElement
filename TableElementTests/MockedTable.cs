using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Moq;
using OpenQA.Selenium;

namespace TableElementTests
{
    internal class MockedTable
    {
        private readonly Mock<IWebElement> mockTableRootElement = new Mock<IWebElement>();

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
            var thList = header.Select(CreateMockElementWithText).ToList();

            var thCollection = new ReadOnlyCollection<IWebElement>(thList);
            mockTableRootElement.Setup(foo => foo.FindElements(By.CssSelector("th")))
                .Returns(thCollection);

            return mockTableRootElement.Object;
        }

        internal IWebElement CreateNotATable()
        {
            mockTableRootElement.Setup(foo => foo.FindElement(By.CssSelector("tr")))
                .Throws(new NotFoundException());

            var trList = new List<IWebElement>();
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
            var tdList = texts.Select(CreateMockElementWithText).ToList();

            var tdCollection = new ReadOnlyCollection<IWebElement>(tdList);
            var trElement = new Mock<IWebElement>();
            trElement.Setup(foo => foo.FindElements(By.CssSelector("td"))).Returns(tdCollection);
            return trElement.Object;
        }

        private void CreateBody(string[][] body)
        {
            var trList = body.Select(CreateMockElementReturnsList).ToList();

            var trCollection = new ReadOnlyCollection<IWebElement>(trList);

            mockTableRootElement.Setup(foo => foo.FindElement(By.CssSelector("tr")))
                .Returns(trCollection.First());
            mockTableRootElement.Setup(foo => foo.FindElements(By.CssSelector("tr")))
                .Returns(trCollection);
        }
    }
}