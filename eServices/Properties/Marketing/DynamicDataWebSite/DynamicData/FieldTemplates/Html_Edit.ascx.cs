using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DynamicDataLibrary;
using AjaxControlToolkit.HTMLEditor;

namespace DynamicDataWebSite
{
    public partial class Html_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var att = Column.Attributes[typeof(MaxLengthAttribute)].CustomCast<MaxLengthAttribute>();
            if(att!=null)
                if (Column.MaxLength < 100)
                {
                    TextBox1.Columns = att.Length;
                }
            TextBox1.ToolTip = Column.Description;

            //SetUpValidator(RequiredFieldValidator1);
           // SetUpValidator(RegularExpressionValidator1);
            //SetUpValidator(DynamicValidator1);

            RequiredFieldValidator1.ErrorMessage =
                String.Format(Convert.ToString(GetGlobalResourceObject("DynamicData", "RequiredFieldValidator_MessageFormat")),
                Column.DisplayName);
            RegularExpressionValidator1.ErrorMessage =
                String.Format(Convert.ToString(GetGlobalResourceObject("DynamicData", "RegularExpressionValidator_MessageFormat"))
                , Column.DisplayName);

            this.setCustomAttribute();

        }

        private void setCustomAttribute()
        {
            var htmlAttr = Column.GetAttributeOrDefault<HtmlEditorAttribute>();
            if (htmlAttr.TextBoxCssClass != null)
                TextBox1.CssClass = htmlAttr.TextBoxCssClass;
            if (htmlAttr.TextBoxHeight.Length > 0)
                TextBox1.Height = new Unit(htmlAttr.TextBoxHeight);
            if (htmlAttr.TextBoxWidth.Length > 0)
                TextBox1.Width = new Unit(htmlAttr.TextBoxWidth);

            htmlEditorExtender1.DisplaySourceTab = htmlAttr.DisplaySourceTab;
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            var att = Column.Attributes[typeof(MaxLengthAttribute)].CustomCast< MaxLengthAttribute>();
            int length = 50;
            if (att!=null)
            {
                length = att.Length;
            }
            else
            {
                length = Column.MaxLength;
            }
            if (length> 0)
            {
                TextBox1.MaxLength = Math.Max(FieldValueEditString.Length, length);
            }
        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            dictionary[Column.Name] = ConvertEditedValue(HttpUtility.HtmlDecode(TextBox1.Text));
        }

        public override Control DataControl
        {
            get
            {
                return TextBox1;
            }
        }

        protected void TextBox1_PreRender(object sender, EventArgs e)
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
                    TextBox1.Text = value;
                    TextBox1.Enabled = enableValueChanging;
                    TextBox1.ReadOnly = !enableValueChanging;
                }
            }

            if (this.IsPostBack)
                TextBox1.Text = HttpUtility.HtmlDecode(TextBox1.Text);
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                this.RequiredFieldValidator1.ValidationGroup =
                    this.RegularExpressionValidator1.ValidationGroup =
                    this.DynamicValidator1.ValidationGroup = (this.Parent as System.Web.DynamicData.DynamicControl).ValidationGroup;
            }
            catch (Exception)
            {

            }
        }
    }
}
