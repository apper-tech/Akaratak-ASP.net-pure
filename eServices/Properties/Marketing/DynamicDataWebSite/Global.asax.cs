using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.DynamicData;
using System.Web.Routing;
using System.Web.UI;
using DynamicDataWebSite;
using NotAClue.Web.DynamicData;
using System.Data.Entity.Core.Objects;
using DynamicDataLibrary.ModelRelated;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using DynamicDataModel.Model;
using DynamicDataModel;
using System.Web.Optimization;
using Resources;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Identity;

namespace DynamicDataWebSite
{
    public class Global : System.Web.HttpApplication
    {
        private static MetaModel s_defaultModel = new AdvancedMetaModel();
        public static MetaModel DefaultModel
        {
            get
            {
                return s_defaultModel;
            }
        }
        public static void TransolateRoutes(System.Globalization.CultureInfo culture, RouteCollection routes,bool _static)
        {
            if (_static)
            {
                routes.Add(new DynamicDataRoute("List")
                {
                    Constraints = new RouteValueDictionary(new { action = "List" }),
                    Action = PageAction.List,
                    Model = DefaultModel,
                    Table = "Properties",
                    ViewName = "ListWithIcons"

                });
                routes.Add(new DynamicDataRoute("List/{Property_Category.Cat_Name}/{Property_Type.Property_Type_Name}")
                {
                    Constraints = new RouteValueDictionary(new { action = "List" }),
                    Action = PageAction.List,
                    Model = DefaultModel,
                    Table = "Properties",
                    ViewName = "ListWithIcons"

                });
                routes.Add(new DynamicDataRoute("Edit" + "/{PropertyID}")
                {
                    Action = PageAction.Edit,
                    Model = DefaultModel,
                    Table = "Properties",
                });
                routes.Add(new DynamicDataRoute("Details" + "/{PropertyID}")
                {
                    Constraints = new RouteValueDictionary(new { action = "Details" }),
                    Action = PageAction.Details,
                    Table = "Properties",
                    Model = DefaultModel,
                    ViewName = "Details"
                });
                routes.Add(new DynamicDataRoute("Insert")
                {
                    Constraints = new RouteValueDictionary(new { action = "Insert" }),
                    Action = PageAction.Insert,
                    Model = DefaultModel,
                    Table = "Properties",
                    ViewName = "Insert"

                });
                routes.Add(new DynamicDataRoute("Details" + "/{PropertyID}/{Country.Country_Name}/{City.City_Name}/{Property_Category.Cat_Name}/{Property_Type.Property_Type_Name}/{Address}")
                {
                    Constraints = new RouteValueDictionary(new { action = "Details" }),
                    Action = PageAction.Details,
                    Table = "Properties",
                    Model = DefaultModel,
                    ViewName = "Details"
                });
                ///users
                routes.Add(new DynamicDataRoute("List")
                {
                    Constraints = new RouteValueDictionary(new { action = "List" }),
                    Action = PageAction.List,
                    Model = DefaultModel,
                    Table = "Users",
                    ViewName = "List"

                });
                routes.Add(new DynamicDataRoute("Edit")
                {
                    Constraints = new RouteValueDictionary(new { action = "Edit" }),
                    Action = PageAction.List,
                    Model = DefaultModel,
                    Table = "Users",
                    ViewName = "Edit"

                });
                routes.Add(new DynamicDataRoute("Account" + "/Details" + "/{User_ID}")
                {
                    Constraints = new RouteValueDictionary(new { action = "Details" }),
                    Action = PageAction.Details,
                    Model = DefaultModel,
                    Table = "Users",
                    ViewName = "Details"

                });
                routes.Add(new DynamicDataRoute("Account" + "/CompleteData")
                {
                    Constraints = new RouteValueDictionary(new { action = "Insert" }),
                    Action = PageAction.Insert,
                    Model = DefaultModel,
                    Table = "Users",
                    ViewName = "Insert"

                });
            }
            else
            {
                Resources.Route.Culture = culture;
                routes.Add(new DynamicDataRoute(Resources.Route.Account + "/" + Resources.Route.UserList)
                {
                    Constraints = new RouteValueDictionary(new { action = "List" }),
                    Action = PageAction.List,
                    Model = DefaultModel,
                    Table = "Users",
                    ViewName = "List"

                });
                routes.Add(new DynamicDataRoute(Resources.Route.Account + "/" + Resources.Route.CompleteData)
                {
                    Constraints = new RouteValueDictionary(new { action = "insert" }),
                    Action = PageAction.Insert,
                    Model = DefaultModel,
                    Table = "Users",
                    ViewName = "Insert"

                });
                routes.Add(new DynamicDataRoute(Resources.Route.Account + "/" + Resources.Route.Edit + "/{User_ID}")
                {
                    Constraints = new RouteValueDictionary(new { action = "Edit" }),
                    Action = PageAction.Edit,
                    Model = DefaultModel,
                    Table = "Users",
                    ViewName = "Edit"

                });
                routes.Add(new DynamicDataRoute(Resources.Route.Account + "/" + Resources.Route.Details + "/{User_ID}")
                {
                    Constraints = new RouteValueDictionary(new { action = "Details" }),
                    Action = PageAction.Details,
                    Model = DefaultModel,
                    Table = "Users",
                    ViewName = "Details"

                });
                routes.Add(new DynamicDataRoute(Resources.Route.List)
                {
                    Constraints = new RouteValueDictionary(new { action = "List" }),
                    Action = PageAction.List,
                    Model = DefaultModel,
                    Table = "Properties",
                    ViewName = "ListWithIcons"
                });
                routes.Add(new DynamicDataRoute(Resources.Route.List + "/{Property_Category.Cat_Name}/{Property_Type.Property_Type_Name}")
                {
                    Constraints = new RouteValueDictionary(new { action = "List" }),
                    Action = PageAction.List,
                    Model = DefaultModel,
                    Table = "Properties",
                    ViewName = "ListWithIcons"

                });
                routes.Add(new DynamicDataRoute(Resources.Route.Added + "/{User_ID}")
                {
                    Constraints = new RouteValueDictionary(new { action = "List" }),
                    Action = PageAction.List,
                    Model = DefaultModel,
                    Table = "Properties",
                    ViewName = "List"
                });
                routes.Add(new DynamicDataRoute(Resources.Route.Edit + "/{PropertyID}")
                {
                    Action = PageAction.Edit,
                    Model = DefaultModel,
                    Table = "Properties"
                });
                routes.Add(new DynamicDataRoute(Resources.Route.Details + "/{PropertyID}")
                {
                    Constraints = new RouteValueDictionary(new { action = "Details" }),
                    Action = PageAction.Details,
                    Table = "Properties",
                    Model = DefaultModel,
                    ViewName = "Details"
                });
                routes.Add(new DynamicDataRoute(Resources.Route.Details + "/{PropertyID}/{Country.Country_Name}/{City.City_Name}/{Property_Category.Cat_Name}/{Property_Type.Property_Type_Name}/{Address}")
                {
                    Constraints = new RouteValueDictionary(new { action = "Details" }),
                    Action = PageAction.Details,
                    Table = "Properties",
                    Model = DefaultModel,
                    ViewName = "Details"
                });
                routes.Add(new DynamicDataRoute(Resources.Route.Insert)
                {
                    Constraints = new RouteValueDictionary(new { action = "Insert" }),
                    Action = PageAction.Insert,
                    Table = "Properties",
                    Model = DefaultModel,
                    ViewName = "Insert"
                });
                routes.Add(new DynamicDataRoute(Resources.Route.Insert + "/{Property_Category.Cat_Name}/{Property_Type.Property_Type_Name}")
                {
                    Constraints = new RouteValueDictionary(new { action = "Insert" }),
                    Action = PageAction.Insert,
                    Model = DefaultModel,
                    Table = "Properties",
                    ViewName = "Insert"

                });
            }
        }
        //public static ObjectContext objectContext;
        public static void RegisterRoutes(RouteCollection routes)
        {
            //                    IMPORTANT: DATA MODEL REGISTRATION 
            // Uncomment this line to register an ADO.NET Entity Framework model for ASP.NET Dynamic Data.
            // Set ScaffoldAllTables = true only if you are sure that you want all tables in the
            // data model to support a scaffold (i.e. templates) view. To control scaffolding for
            // individual tables, create a partial class for the table and apply the
            // [ScaffoldTable(true)] attribute to the partial class.
            // Note: Make sure that you change "YourDataContextType" to the name of the data context
            // class in your application.
            // See http://go.microsoft.com/fwlink/?LinkId=257395 for more information on how to add and configure an Entity Data model to this project

            
            DefaultModel.RegisterContext(
                new Microsoft.AspNet.DynamicData.ModelProviders.EFDataModelProvider(() => new Entities()),
                new ContextConfiguration() { ScaffoldAllTables = true });
            //we add default routes for DD to recognize and function
            #region defaultroutes
            TransolateRoutes(null, routes, true);
            #endregion
            var c = DynamicDataHelper.DefaultCulture;
            // we add transolated route for multilang int 
            #region transolatedroutes
            //trans for en//////////////////////////////////////////////////////////////////////////////////
            TransolateRoutes(new System.Globalization.CultureInfo("en"), routes,false);
            //trans for ar//////////////////////////////////////////////////////////////////////////////////////
            TransolateRoutes(new System.Globalization.CultureInfo("ar"), routes, false);
            //trans for tr//////////////////////////////////////////////////////////////////////////////////////
            TransolateRoutes(new System.Globalization.CultureInfo("tr"), routes, false);
            #endregion
            Resources.Route.Culture = System.Globalization.CultureInfo.CurrentCulture;
            //ObjectContext objectContext = null;
            //DefaultModel.RegisterContext(() =>
            //{
            //    return objectContext = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)new Entities(true, DefaultModel)).ObjectContext;
            //}, new ContextConfiguration() { ScaffoldAllTables = true });

            // The following statement supports separate-page mode, where the List, Detail, Insert, and 
            // Update tasks are performed by using separate pages. To enable this mode, uncomment the following 
            // route definition, and comment out the route definitions in the combined-page mode section that follows.
            //routes.Add(new DynamicDataRoute("{table}/{action}")
            //{
            //    //Constraints = new RouteValueDictionary(new { action = "List|ListOriginal|ListDetails|Details|DetailsOriginal|Edit|Insert|ListViewOnly|Report|Default" }),
            //    Constraints = new RouteValueDictionary(new { action = "List|ListWithIcons|ListOriginal|Edit|Insert|Report|Default|Details|ListDetails" }),
            //    Model = DefaultModel
            //});
            //routes.Add(new DynamicDataRoute("Dynamic/{table}/{action}")
            //{
            //    Constraints = new RouteValueDictionary(new
            //    {
            //        action = "ListViewOnly",
            //        table = "View_AbsenceCount"
            //    }),
            //    Model = DefaultModel
            //});
            // The following statements support combined-page mode, where the List, Detail, Insert, and
            // Update tasks are performed by using the same page. To enable this mode, uncomment the
            // following routes and comment out the route definition in the separate-page mode section above.
            //routes.Add(new DynamicDataRoute("{table}/ListDetails.aspx") {
            //    Action = PageAction.List,
            //    ViewName = "ListDetails",
            //    Model = DefaultModel
            //});

            //routes.Add(new DynamicDataRoute("{table}/ListDetails.aspx") {
            //    Action = PageAction.Details,
            //    ViewName = "ListDetails",
            //    Model = DefaultModel
            //});

        }


        private static void RegisterScripts()
        {
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/Scripts/jquery-1.7.1.min.js",
                DebugPath = "~/Scripts/jquery-1.7.1.js",
                CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.1.min.js",
                CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.1.js",
                CdnSupportsSecureConnection = true,
                LoadSuccessExpression = "window.jQuery"
            });
        }

        static System.Threading.Timer timerService;
        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterScripts();

            ////Run now:
            //new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(ImportDataFromService)).Start();

            ////Run at every mid-night:
            //timerService = new System.Threading.Timer(this.ImportDataFromService,
            //    null,
            //    new TimeSpan(24, 0, 0) - DateTime.Now.TimeOfDay,
            //    new TimeSpan(24, 0, 0));
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            // An error has occured on a .Net page.
            var serverError = Server.GetLastError() as HttpException;

            if (null != serverError)
            {
                int errorCode = serverError.GetHttpCode();

                if (404 == errorCode)
                {
                    Server.ClearError();
                    Response.Redirect("~/");
                }
            }
        }
        void Application_BeginRequest(object sender, System.EventArgs e)
        {
            string name = Context.Request.Url.LocalPath;
            RewriteURL(name);
        }
            void Application_EndRequest(object sender, System.EventArgs e)
        {
            if (((Response.StatusCode == 401)
            && (Request.IsAuthenticated == true)))
            {
                Response.ClearContent();
                Response.Redirect("~/AccessDenied.aspx");
            }
        }
        void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            string name = Context.Request.Url.LocalPath;
            if (name.Contains(Resources.Route.Added )|| name.Contains(Resources.Route.Edit))
            {
                RewriteUrlSession(name);
            }
        }
        void Session_Start(object sender, EventArgs e)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    if (!SessionData.FillDataSession(entities))
                    {
                        Response.Redirect("~/AccessDenied.aspx");
                    }

                }
            }
            catch (Exception)
            {

            }
        }
        private void RewriteURL(string name)
        {
            //get current request for redirection
            HttpContext incoming = HttpContext.Current;
            //check for a file instead of a route (fix for manual file loading)
            string ext = System.IO.Path.GetExtension(name);
            if (string.IsNullOrEmpty(ext))
            {
                if(name.ToLower().Contains("webhook"))
                {
                    string path = "~/" + "TelegramBot_WebHook.aspx" + "?";
                    for (int i = 0; i < Request.QueryString.Count; i++)
                    {
                        path += Request.QueryString.AllKeys[i] + "=" + Request.QueryString[i]+"&";
                    }
                    path = path.TrimEnd('&');
                    incoming.RewritePath(path, false);
                }
                //loop throught resources and assign each requested model
                string list = Resources.Route.ResourceManager.GetString("List", new System.Globalization.CultureInfo(DynamicDataHelper.DefaultCulture));
                if (name.ToLower().Contains(Resources.Route.List.ToLower()) ||
                    name.ToLower().Contains(list.ToLower()))
                {
                        string par = DynamicDataHelper.RewriteRouteUrl(incoming.Request.QueryString, incoming.Request.Url);
                        incoming.RewritePath("~/" + Resources.Route.List + par, false); return;
                 
                }
                if (name.ToLower().Replace("/", "").Contains("List".ToLower()))
                {
                    incoming.Response.Redirect("~/" + Resources.Route.List);return;
                }
                string about = Resources.Route.ResourceManager.GetString("About", new System.Globalization.CultureInfo(DynamicDataHelper.DefaultCulture));
                if (name.ToLower().Contains(Resources.Route.About.ToLower()) ||
                   name.ToLower().Contains(about.ToLower()))
                {
                    incoming.RewritePath("~/about", false); return;
                }
                string feed = Resources.Route.ResourceManager.GetString("Feed", new System.Globalization.CultureInfo(DynamicDataHelper.DefaultCulture));
                if (name.ToLower().Contains(Resources.Route.Feed.ToLower()) ||
                   name.ToLower().Contains(feed.ToLower()))
                {
                    incoming.RewritePath("~/feed", false); return;
                }
                string policy = Resources.Route.ResourceManager.GetString("Policy", new System.Globalization.CultureInfo(DynamicDataHelper.DefaultCulture));
                if (name.ToLower().Contains(Resources.Route.Policy.ToLower()) ||
                   name.ToLower().Contains(policy.ToLower()))
                {
                    incoming.RewritePath("~/PrivacyPolicy", false); return;
                }
                string terms = Resources.Route.ResourceManager.GetString("Terms", new System.Globalization.CultureInfo(DynamicDataHelper.DefaultCulture));
                if (name.ToLower().Contains(Resources.Route.Terms.ToLower()) ||
                   name.ToLower().Contains(terms.ToLower()))
                {
                    incoming.RewritePath("~/Terms", false); return;
                }
                string carrer = Resources.Route.ResourceManager.GetString("Carrer", new System.Globalization.CultureInfo(DynamicDataHelper.DefaultCulture));
                if (name.ToLower().Contains(Resources.Route.Carrer.ToLower()) ||
                   name.ToLower().Contains(carrer.ToLower()))
                {
                    incoming.RewritePath("~/Carrer", false); return;
                }
                string contact = Resources.Route.ResourceManager.GetString("Contact", new System.Globalization.CultureInfo(DynamicDataHelper.DefaultCulture));
                if (name.ToLower().Contains(Resources.Route.Contact.ToLower()) ||
                   name.ToLower().Contains(contact.ToLower()))
                {
                    incoming.RewritePath("~/Contact", false); return;
                }
                string login = Resources.Account.ResourceManager.GetString("Login", new System.Globalization.CultureInfo(DynamicDataHelper.DefaultCulture));
                if (name.ToLower().Contains(Resources.Account.Login.ToLower()) ||
                   name.ToLower().Contains(login.ToLower()))
                {
                    incoming.RewritePath("~/Account/Login", false); return;
                }
                string register = Resources.Account.ResourceManager.GetString("Register", new System.Globalization.CultureInfo(DynamicDataHelper.DefaultCulture));
                if (name.ToLower().Contains(Resources.Account.Register.ToLower()) ||
                   name.ToLower().Contains(register.ToLower()))
                {
                    incoming.RewritePath("~/Account/Register", false); return;
                }
                string manage = Resources.Route.ResourceManager.GetString("Manage", new System.Globalization.CultureInfo(DynamicDataHelper.DefaultCulture));
                if (name.ToLower().Contains(Resources.Route.Manage.ToLower()) ||
                   name.ToLower().Contains(manage.ToLower()))
                {
                    incoming.RewritePath("~/Account/Manage", false); return;
                }
                string password = Resources.Route.ResourceManager.GetString("ManagePassword", new System.Globalization.CultureInfo(DynamicDataHelper.DefaultCulture));
                if (name.ToLower().Contains(Resources.Route.ManagePassword.ToLower()) ||
                    name.ToLower().Contains(password.ToLower()))
                {
                    incoming.RewritePath("~/Account/ManagePassword", false); return;
                }
                string roles = Resources.Route.ResourceManager.GetString("ManageRoles", new System.Globalization.CultureInfo(DynamicDataHelper.DefaultCulture));
                if (name.ToLower().Contains(Resources.Route.ManageRoles.ToLower()) ||
                    name.ToLower().Contains(roles.ToLower()))
                {
                    incoming.RewritePath("~/Account/Role/RolesManage", false); return;
                }
                string insrt = Resources.Route.ResourceManager.GetString("Insert", new System.Globalization.CultureInfo(DynamicDataHelper.DefaultCulture));
                if (name.ToLower().Contains(Resources.Route.Insert.ToLower()) ||
                    name.ToLower().Contains(insrt.ToLower()))
                {
                    incoming.RewritePath("/" + Resources.Route.Insert, false); return;
                }
            }
        }
        private void RewriteUrlSession(string name)
        {
            HttpContext incoming = HttpContext.Current;
            string edituser = Resources.Route.ResourceManager.GetString("Edit", new System.Globalization.CultureInfo(DynamicDataHelper.DefaultCulture));
            string user = Resources.Route.ResourceManager.GetString("Account", new System.Globalization.CultureInfo(DynamicDataHelper.DefaultCulture));
            if ((name.ToLower().Contains(Resources.Route.Edit.ToLower()) ||
               name.ToLower().Contains(edituser.ToLower())) &&
               (name.ToLower().Contains(Resources.Route.Account.ToLower()) ||
               name.ToLower().Contains(user.ToLower())))
            {
                incoming.RewritePath("~/" + Resources.Route.Account + "/" + Context.User.Identity.GetUserId() != null ? Context.User.Identity.GetUserId() : "", false);
            }
            string myhouse = Resources.Route.ResourceManager.GetString("Added", new System.Globalization.CultureInfo(DynamicDataHelper.DefaultCulture));
            if (name.ToLower().Contains(Resources.Route.Added.ToLower()) ||
               name.ToLower().Contains(myhouse.ToLower()))
            {
                incoming.RewritePath("~/" + Resources.Route.Added + "/" + Context.User != null ? Context.User.Identity.GetUserId() : "", false);
            }
        }
        //public void ImportDataFromService(object state)
        //{
        //    try
        //    {
        //        wsStarWays ws = new wsStarWays();
        //        using (Entities entities = new Entities())
        //        {

        //            #region Update Ranks
        //            ClsRanks[] serviceRanks = ws.GetFullRanks();
        //            for (int i = 0; i < serviceRanks.Length; i++)
        //            {
        //                int rankId = serviceRanks[i].RankID;
        //                Rank rank = entities.Ranks.Where(r => r.Id == rankId).FirstOrDefault();
        //                int serviceRankType = serviceRanks[i].RankSystem == Systems.Officers ? 1 : (serviceRanks[i].RankSystem == Systems.Persons ? 2 : 3);
        //                if (rank == null)
        //                    entities.Ranks.Add(new Rank()
        //                    {
        //                        Id = serviceRanks[i].RankID,
        //                        Name = serviceRanks[i].RankName.Trim(),
        //                        RankOrder = serviceRanks[i].RankOrder,
        //                        TypeId = serviceRankType
        //                    });
        //                else
        //                {
        //                    rank.Id = serviceRanks[i].RankID;
        //                    rank.Name = serviceRanks[i].RankName.Trim();
        //                    rank.RankOrder = serviceRanks[i].RankOrder;
        //                    rank.TypeId = serviceRankType;
        //                }
        //            }
        //            #endregion

        //            #region Update Employees
        //            ClsEmployees[] serviceEmps;
        //            if (ApplicationSettings.DepartmentIdAtService != -1)
        //                serviceEmps = ws.GetEmployeesByDepartmentID(ApplicationSettings.DepartmentIdAtService);
        //            else
        //                serviceEmps = ws.GetFullEmployees();

        //            if (serviceEmps != null)
        //                for (int i = 0; i < serviceEmps.Length; i++)
        //                {
        //                    //if (String.IsNullOrEmpty(serviceEmps[i].DomainUserName))
        //                    //    if (serviceEmps[i].IDNum != null)
        //                    //        serviceEmps[i].DomainUserName = serviceEmps[i].IDNum;
        //                    //    else
        //                    //    continue;
        //                    if (!String.IsNullOrEmpty(serviceEmps[i].DomainUserName))
        //                    {
        //                        if (!serviceEmps[i].DomainUserName.Contains("\\")
        //                            && !serviceEmps[i].DomainUserName.Contains("@"))
        //                            serviceEmps[i].DomainUserName = String.Format("{0}\\{1}",
        //                                System.Configuration.ConfigurationManager.AppSettings["DomainName"]
        //                                , serviceEmps[i].DomainUserName);
        //                        serviceEmps[i].DomainUserName = serviceEmps[i].DomainUserName.ToLower();
        //                    }

        //                    string emplyeeId = serviceEmps[i].IDNum;
        //                    Employee emp = entities.Employees.Where(e => e.IdentityNumber == emplyeeId).FirstOrDefault();

        //                    if (emp != null && emp.UserName != serviceEmps[i].DomainUserName)
        //                    {
        //                        entities.Employees.Remove(emp);
        //                        emp = null;
        //                    }

        //                    if (emp == null)
        //                        entities.Employees.Add(new Employee()
        //                        {
        //                            Name = serviceEmps[i].FullName,
        //                            IdentityNumber = serviceEmps[i].IDNum,
        //                            Mobile = serviceEmps[i].MobileNum,
        //                            OfficialNumber = serviceEmps[i].MiliratyNum,
        //                            RankId = serviceEmps[i].RankID,
        //                            UserName = serviceEmps[i].DomainUserName,
        //                        });
        //                    else
        //                    {
        //                        emp.Name = serviceEmps[i].FullName;
        //                        emp.IdentityNumber = serviceEmps[i].IDNum;
        //                        emp.Mobile = serviceEmps[i].MobileNum;
        //                        emp.OfficialNumber = serviceEmps[i].MiliratyNum;
        //                        emp.RankId = serviceEmps[i].RankID;
        //                        emp.UserName = serviceEmps[i].DomainUserName;
        //                    }
        //                }
        //            #endregion

        //            entities.SaveChanges();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}
    }
}
