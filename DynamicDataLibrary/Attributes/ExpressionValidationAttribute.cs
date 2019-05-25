using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortableLibrary;

namespace DynamicDataLibrary
{
    /// <summary>
    /// Original code from: http://weblogs.asp.net/ricardoperes/archive/2012/02/20/general-purpose-data-annotations-validation-attribute.aspx
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = true)]
    public sealed class ExpressionValidationAttribute : ValidationAttribute
    {
        public ExpressionValidationAttribute(String expression)
        {
            this.Expression = expression;
        }

        /// <summary>
        /// The expression to evaluate. May not be null.
        /// Supported values:
        /// PropertyName
        /// null
        /// {0}
        /// Supported operators:
        /// &gt;, &lt;, &gt;=, &lt;=, ==, !=
        /// </summary>
        /// <example>
        /// PropertyA != null
        /// PropertyA > PropertyB
        /// </example>
        public String Expression
        {
            get;
            private set;
        }

        public override Boolean IsDefaultAttribute()
        {
            return (this.Expression == null);
        }

        public override Boolean Equals(Object obj)
        {
            if (base.Equals(obj) == false)
            {
                return (false);
            }

            if (Object.ReferenceEquals(this, obj) == true)
            {
                return (true);
            }

            ExpressionValidationAttribute other = obj as ExpressionValidationAttribute;

            if (other == null)
            {
                return (false);
            }

            return (other.Expression == this.Expression);
        }

        public override Int32 GetHashCode()
        {
            Int32 hashCode = 1;

            hashCode = (hashCode * 397) ^ (this.Expression != null ? this.Expression.GetHashCode() : 0);

            return (hashCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        /// <remarks>The Error Message of the validation is not localized and could not be customized... May work on this later...
        /// Example of the bad error message: "The field رقم الصادر is invalid."
        /// Additionally, the param: validationContext is always null!!!!!!!</remarks>
        protected override ValidationResult IsValid(Object value, ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(this.Expression) == true)
            {
                return (ValidationResult.Success);
            }

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
            expression = expression.Replace("{0}", validationContext != null ? validationContext.MemberName : value.ToString());

            PropertyDescriptor[] props = null;
            if (validationContext != null)
            {
                Object instance = validationContext.ObjectInstance;
                props = TypeDescriptor
                    .GetProperties(instance)
                    .OfType<PropertyDescriptor>()
                    .Where(x => x.IsReadOnly == false)
                    .Where(x => x.PropertyType.IsPrimitive || x.PropertyType == typeof(String))
                    .ToArray();

                foreach (PropertyDescriptor prop in props)
                {
                    temp.Columns.Add(prop.Name, prop.PropertyType);
                }
            }

            temp.BeginLoadData();

            DataRow row = temp.NewRow();
            temp.Rows.Add(row);

            if (validationContext != null)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    row[prop.Name] = prop.GetValue(validationContext.ObjectInstance);
                }
            }
            DataColumn isValidColumn = new DataColumn();
            isValidColumn.ColumnName = "_is_valid";
            isValidColumn.Expression = expression;

            temp.Columns.Add(isValidColumn);

            temp.EndLoadData();

            Boolean isValid = Convert.ToBoolean(row[isValidColumn]);

            if (isValid == true)
            {
                return (ValidationResult.Success);
            }
            else
            {
                if (validationContext != null)
                {
                    String errorMessage = this.FormatErrorMessage(validationContext.MemberName != null ? validationContext.MemberName : validationContext.ObjectInstance.GetType().Name);
                    return (new ValidationResult(errorMessage, ((validationContext.MemberName != null) ? new String[] { validationContext.MemberName } : Enumerable.Empty<String>())));
                }
                else
                {
                    ValidationResult validationRes = new ValidationResult("The value is invalide because it meets the expression: " + expression);
                    return validationRes;
                }
            }
        }
    }
}