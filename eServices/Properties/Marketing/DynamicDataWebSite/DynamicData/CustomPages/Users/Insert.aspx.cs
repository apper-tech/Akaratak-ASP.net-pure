using DynamicDataLibrary.Attributes;
using System;
using System.Web.DynamicData;
using System.Web.UI.WebControls;
using NotAClue.Web.DynamicData;
using Microsoft.AspNet.Identity;
using DynamicDataLibrary.ModelRelated;
using DynamicDataLibrary;
using DynamicDataModel.Model;
using System.Text;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;

using EntityDataSourceChangingEventArgs = Microsoft.AspNet.EntityDataSource.EntityDataSourceChangingEventArgs;
using EntityDataSourceChangedEventArgs = Microsoft.AspNet.EntityDataSource.EntityDataSourceChangedEventArgs;
using System.Web.Security;

namespace DynamicDataWebSite.DynamicData.CustomPages.Users
{
    public partial class Insert : System.Web.UI.Page
    {
        protected MetaTable table;

        protected string ValidationGroupNew { get { return this.table.Name + "_New"; } }

        public bool ForceInsert { set; get; }

        public string NewItemTitle { get; set; }

        public string NewItemCommandText { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {

            table = DynamicDataRouteHandler.GetRequestMetaTable(Context);
            if (table != null)
                FormView1.SetMetaTable(table, table.GetColumnValuesFromRoute(Context));
            else
                Response.Redirect("~/");
            DetailsDataSource.EntityTypeFilter = table.EntityType.Name;
            if (UserProfile.UserExist(User.Identity.GetUserId()))
            {
                Response.Redirect(table.GetActionPath("Edit") + "?User_ID=" + User.Identity.GetUserId());
            }
            DynamicDataEntityCustomTextAttribute customText = table.Attributes.GetAttribute<DynamicDataEntityCustomTextAttribute>();
            if (customText != null)
            {
                this.NewItemTitle = String.Format(customText.NewItemTitleFormat, table.DisplayName);
                this.NewItemCommandText = customText.NewItemCommandText;
            }
            if (String.IsNullOrEmpty(this.NewItemTitle))
                this.NewItemTitle = String.Format(Convert.ToString(GetGlobalResourceObject("DynamicData", "NewItemTitleFormat")), table.DisplayName);
            if (String.IsNullOrEmpty(this.NewItemCommandText))
                this.NewItemCommandText = Convert.ToString(GetGlobalResourceObject("DynamicData", "Add"));


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Resources.RealEstate.Page_User_Insert;
        }


        protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            string message;
            if (!AdvancedRangeAttribute.IsValideFor(table.Columns, e.Values, out message))
            {
                e.Cancel = true;
                this.ShowErrorMessage(message);
                return;
            }

            //Handle custom actions if any:
            if (Entities.CustomCheckBeforeInsert.ContainsKey(table.EntityType))
            {
                MessageActionType messageActionType;
                bool canInsert = Entities.CustomCheckBeforeInsert[table.EntityType]
                    .Invoke(e.Values, ForceInsert, out message, out messageActionType);
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


        private string urlParams;

        protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            if (e.Exception == null || e.ExceptionHandled)
            {
                foreach (System.Collections.DictionaryEntry item in this.primaryKeysValues)
                {
                    if (e.Values[item.Key] == null)
                        e.Values.Add(item.Key, item.Value);
                }

                //Handle custom actions if any:
                if (Entities.CustomProcessAfterInsert.ContainsKey(table.EntityType))
                {
                    string message = null;
                    MessageAfterActionType messageActionType;
                    Entities.CustomProcessAfterInsert[table.EntityType]
                        .Invoke(e.Values, e.AffectedRows, out message, out messageActionType);
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
                //add user to stackholder if inserted on users
                var rol = table.Attributes.OfType<DynamicDataLibrary.Attributes.EnroleUserAttribute>().FirstOrDefault();
                if (rol != null)
                {
                    if (!Roles.IsUserInRole(User.Identity.Name, rol.DefaultRole))
                        Roles.AddUserToRole(User.Identity.Name, rol.DefaultRole);
                }
                Response.Redirect("~/");
            }
            else
            {
                if (e.Exception.InnerException != null
                    && (e.Exception.InnerException.Message.StartsWith("Cannot insert duplicate key row")
                    || e.Exception.InnerException.Message.StartsWith("Violation of UNIQUE KEY constraint")))
                {
                    this.ShowErrorMessage("حدث خطأ أثناء محاولة الإضافة بسبب وجود عناصر متكررة بقيم مشابهة.");
                }
                else
                {
                    this.ShowErrorMessage("حدث خطأ أثناء محاولة الإضافة, تفاصيل الخطأ:<br/>"
                        +
                        e.Exception.InnerException == null ? e.Exception.Message : e.Exception.InnerException.Message);
                }
                e.ExceptionHandled = true;
                return;
            }
        }

        protected void FormView1_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName == DataControlCommands.CancelCommandName)
            {
                Response.Redirect(table.ListActionPath);
            }
        }

        OrderedDictionary primaryKeysValues;
        protected void DetailsDataSource_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            if (e.Entity != null)
            {
                primaryKeysValues = new OrderedDictionary();
                StringBuilder urlParamsStringBuilder = new StringBuilder("?");
                // setup the GridView's DataKeys
                System.Type entityType = e.Entity.GetType();
                foreach (var keyColumn in table.PrimaryKeyColumns)
                {
                    object value = entityType.GetProperty(keyColumn.Name).GetValue(e.Entity);
                    urlParamsStringBuilder.AppendFormat("{0}={1}", keyColumn.Name, value);
                    primaryKeysValues.Add(keyColumn.Name, value);
                }
                this.urlParams = urlParamsStringBuilder.ToString();
            }
        }

        private void RedirectToPageAction()
        {
            DefaultPageActionAfterInsertAttribute defaultPageActionAfterInsert = table.GetAttribute<DefaultPageActionAfterInsertAttribute>();
            if (defaultPageActionAfterInsert != null)
            {
                Response.Redirect(table.GetActionPath(defaultPageActionAfterInsert.PageAction.ToString())
                    + this.urlParams);
            }

            Response.Redirect(table.ListActionPath);
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

        protected void LinkButtonConfirmation_Click(object sender, EventArgs e)
        {
            this.ForceInsert = true;
            this.FormView1.InsertItem(true);
        }

        protected void LinkButtonConfirmationCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(table.ListActionPath);
        }

        protected void LinkButtonErrorMessageOK_Click(object sender, EventArgs e)
        {

        }
        protected void LinkButtonInformationMessageOK_Click(object sender, EventArgs e)
        {
            RedirectToPageAction();
        }

        protected void FormView1_PreRender(object sender, EventArgs e)
        {
            this.FormView1.HandleDependant(this.table);
        }

        protected void DynamicEntity_Load(object sender, EventArgs e)
        {
            (sender as DynamicEntity).ValidationGroup = this.ValidationGroupNew;
            this.ValidationSummary.ValidationGroup = this.ValidationGroupNew;
            this.DetailsViewValidator.ValidationGroup = this.ValidationGroupNew;
        }

        protected void LinkButtonInsert_Load(object sender, EventArgs e)
        {
            (sender as Button).ValidationGroup = this.ValidationGroupNew;
        }

    }
}