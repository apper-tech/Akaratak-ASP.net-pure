﻿using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DynamicDataLibrary;
using System.Linq;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace DynamicDataWebSite
{
    public partial class EmailAddress_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Column.MaxLength < 20)
            {
                TextBox1.Columns = Column.MaxLength;
            }
            TextBox1.ToolTip = Column.Description;

            SetUpValidator(RequiredFieldValidator1);
            SetUpValidator(RegularExpressionValidator1);
            SetUpValidator(DynamicValidator1);

            RequiredFieldValidator1.ErrorMessage =
                String.Format(Convert.ToString(GetGlobalResourceObject("DynamicData", "RequiredFieldValidator_MessageFormat")),
                Column.DisplayName);
            RegularExpressionValidator1.ErrorMessage =
                String.Format(Convert.ToString(GetGlobalResourceObject("DynamicData", "RegularExpressionValidator_MessageFormat"))
                , Column.DisplayName);
            var emb = Column.Attributes.OfType<DynamicDataLibrary.Attributes.EmbedField>().FirstOrDefault();
            if(emb!=null)
            {
                Embed = true;
                if (emb.Hide)
                    TextBox1.Enabled = false;
                if (emb.GetfieldfromPrinceble)
                {
                    var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var user = manager.FindById(Context.User.Identity.GetUserId());
                    if (user != null)
                        TextBox1.Text = user.Email;

                }

            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            if (Column.MaxLength > 0)
            {
                TextBox1.MaxLength = Math.Max(FieldValueEditString.Length, Column.MaxLength);
            }
        }
        bool embed;
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

        public bool Embed
        {
            get
            {
                return embed;
            }

            set
            {
                embed = value;
            }
        }

        protected void TextBox1_PreRender(object sender, EventArgs e)
        {
            // value gotten via a query string as below 
            //string value = Request.QueryString[Column.Name];

            // value gotten via metadata 
            bool enableValueChanging;
            bool useOnEdit;
            if (!Embed)
            {
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