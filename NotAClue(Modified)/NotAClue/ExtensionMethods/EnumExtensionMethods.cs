using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Web.DynamicData;
using System.Web.UI.WebControls;
using System.Collections;
//using NotAClue.ComponentModel.DataAnnotations;

namespace NotAClue
{
    public static class EnumExtensionMethods
    {
        /// <summary>
        /// Gets the underlying type value string.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="enumValue">The enum value.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetUnderlyingTypeValueString(Type enumType, object enumValue)
        {
            return Convert.ChangeType(enumValue, Enum.GetUnderlyingType(enumType)).ToString();
        }

        /// <summary>
        /// Gets the enum names and values.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static IOrderedDictionary GetEnumNamesAndValues(Type enumType)
        {
            OrderedDictionary result = new OrderedDictionary();
            foreach (object enumValue in Enum.GetValues(enumType))
            {
                // TODO add way to localize the displayed name
                string name = Enum.GetName(enumType, enumValue);
                string value = EnumExtensionMethods.GetUnderlyingTypeValueString(enumType, enumValue);
                result.Add(name, value);
            }
            return result;
        }

        /// <summary>
        /// Toes the list.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static List<int> ToList(Type enumType)
        {
            var result = new List<int>();
            foreach (object enumValue in Enum.GetValues(enumType))
            {
                result.Add((int)enumValue);
            }
            return result;
        }

        /// <summary>
        /// Gets the enum names and hex values.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Dictionary<String, String> GetEnumNamesAndHexValues(Type enumType)
        {
            var result = new Dictionary<String, String>();
            foreach (object enumValue in Enum.GetValues(enumType))
            {
                string name = Enum.GetName(enumType, enumValue);
                var value = ("000000" + ((int)enumValue).ToString("X"));
                var hex = "#" + value.Substring(value.Length - 6, 6);
                result.Add(name, hex);
            }
            return result;
        }

        /// <summary>
        /// Fills the enum list control.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="enumType">Type of the enum.</param>
        /// <remarks></remarks>
        public static void FillEnumListControl(ListControl list, Type enumType)
        {
            foreach (DictionaryEntry entry in EnumExtensionMethods.GetEnumNamesAndValues(enumType))
            {
                list.Items.Add(new ListItem((string)entry.Key, (string)entry.Value));
            }
        }

        /// <summary>
        /// Gets the enum list.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static List<ListItem> GetEnumList(Type enumType)
        {
            var list = new List<ListItem>();
            foreach (DictionaryEntry entry in EnumExtensionMethods.GetEnumNamesAndValues(enumType))
            {
                list.Add(new ListItem((string)entry.Key, (string)entry.Value));
            }
            return list;
        }

        /// <summary>
        /// Determines whether [is enum type in flags mode] [the specified enum type].
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns>
        /// 	<c>true</c> if [is enum type in flags mode] [the specified enum type]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEnumTypeInFlagsMode(Type enumType)
        {
            return enumType.GetCustomAttributes(typeof(FlagsAttribute), false).Length != 0;
        }

        /// <summary>
        /// Gets the enum string from string.
        /// </summary>
        /// <param name="enumString">The enum string.</param>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns></returns>
        public static String GetEnumStringFromString(this String enumString, Type enumType)
        {
            var value = Enum.Parse(enumType, enumString);
            return value.ToString();
        }

        /// <summary>
        /// Toes the enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumString">The enum string.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static T ToEnum<T>(this String enumString) //where T : Enum
        {
            var value = Enum.Parse(typeof(T), enumString);
            return (T)value;
        }

        /// <summary>
        /// Toes the enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumInt">The enum int.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static T ToEnum<T>(this int enumInt) //where T : Enum
        {
            var value = Enum.Parse(typeof(T), enumInt.ToString());
            return (T)value;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumInt">The enum int.</param>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        /// <remarks></remarks>
        public static String ToString<T>(this int enumInt) //where T : Enum
        {
            var value = Enum.Parse(typeof(T), enumInt.ToString()).ToString();
            return value;
        }
    }
}
