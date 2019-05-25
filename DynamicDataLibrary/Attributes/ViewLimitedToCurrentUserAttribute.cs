using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Linq.Dynamic;
using System.Web.UI;
using DynamicDataLibrary.ModelRelated;
using DynamicDataLibrary;

namespace DynamicDataLibrary.Attributes
{
    public class ViewLimitedToCurrentUserAttribute : Attribute
    {
        public bool justIfCannotContribute = false;
        public bool JustIfCannotContribute { get { return this.justIfCannotContribute; } }

        public string UserIDFiled
        {
            get
            {
                return userIDFiled;
            }

            set
            {
                userIDFiled = value;
            }
        }

        private string userIDFiled = "Userid";
     


        public ViewLimitedToCurrentUserAttribute(bool justIfHasNoContribution, string useridFiled)
        {
            this.justIfCannotContribute = justIfHasNoContribution;
            this.UserIDFiled = useridFiled;
        }


        public bool IsSelectionLimited(MetaTable table, System.Security.Principal.IPrincipal user, out bool canEdit, out bool canInsert)
        {
            canInsert = DynamicDataLibrary.AuthorizationManager.CheckUrlAccessForPrincipal(table.GetActionPath(PageAction.Insert), user);
            canEdit = DynamicDataLibrary.AuthorizationManager.CheckUrlAccessForPrincipal(table.GetActionPath(PageAction.Edit), user);
            if (this.JustIfCannotContribute == false || (this.JustIfCannotContribute && !canEdit && !canInsert))
                return true;
            return false;
        }

        public void Handle(System.Web.UI.WebControls.QueryCreatedEventArgs e, MetaTable table,
            Page page, out Nullable<int> numberOfItems)
        {
            bool canEdit, canInsert;
            numberOfItems = null;
            if (this.IsSelectionLimited(table, page.User, out canEdit, out canInsert))
            {
                e.Query = e.Query.Where(String.Format("{0} = \"{1}\"",
                   this.UserIDFiled, AuditingHelperUtility.GetUserId()));
                numberOfItems = e.Query.Count();
                //If there is only one item and the user cannot insert, redirect the user to the display form of this one item.
                #region Redirect to Details
                if (numberOfItems == 1 && !canInsert
                    //Do this only if there is no pervious paramerter
                    && page.Request.QueryString.Count == 0)
                {
                    List<String> urlQuery = new List<String>();
                    foreach (MetaColumn column in table.PrimaryKeyColumns)
                    {
                        IQueryable qur = e.Query.Select(column.Name);
                        string primarykey = null;
                        foreach (var key in qur)
                        {
                            primarykey = key.ToString();
                        }
                        urlQuery.Add(String.Format("{0}={1}", column.Name, HttpUtility.UrlEncode(primarykey)));
                    }
                    page.Response.Redirect(table.GetActionPath(PageAction.Details)
                        + "?" + String.Join("&", urlQuery)
                           , true);
                }
                #endregion
            }
        }

    }
}