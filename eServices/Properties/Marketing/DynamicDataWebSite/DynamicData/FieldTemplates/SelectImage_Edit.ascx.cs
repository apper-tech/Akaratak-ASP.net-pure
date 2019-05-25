using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Web.DynamicData;
using System.Text;
using NotAClue.ComponentModel.DataAnnotations;
using NotAClue.Web.DynamicData;
using NotAClue.Web;
using System.IO;

namespace DynamicDataWebSite
{
    public partial class SelectImage_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // get attributes
            var uploadAttribute = MetadataAttributes.GetAttribute<UploadAttribute>();
            if (uploadAttribute == null)
                throw new InvalidOperationException("FileUpload must have valid uploadAttribute applied");

            SetUpValidator(RequiredFieldValidator1);
            SetUpValidator(DynamicValidator1);

            RequiredFieldValidator1.ErrorMessage =
                String.Format(Convert.ToString(GetGlobalResourceObject("DynamicData", "RequiredFieldValidator_MessageFormat")),
                Column.DisplayName);

            // if no images folder return
            var dirInfo = new DirectoryInfo(Server.MapPath(uploadAttribute.ImagesFolder));
            if (!dirInfo.Exists)
            {
                RadioButtonList1.Visible = false;
                return;
            }

            if (RadioButtonList1.Items.Count == 0)
            {
                // get a list of images in the ImageUrlAttribute folder
                var imagesFolder = ResolveUrl(uploadAttribute.ImagesFolder);
                var files = dirInfo.GetFiles(String.Format("*.{0}", uploadAttribute.ImageExtension));

                foreach (FileInfo file in files)
                {
                    // size image to uploadAttribute
                    var imgString = new StringBuilder();

                    imgString.Append(
                        String.Format("<img src='{0}' alt='{1}' ",
                            imagesFolder + file.Name,
                            file.Name.GetFileNameTitle()
                       ));

                    if (uploadAttribute.Width > 0)
                        imgString.Append(String.Format("width='{0}' ", uploadAttribute.Width));

                    if (uploadAttribute.Height > 0)
                        imgString.Append(String.Format("height='{0}' ", uploadAttribute.Height));

                    imgString.Append(" />");

                    // embed image in the radio button
                    var listItem = new ListItem(imgString.ToString(), file.Name);
                    listItem.Attributes.Add("title", file.Name.GetFileNameTitle());

                    this.RadioButtonList1.Items.Add(listItem);
                }
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            var item = RadioButtonList1.Items.FindByValue(FieldValueString);
            if (item != null)
                RadioButtonList1.SelectedValue = FieldValueString;
        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            dictionary[Column.Name] = ConvertEditedValue(RadioButtonList1.SelectedValue);
        }

        public override Control DataControl
        {
            get { return RadioButtonList1; }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                this.RequiredFieldValidator1.ValidationGroup =
                    this.DynamicValidator1.ValidationGroup = (this.Parent as System.Web.DynamicData.DynamicControl).ValidationGroup;
            }
            catch (Exception)
            {

            }
        }
    }
}
