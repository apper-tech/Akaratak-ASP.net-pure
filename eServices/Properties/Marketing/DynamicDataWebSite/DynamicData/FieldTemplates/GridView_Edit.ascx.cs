﻿using DynamicDataLibrary;
using DynamicDataLibrary.Attributes;
using DynamicDataLibrary.ModelRelated;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.DynamicData;
using System.Web.Routing;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;
using DynamicDataModel.Model;
using System.ComponentModel;
using NotAClue.Web.DynamicData;
using System.Web.UI;
using System.Collections.Specialized;

using EntityDataSourceContextCreatingEventArgs = Microsoft.AspNet.EntityDataSource.EntityDataSourceContextCreatingEventArgs;
using EntityDataSource = Microsoft.AspNet.EntityDataSource.EntityDataSource;
using EntityDataSourceChangingEventArgs = Microsoft.AspNet.EntityDataSource.EntityDataSourceChangingEventArgs;
using EntityDataSourceChangedEventArgs = Microsoft.AspNet.EntityDataSource.EntityDataSourceChangedEventArgs;

namespace DynamicDataWebSite
{
    public partial class GridView_EditField : FieldTemplateUserControl
    {
        protected bool ForceDelete { get; set; }
        public int DeleteRowIndex
        {
            get
            {
                int? deleteRowIndex = this.ViewState["DeleteRowIndex"] as int?;
                return deleteRowIndex.HasValue ? deleteRowIndex.Value : -1;
            }
            set
            {
                this.ViewState["DeleteRowIndex"] = value;
            }
        }

        IOrderedDictionary backUpKeys;
        IOrderedDictionary backUpValues;

        private bool ForceInsert { set; get; }
        private OrderedDictionary primaryKeysValues;

        protected MetaTable table;

        public bool EnableDelete { get; set; }
        public bool EnableUpdate { get; set; }
        public bool EnableInsert { get; set; }

        public string[] DisplayColumns { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.table = this.Column.FigureOutForeignOrChildTable();
            if (this.Table == null)
                // throw an error if set on column other than MetaChildrenColumns
                throw new InvalidOperationException("The GridView FieldTemplate can only be used with One-To-One or One-To-Many relation!");

            this.GridView1.SetMetaTable(table);


            bool canInsert = DynamicDataLibrary.AuthorizationManager.CheckUrlAccessForPrincipal(table.GetActionPath(PageAction.Insert), Context.User);
            bool canDelete = DynamicDataLibrary.AuthorizationManager.CheckUrlAccessForPrincipal(
                table.GetActionPath(PageAction.Insert)/*.Replace("Insert", "Delete")*/, Context.User);
            bool canEdit = DynamicDataLibrary.AuthorizationManager.CheckUrlAccessForPrincipal(table.GetActionPath(PageAction.Edit), Context.User);

            var attribute = Column.Attributes.OfType<ShowColumnsAttribute>().SingleOrDefault();

            ShowColumnsAttribute.HideIfCannotView(attribute, this, table, this.Context);

            if (attribute != null)
            {
                EnableInsert = attribute.EnableInsert && canInsert || attribute.EnableInsertEvenWithNoPermission;
                EnableUpdate = attribute.EnableUpdate && canEdit || attribute.EnableUpdateEvenWithNoPermission;
                EnableDelete = attribute.EnableDelete && canEdit || attribute.EnableDeleteEvenWithNoPermission;

                if (attribute.DisplayColumns.Length > 0)
                    DisplayColumns = attribute.DisplayColumns;

                if (attribute.PageSize != 0)
                    this.GridView1.PageSize = attribute.PageSize;
            }


            //GridDataSource.ContextTypeName = metaChildColumn.ChildTable.DataContextType.Name;
            //GridDataSource.ContextTypeName = "DynamicDataModel.Model.Entities";

            //GridDataSource.TableName = metaChildColumn.ChildTable.Name;
            DataSource.EntitySetName = table.Name;

            // enable update, delete and insert
            DataSource.EnableUpdate = EnableUpdate;
            //GridView1.AutoGenerateEditButton = EnableUpdate;
            DataSource.EnableDelete = EnableDelete;
            //GridView1.AutoGenerateDeleteButton = EnableDelete;
            DataSource.EnableInsert = EnableInsert;
            LinkButtonNew.Visible = EnableInsert;

            Initialize();


            this.ValidationSummaryFormView.ValidationGroup = this.ValidationGroupNew;
            this.DynamicValidatorFormView.ValidationGroup = this.ValidationGroupNew;

            this.ValidationSummaryGridView.ValidationGroup = this.ValidationGroupEdit;
            this.DynamicValidatorGridView.ValidationGroup = this.ValidationGroupEdit;
        }

        private void Initialize()
        {
            // Generate the columns as we can't rely on 
            // DynamicDataManager to do it for us.
            GridView1.ColumnsGenerator = new FieldTemplateRowGenerator(table, DisplayColumns);

            // setup the GridView's DataKeys
            String[] keys = new String[table.PrimaryKeyColumns.Count];
            int i = 0;
            foreach (var keyColumn in table.PrimaryKeyColumns)
            {
                keys[i] = keyColumn.Name;
                i++;
            }
            GridView1.DataKeyNames = keys;

            DataSource.AutoGenerateWhereClause = true;

            //To Order By:
            var displayColumnAtt = table
                .Attributes.OfType<DisplayColumnAttribute>().FirstOrDefault();
            if (displayColumnAtt != null)
            {
                DataSource.OrderBy = "it." + displayColumnAtt.SortColumn;
                if (displayColumnAtt.SortDescending)
                    DataSource.OrderBy += " desc";
            }
            else
                DataSource.OrderBy =
                    String.Concat(table.PrimaryKeyColumns.Select(c => "it." + c.Name + " desc ,")).TrimEnd(',');
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //Not a good way to put the first column at the end. But this what could be done!
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                GridViewRow row = e.Row;
                // Intitialize TableCell list
                List<TableCell> columns = new List<TableCell>();
                foreach (DataControlField column in GridView1.Columns)
                {
                    //Get the first Cell /Column
                    TableCell cell = row.Cells[0];
                    // Then Remove it after
                    row.Cells.Remove(cell);
                    //And Add it to the List Collections

                    if (EnableUpdate || EnableDelete)
                    {
                        columns.Add(cell);
                        //row.FindControl("LinkButtonEdit")

                        System.Web.UI.Control control1 = cell.MyFindControl("LinkButtonEdit");
                        System.Web.UI.Control control2 = cell.MyFindControl("LinkButtonDelete");
                        if (control1 != null)
                            control1.Visible = EnableUpdate;
                        if (control2 != null)
                            control2.Visible = EnableDelete;

                    }
                }
                // Add cells
                row.Cells.AddRange(columns.ToArray());


                if (row.DataItem != null)
                {
                    //object item = EntityDataSourceHelper.GetItemObject(e.Row.DataItem);

                    ContributionLimitedToCreatorAttribute contributionLimitedToCreator
                        = table.GetAttribute<ContributionLimitedToCreatorAttribute>();
                    if (contributionLimitedToCreator != null)
                    {
                        if (!contributionLimitedToCreator.CanContributeOnDataItem(row.DataItem))
                        {
                            TableCell cell = row.Cells[row.Cells.Count - 1];
                            cell.Controls.Clear();
                        }
                    }

                    #region Added to Force the grid view to correctly evaluate the forien keys!!!
                    var metaChildColumn = Column as MetaChildrenColumn;
                    if (metaChildColumn == null)
                    {
                        //To set breakpoint!!!
                        int x = 0;
                        int y = x;
                    }
                    //var metaChildForeignKeyColumn = Column as MetaForeignKeyColumn;
                    //MetaTable table = metaChildColumn != null ? metaChildColumn.Table : metaChildForeignKeyColumn.ParentTable;
                    object item = EntityDataSourceHelper.GetItemObject(e.Row.DataItem);
                    foreach (string foreignKeyColumnName in table.ForeignKeyColumnsNames.Split(','))
                    {
                        System.Reflection.PropertyInfo pi = item.GetType().GetProperty(foreignKeyColumnName);
                        if (pi != null)
                            pi.GetValue(item);
                    }
                    #endregion
                }
            }
        }

        protected void GridView1_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (e.Keys.Count != 0)
            {
                this.backUpKeys = new OrderedDictionary();

                this.table.Copy(e.Keys, this.backUpKeys);
            }
            else
            {
                //throw new Exception();
            }

            bool isValueFilledManually = false;
            if (e.Keys.Count != 0 && e.Values.Count == 0)
            {
                this.table.FillValues(e.Keys, e.Values);
                isValueFilledManually = true;
            }
            this.backUpValues = new OrderedDictionary();
            foreach (System.Collections.DictionaryEntry item in e.Values)
            {
                backUpValues.Add(item.Key, item.Value);
            }

            if (Entities.CustomCheckBeforeDelete.ContainsKey(table.EntityType))
            {
                string message;
                MessageActionType messageActionType;
                bool canDelete = Entities.CustomCheckBeforeDelete[table.EntityType]
                    .Invoke(e.Values, ForceDelete, out message, out messageActionType);
                e.Cancel = !canDelete;
                if (messageActionType == MessageActionType.NeedConfirmation)
                {
                    this.DeleteRowIndex = e.RowIndex;
                    this.ShowConfirmationMessage(message);
                    return;
                }
                else if (messageActionType == MessageActionType.Error)
                {
                    this.ShowErrorMessage(message);
                    return;
                }
                else if (messageActionType == MessageActionType.Information)
                {
                    this.ShowInfoMessage(message);
                    return;
                }
            }

            if (isValueFilledManually)
                e.Values.Clear();
        }

        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            if (e.Exception == null || e.ExceptionHandled)
            {
                string message = null;
                MessageAfterActionType messageActionType = MessageAfterActionType.NoMessage;
                //Handle custom actions if any:
                if (Entities.CustomProcessAfterDelete.ContainsKey(table.EntityType))
                {
                    bool isValuesFilledManually = false;
                    if (e.Keys.Count == 0)
                    {
                        this.table.Copy(this.backUpKeys, e.Keys);
                    }
                    if (e.Values.Count == 0)
                    {
                        foreach (System.Collections.DictionaryEntry item in this.backUpValues)
                        {
                            e.Values.Add(item.Key, item.Value);
                        }
                        isValuesFilledManually = true;
                    }
                    Entities.CustomProcessAfterDelete[table.EntityType]
                        .Invoke(e.Values, e.AffectedRows, out message, out messageActionType);
                    if (isValuesFilledManually)
                        e.Values.Clear();
                    if (messageActionType == MessageAfterActionType.Error)
                    {
                        this.ShowErrorMessage(message);
                        this.DataBind();
                        return;
                    }
                    else if (messageActionType == MessageAfterActionType.Information)
                    {
                        this.ShowInfoMessage(message);
                        this.DataBind();
                        return;
                    }
                }
                this.DataBind();
            }
            else
            {
                exceptionHandlingForInsert(e.Exception, DMLOperation.Delete);
                e.ExceptionHandled = true;

                //if (e.Exception != null && e.Exception.HResult == -2146233088)
                //{
                //IQueryable query = this.table.GetQuery();
                //this.table.FillKeysAndValuesFromBackup(e.Keys, e.Values, this.keysOfDeletedRow, this.valuesOfDeletedRow);
                //isValueFilledManually = true;
                //if (e.Keys.Count != 0)
                //{
                //    string str = this.table.QueryAccordingToKeysAndValues(e.Keys).Expression.ToString();
                //    System.Linq.Expressions.Expression ex = this.table.QueryAccordingToKeysAndValues(e.Keys).Expression;

                //    new Entities().Set(table.EntityType).Remove(
                //        new Entities().Database.SqlQuery(table.EntityType,
                //            this.table.QueryAccordingToKeysAndValues(e.Keys).Expression.ToString()
                //        )
                //        );
                //    e.ExceptionHandled = true;  
                //}
                //}
            }
        }

        //protected void GridView1_PreRender(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow row in this.GridView1.Rows)
        //    {
        //        if (row.RowType != DataControlRowType.Pager)
        //        {
        //            if (row.DataItem != null)
        //            {
        //                ContributionLimitedToCreatorAttribute contributionLimitedToCreator = table.GetAttribute<ContributionLimitedToCreatorAttribute>();
        //                if (contributionLimitedToCreator != null)
        //                {
        //                    if (!contributionLimitedToCreator.CanContributeOnDataItem(row.DataItem))
        //                    {
        //                        TableCell cell = row.Cells[row.Cells.Count - 1];
        //                        cell.Controls.Clear();
        //                        //System.Web.UI.Control control1 = cell.MyFindControl("LinkButtonEdit");
        //                        //System.Web.UI.Control control2 = cell.MyFindControl("LinkButtonDelete");
        //                        //if (control1 != null)
        //                        //    control1.Visible = false;
        //                        //if (control2 != null)
        //                        //    control2.Visible = false;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            object row = null;
            try
            {
                row = EntityDataSourceHelper.GetItemObject(this.Row);
            }
            catch (Exception)
            {
                return;
            }

            var metaChildrenColumn = Column as MetaChildrenColumn;
            var metaChildForeignKeyColumn = Column as MetaForeignKeyColumn;

            string[] columns = null;

            // get the association attributes associated with MetaChildrenColumns
            AssociationKeysAttribute keyAssociation = null;
            System.Data.Linq.Mapping.AssociationAttribute association = null;

            if (metaChildrenColumn != null)
            {
                columns = (metaChildrenColumn.ColumnInOtherTable as MetaForeignKeyColumn).ForeignKeyNames.ToArray();
                keyAssociation = metaChildrenColumn.Attributes.OfType<AssociationKeysAttribute>().FirstOrDefault();
                association = metaChildrenColumn.Attributes.OfType<System.Data.Linq.Mapping.AssociationAttribute>().FirstOrDefault();
            }
            else if (metaChildForeignKeyColumn != null)
            {
                columns = metaChildForeignKeyColumn.ParentTable.PrimaryKeyColumns.Select(c => c.Name).ToArray();
                keyAssociation = metaChildForeignKeyColumn.Attributes.OfType<AssociationKeysAttribute>().FirstOrDefault();
                association = metaChildForeignKeyColumn.Attributes.OfType<System.Data.Linq.Mapping.AssociationAttribute>().FirstOrDefault();
            }

            if (columns != null && (association != null || keyAssociation != null))
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
                DataSource.WhereParameters.Clear();
                foreach (String fkName in columns)
                {
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
                        param.DefaultValue = Request.QueryString[keys[fkName]];


                    // add the where clause
                    DataSource.WhereParameters.Add(param);
                }
            }
            var displayColumnAtt = table
                .Attributes.OfType<DisplayColumnAttribute>().FirstOrDefault();
            if (displayColumnAtt != null)
            {
                DataSource.OrderBy = "it." + displayColumnAtt.SortColumn;
                if (displayColumnAtt.SortDescending)
                    DataSource.OrderBy += " desc";
            }
            else
                DataSource.OrderBy =
                    String.Concat(table.PrimaryKeyColumns.Select(c => "it." + c.Name + " desc ,")).TrimEnd(',');

            // doing the work of this above because we can't
            // set the DynamicDataManager table or where values
            //DynamicDataManager1.RegisterControl(GridView1, false);
        }

        protected void LinkButtonNew_Click(object sender, EventArgs e)
        {
            NewRecordPanel.Visible = true;
            LinkButtonNew.Visible = false;
            this.GridView1.DataBind();
        }

        protected void FormView1_PreRender(object sender, System.EventArgs e)
        {
            //HideColumnsInFormViewFieldTemplateAttribute hideColumnsInFormViewFieldTemplate
            //    = table.GetAttribute<HideColumnsInFormViewFieldTemplateAttribute>();
            //if (hideColumnsInFormViewFieldTemplate != null)
            //{
            //    foreach (string columnName in hideColumnsInFormViewFieldTemplate.HideColumns)
            //    {
            //        Control control = this.FormView1.FindDynamicControlRecursive(columnName);
            //        if (control != null)
            //            control.Parent.Visible = false;
            //    }
            //}
            if (this.DisplayColumns != null && this.DisplayColumns.Length > 0)
                foreach (MetaColumn column in table.Columns)
                {
                    if (!this.DisplayColumns.Contains(column.Name))
                    {
                        Control control = this.FormView1.FindDynamicControlRecursive(column.Name);
                        if (control != null)
                        {
                            control.Parent.Visible = false;
                            control.Parent.Controls.Clear();
                        }
                    }
                }


            //if (!string.IsNullOrEmpty(this.parentName))
            //{
            //    Control controlOfParent = this.FormView1.FindDynamicControlRecursive(parentName);
            //    if (controlOfParent != null)
            //        controlOfParent.Parent.Visible = false;
            //}

        }

        protected void FormView1_DataBound(object sender, EventArgs e)
        {
            (this.FormView1.FindControl("DynamicEntityInsert") as DynamicEntity).ValidationGroup = this.ValidationGroupNew;
            (this.FormView1.FindControl("LinkButtonInsert") as LinkButton).ValidationGroup = this.ValidationGroupNew;
        }

        protected string ValidationGroupEdit { get { return this.table.Name + "_Edit"; } }

        protected string ValidationGroupNew { get { return this.table.Name + "_New"; } }

        protected void LinkButtonUpdate_Load(object sender, System.EventArgs e)
        {
            (sender as LinkButton).ValidationGroup = this.ValidationGroupEdit;
        }
        protected void LinkButtonEdit_Load(object sender, System.EventArgs e)
        {
            (sender as LinkButton).ValidationGroup = this.ValidationGroupEdit;
        }
        protected void LinkButtonInsert_Load(object sender, System.EventArgs e)
        {
            (sender as LinkButton).ValidationGroup = this.ValidationGroupNew;
        }

        protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            OrderedDictionary values = GetForeignKeysValues(e);

            foreach (var key in values.Keys)
            {
                e.Values[key] = values[key];
            }
            //primaryKeysValues = e.Values as OrderedDictionary;


            string message;
            if (!AdvancedRangeAttribute.IsValideFor(table.Columns, e.Values, out message))
            {
                e.Cancel = true;
                this.ShowErrorMessage(message);
                return;
            }

            //Handle custom actions if any:
            if (Entities.CustomCheckBeforeInsert.ContainsKey(table.EntityType))
            {
                MessageActionType messageActionType;
                bool canInsert = Entities.CustomCheckBeforeInsert[table.EntityType]
                    .Invoke(e.Values, ForceInsert, out message, out messageActionType);
                e.Cancel = !canInsert;
                if (messageActionType == MessageActionType.NeedConfirmation)
                {
                    this.ShowConfirmationMessage(message);
                    return;
                }
                else if (messageActionType == MessageActionType.Error)
                {
                    this.ShowErrorMessage(message);
                    return;
                }
                else if (messageActionType == MessageActionType.Information)
                {
                    this.ShowInfoMessage(message);
                    return;
                }
            }
        }

        private OrderedDictionary GetForeignKeysValues(FormViewInsertEventArgs e)
        {
            OrderedDictionary values = new OrderedDictionary();
            //If this Filed Template is inside a GridView Field Template, access the datakey of the container GridView.
            //Something similare has to be writen to handle the case if this Field Template is inside a FormView Filed Template...
            DataKey dataKey = null;
            try
            {
                dataKey = (this.Parent.Parent.Parent.Parent.Parent as System.Web.UI.WebControls.GridView).DataKeys[
                    (this.Parent.Parent.Parent as System.Web.UI.WebControls.GridViewRow).RowIndex];
            }
            catch (Exception)
            {
            }

            var metaChildrenColumn = Column as MetaChildrenColumn;
            var metaForeignKeyColumn = metaChildrenColumn.ColumnInOtherTable as MetaForeignKeyColumn;


            // get the association attributes associated with MetaChildrenColumns
            AssociationKeysAttribute keyAssociation = metaChildrenColumn.Attributes.
                OfType<AssociationKeysAttribute>().FirstOrDefault();
            System.Data.Linq.Mapping.AssociationAttribute association = metaChildrenColumn.Attributes.
                OfType<System.Data.Linq.Mapping.AssociationAttribute>().FirstOrDefault();

            if (metaForeignKeyColumn != null && (association != null || keyAssociation != null))
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
                foreach (String fkName in metaForeignKeyColumn.ForeignKeyNames)
                {
                    // get the current pk column
                    var fkColumn = metaChildrenColumn.ChildTable.GetColumn(fkName);
                    if (dataKey != null)
                    {
                        values.Add(fkColumn.Name, dataKey[keys[fkName]]);
                        //e.Values[fkColumn.Name] = dataKey[keys[fkName]];
                    }
                    else
                    {
                        values.Add(fkColumn.Name, Request.QueryString[keys[fkName]]);
                        //e.Values[fkColumn.Name] = Request.QueryString[keys[fkName]];
                    }
                }
            }
            return values;
        }

        protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            if (e.Exception == null || e.ExceptionHandled)
            {
                //foreach (System.Collections.DictionaryEntry item in this.primaryKeysValues)
                //{
                //    e.Values[item.Key] = item.Value;
                //}

                //Handle custom actions if any:
                if (Entities.CustomProcessAfterInsert.ContainsKey(table.EntityType))
                {
                    string message = null;
                    MessageAfterActionType messageActionType;
                    Entities.CustomProcessAfterInsert[table.EntityType]
                        .Invoke(e.Values, e.AffectedRows, out message, out messageActionType);
                    if (messageActionType == MessageAfterActionType.Error)
                    {
                        this.ShowErrorMessage(message);
                        return;
                    }
                    else if (messageActionType == MessageAfterActionType.Information)
                    {
                        this.ShowInfoMessage(message);
                        return;
                    }
                }
                NewRecordPanel.Visible = false;
                LinkButtonNew.Visible = true;
                GridView1.DataBind();
            }
            else
            {
                exceptionHandlingForInsert(e.Exception, DMLOperation.Insert);
                e.ExceptionHandled = true;
                return;
            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            table.RemoveWrongEntries(e.OldValues);
            table.RemoveWrongEntries(e.NewValues);
            if (this.primaryKeysValues != null)
                foreach (System.Collections.DictionaryEntry item in this.primaryKeysValues)
                {
                    e.OldValues.Add(item.Key, item.Value);
                    e.NewValues.Add(item.Key, item.Value);
                }


            string message;
            if (!AdvancedRangeAttribute.IsValideFor(table.Columns, e.NewValues, out message))
            {
                e.Cancel = true;
                this.ShowErrorMessage(message);
                return;
            }

            if (Entities.CustomCheckBeforeEdit.ContainsKey(table.EntityType))
            {
                MessageActionType messageActionType;
                bool canInsert = Entities.CustomCheckBeforeEdit[table.EntityType]
                    .Invoke(e.OldValues, e.NewValues, ForceInsert, out message, out messageActionType);
                e.Cancel = !canInsert;
                if (messageActionType == MessageActionType.NeedConfirmation)
                {
                    this.ShowConfirmationMessage(message);
                    return;
                }
                else if (messageActionType == MessageActionType.Error)
                {
                    ShowErrorMessage(message);
                    return;
                }
                else if (messageActionType == MessageActionType.Information)
                {
                    this.ShowInfoMessage(message);
                    return;
                }
            }
        }

        protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            foreach (System.Collections.DictionaryEntry item in this.primaryKeysValues)
            {
                if (!e.OldValues.Contains(item.Key))
                    e.OldValues.Add(item.Key, item.Value);
                if (!e.NewValues.Contains(item.Key))
                    e.NewValues.Add(item.Key, item.Value);
            }
            if (e.Exception == null || e.ExceptionHandled)
            {
                //Handle custom actions if any:
                string message = null;
                MessageAfterActionType messageActionType;
                if (Entities.CustomProcessAfterEdit.ContainsKey(table.EntityType))
                {
                    Entities.CustomProcessAfterEdit[table.EntityType]
                        .Invoke(e.OldValues, e.NewValues, e.AffectedRows, out message, out messageActionType);
                    if (messageActionType == MessageAfterActionType.Error)
                    {
                        this.ShowErrorMessage(message);
                        return;
                    }
                    else if (messageActionType == MessageAfterActionType.Information)
                    {
                        this.ShowInfoMessage(message);
                        return;
                    }
                }
                this.DataBind();
            }
            else
            {
                exceptionHandlingForInsert(e.Exception, DMLOperation.Update);
                e.ExceptionHandled = true;
                return;
            }
        }
        public enum DMLOperation { Insert, Update, Delete }
        private void exceptionHandlingForInsert(Exception exception, DMLOperation operation)
        {
            string message;
            if (exception.InnerException != null
                && (exception.InnerException.Message.Contains("duplicate key row")
                || exception.InnerException.Message.StartsWith("Violation of UNIQUE KEY constraint")))
            {
                if (operation == DMLOperation.Insert)
                    message = Convert.ToString(GetGlobalResourceObject("DynamicData", "Error_CannotAddBecauseOfDuplication"));
                else if (operation == DMLOperation.Update)
                    message = Convert.ToString(GetGlobalResourceObject("DynamicData", "Error_CannotUpdateBecauseOfDuplication"));
                else
                    message = null;
            }
            else if (exception.HResult == -2146233087)
                message = Convert.ToString(GetGlobalResourceObject("DynamicData", "Error_CannotDeleteBecauseOfReletedRecords"));

            else
            {
                object friendlyError;
                switch (operation)
                {
                    case DMLOperation.Insert:
                        friendlyError = GetGlobalResourceObject("DynamicData", "Error_CannotAddWithFollowingDetails");
                        break;
                    case DMLOperation.Update:
                        friendlyError = GetGlobalResourceObject("DynamicData", "Error_CannotUpdateWithFollowingDetails");
                        break;
                    case DMLOperation.Delete:
                        friendlyError = GetGlobalResourceObject("DynamicData", "Error_CannotDeleteBecauseOf");
                        break;
                    default:
                        friendlyError = null;
                        break;
                }
                message = String.Format("{0}:<br/>{1}", friendlyError, exception.InnerException == null ? exception.Message : exception.InnerException.Message);
            }
            ShowErrorMessage(message);
        }

        protected void DataSource_Updating(object sender, EntityDataSourceChangingEventArgs e)
        {
            primaryKeysValues = new OrderedDictionary();
            // setup the GridView's DataKeys
            System.Type entityType = e.Entity.GetType();
            foreach (var keyColumn in table.PrimaryKeyColumns)
            {
                object value = entityType.GetProperty(keyColumn.Name).GetValue(e.Entity);
                primaryKeysValues.Add(keyColumn.Name, value);
            }
        }

        protected void DataSource_Updated(object sender, EntityDataSourceChangedEventArgs e)
        {

        }

        protected void DataSource_Inserting(object sender, EntityDataSourceChangingEventArgs e)
        {

        }
        protected void DataSource_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {

        }
        protected void DataSource_QueryCreated(object sender, QueryCreatedEventArgs e)
        {
            if (Entities.CustomQuery.ContainsKey(table.EntityType))
            {
                e.Query = Entities.CustomQuery[table.EntityType]
                    .Invoke(e.Query);
            }
            else
            {
                //ViewLimitedToCurrentUserAttribute dataLimitedToCurrentUser = table.GetAttribute<ViewLimitedToCurrentUserAttribute>();
                //if (dataLimitedToCurrentUser != null)
                //{
                //    Nullable<int> numberOfItems;
                //    dataLimitedToCurrentUser.Handle(e, this.table, this.Page, out numberOfItems);
                //}
            }
        }

        protected void DataSource_ContextCreating(object sender, EntityDataSourceContextCreatingEventArgs e)
        {
            e.Context = ((IObjectContextAdapter)new Entities(true, Global.DefaultModel)).ObjectContext;
        }

        protected void LinkButtonCancel_Click(object sender, EventArgs e)
        {
            NewRecordPanel.Visible = false;
            LinkButtonNew.Visible = true;
            this.GridView1.DataBind();
        }

        protected void LinkButtonConfirmationOk_Click(object sender, EventArgs e)
        {
            this.ForceInsert = true;
            this.FormView1.UpdateItem(true);
        }

        protected void LinkButtonConfirmationCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(table.ListActionPath);
        }

        protected void LinkButtonInformationMessageOK_Click(object sender, EventArgs e)
        {
            this.FormView1.DataBind();
        }

        protected void LinkButtonErrorMessageOK_Click(object sender, EventArgs e)
        {
            this.FormView1.DataBind();
        }

        private void ShowErrorMessage(string message)
        {
            this.LabelErrorMessage.Text = message;
            this.ModalPopupExtenderErrorMessage.Show();
        }

        private void ShowInfoMessage(string message)
        {
            this.LabelInformationMessage.Text = message;
            this.ModalPopupExtenderInformationMessage.Show();
        }

        private void ShowConfirmationMessage(string message)
        {
            this.LabelConfirmationMessage.Text = message;
            this.ModalPopupExtenderConfirmationMessage.Show();
        }

        protected void DynamicEntity_Load(object sender, EventArgs e)
        {
            (sender as DynamicEntity).ValidationGroup = this.ValidationGroupNew;
            this.ValidationSummaryFormView.ValidationGroup = this.ValidationGroupNew;
            this.DynamicValidatorFormView.ValidationGroup = this.ValidationGroupNew;
        }
    }
}

//Source: http://csharpbits.notaclue.net/2008/08/dynamic-data-and-field-templates.html