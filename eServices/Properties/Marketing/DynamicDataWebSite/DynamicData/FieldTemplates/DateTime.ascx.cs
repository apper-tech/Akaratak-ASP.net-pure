using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite
{
    public partial class DateTimeField : System.Web.DynamicData.FieldTemplateUserControl
    {
        public override Control DataControl
        {
            get
            {
                return Literal1;
            }
        }
        public string FormatFieldValue(string fieldValueString)
        {
            if (String.IsNullOrEmpty(fieldValueString))
                return fieldValueString;

            if (!String.IsNullOrEmpty(Column.DataFormatString))
                return base.FormatFieldValue(fieldValueString);

            if (Column.DataTypeAttribute != null)
            {
                switch (Column.DataTypeAttribute.DataType)
                {
                    case DataType.Date:
                        return Convert.ToDateTime(fieldValueString).ToString("dd/MM/yyyy");
                    case DataType.DateTime:
                        return Convert.ToDateTime(fieldValueString).ToString("dd/MM/yyyy hh:mm tt");
                    case DataType.Time:
                        return Convert.ToDateTime(fieldValueString).ToString("hh:mm tt");
                }
            }
            else if (Column.ColumnType.Equals(typeof(DateTime)))
            {
                return Convert.ToDateTime(fieldValueString).ToString("dd/MM/yyyy hh:mm tt");
            }
            else if (Column.ColumnType.Equals(typeof(TimeSpan)))
            {
                return Convert.ToDateTime(fieldValueString).ToString("hh:mm tt");
            }

            return fieldValueString;
        }
    }
}
