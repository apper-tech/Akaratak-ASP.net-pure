using System;

namespace NotAClue.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Attribute to identify which column to use as a 
    /// parent column for the child column to depend upon
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CascadeAttribute : Attribute
    {
        /// <summary>
        /// Name of the parent column
        /// </summary>
        public String ParentColumn { get; set; }
        public string ConnectedColumn { get; set; }
        /// <summary>
        /// custom text to display for select first
        /// </summary>
        public string Child_Select_First { get; set; }
        public string CustomFeildName { get; set; }
        public string CustomFeildValue { get; set; }
        /// <summary>
        /// Allow the navigation to the table in the list view
        /// </summary>
        public bool AllowNavigation { get; set; }
        /// <summary>
        /// Default Constructor sets ParentColumn
        /// to an empty string 
        /// </summary>
        public CascadeAttribute()
        {
            ParentColumn = "";
        }

        /// <summary>
        /// Constructor to use when
        /// setting up a cascade column
        /// </summary>
        /// <param name="parentColumn">Name of column to use in cascade</param>
        public CascadeAttribute(string parentColumn)
        {
            ParentColumn = parentColumn;
        }
    }
}