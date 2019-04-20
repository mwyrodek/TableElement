using System.Collections.Generic;

namespace TableElement.Interfaces
{
    public interface IRow
    {
        IList<ICell> Cells{ get;}

        ICell GetCell(int positon);
    }
}