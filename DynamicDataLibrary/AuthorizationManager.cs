using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace DynamicDataLibrary
{
    public class AuthorizationManager : Attribute
    {
        //private static Dictionary<string, Dictionary<string, bool>> urlAccessForPrincipals
        //    = new Dictionary<string, Dictionary<string, bool>>();

        public static bool CheckUrlAccessForPrincipal(string urlString, IPrincipal user)
        {
            string urlPath = urlString.Split('?')[0];
            string userName = user != null && user.Identity != null ? user.Identity.Name : String.Empty;
            //if (!urlAccessForPrincipals.ContainsKey(urlPath))
            //    urlAccessForPrincipals.Add(urlPath, new Dictionary<string, bool>());
            //if (!urlAccessForPrincipals[urlPath].ContainsKey(userName))
            //{
            try
            {
               return UrlAuthorizationModule.CheckUrlAccessForPrincipal(urlPath, user, "GET");
            }
            catch { return false; }
            //urlAccessForPrincipals[urlPath].Add(userName, hasAccess);
            //}
            //return urlAccessForPrincipals[urlPath][userName];
        }
    }
}
