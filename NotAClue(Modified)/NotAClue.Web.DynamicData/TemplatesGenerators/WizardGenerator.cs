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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NotAClue.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace NotAClue.Web.DynamicData
{
    /// <summary>
    /// Creates an item template that renders any children columns in the passed in table as GridViews
    /// </summary>
    public class WizardGenerator : ITemplate
    {
        private const String TEMPLATE_FOLDER = "Content/";
        private const string SEPERATOR = "$";
        private String _cssClass = "DDWizard";
        private readonly MetaTable _table;
        private readonly Page _page;
        private readonly FormViewTemplateType _type;
        private readonly List<Button> _buttons;

        public WizardGenerator(Page page, MetaTable table, FormViewTemplateType type, List<Button> buttons = null)
        {
            _table = table;
            _page = page;
            _type = type;
            _buttons = buttons;
        }

        public void InstantiateIn(Control container)
        {
            IParserAccessor accessor = container;
            switch (_type)
            {
                case FormViewTemplateType.ItemTemplate:
                    GetItemTemplate(accessor, DataBoundControlMode.ReadOnly);
                    break;
                case FormViewTemplateType.EditItemTemplate:
                    GetItemTemplate(accessor, DataBoundControlMode.Edit);
                    break;
                case FormViewTemplateType.InsertItemTemplate:
                    GetItemTemplate(accessor, DataBoundControlMode.Insert);
                    break;
                case FormViewTemplateType.EmptyDataTemplate:
                case FormViewTemplateType.HeaderTemplate:
                case FormViewTemplateType.FooterTemplate:
                case FormViewTemplateType.PagerTemplate:
                default:
                    break;
            }
        }

        private void GetItemTemplate(IParserAccessor acessor, DataBoundControlMode mode)
        {
            var groupsAttribute = _table.GetAttribute<GroupsAttribute>();
            if (groupsAttribute == null)
                groupsAttribute = new GroupsAttribute(_table.DisplayName);

            var wizardAttribute = _table.GetAttribute<WizardAttribute>();
            if (wizardAttribute == null)
                wizardAttribute = new WizardAttribute();


            // create wizard container to hold each children column
            var FormWizard = new Wizard()
            {
                ID = String.Format("Wizard_{0}", _table.Name),
                HeaderText = groupsAttribute.Title,
                CssClass = wizardAttribute.CssClass,
                DisplayCancelButton = wizardAttribute.ShowCancelButton,
                DisplaySideBar = wizardAttribute.ShowSideBar
            };


            //// get wizard templates
            //var HeaderTemplate = VirtualPathUtility.Combine(table.Model.DynamicDataFolderVirtualPath, String.Format("{0}HeaderTemplate.ascx", TEMPLATE_FOLDER));
            //if (File.Exists(Page.Server.MapPath(HeaderTemplate)))
            //    wizard.HeaderTemplate = Page.LoadTemplate(HeaderTemplate);

            //var sideBarTemplate = VirtualPathUtility.Combine(_table.Model.DynamicDataFolderVirtualPath, String.Format("{0}SideBarTemplate.ascx", TEMPLATE_FOLDER));
            //if (File.Exists(_page.Server.MapPath(sideBarTemplate)))
            //    FormWizard.SideBarTemplate = _page.LoadTemplate(sideBarTemplate);

            // apply styles
            FormWizard.SideBarStyle.CssClass = wizardAttribute.SideBarCssClass;
            FormWizard.HeaderStyle.CssClass = wizardAttribute.HeaderCssClass;
            FormWizard.StepStyle.CssClass = wizardAttribute.StepCssClass;
            FormWizard.NavigationStyle.CssClass = wizardAttribute.NavigationCssClass;

            // wizzard to template
            acessor.AddParsedSubObject(FormWizard);

            // get a list of groups ordered by group name
            var steps = from t in _table.GetScaffoldColumns(mode, ContainerType.Item)
                        group t by t.GetAttributeOrDefault<GroupItemAttribute>().Position into g
                        orderby g.Key
                        select g.Key;

            // add table for each group
            foreach (var stepIndex in steps)
            {
                // create table panel
                var wizardStep = new WizardStep()
                {
                    ID = (String.Format("{0} {1}", _table.Name, stepIndex)).Replace(" ", SEPERATOR),
                    Title = groupsAttribute.Groups[stepIndex],
                    StepType = WizardStepType.Auto
                };

                // organize steps
                //if(groupings.First() == groupName)
                //    wizardStep.StepType= WizardStepType.Start;
                //else if(groupings.Last() == groupName)
                //    wizardStep.StepType= WizardStepType.Finish;
                //else
                //    wizardStep.StepType= WizardStepType.Step;

                // add tab to tab container
                FormWizard.WizardSteps.Add(wizardStep);

                // create table to go inside the wizard step
                var tabTable = new HtmlTable();
                tabTable.Attributes.Add("class", "DDDetailsTable");
                tabTable.Attributes.Add("cellpadding", "6");

                // add the DynamicControl to the tab panel
                wizardStep.Controls.Add(tabTable);

                // get columns for this group
                var columns = from c in _table.GetScaffoldColumns(mode, ContainerType.Item)
                              where c.GetAttributeOrDefault<GroupItemAttribute>().Position == stepIndex
                              orderby c.GetAttributeOrDefault<DisplayAttribute>().GetOrder()
                              select c;

                // add fields
                foreach (MetaColumn column in columns)
                {
                    var tableRow = new HtmlTableRow();
                    tabTable.Controls.Add(tableRow);

                    var tdHeader = new HtmlTableCell();
                    tdHeader.Attributes.Add("class", "DDLightHeader");
                    tdHeader.InnerText = column.DisplayName;

                    // add header cell to row
                    tableRow.Controls.Add(tdHeader);

                    var tdData = new HtmlTableCell();
                    var dynamicControl = new DynamicControl(mode)
                        {
                            DataField = column.Name,
                        };
                    tdData.Controls.Add(dynamicControl);

                    // add data cell to row
                    tableRow.Controls.Add(tdData);
                }
            }
        }
    }
}