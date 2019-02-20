using System;
using System.Collections.Generic;
using System.Linq;
using TableElement.Exception;

namespace TableElement
{
    public class Mapper : IMappers
    {
        /// <summary>
        /// maps field to object based on map - PropertyName, ColumnNumber
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="Map"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IList<T> MapTableToObjectList<T>(ITableWithHeader Table, IDictionary<string, int> Map) where T : new()
        {
            var list = new List<T>();
            foreach (var row in Table.Rows)
            {
                list.Add(CastRow<T>(row, Map));
            }

            return list;
        }

        private T CastRow<T>(IRow row, IDictionary<string, int> map) where T : new()
        {
            var props = typeof(T).GetProperties();
            var foo = new T();
            foreach (var propertyInfo in props)
            {
                var columnNumber = map[propertyInfo.Name];
                var elementText = row.GetCell(columnNumber).Element.Text;

                if (null != propertyInfo && propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(foo, elementText, null);
                }
            }

            return foo;
        }

        /// <summary>
        ///  maps collumn to object based on properties and headers name. ignoring whitespaces 
        /// </summary>
        /// <param name="Table"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IList<T> MapTableToObjectList<T>(ITableWithHeader Table) where T : new()
        {
            var list = new List<T>();
            var headers = RemoveWhiteSpaces(Table);

            foreach (var row in Table.Rows)
            {
                list.Add(CastRow<T>(row, headers));
            }

            return list;
        }

        private IList<string> RemoveWhiteSpaces(ITableWithHeader table)
        {
            var list = new List<string>();
            foreach (var header in table.GetHeaderNames())
            {
                list.Add(header.Replace(" ", ""));
            }

            return list;
        }


        private T CastRow<T>(IRow row, IList<string> headers) where T : new()
        {
            var props = typeof(T).GetProperties();
            var foo = new T();
            foreach (var propertyInfo in props)
            {
                var columnNumber = headers.IndexOf(propertyInfo.Name);
                var elementText = row.GetCell(columnNumber).Element.Text;

                if (null != propertyInfo && propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(foo, elementText, null);
                }
            }

            return foo;
        }
    }
}