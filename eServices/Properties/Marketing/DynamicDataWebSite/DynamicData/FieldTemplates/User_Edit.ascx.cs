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
    public partial class User_Edit : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            DynamicDataModel.Model.User s = (DynamicDataModel.Model.User)FieldValue;
            
            if (s.User_ID != Context.User.Identity.GetUserId())
            {
                Response.Redirect("~/Account/Register");
            }
            else
            {
                if (FieldValue.GetType() != typeof(string))
                    UserID = (FieldValue as DynamicDataModel.Model.User).User_ID;
                else
                    userID = FieldValueString;
            }
        }
        string userID;
        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            dictionary[Column.Name] = UserID;
        }
        public override Control DataControl
        {
            get
            {
                return base.DataControl;
            }
        }

        public string UserID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
            }
        }
    }

    }
