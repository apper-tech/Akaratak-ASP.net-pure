using DynamicDataModel;
using DynamicDataWebSite.Classes;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using System.Web.DynamicData;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Web.UI.HtmlControls;

namespace DynamicDataWebSite
{
    public partial class Site : System.Web.UI.MasterPage
    {
        #region Property
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

        public bool IsAdmin
        {
            get
            {
                return isAdmin;
            }

            set
            {
                isAdmin = value;
            }
        }

        public string UserText
        {
            get
            {
                return userText;
            }

            set
            {
                userText = value;
            }
        }
        #endregion
        #region Load
        protected void Page_Init(object sender, EventArgs e)
        {
            AutoCheckLLanguage(true, "");
            //ChangeCulture("en");
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
        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            // ChangeCulture("en");
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
            sitemap.DataSource = sitemapSource;
        }
        bool isAdmin;
        string userText;
      
        protected void Page_Load(object sender, EventArgs e)
        {
            InitilizeUser();
            Page.MetaKeywords = Resources.DynamicData.Keywords;
        }
        #endregion
        #region User
        public void InitilizeUser()
        {
            try
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = manager.FindById(Context.User.Identity.GetUserId());
                if (user != null)
                {
                    Username = UserProfile.GetFullName(user.Id);
                    if (Username == "NO_ERR")
                    {
                        AccountUrl = "/"+Resources.Route.Account + "/" + Resources.Route.CompleteData;
                        WelcomeText = GetGlobalResourceObject("Account", "Complete_Profile").ToString();
                        Username = WelcomeText;
                    }
                    else
                    {
                        if (Username == "NULL_ERR")
                        {
                            AccountUrl = "/"+Resources.Route.Account + "/" + Resources.Route.Edit;
                            WelcomeText = GetGlobalResourceObject("Account", "Complete_Profile").ToString();
                            Username = WelcomeText;
                        }
                        else
                        {
                            isAdmin = SecurityHandler.UserInRole(user.Roles, "Admin");
                            AccountUrl = "/" + Resources.Route.Account + "/" + Resources.Route.Manage;
                            WelcomeText = "" + GetGlobalResourceObject("Account", "Page_Manage_Name");
                        }
                    }
                    UserText = GetLoggedUserView(isAdmin, accountUrl);
                    if (Request.Cookies.Get("CurrentUser") == null)
                    {
                        Response.Cookies.Set(new HttpCookie("CurrentUser", user.Id));
                    }
                    if (Session["CurrentUserID"] == null)
                    {
                        Session["CurrentUserID"] = user.Id;
                    }
                    var d = LoginView.FindControl("logoutST") as LoginStatus;
                    d.LogoutText = GetAccountText("Lgt");
                }
            }
            catch (Exception ex)
            {
                Response.Cookies.Set(new HttpCookie("CurrentUser", null));

                Session["CurrentUserID"] = null;

            }
        }
        string accountUrl;
        string welcomeText;
        public string GetLoggedUserView(bool isad, string accu)
        {
            if (accu == "/Account/Manage")
            {
                if (isad)
                {
                    return @"&nbsp;&nbsp;&nbsp;&nbsp;   <img src=' / CustomDesign / images / admin.png' style='width: 40px' alt='ادارة الموقع' />  &nbsp;&nbsp;&nbsp;&nbsp;";
                }
                else
                {
                    return @"&nbsp;&nbsp;&nbsp;&nbsp;   <img src=' / CustomDesign / images / mng.png' style='width: 40px' alt='ادارة الحساب' />  &nbsp;&nbsp;&nbsp;&nbsp;";
                }
            }
            else
            {
                return @"&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;  <img src=' / CustomDesign / images / err.png' style='width: 40px' alt='اكمال البيانات' />  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ";
            }
        }
        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Session["CurrentUserID"] = null;
            Response.Cookies.Remove("CurrentUser");
            Context.GetOwinContext().Authentication.SignOut();
        }
        public string GetAccountText(string name)
        {
            if (name == "Lgt")
            {
                string text = "<span data-letters=\"" + GetAccountText("Logout") + "\">" + GetAccountText("Logout") + "</span>";
                return text;
            }
            if (name == "Complete_Profile")
            {
                if (AccountUrl == "/"+Resources.Route.Account+"/"+Resources.Route.Manage)
                {
                    return Username;
                }
                else
                {
                    return Resources.DynamicData.CompleteProfile;
                }
            }
            return string.IsNullOrEmpty(GetGlobalResourceObject("Account", name).ToString()) ? "" : GetGlobalResourceObject("Account", name).ToString();
        }
        #endregion
        #region Language
        public void AutoCheckLLanguage(bool load,string cult)
        {
            if (load)
            {
                //check url for arabic and change accordingly
                string cultload = "";

                if(Request.Url.Host.Contains("akaratak"))
                {
                    cultload = "en";
                }
                if (Request.Url.Host.Contains("xn--mgbaj0a2b2cl"))//عقاراتك
                {
                    cultload = "ar";
                }
                if (Request.Url.Host.Contains("emlaklariniz"))//emlaklariniz 
                {
                    cultload = "tr";
                }
                if (Request.Url.Host.Contains("xn--emlaklarnz-4ubb"))//emlaklarınız 
                {
                    cultload = "tr";
                }
                //if no session so this is the first load
                if (Session["Culture"] == null)
                {
                    ChangeCulture(cultload);
                }
                //this is a pst back
                else
                {
                   string name= ChangeCulture(Session["Culture"].ToString());
                }
            }
            else
            {
                ChangeCulture(cult);
                if(!Request.Url.Authority.Contains("local") && !Request.Url.Authority.Contains("test"))
                    switch(cult)
                    {
                        case "en":
                            Response.Redirect("http://akaratak.com");
                            break;
                        case "ar":
                            Response.Redirect("http://عقاراتك.com");
                            break;
                        case "tr":
                            Response.Redirect("http://xn--emlaklarnz-4ubb.com");
                            break;
                    }
                    Response.Redirect(Request.Url.AbsolutePath.Contains("Default") ? "~/" : Request.Url.AbsolutePath);
            }
          
        }      
        public string ChangeCulture(string cult)
        {
            System.Globalization.CultureInfo c = new System.Globalization.CultureInfo(WebConfigurationManager.AppSettings["DefaultCulture"]);
            if (cult == "load")
            {
                var dc = Session["Culture"] as System.Globalization.CultureInfo;
                if (dc != null)
                {
                    c = dc;
                }
            }
            else
                c = new System.Globalization.CultureInfo(cult);
            System.Threading.Thread.CurrentThread.CurrentCulture = c;
            System.Threading.Thread.CurrentThread.CurrentUICulture = c;
            Session["Culture"] = c;
            Resources.Route.Culture = c;
            return c.Name;
        }
        protected void lang_en_Click(object sender, EventArgs e)
        {
            AutoCheckLLanguage(false,"en");
        }
        protected void lang_ar_Click(object sender, EventArgs e)
        {
            AutoCheckLLanguage(false, "ar");
        }
        protected void lang_tr_Click(object sender, EventArgs e)
        {
            AutoCheckLLanguage(false, "tr");
        }
        #endregion

    }
}
