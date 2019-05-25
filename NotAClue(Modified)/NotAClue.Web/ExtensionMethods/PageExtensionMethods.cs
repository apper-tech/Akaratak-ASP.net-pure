using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.DynamicData;

namespace NotAClue.Web
{
    public static class PageExtensionMethods
    {
        /// <summary>
        /// Sets the pop-up message.
        /// </summary>
        /// <param name="updatePanel">The page.</param>
        /// <param name="message">The message.</param>
        /// <remarks></remarks>
        public static void SetPopupMessage(this UpdatePanel updatePanel, String message)
        {
            var script = String.Format("alert('{0}');", message);
            ScriptManager.RegisterStartupScript(updatePanel, updatePanel.GetType(), "NAC_UpdateAlert", script, true);
        }

        /// <summary>
        /// Sets the pop-up message.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="message">The message.</param>
        /// <remarks></remarks>
        public static void SetPopupMessage(this Page page, String message)
        {
            var csm = page.ClientScript;
            var script = String.Format("window.onload = function () {{ alert('{0}');}}", message);
            csm.RegisterStartupScript(page.GetType(), "NAC_alert", script, true);
        }

        /// <summary>
        /// Disables the partial render.
        /// </summary>
        /// <param name="page">The page.</param>
        public static void DisablePartialRender(this Page page)
        {
            var sm = ScriptManager.GetCurrent(page);
            if (sm != null)
                sm.EnablePartialRendering = false;
        }

        /// <summary>
        /// Sets the favicon.
        /// </summary>
        /// <param name="page">The page.</param>
        public static void SetFavicon(this Page page)
        {
            //<link href="~/favicon.ico" rel="shortcut icon" type="image/vnd.microsoft.icon"/>
            var authority = page.Request.Url.Authority;
            var href = String.Format("http://{0}/favicon.ico", authority);
            var favicon = new HtmlLink();
            favicon.Href = href;
            favicon.Attributes.Add("rel", "shortcut icon");
            favicon.Attributes.Add("type", "image/vnd.microsoft.icon");
            page.Header.Controls.Add(favicon);
        }

        /// <summary>
        /// Adds the client script.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="key">The key.</param>
        /// <param name="script">The script.</param>
        public static void AddClientScript(this Page page, String key, String script)
        {
            // Define the name and type of the client scripts on the page.
            Type scriptType = page.GetType();

            // Get a ClientScriptManager reference from the Page class.
            ClientScriptManager csm = page.ClientScript;

            // Check to see if the Client Script Include is already registered.
            if (!csm.IsClientScriptBlockRegistered(scriptType, key))
            {
                // include main Flash Content script
                csm.RegisterClientScriptBlock(scriptType, key, script, true);
            }
        }

        /// <summary>
        /// Adds the CSS to head.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="styleSheetUrl">The style sheet URL.</param>
        public static void AddCssToHead(this Page page, String styleSheetUrl)
        {
            var styleSheetName = styleSheetUrl.Substring(styleSheetUrl.LastIndexOf("/") + 1, styleSheetUrl.Length - (styleSheetUrl.LastIndexOf("/") + 1));
            if (!page.HasStyleSheet(styleSheetName))
            {
                var link = new HtmlLink() { Href = styleSheetUrl };
                link.Attributes.Add("rel", "stylesheet");
                link.Attributes.Add("type", "text/css");
                page.Header.Controls.Add(link);
            }
        }

        /// <summary>
        /// Determines whether [has style sheet] [the specified page].
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="styleSheet">The style sheet.</param>
        /// <returns>
        /// 	<c>true</c> if [has style sheet] [the specified page]; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean HasStyleSheet(this Page page, String styleSheet)
        {
            var links = from l in page.Header.Controls.OfType<HtmlLink>()
                        where l.Href.Contains(styleSheet)
                        select l;

            return links.Count() > 0;
        }

        /// <summary>
        /// Add a client script or CSS file to the header once per page
        /// </summary>
        /// <param name="page">of type System.Web.UI.Page</param>
        /// <param name="scriptName">Name of the script.</param>
        /// <param name="clientScript">Client script content</param>
        /// <param name="addScriptTags">if set to <c>true</c> [add script tags].</param>
        public static void RegisterClientScript(this Page page, String scriptName, String clientScript, Boolean addScriptTags = false)
        {
            // Define the name and type of the client scripts on the page.
            Type scriptType = page.GetType();

            // Get a ClientScriptManager reference from the Page class.
            ClientScriptManager csm = page.ClientScript;

            // Check to see if the Client Script Include is already registered.
            if (!csm.IsClientScriptBlockRegistered(scriptType, clientScript))
                csm.RegisterClientScriptBlock(scriptType, scriptName, clientScript, addScriptTags);
        }

        /// <summary>
        /// Add a client script or CSS file to the header once per page
        /// </summary>
        /// <param name="page">of type System.Web.UI.Page</param>
        /// <param name="scriptFile">path to script file</param>
        public static void IncludeClientScrip(this Page page, String scriptFile)
        {
            // Define the name and type of the client scripts on the page.
            string scriptName = scriptFile.GetFileName();
            Type scriptType = page.GetType();

            // Get a ClientScriptManager reference from the Page class.
            ClientScriptManager csm = page.ClientScript;

            // Check to see if the Client Script Include is already registered.
            if (!csm.IsClientScriptIncludeRegistered(scriptType, scriptName))
            {
                // include main Flash Content script
                string urlJS = page.ResolveUrl(scriptFile);
                csm.RegisterClientScriptInclude(scriptType, scriptName, urlJS);
            }
        }

        /// <summary>
        /// Add a client script or css file to the header once per page
        /// </summary>
        /// <param name="page">of type System.Web.UI.Page</param>
        /// <param name="styleSheet">path to style sheet file</param>
        /// <param name="scriptType">An enum of the file types supported</param>
        public static void AddStyleToHead(this Page page, String styleSheet)
        {
            //var controlId = page.GetType().Name + "." + scriptFile.GetFileName();
            var controlId = styleSheet.GetFileNameTitle();
            var includePresent = page.Header.FindControl(controlId);
            if (includePresent == null)
            {
                //<link href="../css/style.css" rel="stylesheet" type="text/css" />
                var link = new HtmlLink();
                link.ID = controlId;
                link.Href = page.ResolveUrl(styleSheet);
                link.Attributes.Add("rel", "stylesheet");
                link.Attributes.Add("type", "text/css");
                page.Header.Controls.Add(link);
            }
        }

        /// <summary>
        /// Gets the path seperator.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static String GetPathSeperator(this String fileName)
        {
            String pathSeperator;

            if (fileName.Contains("\\"))
                pathSeperator = "\\";
            else
                pathSeperator = "/";
            return pathSeperator;
        }
    }
}
