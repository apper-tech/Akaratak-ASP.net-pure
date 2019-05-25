using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.DynamicData;

namespace DynamicDataWebSite.DynamicData.FieldTemplates
{
    public partial class HTMLEditor : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnDataBinding(EventArgs e)
        {
            html.Text = FieldValueString;
            if (FieldValueString.Contains('.'))
                Page.Title = FieldValueString.Substring(0, FieldValueString.IndexOf('.'));
            else
                Page.Title = FieldValueString.Substring(0, 30) + "...";
            Page.MetaDescription = Page.Title;
        }

    }
}