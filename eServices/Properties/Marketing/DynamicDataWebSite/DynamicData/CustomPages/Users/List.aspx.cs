using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;
using System.Linq;
using DynamicDataLibrary;
using System.Linq.Dynamic;
using DynamicDataLibrary.ModelRelated;
using DynamicDataLibrary.Attributes;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Security.Principal;
using NotAClue.Web.DynamicData;
using System.Collections.Specialized;
using DynamicDataModel.Model;

using EntityDataSourceChangingEventArgs = Microsoft.AspNet.EntityDataSource.EntityDataSourceChangingEventArgs;
using EntityDataSourceChangedEventArgs = Microsoft.AspNet.EntityDataSource.EntityDataSourceChangedEventArgs;

namespace DynamicDataWebSite.DynamicData.CustomPages.Users
{
    public partial class List : System.Web.UI.Page
    {
        protected bool ForceDelete { get; set; }
        protected MetaTable table;

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

        public string DeleteItemCommandText { get; set; }
        public string DeleteConfirmationMesssage { get; set; }
        public string NewItemLinkText { get; set; }
        public string ItemDetailsCommandText { get; set; }
        public string EditItemCommandText { get; set; }
        public string NoRecordsAtList { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            table = DynamicDataRouteHandler.GetRequestMetaTable(Context);
            if (table != null)
                GridView1.SetMetaTable(table, table.GetColumnValuesFromRoute(Context));
            else
                Response.Redirect("~/");
            GridDataSource.EntityTypeFilter = table.EntityType.Name;

            DynamicDataEntityCustomTextAttribute customText = table.Attributes.GetAttribute<DynamicDataEntityCustomTextAttribute>();
            if (customText != null)
            {
                this.NewItemLinkText = customText.NewItemLinkText;
                this.ItemDetailsCommandText = customText.ItemDetailsCommandText;
                this.EditItemCommandText = customText.EditItemCommandTextAtList;
                this.DeleteItemCommandText = customText.DeleteItemCommandText;
                this.DeleteConfirmationMesssage = customText.DeleteConfirmationMesssage;
                this.NoRecordsAtList = customText.NoRecordsAtList;
            }
            if (String.IsNullOrEmpty(this.NewItemLinkText))
                this.NewItemLinkText = Convert.ToString(GetGlobalResourceObject("DynamicData", "NewRecord"));
            if (String.IsNullOrEmpty(this.ItemDetailsCommandText))
                this.ItemDetailsCommandText = Convert.ToString(GetGlobalResourceObject("DynamicData", "Details"));
            if (String.IsNullOrEmpty(this.EditItemCommandText))
                this.EditItemCommandText = Convert.ToString(GetGlobalResourceObject("DynamicData", "Update"));
            if (String.IsNullOrEmpty(this.DeleteItemCommandText))
                this.DeleteItemCommandText = Convert.ToString(GetGlobalResourceObject("DynamicData", "Delete"));
            if (String.IsNullOrEmpty(this.DeleteConfirmationMesssage))
                this.DeleteConfirmationMesssage = Convert.ToString(GetGlobalResourceObject("DynamicData", "DelConfirm"));
            if (String.IsNullOrEmpty(this.NoRecordsAtList))
                this.NoRecordsAtList = Convert.ToString(GetGlobalResourceObject("DynamicData", "EmptyRecords"));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Resources.Account.View_Users;
            GridDataSource.Include = table.ForeignKeyColumnsNames;

            // Disable various options if the table is readonly
            if (table.IsReadOnly)
            {
                GridView1.EnablePersistedSelection = false;
            }

            var displayColumn = table.GetAttribute<DisplayColumnAttribute>();
            if (displayColumn != null && displayColumn.SortColumn != null)
            {
                GridView1.Sort(displayColumn.SortColumn,
                    displayColumn.SortDescending ? SortDirection.Descending : SortDirection.Ascending);
            }

            //if (ViewState["PageIndex"] != null)
            //{
            //    var pager = this.GridView1.BottomPagerRow.FindControlRecursive<GridViewPager>();
            //    if (pager != null)
            //        pager.LastPageIndex = (int)ViewState["PageIndex"];
            //    this.GridView1.PageIndex = (int)ViewState["PageIndex"];
            //}
            //custo table attribs

            var f = table.Attributes.OfType<CustomViewAttribute>().FirstOrDefault();
            if (f != null)
            {
                //custom table view attributes
                if (f.HideFilters)
                {
                    FilterRepeater.Visible = false;
                }
                if (f.HideColumnInTemplate == NotAClue.ComponentModel.DataAnnotations.PageTemplate.List)
                {
                    if (f.HideColumnIfEmpty)
                    {

                    }
                }
            }

        }

        protected void Label_PreRender(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            DynamicFilter dynamicFilter = (DynamicFilter)label.FindControl("DynamicFilter");
            QueryableFilterUserControl fuc = dynamicFilter.FilterTemplate as QueryableFilterUserControl;
            if (fuc != null && fuc.FilterControl != null)
            {
                label.AssociatedControlID = fuc.FilterControl.GetUniqueIDRelativeTo(label);
            }
            foreach (MetaColumn item in table.Columns)
            {
                if (item.Name == dynamicFilter.DataField)
                {
                    var filter = item.Attributes.OfType<NotAClue.ComponentModel.DataAnnotations.FilterAttribute>().FirstOrDefault();
                    if (filter != null)
                    {
                        //apply all filter attribs here
                        if (filter.Hidden)
                        {
                            label.Visible = false;
                            dynamicFilter.Visible = false;
                        }
                        break;
                    }
                }
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            RouteValueDictionary routeValues = new RouteValueDictionary(GridView1.GetDefaultValues());
           base.OnPreRenderComplete(e);

            //ViewState["PageIndex"] = this.GridView1.PageIndex;
        }

        protected void DynamicFilter_FilterChanged(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
        }

        protected void GridDataSource_QueryCreated(object sender, QueryCreatedEventArgs e)
        {
            if (Entities.CustomQuery.ContainsKey(table.EntityType))
            {
                e.Query = Entities.CustomQuery[table.EntityType]
                    .Invoke(e.Query);
            }
            else
            {
                ViewLimitedToCurrentUserAttribute dataLimitedToCurrentUser = table.GetAttribute<ViewLimitedToCurrentUserAttribute>();
                if (dataLimitedToCurrentUser != null)
                {
                    Nullable<int> numberOfItems;
                    dataLimitedToCurrentUser.Handle(e, this.table, this.Page, out numberOfItems);
                }
            }
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

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            bool hasInsertPermission = DynamicDataLibrary.AuthorizationManager.CheckUrlAccessForPrincipal(table.GetActionPath(PageAction.Insert), this.Page.User);
            if (!hasInsertPermission)
            {
                e.Cancel = true;
            }
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
                if (isValueFilledManually)
                    e.Values.Clear();
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

        }

        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            if (e.Exception != null && !e.ExceptionHandled)
            {
                e.ExceptionHandled = true;
                //this.FormView1.Visible = false;
                if (e.Exception.HResult == -2146233087)
                    ShowErrorMessage(Convert.ToString(GetGlobalResourceObject("DynamicData", "Error_CannotDeleteBecauseOfReletedRecords")));
                else
                    ShowErrorMessage(String.Format("{0}:<br/>{1}", GetGlobalResourceObject("DynamicData", "Error_CannotDeleteBecauseOf"),
                        e.Exception.Message));

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
            else
            {
                bool isValuesFilledManually = false;
                string message = null;
                MessageAfterActionType messageActionType = MessageAfterActionType.NoMessage;
                //Handle custom actions if any:
                if (Entities.CustomProcessAfterDelete.ContainsKey(table.EntityType))
                {
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

                //this.Response.Redirect(table.ListActionPath);
                this.DataBind();
            }
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                #region Show Custom Properties
                //To Show Custom Properties, but did not Work!!!
                //Something like: http://csharpbits.notaclue.net/2010/02/new-way-to-do-column-generation-in.html 
                //  must be done when having time!
                //if (e.Row.DataItem != null)
                //{
                //    System.Type type = table.EntityType;
                //    var item =
                //        ExtentionMethods.GetItemObject(type, e.Row.DataItem);
                //    e.Row.DataItem = item;

                //    e.Row.Cells.Add(new TableCell() { Text = (item as SickLeave).EndDate.ToString() });
                //}
                #endregion

                #region Put First Column at End
                //Not a good way to put the first column at the end. But this what could be done!
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
                    columns.Add(cell);
                }
                // Add cells
                row.Cells.AddRange(columns.ToArray());
                #endregion
                

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ContributionLimitedToCreatorAttribute contributionLimitedToCreator
                    = table.GetAttribute<ContributionLimitedToCreatorAttribute>();
                if (contributionLimitedToCreator != null)
                {
                    if (!contributionLimitedToCreator.CanContributeOnDataItem(row.DataItem))
                    {
                        TableCell cell = row.Cells[row.Cells.Count - 1];
                       var f= cell.FindControl("DynamicHyperLink1");
                        cell.Controls.Clear();
                        cell.Controls.Add(f);
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Pager)
            {
                ViewState["PageIndex"] = this.GridView1.PageIndex;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            this.GridView1.DataBind(); // you data bind code
        }


        protected void LinkButtonConfirmationDelete_Click(object sender, EventArgs e)
        {
            this.ForceDelete = true;
            this.GridView1.DeleteRow(this.DeleteRowIndex);
        }

        protected void LinkButtonConfirmationCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(table.ListActionPath);
        }

        protected void LinkButtonInformationMessageOK_Click(object sender, EventArgs e)
        {
            this.GridView1.DataBind();
        }

        protected void LinkButtonErrorMessageOK_Click(object sender, EventArgs e)
        {
            this.GridView1.DataBind();
        }

        protected void GridDataSource_Deleting(object sender, EntityDataSourceChangingEventArgs e)
        {

        }

        protected void GridDataSource_Deleted(object sender, EntityDataSourceChangedEventArgs e)
        {

        }

        protected bool CouldBeDeleted(IDataItemContainer container)
        {
            object item = EntityDataSourceHelper.GetItemObject(container.DataItem);
            foreach (DisableDeleteIfEqualAttribute att in table.Attributes.OfType<DisableDeleteIfEqualAttribute>())
            {
                if (att.Values.Contains(item.GetType().GetProperty(att.PropertyName).GetValue(item)))
                    return false;
            }
            return true;
        }

        protected void FilterRepeater_DataBinding(object sender, EventArgs e)
        {

        }

        protected void DynamicHyperLink1_PreRender(object sender, EventArgs e)
        {
            var hy = sender as DynamicHyperLink;
            var id = "";
            var ac = "";
            DynamicDataHelper.TransolateRouteURl(hy, out id, out ac);
            DynamicDataHelper.RewriteRouteUrl(hy, id, ac);

        }
    }
}