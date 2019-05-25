using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace DynamicDataWebSite
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            ////disable the mobile master
            //var settings = new FriendlyUrlSettings();
            //settings.AutoRedirectMode = RedirectMode.Permanent;
            //routes.EnableFriendlyUrls(settings);
            routes.EnableFriendlyUrls();
        }
    }
}
