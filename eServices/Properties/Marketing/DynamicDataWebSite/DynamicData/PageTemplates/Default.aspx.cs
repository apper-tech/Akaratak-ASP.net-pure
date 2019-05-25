using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;
using System.Linq;
using DynamicDataLibrary;
using System.Linq.Dynamic;
using DynamicDataLibrary.ModelRelated;
using DynamicDataLibrary.Attributes;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Security.Principal;
using NotAClue.Web.DynamicData;
using System.Collections.Specialized;
using DynamicDataModel.Model;

namespace DynamicDataWebSite.DynamicData
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //Possible to use a custom attribute to detect the defult page for the table...
            //MetaTable table = DynamicDataRouteHandler.GetRequestMetaTable(Context);
            //if(hasCustomDefaltPage)
            //Page.Response.Redirect(customDefaltPage);
            //else

            Page.Response.Redirect("~/");
        }
    }
}
