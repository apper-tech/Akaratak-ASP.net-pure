using System;

namespace NotAClue.Web.DynamicData
{
    /// <summary>
    /// Event Arguments for Selection Changed 
    /// Event of a cascading filter or field template.
    /// </summary>
    public class SelectionChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="SelectionChangedEventArgs"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="text">The text.</param>
        /// <remarks></remarks>
        public SelectionChangedEventArgs(String value, String text)
        {
            Value = value;
            Text = text;
        }
        /// <summary>
        /// Gets the value of the parent control.
        /// </summary>
        public String Value { get; private set; }

        /// <summary>
        /// Gets the text value of the parent control.
        /// </summary>
        public String Text { get; private set; }
    }
}