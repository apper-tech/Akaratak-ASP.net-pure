using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Web.DynamicData;

namespace NotAClue
{
    public static class CollectionExtensionMethods
    {
        /// <summary>
        /// Toes the comma separated string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public static String ToCommaSeparatedString<T>(this List<T> list)
        {
            var toString = new StringBuilder();
            foreach (var item in list)
            {
                toString.Append(item.ToString() + ",");
            }
            return toString.ToString().Substring(0, toString.Length - 1);
        }

        /// <summary>
        /// Toes the comma separated string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public static String ToCommaSeparatedString<T>(this ReadOnlyCollection<T> list)
        {
            var toString = new StringBuilder();
            foreach (var item in list)
            {
                toString.Append(item.ToString() + ",");
            }
            return toString.ToString().Substring(0, toString.Length - 1);
        }

        //public static String ToCommaSeparatedString(this ReadOnlyCollection<MetaColumn> list)
        //{
        //    var toString = new StringBuilder();
        //    foreach (var column in list)
        //    {
        //        toString.Append(column.Name + ",");
        //    }
        //    return toString.ToString().Substring(0, toString.Length - 1);
        //}
    }
}
