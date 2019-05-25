using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace DynamicDataLibrary
{
    public static class WebControlExtention
    {
        public static System.Web.UI.Control MyFindControl(this System.Web.UI.WebControls.TableCell cell, String controlId)
        {
            for (int i = 0; i < cell.Controls.Count; i++)
            {
                if (controlId == cell.Controls[i].ID)
                    return cell.Controls[i];
            }
            return null;
        }
        public static T GetParent<T>(this Control control) where T : Control
        {
            var parentControl = control.Parent;
            while (parentControl != null)
            {
                var formView = parentControl as T;
                if (formView != null)
                    return formView;
                else
                    parentControl = parentControl.Parent;
            }
            return null;
        }
        public static Type FindControlRecursive<Type>(this Control control)
            where Type : Control
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.GetType() == typeof(Type))
                {
                    return (Type)childControl;
                }
                else
                {
                    return FindControlRecursive<Type>(childControl);
                }
            }
            return null;
        }
        public static Type FindControlRecursive<Type>(this Control control, string id)
            where Type : Control
        {
            if (control.ID == id || control.ClientID == id) return (Type)control;
            foreach (Control item in control.Controls)
            {
                Control t = FindControlRecursive<Type>(item, id);
                if (t != null) return (Type)t;
            }
            return null;
        }
    }
}
