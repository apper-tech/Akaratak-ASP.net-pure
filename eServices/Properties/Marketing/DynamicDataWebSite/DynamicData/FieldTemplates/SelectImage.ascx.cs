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
    public partial class SelectImage : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            // get attributes
            var uploadAttribute = MetadataAttributes.GetAttribute<UploadAttribute>();
            if (uploadAttribute == null)
                throw new InvalidOperationException("FileUpload must have valid uploadAttribute applied");

            var extension = FieldValueString.GetFileExtension();
            Image1.ImageUrl = String.Format("{0}{1}", VirtualPathUtility.AppendTrailingSlash(uploadAttribute.ImagesFolder), FieldValueString);
            Image1.BorderStyle = BorderStyle.None;
            Image1.AlternateText = FieldValueString.GetFileNameTitle();
            Image1.Attributes.Add("title", FieldValueString.GetFileNameTitle());

            // set width
            if (uploadAttribute.Width > 0)
                Image1.Width = uploadAttribute.Width;

            // set height
            if (uploadAttribute.Height > 0)
                Image1.Height = uploadAttribute.Height;

        }

        public override Control DataControl
        {
            get { return Image1; }
        }
    }
}
