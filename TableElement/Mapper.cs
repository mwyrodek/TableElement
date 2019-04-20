using System;
using System.Collections.Generic;
using TableElement.Interfaces;

namespace TableElement
{
    public class Mapper : IMappers
    {
        /// <summary>
        /// maps field to object based on map - PropertyName, ColumnNumber
        /// </summary>
        /// <param name="table"></param>
        /// <param name="map"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IList<T> MapTableToObjectList<T>(ITableWithHeader table, IDictionary<string, int> map) where T : new()
        {
            var list = new List<T>();
            foreach (var row in table.Rows)
            {
                list.Add(CastRow<T>(row, map));
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

                if (propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(foo, elementText, null);
                }
            }

            return foo;
        }

        /// <summary>
        ///  maps collumn to object based on properties and headers name. ignoring whitespaces 
        /// </summary>
        /// <param name="table"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IList<T> MapTableToObjectList<T>(ITableWithHeader table) where T : new()
        {
            var list = new List<T>();
            var headers = RemoveWhiteSpaces(table);

            foreach (var row in table.Rows)
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

                if (propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(foo, elementText, null);
                }
            }

            return foo;
        }
    }
}