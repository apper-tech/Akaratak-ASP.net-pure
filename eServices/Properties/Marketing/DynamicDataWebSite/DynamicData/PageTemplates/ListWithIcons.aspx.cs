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
using System.Globalization;

namespace DynamicDataWebSite.DynamicData.PageTemplates
{
    public partial class ListWithIcons : System.Web.UI.Page
    {
        protected bool ForceDelete { get; set; }
        public MetaTable table;

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
            try
            {
                table = DynamicDataRouteHandler.GetRequestMetaTable(Context);

                if (table == null) Response.Redirect(SecurityHandler.ErrorPath);
                var l = table.GetColumnValuesFromRoute(Context);
                listView.SetMetaTable(table, l);
                GridDataSource.EntityTypeFilter = table.EntityType.Name;
                //GridDataSource.AutoGenerateWhereClause = false;
                //GridDataSource.Where = string.Format("CONVERT(datetime,it.Expire_Date,131) >= getdate()");
               // GridDataSource.Where = string.Format("it.Expire_Date >='{0}' ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.GetCultureInfo("en-US")));
               // GridDataSource.WhereParameters.Clear();
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
                    this.NewItemLinkText = Convert.ToString(GetGlobalResourceObject("RealEstate", "NewRecord"));
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
            catch (Exception xe)
            {
                //err
            }
        }

        private void ProccessTitle(string res)
        {
            Title = res != "" ? res : table.DisplayName;
            var seg = new Uri("http://" + Request.Url.Authority + Request.RawUrl).Segments;
            foreach (var item in seg)
            {
                string t = System.Net.WebUtility.UrlDecode(item.Replace("/", ""));
                if (!Title.Contains(t))
                    Title += " ," + t;
            }

        }
        public string ProccessFilterName(string text)
        {
            string value=DynamicDataModel.App_LocalResources.TableCols.ResourceManager.GetString(("Column " + text).Replace(' ','_'), CultureInfo.CurrentCulture);
            if (!string.IsNullOrEmpty(value)&& value != text)
                return value;
            return text;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string title = GetGlobalResourceObject("RealEstate", "Page_List_Title") as string;
            ProccessTitle(title);
            GridDataSource.Include = table.ForeignKeyColumnsNames;
            // Disable various options if the table is readonly
            if (table.IsReadOnly)
            {
                InsertHyperLink.Visible = false;
                listView.EnablePersistedSelection = false;
            }

            var displayColumn = table.GetAttribute<DisplayColumnAttribute>();
            if (displayColumn != null && displayColumn.SortColumn != null)
            {
                listView.Sort(displayColumn.SortColumn,
                    displayColumn.SortDescending ? SortDirection.Descending : SortDirection.Ascending);
            }
            //if (!IsPostBack)
            //    ModalPopupExtenderLoadingMessage.Hide();
            //if (ViewState["PageIndex"] != null)
            //{
            //    var pager = this.GridView1.BottomPagerRow.FindControlRecursive<GridViewPager>();
            //    if (pager != null)
            //        pager.LastPageIndex = (int)ViewState["PageIndex"];
            //    this.GridView1.PageIndex = (int)ViewState["PageIndex"];
            //}
            //custo table attribs

            //var f = table.Attributes.OfType<CustomViewAttribute>().FirstOrDefault();
            //if (f != null)
            //{
            //    //custom table view attributes
            //    if (f.HideFilters)
            //    {
            //        FilterRepeater.Visible = false;
            //    }
            //    if (f.HideColumnInTemplate == NotAClue.ComponentModel.DataAnnotations.PageTemplate.List)
            //    {
            //        if (f.HideColumnIfEmpty)
            //        {

            //        }
            //    }
            //}
            //manage authorization


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
                            HideFilter(item.Name);
                        }
                        break;
                    }
                }

            }
        }
        public void HideFilter(string datafeild)
        {
            foreach (Control item in FilterRepeater.Controls)
            {
                Control c = GetControlRec("holder", item.Controls);
                Label l = (Label)GetControlRec(typeof(Label), c.Controls);
                if (l.ToolTip == datafeild)
                {
                    c.Visible = false;
                }
            }
        }
        public bool chkstring(string src, string comp, StringComparison co)
        {
            if (!string.IsNullOrEmpty(src))
                return src.Substring(0, 5).IndexOf(comp.Substring(0, 5), co) >= 0;
            else
                return true;
        }
        public Control GetControlRec(string id, ControlCollection root)
        {
            foreach (Control item in root)
            {
                if (item.ID == id)
                    return item;
            }
            return null;
        }
        public Control GetControlRec(Type id, ControlCollection root)
        {
            foreach (Control item in root)
            {
                if (item.GetType() == id)
                    return item;
            }
            return null;
        }
        protected override void OnPreRenderComplete(EventArgs e)
        {
            RouteValueDictionary routeValues = new RouteValueDictionary(listView.GetDefaultValues());
            InsertHyperLink.NavigateUrl = table.GetActionPath(PageAction.Insert, routeValues);
            base.OnPreRenderComplete(e);
            //ViewState["PageIndex"] = this.GridView1.PageIndex;
        }

        protected void DynamicFilter_FilterChanged(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ScrollToResult", "alert('cool')", true);
        }
        public string GetPageName()
        {
            return System.IO.Path.GetFileName(Request.Url.AbsolutePath);
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
            //(listView.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Format("Expire_Date > '{0}'", DateTime.Now);
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
            }

        }

        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            if (e.Exception != null && !e.ExceptionHandled)
            {
                e.ExceptionHandled = true;
                //this.FormView1.Visible = false;

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

                }

                //this.Response.Redirect(table.ListActionPath);
                this.DataBind();
            }
        }
        public int RowCount { get; set; }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //this.GridView1.PageIndex = e.NewPageIndex;
            //this.GridView1.DataBind(); // you data bind code
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

        protected void listView_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            listView.Items.Count.ToString();

        }

        protected void listView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //     RowCount++;
            try
            {
                ListViewItem row = e.Item;
                if (row.ItemType == ListViewItemType.DataItem)
                {

                    ContributionLimitedToCreatorAttribute contributionLimitedToCreator
                        = table.GetAttribute<ContributionLimitedToCreatorAttribute>();
                    if (contributionLimitedToCreator != null)
                    {
                        if (!contributionLimitedToCreator.CanContributeOnDataItem(row.DataItem))
                        {
                            row.Controls.Clear();
                            row.DataItem = null;
                        }
                    }
                    if(DateTime.Parse(DataBinder.Eval(row.DataItem,"Expire_Date").ToString()) <  DateTime.Now)
                    {
                        row.Controls.Clear();
                        row.DataItem = null;
                    }
                }
            }
            catch { }
        }
        protected void Label_Load(object sender, EventArgs e)
        {
            Label label = sender as Label;
            var fname = table.Attributes.OfType<DynamicDataLibrary.Attributes.CustomViewAttribute>().FirstOrDefault();
            if (fname != null && label.Text.Contains("Column"))
            {
                string text = GetGlobalResourceObject(fname.Coulmn_Names_File_Name, label.Text).ToString();
                label.Text = text;

            }
        }

        protected void listView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void DataPager1_PreRender(object sender, EventArgs e)
        {

        }

        protected void listView_DataBound(object sender, EventArgs e)
        {
        }


        protected void PagerCommand(object sender, DataPagerCommandEventArgs e)
        {
            e.NewMaximumRows = e.Item.Pager.MaximumRows;
            switch (e.CommandName)
            {
                case "Next":
                    if (e.Item.Pager.StartRowIndex < e.TotalRowCount - e.Item.Pager.MaximumRows)
                        e.NewStartRowIndex = e.Item.Pager.StartRowIndex + e.Item.Pager.MaximumRows;
                    var n = listView.FindControlRecursive<LinkButton>("navNextbtn");
                    if (e.TotalRowCount <= e.Item.Pager.StartRowIndex + e.Item.Pager.MaximumRows)
                        n.Enabled = false;
                    else
                        n.Enabled = true;
                    break;
                case "Previous":
                    if (e.Item.Pager.StartRowIndex - e.Item.Pager.MaximumRows >= 0)
                        e.NewStartRowIndex = e.Item.Pager.StartRowIndex - e.Item.Pager.MaximumRows;
                    var p = listView.FindControlRecursive<LinkButton>("navPrevbtn");
                    if (e.Item.Pager.StartRowIndex <= e.Item.Pager.MaximumRows)
                        p.Enabled = false;
                    else
                        p.Enabled = true;
                    break;
                case "First":
                    e.NewStartRowIndex = 0;
                    var pf = listView.FindControlRecursive<LinkButton>("navPrevbtn");
                    pf.Enabled = false;
                    break;
                case "Last":
                    e.NewStartRowIndex = e.TotalRowCount - e.Item.Pager.MaximumRows;
                    var nl = listView.FindControlRecursive<LinkButton>("navNextbtn");
                    nl.Enabled = false;
                    break;
            }
        }

        protected void num_SelectedIndexChanged(object sender, EventArgs e)
        {
            var p = listView.FindControlRecursive<DataPager>("DataPager1");
            p.SetPageProperties(int.Parse(((DropDownList)sender).SelectedValue) * p.MaximumRows, p.MaximumRows, true);
            ViewState.Add("pager", ((DropDownList)sender).SelectedValue);
        }


        public HyperLink link_url { get; set; }
        public HyperLink linkimg_url { get; set; }
        protected void imglink2_PreRender(object sender, EventArgs e)
        {
            link_url = sender as HyperLink;
        }
        protected void imglink1_PreRender(object sender, EventArgs e)
        {
            linkimg_url = sender as HyperLink;
        }
        protected void DynamicHyperLink1_PreRender(object sender, EventArgs e)
        {
            var hy = sender as DynamicHyperLink;
            var id = "";
            var ac = "";
            DynamicDataHelper.TransolateRouteURl(hy, out id, out ac);
            DynamicDataHelper.RewriteRouteUrl(hy, id, ac);
            if (hy != null)
            {
                link_url.NavigateUrl = hy.NavigateUrl;
                linkimg_url.NavigateUrl = hy.NavigateUrl;
            }
        }

        protected void DateFilter_Querying(object sender, CustomExpressionEventArgs e)
        {
            using (var db = new Entities())
            {
                DateTime dt = DateTime.Now;
                e.Query = from a in e.Query.Cast<Property>()
                          where a.Expire_Date >= dt
                          select a;
            }
        }
    }
}