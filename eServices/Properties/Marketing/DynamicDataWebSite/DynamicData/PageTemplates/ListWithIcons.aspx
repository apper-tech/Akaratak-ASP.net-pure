<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ListWithIcons.aspx.cs" Inherits="DynamicDataWebSite.DynamicData.PageTemplates.ListWithIcons" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/ServerControls/Loader.ascx" TagName="Loader" TagPrefix="ServerControls" %>
<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="asp" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">
    <style>
        .EditLink {
            <%= DynamicDataLibrary.AuthorizationManager.CheckUrlAccessForPrincipal(table.GetActionPath(PageAction.Edit), this.Page.User) ? "" : "display: none;"%>;
        }

        .ActionLink {
            display: none;
        }

        #navpager {
            position: relative;
            background: rgba(0,0,0,0.7);
            -webkit-transition: all 0.5s;
            -moz-transition: all 0.5s;
            transition: all 0.5s;
            color: #27DA93;
            background-color: #fff;
            box-shadow: none;
            font-size: 20px;
            z-index: 1 !important;
            padding: 0;
        }

        .num-pager {
            color: #27DA93;
        }

        .row {
            -moz-column-width: 12em;
            -webkit-column-width: 12em;
            -moz-column-gap: 1em;
            -webkit-column-gap: 1em;
            column-width: 12em;
            column-gap: 1em;
            column-fill: balance;
            width: 100%;
        }

        /* Outer div block */
        .section-block {
            display: block;
            padding: .25rem;
            width: 100%;
        }

        /* Inner div block */
        .section-item {
            position: relative;
            display: inline-block;
            width: 100%;
        }

            .section-item img {
                width: 100%;
            }

        nav ul li a {
            /* color: #27DA93 !important;*/
        }

        .num-pager-sel {
            color: #fff !important;
            background-color: #27DA93 !important;
        }

        .pagination {
            font-size: 15px;
        }

        @media (max-width: 640px) {
            .pagination {
                font-size: 10px !important;
            }
        }

        .overlay {
            width: 100%;
            height: 100%;
            position: absolute;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="listView" />
        </DataControls>
    </asp:DynamicDataManager>
    <div class="privacy">
        <div class="container">
            <h3 class="headerlogo3"><%= Resources.RealEstate.Page_List_Title%></h3>
        </div>
    </div>
    <div class="dealer" style="display: none">
        <div class="dealer-grid">
            <div class="col-sm-4 dealer-grid1">
                <div class="dealer-grid-top">
                    <span>1</span>
                    <h6><a href="#">
                        <%= Resources.RealEstate.Filter_Desc_1_T %>
                    </a></h6>
                    <div class="clearfix"></div>
                </div>
                <p>
                    <%= Resources.RealEstate.Filter_Desc_1_D %>
                </p>
            </div>
            <div class="col-sm-4 dealer-grid1">
                <div class="dealer-grid-top">
                    <span>2</span>
                    <h6><a href="#">
                        <%= Resources.RealEstate.Filter_Desc_2_T %>
                    </a></h6>
                    <div class="clearfix"></div>
                </div>
                <p>
                    <%= Resources.RealEstate.Filter_Desc_2_D %>
                </p>
            </div>
            <div class="col-sm-4 dealer-grid1">
                <div class="dealer-grid-top">
                    <span>3</span>
                    <h6><a href="#">
                        <%= Resources.RealEstate.Filter_Desc_3_T %>
                    </a></h6>
                    <div class="clearfix"></div>
                </div>
                <p>
                    <%= Resources.RealEstate.Filter_Desc_3_D %>
                </p>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
                HeaderText="<%$Resources:DynamicData, ValidationSummaryHeaderText%>" CssClass="DDValidator" />
            <asp:DynamicValidator runat="server" ID="GridViewValidator" ControlToValidate="listView" Display="None" CssClass="DDValidator" />
            <div id="filter" class="price">
                <div class="price-grid">
                    <asp:QueryableFilterRepeater runat="server" ID="FilterRepeater" OnDataBinding="FilterRepeater_DataBinding">
                        <ItemTemplate>
                            <div class="col-sm-4 price-top" id="holder" runat="server">
                                <h4>
                                    <asp:Label ID="Label1" runat="server" Text='<%# ProccessFilterName(Eval("DisplayName").ToString()) %>' ToolTip='<%# Eval("Name") %>' OnPreRender="Label_PreRender" OnLoad="Label_Load" />
                                </h4>
                                <asp:DynamicFilter runat="server" ID="DynamicFilter" OnFilterChanged="DynamicFilter_FilterChanged" />
                            </div>
                        </ItemTemplate>
                    </asp:QueryableFilterRepeater>
                </div>
            </div>
            <div class="dealer">
                <div class="container">
                    <div id="reslist" class="dealer-top">
                        <div class="deal-top-top">
                            <asp:ListView runat="server" ID="listView" DataSourceID="GridDataSource" EnablePersistedSelection="true" OnSelectedIndexChanged="listView_SelectedIndexChanged"
                                AllowPaging="True" AllowSorting="True" OnItemCreated="listView_ItemCreated" OnItemDataBound="listView_ItemDataBound" ItemPlaceholderID="itemPlaceHolder1"
                                OnPageIndexChanging="GridView1_PageIndexChanging" AutoGenerateColumns="false" GroupItemCount="12" GroupPlaceholderID="groupPlaceHolder1" OnDataBound="listView_DataBound">
                                <ItemTemplate>
                                    <%--                            <%if (RowCount == 3 || RowCount==2)
                                { %>
                            <div class="col-xs-12 col-sm-3" style="cursor:pointer;margin-bottom:20px">
                                <%}
                                    else
                                    { %>
                                  <div class="col-xs-6 col-sm-3" style="cursor:pointer;margin-bottom:20px">
                                <%} %>--%>
                                    <div class="col-xs-12 col-md-3 col-sm-3 col-lg-3 buttom-margin" style="cursor: pointer;">
                                        <asp:HyperLink runat="server" ID="link" OnPreRender="imglink2_PreRender" CssClass="overlay"> 
                                <div class=" top-deal">
                                  <asp:HyperLink runat="server" ID="imglink" OnPreRender="imglink1_PreRender" CssClass="mask"> 
                                       <div class='section-block'>
                                        <div class='section-item'>
                                            <div>
                                        <asp:DynamicControl runat="server" DataField="Property_Photo" />
                                                </div></div></div>
                                                </asp:HyperLink>
                                    
                                    <div class="deal-bottom">
                                        <div class="top-deal1">
                                            <h5>   <asp:DynamicControl runat="server" DataField="Property_Type" /></h5>
                                            <p><asp:Literal runat="server" Text="<%$Resources:RealEstate, Size%>" />:<asp:DynamicControl runat="server" DataField="Property_Size" /><asp:Literal runat="server" Text="<%$Resources:RealEstate, MSQR%>" /></p>
                                            <p> <asp:DynamicControl runat="server" DataField="City" /></p>
                                        </div>
                                           <div class="top-deal2" style="float:right;color:#a3a3a3">
                                              <p>
                                               <i class="fa fa-bath"></i> <asp:DynamicControl runat="server" DataField="Num_Bathrooms" />
                                            </p>
                                            <p>
                                                <i class="fa fa-bed"></i> <asp:DynamicControl runat="server" DataField="Num_Bedrooms" />
                                            </p></div>
                                        <div class="top-deal2" style="display:none">
                                            <asp:DynamicHyperLink ID="DynamicHyperLink1" OnPreRender="DynamicHyperLink1_PreRender" runat="server" Text="<%# this.ItemDetailsCommandText%>"
                                                CssClass="ActionLink hvr-sweep-to-right more" />
                                        <%--    <asp:DynamicHyperLink ID="DynamicHyperLink2" OnPreRender="DynamicHyperLink1_PreRender" Action="Edit" runat="server" Text="<%# this.EditItemCommandText%>"
                                                CssClass="ActionLink hvr-sweep-to-right more EditLink" />--%>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                                        </asp:HyperLink>
                                    </div>
                                    <%--                            <%if (RowCount == 3){%>
                            <div class="clearfix"></div>
                            </div>
                            <div class="deal-top-top">
                                <%RowCount = 0;}
                              else { RowCount++; }%>--%>
                                </ItemTemplate>
                                <GroupTemplate>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                </GroupTemplate>
                                <EmptyDataTemplate>
                                    <div class="info">
                                        <div class="container">
                                            <h4 style="margin-bottom: -10px; padding-bottom: 1cm;">
                                                <img class="infoimg" src="/CustomDesign/images/info.png" />
                                                <%= Resources.RealEstate.No_Records %></h4>
                                        </div>
                                    </div>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>

                                    <table class="table-pager">
                                        <tr>
                                            <td>
                                                <hr />
                                                <div class="container">
                                                    <div style="text-align: center">
                                                        <div style="display: inline-block">
                                                            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="listView" PageSize="12">
                                                                <Fields>
                                                                    <asp:TemplatePagerField OnPagerCommand="PagerCommand">
                                                                        <PagerTemplate>
                                                                            <nav id="navpager">
                                                                                <ul class="pagination">
                                                                                    <li>
                                                                                        <asp:LinkButton ID="navFrsrbtn" runat="server" CommandName="First"><span aria-hidden="true" style="color:#27DA93"><i class="fa fa-step-backward" aria-hidden="true"></i></span></asp:LinkButton></li>
                                                                                    <li>
                                                                                        <asp:LinkButton ID="navPrevbtn" runat="server" CommandName="Previous"><span aria-hidden="true" style="color:#27DA93"><i class="fa fa-arrow-left" aria-hidden="true"></i></span></asp:LinkButton></li>
                                                                                    <li>
                                                                        </PagerTemplate>
                                                                    </asp:TemplatePagerField>
                                                                    <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="num-pager" CurrentPageLabelCssClass="num-pager-sel" ButtonCount="4" />
                                                                    <asp:TemplatePagerField OnPagerCommand="PagerCommand">
                                                                        <PagerTemplate>
                                                                            </li>
                                                         <li>
                                                             <asp:LinkButton ID="navNextbtn" runat="server" CommandName="Next"><span aria-hidden="true" style="color:#27DA93"><i class="fa fa-arrow-right" aria-hidden="true"></i></span></asp:LinkButton></li>
                                                                            <li>
                                                                                <asp:LinkButton ID="navLastbtn" runat="server" CommandName="Last"><span aria-hidden="true" style="color:#27DA93"><i class="fa fa-step-forward" aria-hidden="true"></i></span></asp:LinkButton></li>
                                                                            </ul></nav>
                                                                        </PagerTemplate>
                                                                    </asp:TemplatePagerField>
                                                                </Fields>
                                                            </asp:DataPager>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                </div>
            </div>
            <div id="insert">
                <div class="clearfix"></div>

                <center>
                <div class="create"><div class="login-top">
                <label style="font-size:26px;position:center">
                <asp:DynamicHyperLink ID="InsertHyperLink" runat="server" Action="Insert">
                  <%= Resources.RealEstate.NewRecord %></asp:DynamicHyperLink></label></div></div></center>
            </div>
            <div id="else">
                <ef:EntityDataSource ID="GridDataSource" runat="server" EnableDelete="true"
                    OnQueryCreated="GridDataSource_QueryCreated"
                    OnDeleting="GridDataSource_Deleting"
                    OnDeleted="GridDataSource_Deleted" />
                <asp:QueryExtender TargetControlID="GridDataSource" ID="GridQueryExtender" runat="server">
                    <asp:DynamicFilterExpression ControlID="FilterRepeater" />
                </asp:QueryExtender>
                <asp:QueryExtender TargetControlID="GridDataSource" ID="dateFilter" runat="server">
                    <asp:CustomExpression OnQuerying="DateFilter_Querying"></asp:CustomExpression>
                </asp:QueryExtender>
                <ajaxToolkit:ModalPopupExtender ID="MPE001" BackgroundCssClass="maskbg"
                    runat="server" TargetControlID="dummyLink" PopupControlID="divLoader" Drag="false"
                    RepositionMode="None" BehaviorID="bMPLoad" />
                <asp:HyperLink ID="dummyLink" runat="server" CssClass="hidden"
                    NavigateUrl=""></asp:HyperLink><ServerControls:Loader runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
