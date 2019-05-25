using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using NotAClue;
using NotAClue.ComponentModel.DataAnnotations;
using NotAClue.Web;

namespace NotAClue.Web.DynamicData
{
    public static class FieldTemplateExtensionMethods
    {
        #region Static
        public static String RequiredCssClass { get; set; }
        static FieldTemplateExtensionMethods()
        {
            RequiredCssClass = "DDRequired";
        }
        #endregion

        ///// <summary>
        ///// Populates the list control filtered attribute in meta data.
        ///// </summary>
        ///// <param name="fieldTemplate">The field template.</param>
        ///// <param name="listControl">The list control.</param>
        ///// <remarks></remarks>
        //public static void PopulateListControlFiltered(this FieldTemplateUserControl fieldTemplate, ListControl listControl)
        //{
        //    var parentTable = fieldTemplate.ForeignKeyColumn.ParentTable;
        //    var items = parentTable.GetQuery().ApplyColumnFilter(fieldTemplate.ForeignKeyColumn.ParentTable);
        //    foreach (object obj2 in items)
        //    {
        //        string displayString = parentTable.GetDisplayString(obj2);
        //        string primaryKeyString = parentTable.GetPrimaryKeyString(obj2);
        //        listControl.Items.Add(new ListItem(displayString, primaryKeyString.TrimEnd(new char[0])));
        //    }
        //}

        ///// <summary>
        ///// Applies the column filter from attribute.
        ///// </summary>
        ///// <param name="query">The query.</param>
        ///// <param name="Table">The table.</param>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //public static IQueryable ApplyColumnFilter(this IQueryable query, MetaTable Table)
        //{
        //    var attribute = Table.GetAttribute<FilterColumnAttribute>();
        //    if (attribute != null)
        //    {
        //        // do filter by column
        //        var filterColumn = Table.GetColumn(attribute.Name);

        //        // get list of values filtered by the parent's selected entity
        //        var filteredQuery = query.GetQueryFilteredByColumn(filterColumn, attribute.Value);

        //        return filteredQuery;
        //    }

        //    return query;
        //}

        /// <summary>
        /// Sets the required CSS class.
        /// </summary>
        /// <param name="fieldTemplate">The field template.</param>
        /// <param name="control">The control.</param>
        public static void SetRequiredCssClass(this FieldTemplateUserControl fieldTemplate, WebControl control)
        {
            if (fieldTemplate.Column.IsRequired)
                control.CssClass += String.Format(" {0}", RequiredCssClass);
        }

        ///// <summary>
        ///// Determines whether [is navigation disabled] [the specified field template].
        ///// </summary>
        ///// <param name="fieldTemplate">The field template.</param>
        ///// <param name="column">The column.</param>
        ///// <returns>
        ///// 	<c>true</c> if [is navigation disabled] [the specified field template]; otherwise, <c>false</c>.
        ///// </returns>
        //public static Boolean IsNavigationDisabled(this FieldTemplateUserControl fieldTemplate, MetaColumn column)
        //{
        //    DisableNavigationInAttribute disableNavigationIn;
        //    var pageTemplate = fieldTemplate.Page.GetPageTemplate();

        //    // get table version of attribute
        //    if (column is MetaForeignKeyColumn)
        //        disableNavigationIn = ((MetaForeignKeyColumn)column).ParentTable.GetAttribute<DisableNavigationInAttribute>();

        //    // get table version of attribute
        //    if (column is MetaChildrenColumn)
        //        disableNavigationIn = ((MetaChildrenColumn)column).ChildTable.GetAttribute<DisableNavigationInAttribute>();

        //    // get column version of attribute
        //    disableNavigationIn = column.GetAttribute<DisableNavigationInAttribute>();

        //    if (disableNavigationIn != null && (disableNavigationIn.PageTemplate & pageTemplate) == pageTemplate)
        //        return true;

        //    return false;
        //}


        /// <summary>
        /// Gets the default value.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        public static String GetDefaultValue(this Control control, MetaColumn column)
        {
            String returnValue = String.Empty;
            if (column is MetaForeignKeyColumn)
            {
                var keys = ((MetaForeignKeyColumn)column).ForeignKeyNames.ToCsvString();
                returnValue = control.Page.Request.QueryString[keys];
            }

            if (String.IsNullOrEmpty(returnValue))
            {
                var value = column.GetAttribute<DefaultValueAttribute>();
                if (value != null)
                    returnValue = value.Value.ToString();
            }

            if (String.IsNullOrEmpty(returnValue))
                returnValue = control.Page.Request.QueryString[column.Name];

            return returnValue;
        }

        //// set default values if present in query string
        //foreach (var fKey in ForeignKeyColumn.ForeignKeyNames)
        //{
        //    var fKeyValue = Request.QueryString[fKey];
        //    if (!String.IsNullOrEmpty(fKeyValue))
        //    {
        //        ListItem item = DropDownList1.Items.FindByValue(fKeyValue);
        //        if (item != null)
        //            DropDownList1.SelectedValue = fKeyValue;
        //    }
        //}

        ///// <summary>
        ///// Disables the field template.
        ///// </summary>
        ///// <param name="column">The column.</param>
        ///// <param name="mode">The mode.</param>
        ///// <param name="control">The control.</param>
        //public static void DisableFieldTemplate(this MetaColumn column, DataBoundControlMode mode, WebControl control)
        //{
        //    if (mode == DataBoundControlMode.Insert)
        //    {
        //        var insertPageTemplate = PageTemplate.Insert;
        //        var readOnly = column.GetAttributeOrDefault<ColumnReadOnlyInAttribute>();
        //        if ((readOnly.PageTemplate & insertPageTemplate) == insertPageTemplate)
        //        {
        //            control.Enabled = false;
        //            control.CssClass += " " + readOnly.DisabledCssClass;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Sets the field options.
        ///// </summary>
        ///// <param name="control">The control.</param>
        ///// <param name="column">The column.</param>
        //public static void SetFieldOptions(this WebControl control, MetaColumn column)
        //{
        //    var fieldOptions = column.GetAttribute<FieldOptionsAttribute>();
        //    if (fieldOptions == null)
        //        return;

        //    if (control is TextBox)
        //    {
        //        var textBox = (TextBox)control;
        //        textBox.TextMode = fieldOptions.TextBoxMode;
        //        if (fieldOptions.Columns > 0)
        //            textBox.Columns = fieldOptions.Columns;
        //        if (fieldOptions.Rows > 0)
        //            textBox.Rows = fieldOptions.Rows;
        //    }

        //    if (fieldOptions != null && fieldOptions.Width > 0)
        //        control.Width = fieldOptions.Width;
        //    if (fieldOptions != null && fieldOptions.Height > 0)
        //        control.Width = fieldOptions.Height;
        //}

        /// <summary>
        /// Gets the page template.
        /// </summary>
        /// <returns></returns>
        public static PageTemplate GetPageTemplate()
        {
            var pageTemplate = PageTemplate.Unknown;
            if (System.Web.HttpContext.Current != null)
            {
                var page = System.Web.HttpContext.Current.Handler as Page;
                pageTemplate = page.GetPageTemplate();
            }

            return pageTemplate;
        }

        /// <summary>
        /// Gets the primary key values as string.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public static String GetPrimaryKeyValuesAsString(this MetaTable table, Page page)
        {
            var pkValues = new StringBuilder();
            var keys = table.PrimaryKeyColumns;
            foreach (var key in keys)
            {
                pkValues.Append(page.Request.QueryString[key.Name] + ",");
            }
            return pkValues.ToString().Substring(0, pkValues.Length - 1);
        }

        /// <summary>
        /// Gets the foreign key values as string.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="table">The table.</param>
        /// <param name="fkColumnName">Name of the foreign key column.</param>
        /// <returns></returns>
        public static String GetForiegnKeyValuesAsString(this Control control, MetaTable table, String fkColumnName)
        {
            var foreignKeyColumn = table.GetColumn(fkColumnName) as MetaForeignKeyColumn;
            if (foreignKeyColumn == null)
                return null;

            var fkValues = new StringBuilder();
            var listView = control.GetParent<ListView>();
            if (listView != null)
            {
                // in DetailsList page set default 
                // value based on where parameters
                var dataSource = listView.DataSourceObject as IDynamicDataSource;
                foreach (Parameter p in dataSource.WhereParameters)
                {
                    if (foreignKeyColumn.ForeignKeyNames.Contains(p.Name))
                        fkValues.Append(p.DefaultValue + ",");
                }
            }
            if (fkValues.Length > 0)
                return fkValues.ToString().Substring(0, fkValues.Length - 1);
            else
                return null;
        }

        /// <summary>
        /// Gets the default value.
        /// </summary>
        /// <param name="fieldTemplate">The field template.</param>
        /// <returns></returns>
        public static String GetDefaultValue(this FieldTemplateUserControl fieldTemplate)
        {
            String returnValue = String.Empty;
            if (fieldTemplate.Column is MetaForeignKeyColumn)
            {
                var keys = ((MetaForeignKeyColumn)fieldTemplate.Column).ForeignKeyNames.ToCsvString();
                returnValue = fieldTemplate.Page.Request.QueryString[keys];
            }

            if (String.IsNullOrEmpty(returnValue))
            {
                var value = fieldTemplate.Column.GetAttribute<DefaultValueAttribute>();
                if (value != null)
                    returnValue = value.Value.ToString();
            }

            if (String.IsNullOrEmpty(returnValue))
                returnValue = fieldTemplate.Page.Request.QueryString[fieldTemplate.Column.Name];

            return returnValue;
        }
    }
}
