using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.DynamicData;
using System.ComponentModel;

namespace NotAClue.Web.DynamicData
{
    public static class AttributeExtensionMethods
    {
        /// <summary>
        /// Determines whether the specified descriptor has attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="descriptor">The descriptor.</param>
        /// <returns>
        /// 	<c>true</c> if the specified descriptor has attribute; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean HasAttribute<T>(this PropertyDescriptor descriptor) where T : Attribute
        {
            Boolean value = false;
            for (int i = 0; i < descriptor.Attributes.Count && !value; i++)
            {
                value = (descriptor.Attributes[i] is T);
            }

            return value;
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="descriptor">The descriptor.</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this PropertyDescriptor descriptor) where T : Attribute
        {
            return descriptor.Attributes.OfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// Gets the attribute or default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attributes">The attributes.</param>
        /// <returns></returns>
        public static T GetAttributeOrDefault<T>(this AttributeCollection attributes) where T : Attribute, new()
        {
            return attributes.OfType<T>().DefaultIfEmpty(new T()).FirstOrDefault();
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attributes">The attributes.</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this AttributeCollection attributes) where T : Attribute
        {
            return attributes.OfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// Get the attribute or a default instance of the attribute
        /// if the Table attribute do not contain the attribute
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="table">
        /// Table to search for the attribute on.
        /// </param>
        /// <returns>
        /// The found attribute or a default 
        /// instance of the attribute of type T
        /// </returns>
        public static T GetAttributeOrDefault<T>(this MetaTable table) where T : Attribute, new()
        {
            return table.Attributes.OfType<T>().DefaultIfEmpty(new T()).FirstOrDefault();
        }

        /// <summary>
        /// Get the attribute of type T or null if not found
        /// </summary>
        /// <typeparam name="T">
        /// Attribute type
        /// </typeparam>
        /// <param name="table">
        /// Table to search for the attribute on.
        /// </param>
        /// <returns>
        /// Returns the attribute T or null
        /// </returns>
        public static T GetAttribute<T>(this MetaTable table) where T : Attribute
        {
            return table.Attributes.OfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// Get the attribute or a default instance of the attribute
        /// if the Column attribute do not contain the attribute
        /// </summary>
        /// <typeparam name="T">
        /// Attribute type
        /// </typeparam>
        /// <param name="table">
        /// Column to search for the attribute on.
        /// </param>
        /// <returns>
        /// The found attribute or a default 
        /// instance of the attribute of type T
        /// </returns>
        public static T GetAttributeOrDefault<T>(this MetaColumn column) where T : Attribute, new()
        {
            return column.Attributes.OfType<T>().DefaultIfEmpty(new T()).FirstOrDefault();
        }

        /// <summary>
        /// Get the attribute of type T or null if not found
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="table">Column to search for the attribute on.</param>
        /// <returns>Returns the attribute T or null</returns>
        public static T GetAttribute<T>(this MetaColumn column) where T : Attribute
        {
            return column.Attributes.OfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// Gets the attribute or default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        public static T GetAttributeOrDefault<T>(this MetaChildrenColumn column) where T : Attribute, new()
        {
            return column.Attributes.OfType<T>().DefaultIfEmpty(new T()).FirstOrDefault();
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this MetaChildrenColumn column) where T : Attribute
        {
            return column.Attributes.OfType<T>().FirstOrDefault();
        }
    }
}
