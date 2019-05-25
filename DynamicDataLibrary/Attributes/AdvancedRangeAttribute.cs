using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.DynamicData;

namespace DynamicDataLibrary
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class AdvancedRangeAttribute : Attribute
    {
        public override object TypeId
        {
            get
            {
                return this;
            }
        }

        public string PropertyName { get; set; }
        public object PropertyValue { get; set; }
        public double Minimum { get; set; }
        public double Maximum { get; set; }
        public string Message { get; set; }

        // Summary:
        //     Initializes a new instance of the System.ComponentModel.DataAnnotations.RangeAttribute
        //     class by using the specified minimum and maximum values.
        //
        // Parameters:
        //   minimum:
        //     Specifies the minimum value allowed for the data field value.
        //
        //   maximum:
        //     Specifies the maximum value allowed for the data field value.
        public AdvancedRangeAttribute(string propertyName, object propertyValue, double minimum, double maximum,
            string message)
        {
            this.PropertyName = propertyName;
            this.PropertyValue = propertyValue;
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.Message = message;
        }


        public static bool IsValideFor(IEnumerable<MetaColumn> columns, IOrderedDictionary values, out string message)
        {
            bool isValid = true;
            message = String.Empty;
            //Check for AdvancedRangeAttribute. 
            //  This could not be done inside "Integer_Edit" because it requeres to evaluate other field values.
            foreach (var column in columns)
            {
                IEnumerable<AdvancedRangeAttribute> advRangeAtts = column.Attributes.OfType<AdvancedRangeAttribute>();
                foreach (AdvancedRangeAttribute advRange in advRangeAtts)
                {
                    if (Convert.ToString(values[advRange.PropertyName]) == Convert.ToString(advRange.PropertyValue))
                    {
                        if (Convert.ToInt32(values[column.Name]) < advRange.Minimum
                            || Convert.ToInt32(values[column.Name]) > advRange.Maximum)
                        {
                            message = advRange.Message + "<br />";
                            isValid = false;
                        }
                    }
                }
            }
            return isValid;
        }

    }
}
