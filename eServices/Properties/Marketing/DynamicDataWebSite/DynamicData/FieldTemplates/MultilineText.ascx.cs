using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite.DynamicData.FieldTemplates
{
    public partial class MultilineText : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         
        }
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            Text = FieldValueString;
            string pageName = Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);
            if (pageName == "Details"  && Text.Length>15)
            {
               Page.Title = Text.Substring(0, 15);
            }
        }
        string text;

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
                txt.Text = text;
            }
        }
    }
}