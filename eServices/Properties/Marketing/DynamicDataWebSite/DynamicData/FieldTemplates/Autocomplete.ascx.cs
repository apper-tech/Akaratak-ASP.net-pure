using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NotAClue.Web.DynamicData;

namespace DynamicDataWebSite
{
    public partial class AutocompleteField : System.Web.DynamicData.FieldTemplateUserControl
    {
        private bool _allowNavigation = true;

        //private object FieldValueBackup
        //{
        //    get
        //    {
        //        return this.ViewState["FieldValueBackup"];
        //    }
        //    set
        //    {
        //        this.ViewState["FieldValueBackup"] = value;
        //    }
        //}

        public string NavigateUrl { get; set; }

        public bool AllowNavigation
        {
            get { return _allowNavigation; }
            set { _allowNavigation = value; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //try
            //{
            //    this.FieldValueBackup = this.GetDisplayString();
            //}
            //catch (Exception)
            //{
            //}
        }

        protected string GetDisplayString()
        {
            //if (FieldValue == null)
            //    this.FieldValue = this.FieldValueBackup;
            //else if (this.FieldValueBackup == null)
            //    this.FieldValueBackup = this.FieldValue;

            object value = FieldValue;

            if (value == null)
                return FormatFieldValue(ForeignKeyColumn.GetForeignKeyString(Row));
            else
                return FormatFieldValue(ForeignKeyColumn.ParentTable.GetDisplayString(value));
        }

        protected string GetNavigateUrl()
        {
            if (!AllowNavigation)
                return null;

            if (String.IsNullOrEmpty(NavigateUrl))
                return ForeignKeyPath;
            else
                return BuildForeignKeyPath(NavigateUrl);
        }

        public override Control DataControl
        {
            get { return HyperLink1; }
        }
    }
}
