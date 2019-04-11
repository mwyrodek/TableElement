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
                throw new HeaderMissMatchException("Table body and header");
            }
        }

        public IWebElement GetCell(string header, int row)
        {
            var position = GetHeaderPosition(header);
            return GetCell(position, row);
        }

        private int GetHeaderPosition(string header)
        {
            if (ColumnHeaders.All(ie => ie.Text != header)) //todo add test for exception
            {
                throw new HeaderNotFoundException($"Header {header} was not found.");
            }

            var element = ColumnHeaders.First(ie => ie.Text == header);
            var position = ColumnHeaders.IndexOf(element);
            return position;
        }

        //todo add test
        public IList<ICell> GetColumn(string header)
        {
            var position = GetHeaderPosition(header);
            return GetColumn(position);
        }
    }
}