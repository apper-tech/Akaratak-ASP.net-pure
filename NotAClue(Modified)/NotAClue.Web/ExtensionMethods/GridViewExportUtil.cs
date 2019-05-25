using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace NotAClue.Web
{
    public static class GridViewExportUtil
    {
        /// <summary>
        /// Exports a GridView columns and all to Excel
        /// </summary>
        /// <param name="gridView">The GridView to export.</param>
        /// <param name="fileName">The filename to export to</param>
        public static void Export(this  GridView gridView, string fileName)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader(
                "content-disposition",
                string.Format("attachment; filename={0}", fileName));

            HttpContext.Current.Response.ContentType = "application/ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a table to contain the grid
                    Table table = new Table();

                    //  include the grid line settings
                    table.GridLines = gridView.GridLines;

                    //  add the header row to the table
                    if (gridView.HeaderRow != null)
                    {
                        gridView.HeaderRow.Controls.RemoveAt(0);
                        GridViewExportUtil.PrepareControlForExport(gridView.HeaderRow);
                        table.Rows.Add(gridView.HeaderRow);
                    }

                    //  add each of the data rows to the table
                    foreach (GridViewRow row in gridView.Rows)
                    {
                        row.Controls.RemoveAt(0);
                        GridViewExportUtil.PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }

                    //  add the footer row to the table
                    if (gridView.FooterRow != null)
                    {
                        gridView.FooterRow.Controls.RemoveAt(0);
                        GridViewExportUtil.PrepareControlForExport(gridView.FooterRow);
                        table.Rows.Add(gridView.FooterRow);
                    }

                    //  render the table into the HTML writer
                    table.RenderControl(htw);

                    //  render the HTML writer into the response
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }

        /// <summary>
        /// Replace any of the contained controls with literals
        /// </summary>
        /// <param name="control">The control.</param>
        private static void PrepareControlForExport(Control control)
        {
            // remove command column
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    GridViewExportUtil.PrepareControlForExport(current);
                }
            }
        }
    }
}