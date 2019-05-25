using DynamicDataLibrary;
using DynamicDataLibrary.Attributes;
using DynamicDataWebSite.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite.ServerControls
{
    public partial class Icons : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<IconData> paths = IconData.GetList(this.Page.User);

                //you can add custom additional icons as follow. If you need such you can also store them in DB...
                ////paths.Add(new IconData() { Path = "/about", Title = Resources.RealEstate.About + Resources.RealEstate.Company, Desc = "", IconUrl = "\\CustomDesign\\images\\at.jpg" });

                //int rowsNumber = (int)Math.Ceiling(paths.Count * 3.0 / 12.0);//bootstrap cells are 12 in width. Min for each icon is 2 cells.
                //int horizontalCellsForEachIcon = rowsNumber * 12 / paths.Count; //The best fit will be when 12 % paths.Count = 0 (باقي القسمة 0)

                ClassString = "box_2 col-md-" +( paths.Count>3?"3":"4");

                for (int i = 0; i < paths.Count; i++)
                {
                    paths[i].Path = DynamicDataHelper.TransolateRouteURl(paths[i].Path);
                }
                this.Repeater1.DataSource = paths;
                this.Repeater1.DataBind();
            }
        }
        public string ClassString { get; set; }
    }
}