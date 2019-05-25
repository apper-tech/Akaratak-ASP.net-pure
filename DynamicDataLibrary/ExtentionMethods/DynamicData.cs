using DynamicDataLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataLibrary
{
    public static class DynamicData
    {
        public static Control FindDynamicControlRecursive(this Control root, string dataField)
        {
            var dc = root as DynamicControl;
            if (dc != null)
            {
                if (dc.DataField == dataField)
                    return dc;
            }

            foreach (Control Ctl in root.Controls)
            {
                Control FoundCtl = FindDynamicControlRecursive(Ctl, dataField);

                if (FoundCtl != null)
                    return FoundCtl;
            }
            return null;
        }
        public static Control FindDynamicControlRecursive(this Control root, Control type)
        {
            var dc = root as Control;
            if (dc != null)
            {
                if (dc.GetType() == type.GetType())
                    return dc;
            }

            foreach (Control Ctl in root.Controls)
            {
                Control FoundCtl = FindDynamicControlRecursive(Ctl, type);

                if (FoundCtl != null)
                    return FoundCtl;
            }
            return null;
        }

        public static Control FindDynamicFilterRecursive(this Control root, string dataField)
        {
            var df = root as DynamicFilter;
            if (df != null)
            {
                if (df.DataField == dataField)
                    return df;
            }

            foreach (Control Ctl in root.Controls)
            {
                Control FoundCtl = FindDynamicFilterRecursive(Ctl, dataField);

                if (FoundCtl != null)
                    return FoundCtl;
            }
            return null;
        }

        public static List<DynamicFilter> FindDynamicFiltersRecursive(this Control root)
        {
            List<DynamicFilter> daynamicFilters = new List<DynamicFilter>();
            var df = root as DynamicFilter;
            if (df != null)
            {
                daynamicFilters.Add(df);
                return daynamicFilters;
            }

            foreach (Control Ctl in root.Controls)
            {
                daynamicFilters.AddRange(FindDynamicFiltersRecursive(Ctl));

                //if (FoundCtl != null)
                //    return FoundCtl;
            }
            return daynamicFilters;
        }

        public static object GetCustomDefaultValue(this MetaColumn column, out bool enableValueChanging, out bool useOnEdit)
        {
            CustomDefaultValueAttribute defaultValueAtt = column.Attributes.OfType<CustomDefaultValueAttribute>().FirstOrDefault();
            if (defaultValueAtt != null)
            {
                enableValueChanging = defaultValueAtt.EnableValueChanging;
                useOnEdit = defaultValueAtt.UseOnEdit;
                return defaultValueAtt.Value;
            }
            else
            {
                enableValueChanging = true;
                useOnEdit = false;
                return null;
            }
        }


        public static bool TableAlreadyAddedAtParent(this Control control)
        {
            MetaTable currentTable = control.FindMetaTable();
            Control tempControl = control;
            bool startchecking = false;
            do
            {
                MetaTable parentTable = tempControl.FindMetaTable();
                if (parentTable != null && parentTable.Name != currentTable.Name)
                    startchecking = true;
                if (startchecking && parentTable == currentTable)
                    return true;
                tempControl = tempControl.Parent;
            } while (tempControl != null);

            return false;
        }

        public static bool IsColumnOfParent(this MetaColumn column, Control control)
        {
            bool isColumnOfParent = false;
            if (column is MetaForeignKeyColumn || column is MetaChildrenColumn)
            {
                MetaTable currentTable = column.FigureOutForeignOrChildTable();
                Control tempControl = control;
                bool startchecking = false;
                do
                {
                    MetaTable parentTable = tempControl.FindMetaTable();
                    if (parentTable != null && parentTable.Name != currentTable.Name)
                        startchecking = true;
                    if (startchecking && parentTable == currentTable)
                    {   //return true;
                        isColumnOfParent = true;
                        break;
                    }
                    tempControl = tempControl.Parent;
                } while (tempControl != null);

            }
            return isColumnOfParent;
        }
        public static MetaTable FigureOutForeignOrChildTable(this MetaColumn column)
        {
            var metaChildColumn = column as MetaChildrenColumn;
            var metaForeignKeyColumn = metaChildColumn != null ? metaChildColumn.ColumnInOtherTable as MetaForeignKeyColumn : null;

            var metaChildForeignKeyColumn = column as MetaForeignKeyColumn;

            //One to Many relation
            if (metaChildColumn != null && metaForeignKeyColumn != null)
            {
                // get an instance of the MetaTable
                //table = GridDataSource.GetTable();
                return metaChildColumn.ChildTable;
            }
            // One to One relation
            else if (metaChildForeignKeyColumn != null)
            {
                // get an instance of the MetaTable
                //table = GridDataSource.GetTable();
                return metaChildForeignKeyColumn.ParentTable;
            }
            else
            {
                return null;
            }
        }


        public static void AddWhereAndOrderByToDataSource(this FieldTemplateUserControl control, LinqDataSource linqDataSource,
            MetaTable table, HttpRequest request)
        {
            var row = EntityDataSourceHelper.GetItemObject(control.Row);

            var metaChildrenColumn = control.Column as MetaChildrenColumn;
            var metaChildForeignKeyColumn = control.Column as MetaForeignKeyColumn;

            string[] keyColumns = null;

            // get the association attributes associated with MetaChildrenColumns
            AssociationKeysAttribute keyAssociation = null;
            System.Data.Linq.Mapping.AssociationAttribute association = null;

            if (metaChildrenColumn != null)
            {
                keyColumns = (metaChildrenColumn.ColumnInOtherTable as MetaForeignKeyColumn).ForeignKeyNames.ToArray();
                keyAssociation = metaChildrenColumn.Attributes.OfType<AssociationKeysAttribute>().FirstOrDefault();
                association = metaChildrenColumn.Attributes.OfType<System.Data.Linq.Mapping.AssociationAttribute>().FirstOrDefault();
            }
            else if (metaChildForeignKeyColumn != null)
            {
                keyColumns = metaChildForeignKeyColumn.ParentTable.PrimaryKeyColumns.Select(c => c.Name).ToArray();
                keyAssociation = metaChildForeignKeyColumn.Attributes.OfType<AssociationKeysAttribute>().FirstOrDefault();
                association = metaChildForeignKeyColumn.Attributes.OfType<System.Data.Linq.Mapping.AssociationAttribute>().FirstOrDefault();
            }

            if (keyColumns != null && (association != null || keyAssociation != null))
            {
                // get keys ThisKey and OtherKey into Pairs
                var keys = new Dictionary<String, String>();
                var separator = new char[] { ',' };
                var thisKeys = keyAssociation != null ? keyAssociation.ThisKey.Split(separator) : association.ThisKey.Split(separator);
                var otherKeys = keyAssociation != null ? keyAssociation.OtherKey.Split(separator) : association.OtherKey.Split(separator);
                for (int i = 0; i < thisKeys.Length; i++)
                {
                    keys.Add(otherKeys[i], thisKeys[i]);
                }

                // setup the where clause 
                // support composite foreign keys
                linqDataSource.WhereParameters.Clear();
                foreach (String fkName in keyColumns)
                {
                    if (!keys.ContainsKey(fkName))
                    {
                        //continue;
                        throw new Exception("Someting worng with the relation! between: " + table.Name + " and " + fkName);
                    }

                    // get the current pk column
                    var fkColumn = table.GetColumn(fkName);

                    // setup parameter
                    var param = new Parameter();
                    param.Name = fkColumn.Name;
                    param.Type = fkColumn.TypeCode;

                    // get the PK value for this FK column using the fk pk pairs
                    //param.DefaultValue = request.QueryString[keys[fkName]];
                    if (row != null //&& param.DefaultValue == null
                        )
                    {
                        param.DefaultValue = row.GetType().GetProperty(keys[fkName]).GetValue(row).ToString();
                    }
                    else
                        param.DefaultValue = request.QueryString[keys[fkName]];

                    // add the where clause
                    linqDataSource.WhereParameters.Add(param);
                }
            }
            var displayColumnAtt = table
                .Attributes.OfType<DisplayColumnAttribute>().FirstOrDefault();
            if (displayColumnAtt != null)
            {
                linqDataSource.OrderBy = displayColumnAtt.SortColumn;
                if (displayColumnAtt.SortDescending)
                    linqDataSource.OrderBy += " desc";
            }
            else
                linqDataSource.OrderBy =
                    String.Concat(table.PrimaryKeyColumns.Select(c => c.Name + " desc ,")).TrimEnd(',');
        }
    }
}
