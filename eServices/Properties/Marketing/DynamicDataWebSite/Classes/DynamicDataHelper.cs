using DynamicDataLibrary.Attributes;
using DynamicDataWebSite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Data.SqlClient;
using System.Data;
using DynamicDataWebSite.DynamicData.FieldTemplates;
using DynamicDataModel.Model;
using System.Web.UI;
using DynamicDataLibrary;
using System.Resources;
using System.Globalization;
using System.Collections;
using System.Collections.Specialized;
using System.Net;
using DynamicDataWebSite.Classes;

namespace DynamicDataWebSite
{
    public class DynamicDataHelper
    {
        #region LIST      
        public static IList GetVisibleTables(bool donotShowViews = true)
        {
            System.Collections.IList visibleTables = Global.DefaultModel.VisibleTables;
            if (donotShowViews)
            {
                List<MetaTable> views = new List<MetaTable>();
                Global.DefaultModel.VisibleTables.FindAll(
                    m => m.Name.StartsWith("View_")).ForEach(
                        m => views.Add(m)
                    );
                views.ForEach(v => visibleTables.Remove(v));
            }
            return visibleTables;
        }
        public static List<BreadItem> UrlToBbread(Uri uri)
        {
            List<BreadItem> ls = new List<BreadItem>();
            foreach (var item in uri.Segments)
            {
                if (item == "/")
                {
                    ls.Add(new BreadItem { Url = "/", Title = "<i class='fa fa-home'></i>" });
                }
                else if (!item.Contains("aspx"))
                {
                    string rounditem = "";
                    foreach (var itm in uri.Segments)
                    {
                        if (itm != "/")
                            if (itm != item)
                            {
                                var t = RewriteRouteUrl("/"+itm);
                                rounditem += t;
                            }
                            else
                            {
                                var t = RewriteRouteUrl("/"+itm);
                                rounditem += t;
                                ls.Add(new BreadItem { Url = rounditem, Title = t.Replace("/", ""), Description = t.Replace("/", ""), Active = true });
                                break;
                            }
                    }
                    
                }
            }
            if (ls.Count > 1)
                ls[ls.Count - 1].Active = true;
            //foreach (var item in ls)
            //{
            //    if(item.Title==Resources.Route.Details)
            //    {
            //        List<BreadItem> t = new List<BreadItem>();
            //        t.Add(ls[0]);
            //        t.Add(ls[1]);
            //        return t;
            //    }
            //}
            return ls;
        }
        /// <summary>
        /// Get List of Properties for Repeaters
        /// </summary>
        /// <param name="count"> how many properties to return </param>
        /// <returns>list of a properties (ext_url is the details url)</returns>
        public static List<Property> GetListOfProperties(int count)
        {
            //entites to get list
            Entities entities = new Entities();
            // the source list
            List<Property> ls = entities.Properties.Where(w => w.PropertyID.ToString() != "").ToList();
            //the return after processing
            List<Property> res = new List<Property>();
            try
            {

                while (res.Count <= count)
                {
                    // randomly get properties to keep dynamic
                    var item = ls[(int)new Random().Next(0, ls.Count - 1)];
                    ///////////////get the first image path
                    string imagepath = ImagePathUpload.GetFileNames(item.Property_Photo, null)[0];
                    if (!string.IsNullOrEmpty(imagepath) && System.IO.File.Exists(HttpContext.Current.Server.MapPath(imagepath)))
                    {
                        ///////////////get the transolated name
                        string type = HttpContext.GetGlobalResourceObject("Property_Type", item.Property_Type.Property_Type_Name).ToString();
                        ///////////////get the url to details
                        string url = "/" + Resources.Route.Details + "/" + item.PropertyID;
                        url = RewriteRouteUrl(url, item.PropertyID.ToString(), PageAction.Details);
                        //////////////add to list
                        res.Add(new Property { Property_Type = new Property_Type { Property_Type_Name = type }, Property_Photo = imagepath, Url_ext = url });
                    }
                }
            }
            catch (Exception e)
            { return res; }
            return res;
        }
        /// <summary>
        /// get list of countries using entites
        /// </summary>
        /// <returns>list of Countires</returns>
        public static List<Country> GetCountryList()
        {  //entites to get list
            Entities entities = new Entities();
            // the source list
            List<Country> ls = entities.Countries.Where(w => w.Country_ID.ToString() != "").ToList();
            return ls;
        }
        /// <summary>
        /// converts date between hijri and georgian
        /// </summary>
        /// <param name="DateConv">input value</param>
        /// <param name="Calendar">Type: (Gregorian,Hijri)</param>
        /// <param name="DateLangCulture"></param>
        /// <returns>Converted date</returns>
        public static DateTime ConvertDateCalendar(DateTime DateConv, string Calendar, string DateLangCulture)
        {
            System.Globalization.DateTimeFormatInfo DTFormat;
            DateLangCulture = DateLangCulture.ToLower();
            /// We can't have the hijri date writen in English. We will get a runtime error - LAITH - 11/13/2005 1:01:45 PM -

            if (Calendar == "Hijri" && DateLangCulture.StartsWith("en-"))
            {
                DateLangCulture = "ar-sa";
            }

            /// Set the date time format to the given culture - LAITH - 11/13/2005 1:04:22 PM -
            DTFormat = new System.Globalization.CultureInfo(DateLangCulture, false).DateTimeFormat;

            /// Set the calendar property of the date time format to the given calendar - LAITH - 11/13/2005 1:04:52 PM -
            switch (Calendar)
            {
                case "Hijri":
                    DTFormat.Calendar = new System.Globalization.HijriCalendar();
                    break;

                case "Gregorian":
                    DTFormat.Calendar = new System.Globalization.GregorianCalendar();
                    break;

                default:
                    return DateTime.Now;
            }

            /// We format the date structure to whatever we want - LAITH - 11/13/2005 1:05:39 PM -
            DTFormat.ShortDatePattern = "dd/MM/yyyy";
            return DateTime.Parse(DateConv.Date.ToString("f", DTFormat));
        }
        /// <summary>
        /// get list of cites using enities
        /// </summary>
        /// <param name="code">the country id</param>
        /// <returns>list of cities</returns>
        public static List<City> GetCityList(string code)
        {
            //entites to get list
            Entities entities = new Entities();
            // the source list
            List<City> ls = entities.Cities.Where(w => w.Country_ID.ToString() == code).ToList();
            return ls;
        }
        public static User GetUserDetails(string id)
        {
            Entities entites = new Entities();
            if (!id.Contains("http"))
                {
                User user = entites.Users.Where(w => w.User_ID.ToString() == id).ToList()[0];
                return user;
            }
            return null;
        }
        /// <summary>
        /// get the top level tables in Context
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<AddIconToHomePageAttribute> GetTablesAtTopLevelView()
        {
            System.Collections.IList visibleTables = Global.DefaultModel.VisibleTables;

            //The formate is: <Table, <order, path>>
            List<AddIconToHomePageAttribute> visibleIcons = new List<AddIconToHomePageAttribute>();
            foreach (MetaTable table in visibleTables)
            {
                foreach (AddIconToHomePageAttribute att in table.Attributes.OfType<AddIconToHomePageAttribute>())
                {
                    if (att != null)
                    {
                        att.fixAccordingToTable(table);
                        visibleIcons.Add(att);
                    }
                }

            }
            return visibleIcons.OrderBy(t => t.Order);
        }
#endregion
        #region URL
        /// <summary>
        /// Fix for List Details and Edit Links to add the remaining feild to the links (no DD support)
        /// </summary>
        /// <param name="h"> the dynamic Hyperlink to modify</param>
        /// <param name="propertyid">the current property id to filter agenst</param>
        /// <param name="action"> the current page action (Details)</param>
        public static void RewriteRouteUrl(DynamicHyperLink h, string propertyid, string action)
        {

            if (!string.IsNullOrEmpty(propertyid) && action == PageAction.Details)
            {
                //feach an entity
                Entities entities = new Entities();
                List<Property> houses = entities.Properties.Where(w => w.PropertyID.ToString() != "").ToList();
                foreach (var item in houses)
                {
                    //loop thrugh houses by id and match
                    if (item.PropertyID.ToString() == propertyid)
                    {
                        //add the messing feilds
                        // h.NavigateUrl += string.Format("/{Country}/{City}/{Property_Category}/{Property_Type}/{Address}", item.Country.Country_Name, item.City.City_Name, item.Property_Category.Cat_Name, item.Property_Type.Property_Type_Name, item.Address);
                        h.NavigateUrl += "/" + item.Country.Country_Name + "/";
                        h.NavigateUrl += item.City.City_Name + "/";
                        h.NavigateUrl += item.Property_Category.Cat_Name + "/";
                        h.NavigateUrl += item.Property_Type.Property_Type_Name + "/";
                        h.NavigateUrl += item.Address + "/";
                    }
                }
            }
        }
        /// <summary>
        /// Fix for List Details and Edit Links to add the remaining feild to the links (no DD support)
        /// </summary>
        /// <param name="h"> the string url to modify</param>
        /// <param name="propertyid">the current property id to filter agenst</param>
        /// <param name="action"> the current page action (Details)</param>
        public static string RewriteRouteUrl(string h, string propertyid, string action)
        {

            if (!string.IsNullOrEmpty(propertyid) && action == PageAction.Details)
            {
                //feach an entity
                Entities entities = new Entities();
                List<Property> houses = entities.Properties.Where(w => w.PropertyID.ToString() != "").ToList();
                foreach (var item in houses)
                {
                    //loop thrugh houses by id and match
                    if (item.PropertyID.ToString() == propertyid)
                    {
                        //add the messing feilds
                        // h.NavigateUrl += string.Format("/{Country}/{City}/{Property_Category}/{Property_Type}/{Address}", item.Country.Country_Name, item.City.City_Name, item.Property_Category.Cat_Name, item.Property_Type.Property_Type_Name, item.Address);
                        h += "/" + item.Country.Country_Name + "/";
                        h += item.City.City_Name + "/";
                        h += item.Property_Category.Cat_Name + "/";
                        h += item.Property_Type.Property_Type_Name + "/";
                        h += item.Address + "/";
                    }
                }
               
            }
            return h;
        }
        /// <summary>
        /// Fix for Arabic URL to change url parts according to the lang
        /// </summary>
        /// <param name="table">the table name (Property)</param>
        /// <param name="action">the action (List)</param>
        /// <param name="param">set of parameters to add(User id)</param>
        /// <returns>fixed url according to Global Routes</returns>
        public static string RewriteRouteUrl(string table, string action, List<string> param)
        {
            Resources.Route.Culture = CultureInfo.CurrentCulture;
            string url = HttpContext.GetGlobalResourceObject("Route", table) + "/" + HttpContext.GetGlobalResourceObject("Route", action);
            foreach (var item in param)
            {
                url += "/" + item;
            }
            return url;
        }
        /// <summary>
        /// Convert non querystring and arabic non query string to valid search list parameters 
        /// </summary>
        /// <param name="q"> the query string of the request call</param>
        /// <param name="url"> the current url to be modified</param>
        /// <returns> new url for redirect</returns>
        public static string RewriteRouteUrl(NameValueCollection q, Uri url)
        {
            //new entity to search values
            Entities entities = new Entities();
            string res = "";
            foreach (var item in url.Segments)
            {
                //check for valid parts
                if (item != "/")
                {
                    //convert the part to the database value of category
                    var itm = TransolateRouteURl(Resources.Property_Category.ResourceManager, item,false);
                    //lookup the category by name
                    List<Property_Category> Cat = entities.Property_Category.Where(w => w.Cat_Name.ToString().Contains(itm)).ToList();
                    if (Cat.Count > 0)
                    {
                        //get the id
                        int c = Cat[0].Cat_ID;
                        //lookup the type by cat id
                        res += "?Property_Category=" + c;
                        List <Property_Type> Typ = entities.Property_Type.Where(w => w.Cat_ID == c).ToList();
                        if (Typ.Count > 0)
                        {
                            foreach (var item2 in url.Segments)
                            {
                                if (item2 != "/")
                                {
                                    //reloop through the url and find the item from the property type according to the cat
                                    itm = TransolateRouteURl(Resources.Property_Type.ResourceManager, item2, false);
                                    foreach (var item3 in Typ)
                                    {
                                        //decode and convert to db name and match type name
                                        if (itm == item3.Property_Type_Name)
                                        {
                                            //generate querry string
                                            res +=  "&Property_Type=" + item3.Property_Type_ID.ToString();
                                            //return new parameters
                                            return res;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return res;
        }
        public static string RewriteRouteUrl(string url)
        {
            //new entity to search values
            Entities entities = new Entities();
            var uri = new Uri("http://" + HttpContext.Current.Request.Url.Authority + url);
            string res = "/";
            foreach (var item in uri.Segments)
            {
                //check for valid parts
                if (item != "/")
                {
                    //convert the part to the database value of category
                    var itm = TransolateRouteURl(Resources.Property_Category.ResourceManager, item,true);
                    res += itm;
                    //lookup the category by name

                    List<Property_Category> Cat = entities.Property_Category.Where(w => w.Cat_Name.ToString().Contains(itm)).ToList();
                    if (Cat.Count > 0)
                    {
                        //get the id
                        int c = Cat[0].Cat_ID;
                        //lookup the type by cat id
                        List<Property_Type> Typ = entities.Property_Type.Where(w => w.Cat_ID == c).ToList();
                        if (Typ.Count > 0)
                        {
                            foreach (var item2 in uri.Segments)
                            {
                                if (item2 != "/")
                                {
                                    //reloop through the url and find the item from the property type according to the cat
                                    itm = TransolateRouteURl(Resources.Property_Type.ResourceManager, item2, true);
                                    foreach (var item3 in Typ)
                                    {
                                        //decode and convert to db name and match type name
                                        if (itm == item3.Property_Type_Name)
                                        {
                                            //generate querry string
                                            string s = "?Property_Category=" + c + "&Property_Type=" + item3.Property_Type_ID.ToString();
                                            //return new parameters
                                            return s;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return res;
        }
        /// <summary>
        /// Finds DB Name of Resource Value 
        /// </summary>
        /// <param name="r">the set to check</param>
        /// <param name="item">the value</param>
        /// <returns>DB Name To Lookup</returns>
        public static string TransolateRouteURl(ResourceManager r, string item,bool local)
        {
            item = WebUtility.UrlDecode(item);
            var resourceSet = r.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry entry in resourceSet)
            {
                string resourceKey = entry.Key.ToString();
                string resource = entry.Value.ToString();
                if (resource == item.Replace("/", ""))
                {
                    if (local)
                        return resource;
                    return resourceKey;

                }
            }
            return item.Replace("/", "");
        }
        /// <summary>
        /// Fix For HyperLink to Fix Url Parameters And Routes
        /// </summary>
        /// <param name="h">the hyperlink</param>
        /// <param name="propertyid">returns the extracted id</param>
        /// <param name="action">returns the current action</param>
        public static void TransolateRouteURl(DynamicHyperLink h, out string propertyid, out string action)
        {
            string act = "";
            var url = new Uri("http://" + HttpContext.Current.Request.Url.Authority + h.NavigateUrl).Segments;
            ResourceSet resourceSet = Resources.Route.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (var item in url)
            {

                foreach (DictionaryEntry entry in resourceSet)
                {
                    string resourceKey = entry.Key.ToString();
                    string resource = entry.Value.ToString();
                    if (item.Contains(resourceKey) || item.Contains(resource))
                    {
                        if (resource.Length > 0)
                        {
                            h.NavigateUrl = h.NavigateUrl.Replace(item.Replace("/", ""), resource);
                            act = resourceKey;
                        }
                    }
                }

            }
            int id = 0;
            if (url.Length > 2 && int.TryParse(url[2], out id))
            {
                propertyid = id.ToString();
            }
            else
            {
                propertyid = "";
            }
            action = act;
            return;
        }
        /// <summary>
        /// Convert /Page?p1=data&p2=data To /Page/data/data and fix for Lang
        /// </summary>
        /// <param name="h">the source URL</param>
        /// <returns>Fixed URL</returns>
        public static string TransolateRouteURl(string h)
        {
            //convert url to uri to work with and save params
            var url = new Uri("http://" + HttpContext.Current.Request.Url.Authority + h);
            var query = HttpUtility.ParseQueryString(url.Query);
            h = url.GetLeftPart(UriPartial.Path).Replace(url.GetLeftPart(UriPartial.Authority), "");
            //loop through res file to replace text with relevant local
            ResourceSet resourceSet = Resources.Route.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry entry in resourceSet)
            {
                string resourceKey = entry.Key.ToString();
                string resource = entry.Value.ToString();


                for (int i = 0; i < url.Segments.Length; i++)
                {
                    var item = url.Segments[i];
                    if (item.Contains(resourceKey) && !item.Contains(resource))
                    {
                        /// also remove the after / cuz we'll add it in a sec
                        h = h.Replace(item.Replace("/", ""), resource);
                    }
                }
            }
            //readd the new styled params if any
            if (query.Count > 0)
            {
                foreach (var item in query)
                {
                    h += "/" + query.Get(item.ToString());
                }
            }
            return h;
        }
        #endregion
        /// <summary>
        /// use Trigger function in ASP Args to get ref to the called control
        /// </summary>
        /// <param name="page">called page</param>
        /// <param name="root">root directory</param>
        /// <returns>the call control</returns>
        public static Control GetPostbackControl(Page page, Control root)
        {
            Control c = null;
            string name = page.Request.Params.Get("__EVENTTARGET");
            if (!string.IsNullOrEmpty(name))
            {
                name = name.Substring(name.LastIndexOf("$") + 1);
                c = root != null ? root.FindControlRecursive<Control>(name) : page.FindControlRecursive<Control>(name);
            }
            return c;
        }
        ///default culture defined in app setting
        public static string DefaultCulture
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultCulture"];
            }
        }
    }
}