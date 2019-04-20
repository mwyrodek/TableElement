using System.Collections.Generic;
using System.Linq;
using TableElement.Interfaces;

namespace TableElement
{
    public class TableRow : IRow
    {
        public TableRow(IEnumerable<ICell> cells)
        {
            Cells = cells.ToList();
        }

        public IList<ICell> Cells { get; }

        public ICell GetCell(int positon)
        {
            return Cells[positon];
        }
    }
}