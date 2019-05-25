using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotAClue
{
    public static class ReflectionExtensionMethods
    {
        /// <summary>
        /// Gets the prop value.
        /// </summary>
        /// <param name="src">The source object.</param>
        /// <param name="propName">Name of the property.</param>
        /// <returns></returns>
        public static Object GetPropValue(this Object sourceObject, string propertyName)
        {
            if (sourceObject != null)
            {
                try
                {
                    var value = sourceObject.GetType().GetProperty(propertyName).GetValue(sourceObject, null);
                    return value;
                }
                catch
                {
                    return null;
                }
            }
            else
                return null;
        }
    }
}
