using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.DynamicData;
using NotAClue.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace NotAClue.Web.DynamicData
{
    public static class MetaModelExtensionMethods
    {
        /// <summary>
        /// Gets the data keys names.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public static String[] GetDataKeysNames(this MetaTable table)
        {
            int dk = 0;
            String[] dataKeys = new String[table.PrimaryKeyColumns.Count];

            foreach (var keyColumn in table.PrimaryKeyColumns)
            {
                dataKeys[dk] = keyColumn.Name;
                dk++;
            }
            return dataKeys;
        }

        /// <summary>
        /// Gets the key dictionary.
        /// </summary>
        /// <param name="childrenMetaTable">The children meta table.</param>
        /// <param name="childMetaForeignKeyColumn">The child meta foreign key column.</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetKeyDictionary(this MetaTable childrenMetaTable, MetaForeignKeyColumn childMetaForeignKeyColumn)
        {
            // setup the GridView's DataKeys
            var keys = new Dictionary<String, String>();
            var seperator = new char[] { ',' };
            var thisPrimaryKeys = childrenMetaTable.PrimaryKeyColumns.ToArray();
            var childrenForeignKeys = childMetaForeignKeyColumn.ForeignKeyNames.ToArray();
            for (int i = 0; i < thisPrimaryKeys.Length; i++)
            {
                keys.Add(childrenForeignKeys[i], thisPrimaryKeys[i].Name);
            }
            return keys;
        }

        /// <summary>
        /// Determines whether the specified column is hidden.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="currentPage">The current page.</param>
        /// <returns>
        /// 	<c>true</c> if the specified column is hidden; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsHidden(this MetaColumn column, PageTemplate currentPage)
        {
            var hideIn = column.GetAttribute<HideColumnInAttribute>();
            if (hideIn != null)
                return (hideIn.PageTemplate & currentPage) == currentPage;

            var showIn = column.GetAttribute<ShowColumnOnlyInAttribute>();
            if (showIn != null)
                return (showIn.PageTemplate & currentPage) != currentPage;

            return false;
        }

        //public static int GetColumnOrdering(this MetaColumn column, PageTemplate pageTemplate)
        //{
        //    var displayAttribute = column.GetAttributeOrDefault<DisplayAttribute>();
        //    switch (pageTemplate)
        //    {
        //        case PageTemplate.Details:
        //            goto default;
        //        case PageTemplate.Edit:
        //            goto default;
        //        case PageTemplate.Insert:
        //            goto default;
        //        case PageTemplate.List:
        //            return displayAttribute.Order != -1 ? displayAttribute.Order : displayAttribute.Order;
        //        case PageTemplate.ListDetails:
        //            goto default;
        //        default:
        //            return displayAttribute.Order;
        //    }
        //}

        /// <summary>
        /// Gets the column order.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        public static int GetColumnOrder(this MetaColumn column)
        {
            if (column.GetAttributeOrDefault<DisplayAttribute>().GetOrder() != null)
                return column.GetAttributeOrDefault<DisplayAttribute>().Order;
            else
                return 0;
        }

        /// <summary>
        /// Gets the visible columns.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="multiItemMode">if set to <c>true</c> [multi item mode].</param>
        /// <param name="template">The template.</param>
        /// <returns></returns>
        public static IEnumerable<MetaColumn> GetVisibleColumns(this MetaTable table, Boolean multiItemMode, PageTemplate template)
        {
            var r = from c in table.Columns
                    where c.IncludeField(multiItemMode, template)
                    orderby c.GetColumnOrder(), c.DisplayName
                    select c;

            return r;
        }

        /// <summary>
        /// Includes the field.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="multiItemMode">if set to <c>true</c> [multi item mode].</param>
        /// <param name="template">The template.</param>
        /// <returns></returns>
        public static bool IncludeField(this MetaColumn column, Boolean multiItemMode, PageTemplate template)
        {
            // Skip columns that should not be scaffolded
            if (!column.Scaffold)
                return false;

            // Don't display long strings in controls that show multiple items
            //var showColumn = column.GetAttributeOrDefault<ShowLongFieldAttribute>();
            if (column.IsLongString && multiItemMode) // && !showColumn.Show)
                return false;

            // Skip column if HideColumnInAttribute matches
            if (column.IsHidden(template))
                return false;

            return true;
        }

        /// <summary>
        /// Conditionals the UIHint.
        /// </summary>
        /// <param name="column">The current MetaColumn.</param>
        /// <param name="currentPage">The current page.</param>
        /// <returns></returns>
        public static String ConditionalUIHint(this MetaColumn column, PageTemplate currentPage)
        {
            var conditionalUIHint = column.GetAttribute<ConditionalUIHintAttribute>();
            if (conditionalUIHint != null && conditionalUIHint.AlternateUIHint(currentPage))
                return conditionalUIHint.UIHint;
            else
                return column.UIHint;
        }

        /// <summary>
        /// Alternates the UI hint.
        /// </summary>
        /// <param name="cUIHint">The c UI hint.</param>
        /// <param name="currentPage">The current page.</param>
        /// <returns></returns>
        public static Boolean AlternateUIHint(this ConditionalUIHintAttribute cUIHint, PageTemplate currentPage)
        {
            return (cUIHint.PageTemplates & currentPage) == currentPage;
        }

        /// <summary>
        /// Gets the primary key column.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public static MetaColumn GetPrimaryKeyColumn(this MetaTable table)
        {
            return table.Columns.FirstOrDefault(c => c.IsPrimaryKey);
        }
    }
}
