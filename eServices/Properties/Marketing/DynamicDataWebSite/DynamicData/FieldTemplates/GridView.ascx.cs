using DynamicDataLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.DynamicData;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;
using DynamicDataLibrary;
using DynamicDataModel.Model;
using NotAClue.Web.DynamicData;
using DynamicDataLibrary;

namespace DynamicDataWebSite
{
    public partial class GridViewField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected MetaTable table;

        public bool EnableDelete { get; set; }

        public bool EnableUpdate { get; set; }

        public bool EnableInsert { get; set; }

        public string[] DisplayColumns { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.table = this.Column.FigureOutForeignOrChildTable();
            if (this.table == null)
                // throw an error if set on column other than MetaChildrenColumns
                throw new InvalidOperationException("The GridView FieldTemplate can only be used with One-To-One or One-To-Many relation!");


            this.GridView1.SetMetaTable(table);

            var attribute = Column.Attributes.OfType<ShowColumnsAttribute>().FirstOrDefault();
            if(attribute == null)
                attribute = table.Attributes.OfType<ShowColumnsAttribute>().FirstOrDefault();
            ShowColumnsAttribute.HideIfCannotView(attribute, this, table, this.Context);
            
            if (attribute != null)
            {
                if (attribute.DisplayColumns.Length > 0)
                    DisplayColumns = attribute.DisplayColumns;

                if (attribute.PageSize != 0)
                    this.GridView1.PageSize = attribute.PageSize;
            }

            //GridDataSource.ContextTypeName = metaChildColumn.ChildTable.DataContextType.Name;
            GridDataSource.ContextTypeName = "DynamicDataModel.Model.Entities";

            GridDataSource.TableName = table.Name;

            Initialize();
        }

        private void HideIfCannotView(ShowColumnsAttribute showColumnsAttribute)
        {
            bool canView = DynamicDataLibrary.AuthorizationManager.CheckUrlAccessForPrincipal(table.GetActionPath(PageAction.List), Context.User);

            if (!canView &&
                (showColumnsAttribute == null || !showColumnsAttribute.EnableViewEvenWithNoPermission))
            {
                this.Parent.Parent.Visible = false;
            }
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

            GridDataSource.AutoGenerateWhereClause = true;

            //To Order By:
            var displayColumnAtt = table
                .Attributes.OfType<DisplayColumnAttribute>().FirstOrDefault();
            if (displayColumnAtt != null)
            {
                GridDataSource.OrderBy = "it." + displayColumnAtt.SortColumn;
                if (displayColumnAtt.SortDescending)
                    GridDataSource.OrderBy += " desc";
            }
            else
                GridDataSource.OrderBy =
                    String.Concat(table.PrimaryKeyColumns.Select(c => "it." + c.Name + " desc ,")).TrimEnd(',');

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            var attribute = Column.Attributes.OfType<ShowColumnsAttribute>().SingleOrDefault();
            if (attribute != null && attribute.HideHeader)
                this.GridView1.HeaderStyle.CssClass = "Hidden";
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            this.AddWhereAndOrderByToDataSource(this.GridDataSource, this.table, this.Request);
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
                //ViewLimitedToCurrentUserAttribute dataLimitedToCurrentUser = table.GetAttribute<ViewLimitedToCurrentUserAttribute>();
                //if (dataLimitedToCurrentUser != null)
                //{
                //    Nullable<int> numberOfItems;
                //    dataLimitedToCurrentUser.Handle(e, this.table, this.Page, out numberOfItems);
                //}
            }
        }
    }
}
