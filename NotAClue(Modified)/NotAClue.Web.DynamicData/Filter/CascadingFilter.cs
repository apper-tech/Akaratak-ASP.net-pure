using System;
using System.Web.DynamicData;
using System.Web.UI.WebControls;
using NotAClue.ComponentModel.DataAnnotations;
using System.Linq;

namespace NotAClue.Web.DynamicData
{
    /// <summary>
    /// Modifies the standard FieldTEmplateUserControl 
    /// to support cascading of selected values.
    /// </summary>
    public class CascadingFilter : QueryableFilterUserControl, ICascadingControl
    {
        #region Properties
        /// <summary>
        /// Parent column of this column named in metadata
        /// </summary>
        public MetaForeignKeyColumn ParentColumn { get; private set; }

        /// <summary>
        /// This FieldTemplates column as MetaForeignKeyColumn
        /// </summary>
        public MetaForeignKeyColumn ChildColumn { get; private set; }

        /// <summary>
        /// Parent control acquired from ParentColumn 
        /// </summary>
        public ICascadingControl ParentControl { get; set; }

        /// <summary>
        /// Controls selected value
        /// </summary>
        public String SelectedValue { get; set; }
        #endregion

        protected override void OnInit(EventArgs e)
        {
            // get the parent column
            var parentColumn = Column.GetAttributeOrDefault<CascadeAttribute>().ParentColumn;
            if (!String.IsNullOrEmpty(parentColumn))
                ParentColumn = Column.Table.GetColumn(parentColumn) as MetaForeignKeyColumn;

            // cast Column as MetaForeignKeyColumn
            ChildColumn = Column as MetaForeignKeyColumn;

            // get parent field (note you must specify the
            // container control type in <DetailsView>, <FormView> or <ListView>
            if (ParentColumn != null)
                ParentControl = this.GetParentControl<QueryableFilterRepeater>(ParentColumn.Name);

            base.OnInit(e);
        }

        /// <summary>
        /// Publish event for when selection changes.
        /// </summary>
        /// <remarks></remarks>
        public event SelectionChangedEventHandler SelectionChanged;

        /// <summary>
        /// Raises the event checking first that an event if hooked up
        /// </summary>
        /// <param name="value">The value of the currently selected item</param>
        public void RaiseSelectedIndexChanged(String value, String text)
        {
            // make sure we have a handler attached
            if (SelectionChanged != null)
            {
                //raise event
                SelectionChanged(this, new SelectionChangedEventArgs(value, text));
            }
        }

        public override IQueryable GetQueryable(IQueryable source)
        {
            return source;
        }
        
        /// <summary>
        /// Advanced populate list control.
        /// </summary>
        /// <param name="listControl">The list control.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <remarks></remarks>
        protected void PopulateListControl(ListControl listControl, String filterValue)
        {
            //get the parent column
            if (ParentColumn == null)
            {
                // if no parent column then just call
                // the base to populate the control
                PopulateListControl(listControl);
                // make sure control is enabled
                listControl.Enabled = true;
            }
            else if (String.IsNullOrEmpty(filterValue))
            {
                // if there is a parent column but no filter value
                // then make sure control is empty and disabled
                listControl.Items.Clear();
                var att = ChildColumn.Attributes.OfType<CascadeAttribute>().FirstOrDefault();
                if (att != null)
                {
                    listControl.Items.Add(new ListItem(Convert.ToString(GetGlobalResourceObject("RealEstate", att.Child_Select_First)), ""));
                }
                else
                {
                    listControl.Items.Add(new ListItem(Convert.ToString(GetGlobalResourceObject("DynamicData", "All")), ""));
                }
                // make sure control is disabled
                listControl.Enabled = false;
            }
            else
            {
                // get the child columns parent table
                var childTable = ChildColumn.ParentTable;

                // get query {Table(Developer).OrderBy(d => d.Name)}
                var query = ChildColumn.ParentTable.GetQuery(Column.Table.CreateContext());

                // filter the query by the parent
                var itemlist = query.GetQueryFilteredByParent(ParentColumn, filterValue);

                // clear list controls items collection before adding new items
                listControl.Items.Clear();
                listControl.Items.Add(new ListItem(Convert.ToString(GetGlobalResourceObject("DynamicData", "All")),""));

                // add returned values to list control
                foreach (var row in itemlist)
                {
                    listControl.Items.Add(
                        new ListItem(
                            childTable.GetDisplayString(row),
                            childTable.GetPrimaryKeyString(row)));
                }

                // make sure control is enabled
                listControl.Enabled = true;
            }
        }
        protected void PopulateListControl(ListControl listControl, String filterValue, String listValue)
        {
            //get the parent column
            if (ParentColumn == null)
            {
                // if no parent column then just call
                // the base to populate the control
                PopulateListControl(listControl);
                // make sure control is enabled
                listControl.Enabled = true;
            }
            else if (String.IsNullOrEmpty(filterValue))
            {
                // if there is a parent column but no filter value
                // then make sure control is empty and disabled
                listControl.Items.Clear();
                var att = ChildColumn.Attributes.OfType<CascadeAttribute>().FirstOrDefault();
                if (att != null)
                {
                    listControl.Items.Add(new ListItem(Convert.ToString(GetGlobalResourceObject("RealEstate", att.Child_Select_First)), ""));
                }
                else
                {
                    listControl.Items.Add(new ListItem(Convert.ToString(GetGlobalResourceObject("DynamicData", "All")), ""));
                }
                // make sure control is disabled
                listControl.Enabled = false;
            }
            else
            {
                // get the child columns parent table
                var childTable = ChildColumn.ParentTable;

                // get query {Table(Developer).OrderBy(d => d.Name)}
                var query = ChildColumn.ParentTable.GetQuery(Column.Table.CreateContext());

                // filter the query by the parent
                var itemlist = query.GetQueryFilteredByParent(ParentColumn, filterValue);

                // clear list controls items collection before adding new items
                listControl.Items.Clear();
                listControl.Items.Add(new ListItem(Convert.ToString(GetGlobalResourceObject("DynamicData", "All")), ""));

                // add returned values to list control
                foreach (var row in itemlist)
                {
                    listControl.Items.Add(
                        new ListItem(
                            childTable.GetDisplayString(row),
                            childTable.GetPrimaryKeyString(row)));
                }

                // make sure control is enabled
                listControl.Enabled = true;
                if (listValue != "0")
                    listControl.SelectedValue = listValue;
            }
        }
    }
}