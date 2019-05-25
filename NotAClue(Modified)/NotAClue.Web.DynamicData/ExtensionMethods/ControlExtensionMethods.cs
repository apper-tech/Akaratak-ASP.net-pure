using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.DynamicData;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace NotAClue.Web.DynamicData
{
    public static partial class ControlExtensionMethods
    {
        /// <summary>
        /// Get the CascadingFilterTemplate by searching recursively for it by DataField.
        /// </summary>
        /// <param name="Root">The control to start the search at.</param>
        /// <param name="Id">The DataField of the control to find</param>
        /// <returns>The found control or NULL if not found</returns>
        public static ICascadingControl FindCascadingControl(this Control root, string columnName)
        {
            var dc = root as ICascadingControl;
            if (dc != null)
            {
                if (String.Compare(dc.ChildColumn.Name, columnName, true) == 0)
                    return dc;
            }

            foreach (Control Ctl in root.Controls)
            {
                ICascadingControl FoundCtl = FindCascadingControl(Ctl, columnName);

                if (FoundCtl != null)
                    return FoundCtl;
            }
            return null;
        }

        /// <summary>
        /// Gets the Parent control in a cascade of controls
        /// </summary>
        /// <returns>An the parent control or null</returns>
        public static ICascadingControl GetParentControl<T>(this Control control, String controlName) where T : Control
        {
            if (controlName != null)
            {
                // get the parent container
                var parentDataControl = control.GetContainerControl<T>();

                // get the parent container
                if (parentDataControl != null)
                    return parentDataControl.FindCascadingControl(controlName)
                        as ICascadingControl;
            }
            return null;
        }

        /// <summary>
        /// Get the Data Control containing the FiledTemplate
        /// usually a DetailsView or FormView
        /// </summary>
        /// <param name="control">
        /// Use the current field template as a starting point
        /// </param>
        /// <returns>
        /// A FilterRepeater the control that 
        /// contains the current control
        /// </returns>
        public static T GetContainerControl<T>(this Control control) where T : Control
        {
            var parentControl = control.Parent;
            while (parentControl != null)
            {
                var p = parentControl as T;
                if (p != null)
                    return p;
                else
                    parentControl = parentControl.Parent;
            }
            return null;
        }
    }
}