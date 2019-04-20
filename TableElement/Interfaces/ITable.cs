using System.Collections.Generic;
using OpenQA.Selenium;

namespace TableElement.Interfaces
{
    public interface ITable
    {
        IWebElement Table { get; }
        IList<IWebElement> ColumnHeaders{ get; }
        IList<ICell> Cells{ get; }
        IList<IRow> Rows{ get; }

        //doesnt count footer and header
        IRow GetRow(int rowNumber);

        IWebElement GetCell(int column, int row);
        IList<ICell> GetColumn(int column);

        IList<string> GetHeaderNames();


    }
}