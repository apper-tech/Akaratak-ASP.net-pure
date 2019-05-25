using DynamicDataLibrary;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite.DynamicData.FieldTemplates
{
    public partial class Localized_Foreign_Key_Edit : System.Web.DynamicData.FieldTemplateUserControl, IValueChangable
    {
        private bool _allowNavigation = false;

        private bool isDropDownList;
        public bool IsDropDownList
        {
            get
            {
                return this.isDropDownList;
            }
            set
            {
                this.isDropDownList = value;

                this.DropDownList1.Visible = value;
                this.HyperLink1.Visible = !value;
            }
        }

        public string NavigateUrl
        {
            get;
            set;
        }

        public bool AllowNavigation
        {
            get
            {
                return _allowNavigation;
            }
            set
            {
                _allowNavigation = value;
            }
        }

        public string Value
        {
            get
            {
                return DropDownList1.SelectedValue;
            }
        }

        public bool AutoPostBack
        {
            set { this.DropDownList1.AutoPostBack = value; }
            get { return this.DropDownList1.AutoPostBack; }
        }

        protected string GetDisplayString()
        {
            try
            {
                object value = FieldValue;

                if (value == null)
                {
                    return FormatFieldValue(ForeignKeyColumn.GetForeignKeyString(Row));
                }
                else
                {
                    return FormatFieldValue(ForeignKeyColumn.ParentTable.GetDisplayString(value));
                }
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        protected string GetNavigateUrl()
        {
            if (!AllowNavigation)
            {
                return null;
            }

            if (String.IsNullOrEmpty(NavigateUrl))
            {
                return ForeignKeyPath;
            }
            else
            {
                return BuildForeignKeyPath(NavigateUrl);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (DropDownList1.Items.Count == 0)
            {
                if (Mode == DataBoundControlMode.Insert || !Column.IsRequired)
                {
                    DropDownList1.Items.Add(new ListItem(Convert.ToString(GetGlobalResourceObject("DynamicData", "NotSet")), ""));
                }
                try {
                    PopulateListControl(DropDownList1);
                }
                catch
                {
                    Response.Redirect(SecurityHandler.ErrorPath);
                }
                ChangeTheItemText(DropDownList1);
            }

            SetUpValidator(RequiredFieldValidator1);
            SetUpValidator(DynamicValidator1);

            RequiredFieldValidator1.ErrorMessage = string.Format(
                Convert.ToString(GetGlobalResourceObject("DynamicData", "RequiredFieldValidator_MessageFormat")), Column.DisplayName);
        }
        public void ChangeTheItemText(DropDownList d)
        {
                var att = MetadataAttributes.OfType<DynamicDataLibrary.Attributes.LocalizedForeignKeyAttribute>().FirstOrDefault();
            if (att != null)
            {
                if (!string.IsNullOrEmpty( att.ResourceFileName))
                    for (int i = 0; i < d.Items.Count; i++)
                    {
                        d.Items[i].Text = GetLocalizedText(att.ResourceFileName, d.Items[i].Text);
                    }
               
            }
            
        }
        public static string GetLocalizedText(string fname, string item)
        {
            //gets the localized text
            string t = Convert.ToString( HttpContext.GetGlobalResourceObject(fname, item));
            if (t != "")
                item = t;
            return item;
        }
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            string selectedValueString = GetSelectedValueString();
            ListItem item = DropDownList1.Items.FindByValue(selectedValueString);
            if (item != null)
            {
                DropDownList1.SelectedValue = selectedValueString;

                if (false
                    //this.Table.Name == "Letters" && this.Mode == DataBoundControlMode.Edit
                    )
                {
                    this.HyperLink1.Text = item.Text;
                    this.IsDropDownList = false;
                }
                else if (false
                   //ForeignKeyColumn.ForeignKeyNames[0] == "CarId" &&
                   //  (this.Table.GetColumnValuesFromRoute(HttpContext.Current).ContainsKey(ForeignKeyColumn.ForeignKeyNames[0])
                   //       &&
                   //       this.Mode == DataBoundControlMode.Insert)
                   )
                {
                    this.HyperLink1.Text = item.Text;
                    this.IsDropDownList = false;
                }
                else
                {
                    this.IsDropDownList = true;
                }
            }
        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            // If it's an empty string, change it to null
            string value = DropDownList1.SelectedValue;
            ExtractForeignKey(dictionary, string.IsNullOrEmpty(value)?string.Empty:value);
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
                if (this.Mode == DataBoundControlMode.Insert
                    || (this.Mode == DataBoundControlMode.Edit && useOnEdit))
                {
                    ListItem item = this.DropDownList1.Items.FindByValue(value);
                    if (item != null)
                    {
                        this.DropDownList1.SelectedIndex = -1;
                        item.Selected = true;
                        if (!enableValueChanging)
                        {
                            this.HyperLink1.Text = this.DropDownList1.SelectedItem.Text;
                            this.IsDropDownList = false;
                        }
                    }
                }
            }
        }


        //// advanced populate list control
        //protected new void PopulateListControl(ListControl listControl)
        //{
        //    // cast Column as MetaForeignKeyColumn
        //    var ChildColumn = Column as MetaForeignKeyColumn;

        //    // get the child columns parent table
        //    var childTable = ChildColumn.ParentTable;

        //    // get query {Table(Developer).OrderBy(d => d.Name)}
        //    var query = ChildColumn.ParentTable.GetQuery(Column.Table.CreateContext());

        //    // clear list controls items collection before adding new items
        //    listControl.Items.Clear();
        //    listControl.Items.Add(new ListItem("[All]", ""));

        //    // add returned values to list control
        //    foreach (var row in query)
        //        listControl.Items.Add(
        //            new ListItem(
        //                childTable.GetDisplayString(row),
        //                childTable.GetPrimaryKeyString(row)));

        //    // make sure control is enabled
        //    listControl.Enabled = true;
        //}
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
