using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace NotAClue
{
    public static class StringExtensionMethods
    {
        #region Properties
        private static Dictionary<String, String> s_replaceableWords;
        /// <summary>
        /// Gets or sets the replaceable words.
        /// </summary>
        /// <value>The replaceable words.</value>
        public static Dictionary<String, String> ReplaceableWords
        {
            get { return s_replaceableWords; }
            set { s_replaceableWords = value; }
        }

        private static List<String> s_removableWords;
        /// <summary>
        /// Gets or sets the removable words.
        /// </summary>
        /// <value>The removable words.</value>
        public static List<String> RemovableWords
        {
            get { return s_removableWords; }
            set { s_removableWords = value; }
        }
        #endregion

        static StringExtensionMethods()
        {
            s_replaceableWords = new Dictionary<string, string>();
            s_removableWords = new List<string>();
        }

        /// <summary>
        /// Toes the title from pascal.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns></returns>
        public static String ToTitleFromPascal(this String label)
        {
            // remove name space
            String s0 = Regex.Replace(label, "(.*\\.)(.*)", "$2");
            // add space before Capital letter
            String s1 = Regex.Replace(s0, "[A-Z]", " $&");

            // replace '_' with space
            String s2 = Regex.Replace(s1, "[_]", " ");

            // replace double space with single space
            String s3 = Regex.Replace(s2, "  ", " ");

            // remove and double capitals with inserted space
            String s4 = Regex.Replace(s3, "(?<before>[A-Z])\\s(?<after>[A-Z])", "${before}${after}");

            // remove and double capitals with inserted space
            String sf = Regex.Replace(s4, "^\\s", "");

            var wordsRemoved = sf.RemoveWords();

            var titleCased = wordsRemoved.ToTitleCase();

            var wordsReplaced = titleCased.ReplaceWords();

            return wordsReplaced;
        }

        /// <summary>
        /// Replaces words in the replaceable words dictionary.
        /// </summary>
        /// <param name="theText">The text.</param>
        /// <returns></returns>
        private static String ReplaceWords(this String value)
        {
            String newValue = value;
            // replace words in friendly name
            if (ReplaceableWords.Count > 0)
            {
                foreach (var word in ReplaceableWords)
                {
                    if (newValue.Contains(word.Key))
                        newValue = newValue.Replace(word.Key, word.Value);
                }
            }
            return newValue;
        }

        /// <summary>
        /// Removes words that are in the removable words list.
        /// </summary>
        /// <param name="theText">The text.</param>
        /// <returns></returns>
        private static String RemoveWords(this String theText)
        {
            // replace words in friendly name
            if (RemovableWords.Count() > 0)
            {
                foreach (var removableWord in RemovableWords)
                {
                    var word = removableWord + " ";
                    if (theText.Contains(word))
                        theText = theText.Replace(word, String.Empty);
                }
            }
            // return text and replace any double spaces with single
            return theText.ToString().Replace("  ", " ").Trim();
        }

        /// <summary>
        /// Gets the right most 'n' characters from the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="length">The index.</param>
        /// <returns></returns>
        public static String Right(this String value, int length)
        {
            return value.Substring(value.Length - length, length);
        }

        /// <summary>
        /// Gets the left most 'n' characters from the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="length">The index.</param>
        /// <returns></returns>
        public static String Left(this String value, int length)
        {
            return value.Substring(0, length);
        }

        public static String Mid(this String value, int start, int length)
        {
            return value.Substring(start, length);
        }

        /// <summary>
        /// STRs to byte array.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns>String as an array of bytes</returns>
        public static byte[] StrToByteArray(this String str)
        {
            var encoding = new System.Text.UTF8Encoding();
            return encoding.GetBytes(str);
        }

        /// <summary>
        /// Determines whether the specified STR1 contains any.
        /// </summary>
        /// <param name="str1">The STR1.</param>
        /// <param name="str2">The STR2.</param>
        /// <returns>
        /// 	<c>true</c> if the specified STR1 contains any; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean ContainsAny(this String[] str1, params String[] str2)
        {
            // call extension method to convert array to lower case for compare
            var lowerCaseStr1 = str1.AllToLower();
            foreach (String str in str2)
            {
                if (lowerCaseStr1.Contains(str.ToLower()))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Appends the name of to end of file.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="stringToAppend">The string to append.</param>
        /// <returns></returns>
        public static String AppendToEndOfFileName(this String text, String stringToAppend)
        {
            var parts = text.Split(new char[] { '.' });
            if (parts.Length > 2)
                throw new InvalidOperationException("too many '.' in filename");
            return String.Format("{0}{1}.{2}", parts[0], stringToAppend, parts[1]);
        }

        /// <summary>
        /// converts a comma separated values string to array.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A String array</returns>
        public static String[] CsvToArray(this String text)
        {
            return text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Returns a copy of the array of string 
        /// all in lowercase
        /// </summary>
        /// <param name="strings">Array of strings</param>
        /// <returns>array of string all in lowercase</returns>
        public static String[] AllToLower(this String[] strings)
        {
            String[] temp = new String[strings.Count()];
            for (int i = 0; i < strings.Count(); i++)
            {
                temp[i] = strings[i].ToLower();
            }
            return temp;
        }

        /// <summary>
        /// Sets all characters in a List to lower case.
        /// </summary>
        /// <param name="strings">The strings.</param>
        /// <returns></returns>
        public static List<String> AllToLower(this List<String> strings)
        {
            var temp = new List<String>();
            foreach (var item in strings)
            {
                temp.Add(item.ToLower());
            }
            return temp;
        }

        /// <summary>
        /// Toes the title case.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static String ToTitleCase(this String text)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                if (i > 0)
                {
                    if (text.Substring(i - 1, 1) == " " ||
                        text.Substring(i - 1, 1) == "\t" ||
                        text.Substring(i - 1, 1) == "/")
                        sb.Append(text.Substring(i, 1).ToUpper());
                    else
                        sb.Append(text.Substring(i, 1).ToLower());
                }
                else
                    sb.Append(text.Substring(i, 1).ToUpper());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Toes the sentence case.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static String ToSentenceCase(this String text)
        {
            return text.Substring(0, 1).ToUpper() + text.Substring(1, text.Length - 1).ToLower();
        }

        /// <summary>
        /// Gets the display format.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static String GetDisplayFormat(this Type type)
        {
            string defaultFormat = "{0}";

            if (type == typeof(DateTime) || type == typeof(Nullable<DateTime>))
            {
                defaultFormat = "{0:d}";
            }
            else if (type == typeof(decimal) || type == typeof(Nullable<decimal>))
            {
                defaultFormat = "{0:c}";
            }

            return defaultFormat;
        }

        /// <summary>
        /// Gets the values list.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static ListItem[] GetValuesList(this String values)
        {
            var items = values.Split(new char[] { ',', ';' });
            var listItems = (from item in items
                             select new ListItem()
                             {
                                 Text = item.Split(new char[] { '|' })[1],
                                 Value = item.Split(new char[] { '|' })[0]
                             }).ToArray();
            return listItems;
        }
    }
}
