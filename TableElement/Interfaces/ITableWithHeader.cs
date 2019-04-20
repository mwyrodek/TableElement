using System.Collections.Generic;
using OpenQA.Selenium;

namespace TableElement.Interfaces
{
    public interface ITableWithHeader : ITable
    {
        IWebElement GetCell(string header, int row);
        IList<ICell> GetColumn(string header);
    }
}