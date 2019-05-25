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
    public class CascadingFieldTemplate : FieldTemplateUserControl, ICascadingControl
    {
        /// <summary>
        /// Controls selected value
        /// </summary>
        public String SelectedValue { get; private set; }

        /// <summary>
        /// Parent column of this column named in meta data
        /// </summary>
        public MetaForeignKeyColumn ParentColumn { get; private set; }

        /// <summary>
        /// This FieldTemplates column as MetaForeignKeyColumn
        /// </summary>
        public MetaForeignKeyColumn ChildColumn { get; private set; }
        private string NullValueString { get { return Convert.ToString(GetGlobalResourceObject("DynamicData", "Null")); } }
        /// <summary>
        /// Parent control acquired from ParentColumn 
        /// </summary>
        public ICascadingControl ParentControl { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has events.
        /// </summary>
        /// <remarks></remarks>
        public new Boolean HasEvents
        {
            get { return SelectionChanged != null; }
        }

        //publish event
        public event SelectionChangedEventHandler SelectionChanged;

        protected override void OnInit(EventArgs e)
        {
            // get the parent column
            var parentColumn = Column.GetAttributeOrDefault<CascadeAttribute>().ParentColumn;
            if (!String.IsNullOrEmpty(parentColumn))
                ParentColumn = Column.Table.GetColumn(parentColumn) as MetaForeignKeyColumn;

            // cast Column as MetaForeignKeyColumn
            ChildColumn = Column as MetaForeignKeyColumn;

            // get parent field (note you must specify the
            // container control type in <ListView> <GridView>, <CompositeDataBoundControl> equivalent to FormView or ListView
            var s = new GridView();
            if (ParentColumn != null)
                ParentControl = this.GetParentControl<DataBoundControl>(ParentColumn.Name);

            // call base
            base.OnInit(e);
        }

        /// <summary>
        /// Raises the event checking first that an event if hooked up
        /// </summary>
        /// <param name="value">The value of the currently selected item</param>
        public void RaiseSelectedIndexChanged(String value, String text)
        {
            // make sure we have a handler attached
            if (SelectionChanged != null)
                SelectionChanged(this, new SelectionChangedEventArgs(value, text));
        }

        // advanced populate list control
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

                if (Mode == DataBoundControlMode.Insert || !Column.IsRequired)
                {
                    var att = ChildColumn.Attributes.OfType<CascadeAttribute>().FirstOrDefault();
                    if (att != null)
                    {
                        listControl.Items.Add(new ListItem(Convert.ToString(GetGlobalResourceObject("RealEstate", att.Child_Select_First)), ""));
                    }
                    else
                    {
                        listControl.Items.Add(new ListItem(Convert.ToString(GetGlobalResourceObject("DynamicData", "All")), ""));
                    }
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

                // get list of values filtered by the parent's selected entity
                var itemlist = query.GetQueryFilteredByParent(ParentColumn, filterValue);

                // clear list controls items collection before adding new items
                listControl.Items.Clear();

                // only add [Not Set] if in insert mode or column is not required
                if (Mode == DataBoundControlMode.Insert && !Column.IsRequired)
                    listControl.Items.Add(new ListItem(Convert.ToString(GetGlobalResourceObject("DynamicData", "NotSet")), NullValueString));
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
    }
}