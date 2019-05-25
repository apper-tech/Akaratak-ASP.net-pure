using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataLibrary
{
    public interface IValueChangable
    {
        /// <summary>
        /// Returns the selected value of the drop down list
        /// </summary>
        string Value
        {
            get;
        }

        bool AutoPostBack
        {
            get;
            set;
        }
    }
}
