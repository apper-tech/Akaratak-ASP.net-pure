using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NotAClue.Web.DynamicData;

namespace $rootnamespace$
{
    public partial class CascadingForeignKey_EditField : CascadingFieldTemplate //System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            // add event handler if parent exists
            if (ParentControl != null)
                ParentControl.SelectionChanged += SelectedIndexChanged;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (DropDownList1.Items.Count == 0)
            {
                if (Mode == DataBoundControlMode.Insert || !Column.IsRequired)
                    DropDownList1.Items.Add(new ListItem("[Not Set]", ""));

                PopulateListControl(DropDownList1);
            }

            SetUpValidator(RequiredFieldValidator1);
            SetUpValidator(DynamicValidator1);

            // only enable post-back if has a event hooked up
            DropDownList1.AutoPostBack = this.HasEvents;
        }

        #region Event
        // raise event
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RaiseSelectedIndexChanged(DropDownList1.SelectedValue, DropDownList1.SelectedItem.Text);
        }

        // consume event
        protected void SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateListControl(DropDownList1, e.Value);

            if (Mode == DataBoundControlMode.Insert || !Column.IsRequired)
                RaiseSelectedIndexChanged("", "");
            else if(DropDownList1.Items.Count > 0)
                RaiseSelectedIndexChanged(DropDownList1.Items[0].Value, DropDownList1.Items[0].Text);
            else
                RaiseSelectedIndexChanged("", "");
        }
        #endregion

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            string selectedValueString = GetSelectedValueString();
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
    }
}
