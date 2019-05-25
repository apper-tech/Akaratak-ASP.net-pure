using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace DynamicDataWebSite
{
    public partial class CascadingForeignKeyField : System.Web.DynamicData.FieldTemplateUserControl
    {
        private bool _allowNavigation = true;

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
                var c = Column.Attributes.OfType<NotAClue.ComponentModel.DataAnnotations.CascadeAttribute>().FirstOrDefault();
                if (c != null)
                {
                    _allowNavigation = c.AllowNavigation;
                }
                else
                {
                    _allowNavigation = value;
                }
            }
        }

        protected string GetDisplayString()
        {
            object value = FieldValue;
            string res = "";
          
            if (value == null)
            {
                res= FormatFieldValue(ForeignKeyColumn.GetForeignKeyString(Row));
            }
            else
            {
                res= FormatFieldValue(ForeignKeyColumn.ParentTable.GetDisplayString(value));
            }
           
            if (Table.GetActionPath("ListWithIcons")==Request.Url.AbsolutePath && res.Length>8)
            {
              if(res.Contains('('))
                {
                    int i = res.IndexOf('(');
                    res = res.Remove(i, res.Length - i);
                }
              else if (res.Length > 20)
                {
                    res = res.Remove(res.IndexOf(" ",10));
                }
            }
            var c = Column.Attributes.OfType<DynamicDataLibrary.Attributes.CustomViewAttribute>().FirstOrDefault();
            if (c != null)
            {
                if(res.Length>=c.Length)
                res = res.Substring(0, c.Length - 3) + "...";
            }
            if (res == Resources.Property_Type.Commonhold)
            {
                HyperLink1.Attributes.Add("style", "font-size: 13px;");
            }
            try
            {
                string rs = GetGlobalResourceObject("Property_Type", res).ToString();
                if (!string.IsNullOrEmpty(rs))
                    res = rs;
                else
                {
                    rs = GetGlobalResourceObject("Property_Category", res).ToString();
                    if (!string.IsNullOrEmpty(rs))
                        res = rs;
                }
            }
            catch { return res; }
        
                return res;
        }

        protected string GetNavigateUrl()
        {
            var c = Column.Attributes.OfType<NotAClue.ComponentModel.DataAnnotations.CascadeAttribute>().FirstOrDefault();
            if (c != null)
            {
                _allowNavigation = c.AllowNavigation;
            }
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
