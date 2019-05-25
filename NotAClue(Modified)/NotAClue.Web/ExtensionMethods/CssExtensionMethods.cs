using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace NotAClue.Web
{
    public static class CssExtensionMethods
    {
        /// <summary>
        /// Sets the CSS class.
        /// </summary>
        /// <param name="htmlControl">The HTML control.</param>
        /// <param name="cssClass">The CSS class.</param>
        public static void SetCssClass(HtmlControl htmlControl, String cssClass)
        {
            if (!String.IsNullOrWhiteSpace(htmlControl.Attributes["class"]))
                htmlControl.Attributes["class"] += " " + cssClass;
            else
                htmlControl.Attributes.Add("class", cssClass);
        }

        /// <summary>
        /// Sets the CSS class.
        /// </summary>
        /// <param name="webControl">The web control.</param>
        /// <param name="cssClass">The CSS class.</param>
        public static void SetCssClass(this WebControl webControl, String cssClass)
        {
            if (!String.IsNullOrEmpty(webControl.CssClass))
                webControl.CssClass = " " + cssClass;
            else
                webControl.CssClass = cssClass;
        }

        /// <summary>
        /// Sets the style of a table html control.
        /// </summary>
        /// <param name="htmlControl">The table cell.</param>
        /// <param name="attribute">The property.</param>
        /// <param name="value">The value.</param>
        public static void SetAttributeClass(this HtmlControl htmlControl, String attribute, String value)
        {
            if (htmlControl.Attributes[attribute] != null)
                htmlControl.Attributes[attribute] = value;
            else
                htmlControl.Attributes.Add(attribute, value);
        }

        /// <summary>
        /// Sets the style of a table web control.
        /// </summary>
        /// <param name="webControl">The table cell.</param>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        public static void SetStyle(this WebControl webControl, String property, String value)
        {
            if (webControl.Style[property] != null)
                webControl.Style[property] = value;
            else
                webControl.Style.Add(property, value);
        }
    }
}
