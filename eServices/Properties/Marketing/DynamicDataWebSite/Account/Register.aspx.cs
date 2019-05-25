using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using DynamicDataWebSite.Models;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

namespace DynamicDataWebSite.Account
{
    public partial class Register : Page
    {
        public void Page_Load(object sender,EventArgs e)
        {
            string title = GetGlobalResourceObject("Account", "Page_Register_Name") as string;
            Title = title != "" ? title : "Register";
            Title = title;
            SetPageText(System.Threading.Thread.CurrentThread.CurrentUICulture.Name.Contains("ar"));
        }
        public void CheckBoxRequired_ServerValidate(object sernder, ServerValidateEventArgs e)
        {
           e.IsValid = Agree.Checked;
        }
        public void SetPageText(bool ar)
        {
            RegBtn.Text = Resources.Account.Page_Register_Submit;
            Agree.Text =  Resources.Account.Page_Register_Agree;
            Agree.ValidateCheckboxError.Text = Resources.Account.Page_Register_CheckAgree;
            Email.Attributes.Add("placeholder", Resources.Account.Page_Register_Email);
            Password.Attributes.Add("placeholder", Resources.Account.Page_Register_Password);
            ConfirmPassword.Attributes.Add("placeholder", Resources.Account.Page_Register_ConfirmPassword);
            EmailVal.ErrorMessage = Resources.Account.Page_Register_Email_RequiredFieldValidator;
            EmailRegVal.ErrorMessage = Resources.Account.Page_Register_Email_RegularExpVal;
            PasswordVal.ErrorMessage= Resources.Account.Page_Register_Password_RequiredFieldValidator;
            ConfirmPasswordVal.ErrorMessage = Resources.Account.Page_Register_ConfirmPassword_RequiredFieldValidator;
            ConfirmPasswordCompareVal.ErrorMessage = Resources.Account.Page_Register_ConfirmPassword_CompareValidator;
           
            if (ar)
            {
                ScrollHere.Attributes.Add("dir", "rtl");
            }
        }
       
        protected void CreateUser_Click(object sender, EventArgs e)
        {
           if (Agree.Checked)
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
                var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text, FirstName = "", LastName = "", Address = "",
                    HasOffice = false,PhoneNumber = "", PhoneNumberConfirmed = false,EmailConfirmed=true };
                IdentityResult result = manager.Create(user, Password.Text);
                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    string code = manager.GenerateEmailConfirmationToken(user.Id);
                    string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                    string title = Resources.RealEstate.Page_Register_Email_Confirm_Title;
                    string body = string.Format(Resources.RealEstate.Page_Register_Email_Confirm_body, callbackUrl);
                    SecurityHandler.SendConfirmMail(user.Email, title, body);
                    if (!Roles.IsUserInRole(user.UserName, "Visitor"))
                        Roles.AddUserToRole(user.UserName, "Stackholder");
                    signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                    IdentityHelper.RedirectToReturnUrl(Global.DefaultModel.GetActionPath("Users", "Insert", null), Response);

                }
                else
                {
                    ErrorMessage.Text = IdentityErrorLocalizer.LocalizeIdentityError(result.Errors.FirstOrDefault(), user);

                }
            }
            else
            {
                Agree.ValidateCheckboxError.Visible = true;
            }
        }
    }
    public class IdentityErrorLocalizer
    {
        /// <summary>
        /// A workarround because Microsft did not do the job! And did not allow us to do the job in the right way!
        /// So, I did it in the bad way!!!
        /// </summary>
        /// <param name="error"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string LocalizeIdentityError(string error, Microsoft.AspNet.Identity.EntityFramework.IdentityUser user)
        {
            const string seperator = "<br />";
            System.Text.StringBuilder localizedError = new System.Text.StringBuilder();
            bool isErrorLocalized = false;
            if (error.Contains("User already in role."))
            { 
                localizedError.Append(Convert.ToString(HttpContext.GetGlobalResourceObject("IdentityError", "UserInRole")));
                localizedError.Append(seperator);
                isErrorLocalized = true;
            }
            if (error.Contains("User is not in role."))
            {
                localizedError.Append(Convert.ToString(HttpContext.GetGlobalResourceObject("IdentityError", "UserNotInRole")));
                localizedError.Append(seperator);
                isErrorLocalized = true;
            }
            if (error.Contains("Incorrect password."))
            { 
                localizedError.Append(Convert.ToString(HttpContext.GetGlobalResourceObject("IdentityError", "IncorrectPassword")));
                localizedError.Append(seperator);
                isErrorLocalized = true;
            }
            if (error.Contains(String.Format("User name {0} is invalid, can only contain letters or digits.", user.UserName)))
            { 
                localizedError.Append(Convert.ToString(HttpContext.GetGlobalResourceObject("IdentityError", "UsernameInvalid")));
                localizedError.Append(seperator);
                isErrorLocalized = true;
            }
            if (error.Contains("Passwords must be at least"))
            { 
                localizedError.Append(String.Format(Convert.ToString(HttpContext.GetGlobalResourceObject("IdentityError", "PasswordsAtLeast")), System.Configuration.ConfigurationManager.AppSettings["PasswordValidator_RequiredLength"]));
                localizedError.Append(seperator);
                isErrorLocalized = true;
            }
            if (user != null && error.Contains(String.Format("Name {0} is already taken.", user.UserName)))
            {
                localizedError.Append(String.Format(Convert.ToString(HttpContext.GetGlobalResourceObject("IdentityError", "NameTaken")),user.UserName));
                localizedError.Append(seperator);
                isErrorLocalized = true;
            }
            if (error.Contains("User already has a password set."))
            { 
                localizedError.Append(Convert.ToString(HttpContext.GetGlobalResourceObject("IdentityError", "UserHasPassword")));
                localizedError.Append(seperator);
                isErrorLocalized = true;
            }
            if (error.Contains("Passwords must have at least one non letter or digit character."))
            { 
                localizedError.Append(Convert.ToString(HttpContext.GetGlobalResourceObject("IdentityError", "PasswordsMustHaveNonletterOrDigit")));
                localizedError.Append(seperator);
                isErrorLocalized = true;
            }
            if (error.Contains("UserId not found."))
            { 
                localizedError.Append(Convert.ToString(HttpContext.GetGlobalResourceObject("IdentityError", "UserIdNotFound")));
                localizedError.Append(seperator);
                isErrorLocalized = true;
            }
            if (error.Contains("Invalid token."))
            { 
                localizedError.Append(Convert.ToString(HttpContext.GetGlobalResourceObject("IdentityError", "InvalidToken")));
                localizedError.Append(seperator);
                isErrorLocalized = true;
            }
            if (user != null && error.Contains(String.Format("Email '{0}' is invalid.", user.Email)))
            { 
                localizedError.Append(String.Format(Convert.ToString(HttpContext.GetGlobalResourceObject("IdentityError", "EmailInvalid")), user.Email));
                localizedError.Append(seperator);
                isErrorLocalized = true;
            }
            if (user != null && error.Contains(String.Format("User {0} does not exist.", user.UserName)))
            { 
                localizedError.Append(String.Format(Convert.ToString(HttpContext.GetGlobalResourceObject("IdentityError", "UserNotExist")), user.UserName));
                localizedError.Append(seperator);
                isErrorLocalized = true;
            }
            if (error.Contains("Lockout is not enabled for this user."))
            { 
                localizedError.Append(Convert.ToString(HttpContext.GetGlobalResourceObject("IdentityError", "LockoutNotEnabledForUser")));
                localizedError.Append(seperator);
                isErrorLocalized = true;
            }
            if (error.Contains("Passwords must have at least one uppercase"))
            { 
                localizedError.Append(Convert.ToString(HttpContext.GetGlobalResourceObject("IdentityError", "PasswordsMustHaveUppercase")));
                localizedError.Append(seperator);
                isErrorLocalized = true;
            }
            if (error.Contains("Passwords must have at least one digit"))
            { 
                localizedError.Append(Convert.ToString(HttpContext.GetGlobalResourceObject("IdentityError", "PasswordsMustHaveDigit")));
                localizedError.Append(seperator);
                isErrorLocalized = true;
            }
            if (error.Contains("Passwords must have at least one lowercase"))
            { 
                localizedError.Append(Convert.ToString(HttpContext.GetGlobalResourceObject("IdentityError", "PasswordsMustHaveLowercase")));
                localizedError.Append(seperator);
                isErrorLocalized = true;
            }
            if (user != null && error.Contains(String.Format("Email '{0}' is already taken.", user.Email)))
            { 
                localizedError.Append(String.Format(Convert.ToString(HttpContext.GetGlobalResourceObject("IdentityError", "EmailAlreadyTaken")), user.Email));
                localizedError.Append(seperator);
                isErrorLocalized = true;
            }
            if (error.Contains("A user with that external login already exists."))
            { 
                localizedError.Append(Convert.ToString(HttpContext.GetGlobalResourceObject("IdentityError", "UserWithThatExternalLoginAlreadyExists")));
                localizedError.Append(seperator);
                isErrorLocalized = true;
            }
            if (error.Contains("An unknown failure has occured."))
            { 
                localizedError.Append(Convert.ToString(HttpContext.GetGlobalResourceObject("IdentityError", "UnknownFailureOccured")));
                localizedError.Append(seperator);
                isErrorLocalized = true;
            }
            if (isErrorLocalized)
                return localizedError.ToString();
            else
                return error;
        }
    }
}