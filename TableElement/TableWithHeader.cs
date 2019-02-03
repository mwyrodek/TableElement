using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using TableElement.Exception;

namespace TableElement
{
    public class TableWithHeader : TableElement, ITableWithHeader
    {
        public TableWithHeader(IWebElement tableRoot) : base(tableRoot)
        {
            if (Rows.Any(r => r.Cells.Count != ColumnHeaders.Count))
            {
                throw new HeaderMissMatch("Table body and header");
            }
        }

        public IWebElement GetCell(string header, int row)
        {
            var position = GetHeadePosition(header);
            return GetCell(position, row);
        }

        private int GetHeadePosition(string header)
        {
            if (ColumnHeaders.All(ie => ie.Text != header))
            {
                throw new HeaderNotFound($"Header {header} was not found.");
            }

            var element = ColumnHeaders.First(ie => ie.Text == header);
            var position = ColumnHeaders.IndexOf(element);
            return position;
        }

        public IList<ICell> GetColumn(string header)
        {
            var position = GetHeadePosition(header);
            return GetColumn(position);
        }
    }
}