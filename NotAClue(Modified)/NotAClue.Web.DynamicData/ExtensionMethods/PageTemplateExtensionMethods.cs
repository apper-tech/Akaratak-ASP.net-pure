using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.DynamicData;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;
using NotAClue.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls.Expressions;

namespace NotAClue.Web.DynamicData
{
    public static class PageTemplateExtensionMethods
    {
        // e.g. "~/DynamicData/PageTemplates/List.aspx"
        private const String extension = ".aspx";

        public static Dictionary<String, PageTemplate> PageTemplates { get; private set; }

        /// <summary>
        /// Maps unknown pages to page templates.
        /// </summary>
        /// <value>Page template mappings.</value>
        public static Dictionary<String, PageTemplate> PageTemplateMappings { get; set; }

        /// <summary>
        /// Initializes the <see cref="PageExtensionMethods"/> class.
        /// </summary>
        static PageTemplateExtensionMethods()
        {
            PageTemplateMappings = new Dictionary<string, PageTemplate>();
            PageTemplates = new Dictionary<string, PageTemplate>();
            foreach (PageTemplate item in Enum.GetValues(typeof(PageTemplate)))
            {
                PageTemplates.Add(item.ToString(), item);
            }
        }

        /// <summary>
        /// Sets the initial sort order.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="table">The table.</param>
        /// <param name="queryExtender">The query extender.</param>
        public static void SetInitialSortOrder(this Page page, MetaTable table, QueryExtender queryExtender)
        {
            // set default sort
            if (!page.IsPostBack && table.SortColumn != null)
            {
                var order = new OrderByExpression()
                {
                    DataField = table.SortColumn.Name,
                    Direction = table.SortDescending ? SortDirection.Descending : SortDirection.Ascending,
                };
                queryExtender.Expressions.Add(order);
            }

        }

        /// <summary>
        /// Gets the page template from the page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public static PageTemplate GetPageTemplate(this Page page)
        {
            var path = page.AppRelativeVirtualPath;
            var pageName = path.Substring(path.LastIndexOf("/") + 1, path.Length - path.LastIndexOf("/") - extension.Length - 1);

            if (PageTemplates.Keys.Contains(pageName))
                return PageTemplates[pageName];

            if (PageTemplateMappings.Keys.Contains(pageName))
                return PageTemplateMappings[pageName];
            else
                return PageTemplate.Unknown;
        }

        /// <summary>
        /// Get a data bindable list of permissions for the DDL
        /// </summary>
        private readonly static Dictionary<String, FormViewMode> _formViewModes = new Dictionary<String, FormViewMode>() 
            { 
                { PageAction.Details, FormViewMode.ReadOnly }, 
                { PageAction.Edit, FormViewMode.Edit }, 
                { PageAction.Insert, FormViewMode.Insert }
            };

        /// <summary>
        /// Gets the form view mode.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public static FormViewMode GetFormViewMode(this Page page)
        {
            var action = page.RouteData.Values["action"].ToString();
            if (_formViewModes.Keys.Contains(action))
                return _formViewModes[action];

            return FormViewMode.ReadOnly;
        }
    }
}
