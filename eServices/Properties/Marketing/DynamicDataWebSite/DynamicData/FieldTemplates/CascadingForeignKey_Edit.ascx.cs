using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NotAClue.Web.DynamicData;
using System.Linq;
using System.Collections.Generic;

namespace DynamicDataWebSite
{
    public partial class CascadingForeignKey_EditField : CascadingFieldTemplate //System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            // add event handler if parent exists
            Column.Name.ToCharArray();
            if (ParentControl != null)
            {
                ParentControl.SelectionChanged += SelectedIndexChanged;

            }
            FixText(DropDownList1);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (DropDownList1.Items.Count == 0)
            {
                if (ParentColumn == null)
                {
                    DropDownList1.Items.Add(new ListItem("", ""));
                    PopulateListControl(DropDownList1);
                }
                else
                {
                    var att = ChildColumn.Attributes.OfType<NotAClue.ComponentModel.DataAnnotations.CascadeAttribute>().FirstOrDefault();
                    if (att != null)
                    {
                        DropDownList1.Items.Add(new ListItem(Convert.ToString(GetGlobalResourceObject("RealEstate", att.Child_Select_First)), ""));
                    }
                    DropDownList1.Enabled = false;
                }
            }
            if (Request.QueryString.Count > 0)
            {
                var d = GetQueryValue(DropDownList1);
                if (d != null)
                    RaiseSelectedIndexChanged(d.Value, d.Text);
            }
            FixText(DropDownList1);
            //SetUpValidator(RequiredFieldValidator1);
            //SetUpValidator(DynamicValidator1);

            RequiredFieldValidator1.ErrorMessage = string.Format(
                Convert.ToString(GetGlobalResourceObject("DynamicData", "RequiredFieldValidator_MessageFormat")), Column.DisplayName);

            // only enable post-back if has a event hooked up
            DropDownList1.AutoPostBack = this.HasEvents;

        }
        public ListItem GetQueryValue(DropDownList d)
        {
            for (int i = 0; i < Request.QueryString.Count; i++)
            {
                if (Request.QueryString.AllKeys[i].Contains(Column.Name))
                {
                    return new ListItem(Request.QueryString.AllKeys[i], Request.QueryString[i]);
                }
            }
            return null;
        }
        public void FixText(DropDownList dr)
        {
            var att = Column.Attributes.OfType<DynamicDataLibrary.Attributes.LocalizedForeignKeyAttribute>().FirstOrDefault();
            if (att != null && att.ResourceFileName != null)
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
        // raise event
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RaiseSelectedIndexChanged(DropDownList1.SelectedValue, DropDownList1.SelectedItem.Text);
            FixText(DropDownList1);

        }
        public string GetAttributeVariable(string name)
        {
            if (DropDownList1.SelectedItem.Attributes.Count > 0 && !string.IsNullOrEmpty(DropDownList1.SelectedItem.Attributes[name]))
                return DropDownList1.SelectedItem.Attributes[name];
            return "";
        }

        // consume event
        protected void SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateListControl(DropDownList1, e.Value);
            FixText(DropDownList1);
            if (Mode == DataBoundControlMode.Insert || !Column.IsRequired)
                RaiseSelectedIndexChanged("", "");
            else if (DropDownList1.Items.Count > 0)
                RaiseSelectedIndexChanged(DropDownList1.Items[0].Value, DropDownList1.Items[0].Text);
            else
                RaiseSelectedIndexChanged("", "");
            AttachEvent(DropDownList1, e.Value);
            SortList(DropDownList1);
            //CorrectText(e.Value, true);
        }
        private void SortList(DropDownList dr)
        {
            List<ListItem> list = new List<ListItem>();
            foreach (ListItem item in dr.Items)
            {
                list.Add(item);
            }
            list=list.OrderBy(t => t.Text).ToList();
            DropDownList1.Items.Clear();
            foreach (ListItem item in list)
            {
                dr.Items.Add(item);
            }
        }
        private void AttachEvent(DropDownList dr, string value)
        {
            List<DynamicDataModel.Model.City> ls = DynamicDataHelper.GetCityList(value);
            foreach (ListItem item in dr.Items)
            {
                try
                {
                   item.Attributes.Add("tooltip", ls[int.Parse(item.Value) - 1].Latitude + "|" + ls[int.Parse(item.Value) - 1].Longitude);
                }
                catch { continue; }
            }
            dr.Attributes.Add("onchange", "HookbtnSelect()");
        }
        #endregion

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            string selectedValueString = GetSelectedValueString();
            RaiseSelectedIndexChanged(selectedValueString, "");
            ListItem item = DropDownList1.Items.FindByValue(selectedValueString);
            if (item != null)
                DropDownList1.SelectedValue = selectedValueString;
        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            // If it's an empty string, change it to null
            string value = DropDownList1.SelectedValue;
            if (String.IsNullOrEmpty(value))
                value = null;

            ExtractForeignKey(dictionary, value);
        }

        public override Control DataControl
        {
            get { return DropDownList1; }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                this.RequiredFieldValidator1.ValidationGroup =
                    this.DynamicValidator1.ValidationGroup = (this.Parent as DynamicControl).ValidationGroup;
            }
            catch (Exception)
            {

            }
        }

        protected void DropDownList1_DataBinding(object sender, EventArgs e)
        {

        }
    }
}
