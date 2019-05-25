/// <copyright file="ChildrenListTemplateGenerator.cs" company="NotAClue? Studios">
/// Copyright (c) 2011 All Right Reserved, http://csharpbits.notaclue.net/
///
/// This source is subject to the per project license unless otherwise agreed.
/// All other rights reserved.
///
/// </copyright>
/// <author>Stephen J Naughton</author>
/// <email>steve@notaclue.net</email>
/// <project>NotAClue.Web.DynamicData</project>
/// <date>24/07/2011</date>
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NotAClue.ComponentModel.DataAnnotations;

namespace NotAClue.Web.DynamicData
{
    /// <summary>
    /// Creates an item template that renders any children columns in the passed in table as GridViews
    /// </summary>
    public class ChildrenListTemplateGenerator : ITemplate
    {
        private String _UIHineName = "ChildrenList";
        private MetaTable _table;
        private Page _page;
        private FormViewTemplateType _type;
        private IEnumerable<MetaChildrenColumn> _columns;
        private Boolean _readOnly;

        public ChildrenListTemplateGenerator(Page page, MetaTable table, FormViewTemplateType type, Boolean readOnly = false)
        {
            _table = table;
            _page = page;
            _type = type;
            _readOnly = readOnly;
        }

        public ChildrenListTemplateGenerator(Page page, MetaTable table, FormViewTemplateType type, IEnumerable<MetaChildrenColumn> columns, Boolean readOnly = false)
            : this(page, table, type)
        {
            _columns = columns;
            _readOnly = readOnly;
        }

        public void InstantiateIn(Control container)
        {
            IParserAccessor accessor = container;
            IEnumerable<MetaChildrenColumn> columns;

            // allow delegate column generator
            if (_columns != null)
                columns = _columns;
            else
                columns = from c in _table.Columns.OfType<MetaChildrenColumn>()
                          where c.GetAttributeOrDefault<ShowChildTablesAttribute>().Enabled
                          orderby c.GetAttributeOrDefault<ShowChildTablesAttribute>().Order, c.DisplayName
                          select c;

            GetItemTemplate(accessor, columns);
        }

        private void GetItemTemplate(IParserAccessor acessor, IEnumerable<MetaChildrenColumn> childTables) //, DataBoundControlMode templateMode)
        {
            // make sure there are some children columns
            if (childTables.Count() > 0)
            {
                //paragraph
                var containerDiv = new HtmlGenericControl("div");

                // add the title container to the page
                acessor.AddParsedSubObject(containerDiv);

                // create tab container to hold each children column
                var tabContainer = new AjaxControlToolkit.TabContainer();
                tabContainer.ID = "tabContainer";

                // add event handler
                tabContainer.AutoPostBack = true;

                // add the tab container to the page
                containerDiv.Controls.Add(tabContainer);

                // add a tab panel for each children table
                foreach (var childTable in childTables)
                {
                    var tabPanel = new AjaxControlToolkit.TabPanel();
                    tabPanel.ID = "tp" + childTable.Name;

                    // add the tab panel
                    tabContainer.Tabs.Add(tabPanel);

                    var subGridAttributes = childTable.GetAttributeOrDefault<ShowChildTablesAttribute>();
                    // set the Tab's name to be the tables display name 
                    // or table Name if no attribute is present
                    if (!String.IsNullOrEmpty(subGridAttributes.TabName))
                        tabPanel.HeaderText = subGridAttributes.TabName;
                    else
                        tabPanel.HeaderText = childTable.ChildTable.DataContextPropertyName.ToTitleFromPascal();

                    //Instantiate a DynamicControl for this Children Column
                    var mode = _readOnly ? DataBoundControlMode.ReadOnly : DataBoundControlMode.Edit;
                    var childrenGrid = new DynamicControl(mode)
                    {
                        ID = childTable.Name,

                        // set UIHint
                        UIHint = _UIHineName,

                        // set data field to column name
                        DataField = childTable.Name,
                        //Mode = childTable.IsReadOnly() ? DataBoundControlMode.ReadOnly : templateMode
                    };

                    // add the DynamicControl to the tab panel
                    tabPanel.Controls.Add(childrenGrid);
                }
                // set the tab panels index to 0 which
                // forces the first tab to be selected
                if (!_page.IsPostBack)
                    tabContainer.ActiveTabIndex = 0;
            }
            else
            {
                //// if no children columns
                //// add label to show no grids
                //var label = new Label();
                //label.Text = "There are no SubGrids";
                //label.CssClass = "field";
                //// add the label to the page
                //acessor.AddParsedSubObject(label);
            }
        }
    }

    public static partial class HelperExtensionMethods
    {
        public static void GetTemplate(this FormView childrenFormView, MetaTable table, Boolean readOnly = false)
        {
            // supported templates ReadOnly, Insert & Update
            // get template path
            var formViewTemplatePath = table.Model.DynamicDataFolderVirtualPath + "Templates/ChildrenList/" + table.Name + "/";

            // load user templates if they exist
            if (File.Exists(childrenFormView.Page.Server.MapPath(formViewTemplatePath + "ItemTemplate.ascx")))
                childrenFormView.ItemTemplate = childrenFormView.Page.LoadTemplate(formViewTemplatePath + "ItemTemplate.ascx");
            else
                childrenFormView.ItemTemplate = new ChildrenListTemplateGenerator(childrenFormView.Page, table, FormViewTemplateType.ItemTemplate, readOnly: readOnly);
        }

        public static void GetTemplate(this FormView childrenFormView, MetaTable table, IEnumerable<MetaChildrenColumn> columns, Boolean readOnly = false)
        {
            childrenFormView.ItemTemplate = new ChildrenListTemplateGenerator(childrenFormView.Page, table, FormViewTemplateType.ItemTemplate, columns, readOnly: readOnly);
        }
    }
}