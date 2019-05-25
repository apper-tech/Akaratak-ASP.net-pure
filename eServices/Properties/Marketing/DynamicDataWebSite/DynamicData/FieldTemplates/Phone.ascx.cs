using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DynamicDataLibrary;

namespace DynamicDataWebSite
{
    public partial class PhoneField : System.Web.DynamicData.FieldTemplateUserControl
    {
        private const int MAX_DISPLAY_LENGTH_IN_LIST = 50;

        //public override string FieldValueString
        //{
        //    get
        //    {
        //        string value = base.FieldValueString;
        //        if (ContainerType == ContainerType.List)
        //        {
        //            int maxDisplayLength = MAX_DISPLAY_LENGTH_IN_LIST;

        //            MaxLengthAtListAttribute lenAttribute = 
        //                (MaxLengthAtListAttribute)this.Column.Attributes[typeof(MaxLengthAtListAttribute)];
        //            if (lenAttribute != null)
        //                maxDisplayLength = lenAttribute.Length;
        //            if (value != null && value.Length > maxDisplayLength)
        //            {
        //                value = value.Substring(0, maxDisplayLength - 3) + "...";
        //            }
        //        }
        //        return value;
        //    }
        //}

        public override Control DataControl
        {
            get
            {
                return HyperLinkUrl;
            }
        }

    }
}
