using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace DynamicDataLibrary
{
    public class FieldTemplateRowGenerator : IAutoFieldGenerator
    {
        protected MetaTable _table;
        protected String[] _displayColumns;

        public FieldTemplateRowGenerator(MetaTable table)
        {
            _table = table;
        }

        public FieldTemplateRowGenerator(MetaTable table, String[] displayColumns)
        {
            _table = table;
            _displayColumns = displayColumns;
        }

        //public ICollection GenerateFields(Control control)
        //{
        //    List<DynamicField> oFields = new List<DynamicField>();

        //    foreach (var column in _table.Columns)
        //    {
        //        // carry on the loop at the next column  
        //        // if scaffold table is set to false or DenyRead
        //        if (!column.Scaffold)
        //            continue;

        //        if (_displayColumns != null && !_displayColumns.Contains(column.Name))
        //            continue;

        //        DynamicField f = new DynamicField();

        //        f.DataField = column.Name;
        //        f.ApplyFormatInEditMode = true;
        //        oFields.Add(f);
        //    }
        //    return oFields;
        //}

        public ICollection GenerateFields(Control control)
        {
            Dictionary<DynamicField, int> oFields = new Dictionary<DynamicField, int>();

            foreach (var column in _table.Columns)
            {
                // carry on the loop at the next column  
                // if scaffold table is set to false or DenyRead
                if (!column.Scaffold)
                    continue;
                if (_displayColumns != null && !_displayColumns.Contains(column.Name))
                    continue;


                int order = 0;
                DisplayAttribute disAtt = column.Attributes.OfType<DisplayAttribute>().FirstOrDefault();
                if (disAtt != null)
                    try
                    {
                        order = disAtt.Order;
                    }
                    catch (InvalidOperationException)
                    {
                        //Catch Possible Exception: The Order property has not been set.  Use the GetOrder method to get the value.

                    }

                DynamicField f = new DynamicField();
                f.DataField = column.Name;
                f.ApplyFormatInEditMode = true;
                f.ValidationGroup = _table + "_Edit";

                oFields.Add(f, order);
            }
            return oFields.OrderBy(d => d.Value).Select(d => d.Key).ToList();
        }
    }
}