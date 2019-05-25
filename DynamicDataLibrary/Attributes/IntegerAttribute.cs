using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataLibrary.Attributes
{
    public class IntegerAttribute : Attribute
    {
        int minValue;
        int maxValue;
        bool emptyString;
        public int MinValue
        {
            get
            {
                return minValue;
            }

            set
            {
                minValue = value;
            }
        }

        public int MaxValue
        {
            get
            {
                return maxValue;
            }

            set
            {
                maxValue = value;
            }
        }

        public bool EmptyString
        {
            get
            {
                return emptyString;
            }

            set
            {
                emptyString = value;
            }
        }
    }
}
