using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicDataWebSite
{
    public static class ApplicationSettings
    {
        public static bool DisplayForeignKeyAsLink
        {
            get
            {
                //if(System.Configuration.ConfigurationManager.AppSettings["DisplayForeignKeyAsLink"])
                return Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["DisplayForeignKeyAsLink"]);
            }
        }
    }
}