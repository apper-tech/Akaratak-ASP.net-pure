using DynamicDataLibrary;
using DynamicDataLibrary.Attributes;
using DynamicDataLibrary.ModelRelated;
using System;
using System.Web.DynamicData;
using System.Web.UI.WebControls;
using NotAClue.Web.DynamicData;
using System.Collections.Specialized;
using DynamicDataModel.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.UI;
using System.Web;

namespace DynamicDataWebSite
{
    public partial class FormViewField : System.Web.DynamicData.FieldTemplateUserControl
    {
        private string parentName;

        private OrderedDictionary backUpKeys = new OrderedDictionary();
        protected bool ForceDelete { get; set; }
        protected MetaTable table;

        public bool EnableDelete { get; set; }
        public bool EnableUpdate { get; set; }
        public bool EnableInsert { get; set; }

        public string[] DisplayColumns { get; set; }

        public string DeleteItemCommandText { get; set; }
        public string DeleteConfirmationMesssage { get; set; }
        public string EditItemCommandText { get; set; }
        public string ItemDetailsTitle { get; set; }

        protected void Page_PreInit(object sender, EventArgs e)
        {
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.table = this.Column.FigureOutForeignOrChildTable();
            if (this.table == null)
                // throw an error if set on column other than MetaChildrenColumns
                throw new InvalidOperationException("The GridView FieldTemplate can only be used with One-To-One or One-To-Many relation!");


            this.FormView1.SetMetaTable(table);

            var attribute = table.Attributes.OfType<ShowColumnsAttribute>().FirstOrDefault();

            ShowColumnsAttribute.HideIfCannotView(attribute, this, table, this.Context);

            if (attribute != null)
            {
                if (attribute.DisplayColumns.Length > 0)
                    DisplayColumns = attribute.DisplayColumns;
            }

            //DetailsDataSource.ContextTypeName = metaChildColumn.ChildTable.DataContextType.Name;
            DetailsDataSource.ContextTypeName = "DynamicDataModel.Model.Entities";

            DetailsDataSource.TableName = table.Name;

            Initialize();

            SetText();
        }

        private void SetText()
        {
            DynamicDataEntityCustomTextAttribute customText = table.Attributes.GetAttribute<DynamicDataEntityCustomTextAttribute>();
            if (customText != null)
            {
                this.EditItemCommandText = customText.EditItemCommandTextAtList;
                this.DeleteItemCommandText = customText.DeleteItemCommandText;
                if (customText.ItemDetailsTitleFormat != null)
                    this.ItemDetailsTitle = String.Format(customText.ItemDetailsTitleFormat, table.DisplayName);
                this.DeleteConfirmationMesssage = customText.DeleteConfirmationMesssage;
            }
            if (String.IsNullOrEmpty(this.ItemDetailsTitle))
                this.ItemDetailsTitle = String.Format(Convert.ToString(GetGlobalResourceObject("DynamicData", "ItemDetailsTitleFormat")), table.DisplayName);
            if (String.IsNullOrEmpty(this.EditItemCommandText))
                this.EditItemCommandText = Convert.ToString(GetGlobalResourceObject("DynamicData", "Update"));
            if (String.IsNullOrEmpty(this.DeleteItemCommandText))
                this.DeleteItemCommandText = Convert.ToString(GetGlobalResourceObject("DynamicData", "Delete"));
            if (String.IsNullOrEmpty(this.DeleteConfirmationMesssage))
                this.DeleteConfirmationMesssage = Convert.ToString(GetGlobalResourceObject("DynamicData", "DelConfirm"));
        }

        private void Initialize()
        {
            // Generate the columns as we can't rely on 
            // DynamicDataManager to do it for us.
            //FormView1.ColumnsGenerator = new FieldTemplateRowGenerator(table, DisplayColumns);

            // setup the GridView's DataKeys
            String[] keys = new String[table.PrimaryKeyColumns.Count];
            int i = 0;
            foreach (var keyColumn in table.PrimaryKeyColumns)
            {
                keys[i] = keyColumn.Name;
                i++;
            }
            FormView1.DataKeyNames = keys;

            DetailsDataSource.AutoGenerateWhereClause = true;

            //To Order By:
            var displayColumnAtt = table
                .Attributes.OfType<DisplayColumnAttribute>().FirstOrDefault();
            if (displayColumnAtt != null)
            {
                DetailsDataSource.OrderBy = "it." + displayColumnAtt.SortColumn;
                if (displayColumnAtt.SortDescending)
                    DetailsDataSource.OrderBy += " desc";
            }
            else
                DetailsDataSource.OrderBy =
                    String.Concat(table.PrimaryKeyColumns.Select(c => "it." + c.Name + " desc ,")).TrimEnd(',');


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //DetailsDataSource.Include = table.ForeignKeyColumnsNames;


            SetNullToUndeededColumns();
        }

        private void SetNullToUndeededColumns()
        {

            //HideColumnsInFormViewFieldTemplateAttribute hideColumnsInFormViewFieldTemplate
            //    = table.GetAttribute<HideColumnsInFormViewFieldTemplateAttribute>();
            //if (hideColumnsInFormViewFieldTemplate != null)
            //{
            //    foreach (string columnName in hideColumnsInFormViewFieldTemplate.HideColumns)
            //    {
            //        System.Reflection.MemberInfo m = this.Row.GetType().GetMember(Column.ColumnType.Name)[0];
            //        System.Reflection.MemberInfo m2 = this.Row.GetType().GetMember(table.EntityType.Name)[0];

            //        var p = m.DeclaringType.GetProperties()[0];
            //        p.SetValue(this.Row, null);
            //        System.Reflection.PropertyInfo pro = m.DeclaringType.GetProperty(table.Name);
            //        //.GetProperty(columnName);
            //        //pro.SetValue(m, null);
            //    }
            //}
            //try
            //{
            //    var p2 = this.Row.GetType().GetProperties();

            //    HideColumnsInFormViewFieldTemplateAttribute hideColumnsInFormViewFieldTemplate
            //        = table.GetAttribute<HideColumnsInFormViewFieldTemplateAttribute>();
            //    if (hideColumnsInFormViewFieldTemplate != null)
            //    {
            //        foreach (string columnName in hideColumnsInFormViewFieldTemplate.HideColumns)
            //        {
            //            foreach (System.Reflection.PropertyInfo pro2 in p2)
            //            {
            //                if (pro2.Name == columnName)
            //                {
            //                    pro2.SetValue(this.Row, null);
            //                    this.Visible = false;
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (System.Exception)
            //{
            //    //throw;
            //}
        }

        protected void DetailsDataSource_QueryCreated(object sender, QueryCreatedEventArgs e)
        {
            //ViewLimitedToCurrentUserAttribute dataLimitedToCurrentUser = table.GetAttribute<ViewLimitedToCurrentUserAttribute>();
            //if (dataLimitedToCurrentUser != null)
            //{
            //    dataLimitedToCurrentUser.Handle(e, this.table, this.Page);
            //}
        }

        protected void FormView1_ItemDeleting(object sender, FormViewDeleteEventArgs e)
        {
            bool hasInsertPermission = DynamicDataLibrary.AuthorizationManager.CheckUrlAccessForPrincipal(table.GetActionPath(PageAction.Insert), this.Page.User);
            if (!hasInsertPermission)
            {
                throw new UnauthorizedAccessException();
            }

            this.table.Copy(e.Keys, this.backUpKeys);

            if (Entities.CustomCheckBeforeDelete.ContainsKey(table.EntityType))
            {
                string message;
                MessageActionType messageActionType;
                bool isValueFilledManually = false;
                if (e.Values.Count == 0)
                {
                    this.table.FillValues(e.Keys, e.Values);
                    isValueFilledManually = true;
                }
                bool canDelete = Entities.CustomCheckBeforeDelete[table.EntityType]
                    .Invoke(e.Values, ForceDelete, out message, out messageActionType);
                if (isValueFilledManually)
                    e.Values.Clear();

                e.Cancel = !canDelete;
                if (messageActionType == MessageActionType.NeedConfirmation)
                {
                    this.LabelConfirmationMessage.Text = message;
                    this.ModalPopupExtenderConfirmationMessage.Show();
                }
                else if (messageActionType == MessageActionType.Error)
                {
                    this.ShowErrorMessage(message);
                }
                else if (messageActionType == MessageActionType.Information)
                {
                    this.LabelInformationMessage.Text = message;
                    this.ModalPopupExtenderInformationMessage.Show();
                }
            }
        }

        protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
        {
            if (e.Exception == null || e.ExceptionHandled)
            {
                //Handle custom actions if any:
                string message = null;
                MessageAfterActionType messageAfterActionType = MessageAfterActionType.NoMessage;
                if (Entities.CustomProcessAfterDelete.ContainsKey(table.EntityType))
                {
                    bool isValueFilledManually = false;
                    if (e.Keys.Count == 0)
                    {
                        this.table.Copy(this.backUpKeys, e.Keys);
                    }
                    if (e.Values.Count == 0)
                    {
                        this.table.FillValues(e.Keys, e.Values);
                        isValueFilledManually = true;
                    }
                    Entities.CustomProcessAfterDelete[table.EntityType]
                        .Invoke(e.Values, e.AffectedRows, out message, out messageAfterActionType);
                    if (isValueFilledManually)
                        e.Values.Clear();
                }
                if (!String.IsNullOrEmpty(message))
                {
                    if (messageAfterActionType == MessageAfterActionType.Information)
                    {
                        this.LabelInformationMessage.Text = message;
                        this.ModalPopupExtenderInformationMessage.Show();
                    }
                    else
                    {
                        this.FormView1.Visible = false;
                        this.ShowErrorMessage(message);
                    }
                    //this.UpdatePanel1.Visible = false;
                }
                else
                    Response.Redirect(table.ListActionPath);
            }
            else
            {
                //this.FormView1.Visible = false;
                if (e.Exception.HResult == -2146233087)
                    ShowErrorMessage(Convert.ToString(GetGlobalResourceObject("DynamicData", "Error_CannotDeleteBecauseOfReletedRecords")));
                else
                    ShowErrorMessage(String.Format("{0}:<br/>{1}", GetGlobalResourceObject("DynamicData", "Error_CannotDeleteBecauseOf"),
                        e.Exception.Message));
                e.ExceptionHandled = true;
                return;
            }
        }

        protected void LinkButtonConfirmationDelete_Click(object sender, EventArgs e)
        {
            this.ForceDelete = true;
            this.FormView1.DeleteItem();
        }

        protected void LinkButtonInformationMessageOK_Click(object sender, EventArgs e)
        {
            this.FormView1.DataBind();
        }

        protected void LinkButtonErrorMessageOK_Click(object sender, EventArgs e)
        {
            this.FormView1.DataBind();
        }

        protected void LinkButtonConfirmationCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(table.ListActionPath);
        }

        protected void FormView1_DataBound(object sender, EventArgs e)
        {
            this.FormView1.HandleDependant(this.table);


            ContributionLimitedToCreatorAttribute contributionLimitedToCreator
                = table.GetAttribute<ContributionLimitedToCreatorAttribute>();
            if (contributionLimitedToCreator != null)
            {
                if (!contributionLimitedToCreator.CanContributeOnDataItem(this.FormView1.DataItem))
                {
                    this.FormView1.FindControl("DynamicHyperLink_Edit").Visible = false;
                    this.FormView1.FindControl("LinkButton1").Visible = false;
                }
            }
        }

        protected void DetailsDataSource_Deleting(object sender, System.Web.UI.WebControls.LinqDataSourceDeleteEventArgs e)
        {

        }

        protected void DetailsDataSource_Deleted(object sender, System.Web.UI.WebControls.LinqDataSourceStatusEventArgs e)
        {

        }

        private void ShowErrorMessage(string message)
        {
            this.LabelErrorMessage.Text = message;
            this.ModalPopupExtenderErrorMessage.Show();
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            this.AddWhereAndOrderByToDataSource(this.DetailsDataSource, this.table, this.Request);

            // doing the work of this above because we can't
            // set the DynamicDataManager table or where values
            //DynamicDataManager1.RegisterControl(GridView1, false);

            ////this.HideParentColumn();
            //if (row != null)
            //    this.parentName = row.GetType().BaseType.Name;
            ////else
            ////    System.Diagnostics.Debugger.Break();//Strange Case!
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

    }
}
