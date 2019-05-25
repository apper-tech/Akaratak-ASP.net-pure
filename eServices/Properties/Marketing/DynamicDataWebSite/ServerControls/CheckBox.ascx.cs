using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite.ServerControls
{
    public partial class CheckBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        public delegate void CheckBox_Delegate(object sender, EventArgs e);
        public event CheckBox_Delegate Checked_Changed;
        public bool Checked { set { Agree.Checked = value; } get { return Agree.Checked; } }
        public bool BoldFont { set { Agree.Font.Bold = value; } get { return Agree.Font.Bold; } }
        public Label ValidateCheckboxError { get { return ValidateCheckboxErrorLt; } }
        public string Text { set { Agree.Text = value; } get { return Agree.Text; } }
        public string Style { set; get; }
        public string LabelStyle { set; get; }
        public bool Required { set; get; }

        protected void Agree_CheckedChanged(object sender, EventArgs e)
        {
            if (Checked_Changed != null)
                Checked_Changed(this, new EventArgs());
        }
    }
}