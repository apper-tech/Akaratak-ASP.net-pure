using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite.DynamicData.FieldTemplates
{
    public partial class User_Insert : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Register");
            }
        }
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
        }
        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            base.ExtractValues(dictionary);
            string value = Context.User.Identity.GetUserId();
            if (String.IsNullOrEmpty(value))
            {
                value = null;
            }
            if (Table.Name != "Users")
                ExtractForeignKey(dictionary, value);
            else
                dictionary[Column.Name] = value;
        }

        public override Control DataControl
        {
            get
            {
                return lbl1;
            }
        }
    }
}