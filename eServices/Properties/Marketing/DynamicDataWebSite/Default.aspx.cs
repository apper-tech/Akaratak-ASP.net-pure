using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string title = GetGlobalResourceObject("RealEstate", "Welcome") as string;
            Title = title != "" ? title : "Akaratak";
            Title = title;
            MetaDescription = Page.Title;
        }
      
    }
}