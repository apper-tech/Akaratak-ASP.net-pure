using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DynamicDataLibrary;
using System.Linq;

namespace DynamicDataWebSite
{
    public partial class ForeignKeyField : System.Web.DynamicData.FieldTemplateUserControl, IValueChangable
    {
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
            var loc = MetadataAttributes.OfType<DynamicDataLibrary.Attributes.LocalizedForeignKeyAttribute>().FirstOrDefault();
            if (loc != null)
            {
                string t = GetGlobalResourceObject(loc.ResourceFileName, res).ToString();
                if (t != "") res = t;
            }
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
                return HyperLink1;
            }
        }
    }

}