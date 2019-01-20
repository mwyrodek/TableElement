using OpenQA.Selenium;

namespace TableElement
{
    public class Cell : ICell
    {
        public Cell(int column, int row, IWebElement element)
        {
            Column = column;
            Row = row;
            Element = element;
        }

        public int Column { get;  }
        public int Row { get;  }
        public IWebElement Element { get;  }
    }
}