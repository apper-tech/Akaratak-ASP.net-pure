using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using DynamicDataWebSite.Models;

namespace DynamicDataWebSite.Account
{
    public partial class Manage : System.Web.UI.Page
    {
        protected string SuccessMessage
        {
            get;
            private set;
        }
        
        private bool HasPassword(ApplicationUserManager manager)
        {
            return manager.HasPassword(User.Identity.GetUserId());
        }

        public bool HasPhoneNumber { get; private set; }

        public bool TwoFactorEnabled { get; private set; }

        public bool TwoFactorBrowserRemembered { get; private set; }

        public int LoginsCount { get; set; }

        public string EditDetails
        {
            get
            {
                return editDetails;
            }

            set
            {
                editDetails = value;
            }
        }

        public string ViewProp
        {
            get
            {
                return viewProp;
            }

            set
            {
                viewProp = value;
            }
        }

        public string UsersListLink
        {
            get
            {
                return usersListLink;
            }

            set
            {
                usersListLink = value;
            }
        }

        public string RolesLink { get => rolesLink; set => rolesLink = value; }

        string editDetails;
        string usersListLink;
        string rolesLink;
        protected void Page_Load()
        {
            string title = GetGlobalResourceObject("Account", "Page_Manage_Name") as string;
            Title = title != "" ? title : "Manage Account";
            Title = title;
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var userid = User.Identity.GetUserId();
            if (userid != null)
            {
                HasPhoneNumber = String.IsNullOrEmpty(manager.GetPhoneNumber(userid));
                if (!(User.IsInRole("StackHolder") || User.IsInRole("Admin")))
                    Response.Redirect("/" + Resources.Route.Account + "/" + Resources.Route.CompleteData);
                // Enable this after setting up two-factor authentientication
                //PhoneNumber.Text = manager.GetPhoneNumber(User.Identity.GetUserId()) ?? String.Empty;

                TwoFactorEnabled = manager.GetTwoFactorEnabled(User.Identity.GetUserId());

                LoginsCount = manager.GetLogins(userid).Count;
            
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            UsersListLink = "/" + Resources.Route.Account + "/" + Resources.Route.UserList;
            RolesLink = "/" + Resources.Route.ManageRoles;
            A1.HRef = UsersListLink;
            rolehy.HRef = RolesLink;
            if (!IsPostBack)
            {
                // Determine the sections to render
                if (HasPassword(manager))
                {
                    ChangePassword.Visible = true;
                }
                else
                {
                    CreatePassword.Visible = true;
                    ChangePassword.Visible = false;
                }
                ChangePassword.NavigateUrl = "/" + Resources.Route.Account + "/" + Resources.Route.ManagePassword;
                // Render success message
                var message = Request.QueryString["m"];
                if (message != null)
                {
                    // Strip the query string from action
                    Form.Action = ResolveUrl("~/Account/Manage");

                    SuccessMessage =
                        message == "ChangePwdSuccess" ? Convert.ToString(HttpContext.GetGlobalResourceObject("Account", "Page_Manage_SuccessMessage_ChangePwdSuccess"))
                        : message == "SetPwdSuccess" ? Convert.ToString(HttpContext.GetGlobalResourceObject("Account", "Page_Manage_SuccessMessage_SetPwdSuccess"))
                        : message == "RemoveLoginSuccess" ? Convert.ToString(HttpContext.GetGlobalResourceObject("Account", "Page_Manage_SuccessMessage_RemoveLoginSuccess"))
                        : message == "AddPhoneNumberSuccess" ? Convert.ToString(HttpContext.GetGlobalResourceObject("Account", "Page_Manage_SuccessMessage_AddPhoneNumberSuccess"))
                        : message == "RemovePhoneNumberSuccess" ? Convert.ToString(HttpContext.GetGlobalResourceObject("Account", "Page_Manage_SuccessMessage_RemovePhoneNumberSuccess"))
                        : String.Empty;
                    successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
                }
                //details link
                string s = UserProfile.GetFullName(userid);

                if (s != "NO_ERR")
                {
                    string sd = Global.DefaultModel.GetActionPath("Users", "Edit", null) + "?User_ID=" + userid;
                    string vd = Global.DefaultModel.GetActionPath("Properties", "List", null) + "?User_ID=" + userid;
                    EditDetails = sd;
                    ViewProp = vd;
                    LtDt.Text = Resources.Account.Page_Manage_Details;
                    viewPropLt.Text = Resources.Account.Page_Manage_Property;
                    rolelbl.Text = Resources.Account.Page_Manage_Role;
                    Literal1.Text = Resources.Account.View_Users;

                }
                else
                {
                    EditDetails = Global.DefaultModel.GetActionPath("Users", "Insert", null);
                }
                if (!User.IsInRole("Admin"))
                {
                    A1.Visible = Literal1.Visible = rolelbl.Visible = rolehy.Visible = rolehylble.Visible = false;
                }
                else
                {
                    viewPropLt.Visible = LtView.Visible = LtDt.Visible = LtEditHy.Visible = false;

                }
                //set user data
                DynamicDataModel.Model.User u = UserProfile.GetUserData(userid);
                if (u != null)
                {
                    FirstName.Text = u.First_Name;
                    LastName.Text = u.Last_Name;
                    Address.Text = u.Address;
                    Email.Text = u.Email;
                    Phone.Text = u.Phone_Num.ToString();
                }
            }
        }
            else
            {
                Response.Redirect("~/");
            }
        }
        string viewProp;
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        public string Get_Details_Link(bool edit)
        {
            string s = UserProfile.GetFullName(User.Identity.GetUserId());
            string sd, vd = "";
            if (s != "NO_ERR")
            {
                 sd ="/"+Resources.Route.Account+"/"+ Resources.Route.Edit+"/" + User.Identity.GetUserId();
                 vd = "/"+Resources.Route.Added+"/" + User.Identity.GetUserId();
            }
            else
            {
                sd = EditDetails = "/" + Resources.Route.Account + "/" + Resources.Route.CompleteData;
            }
            return edit ? sd : vd;
        }

        // Remove phonenumber from user
        protected void RemovePhone_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var result = manager.SetPhoneNumber(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return;
            }
            var user = manager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                Response.Redirect("/Account/Manage?m=RemovePhoneNumberSuccess");
            }
        }

        // DisableTwoFactorAuthentication
        protected void TwoFactorDisable_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            manager.SetTwoFactorEnabled(User.Identity.GetUserId(), false);

            Response.Redirect("/Account/Manage");
        }

        //EnableTwoFactorAuthentication 
        protected void TwoFactorEnable_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            manager.SetTwoFactorEnabled(User.Identity.GetUserId(), true);

            Response.Redirect("/Account/Manage");
        }
    }
}