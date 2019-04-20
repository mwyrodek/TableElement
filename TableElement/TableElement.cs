using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using TableElement.Exception;

namespace TableElement
{
    public class TableElement : ITable
    {
        public virtual IWebElement Table { get; }

        public IList<IWebElement> ColumnHeaders { get; private set; }

        public IList<ICell> Cells { get; private set; }

        public IList<IRow> Rows { get; private set; }

        public TableElement(IWebElement tableRoot)
        {
            ValidateTableOnInit(tableRoot);
            Table = tableRoot;
            Rows = new List<IRow>();
            Cells = new List<ICell>();
            FillHeader();
            FillBody();
        }

        /// <summary>
        /// Returns the valuses of all headers
        /// </summary>
        /// <returns>list of string elements are sorted from left to right</returns>
        public IList<string> GetHeaderNames()
        {
            return ColumnHeaders.Select(x => x.Text).ToList();
        }

        /// <summary>
        /// Returns selected row from body
        /// Won't return rows from header and footer
        /// </summary>
        /// <param name="rowNumber">row number - start from 0</param>
        /// <returns>row</returns>
        public IRow GetRow(int rowNumber) //todo addtests
        {
            return Rows[rowNumber];
        }
        /// <summary>
        /// Returns cell from table body
        /// Header and Footer not included
        /// </summary>
        /// <param name="column">0 - is fisrt collumn from left</param>
        /// <param name="row">O - is first row from the top</param>
        /// <returns></returns>
        public IWebElement GetCell(int column, int row)
        {
            return Cells.FirstOrDefault(cell => (cell.Row == row && cell.Column == column))?.Element;
        }

        /// <summary>
        /// retruns all cells in given collumn
        /// WARNING function may return incorrect cells in rows with merged cells.
        /// it will retrun null for such row.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public IList<ICell> GetColumn(int column) //todo add test for both tipes
        {
            List<ICell> cells = new List<ICell>();
            foreach (var row in Rows)
            {
                if (row.Cells.Count<= column)
                {
                    cells.Add(null);
                }
                else
                {
                    cells.Add(row.GetCell(column));
                }
            }

            return cells;
        }

        private void CreateRow(IWebElement row, int rowNumber)
        {
            var cells = row.FindElements(By.CssSelector("td"));
            var list = new List<ICell>();
            for (int i = 0; i < cells.Count; i++)
            {
                ICell cell = new Cell(i, rowNumber, cells[i]);
                list.Add(cell);
                Cells.Add(cell);
            }

            Rows.Add(new TableRow(list));
        }

        private void FillBody()
        {
            var rows = Table.FindElements(By.CssSelector("tr"));
            //todo maybe this should be moved to fill header ergo row with headers should be removed after filling?
            bool hasHeader = ColumnHeaders.Count > 0;
            int iterator = hasHeader ? 1 : 0;

            for (int i = iterator; i < rows.Count; i++)
            {
                if (hasHeader)
                {
                    //first row will contiain hearedrs;
                    CreateRow(rows[i], i - 1);
                }
                else //todo cover it with test
                {
                    CreateRow(rows[i], i);
                }
            }
        }


        private void FillHeader()
        {
            ColumnHeaders = Table.FindElements(By.CssSelector("th")).ToList();
        }

        private static void ValidateTableOnInit(IWebElement tableRoot)
        {
            try
            {
                tableRoot.FindElement(By.CssSelector("tr"));
            }
            catch (NotFoundException e)
            {
                throw new TableNotFoundException("Table was not found", e);
            }
        }
    }
}