using System.Collections.Generic;

namespace TableElement.Interfaces
{
    public interface IMappers
    {
        IList<T> MapTableToObjectList<T>(ITableWithHeader table, IDictionary<string, int> map) where T : new();
        IList<T> MapTableToObjectList<T>(ITableWithHeader table) where T : new();
    }
}