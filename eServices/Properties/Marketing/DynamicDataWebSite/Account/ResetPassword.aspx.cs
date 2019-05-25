using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using DynamicDataWebSite.Models;

namespace DynamicDataWebSite.Account
{
    public partial class ResetPassword : Page
    {
        protected string StatusMessage
        {
            get;
            private set;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title =  Resources.Account. Page_ResetPassword_Name;
            head.Text = Resources.Account.Page_ResetPassword_Name;
            Email.Attributes.Add("placeholder", Resources.Account.Page_Forgot_Email);
            Email_Validator.ErrorMessage = Resources.Account.Page_ResetPassword_Email_RequiredFieldValidator;
            Password.Attributes.Add("placeholder", Resources.Account.Page_ResetPassword_Password);
            Password_Validator.ErrorMessage = Resources.Account.Page_ResetPassword_Password_RequiredFieldValidator;
            ConfirmPassword.Attributes.Add("placeholder", Resources.Account.Page_ResetPassword_ConfirmPassword);
            ConfirmPassword_Validator.ErrorMessage = Resources.Account.Page_ResetPassword_ConfirmPassword_RequiredFieldValidator;
            reset.Text = Resources.Account.Page_ResetPassword_Submit;
        }
        protected void Reset_Click(object sender, EventArgs e)
        {
            string code = IdentityHelper.GetCodeFromRequest(Request);
            if (code != null)
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

                var user = manager.FindByName(Email.Text);
                if (user == null)
                {
                    ErrorMessage.Text = Convert.ToString(HttpContext.GetGlobalResourceObject("Account", "Page_ResetPassword_Error_NoUserFound"));
                    return;
                }
                var result = manager.ResetPassword(user.Id, code, Password.Text);
                if (result.Succeeded)
                {
                    Response.Redirect("~/Account/ResetPasswordConfirmation");
                    return;
                }
                ErrorMessage.Text = result.Errors.FirstOrDefault();
                return;
            }

            ErrorMessage.Text = Convert.ToString(HttpContext.GetGlobalResourceObject("Account", "Page_ResetPassword_Error"));
        }
    }
}