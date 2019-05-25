using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite
{
    public partial class GridViewPager : System.Web.UI.UserControl
    {
        private GridView _gridView;

        //public int LastPageIndex { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Control c = Parent;
            while (c != null)
            {
                if (c is GridView)
                {
                    _gridView = (GridView)c;
                    //this._gridView.PageIndex = LastPageIndex;
                    break;
                }
                c = c.Parent;
            }
        }

        void _gridView_PageIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        void _gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //throw new NotImplementedException();
        }

        protected void TextBoxPage_TextChanged(object sender, EventArgs e)
        {
            if (_gridView == null)
            {
                return;
            }
            int page;
            if (int.TryParse(TextBoxPage.Text.Trim(), out page))
            {
                if (page <= 0)
                {
                    page = 1;
                }
                if (page > _gridView.PageCount)
                {
                    page = _gridView.PageCount;
                }
                _gridView.PageIndex = page - 1;
            }
            TextBoxPage.Text = (_gridView.PageIndex + 1).ToString(CultureInfo.CurrentCulture);
            //_gridView.PageSize = Convert.ToInt32(DropDownListPageSize.SelectedValue, CultureInfo.CurrentCulture);
        }

        protected void DropDownListPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_gridView == null)
            {
                return;
            }
            DropDownList dropdownlistpagersize = (DropDownList)sender;
            _gridView.PageSize = Convert.ToInt32(dropdownlistpagersize.SelectedValue, CultureInfo.CurrentCulture);
            int pageindex = _gridView.PageIndex;
            _gridView.DataBind();
            if (_gridView.PageIndex != pageindex)
            {
                //if page index changed it means the previous page was not valid and was adjusted. Rebind to fill control with adjusted page
                _gridView.DataBind();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (_gridView != null)
            {
                LabelNumberOfPages.Text = _gridView.PageCount.ToString(CultureInfo.CurrentCulture);
                TextBoxPage.Text = (_gridView.PageIndex + 1).ToString(CultureInfo.CurrentCulture);
                //DropDownListPageSize.SelectedValue = _gridView.PageSize.ToString(CultureInfo.CurrentCulture);
            }
        }

        protected void ImageButtonFirst_Click(object sender, ImageClickEventArgs e)
        {
            _gridView.PageIndex = 0;
            //_gridView.PageSize = Convert.ToInt32(DropDownListPageSize.SelectedValue, CultureInfo.CurrentCulture);
            this._gridView.DataBind();
        }

        protected void ImageButtonPrev_Click(object sender, ImageClickEventArgs e)
        {
            _gridView.PageIndex = _gridView.PageIndex > 0 ? _gridView.PageIndex - 1 : _gridView.PageCount - 1;
            //_gridView.PageSize = Convert.ToInt32(DropDownListPageSize.SelectedValue, CultureInfo.CurrentCulture);
            this._gridView.DataBind();
        }

        protected void ImageButtonNext_Click(object sender, ImageClickEventArgs e)
        {
            _gridView.PageIndex = _gridView.PageIndex != _gridView.PageCount - 1 ? _gridView.PageIndex + 1 : 0;
            //_gridView.PageSize = Convert.ToInt32(DropDownListPageSize.SelectedValue, CultureInfo.CurrentCulture);
            this._gridView.DataBind();
        }

        protected void ImageButtonLast_Click(object sender, ImageClickEventArgs e)
        {
            _gridView.PageIndex = _gridView.PageCount - 1;
            //_gridView.PageSize = Convert.ToInt32(DropDownListPageSize.SelectedValue, CultureInfo.CurrentCulture);
            this._gridView.DataBind();
        }

    }
}
