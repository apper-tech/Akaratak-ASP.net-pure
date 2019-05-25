using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataLibrary.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// The words dependant for the field that depends upon another for filtering and dependee for the field that is depended upon.
    /// </remarks>
    public class HideBasedOnDependeeValueAttribute : Attribute
    {
        public string DependeeName { get; set; }
        public string[] DependeeValues { get; set; }
        public HideBasedOnDependeeValueAttribute(string dependeeName, params string[] dependeeValues)
        {
            this.DependeeName = dependeeName;
            this.DependeeValues = dependeeValues;
        }
    }
}
