using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite.DynamicData.FieldTemplates
{
    public partial class MapLocation_Insert : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //sets the Lng Lat if the Geo loc is not avalable
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
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
         
        }
        public string GetCoords(string tp)
        {
            Lat = "33.5131";
            lng = "36.2919";
            Zoom = "5";
            locargs.Value = Lat + "," + Lng + "," + Zoom;
            switch (tp)
            {
                case "lat":
                    return "33.5131";
                case "lng":
                    return "36.2919";
                case "zoom":
                    return "5";
            }
            return "nan";
        }
        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            if (locargs.Value.Contains("("))
                locargs.Value = locargs.Value.Remove(locargs.Value.IndexOf("("), 1);
            if (locargs.Value.Contains(")"))
                locargs.Value = locargs.Value.Remove(locargs.Value.IndexOf(")"), 1);
            dictionary[Column.Name] = ConvertEditedValue(string.IsNullOrEmpty(locargs.Value)?"NaN":locargs.Value);
        }

        public override Control DataControl
        {
            get
            {
                return locargs;
            }
        }
        public void setArgs()
        {
            string lnglatzoom = locargs.Value;
            int two = lnglatzoom.LastIndexOf(",");
            int one = lnglatzoom.IndexOf(",");
            Lat = lnglatzoom.Substring(0, one);
            Lng = lnglatzoom.Substring(one + 1, two - one - 1);
            Zoom = lnglatzoom.Substring(two + 1);
        }
    }
}