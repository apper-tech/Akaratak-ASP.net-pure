using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataLibrary.Attributes
{
    public class Calender : Attribute
    {
        bool disable;
        /// <summary>
        /// disabled for Input
        /// </summary>
        public bool Disable
        {
            get
            {
                return disable;
            }

            set
            {
                disable = value;
            }
        }
        /// <summary>
        /// set the default date vale
        /// example:
        /// today = "today";
        /// +7 Day = "+7d";
        /// +2 Month= "+2m";
        /// -3 years = "-3y";
        /// </summary>
        public string DefaultValue
        {
            get
            {
                return defaultValue;
            }

            set
            {
                defaultValue = value;
            }
        }

        string defaultValue;
    }
}
