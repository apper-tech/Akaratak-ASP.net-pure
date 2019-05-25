using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DynamicDataLibrary;
using System.Linq;
using System.Collections.Generic;

namespace DynamicDataWebSite
{
    public partial class Integer_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox1.ToolTip = Column.Description;
           if(!Column.IsRequired)
            {
                RequiredFieldValidator1.Enabled = false;
            }
            SetUpValidator(RequiredFieldValidator1);
            SetUpValidator(CompareValidator1);
            SetUpValidator(RegularExpressionValidator1);
            SetUpValidator(RangeValidator1);
            SetUpValidator(DynamicValidator1);



            RequiredFieldValidator1.ErrorMessage = string.Format(
                Convert.ToString(GetGlobalResourceObject("DynamicData", "RequiredFieldValidator_MessageFormat")), Column.DisplayName);
            CompareValidator1.ErrorMessage = string.Format(
                Convert.ToString(GetGlobalResourceObject("DynamicData", "Integer_CompareValidator_MessageFormat")), Column.DisplayName);
            RegularExpressionValidator1.ErrorMessage = string.Format(
                Convert.ToString(GetGlobalResourceObject("DynamicData", "RegularExpressionValidator_MessageFormat")), Column.DisplayName);
            RangeAttribute rangeAtt = Column.Attributes.OfType<RangeAttribute>().FirstOrDefault();
            if (rangeAtt != null)
                if (!String.IsNullOrEmpty(rangeAtt.ErrorMessage))
                    RangeValidator1.ErrorMessage = rangeAtt.ErrorMessage;
                else if (!String.IsNullOrEmpty(rangeAtt.ErrorMessageResourceName))
                { 
                    //Keep Defalut!
                }
                else
                    RangeValidator1.ErrorMessage = string.Format(
                        Convert.ToString(GetGlobalResourceObject("DynamicData", "Integer_RangeValidator_MessageFormat")), Column.DisplayName,
                        rangeAtt.Minimum, rangeAtt.Maximum);
            var intaat = Column.Attributes.OfType<DynamicDataLibrary.Attributes.IntegerAttribute>().FirstOrDefault();
            if (intaat != null)
            {
                //neeeds fixing
                if(!String.IsNullOrEmpty(intaat.MinValue.ToString()))
                RangeValidator1.MinimumValue = intaat.MinValue.ToString();
                if (!String.IsNullOrEmpty(intaat.MaxValue.ToString()))
                    RangeValidator1.MaximumValue = intaat.MaxValue.ToString();
            }
            else
            {
                RangeValidator1.MinimumValue = "0";
                RangeValidator1.MaximumValue = "99999999";
            }
            DynamicValidator1.ErrorMessage = Convert.ToString(GetGlobalResourceObject("DynamicData", "Range_Min_Description") + RangeValidator1.MinimumValue + " , "+ GetGlobalResourceObject("DynamicData", "Range_Max_Description") + " , " +RangeValidator1.MaximumValue);
            if (rangeAtt == null)
                RangeValidator1.ErrorMessage = Convert.ToString(DynamicValidator1.ErrorMessage);
        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            dictionary[Column.Name] = ConvertEditedValue(TextBox1.Text);
        }

        public override Control DataControl
        {
            get
            {
                return TextBox1;
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            
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
                    this.CompareValidator1.ValidationGroup =
                    this.RangeValidator1.ValidationGroup =
                    this.DynamicValidator1.ValidationGroup = (this.Parent as System.Web.DynamicData.DynamicControl).ValidationGroup;
            }
            catch (Exception)
            {

            }
        }


    }
}
