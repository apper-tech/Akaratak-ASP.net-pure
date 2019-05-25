using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.DynamicData;

namespace DynamicDataLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ShowColumnsAttribute : Attribute
    {
        public ShowColumnsAttribute(params String[] displayColumns)
        {
            DisplayColumns = displayColumns;
        }

        public Boolean HideHeader { get; set; }
        public int PageSize { get; set; }
        public String[] DisplayColumns { get; set; }
        public Boolean EnableDelete { get; set; }
        public Boolean EnableInsert { get; set; }
        public Boolean EnableUpdate { get; set; }
        public Boolean EnableDeleteEvenWithNoPermission { get; set; }
        public Boolean EnableInsertEvenWithNoPermission { get; set; }
        public Boolean EnableUpdateEvenWithNoPermission { get; set; }
        public Boolean EnableViewEvenWithNoPermission { get; set; }

        public static void HideIfCannotView(ShowColumnsAttribute showColumnsAttribute, System.Web.DynamicData.FieldTemplateUserControl control, MetaTable table, HttpContext context)
        {
            bool canView = DynamicDataLibrary.AuthorizationManager.CheckUrlAccessForPrincipal(table.GetActionPath(PageAction.List), context.User);

            if (!canView &&
                (showColumnsAttribute == null || !showColumnsAttribute.EnableViewEvenWithNoPermission))
            {
                control.Parent.Parent.Visible = false;
            }
        }
    }
}