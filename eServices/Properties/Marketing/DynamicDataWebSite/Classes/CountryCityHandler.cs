using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite
{
    public static class CountryCityHandler
    {
        public static void PubulateCountry(DropDownList dr)
        {
            List<DynamicDataModel.Model.Country> ls = DynamicDataHelper.GetCountryList();
            ls = ls.OrderBy(o => o.Country_Name).ToList();
            foreach (ListItem item in dr.Items)
            {
                if (item.Value != "" && item.Value!=Resources.DynamicData.All && item.Value!=Resources.DynamicData.Null)
                {
                    DynamicDataModel.Model.Country c = ls.Find(x => x.Country_ID == int.Parse(item.Value));
                    if (c != null)
                    {
                        if (c.Country_Name == c.Country_Native_Name)
                            item.Text = c.Country_Name;
                        else
                            item.Text = c.Country_Name + "  (" + c.Country_Native_Name + ")";
                    }

                }
            }
        }
        public static void PubulateEmptyCity(DropDownList dr)
        {
            dr.Items.Clear();
            dr.Items.Add(new ListItem(Resources.RealEstate.SelectFirst, ""));
        }
        public static void PubulateCountryCities(string code,DropDownList dr)
        {
            DropDownList child = dr;
            List<DynamicDataModel.Model.City> ls = DynamicDataHelper.GetCityList(code);
            ls = ls.OrderBy(o => o.City_Name).ToList();
            foreach (ListItem item in child.Items)
            {
                if (item.Value != "" && item.Value != Resources.DynamicData.Null)
                {
                    DynamicDataModel.Model.City c = FindCity(ls, int.Parse(item.Value));
                    if (c != null)
                    {
                        if (c.City_Name == c.City_Native_Name)
                            item.Text = c.City_Name;
                        else
                            item.Text = c.City_Name + "  (" + c.City_Native_Name + ")";
                        item.Attributes.Add("tooltip", c.Latitude + "|" + c.Longitude);
                    }

                }
            }
        }
        private static DynamicDataModel.Model.City FindCity(List<DynamicDataModel.Model.City> ls, int id)
        {
            foreach (DynamicDataModel.Model.City item in ls)
            {
                if (item.City_ID == id)
                {
                    return item;
                }
            }
            return null;
        }
    }
}