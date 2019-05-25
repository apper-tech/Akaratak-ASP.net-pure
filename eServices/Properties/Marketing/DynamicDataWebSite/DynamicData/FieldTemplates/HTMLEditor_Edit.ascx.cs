using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.DynamicData;
using System.Collections.Specialized;

namespace DynamicDataWebSite.DynamicData.FieldTemplates
{
    public partial class HTMLEditor_Edit : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnDataBinding(EventArgs e)
        {
            text.Text = FieldValueString;
        }
        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            dictionary[Column.Name] = text.Text;
        }
    }
}