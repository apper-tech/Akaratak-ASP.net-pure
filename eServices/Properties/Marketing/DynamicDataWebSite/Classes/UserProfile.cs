using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Data.SqlClient;
using System.Configuration;
using DynamicDataModel.Model;

namespace DynamicDataWebSite
{
    public class UserProfile : ProfileBase
    {
        [SettingsAllowAnonymous(false)]
        public String FirstName
        {
            get
            {
                return base["FirstName"] as String;
            }
            set
            {
                base["FirstName"] = value;
            }
        }

        [SettingsAllowAnonymous(false)]
        public String MiddleName
        {
            get
            {
                return base["MiddleName"] as String;
            }
            set
            {
                base["MiddleName"] = value;
            }
        }

        [SettingsAllowAnonymous(false)]
        public String LastName
        {
            get
            {
                return base["LastName"] as String;
            }
            set
            {
                base["LastName"] = value;
            }
        }

        [SettingsAllowAnonymous(false)]
        public String Phone
        {
            get
            {
                return base["Phone"] as String;
            }
            set
            {
                base["Phone"] = value;
            }
        }

        [SettingsAllowAnonymous(false)]
        public String Mobile
        {
            get
            {
                return base["Mobile"] as String;
            }
            set
            {
                base["Mobile"] = value;
            }
        }

        [SettingsAllowAnonymous(false)]
        public String Address
        {
            get
            {
                return base["Address"] as String;
            }
            set
            {
                base["Address"] = value;
            }
        }

        public String ImageUrl
        {
            get
            {
                if (File.Exists(HttpContext.Current.Server.MapPath(String.Format("~/UsersImage/{0}.png", UserName))))
                    return String.Format("~/UsersImage/{0}.png", UserName);

                return "~/App_Themes/Default/Images/AnonymousUser.png";
            }
        }
        public static string GetFullName(string id)
        {
            Entities entities = new Entities();
            // the source list
            List<User> dr = entities.Users.Where(w => w.User_ID.ToString() ==id).ToList();
            string res = "";
            try
            {
                        
                    res = dr[0].First_Name + "  " + dr[0].Last_Name;
                    if (string.IsNullOrEmpty(res) || res== "  ")
                    {
                        res = "NULL_ERR";
                    }
            }
            catch (Exception e)
            { }
            return res == "" ? res == "NULL_ERR" ? res : "NO_ERR" : res;
        }
        public static bool UserExist(string id)
        {
            try
            {
                Entities entities = new Entities();
                // the source list
                List<User> dr = entities.Users.Where(w => w.User_ID.ToString() == id).ToList();
                if(dr.Count>0 && dr[0].User_ID==id)
                {
                    return true;
                }
            }
            catch (Exception e) { }
            return false;
        }
        public static User GetUserData(string id)
        {
            Entities entities = new Entities();
            // the source list
            List<User> dr = entities.Users.Where(w => w.User_ID.ToString() == id).ToList();
            try
            {
                if (dr.Count > 0)
                {
                    return dr[0];
                }
            }
            catch (Exception e) { }
            return null;
        }
    }
}
