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
    public partial class BooleanField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            object val = FieldValue;
            var col = Column.Attributes.OfType<DynamicDataLibrary.Attributes.CustomViewAttribute>().FirstOrDefault();
            bool cus = false;
            if (col!=null)
            {
                string txt = GetGlobalResourceObject(col.CustomRes, col.CustomText) as string;
                er.Text = txt;
                cus = true;
            }
             if(val.GetType()==typeof(bool)&&!cus)
            {
                if ((bool)val)
                    er.Text = GetGlobalResourceObject("DynamicData", "Yes")!=null ? GetGlobalResourceObject("DynamicData", "Yes").ToString() : "Yes";
                else
                    er.Text = GetGlobalResourceObject("DynamicData", "No") != null ? GetGlobalResourceObject("DynamicData", "No").ToString() : "No";
            }
        }

        public override Control DataControl
        {
            get
            {
                return er;
            }
        }

    }
}
