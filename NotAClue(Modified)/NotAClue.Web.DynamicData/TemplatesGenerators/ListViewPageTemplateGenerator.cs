/// <copyright file="ListViewPageTemplateGenerator.cs" company="NotAClue? Studios">
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
using System.Text;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NotAClue.ComponentModel.DataAnnotations;

namespace NotAClue.Web.DynamicData
{
    /// <summary>
    /// defines the delegate for getting a list of columns
    /// </summary>
    /// <param name="table">A MetataTable</MetataTable></param>
    /// <returns>An IEnumerable of type MetaColumn</returns>
    public delegate IEnumerable<MetaColumn> GetVisibleColumns(MetaTable table);

    /// <summary>
    /// Renders templates for ListView
    /// </summary>
    public class ListViewTemplateGenerator : ITemplate
    {
        #region Member Fields, Constructor, Enums & Properties
        private const string EnumerationField = "Enumeration";

        private int cellPadding = 6;
        private MetaTable _table;
        private Page _page;
        private ListViewTemplateType _type;
        private GetVisibleColumns _getVisdibleColumns;
        private CommandButtons _enabledButtons;
        private String _validationGroup;
        private Boolean _showPager;
        private Boolean _addFooter;
        private String _cssClass;
        private String _imageFolderBase = "~/DynamicData/Content/Images/";

        public ListViewTemplateGenerator(Page page, MetaTable table, ListViewTemplateType type, CommandButtons enabledButtons, String validationGroup, GetVisibleColumns lambda, Boolean showPager, Boolean addFooter, String cssClass)
        {
            _page = page;
            _table = table;
            _type = type;
            _enabledButtons = enabledButtons;
            _validationGroup = validationGroup;
            _getVisdibleColumns = lambda;
            _showPager = showPager;
            _addFooter = addFooter;
            _cssClass = cssClass;
        }
        #endregion

        public void InstantiateIn(Control container)
        {
            IParserAccessor accessor = container;
            IEnumerable<MetaColumn> visibleColumns;
            // get all the all scaffold columns 
            //var template = _page.GetPageTemplate() != PageTemplate.FormViewChildListViews ? _page.GetPageTemplate() : PageTemplate.List;
            if (_getVisdibleColumns == null)
                visibleColumns = _table.GetVisibleColumns(false, _page.GetPageTemplate());
            else
                visibleColumns = _getVisdibleColumns(_table);

            // call the appropriate template generator
            switch (_type)
            {
                case ListViewTemplateType.LayoutTemplate:
                    GetLayoutTemplate(accessor, visibleColumns);
                    break;
                case ListViewTemplateType.ItemTemplate:
                    GetItemTemplate(accessor, visibleColumns, ListViewTemplateMode.Normal);
                    break;
                case ListViewTemplateType.AlternatingItemTemplate:
                    GetItemTemplate(accessor, visibleColumns, ListViewTemplateMode.Alternate);
                    break;
                case ListViewTemplateType.EditItemTemplate:
                    GetItemTemplate(accessor, visibleColumns, ListViewTemplateMode.Edit);
                    break;
                case ListViewTemplateType.InsertItemTemplate:
                    GetItemTemplate(accessor, visibleColumns, ListViewTemplateMode.Insert);
                    break;
                case ListViewTemplateType.SelectedItemTemplate:
                    GetItemTemplate(accessor, visibleColumns, ListViewTemplateMode.Selected);
                    break;
                case ListViewTemplateType.GroupTemplate:
                    GetGroupTemplate(accessor, visibleColumns);
                    break;
                case ListViewTemplateType.GroupSeparatorTemplate:
                    GetGroupSeparatorTemplate(accessor, visibleColumns);
                    break;
                case ListViewTemplateType.ItemSeparatorTemplate:
                    GetItemSeparatorTemplate(accessor, visibleColumns);
                    break;
                case ListViewTemplateType.EmptyDataTemplate:
                    GetEmptyDataTemplate(accessor);
                    break;
                case ListViewTemplateType.EmptyItemTemplate:
                    GetEmptyItemTemplate(accessor, visibleColumns);
                    break;
                default:
                    break;
            }
        }

        private void GetLayoutTemplate(IParserAccessor accessor, IEnumerable<MetaColumn> columnDetails)
        {
            // make sure there are some children columns
            if (columnDetails.Count() > 0)
            {
                // HtmlTable/HtmlTableRow/HtmlTableCell 
                // create table
                var htmlTable = new HtmlTable();
                htmlTable.Attributes.Add("cellspacing", "0");
                htmlTable.Attributes.Add("cellpadding", cellPadding.ToString());
                htmlTable.Attributes.Add("rules", "all");
                htmlTable.Attributes.Add("border", "1");
                htmlTable.Attributes.Add("class", _cssClass);

                // add table to accessor
                accessor.AddParsedSubObject(htmlTable);

                // set commandColumnNo to 0 and then 1 if there is a command column
                var CommandColumnNo = _enabledButtons.HasAnyButton(CommandButtons.Delete, CommandButtons.Details, CommandButtons.Edit, CommandButtons.Insert) ? 1 : 0;

                var commandRow = CreateCommantRow(columnDetails.Count(), CommandColumnNo);
                if (commandRow != null)
                    htmlTable.Rows.Add(commandRow);

                // create header row
                var headerRow = new HtmlTableRow();
                headerRow.Attributes.Add("class", "th");

                if (CommandColumnNo == 1)
                {
                    // create empty header cell
                    var commandCell = new HtmlTableCell("th");
                    headerRow.Cells.Add(commandCell);
                }

                // add a column heading for each column
                foreach (var column in columnDetails)
                {
                    //TODO fix sorting on MetaForeignKeyColumn
                    var cell = new HtmlTableCell("th");

                    //// add css
                    //var css = column.GetAttribute<CssAttribute>();
                    //if (css != null)
                    //    cell.SetAttributeClass("class", css.Class);

                    var linkButton = new LinkButton()
                    {
                        ID = column.Name + "LinkButton",
                        CommandName = "Sort",
                        CommandArgument = column.Name,
                        Text = column.DisplayName,
                        //CssClass = "commandField",
                    };

                    if (column is MetaForeignKeyColumn)
                    {
                        var mfkColumn = column as MetaForeignKeyColumn;
                        var keyNames = new StringBuilder();

                        foreach (var keyName in mfkColumn.ForeignKeyNames)
                        {
                            keyNames.Append(keyName + ",");
                        }
                        linkButton.CommandArgument = keyNames.ToString().Substring(0, keyNames.Length - 1);
                    }

                    cell.Controls.Add(linkButton);
                    headerRow.Cells.Add(cell);
                }
                htmlTable.Rows.Add(headerRow);

                //<tbody>
                //    <tr id="itemPlaceHolder" runat="server">
                //    </tr>
                //</tbody>
                // create the table body and item place holder
                var itemPlaceholder = new HtmlTableRow();
                itemPlaceholder.Attributes.Add("class", "td");
                itemPlaceholder.ID = "itemPlaceholder";
                itemPlaceholder.Attributes.Add("runat", "Server");
                htmlTable.Rows.Add(itemPlaceholder);

                if (_showPager)
                {
                    //<tfoot>
                    //    <tr class="footer">
                    //        <td colspan="14">
                    //            <asp:ListViewPager ID="ListViewDataPager" runat="server"></asp:ListViewPager>
                    //        </td>
                    //    </tr>
                    //</tfoot>
                    // create the footer
                    var footerRow = new HtmlTableRow();
                    footerRow.Attributes.Add("class", "footer");

                    // set column span to number of columns
                    // plus 1 to account for the command column
                    var footerCell = new HtmlTableCell();

                    // set column span
                    footerCell.ColSpan = columnDetails.Count() + CommandColumnNo;

                    // get the path to the ListViewPager
                    var listViewPagerPath = _table.Model.DynamicDataFolderVirtualPath + "Content/ListViewPager.ascx";
                    // load ListViewPager control
                    var listViewPager = _page.LoadControl(listViewPagerPath);
                    listViewPager.ID = "ListViewDataPager";
                    footerCell.Controls.Add(listViewPager);
                    footerRow.Cells.Add(footerCell);
                    htmlTable.Rows.Add(footerRow);
                }

                if (_addFooter)
                {
                    var footerRow = new HtmlTableRow();
                    footerRow.Attributes.Add("class", "totals");

                    if (_enabledButtons.HasAnyButton(CommandButtons.Delete, CommandButtons.Details, CommandButtons.Edit))
                    {
                        var commandCell = new HtmlTableCell("td");
                        footerRow.Cells.Add(commandCell);
                    }

                    // add a column heading for each column
                    foreach (var column in columnDetails)
                    {
                        // fix sorting on MetaForeignKeyColumn
                        var cell = new HtmlTableCell("td");

                        //// add css
                        //var css = column.GetAttribute<CssAttribute>();
                        //if (css != null)
                        //    cell.SetAttributeClass("class", css.Class);

                        var litPlaceHolder = new Literal()
                        {
                            ID = column.Name + "PlaceHolder",
                            Text = "0"
                        };

                        cell.Controls.Add(litPlaceHolder);
                        footerRow.Cells.Add(cell);
                    }
                    htmlTable.Rows.Add(footerRow);
                }
            }
            // if there are no children columns don't
            // bother to set the accessor to anything
        }

        private void GetItemTemplate(IParserAccessor accessor, IEnumerable<MetaColumn> columnDetails, ListViewTemplateMode templateMode)
        {
            // make sure there are some children columns
            if (columnDetails.Count() > 0)
            {
                // set the dynamic control mode to read only
                DataBoundControlMode mode = DataBoundControlMode.ReadOnly;

                var validationGroup = _validationGroup + "_" + templateMode.ToString();

                // create a spacer
                var litSpacer = new Literal();
                litSpacer.Text = @"&nbsp;";

                // create new row for template
                var row = new HtmlTableRow();
                row.Attributes.Add("class", "td");

                // add row to accessor
                accessor.AddParsedSubObject(row);

                if (_enabledButtons.HasAnyButton(CommandButtons.Delete, CommandButtons.Details, CommandButtons.Edit, CommandButtons.Insert))
                {
                    // create the cell to hold the command buttons
                    // NOTE: you could move the command cell to after the
                    //       foreach column generator code if you wanted
                    //       the command buttons to be at the end of the row
                    //       you would also need to modify the LayoutTemplate code
                    var commandCell = new HtmlTableCell();
                    commandCell.Attributes.Add("class", "nowrap");
                    row.Cells.Add(commandCell);

                    // set appropriate variable depending
                    // on what mode the row is to be in
                    switch (templateMode)
                    {
                        case ListViewTemplateMode.Edit:
                            mode = DataBoundControlMode.Edit;

                            //Update Link
                            var lbUpdate = new LinkButton()
                            {
                                //ID = "UpdateLinkButton",
                                CommandName = "Update",
                                Text = "Update",
                                //AlternateText = "Update",
                                //ImageUrl = _imageFolderBase + "Save.png",
                                //CssClass = "commandField",
                                ToolTip = "Update current action"
                            };
                            if (!String.IsNullOrEmpty(_validationGroup))
                                lbUpdate.ValidationGroup = validationGroup;

                            commandCell.Controls.Add(lbUpdate);
                            commandCell.Controls.Add(litSpacer);

                            //Cancel Link
                            var lbCancel = new LinkButton()
                            {
                                //ID = "UpdateLinkButton",
                                CommandName = "Cancel",
                                Text = "Cancel",
                                //AlternateText = "Cancel",
                                //ImageUrl = _imageFolderBase + "Cancel.png",
                                //CssClass = "commandField",
                                ToolTip = "Cancel current action"
                            };
                            commandCell.Controls.Add(lbCancel);
                            break;
                        case ListViewTemplateMode.Insert:
                            mode = DataBoundControlMode.Insert;

                            //Insert Link
                            var lkbInsert = new LinkButton()
                            {
                                //ID = "InsertLinkButton",
                                CommandName = "Insert",
                                Text = "Save",
                                //AlternateText = "Insert",
                                //ImageUrl = _imageFolderBase + "Insert.png",
                                //CssClass = "commandField",
                                ToolTip = "Insert new record"
                            };
                            if (!String.IsNullOrEmpty(_validationGroup))
                                lkbInsert.ValidationGroup = validationGroup;

                            commandCell.Controls.Add(lkbInsert);
                            commandCell.Controls.Add(litSpacer);

                            //Cancel Link
                            var lkbCancel = new LinkButton()
                            {
                                //ID = "CancelLinkButton",
                                CommandName = "Cancel",
                                Text = "Cancel",
                                //AlternateText = "Cancel",
                                //ImageUrl = _imageFolderBase + "Cancel.png",
                                //CssClass = "commandField",
                                ToolTip = "Cancel current action",
                                CausesValidation = false,
                            };
                            commandCell.Controls.Add(lkbCancel);
                            break;
                        case ListViewTemplateMode.Selected:
                            row.Attributes.Add("class", "selected");
                            goto case ListViewTemplateMode.Normal;
                        case ListViewTemplateMode.Alternate:
                            row.Attributes.Add("class", "alternate");
                            goto case ListViewTemplateMode.Normal;
                        case ListViewTemplateMode.Normal:
                            if (_enabledButtons.HasAnyButton(CommandButtons.Edit))
                            {
                                //Edit Link
                                var lkbEdit = new LinkButton()
                                {
                                    //ID = "EditLinkButton",
                                    CommandName = "Edit",
                                    Text = "Edit",
                                    //AlternateText = "Edit",
                                    //ImageUrl = _imageFolderBase + "Edit.png",
                                    //CssClass = "commandField",
                                    ToolTip = "Edit current record",
                                    CausesValidation = false,
                                };
                                commandCell.Controls.Add(lkbEdit);
                                commandCell.Controls.Add(litSpacer);
                            }

                            if (_enabledButtons.HasAnyButton(CommandButtons.Delete))
                            {
                                //Delete Link
                                var lkbDelete = new LinkButton()
                                {
                                    //ID = "DeleteLinkButton",
                                    CommandName = "Delete",
                                    Text = "Delete",
                                    //AlternateText = "Delete",
                                    //ImageUrl = _imageFolderBase + "Delete.png",
                                    //CssClass = "commandField",
                                    CausesValidation = true,
                                    OnClientClick = "return confirm(\"Are you sure you want to delete this item?\");",
                                };
                                commandCell.Controls.Add(lkbDelete);
                                if (!String.IsNullOrEmpty(_validationGroup))
                                    lkbDelete.ValidationGroup = validationGroup;
                            }

                            if (_enabledButtons.HasAnyButton(CommandButtons.Details))
                            {
                                var dynamicHyperLink = new DynamicHyperLink()
                                {
                                    ID = "DetailsLinkButton",
                                    Text = "Details",
                                    //ImageUrl = _imageFolderBase + "Details.png",
                                    ToolTip = "Show details"
                                };

                                commandCell.Controls.Add(dynamicHyperLink);
                                commandCell.Controls.Add(litSpacer);
                            }
                            break;
                        default:
                            break;
                    }
                }

                // add a cell for each column in table
                foreach (var column in columnDetails)
                {
                    // create new cell
                    var cell = new HtmlTableCell();

                    //// add css
                    //var css = column.GetAttribute<CssAttribute>();
                    //if (css != null)
                    //    cell.SetAttributeClass("class", css.Class);

                    // add cell to row
                    row.Cells.Add(cell);

                    // instantiate a DynamicControl for this Children Column
                    var lvColumn = new DynamicControl(mode)
                    {
                        ID = column.Name,
                        // set data field to column name
                        DataField = column.Name,
                        UIHint = //column.UIHint
                            column.ConditionalUIHint(_page.GetPageTemplate())
                    };

                    if (!String.IsNullOrEmpty(_validationGroup))
                        lvColumn.ValidationGroup = validationGroup;

                    //if (column.ColumnType.IsEnum)
                    //    lvColumn.UIHint = EnumerationField;

                    //if (column.IsForeignKeyComponent || column.ColumnType == typeof(MetaChildrenColumn))
                    //    lvColumn.AllowNavigation = false;

                    // add control to cell
                    cell.Controls.Add(lvColumn);
                }
            }
            // if there are no children columns don't
            // bother to set the accessor to anything
        }

        private void GetGroupTemplate(IParserAccessor accessor, IEnumerable<MetaColumn> columnDetails)
        {
            throw new NotImplementedException();
        }

        private void GetGroupSeparatorTemplate(IParserAccessor accessor, IEnumerable<MetaColumn> columnDetails)
        {
            throw new NotImplementedException();
        }

        private void GetItemSeparatorTemplate(IParserAccessor accessor, IEnumerable<MetaColumn> columnDetails)
        {
            throw new NotImplementedException();
        }

        private void GetEmptyDataTemplate(IParserAccessor accessor)
        {
            // HtmlTable/HtmlTableRow/HtmlTableCell 
            // create table
            var htmlTable = new HtmlTable();
            htmlTable.Attributes.Add("cellspacing", "0");
            htmlTable.Attributes.Add("cellpadding", cellPadding.ToString());
            htmlTable.Attributes.Add("rules", "all");
            htmlTable.Attributes.Add("border", "1");
            htmlTable.Attributes.Add("class", _cssClass);

            // add table to accessor
            accessor.AddParsedSubObject(htmlTable);

            var commandRow = CreateCommantRow(2, 0);
            if (commandRow != null)
                htmlTable.Rows.Add(commandRow);

            // create new row for template
            var row = new HtmlTableRow();
            row.Attributes.Add("class", "td");

            // add row to htmlTable rows
            htmlTable.Rows.Add(row);

            // create new cell
            var cell = new HtmlTableCell();
            cell.ColSpan = 2;

            // add cell to row
            row.Cells.Add(cell);

            // create a spacer
            cell.InnerText = @"There are no " + _table.DisplayName;
        }

        private void GetEmptyItemTemplate(IParserAccessor accessor, IEnumerable<MetaColumn> columnDetails)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the commant row.
        /// </summary>
        /// <param name="columnCount">The column count.</param>
        /// <returns></returns>
        public HtmlTableRow CreateCommantRow(int columnCount, int commandButtons)
        {
            if (!_enabledButtons.HasAnyButton(CommandButtons.Insert, CommandButtons.Refresh))
                return null;

            var commandRow = new HtmlTableRow();

            var cols = columnCount + commandButtons;

            commandRow.Attributes.Add("class", "th");
            var insertCell = new HtmlTableCell("th");
            insertCell.Attributes.Add("class", "InsertHeader");
            insertCell.ColSpan = (cols / 2);
            if (_enabledButtons.HasAnyButton(CommandButtons.Insert))
                insertCell.Controls.Add(CreateLinkWithImage("ShowInsert", String.Format("Insert new {0}", _table.DisplayName)));

            commandRow.Cells.Add(insertCell);

            if (cols % 2 != 0)
            {
                var paddingCell = new HtmlTableCell("th");
                paddingCell.Attributes.Add("class", "PaddingHeader");
                commandRow.Cells.Add(paddingCell);
            }

            var refreshCell = new HtmlTableCell("th");
            refreshCell.ColSpan = (cols / 2);
            refreshCell.Attributes.Add("class", "RefreshHeader");
            if (_enabledButtons.HasAnyButton(CommandButtons.Refresh))
                refreshCell.Controls.Add(CreateLinkWithImage("Refresh", "Refresh"));

            commandRow.Cells.Add(refreshCell);

            return commandRow;
        }

        /// <summary>
        /// Creates the link with image.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="command">if set to <c>true</c> [command].</param>
        /// <returns></returns>
        public Control CreateLinkWithImage(String action, String message)
        {
            var placeHolder = new PlaceHolder();
            var displayText = String.Format("{0}", message);

            //// new image button
            //var imageButton = new ImageButton();
            //imageButton.CssClass = "InsertImage";
            //imageButton.ImageUrl = String.Format("{0}{1}.png", _imageFolderBase, action);
            //imageButton.ToolTip = displayText;
            //imageButton.CommandName = action;
            //placeHolder.Controls.Add(imageButton);

            //var spacer = new Literal();
            //spacer.Text = "&nbsp;";
            //placeHolder.Controls.Add(spacer);

            var linkButton = new LinkButton();
            linkButton.CssClass = "TableHeadderText";
            linkButton.Text = displayText;
            linkButton.CommandName = action;
            placeHolder.Controls.Add(linkButton);

            return placeHolder;
        }
    }

    public static partial class HelperExtensionMethods
    {
        private static String _layoutContainerId = "layoutTemplate";
        private static String _itemPlaceHolderId = "itemPlaceHolder";

        public static void GetTemplates(this ListView listView, MetaTable table)
        {
            listView.GetTemplates(table, null, Constants.ListViewButtons, "", true, false, "gridview");
        }

        public static void GetTemplates(this ListView listView, MetaTable table, String validationGroup, Boolean showPager, CommandButtons enabledButtons, Boolean addFooter)
        {
            listView.GetTemplates(table, null, enabledButtons, validationGroup, showPager, addFooter, "gridview");
        }

        public static void GetTemplates(this ListView listView, MetaTable table, CommandButtons enabledButtons)
        {
            listView.GetTemplates(table, null, enabledButtons, "", true, false, "gridview");
        }

        public static void GetTemplates(this ListView listView, MetaTable table, CommandButtons enabledButtons, String validationGroup, String cssClass)
        {
            listView.GetTemplates(table, null, enabledButtons, validationGroup, true, false, cssClass);
        }

        public static void GetTemplates(this ListView listView, MetaTable table, GetVisibleColumns getVisibleColumns, CommandButtons enabledButtons, String validationGroup, Boolean showPager, Boolean addFooter, String cssClass)
        {
            // get template path
            var listViewTemplatePath = table.Model.DynamicDataFolderVirtualPath + "Templates/ListViewPage/" + table.Name + "/";

            // load layout template
            if (File.Exists(listView.Page.Server.MapPath(listViewTemplatePath + "LayoutTemplate.ascx")))
            {
                // set Item Placeholder ID
                listView.ItemPlaceholderID = _layoutContainerId + "$" + _itemPlaceHolderId;

                // add event handler to handle the loaded user control
                listView.LayoutTemplate = listView.Page.LoadTemplate(listViewTemplatePath + "LayoutTemplate.ascx");
            }
            else
                listView.LayoutTemplate = new ListViewTemplateGenerator(listView.Page, table, ListViewTemplateType.LayoutTemplate, enabledButtons, validationGroup, getVisibleColumns, showPager, addFooter, cssClass);

            // load item template
            if (File.Exists(listView.Page.Server.MapPath(listViewTemplatePath + "ItemTemplate.ascx")))
                listView.ItemTemplate = listView.Page.LoadTemplate(listViewTemplatePath + "ItemTemplate.ascx");
            else
                listView.ItemTemplate = new ListViewTemplateGenerator(listView.Page, table, ListViewTemplateType.ItemTemplate, enabledButtons, validationGroup, getVisibleColumns, showPager, addFooter, cssClass);

            // load edit template
            if (File.Exists(listView.Page.Server.MapPath(listViewTemplatePath + "EditItemTemplate.ascx")))
                listView.EditItemTemplate = listView.Page.LoadTemplate(listViewTemplatePath + "EditItemTemplate.ascx");
            else
                listView.EditItemTemplate = new ListViewTemplateGenerator(listView.Page, table, ListViewTemplateType.EditItemTemplate, enabledButtons, validationGroup, getVisibleColumns, showPager, addFooter, cssClass);

            // load insert template
            if (File.Exists(listView.Page.Server.MapPath(listViewTemplatePath + "InsertItemTemplate.ascx")))
                listView.InsertItemTemplate = listView.Page.LoadTemplate(listViewTemplatePath + "InsertItemTemplate.ascx");
            else
                listView.InsertItemTemplate = new ListViewTemplateGenerator(listView.Page, table, ListViewTemplateType.InsertItemTemplate, enabledButtons, validationGroup, getVisibleColumns, showPager, addFooter, cssClass);

            // set the location of the insert row
            listView.InsertItemPosition = InsertItemPosition.LastItem;

            // load empty template
            listView.EmptyDataTemplate = new ListViewTemplateGenerator(listView.Page, table, ListViewTemplateType.EmptyDataTemplate, enabledButtons, validationGroup, getVisibleColumns, showPager, addFooter, cssClass);
            //listView.AlternatingItemTemplate = null;
            //listView.EmptyItemTemplate = null;
            //listView.GroupSeparatorTemplate = null;
            //listView.GroupTemplate = null;
            //listView.ItemSeparatorTemplate = null;
            //listView.SelectedItemTemplate = null;
        }

        public static Boolean HasAnyButton(this CommandButtons enabledButtons, params CommandButtons[] lvButtons)
        {
            Boolean tested = false;

            foreach (var button in lvButtons)
            {
                if ((enabledButtons & button) == button)
                    tested = true;
            }
            return tested;
        }
    }
}