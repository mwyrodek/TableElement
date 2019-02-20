using System.Collections.Generic;

namespace TableElement
{
    public interface IMappers
    {
        IList<T> MapTableToObjectList<T>(ITableWithHeader Table, IDictionary<string,int> Map) where T : new();
        IList<T> MapTableToObjectList<T>(ITableWithHeader Table) where T : new();
    }
}