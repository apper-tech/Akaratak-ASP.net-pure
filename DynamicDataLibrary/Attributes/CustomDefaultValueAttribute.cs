using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace DynamicDataLibrary.Attributes
{
    public enum CustomValueType
    {
        StaticValue,
        SessionVariable,
        CurrentDateTime,
        CurrentDateTimePlusMinutes
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CustomDefaultValueAttribute : System.Attribute
    {
        // As implemented, this identifier is merely the Type of the attribute.
        // However, it is intended that the unique identifier be used to identify 
        // two attributes of the same type.
        public override object TypeId { get { return this; } }

        public CustomValueType DefaultValueType { get; set; }

        public bool EnableValueChanging { get; set; }

        public bool UseOnEdit { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">String</param>
        public CustomDefaultValueAttribute(object value, 
            CustomValueType defaultValueType = CustomValueType.StaticValue,
            bool enableValueChanging = true,
            bool useOnEdit = false)
        {
            this.value = value;
            this.DefaultValueType = defaultValueType;
            this.EnableValueChanging = enableValueChanging;
            this.UseOnEdit = useOnEdit;
        }

        private object value;
        public object Value
        {
            get
            {
                switch (this.DefaultValueType)
                {
                    case CustomValueType.SessionVariable:
                        return HttpContext.Current.Session[this.value.ToString()];
                    case CustomValueType.CurrentDateTime:
                        return DateTime.Now;
                    case CustomValueType.StaticValue:
                    default:
                        return this.value;
                }
            }
        }
    }
}
