using DynamicDataLibrary;
using DynamicDataLibrary.Attributes;
using DynamicDataLibrary.ModelRelated;
using DynamicDataModel.Model;
using NotAClue.Web.DynamicData;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.DynamicData;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;

namespace DynamicDataWebSite
{
    public partial class Report : System.Web.UI.Page
    {
        protected MetaTable table;

        protected void Page_Init(object sender, EventArgs e)
        {
            CultureInfo sa = new CultureInfo(DynamicDataWebSite.DynamicDataHelper.DefaultCulture);
            System.Threading.Thread.CurrentThread.CurrentCulture = sa;
            System.Threading.Thread.CurrentThread.CurrentUICulture = sa;

            table = DynamicDataRouteHandler.GetRequestMetaTable(Context);
            GridDataSource.EntityTypeFilter = table.EntityType.Name;

            string tableName = table.EntityType.Name.ToLower().StartsWith("view_")
                ? table.EntityType.Name.Substring("view_".Length)
                : table.EntityType.Name;
            ReportViewer1.LocalReport.ReportPath = String.Format("DynamicDataReports/{0}.rdlc"
                , tableName);

            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource(
                String.Format("DataSet{0}", tableName), "GridDataSource"));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GridDataSource.Include = table.ForeignKeyColumnsNames;
            Title = table.DisplayName;

            //To refresh it ReportViewer when a filter chaned:
            ReportViewer1.LocalReport.Refresh();
        }

        protected void Label_PreRender(object sender, EventArgs e)
        {
            CultureInfo sa = new CultureInfo(DynamicDataWebSite.DynamicDataHelper.DefaultCulture);
            System.Threading.Thread.CurrentThread.CurrentCulture = sa;
            System.Threading.Thread.CurrentThread.CurrentUICulture = sa;

            Label label = (Label)sender;
            DynamicFilter dynamicFilter = (DynamicFilter)label.FindControl("DynamicFilter");
            QueryableFilterUserControl fuc = dynamicFilter.FilterTemplate as QueryableFilterUserControl;
            if (fuc != null && fuc.FilterControl != null)
            {
                label.AssociatedControlID = fuc.FilterControl.GetUniqueIDRelativeTo(label);
            }
        }

        protected void GridDataSource_QueryCreated(object sender, QueryCreatedEventArgs e)
        {
            ViewLimitedToCurrentUserAttribute dataLimitedToCurrentUser = table.GetAttribute<ViewLimitedToCurrentUserAttribute>();
            if (dataLimitedToCurrentUser != null)
            {
                Nullable<int> numberOfItems;
                dataLimitedToCurrentUser.Handle(e, this.table, this.Page, out numberOfItems);
            }
        }

    }

}
