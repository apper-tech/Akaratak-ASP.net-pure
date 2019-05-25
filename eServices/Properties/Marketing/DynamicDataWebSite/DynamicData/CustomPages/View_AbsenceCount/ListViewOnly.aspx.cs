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

namespace DynamicDataWebSite.DynamicData.CustomPages.Absences
{
    /// <summary>
    /// This is made basically to have red background for continues absence larger than or equal 7.
    /// </summary>
    /// <remarks>
    /// I used in this LinqDataSource, but also could use EntityDataSouce.
    /// </remarks>
    public partial class ListViewOnly : System.Web.UI.Page
    {
        protected MetaTable table;

        protected void Page_Init(object sender, EventArgs e)
        {
            table = DynamicDataRouteHandler.GetRequestMetaTable(Context);
            GridView1.SetMetaTable(table, table.GetColumnValuesFromRoute(Context));

            //LinqDataSource.ContextTypeName = "AdventureWorksDataContext";

            LinqDataSource.ContextTypeName = table.DataContextType.Name;
            LinqDataSource.ContextTypeName = "DynamicDataModel.Model.Entities";
            LinqDataSource.TableName = table.EntityType.Name;
            LinqDataSource.OrderBy = "it.Name, it.Date descending";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = table.DisplayName;
            //GridDataSource.Include += ",ContinuesCount";
            // Disable various options if the table is readonly
            if (table.IsReadOnly)
            {
                GridView1.EnablePersistedSelection = false;
            }

            //var displayColumn = table.GetAttribute<DisplayColumnAttribute>();
            //if (displayColumn != null && displayColumn.SortColumn != null)
            //{
            //    GridView1.Sort(displayColumn.SortColumn,
            //        displayColumn.SortDescending ? SortDirection.Descending : SortDirection.Ascending);
            //}
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
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            RouteValueDictionary routeValues = new RouteValueDictionary(GridView1.GetDefaultValues());
            base.OnPreRenderComplete(e);
        }

        protected void DynamicFilter_FilterChanged(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
        }

        protected void LinqDataSource_QueryCreated(object sender, QueryCreatedEventArgs e)
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if ((e.Row.DataItem as View_AbsenceCount).ContinuesCount >= 7)
                //{
                //    e.Row.CssClass = "GridViewRowAlert";
                //}
            }
        }




    }
}