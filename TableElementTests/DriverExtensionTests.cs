using Moq;
using OpenQA.Selenium;
using TableElement;
using TableElement.Exception;
using Xunit;

namespace TableElementTests
{
    public class DriverExtensionTests
    {
        [Fact]
        public void NoTableUnderSelector_Throws_Exception()
        {
            var elementMock = new Mock<IWebElement>();
            elementMock.Setup(foo => foo.FindElement(It.IsAny<By>())).Throws(new NotFoundException());

            var mock = new Mock<IWebDriver>();
            mock.Setup(foo => foo.FindElement(It.IsAny<By>())).Returns(elementMock.Object);

            var driver = mock.Object;

            var tableNotFoundException =
                Assert.Throws<TableNotFoundException>(() => driver.FindTable(By.CssSelector("Xxx")));

            Assert.Equal("Table was not found", tableNotFoundException.Message);
        }
    }
}