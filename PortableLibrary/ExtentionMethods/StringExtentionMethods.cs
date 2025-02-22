﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableLibrary
{
    public static class StringExtentionMethods
    {
        public static String Replace(this String originalString, String oldValue, String newValue, StringComparison comparisonType)
        {
            Int32 startIndex = 0;

            while (true)
            {
                startIndex = originalString.IndexOf(oldValue, startIndex, comparisonType);

                if (startIndex < 0)
                {
                    break;
                }

                originalString = String.Concat(originalString.Substring(0, startIndex), newValue, originalString.Substring(startIndex + oldValue.Length));

                startIndex += newValue.Length;
            }

            return (originalString);
        }
    }
}
