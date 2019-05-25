using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using DynamicDataWebSite.Models;

namespace DynamicDataWebSite.Account
{
    public partial class ForgotPassword : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = Resources.Account.Page_Forgot_Name;
            submit.Text = Resources.Account.Page_Forgot_Submit;
            Email.Attributes.Add("placeholder", Resources.Account.Page_Forgot_Email);
            Email_Validator.ErrorMessage = Resources.Account.Page_Forgot_Email_RequiredFieldValidator;
            Display_Email_Text.Text = Resources.Account.Page_Forgot_DisplayEmail;
        }

        protected void Forgot(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user's email address
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser user = manager.FindByName(Email.Text);
                if (user == null /*|| !manager.IsEmailConfirmed(user.Id)*/)
                {
                    FailureText.Text = Convert.ToString(HttpContext.GetGlobalResourceObject(
                        "Account", "Page_Forgot_FailureText"));
                    ErrorMessage.Visible = true;
                    return;
                }
                string code = manager.GeneratePasswordResetToken(user.Id);
                string callbackUrl = IdentityHelper.GetResetPasswordRedirectUrl(code, Request);
                string title = Resources.RealEstate.Page_Forget_Message_Title;
                string body = string.Format(Resources.RealEstate.Page_Forget_Message_Body, callbackUrl);
                SecurityHandler.SendResetMail(user.Email, title, body);
                loginForm.Visible = false;
                DisplayEmail.Visible = true;
            }
        }
    }
}