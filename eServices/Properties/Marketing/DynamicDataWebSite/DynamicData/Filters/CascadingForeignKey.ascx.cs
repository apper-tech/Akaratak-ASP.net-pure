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
using NotAClue.Web.DynamicData;

namespace DynamicDataWebSite
{
    public partial class CascadingForeignKeyFilter : CascadingFilter, ICascadingControl //System.Web.DynamicData.QueryableFilterUserControl
    {
        private string NullValueString { get { return Convert.ToString(GetGlobalResourceObject("DynamicData", "Null")); } }

        private new MetaForeignKeyColumn Column
        {
            get { return (MetaForeignKeyColumn)base.Column; }
        }

        public override Control FilterControl
        {
            get { return DropDownList1; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // add event handler if parent exists
            if (ParentControl != null)
                ParentControl.SelectionChanged += SelectedIndexChanged;

            if (!Page.IsPostBack)
            {
                DropDownList1.Items.Add(new ListItem(Convert.ToString(GetGlobalResourceObject("DynamicData", "All")),""));
                string initialValue = GetInitialValue(DropDownList1);
                if (ParentControl != null)
                    PopulateListControl(DropDownList1, ParentControl.SelectedValue,initialValue);
                else
                {
                    PopulateListControl(DropDownList1);
                }
                
                // Set the initial value if there is one
                if (DropDownList1.Items.Count > 1 && !String.IsNullOrEmpty(initialValue))
                {
                    DropDownList1.SelectedValue = initialValue;
                    this.SelectedValue = initialValue;
                    RaiseSelectedIndexChanged(DropDownList1.SelectedValue, DropDownList1.SelectedItem.Text);
                }
                else
                {
                    RaiseSelectedIndexChanged("", "");
                }
                
            }
            FixText(DropDownList1);
        }
        public void FixText(DropDownList dr)
        {
            var att = Column.Attributes.OfType<DynamicDataLibrary.Attributes.LocalizedForeignKeyAttribute>().FirstOrDefault();
            if (att != null && att.ResourceFileName!=null)
            {
                foreach (ListItem item in dr.Items)
                {
                    string txt = Convert.ToString(GetGlobalResourceObject(att.ResourceFileName, item.Text));
                    if (!string.IsNullOrEmpty(txt))
                        item.Text = txt;
                }
            }
        }
        #region Event
        // consume event
        protected void SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateListControl(DropDownList1, e.Value,"0");
            RaiseSelectedIndexChanged("", "");
            FixText(DropDownList1);
        }
        #endregion
        public string GetInitialValue(DropDownList dr)
        {
            if (Request.QueryString.Count > 0)
            {
                string val = Request.QueryString.Get(Column.Name);
                if (!string.IsNullOrEmpty(val) && dr.SelectedItem.Text != val)
                {
                    return val;
                }
            }
            return "";
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnFilterChanged();
            // raise event
            RaiseSelectedIndexChanged(DropDownList1.SelectedValue, DropDownList1.SelectedItem.Text);
            if(IsPostBack)
            {
                Page.Title = Resources.RealEstate.Page_List_Title + ": " + DropDownList1.SelectedItem.Text;
            }
        }

        public override IQueryable GetQueryable(IQueryable source)
        {
            string selectedValue = DropDownList1.SelectedValue;
            if (String.IsNullOrEmpty(selectedValue))
                return source;

            if (selectedValue == NullValueString)
                return ApplyEqualityFilter(source, Column.Name, null);

            IDictionary dict = new Hashtable();
            Column.ExtractForeignKey(dict, selectedValue);
            foreach (DictionaryEntry entry in dict)
            {
                string key = (string)entry.Key;
                if (DefaultValues != null)
                    DefaultValues[key] = entry.Value;
                if ((string)entry.Value != Convert.ToString(Convert.ToString(GetGlobalResourceObject("DynamicData", "All"))))
                {
                    source = ApplyEqualityFilter(source, Column.GetFilterExpression(key), entry.Value);
                }
            }
            return source;
        }
    }
}
