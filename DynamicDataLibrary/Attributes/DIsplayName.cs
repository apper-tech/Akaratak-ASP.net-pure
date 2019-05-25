using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Linq.Dynamic;
using System.Web.UI;
using DynamicDataLibrary.ModelRelated;
using DynamicDataLibrary;
using System.Resources;
using System.ComponentModel;
using System.Reflection;

namespace DynamicDataLibrary.Attributes
{
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        private readonly PropertyInfo nameProperty;

        public LocalizedDisplayNameAttribute(string displayNameKey, Type resourceType = null)
            : base(displayNameKey)
        {
            if (resourceType != null)
            {
                nameProperty = resourceType.GetProperty(base.DisplayName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            }
        }
        public override string DisplayName
        {
            get
            {
                if (nameProperty == null)
                {
                    return base.DisplayName;
                }
                string s= (string)nameProperty.GetValue(nameProperty.DeclaringType, null);
                return s;
            }
        }
    }
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private readonly PropertyInfo nameProperty;

        public LocalizedDescriptionAttribute(string displayNameKey, Type resourceType = null)
            : base(displayNameKey)
        {
            if (resourceType != null)
            {
                nameProperty = resourceType.GetProperty(base.Description, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            }
        }
        public override string Description
        {
            get
            {
                if (nameProperty == null)
                {
                    return base.Description;
                }
                string s = (string)nameProperty.GetValue(nameProperty.DeclaringType, null);
                return s;
            }
        }
    }

}
