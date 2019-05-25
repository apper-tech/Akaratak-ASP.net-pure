using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using DynamicDataWebSite.Models;
using System.Resources;

namespace DynamicDataWebSite.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string title = GetGlobalResourceObject("Account", "Page_Login_Name") as string;
            Title = title != "" ? title : "Login";
            Title = title;
            SetPageText(System.Threading.Thread.CurrentThread.CurrentUICulture.Name.Contains("ar"));
            // RegisterHyperLink.NavigateUrl = "Register";
            // Enable this once you have account confirmation enabled for password reset functionality
            //ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            // OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
             //   RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }
        public void SetPageText(bool ar)
        {
            loginBtn.Text = GetGlobalResourceObject("Account", "Page_Login_Submit").ToString();
            rem.Text = "&nbsp;&nbsp;" + Resources.Account.Page_Login_RememberMe;
            Email.Attributes.Add("placeholder", Resources.Account.Page_Login_Email);
            Password.Attributes.Add("placeholder", Resources.Account.Page_Login_Password);
            EmailVal.ErrorMessage = Resources.Account.Page_Login_Email_RequiredFieldValidator;
            PasswordVal.ErrorMessage = Resources.Account.Page_Login_Password_RequiredFieldValidator;
            if (ar)
            {
                ScrollHere.Attributes.Add("dir", "rtl");
            }
        }
        
        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user password
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                // This doen't count login failures towards account lockout
                // To enable password failures to trigger lockout, change to shouldLockout: true
                SignInStatus result = signinManager.PasswordSignIn(Email.Text, Password.Text, rem.Checked, shouldLockout: false);
                
                switch (result)
                {
                    case SignInStatus.Success:
                        Session["CurrentUserID"] = User.Identity.GetUserId();
                        Response.Redirect("~/");
                        break;
                    case SignInStatus.LockedOut:
                        Response.Redirect("/Account/Lockout");
                        break;
                    case SignInStatus.RequiresVerification:
                        Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                                                        Request.QueryString["ReturnUrl"],
                                                        rem.Checked),
                                          true);
                        break;
                    case SignInStatus.Failure:
                    default:
                        FailureText.Text = Convert.ToString(HttpContext.GetGlobalResourceObject("Account", "Page_Login_Error_InvalidLoginAttempt"));
                        ErrorMessage.Visible = true;
                        break;
                }
                
            }
        }
    }
}