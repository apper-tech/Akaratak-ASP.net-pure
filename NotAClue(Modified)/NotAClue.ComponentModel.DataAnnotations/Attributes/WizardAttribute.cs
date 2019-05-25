using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace NotAClue.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class WizardAttribute : Attribute
    {
        private const string CSS_CLASS = "DDWizard";
        private const string SIDE_BAR_CSS_CLASS = "DDSideBar";
        private const string HEADER_CSS_CLASS = "DDHeader";
        private const string STEP_CSS_CLASS = "DDStep";
        private const string NAVIGATION_CSS_CLASS = "DDNavigation";

        /// <summary>
        /// Gets or sets a value indicating whether [show side bar].
        /// </summary>
        /// <value><c>true</c> if [show side bar]; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public Boolean ShowSideBar { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show cancel button].
        /// </summary>
        /// <value><c>true</c> if [show cancel button]; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public Boolean ShowCancelButton { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>The CSS class.</value>
        /// <remarks></remarks>
        public String CssClass { get; set; }

        /// <summary>
        /// Gets or sets the side bar CSS class.
        /// </summary>
        /// <value>The side bar CSS class.</value>
        /// <remarks></remarks>
        public String SideBarCssClass { get; set; }

        /// <summary>
        /// Gets or sets the header CSS class.
        /// </summary>
        /// <value>The header CSS class.</value>
        /// <remarks></remarks>
        public String HeaderCssClass { get; set; }

        /// <summary>
        /// Gets or sets the step CSS class.
        /// </summary>
        /// <value>The step CSS class.</value>
        /// <remarks></remarks>
        public String StepCssClass { get; set; }

        /// <summary>
        /// Gets or sets the navigation CSS class.
        /// </summary>
        /// <value>The navigation CSS class.</value>
        /// <remarks></remarks>
        public String NavigationCssClass { get; set; }

        public WizardAttribute()
        {
            CssClass = CSS_CLASS;
            SideBarCssClass = SIDE_BAR_CSS_CLASS;
            HeaderCssClass = HEADER_CSS_CLASS;
            StepCssClass = STEP_CSS_CLASS;
            NavigationCssClass = NAVIGATION_CSS_CLASS;

            // set defaults
            ShowSideBar = true;
            ShowCancelButton = true;
        }
    }
}