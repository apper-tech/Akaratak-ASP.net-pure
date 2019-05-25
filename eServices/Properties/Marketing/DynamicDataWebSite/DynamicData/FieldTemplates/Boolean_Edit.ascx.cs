using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite
{
    public partial class Boolean_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            chk.Text = "&nbsp;&nbsp;";
            chk.BoldFont = false;
            chk.LabelStyle = "font-weight:normal;color:#27DA93";
        }
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            object val = FieldValue;
            if (val != null)
               chk.Checked = (bool)val;
        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
           dictionary[Column.Name] = chk.Checked;
        }

        public override Control DataControl
        {
            get
            {
                return chk;
            }
        }

    }
}
