using System.Collections.Generic;
using System.Linq;
using TableElement.Interfaces;

namespace TableElement
{
    public class TableRow: IRow
    {
        public IList<ICell> Cells { get; }
        public ICell GetCell(int positon)
        {
            return Cells[positon];
        }

        public TableRow(List<ICell> cells)
        {
            Cells = cells.ToList();
        }
    }
}