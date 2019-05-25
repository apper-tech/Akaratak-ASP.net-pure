using DynamicDataModel.Model;
using DynamicDataLibrary.ModelRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Remoting.Contexts;

namespace DynamicDataModel
{
    public class SessionData
    {
        public static string CurrentUserName
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["CurrentUserName"] != null)
                    return HttpContext.Current.Session["CurrentUserName"] as string;
                else
                    return null;
            }
            //set
            //{
            //    Session["CurrentUserName"] = value;
            //}
        }
        public static string CurrentDisplayName
        {
            get
            {
                return Convert.ToString(HttpContext.Current.Session["CurrentDisplayName"]);
            }
            //set
            //{
            //    Session["CurrentUserName"] = value;
            //}
        }

        public static bool FillDataSession(Entities entities)
        {
            #region Fill Session Variables
            string userId = AuditingHelperUtility.GetUserId();
            HttpContext.Current.Session["CurrentUserName"] = userId;
            //Employee currentEmp = entities.Employees.Where(
            //        emp => emp.UserName.ToLower() == userId.ToLower()).FirstOrDefault();
            //var eee = entities.Employees.ToArray();
            //if (currentEmp != null)
            //{
            //    HttpContext.Current.Session["CurrentDisplayName"] = currentEmp.GetRankAndName();
            //    HttpContext.Current.Session["CurrentEmployeeMobile"] = currentEmp.Mobile;
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return true;
            #endregion
        }
    }
}