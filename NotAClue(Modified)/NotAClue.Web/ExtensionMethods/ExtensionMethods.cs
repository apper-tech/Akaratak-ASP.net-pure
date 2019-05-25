using System;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.DynamicData;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NotAClue.Web
{
    public static class ExtansionMethods
    {
        /// <summary>
        /// Sets the HTML table row class.
        /// </summary>
        /// <param name="tableRow">The table row.</param>
        /// <param name="cssClass">The CSS class.</param>
        public static void SetHtmlTableRowClass(this HtmlTableRow tableRow, String cssClass)
        {
            if (!String.IsNullOrWhiteSpace(tableRow.Attributes["class"]))
                tableRow.Attributes["class"] += " " + cssClass;
            else
                tableRow.Attributes.Add("class", cssClass);
        }

        /// <summary>
        /// Crops the bitmap.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="cropX">The crop X.</param>
        /// <param name="cropY">The crop Y.</param>
        /// <param name="cropWidth">Width of the crop.</param>
        /// <param name="cropHeight">Height of the crop.</param>
        /// <returns></returns>
        public static Bitmap CropBitmap(this Bitmap bitmap, int cropX, int cropY, int cropWidth, int cropHeight)
        {
            // create new rectangle
            Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
            // clone bitmap into smaller rectangle
            Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
            // return cropped bitmap
            return cropped;
        }

        /// <summary>
        /// Times the taken.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static String TimeTaken(this DateTime time)
        {
            TimeSpan timeTaken = DateTime.Now.Subtract(time);
            return String.Format("{0}{1}ms", timeTaken.Hours, timeTaken.Milliseconds);
        }

        public static String GetDisplayFormat(this Type type)
        {
            string defaultFormat = "{0}";

            if (type == typeof(DateTime) || type == typeof(Nullable<DateTime>))
            {
                defaultFormat = "{0:d}";
            }
            else if (type == typeof(decimal) || type == typeof(Nullable<decimal>))
            {
                defaultFormat = "{0:c}";
            }

            return defaultFormat;
        }

        /// <summary>
        /// Adds the script to head.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="scriptName">Name of the script.</param>
        public static void AddScriptToHead(this Page page, String scriptName)
        {
            var scriptPath = String.Format("~/javascript/{0}.js", scriptName);

            var includePresent = page.Header.FindControl(scriptName);
            if (includePresent == null)
            {
                // create the style sheet control and put it in the document header
                var linkRef = "<script src=\"{0}\" type=\"text/javascript\"></script>";
                LiteralControl include = new LiteralControl(String.Format(linkRef, page.ResolveClientUrl(scriptPath)));
                include.ID = scriptName;
                page.Header.Controls.Add(include);
            }
        }

        /// <summary>
        /// Add a client script or css file to the header once per page
        /// </summary>
        /// <param name="page">of type System.Web.UI.Page</param>
        /// <param name="scriptFile">path to script/stylesheet file</param>
        /// <param name="scriptType">An enum of the file types supported</param>
        public static void AddStyleToHead(this Page page, String scriptFile)
        {
            //var controlId = page.GetType().Name + "." + scriptFile.GetFileName();
            var controlId = scriptFile.GetFileNameTitle();
            var includePresent = page.Header.FindControl(controlId);
            if (includePresent == null)
            {
                //<link href="../css/style.css" rel="stylesheet" type="text/css" />
                var link = new HtmlLink();
                link.ID = controlId;
                link.Href = page.ResolveUrl(scriptFile);
                link.Attributes.Add("rel", "stylesheet");
                link.Attributes.Add("type", "text/css");
                page.Header.Controls.Add(link);
            }
        }

        /// <summary>
        /// Add a client script or css file to the header once per page
        /// </summary>
        /// <param name="page">of type System.Web.UI.Page</param>
        /// <param name="scriptFile">path to script/stylesheet file</param>
        public static void AddClientScripToHead(this Page page, String scriptFile)
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
    }
}