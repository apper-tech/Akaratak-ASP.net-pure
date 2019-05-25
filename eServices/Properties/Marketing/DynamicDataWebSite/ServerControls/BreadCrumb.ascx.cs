using DynamicDataWebSite.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite.ServerControls
{
    public partial class BreadCrumb : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rep.DataSource = DynamicDataHelper.UrlToBbread(new Uri("http://" + HttpContext.Current.Request.Url.Authority + Request.RawUrl));
            CheckMobile();
            rep.DataBind();
        }
        private void CheckMobile()
        {
            var repds = (rep.DataSource as List<BreadItem>).Count;
            foreach (var item in Request.Url.Segments)
            {
                    string query = HttpUtility.UrlDecode(item.Replace("/", ""));
                    if (query == Resources.Route.Details)
                    {
                        var l = rep.DataSource as List<BreadItem>;
                        l.RemoveRange(l.Count - 4, 3);
                    }
                
            }
        }
        private int itemCount;

        public int ItemCount { get => itemCount; set => itemCount = value; }
        private string style;
        public string Style { get => style; set => style = value; }

        public bool isActive()
        {
            var repds = (rep.DataSource as List<BreadItem>).Count;
            if (itemCount == repds - 1 && itemCount!=0)
            {
               
                return true;
            }
            else
            {
                itemCount++;

                return false;
            }
        }
    }
}