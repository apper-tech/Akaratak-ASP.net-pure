using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite.DynamicData.FieldTemplates
{
    public partial class Integer : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            txt1.Text = FieldValueString;

            var att = Column.Attributes.OfType<DynamicDataLibrary.Attributes.CustomViewAttribute>().FirstOrDefault();
            if (att != null && txt1.Text != "")
            {
                string res = "";
                int val = int.Parse(txt1.Text);
                switch (val)
                {
                    case -1: res = "Flr_Basment"; break;
                    case 0: res = "Flr_Ground"; break;
                    case 1: res = "Flr_First"; break;
                    case 2: res = "Flr_Second"; break;
                    case 3: res = "Flr_Third"; break;
                    case 4: res = "Flr_Fourth"; break;
                    case 5: res = "Flr_Fifth"; break;
                }
                if (!string.IsNullOrEmpty(res))
                {
                    string text = GetGlobalResourceObject(att.CustomRes, res).ToString();
                    if (!string.IsNullOrEmpty(text))
                        txt1.Text = text;
                }
            }
             var att2 = Column.Attributes.OfType<DynamicDataLibrary.Attributes.IntegerAttribute>().FirstOrDefault();
            if(att2!=null)
            {
                if(att2.EmptyString && string.IsNullOrEmpty(txt1.Text))
                {
                    txt1.Text = Resources.RealEstate.Empty;
                }
                //need to add the currency logo here
            }
        }
    }
}