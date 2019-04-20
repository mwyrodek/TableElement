using System.Collections.Generic;

namespace TableElement
{
    public interface IRow
    {
        IList<ICell> Cells{ get;}

        ICell GetCell(int positon);
    }
}