using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using NotAClue;
using NotAClue.ComponentModel.DataAnnotations;
using NotAClue.Web;
using NotAClue.Web.DynamicData;
using System.IO;

namespace $rootnamespace$
{
    public partial class UploadFile_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {
        private UploadAttribute uploadAttribute;

        protected void Page_Load(object sender, EventArgs e)
        {
            CustomValidator1.Text = "*";
            SetUpValidator(CustomValidator1);

            // get attributes
            uploadAttribute = MetadataAttributes.GetAttribute<UploadAttribute>();
            if (uploadAttribute == null)
            {
                // no attribute thrw an error
                throw new InvalidOperationException("FileUpload must have valid uploadAttribute applied");
            }
            else
            {
                // add tooltip describing what file types can be uploaded.
                FileUpload1.ToolTip = String.Format("Upload {0} files", uploadAttribute.FileTypes.ToCsvString());
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (FileUpload1.HasFile)
            {
                // get files name
                var fileName = FileUpload1.FileName;

                // get files extension without the dot
                String fileExtension = FileUpload1.GetFileExtension();

                // check file has an allowed file extension
                if (!uploadAttribute.FileTypes.Contains(fileExtension))
                {
                    args.IsValid = false;
                    CustomValidator1.ErrorMessage = String.Format("{0} is not a valid upload file type (only {1} are supported).",
                        FileUpload1.FileName,
                        uploadAttribute.FileTypes.ToCsvString());
                }
            }
            else if (Column.IsRequired && String.IsNullOrEmpty(Label1.Text))
            {
                args.IsValid = false;
                CustomValidator1.ErrorMessage = Column.RequiredErrorMessage;
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            //check if field has a value
            if (FieldValue == null)
                return;

            // when there is already a value in the FieldValue
            // then show the icon and label/hyperlink
            PlaceHolder1.Visible = true;

            // get the file extension
            String extension = FieldValueString.GetFileExtension();

            if (uploadAttribute.ShowHyperlink)
            {
                Label1.Visible = false;
                // open in new window
                HyperLink1.Target = "_blank";
                HyperLink1.Text = FieldValueString.GetFileNameTitle();
                HyperLink1.NavigateUrl = VirtualPathUtility.AppendTrailingSlash(uploadAttribute.UploadFolder) 
                    + FieldValueString;
            }
            else
            {
                HyperLink1.Visible = false;
                Label1.Text = FieldValueString;
            }

            // show the icon
            if (!String.IsNullOrEmpty(extension))
            {
                // set the file type image
                if (!String.IsNullOrEmpty(uploadAttribute.ImagesFolder))
                {
                    // get file type image from folder specified in
                    Image1.ImageUrl = String.Format("{0}{1}.{2}",
                        VirtualPathUtility.AppendTrailingSlash(uploadAttribute.ImagesFolder),
                        extension,
                        uploadAttribute.ImageExtension);
                }

                Image1.AlternateText = extension + " file";

                // set width
                if (uploadAttribute.Width > 0)
                    Image1.Width = uploadAttribute.Width;

                // set height
                if (uploadAttribute.Height > 0)
                    Image1.Height = uploadAttribute.Height;
            }
            else
            {
                // if file has no extension then hide image
                Image1.Visible = false;
            }
        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            // make sure file is valid
            if (FileUpload1.HasFile && Page.IsValid)
            {
                // make sure we have the folder to upload the file to
                var uploadFolder = Server.MapPath(VirtualPathUtility.AppendTrailingSlash(uploadAttribute.UploadFolder));
                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                // upload the file
                FileUpload1.SaveAs(uploadFolder + FileUpload1.FileName);

                // update the field with the filename
                dictionary[Column.Name] = ConvertEditedValue(FileUpload1.FileName);
            }
        }

        public override Control DataControl
        {
            get { return FileUpload1; }
        }
    }
}
