using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EmbedField:Attribute
    {
        bool hide;

        public bool Hide
        {
            get
            {
                return hide;
            }

            set
            {
                hide = value;
            }
        }

        public bool GetfieldfromPrinceble
        {
            get
            {
                return getfieldfromPrinceble;
            }

            set
            {
                getfieldfromPrinceble = value;
            }
        }

        /// <summary>
        /// hides a column in the filter view
        /// </summary>
        /// <param name="h"></param>
        public EmbedField(bool h,bool g)
        {
            Hide = h;
            GetfieldfromPrinceble = g;
        }
        bool getfieldfromPrinceble;

    }
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false)]
    public class EmbedView : Attribute
    {
        bool hideFilter;


        public bool HideFilter
        {
            get
            {
                return hideFilter;
            }

            set
            {
                hideFilter = value;
            }
        }
    }
}
