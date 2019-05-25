using DynamicDataLibrary.ModelRelated;
using System;
using System.Web.DynamicData;
using System.Web.UI.WebControls;
using DynamicDataLibrary;
using DynamicDataModel.Model;
using DynamicDataLibrary.Attributes;
using NotAClue.Web.DynamicData;
using System.Linq;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;

using EntityDataSourceChangingEventArgs = Microsoft.AspNet.EntityDataSource.EntityDataSourceChangingEventArgs;
using EntityDataSourceChangedEventArgs = Microsoft.AspNet.EntityDataSource.EntityDataSourceChangedEventArgs;

namespace DynamicDataWebSite.DynamicData.CustomPages.Users
{
    public partial class Edit : System.Web.UI.Page
    {
        private OrderedDictionary primaryKeysValues;
        private bool ForceInsert { set; get; }

        protected string ValidationGroupEdit { get { return this.table.Name + "_Edit"; } }

        protected MetaTable table;

        protected void Page_Init(object sender, EventArgs e)
        {
            table = DynamicDataRouteHandler.GetRequestMetaTable(Context);
            if (table != null)
                FormView1.SetMetaTable(table);
            else
                Response.Redirect("~/");
            DetailsDataSource.EntityTypeFilter = table.EntityType.Name;

            DynamicDataEntityCustomTextAttribute customText = table.Attributes.GetAttribute<DynamicDataEntityCustomTextAttribute>();
            if (customText != null)
            {
                this.EditItemTitle = String.Format(customText.EditItemTitle, table.DisplayName);
                this.EditItemCommandTextAtEdit = customText.EditItemCommandTextAtEdit;
            }
            if (String.IsNullOrEmpty(this.EditItemTitle))
                this.EditItemTitle = String.Format(Convert.ToString(GetGlobalResourceObject("DynamicData", "EditItemTitleFormat")), table.DisplayName);
            if (String.IsNullOrEmpty(this.EditItemCommandTextAtEdit))
                this.EditItemCommandTextAtEdit = Convert.ToString(GetGlobalResourceObject("DynamicData", "Update1"));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Resources.RealEstate.Page_User_Edit;
            DetailsDataSource.Include = table.ForeignKeyColumnsNames;

            this.ValidationSummary.ValidationGroup = this.ValidationGroupEdit;
            this.DetailsViewValidator.ValidationGroup = this.ValidationGroupEdit;
        }

        protected void FormView1_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName == DataControlCommands.CancelCommandName)
            {
                Response.Redirect("~/");
            }
        }

        protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            table.RemoveWrongEntries(e.OldValues);
            table.RemoveWrongEntries(e.NewValues);
            if (this.primaryKeysValues != null)
                foreach (System.Collections.DictionaryEntry item in this.primaryKeysValues)
                {
                    e.OldValues.Add(item.Key, item.Value);
                    e.NewValues.Add(item.Key, item.Value);
                }

            string message;
            if (!AdvancedRangeAttribute.IsValideFor(table.Columns, e.NewValues, out message))
            {
                e.Cancel = true;
                this.ShowErrorMessage(message);
                return;
            }

            if (Entities.CustomCheckBeforeEdit.ContainsKey(table.EntityType))
            {
                MessageActionType messageActionType;
                bool canInsert = Entities.CustomCheckBeforeEdit[table.EntityType]
                    .Invoke(e.OldValues, e.NewValues, ForceInsert, out message, out messageActionType);
                e.Cancel = !canInsert;
                if (messageActionType == MessageActionType.NeedConfirmation)
                {
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

        protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            foreach (System.Collections.DictionaryEntry item in this.primaryKeysValues)
            {
                if (!e.OldValues.Contains(item.Key))
                    e.OldValues.Add(item.Key, item.Value);
                if (!e.NewValues.Contains(item.Key))
                    e.NewValues.Add(item.Key, item.Value);
            }
            if (e.Exception == null || e.ExceptionHandled)
            {
                //Handle custom actions if any:
                string message = null;
               
                MessageAfterActionType messageActionType;
                if (Entities.CustomProcessAfterEdit.ContainsKey(table.EntityType))
                {
                    Entities.CustomProcessAfterEdit[table.EntityType]
                        .Invoke(e.OldValues, e.NewValues, e.AffectedRows, out message, out messageActionType);
                    if (messageActionType == MessageAfterActionType.Error)
                    {
                        this.ShowErrorMessage(message);
                        return;
                    }
                    else if (messageActionType == MessageAfterActionType.Information)
                    {
                        this.ShowInfoMessage(message);
                        return;
                    }
                }
                Response.Redirect("~/");
            }
            else
            {
                if (e.Exception.InnerException != null
                    && (e.Exception.InnerException.Message.StartsWith("Cannot insert duplicate key row")
                    || e.Exception.InnerException.Message.StartsWith("Violation of UNIQUE KEY constraint")))
                {
                    this.ShowErrorMessage("حدث خطأ أثناء محاولة التعديل بسبب وجود عناصر متكررة بقيم مشابهة.");
                }
                else
                {
                    this.ShowErrorMessage("حدث خطأ أثناء محاولة التعديل, تفاصيل الخطأ:<br/>"
                        +
                        e.Exception.InnerException == null ? e.Exception.Message : e.Exception.InnerException.Message);
                }
                e.ExceptionHandled = true;
                return;
            }
        }

        protected void LinkButtonConfirmation_Click(object sender, EventArgs e)
        {
            this.ForceInsert = true;
            this.FormView1.UpdateItem(true);
        }

        protected void LinkButtonConfirmationCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }


        protected void LinkButtonErrorMessageOK_Click(object sender, EventArgs e)
        {

        }
        protected void LinkButtonInformationMessageOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }

        protected void FormView1_PreRender(object sender, EventArgs e)
        {
            this.FormView1.HandleDependant(this.table);
        }

        protected void FormView1_DataBound(object sender, EventArgs e)
        {
            ContributionLimitedToCreatorAttribute contributionLimitedToCreator
                = table.GetAttribute<ContributionLimitedToCreatorAttribute>();
            if (contributionLimitedToCreator != null)
            {
                if (!contributionLimitedToCreator.CanContributeOnDataItem(this.FormView1.DataItem))
                {
                    Response.Redirect(Global.DefaultModel.GetActionPath("Users","Insert",null));
                    this.FormView1.Visible = false;
                }
            }
        }

        protected void DetailsDataSource_Updating(object sender, EntityDataSourceChangingEventArgs e)
        {
            primaryKeysValues = new OrderedDictionary();
            // setup the GridView's DataKeys
            if (e.Entity != null)
            {
                System.Type entityType = e.Entity.GetType();
                foreach (var keyColumn in table.PrimaryKeyColumns)
                {
                    object value = entityType.GetProperty(keyColumn.Name).GetValue(e.Entity);
                    primaryKeysValues.Add(keyColumn.Name, value);
                }
            }
        }

        protected void DetailsDataSource_Updated(object sender, EntityDataSourceChangedEventArgs e)
        {

        }

        public string EditItemCommandTextAtEdit { get; set; }

        public string EditItemTitle { get; set; }

        protected void DetailsDataSource_QueryCreated(object sender, QueryCreatedEventArgs e)
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

        protected void DynamicEntity_Load(object sender, EventArgs e)
        {
            (sender as DynamicEntity).ValidationGroup = this.ValidationGroupEdit;
        }

        protected void LinkButtonUpdate_Load(object sender, EventArgs e)
        {
            (sender as Button).ValidationGroup = this.ValidationGroupEdit;
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
    }
}