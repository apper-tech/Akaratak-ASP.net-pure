using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite.DynamicData.FieldTemplates
{
    public partial class Nestoria_Logo : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnDataBinding(EventArgs e)
        {
           if(!string.IsNullOrEmpty(FieldValueString))
            {
                view.Visible = true;
            }
        }
    }
}