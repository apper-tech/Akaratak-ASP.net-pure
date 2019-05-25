using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Security.Permissions;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;

namespace NotAClue.Web.UI.WebControls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [Designer("NotAClue.Web.UI.WebControls.PopupClientDesigner, NotAClue.Web.UI.WebControls")]
    [ToolboxData("<{0}:PopupClient runat=\"server\"></{0}:PopupClient>")]
    public class PopupClient : WebControl
    {
        private String returnValuesControl;
        //private const String QUERY_STRING = "return";

        //public String ClientButton { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        [Browsable(true)]
        [Bindable(true)]
        [Localizable(false)]
        [Category("Appearance")]
        [Description("Gets or sets the return values")]
        public String ReturnValues { get; set; }

        /// <summary>
        /// Gets or sets the query string field.
        /// </summary>
        /// <value>The query string field.</value>
        [Browsable(true)]
        [Bindable(true)]
        [Localizable(false)]
        [Category("Appearance")]
        [Description("Gets or sets the QueryString Field string")]
        [DefaultValue("ReturnValue")]
        public String QueryStringField { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PopupClient"/> class.
        /// </summary>
        public PopupClient()
        {
            QueryStringField = "ReturnValue";
        }

        protected override void OnInit(EventArgs e)
        {
            // get return values control
            try
            {
                returnValuesControl = Page.Request.QueryString[QueryStringField];
            }
            catch (Exception)
            {
                returnValuesControl = String.Empty;
            }
            base.OnInit(e);
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            //base.RenderBeginTag(writer);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            // hidden field
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            writer.AddAttribute(HtmlTextWriterAttribute.Value, ReturnValues);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            StringBuilder script = new StringBuilder();
            script.Append("\n<script language=\"JavaScript\" type=\"text/javascript\">\n");
            script.Append("    var value = document.getElementById(\"" + this.UniqueID + "\").value;\n");
            script.Append("    if(value != \"\") {\n");
            script.Append("        window.opener.document.getElementById(\"" + returnValuesControl + "\").value = value;\n");
            script.Append("        window.opener.__doPostBack('" + returnValuesControl + "', '');\n");
            script.Append("        window.close();\n");
            script.Append("    }\n");
            script.Append("\n</script>\n");

            writer.Write(script.ToString());

            //base.Render(writer);
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            //base.RenderEndTag(writer);
        }
    }
}
