using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite.DynamicData.FieldTemplates
{
    public partial class DatePicker_Edit : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var cd = Column.Attributes.OfType<DynamicDataLibrary.Attributes.Calender>().FirstOrDefault();
            if (cd != null)
            {
                if (cd.Disable)
                    datef.CssClass += " caldisabled";
                if (cd.DefaultValue == "today")
                    datef.CssClass += " caldeftoday";
            }

        }
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            if (!string.IsNullOrEmpty(FieldValueString))
            {
                datef.Text = FieldValueString;
                dateargs.Value = FieldValueEditString;
            }
        }
        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            base.ExtractValues(dictionary);
            dictionary[Column.Name] = ConvertEditedValue(string.IsNullOrEmpty(datef.Text) == true ? string.IsNullOrEmpty(dateargs.Value)?DateTime.Now.AddMonths(3).ToShortDateString():dateargs.Value : datef.Text);
        }
        public override Control DataControl
        {
            get
            {
                return datef;
            }
        }
    }
}