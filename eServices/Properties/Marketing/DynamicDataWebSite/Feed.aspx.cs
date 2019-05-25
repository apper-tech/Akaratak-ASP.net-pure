using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite
{
    public partial class Feed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string title = Resources.RealEstate.Feed_Back;
            Title = title != "" ? title : "Join Us";
            if(System.Threading.Thread.CurrentThread.CurrentCulture.Name.Contains("ar"))
            {
                scrollhere.Attributes.Add("dir", "rtl");
            }
        }
    }
}