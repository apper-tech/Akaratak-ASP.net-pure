using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Providers.Entities;
using Microsoft.AspNet.Identity;
using DynamicDataWebSite.Models;

namespace DynamicDataWebSite
{

    public partial class UserField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            string user_ID = "";
            if (FieldValue != null && Mode == DataBoundControlMode.ReadOnly)
            {
                if (FieldValue.GetType() != typeof(string))
                    user_ID = (FieldValue as DynamicDataModel.Model.User).User_ID;
                else
                    user_ID = FieldValueString;
               string Nestoria_id = "1e98087e-72ed-4e5e-b2ae-df3952599935";
                if (user_ID != Nestoria_id)
                {
                    var user = DynamicDataHelper.GetUserDetails(user_ID);
                    if (user != null)
                    {
                        name.Text = user.First_Name + "," + user.Last_Name;
                        phone.Text = user.Phone_Num.ToString();
                    }
                    else
                    {
                        user_ID = "";
                        userPH.Visible = false;
                    }
                }
                else
                {
                    user_ID = "";
                    userPH.Visible = false;
                }
            }
            else
            {
                user_ID = "";
                userPH.Visible = false;
            }
        }
   
    }
}
