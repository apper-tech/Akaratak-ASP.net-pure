using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite
{
    public partial class ForeignKeyFilter : System.Web.DynamicData.QueryableFilterUserControl
    {
        private string NullValueString { get { return Convert.ToString(GetGlobalResourceObject("DynamicData", "Null")); } }
        private new MetaForeignKeyColumn Column
        {
            get
            {
                return (MetaForeignKeyColumn)base.Column;
            }
        }

        public override Control FilterControl
        {
            get
            {
                return DropDownList1;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DropDownList1.Items.Add(new ListItem(Resources.DynamicData.All,""));
                if (!Column.IsRequired)
                {
                    DropDownList1.Items.Add(new ListItem(Convert.ToString(GetGlobalResourceObject("DynamicData", "NotSet")), NullValueString));
                }
                PopulateListControl(DropDownList1);
                // Set the initial value if there is one
                string initialValue = DefaultValue;
                if (!String.IsNullOrEmpty(initialValue))
                {
                    DropDownList1.SelectedValue = initialValue;
                    //DropDownList1.Enabled = false;
                }
                //set the value from query string if any
                if (Request.QueryString.Keys.Count > 0)
                {
                    for (int i = 0; i < Request.QueryString.Keys.Count; i++)
                    {
                        string value = Request.QueryString.Keys.Get(i);
                        if (value == Column.Name)
                        {
                            DropDownList1.SelectedValue = Request.QueryString.Get(value);
                        }
                    }
                }
                LocalizeList();
                //end of loc
            }
        }
        private void LocalizeList()
        {
            try
            {
                //localize need to be moved to class
                var loc = Column.Attributes.OfType<DynamicDataLibrary.Attributes.LocalizedForeignKeyAttribute>().FirstOrDefault();
                if (loc != null && loc.DataLoadAccess)
                {
                    if (loc.TableName == "COUNTRIES")
                        CountryCityHandler.PubulateCountry(DropDownList1);
                    if (loc.TableName == "CITIES")
                        CountryCityHandler.PubulateEmptyCity(DropDownList1);
                }
                else
                    foreach (ListItem item in DropDownList1.Items)
                    {
                        if (loc != null && loc.ResourceFileName != null)
                        {
                            //loc
                            if (item.Text != Resources.DynamicData.All)
                            {
                                var t = GetGlobalResourceObject(loc.ResourceFileName, item.Text);
                                if (t != null && t.ToString() != "") item.Text = t.ToString();
                            }
                        }
                        if (Column.Name == "User")
                        {

                        }
                    }
                if (DropDownList1.Items.Count <= 2)
                {
                    DropDownList1.Enabled = false;
                    DropDownList1.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                string err = "error";
                if (ex.GetType() == typeof(NullReferenceException))
                    err = "err";
                Response.Redirect(SecurityHandler.ErrorPath + err);
            }
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnFilterChanged();
        }

        public override IQueryable GetQueryable(IQueryable source)
        {
            string selectedValue = DropDownList1.SelectedValue;
            if (String.IsNullOrEmpty(selectedValue))
            {
                return source;
            }

            if (selectedValue == NullValueString)
            {
                return ApplyEqualityFilter(source, Column.Name, null);
            }

            IDictionary dict = new Hashtable();
            Column.ExtractForeignKey(dict, selectedValue);
            foreach (DictionaryEntry entry in dict)
            {
                string key = (string)entry.Key;
                if (DefaultValues != null)
                {
                    DefaultValues[key] = entry.Value;
                }
                if (entry.Value.ToString() != Resources.DynamicData.All)
                    source = ApplyEqualityFilter(source, Column.GetFilterExpression(key), entry.Value);
            }
            return source;
        }

    }
}
