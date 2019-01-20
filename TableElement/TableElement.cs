using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using OpenQA.Selenium;
using TableElement.Exception;

namespace TableElement
{
    public class TableElement : ITable
    {
        public TableElement(IWebElement tableRoot)
        {
            Table = tableRoot;
            Rows = new List<IRow>();
            Cells = new List<ICell>();
            IsTable();
            FillHeader();
            FillBody();


        }

        private void FillBody()
        {
            var rows = Table.FindElements(By.CssSelector("tr"));
            bool hasHeader = ColumnHeaders.Count > 0;
            int iterator = hasHeader ? 1 : 0;

            for (int i = iterator; i < rows.Count; i++)
            {
                if(hasHeader)
                {
                    CreateRow(rows[i], i-1);
                }
                else
                {
                    CreateRow(rows[i], i);
                }
            }
        }

        private void CreateRow(IWebElement row, int rowNumber)
        {
            var cells= row.FindElements(By.CssSelector("td"));
            var list = new List<ICell>();
            for (int i = 0; i < cells.Count; i++)
            {
                ICell cell = new Cell(i, rowNumber, cells[i]);
                list.Add(cell);
                Cells.Add(cell);
            }
            
            Rows.Add(new TableRow(list));
        }

        public IWebElement Table { get; }

        public IList<IWebElement> ColumnHeaders { get; private set; }

        public IList<ICell> Cells { get; private set; }

        public IList<IRow> Rows { get; private set; }

        public IRow GetRow(int rowNumber)
        {
            return Rows[rowNumber];
        }

        public IWebElement GetCell(int column, int row)
        {
            return Cells.FirstOrDefault(cell => (cell.Row==row && cell.Column==column))?.Element;
        }

        public IList<string> GetHeaderNames()
        {
            return ColumnHeaders.Select(x => x.Text).ToList();
        }

        private void FillHeader()
        {
            ColumnHeaders = Table.FindElements(By.CssSelector("th")).ToList();
        }

        private void IsTable()
        {
            try
            {
                var tbody = Table.FindElement(By.CssSelector("tr"));
            }
            catch (NotFoundException e)
            {
                throw new TableNotFoundException("Table was not found", e);
            }
        }
        
    }
}