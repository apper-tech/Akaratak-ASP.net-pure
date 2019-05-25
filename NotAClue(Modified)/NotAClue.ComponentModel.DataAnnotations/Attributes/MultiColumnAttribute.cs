using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotAClue.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MultiColumnAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the column span.
        /// </summary>
        /// <value>The column span.</value>
        public int ColumnSpan { get; private set; }

        public static MultiColumnAttribute Default = new MultiColumnAttribute();

        public MultiColumnAttribute() 
        { 
            ColumnSpan = 1;
        }

        public MultiColumnAttribute(int columnSpan)
        {
            ColumnSpan = columnSpan;
        }
    }
}