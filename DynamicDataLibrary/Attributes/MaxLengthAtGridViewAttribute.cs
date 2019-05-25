using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataLibrary
{
    public class MaxLengthAtListAttribute : Attribute
    {
        public int Length { get; set; }

        public MaxLengthAtListAttribute(int length)
        {
            this.Length = length;
        }
    }
}
