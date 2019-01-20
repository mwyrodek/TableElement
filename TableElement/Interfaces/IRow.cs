using System.Collections.Generic;
using OpenQA.Selenium;

namespace TableElement
{
    public interface IRow
    {
        IList<ICell> Cells{ get;}

        ICell GetCell(int positon);
    }
}