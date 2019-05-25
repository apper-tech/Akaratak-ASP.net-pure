using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite
{
    public partial class Carrer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string title = GetGlobalResourceObject("DynamicData", "Join_Us") as string;
            Title = title != "" ? title : "Join Us";
            if(System.Threading.Thread.CurrentThread.CurrentCulture.Name.Contains("ar"))
            {
                eng.Visible = false;
                arb.Visible = true;
            }
            else
            {
                eng.Visible = true;
                arb.Visible = false;
            }
        }
    }
}