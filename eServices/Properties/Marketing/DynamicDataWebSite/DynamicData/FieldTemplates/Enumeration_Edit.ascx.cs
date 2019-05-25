using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DynamicDataLibrary;

namespace DynamicDataWebSite
{
    public partial class Enumeration_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {
        private Type _enumType;

        protected void Page_Load(object sender, EventArgs e)
        {
            DropDownList1.ToolTip = Column.Description;

            if (DropDownList1.Items.Count == 0)
            {
                if (Mode == DataBoundControlMode.Insert || !Column.IsRequired)
                {
                    DropDownList1.Items.Add(new ListItem(Convert.ToString(GetGlobalResourceObject("DynamicData", "NotSet")), String.Empty));
                }
                PopulateListControl(DropDownList1);
            }

            //SetUpValidator(RequiredFieldValidator1);
            //SetUpValidator(DynamicValidator1);

            RequiredFieldValidator1.ErrorMessage =
                String.Format(Convert.ToString(GetGlobalResourceObject("DynamicData", "RequiredFieldValidator_MessageFormat")),
                Column.DisplayName);
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            if (Mode == DataBoundControlMode.Edit && FieldValue != null)
            {
                string selectedValueString = GetSelectedValueString();
                ListItem item = DropDownList1.Items.FindByValue(selectedValueString);
                if (item != null)
                {
                    DropDownList1.SelectedValue = selectedValueString;
                }
            }
        }

        private Type EnumType
        {
            get
            {
                if (_enumType == null)
                {
                    _enumType = Column.GetEnumType();
                }
                return _enumType;
            }
        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            string value = DropDownList1.SelectedValue;
            if (value == String.Empty)
            {
                value = null;
            }
            dictionary[Column.Name] = ConvertEditedValue(value);
        }

        public override Control DataControl
        {
            get
            {
                return DropDownList1;
            }
        }

        protected void DropDownList1_DataBound(object sender, EventArgs e)
        {
            // value gotten via a query string as below 
            //string value = Request.QueryString[Column.Name];

            // value gotten via metadata 
            bool enableValueChanging;
            bool useOnEdit;
            string value = Convert.ToString(Column.GetCustomDefaultValue(out enableValueChanging, out useOnEdit));
            if (!string.IsNullOrEmpty(value))
            {
                ListItem item = this.DropDownList1.Items.FindByValue(value);
                if (item != null)
                {
                    if (this.Mode == DataBoundControlMode.Insert
                        || (this.Mode == DataBoundControlMode.Edit && useOnEdit))
                    {
                        this.DropDownList1.SelectedIndex = -1;
                        item.Selected = true;
                        this.DropDownList1.Enabled = enableValueChanging;
                    }
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                this.RequiredFieldValidator1.ValidationGroup =
                    this.DynamicValidator1.ValidationGroup = (this.Parent as System.Web.DynamicData.DynamicControl).ValidationGroup;
            }
            catch (Exception)
            {

            }
        }
    }
}
