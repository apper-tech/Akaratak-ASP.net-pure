using DynamicDataLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.DynamicData;
using System.Web.UI;
using NotAClue.Web.DynamicData;

namespace DynamicDataLibrary
{
    public static class HideBasedOnDependeeValue
    {
        public static IValueChangable GetDependeeField(this Control control, string dependeeName)
        {
            var controlFound = control.FindDynamicControlRecursive(dependeeName);
            var dependeeDynamicControl = controlFound as DynamicControl;

            IValueChangable dependeeField = null;

            // setup the selection event
            if (dependeeDynamicControl != null)
                dependeeField = dependeeDynamicControl.Controls[0] as IValueChangable;

            return dependeeField;
        }

        public static QueryableFilterUserControl GetQueryableFilter(this Control control, string dependeeName)
        {
            var controlFound = control.FindDynamicFilterRecursive(dependeeName);
            var dependeeDynamicFilter = controlFound as DynamicFilter;

            QueryableFilterUserControl dependeeField = null;
            if (dependeeDynamicFilter != null)
                dependeeField = dependeeDynamicFilter.Controls[0] as QueryableFilterUserControl;

            return dependeeField;
        }
        public static void HandleDependant(this Control control, MetaTable table)
        {
            //Hide Dependant Based on Dependee Value
            foreach (MetaColumn col in table.Columns)
            {
                HideBasedOnDependeeValueAttribute hideBasedOnDependee = col.GetAttribute<HideBasedOnDependeeValueAttribute>();
                if (hideBasedOnDependee != null)
                {
                    string dependeeName = hideBasedOnDependee.DependeeName;
                    string[] dependeeValuesToHideBasedOn = hideBasedOnDependee.DependeeValues;

                    IValueChangable valueChangable = control.GetDependeeField(dependeeName);
                    DynamicControl dynamicControl = (control.FindDynamicControlRecursive(col.Name) as DynamicControl);

                    if (dynamicControl != null && valueChangable!=null)
                        dynamicControl.Parent.Visible = !dependeeValuesToHideBasedOn.Contains(valueChangable.Value);
                    if (valueChangable != null)
                        valueChangable.AutoPostBack = true;
                }
            }
        }
    }
}
