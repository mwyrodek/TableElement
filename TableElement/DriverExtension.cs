using System;
using OpenQA.Selenium;

namespace TableElement
{
    public static class DriverExtension
    {
        public static ITable FindTable(this IWebDriver webDriver, By by)
        {
            var findElement = webDriver.FindElement(@by);
            return new TableElement(findElement);
        }
    }
}