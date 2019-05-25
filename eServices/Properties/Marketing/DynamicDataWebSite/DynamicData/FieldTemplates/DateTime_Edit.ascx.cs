using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using DynamicDataLibrary.ModelRelated;
using DynamicDataLibrary;
using DynamicDataLibrary;

namespace DynamicDataWebSite
{
    public partial class DateTime_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {
        private static DataTypeAttribute DefaultDateAttribute = new DataTypeAttribute(DataType.DateTime);
        protected readonly static String DEFAULT_DATE_FORMAT = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox1.ToolTip = Column.Description;

            SetUpValidator(RequiredFieldValidator1);
            SetUpValidator(RegularExpressionValidator1);
            //SetUpValidator(DynamicValidator1);
            SetUpCustomValidator(DateValidator);
            
            RequiredFieldValidator1.ErrorMessage = string.Format(
                Convert.ToString(GetGlobalResourceObject("DynamicData", "Date_RequiredFieldValidator_MessageFormat")),
                Column.DisplayName);
            RegularExpressionValidator1.ErrorMessage = string.Format(
                Convert.ToString(GetGlobalResourceObject("DynamicData", "Date_RegularExpressionValidator_MessageFormat")),
                Column.DisplayName);

            //if (!this.IsPostBack)
            {
                if (!String.IsNullOrEmpty(this.Column.DetectFormat(false)))
                    this.format = this.Column.DetectFormat(false);
                else
                    this.format = DEFAULT_DATE_FORMAT;
            }
        }

        private string format;

        public string FormatFieldValue(string fieldValueString)
        {
            this.format = this.Column.DetectFormat(false);

            this.TextBox1.Columns = this.Column.DetectFormat(true).Length <= 10 ? 10 : 20;

            this.ProcessForCreatedByAndUpdatedBy(ref fieldValueString);

            if (!String.IsNullOrEmpty(this.format) && !String.IsNullOrEmpty(fieldValueString))
            {
                if (this.format.Contains("0:"))
                {
                    //LableNote.Text = "يجب أن يكون التنسيق مثلا: " +
                    //    String.Format(this.format, Convert.ToDateTime(fieldValueString));
                    return String.Format(this.format, Convert.ToDateTime(fieldValueString));
                }
                else
                {
                    //LableNote.Text = "يجب أن يكون التنسيق مثلا: " +
                    //    Convert.ToDateTime(fieldValueString).ToString(this.format);
                    return Convert.ToDateTime(fieldValueString).ToString(this.format);
                }
            }
            else
                return fieldValueString;
        }



        private void ProcessForCreatedByAndUpdatedBy(ref string fieldValueString)
        {
            UIHintAttribute hint = null;
            bool isForUpdatedBy = false;
            bool isForCreatedBy = false;
            bool IsDefaultToNow = false;

            hint = (UIHintAttribute)this.Column.Attributes[typeof(UIHintAttribute)];

            if (hint != null)
            {
                if (hint.ControlParameters.Count > 0)
                {
                    if (hint.ControlParameters.ContainsKey("IsForUpdatedDateTime") && hint.ControlParameters["IsForUpdatedDateTime"] != null)
                    {
                        isForUpdatedBy = Convert.ToBoolean(hint.ControlParameters["IsForUpdatedDateTime"]);
                    }
                    if (hint.ControlParameters.ContainsKey("IsForCreatedDateTime") && hint.ControlParameters["IsForCreatedDateTime"] != null)
                    {
                        isForUpdatedBy = Convert.ToBoolean(hint.ControlParameters["IsForCreatedDateTime"]);
                    }
                    if (hint.ControlParameters.ContainsKey("IsDefaultToNow") && hint.ControlParameters["IsDefaultToNow"] != null)
                    {
                        IsDefaultToNow = Convert.ToBoolean(hint.ControlParameters["IsDefaultToNow"]);
                    }
                }
            }

            if (isForUpdatedBy)
            {
                if (this.Mode == DataBoundControlMode.Insert || this.Mode == DataBoundControlMode.Edit)
                {
                    fieldValueString = DateTime.Now.ToString();
                }
                this.TextBox1.Enabled = false;

                //this.NamingContainer.Visible = false;
            }
            else if (isForCreatedBy)
            {
                if (this.Mode == DataBoundControlMode.Insert)
                {
                    TextBox1.Text = DateTime.Now.ToString();
                }
                this.TextBox1.Enabled = false;
            }
            if (IsDefaultToNow)
            {
                if (this.Mode == DataBoundControlMode.Insert)
                {
                    //if (!String.IsNullOrEmpty(this.format))
                    //    fieldValueString = DateTime.Now.ToString(this.format);
                    //else
                    fieldValueString = DateTime.Now.ToString();
                }
            }

        }

        private void SetUpCustomValidator(CustomValidator validator)
        {
            if (Column.DataTypeAttribute != null)
            {
                switch (Column.DataTypeAttribute.DataType)
                {
                    case DataType.Date:
                    case DataType.DateTime:
                    case DataType.Time:
                        validator.Enabled = true;
                        DateValidator.ErrorMessage = HttpUtility.HtmlEncode(Column.DataTypeAttribute.FormatErrorMessage(Column.DisplayName));
                        break;
                }
            }
            else if (Column.ColumnType.Equals(typeof(DateTime)))
            {
                validator.Enabled = true;
                DateValidator.ErrorMessage = HttpUtility.HtmlEncode(DefaultDateAttribute.FormatErrorMessage(Column.DisplayName));
            }

            var metadata = MetadataAttributes.OfType<DisplayFormatAttribute>().FirstOrDefault();
            if (metadata != null)
            {
                LableNote.Text = "يجب أن يكون التنسيق مثلا: " +
                    metadata.DataFormatString.Replace("{0:", "").Replace("}", "");
            }
        }

        protected void DateValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime dummyResult;
            args.IsValid = DateTime.TryParse(args.Value, out dummyResult);
        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            try
            {
                //Processing for Time:
                if (TextBox1.Text.Contains(":") && !TextBox1.Text.Contains("/"))
                {
                    TextBox1.Text = TextBox1.Text.Replace(" ", "");
                    if (!TextBox1.Text.EndsWith("ص") && !TextBox1.Text.EndsWith("م"))
                        TextBox1.Text = TextBox1.Text + "ص";
                    TextBox1.Text = TextBox1.Text.Insert(TextBox1.Text.Length - 1, " ");
                    if (TextBox1.Text.IndexOf(":") == 1)
                        TextBox1.Text = "0" + TextBox1.Text;
                    if (TextBox1.Text.IndexOf(" ") == 4)
                        TextBox1.Text = TextBox1.Text.Insert(3, "0");

                    CultureInfo arSa = new CultureInfo(DynamicDataWebSite.DynamicDataHelper.DefaultCulture);
                    DateTime dateTime = DateTime.ParseExact(TextBox1.Text, "hh:mm tt", arSa);
                    dictionary[Column.Name] = dateTime.TimeOfDay.ToString();
                }
                else
                    dictionary[Column.Name] = ConvertEditedValue(TextBox1.Text);
            }
            catch (Exception)
            {
                dictionary[Column.Name] = ConvertEditedValue(TextBox1.Text);
            }
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
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                this.RequiredFieldValidator1.ValidationGroup =
                    this.RegularExpressionValidator1.ValidationGroup =
                    this.DateValidator.ValidationGroup =
                    this.DynamicValidator1.ValidationGroup = (this.Parent as System.Web.DynamicData.DynamicControl).ValidationGroup;
            }
            catch (Exception)
            {

            }
        }
    }
}
