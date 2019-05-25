using DynamicDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite.DynamicData.FieldTemplates
{
    public partial class Localized_Foreign_Key : System.Web.DynamicData.FieldTemplateUserControl, IValueChangable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        private Nullable<bool> _allowNavigation;

        public string NavigateUrl
        {
            get;
            set;
        }

        public bool AllowNavigation
        {
            get
            {
                if (_allowNavigation != null)
                    return _allowNavigation.Value;
                else
                {
                    if (!ApplicationSettings.DisplayForeignKeyAsLink)
                        return false;
                    else
                        //This is the default:
                        return true;
                }
            }
            set
            {
                _allowNavigation = value;
            }
        }

        private string value;
        string IValueChangable.Value
        {
            get { return this.value; }
        }

        bool IValueChangable.AutoPostBack
        {
            get
            {
                return true;
            }
            set
            {
                ;
            }
        }

        protected string GetDisplayString()
        {
            object valueObject = FieldValue;

            this.value = ForeignKeyColumn.GetForeignKeyString(Row);

            string res;
            if (valueObject == null)
            {
                res = FormatFieldValue(ForeignKeyColumn.GetForeignKeyString(Row));
            }
            else
            {
                res = FormatFieldValue(ForeignKeyColumn.ParentTable.GetDisplayString(valueObject));
            }
            if (this.value == res)
            {
                //System.ComponentModel.ICustomTypeDescriptor obj = ((System.ComponentModel.ICustomTypeDescriptor)(Row));
            }
            try { 
            var loc = MetadataAttributes.OfType<DynamicDataLibrary.Attributes.LocalizedForeignKeyAttribute>().FirstOrDefault();
            if (loc != null)
            {
                string t = GetGlobalResourceObject(loc.ResourceFileName, res).ToString();
                if (t != "") res = t;
            }
            }
            catch { return res; }
            return res;
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

        public override Control DataControl
        {
            get
            {
                return new Control();
            }
        }
    }
}