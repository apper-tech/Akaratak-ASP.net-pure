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
using NotAClue.ComponentModel.DataAnnotations;
using NotAClue.Web.DynamicData;
using NotAClue.Web;

namespace DynamicDataWebSite
{
    public partial class UploadImage : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            // get attributes
            var uploadAttribute = Column.GetAttributeOrDefault<UploadAttribute>();
            if (uploadAttribute == null)
                throw new InvalidOperationException("FileUpload must have valid uploadAttribute applied");

            var extension = FieldValueString.GetFileExtension();
            var imagePath = String.Format("{0}{1}", VirtualPathUtility.AppendTrailingSlash(uploadAttribute.UploadFolder), FieldValueString);
            var Image1 = new Image()
                {
                    ImageUrl = imagePath,
                    AlternateText = FieldValueString,
                    BorderStyle = BorderStyle.None
                };

            // set width
            if (uploadAttribute.Width > 0)
                Image1.Width = uploadAttribute.Width;

            // set height
            if (uploadAttribute.Height > 0)
                Image1.Height = uploadAttribute.Height;

            var text = new Literal() { Text = "&nbsp;" + FieldValueString.GetFileNameTitle() };

            if (uploadAttribute.ShowHyperlink)
            {
                var hyperlink = new HyperLink()
                {
                    NavigateUrl = VirtualPathUtility.AppendTrailingSlash(uploadAttribute.UploadFolder) + FieldValueString,
                    Target = "_blank"
                };

                hyperlink.Controls.Add(Image1);
                hyperlink.Controls.Add(text);

                PlaceHolder1.Controls.Add(hyperlink);
            }
            else
            {
                PlaceHolder1.Controls.Add(Image1);
                PlaceHolder1.Controls.Add(text);
            }
        }

        public override Control DataControl
        {
            get { return PlaceHolder1; }
        }
    }
}
