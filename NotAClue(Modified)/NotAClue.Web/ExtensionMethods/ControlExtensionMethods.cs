using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
//using NotAClue.ComponentModel.DataAnnotations;
using NotAClue.Web;
using System.Web.UI.HtmlControls;
using System.Collections.ObjectModel;

namespace NotAClue.Web
{
    public static class ControlExtensionMethods
    {
        /// <summary>
        /// Flattens hierarchy of child controls.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IEnumerable<Control> FlattenChildren(this Control control)
        {
            var children = control.Controls.Cast<Control>();
            return children.SelectMany(c => FlattenChildren(c)).Concat(children);
        }

        /// <summary>
        /// Gets all child controls recursively.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IEnumerable<T> GetAllChildControlsRecursive<T>(this Control control)
        {
            return control.FlattenChildren().OfType<T>();
        }

        /// <summary>
        /// Gets the file extension.
        /// </summary>
        /// <param name="fileUpload">The file upload.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static String GetFileExtension(this FileUpload fileUpload)
        {
            return fileUpload.FileName.Substring(fileUpload.FileName.LastIndexOf(".") + 1).ToLower();
        }


        /// <summary>
        /// Makes sure focus is still on this control after update.
        /// </summary>
        /// <param name="webControl">The web control.</param>
        public static void SetFocusHere(this WebControl webControl)
        {
            // get reference to the pages script manager
            var scriptManagerRef = ScriptManager.GetCurrent(webControl.Page);

            // set focus to the passed in web control
            if (scriptManagerRef.EnablePartialRendering)
                scriptManagerRef.SetFocus(webControl);
            else
                webControl.Page.SetFocus(webControl);
        }

        /// <summary>
        /// Get the control by searching recursively for it.
        /// </summary>
        /// <param name="Root">The control to start the search at.</param>
        /// <param name="Id">The ID of the control to find</param>
        /// <returns>The control the was found or NULL if not found</returns>
        public static Control FindControlRecursive(this Control Root, string Id)
        {
            if (Root.ID == Id)
                return Root;

            foreach (Control Ctl in Root.Controls)
            {
                Control FoundCtl = FindControlRecursive(Ctl, Id);

                if (FoundCtl != null)
                    return FoundCtl;
            }
            return null;
        }

        /// <summary>
        /// Finds the control recursive.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root">The root.</param>
        /// <returns></returns>
        public static T FindControlRecursive<T>(this Control root) where T : Control
        {
            var control = root as T;
            if (control != null)
                return control;

            foreach (Control Ctl in root.Controls)
            {
                T FoundCtl = Ctl.FindControlRecursive<T>();

                if (FoundCtl != null)
                    return FoundCtl;
            }
            return null;
        }

        /// <summary>
        /// Finds the link button recursive.
        /// </summary>
        /// <param name="Root">The root.</param>
        /// <param name="commandName">Name of the command.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IButtonControl FindIButtonControlRecursive(this Control Root, string commandName)
        {
            var iButtonCtrl = Root as IButtonControl;
            if (iButtonCtrl != null && iButtonCtrl.CommandName == commandName)
                return iButtonCtrl;

            foreach (Control ctrl in Root.Controls)
            {
                var FoundCtl = FindIButtonControlRecursive(ctrl, commandName);

                if (FoundCtl != null)
                    return FoundCtl;
            }
            return null;
        }

        /// <summary>
        /// Finds the control recursive.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root">The root.</param>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public static T FindControlRecursive<T>(this Control root, string Id) where T : Control
        {
            var control = root as T;
            if (control != null && root.ID == Id)
                return control;

            foreach (Control Ctl in root.Controls)
            {
                T FoundCtl = Ctl.FindControlRecursive<T>(Id);

                if (FoundCtl != null)
                    return FoundCtl;
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

        /// <summary>
        /// Gets the parent control of type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public static T GetParent<T>(this Control control) where T : Control
        {
            var parentControl = control.Parent;
            while (parentControl != null)
            {
                var currentParentControl = parentControl as T;
                if (currentParentControl != null)
                    return currentParentControl;
                else
                    parentControl = parentControl.Parent;
            }
            return null;
        }
    }
}
