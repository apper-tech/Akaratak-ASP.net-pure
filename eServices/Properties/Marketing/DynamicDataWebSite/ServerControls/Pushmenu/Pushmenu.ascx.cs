using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite.ServerControls
{
    public partial class SitemapPushmenu : System.Web.UI.UserControl
    {
        private SiteMapDataSource dataSource;

        public SiteMapDataSource DataSource
        {
            get
            {
                return dataSource;
            }

            set
            {
                dataSource = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            rep.DataSource = DataSource;
            rep.DataBind();
        }
        public SiteMapNodeCollection GetDataSource(SiteMapNode root, Repeater rep)
        {
            if (root.ChildNodes.Count > 0)
                return root.ChildNodes;
            else
            {
                rep.Visible = false;
                return null;
            }
        }
        public string CheckMap(string val)
        {
            if(val.Contains("New"))
            {
                val.Replace("New", "Insert");
            }
            if(val.Contains('#'))
            {
                return "#";
            }
            if(val.Contains("User_ID"))
            {
                val +="/"+ HttpContext.Current.User.Identity.GetUserId();
            }
            val = val.Replace(',', '&');
            return val;
        }
    }
}