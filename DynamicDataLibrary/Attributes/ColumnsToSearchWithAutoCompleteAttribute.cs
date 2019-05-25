using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ColumnsToSearchWithAutoCompleteAttribute : Attribute
    {
        public string[] ColumnsToSearchAt { get; set; }

        public ColumnsToSearchWithAutoCompleteAttribute(params string[] columnsToSearchAt)
        {
            this.ColumnsToSearchAt = columnsToSearchAt;
        }
    }
}
