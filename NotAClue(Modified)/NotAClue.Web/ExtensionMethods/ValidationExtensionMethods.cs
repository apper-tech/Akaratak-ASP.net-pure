using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotAClue.Web
{
    public static class ValidationExtensionMethods
    {
        //public static void ShowAsValidationError(this Page page, String message, String validationGroup = "", String cssClass = "DDControl DDValidator")
        //{
        //    var cv = new CustomValidator()
        //    {
        //        ErrorMessage = message,
        //        Text = " *",
        //        IsValid = false,
        //        CssClass = cssClass
        //    };
        //    if (!String.IsNullOrWhiteSpace(validationGroup))
        //        cv.ValidationGroup = validationGroup;

        //    page.Validators.Add(cv);
        //}

        public static void ShowAsValidationError(this Page page, String message, String controlId = "", String validationGroup = "", String cssClass = "DDControl DDValidator")
        {
            Control control = null;
            if (!String.IsNullOrWhiteSpace(controlId))
                control = page.FindControlRecursive(controlId);

            var cv = new CustomValidator()
            {
                ErrorMessage = message,
                Text = " *",
                IsValid = false,
                CssClass = cssClass
            };

            if (control != null)
            {
                var parent = control.Parent;
                var index = parent.Controls.IndexOf(control);
                parent.Controls.AddAt(index, control);
            }

            if (!String.IsNullOrWhiteSpace(validationGroup))
                cv.ValidationGroup = validationGroup;

            page.Validators.Add(cv);
        }
    }
}
