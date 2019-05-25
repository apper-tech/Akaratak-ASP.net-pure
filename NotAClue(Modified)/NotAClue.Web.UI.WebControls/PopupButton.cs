using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotAClue.Web.UI.WebControls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [DefaultEvent("TextChanged")]
    [ToolboxData("<{0}:PopupButton runat=\"server\"></{0}:PopupButton>")]
    public class PopupButton : Button, IPostBackDataHandler
    {
        private String linkButtonId;

        private static readonly object EventTextChanged = new object();

        [Category("Action")]
        [Description("Raised when text changes")]
        public event EventHandler TextChanged
        {
            add
            {
                Events.AddHandler(EventTextChanged, value);
            }
            remove
            {
                Events.RemoveHandler(EventTextChanged, value);
            }
        }

        [Browsable(true)]
        [Bindable(true)]
        [Localizable(false)]
        [Category("Appearance")]
        [DefaultValue(300)]
        public int WindowWidth { get; set; }

        [Browsable(true)]
        [Bindable(true)]
        [Localizable(false)]
        [Category("Appearance")]
        [DefaultValue(300)]
        public int WindowHeight { get; set; }

        /// <summary>
        /// Gets or sets the popup window's title.
        /// </summary>
        /// <value>The popup window title.</value>
        [Browsable(true)]
        [Bindable(true)]
        [Localizable(false)]
        [Category("Appearance")]
        public String Title { get; set; }

        [Browsable(true)]
        [Bindable(true)]
        [Localizable(false)]
        [Category("Appearance")]
        public String JavaScriptFolder { get; set; }

        [Browsable(true)]
        [Bindable(true)]
        [Localizable(false)]
        [Category("Appearance")]
        public String CssClass { get; set; }

        [Browsable(true)]
        [Bindable(true)]
        [Localizable(false)]
        [Category("Appearance")]
        public String PostBackUrl { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        [Browsable(true)]
        [Bindable(true)]
        [Localizable(false)]
        [Category("Appearance")]
        [Description("The values pssed back from the popup.")]
        public String ReturnValues
        {
            get
            {
                var s = (String)ViewState["Values"];
                return (String.IsNullOrEmpty(s) ? String.Empty : s);
            }
            set
            {
                ViewState["Values"] = value;
            }
        }

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
        /// Gets or sets the additional query string.
        /// </summary>
        /// <value>The additional query string.</value>
        [Browsable(true)]
        [Bindable(true)]
        [Localizable(false)]
        [Category("Appearance")]
        [Description("Gets or sets the AdditionalQueryString Field string")]
        public String AdditionalQueryString { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [scroll bars].
        /// </summary>
        /// <value><c>true</c> if [scroll bars]; otherwise, <c>false</c>.</value>
        public Boolean ScrollBars { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PopupButton"/> class.
        /// </summary>
        public PopupButton()
        {
            QueryStringField = "ReturnValue";
            WindowHeight = 300;
            WindowWidth = 300;
            ScrollBars = false;
        }

        protected override void OnInit(EventArgs e)
        {
            linkButtonId = this.UniqueID + "_Button";

            // Define the name and type of the client scripts on the page.
            string scriptName = "NotAClue.Web.UI.WebControls.ResourceFiles.NAC_OpenPopup.js";
            Type scriptType = this.GetType();

            // Get a ClientScriptManager reference from the Page class.
            ClientScriptManager csm = Page.ClientScript;

            // Check to see if the Client Script Include is already registered.
            if (!csm.IsClientScriptIncludeRegistered(scriptType, scriptName))
            {
                // include main Flash Content script
                string urlJS = csm.GetWebResourceUrl(scriptType, scriptName);
                csm.RegisterClientScriptInclude(scriptType, scriptName, urlJS);
            }

            base.OnInit(e);
        }

        //protected override void OnPreRender(EventArgs e)
        //{
        //    base.OnPreRender(e);
        //}

        //public override void RenderBeginTag(HtmlTextWriter writer)
        //{
        //    //base.RenderBeginTag(writer);
        //}

        protected override void Render(HtmlTextWriter writer)
        {
            //base.Render(writer);
            //declare the function and variable names
            var popupId = this.UniqueID.Replace("$", "").Replace("_", "");
            var functionName = "NAC_" + popupId + "Func";
            var variableName = "NAC_" + popupId + "Var";
            var scrollBars = ScrollBars ? "yes" : "no";

            StringBuilder script = new StringBuilder();
            script.Append("\n<script language=\"JavaScript\" type=\"text/javascript\">\n");
            script.Append("function " + functionName + "() {\n");
            script.Append("var " + variableName + " = NAC_OpenPopup('" +
                PostBackUrl + "', '" +
                QueryStringField + "', '" +
                this.UniqueID + "', '" +
                AdditionalQueryString + "', '" +
                Title + "', '" +
                WindowWidth + "', '" +
                WindowHeight + "', '" +
                scrollBars + "')\n");
            //script.Append("while (!" + variableName + ".closed) { }\n");
            //script.Append(Page.ClientScript.GetPostBackEventReference(this, "") + ";\n");
            script.Append("}\n");
            script.Append("\n</script>\n");

            writer.Write(script.ToString());

            // render button
            writer.AddAttribute(HtmlTextWriterAttribute.Id, linkButtonId);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, linkButtonId);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");

            // set CSS styling
            if (!String.IsNullOrEmpty(CssClass))
                writer.AddAttribute(HtmlTextWriterAttribute.Class, CssClass);

            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "javascript:" + functionName + "();");
            writer.RenderBeginTag(HtmlTextWriterTag.Button);
            writer.Write(Text);
            writer.RenderEndTag();

            // hidden field
            //<input type="hidden" name="HiddenField1" id="HiddenField1" />
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }

        //protected override void RenderContents(HtmlTextWriter writer)
        //{
        //}

        //public override void RenderEndTag(HtmlTextWriter writer)
        //{
        //    //base.RenderEndTag(writer);
        //}

        #region IPostBackDataHandler Members

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            ReturnValues = postCollection[this.UniqueID];
            return true;
        }

        public void RaisePostDataChangedEvent()
        {
            OnTextChanged(EventArgs.Empty);
        }

        protected virtual void OnTextChanged(EventArgs e)
        {
            EventHandler textChangedHandler = (EventHandler)Events[EventTextChanged];
            if (textChangedHandler != null)
                textChangedHandler(this, e);
        }

        #endregion
    }
}