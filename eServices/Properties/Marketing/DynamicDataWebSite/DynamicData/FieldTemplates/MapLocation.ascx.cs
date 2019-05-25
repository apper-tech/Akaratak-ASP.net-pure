using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite.DynamicData.FieldTemplates
{
    public partial class MapLocation : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            if (FieldValueString != "")
            {
                if (FieldValueString == "NaN")
                {
                    MapHolder.Visible = false;
                    DefaultHolder.Visible = true;
                    DefaultText.Text = Resources.RealEstate.Map_Not_Found;
                }
                else
                {
                    try
                    {
                        int two = FieldValueString.LastIndexOf(",");
                        int one = FieldValueString.IndexOf(",");
                        Lat = FieldValueString.Substring(0, one);
                        Lng = FieldValueString.Substring(one + 1, two - one - 1);
                        Zoom = FieldValueString.Substring(two + 1);
                        if (int.Parse(Zoom) < 10)
                            Zoom = (int.Parse(zoom) + 10).ToString();
                    }
                    catch { }
                }
            }
        }
        string lat;

        public string Lat
        {
            get { return lat; }
            set { lat = value; }
        }
  

        string lng;

        public string Lng
        {
            get { return lng; }
            set { lng = value; }
        }

        string zoom;

        public string Zoom
        {
            get { return zoom; }
            set { zoom = value; }
        }

       
    }
}