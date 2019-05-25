using System;
using System.Linq;
using System.Web.DynamicData;

namespace DynamicDataLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AddIconToHomePageAttribute : Attribute
    {
        /// <summary>
        /// To allow multible attributes
        /// </summary>
        public override object TypeId { get { return this; } }


        public int? Order { set; get; }
        public String Title { set; get; }

        /// <summary>
        /// Contains either a PageAction to get the path of, or a path. Check the examples bellow.
        /// </summary>
        /// <example>
        ///     List (--> to get examples check: System.Web.DynamicData.PageAction)
        ///     Insert (--> to get examples check: System.Web.DynamicData.PageAction)
        ///     /Dynamic/Properties/Insert
        /// </example>
        public String PageActionOrPageUrl { set; get; }

        public String IconUrl { set; get; }
        public string Description { set; get; }
        public string UrlParameters { set; get; }
        public string Locale_Resource_Name { get; set; }
        public bool ShowEvenIfUrlNotAccessable { get; set; }
        public bool CheckContribution { get; set; }


        private string[] ParameterList = new string[] { "UserID","","" };
        public AddIconToHomePageAttribute()
        {
        }

        public AddIconToHomePageAttribute(int order)
        {
            this.Order = order;
        }
        public AddIconToHomePageAttribute(int order, String title)
        {
            this.Order = order;
            this.Title = title;
        }
        public AddIconToHomePageAttribute(int order, String title, String pageActionOrPageUrl)
        {
            this.Order = order;
            this.Title = title;
            this.PageActionOrPageUrl = pageActionOrPageUrl;
        }

        public AddIconToHomePageAttribute(int order, String title, String pageActionOrPageUrl, String iconUrl,string desc)
        {
            this.Order = order;
            this.Title = title;
            this.PageActionOrPageUrl = pageActionOrPageUrl;
            this.IconUrl = iconUrl;
            this.Description = desc;
        }
        public AddIconToHomePageAttribute(int order, String title, String pageActionOrPageUrl, String iconUrl, string desc,string param,bool check)
        {
            this.Order = order;
            this.Title = title;
            this.PageActionOrPageUrl = pageActionOrPageUrl;
            this.IconUrl = iconUrl;
            this.Description = desc;
            this.UrlParameters = param;
            this.CheckContribution = check;
        }
        public void fixAccordingToTable(MetaTable table)
        {
            if (this.Order.HasValue == false)
                this.Order = int.MaxValue;
            if (String.IsNullOrEmpty(this.Title))
                this.Title = table.DisplayName;

            if (String.IsNullOrEmpty(this.PageActionOrPageUrl))
                this.PageActionOrPageUrl = table.GetActionPath(PageAction.List);
            else
            {
                try
                {
                    if (!this.PageActionOrPageUrl.Contains('/') && !this.PageActionOrPageUrl.Contains('\\'))
                        this.PageActionOrPageUrl = table.GetActionPath(this.PageActionOrPageUrl);
                   
                }
                catch
                {
                    //Do nothing to keep the 'PageActionOrPageUrl' as-is.
                }
            }
        }
    }
   
}
