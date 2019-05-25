using System;

namespace NotAClue.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Categorize Attribute
    /// </summary>
    /// <remarks></remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CategorizeAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        /// <value>The name of the category foreign key column.</value>
        /// <remarks></remarks>
        public String ColumnName { get; set; }

        public CategorizeAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}