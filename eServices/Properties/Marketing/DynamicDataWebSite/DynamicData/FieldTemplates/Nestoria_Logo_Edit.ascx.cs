using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite.DynamicData.FieldTemplates
{
    public partial class Nestoria_Logo_Edit : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            base.ExtractValues(dictionary);
            dictionary[Column.Name] = "NULL";
        }
    }
}