using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortableLibrary;
using System.ComponentModel;
using System.Web;
using System.Globalization;

namespace DynamicDataLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CustomStyleBaseOnExpressionAttribute : Attribute
    {
        public override object TypeId
        {
            get
            {
                return this;
            }
        }

        public string Expression { get; private set; }
        public string CssClass { get; private set; }

        public CustomValueType ValueType1 { get; set; }
        private object value1;
        public object Value1
        {
            get
            {
                switch (this.ValueType1)
                {
                    case CustomValueType.SessionVariable:
                        return HttpContext.Current.Session[this.value1.ToString()];
                    case CustomValueType.CurrentDateTime:
                        return DateTime.Now;
                    case CustomValueType.CurrentDateTimePlusMinutes:
                        return DateTime.Now.AddSeconds(Convert.ToDouble(this.value1));
                    case CustomValueType.StaticValue:
                    default:
                        return this.value1;
                }
            }
        }

        public CustomValueType ValueType2 { get; set; }
        private object value2;
        public object Value2
        {
            get
            {
                switch (this.ValueType2)
                {
                    case CustomValueType.SessionVariable:
                        return HttpContext.Current.Session[this.value2.ToString()];
                    case CustomValueType.CurrentDateTime:
                        return DateTime.Now;
                    case CustomValueType.CurrentDateTimePlusMinutes:
                        return DateTime.Now.AddSeconds(Convert.ToDouble(this.value2));
                    case CustomValueType.StaticValue:
                    default:
                        return this.value2;
                }
            }
        }
        public CustomStyleBaseOnExpressionAttribute(string expression, string cssClass)
        {
            this.Expression = expression;
            this.CssClass = cssClass;
        }
        public CustomStyleBaseOnExpressionAttribute(string expression, string cssClass,
            CustomValueType customValueType, Object value)
        {
            this.Expression = expression;
            this.CssClass = cssClass;
            this.ValueType1 = customValueType;
            this.value1 = value;
        }
        public CustomStyleBaseOnExpressionAttribute(string expression, string cssClass,
            CustomValueType customValueType1, Object value1,
            CustomValueType customValueType2, Object value2)
        {
            this.Expression = expression;
            this.CssClass = cssClass;
            this.ValueType1 = customValueType1;
            this.value1 = value1;
            this.ValueType2 = customValueType2;
            this.value2 = value2;
        }
        public bool AppliesTo(object instance)
        {
            DataTable temp = new DataTable();
            String expression = this.Expression;

            while (expression.IndexOf("  ") >= 0)
            {
                expression = expression.Replace("  ", " ");
            }

            //translate .NET language operators into SQL ones
            expression = expression.Replace("!=", "<>");
            expression = expression.Replace("==", "=");
            expression = expression.Replace("!", " NOT ");
            expression = expression.Replace("&&", " AND ");
            expression = expression.Replace("||", " OR ");
            expression = expression.Replace("= NULL", " IS NULL ", StringComparison.OrdinalIgnoreCase);
            expression = expression.Replace("<> NULL", " IS NOT NULL ", StringComparison.OrdinalIgnoreCase);
            expression = expression.Replace("null", "NULL", StringComparison.OrdinalIgnoreCase);
            if (!String.IsNullOrEmpty(Convert.ToString(this.Value1)))
            {
                if (this.Value1.ToString() == String.Empty)
                    expression = expression.Replace("{0}", "'" + this.Value1.ToString() + "'");
                else
                {
                    if (this.Value1.GetType() == typeof(Nullable<DateTime>) ||
                        this.Value1.GetType() == typeof(DateTime))
                        expression = expression.Replace("{0}", "'" + this.Value1 + "'");
                    else
                        expression = expression.Replace("{0}", this.Value1.ToString());
                }
            }

            if (!String.IsNullOrEmpty(Convert.ToString(this.Value2)))
            {
                if (this.Value2.ToString() == String.Empty)
                    expression = expression.Replace("{1}", "'" + this.Value2.ToString() + "'");
                else
                {
                    if (this.Value2.GetType() == typeof(Nullable<DateTime>) ||
                        this.Value2.GetType() == typeof(DateTime))
                        expression = expression.Replace("{1}", "'" + this.Value2 + "'");
                    else
                        expression = expression.Replace("{1}", this.Value2.ToString());
                }
            }


            PropertyDescriptor[] props = null;
            if (instance != null)
            {
                props = TypeDescriptor
                    .GetProperties(instance)
                    .OfType<PropertyDescriptor>()
                    //.Where(x => x.IsReadOnly == false)
                    //.Where(x => x.PropertyType.IsPrimitive || x.PropertyType == typeof(String))
                    .ToArray();

                foreach (PropertyDescriptor prop in props)
                {
                    temp.Columns.Add(prop.Name, prop.PropertyType);
                }
            }

            temp.BeginLoadData();

            DataRow row = temp.NewRow();
            temp.Rows.Add(row);

            if (instance != null)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    row[prop.Name] = prop.GetValue(instance);
                }
            }
            DataColumn isValidColumn = new DataColumn();
            isValidColumn.ColumnName = "_is_valid";
            isValidColumn.Expression = expression;

            temp.Columns.Add(isValidColumn);

            temp.EndLoadData();

            Boolean isValid = Convert.ToBoolean(row[isValidColumn]);
            return isValid;
        }
    }
}
