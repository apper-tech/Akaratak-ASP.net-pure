using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace NotAClue
{
    public static class ArrayExtensionMethods
    {
        /// <summary>
        /// Converts a CSV to a List of type int.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public static List<int> ToIntList(this String list)
        {
            var invoices = new List<int>();

            // split string
            var csvList = list.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // extract ints to of List<int>
            foreach (var invoiceId in csvList)
            {
                int value;
                if (int.TryParse(invoiceId, out value))
                    invoices.Add(value);
            }
            return invoices;
        }

        /// <summary>
        /// Toes the CSV string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public static String ToCsvString<T>(this List<T> list)
        {
            var toString = new StringBuilder();
            foreach (var item in list)
            {
                toString.Append(item.ToString() + ",");
            }
            return toString.ToString().Substring(0, toString.Length - 1);
        }

        /// <summary>
        /// Toes the CSV string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public static String ToCsvString<T>(this ReadOnlyCollection<T> list)
        {
            var toString = new StringBuilder();
            foreach (var item in list)
            {
                toString.Append(item.ToString() + ",");
            }
            return toString.ToString().Substring(0, toString.Length - 1);
        }

        /// <summary>
        /// Toes the CSV string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public static String ToCsvString<T>(this T[] list)
        {
            var toString = new StringBuilder();
            foreach (var item in list)
            {
                toString.Append(item.ToString() + ",");
            }
            return toString.ToString().Substring(0, toString.Length - 1);
        }
    }
}
