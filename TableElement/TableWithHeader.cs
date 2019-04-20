using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using TableElement.Exception;

namespace TableElement
{
    public class TableWithHeader : TableElement, ITableWithHeader
    {
        /// <summary>
        /// Create Table with addtional header functionality
        /// </summary>
        /// <param name="tableRoot">DOM NODE with Table tag</param>
        /// <exception cref="HeaderMissMatchException">Exception hapens if any row has diffrent columnt count then the header count</exception>
        public TableWithHeader(IWebElement tableRoot) : base(tableRoot)
        {
            if (Rows.Any(r => r.Cells.Count != ColumnHeaders.Count))
            {
                throw new HeaderMissMatchException("Table body and header");
            }
        }

        /// <summary>
        /// Returns cell from given body row (rows in foother and header are not included)
        /// Based on header name 
        /// </summary>
        /// <param name="header">Name of header</param>
        /// <param name="row">Body row - 0 is first from top</param>
        /// <returns></returns>
        public IWebElement GetCell(string header, int row)
        {
            var position = GetHeaderPosition(header);
            return GetCell(position, row);
        }

        /// <summary>
        /// Returns all cells from selected header
        /// </summary>
        /// <param name="header">header text</param>
        /// <returns>list of cell in given column</returns>
        public IList<ICell> GetColumn(string header)
        {
            var position = GetHeaderPosition(header);
            return GetColumn(position);
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
    }
}