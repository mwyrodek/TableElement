using System;
using System.Collections.Generic;
using System.Linq;
using TableElement.Interfaces;

namespace TableElement
{
    public class Mapper : IMappers
    {
        /// <summary>
        ///     maps field to object based on map - PropertyName, ColumnNumber
        /// </summary>
        /// <param name="table"></param>
        /// <param name="map"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IList<T> MapTableToObjectList<T>(ITableWithHeader table, IDictionary<string, int> map) where T : new()
        {
            return table.Rows.Select(row => CastRow<T>(row, map)).ToList();
        }

        /// <summary>
        ///     maps collumn to object based on properties and headers name. ignoring whitespaces
        /// </summary>
        /// <param name="table"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IList<T> MapTableToObjectList<T>(ITableWithHeader table) where T : new()
        {
            var headers = RemoveWhiteSpaces(table);

            return table.Rows.Select(row => CastRow<T>(row, headers)).ToList();
        }

        private T CastRow<T>(IRow row, IDictionary<string, int> map) where T : new()
        {
            var props = typeof(T).GetProperties();
            var foo = new T();
            foreach (var propertyInfo in props)
            {
                var columnNumber = map[propertyInfo.Name];
                var elementText = row.GetCell(columnNumber).Element.Text;

                if (propertyInfo.CanWrite) propertyInfo.SetValue(foo, elementText, null);
            }

            return foo;
        }

        private IList<string> RemoveWhiteSpaces(ITable table)
        {
            return table.GetHeaderNames().Select(header => header.Replace(" ", "")).ToList();
        }


        private T CastRow<T>(IRow row, IList<string> headers) where T : new()
        {
            var props = typeof(T).GetProperties();
            var foo = new T();
            foreach (var propertyInfo in props)
            {
                var columnNumber = headers.IndexOf(propertyInfo.Name);
                var elementText = row.GetCell(columnNumber).Element.Text;

                if (propertyInfo.CanWrite) propertyInfo.SetValue(foo, elementText, null);
            }

            return foo;
        }
    }
}