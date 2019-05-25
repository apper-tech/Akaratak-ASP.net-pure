using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite.ServerControls
{
    public partial class PushMenu_Item : System.Web.UI.UserControl
    {
        public string Title { set; get; }
        public string Desc { set; get; }
        public string Url { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Url!=null && Url!="/" && ! Url.Contains("#"))
            {
                string tu = Url;
                Uri u = new Uri("http://"+Request.Url.Authority+ tu);
                string ur = "";
                foreach (var item in u.Segments)
                {
                    if (item != "/")
                    {
                        try
                        {
                            if(item.Contains("New"))
                            {
                                ur += GetGlobalResourceObject("Route", "Insert").ToString();
                            }
                            if (GetGlobalResourceObject("Route", item.Replace("/", ""))!=null)
                            {
                                ur += GetGlobalResourceObject("Route", item.Replace("/", "")).ToString();
                            }
                            if (GetGlobalResourceObject("Account", item.Replace("/", "")) != null)
                            {
                                ur += GetGlobalResourceObject("Account", item.Replace("/", "")).ToString();
                            }
                            if (GetGlobalResourceObject("Property_Category", item.Replace("/", "")) != null)
                            {
                                ur += GetGlobalResourceObject("Property_Category", item.Replace("/", "")).ToString();
                            }
                           else if (GetGlobalResourceObject("Property_Type", item.Replace("/", "")) != null)
                            {
                                ur += GetGlobalResourceObject("Property_Type", item.Replace("/", "")).ToString();
                            }
                            if (!ur.EndsWith("/"))
                                ur += "/";
                            if (item.Contains("Added") || item.Contains("Edit"))
                                ur += Page.User.Identity.GetUserId();

                        }
                        catch
                        {
                            if (!ur.EndsWith("/"))
                                ur += "/";
                            continue;
                        }
                    }
                    else
                        ur += "/";
                }
                if (ur != "")
                    Url = ur;
            }
            item.NavigateUrl = Url;
        }
    }
}