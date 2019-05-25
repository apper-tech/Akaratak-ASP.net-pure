using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite.DynamicData.FieldTemplates
{
    public partial class MapLocation_Edit : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
           // if (FieldValueString != "NaN")
                //{
                //    int two = FieldValueString.LastIndexOf(",");
                //    int one = FieldValueString.IndexOf(",");
                //   Lat = FieldValueString.Substring(0, one);
                //    Lng = FieldValueString.Substring(one + 1, two - one - 1);
                //    Zoom = FieldValueString.Substring(two + 1);
                //    House = GetGlobalResourceObject("Map", "house").ToString();
                //}



                if (FieldValueString != "")
                {
                    if (FieldValueString == "NaN")
                    {
                      //  MapHolder.Visible = false;
                      //  DefaultHolder.Visible = true;
                      //  DefaultText.Text = Resources.RealEstate.Map_Not_Found;
                    }
                    else
                    {
                    int two = FieldValueString.LastIndexOf(",");
                    int one = FieldValueString.IndexOf(",");
                    Lat = FieldValueString.Substring(0, one);
                    Lng = FieldValueString.Substring(one + 1, two - one - 1);
                    Zoom = FieldValueString.Substring(two + 1);
                    House = GetGlobalResourceObject("Map", "house").ToString();
                }
            }
        }
        string lat;

        public string Lat
        {
            get { return lat; }
            set { lat = value; }
        }
        string house;

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
        public string GetCoords(string tp)
        {
            if(string.IsNullOrEmpty(lat))
            Lat = "33.5131";
            if (string.IsNullOrEmpty(lng))
                lng = "36.2919";
            if(string.IsNullOrEmpty(zoom))
            Zoom = "5";
            locargs.Value = Lat + "," + Lng + "," + Zoom;
            switch (tp)
            {
                case "lat":
                    return lat;
                case "lng":
                    return lng;
                case "zoom":
                    return zoom;
            }
            return "nan";
        }
        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            if (locargs.Value.Contains("("))
                locargs.Value = locargs.Value.Remove(locargs.Value.IndexOf("("), 1);
            if (locargs.Value.Contains(")"))
                locargs.Value = locargs.Value.Remove(locargs.Value.IndexOf(")"), 1);
            dictionary[Column.Name] = ConvertEditedValue(locargs.Value);
        }

        public override Control DataControl
        {
            get
            {
                return locargs;
            }
        }

        public string House
        {
            get
            {
                return house;
            }

            set
            {
                house = value;
            }
        }
    }
}