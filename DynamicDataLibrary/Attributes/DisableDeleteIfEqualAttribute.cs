using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataLibrary
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DisableDeleteIfEqualAttribute : Attribute
    {
        public override object TypeId
        {
            get
            {
                return this;
            }
        }

        private object[] values;
        public object[] Values
        {
            get { return values; }
            set { values = value; }
        }

        private string propertyName;
        public string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        public DisableDeleteIfEqualAttribute(string propertyName, params object[] values)
        {
            this.propertyName = propertyName;
            this.values = values;
        }
    }
}
