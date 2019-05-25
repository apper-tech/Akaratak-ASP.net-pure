using DynamicDataLibrary;
using DynamicDataLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Linq;
using System.Web.DynamicData;
using Microsoft.AspNet.Identity;

namespace DynamicDataWebSite.Classes
{
    public class IconData
    {
        public string Title { get; set; }
        public string Path { get; set; }
        public string IconUrl { get; set; }
        public string BackgroundColor { get; set; }
        public string Desc { get; set; }

        private static List<string> colors = new List<string>();

        public static List<string> Colors
        {
            get
            {
                if (colors.Count == 0)
                {
                    colors.Add("#c37400");
                    colors.Add("#768935");
                    colors.Add("#b4a943");
                    colors.Add("#0f3f1f");
                    colors.Add("#897635");
                    colors.Add("#357689");
                    colors.Add("#0f1f3f");
                }
                return colors;
            }
            set { colors = value; }
        }


        public static bool Add(List<IconData> iconsData, AddIconToHomePageAttribute iconAtt, IPrincipal user)
        {
            if (iconAtt.ShowEvenIfUrlNotAccessable == true || AuthorizationManager.CheckUrlAccessForPrincipal(iconAtt.PageActionOrPageUrl, user))
            {
                iconsData.Add(new IconData()
                {
                    Path = URlParameter.GetUrlData(iconAtt.PageActionOrPageUrl, iconAtt.UrlParameters),
                    Title = HttpContext.GetGlobalResourceObject(iconAtt.Locale_Resource_Name, iconAtt.Title) as string,
                    IconUrl = iconAtt.IconUrl,
                    Desc = HttpContext.GetGlobalResourceObject(iconAtt.Locale_Resource_Name, iconAtt.Description) as string,
                });
                return true;
            }
            return false;
        }


        public static List<IconData> GetList(IPrincipal user)
        {
            List<IconData> paths = new List<IconData>();
            int counter = 0;
            foreach (AddIconToHomePageAttribute iconAtt in DynamicDataHelper.GetTablesAtTopLevelView())
            {
                if (IconData.Add(paths, iconAtt, user))
                    paths[counter++].BackgroundColor = IconData.Colors[counter % IconData.Colors.Count];
            }
            return paths;
        }
    }
    public class URlParameter
    {
        string url;
        string param;
        public static string GetUrlData(string url, string param)
        {
            switch (param)
            {
                case "User_ID":
                    return url + "?User_ID=" + HttpContext.Current.User.Identity.GetUserId();
            }
            return url;
        }

    }
    public class BreadItem
    {
        string url;

        public string Url { get => url; set => url = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public bool Active { get => active; set => active = value; }

        string title;
        string description;
        bool active;
    }
}