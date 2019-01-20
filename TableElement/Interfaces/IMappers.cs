using System.Collections.Generic;

namespace TableElement
{
    public interface IMappers
    {
        IList<dynamic> MapTabpleToObjet(ITable Table);
        IList<T> MapTabpleToObjet<T>(ITable Table, IDictionary<string,int> Map);
        IList<T> MapTabpleToObjetHeaderBased<T>(ITable Table);
    }
}