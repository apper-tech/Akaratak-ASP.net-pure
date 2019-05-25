using DynamicDataModel;
using DynamicDataWebSite.Classes;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.DynamicData;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace DynamicDataWebSite
{
    public partial class Site : System.Web.UI.MasterPage
    {
        /// <summary>
        /// ajaxScriptManager control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        public global::AjaxControlToolkit.ToolkitScriptManager ajaxScriptManager;

        //From VisualStudio Web Forms Template
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            //if (SessionData.CurrentUserName == -1 && !Request.Url.OriginalString.Contains("/AccessDenied.aspx"))
            //{
            //    Response.ClearContent();
            //    Response.Redirect("~/AccessDenied.aspx");
            //}
            //this.TopMenu.DataSourceID = null;
            //this.TopMenu.DataSource = new IconList();

            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }
        string username;

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        public string WelcomeText
        {
            get
            {
                return welcomeText;
            }

            set
            {
                welcomeText = value;
            }
        }

        public string AccountUrl
        {
            get
            {
                return accountUrl;
            }

            set
            {
                accountUrl = value;
            }
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = manager.FindById(Context.User.Identity.GetUserId());
                if (user != null)
                {
                    username = UserProfile.GetFullName(user.Id);
                    if(Username=="NO_ERR")
                    {
                        AccountUrl = Global.DefaultModel.GetActionPath("Users", "Insert", null);
                        WelcomeText= GetGlobalResourceObject("DynamicData", "CompleteProfile").ToString();
                    }
                    else
                    {
                        AccountUrl = "/Account/Manage";
                        WelcomeText = GetGlobalResourceObject("DynamicData", "Welcome") + "  :  " + Username;
                    }
                  
                    if (Request.Cookies.Get("CurrentUser") == null)
                    {
                        Response.Cookies.Set(new HttpCookie("CurrentUser", user.Id));
                    }
                    if (Session["CurrentUserID"] == null)
                    {
                        Session["CurrentUserID"] = user.Id;
                    }
                }
            }
            catch {
                Response.Cookies.Set(new HttpCookie("CurrentUser", null));

                Session["CurrentUserID"] = null;

            }
        }

        string accountUrl;
        string welcomeText;
       protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Session["CurrentUserID"] = null;
            Response.Cookies.Remove("CurrentUser");
            Context.GetOwinContext().Authentication.SignOut();
        }
    }
}
