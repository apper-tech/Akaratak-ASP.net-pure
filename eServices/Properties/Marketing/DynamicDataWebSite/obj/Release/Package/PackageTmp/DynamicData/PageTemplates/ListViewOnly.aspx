<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="List.aspx.cs" Inherits="DynamicDataWebSite.List" %>

<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="asp" %>


<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">
    <%--<style>
        .EditLink {
            <%= DynamicDataLibrary.AuthorizationManager.CheckUrlAccessForPrincipal(table.GetActionPath(PageAction.Edit), this.Page.User)  
                            ? "" 
                            : "display: none;" %>
        }
    </style>
    <style>
        .InsertLink {
            <%= DynamicDataLibrary.AuthorizationManager.CheckUrlAccessForPrincipal(table.GetActionPath(PageAction.Insert), this.Page.User) 
                            ? "" : 
                            "display: none;" %>
        }
    </style>--%>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="GridView1" />
        </DataControls>
    </asp:DynamicDataManager>

    <h2 class="DDSubHeader"><%= table.DisplayName%></h2>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="DD">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
                    HeaderText="<%$Resources:DynamicData, ValidationSummaryHeaderText%>" CssClass="DDValidator" />
                <asp:DynamicValidator runat="server" ID="GridViewValidator" ControlToValidate="GridView1" Display="None" CssClass="DDValidator" />

                <asp:QueryableFilterRepeater runat="server" ID="FilterRepeater">
                    <ItemTemplate>
                        <div class="DDFilterContainer">
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("DisplayName") + ":"%>' OnPreRender="Label_PreRender" />
                            <asp:DynamicFilter runat="server" ID="DynamicFilter" OnFilterChanged="DynamicFilter_FilterChanged" />
                        </div>
                    </ItemTemplate>
                </asp:QueryableFilterRepeater>
                <br />
            </div>
            <div class="grid_overflow">
                <asp:GridView ID="GridView1" runat="server" DataSourceID="GridDataSource" EnablePersistedSelection="true"
                    AllowPaging="True" AllowSorting="True" CssClass="DDGridView"
                    OnRowDeleting="GridView1_RowDeleting" OnRowDeleted="GridView1_RowDeleted"
                    RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6" PageSize="10" OnRowCreated="GridView1_RowCreated"
                    OnRowDataBound="GridView1_RowDataBound" EnableViewState="false" OnPageIndexChanging="GridView1_PageIndexChanging">
                    <%--<Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:DynamicHyperLink ID="DynamicHyperLink1" runat="server" Text="<%$Resources:DynamicData, Details%>" />
                                <asp:DynamicHyperLink ID="DynamicHyperLink2" runat="server" Action="Edit" Text="<%$Resources:DynamicData, Update%>"
                                    CssClass="EditLink" />
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Delete" Text="<%$Resources:DynamicData, Delete%>"
                                    CssClass="InsertLink"
                                    OnClientClick="<%$Resources:DynamicData, DelConfirm%>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>--%>

                    <PagerStyle CssClass="DDFooter" />
                    <PagerTemplate>
                        <asp:GridViewPager ID="GridViewPager1" runat="server" />
                    </PagerTemplate>
                    <EmptyDataTemplate>
                        <asp:Literal ID="Literal1" runat="server"
                            Text="<%$Resources:DynamicData, EmptyRecords%>" />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
            <asp:EntityDataSource ID="GridDataSource" runat="server" EnableDelete="true" OnQueryCreated="GridDataSource_QueryCreated" />

            <asp:QueryExtender TargetControlID="GridDataSource" ID="GridQueryExtender" runat="server">
                <asp:DynamicFilterExpression ControlID="FilterRepeater" />
            </asp:QueryExtender>

            <br />
            
            <div style="display: none;">
                <br />

                <div class="DDBottomHyperLink">
                    <asp:DynamicHyperLink ID="InsertHyperLink" runat="server" Action="Insert" CssClass="InsertLink"><img id="Img1" runat="server" src="~/DynamicData/Content/Images/plus.gif" alt="إضافة جديد" /><asp:Literal ID="Literal2" runat="server" 
                        Text="<%$Resources:DynamicData, NewRecord %>" /></asp:DynamicHyperLink>
                </div>
            </div>
            <input id="dummy" type="button" style="display: none" runat="server" />
            
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderErrorMessage" runat="server" BackgroundCssClass="modalBackground"
                TargetControlID="dummy"
                PopupControlID="PanelErrorMessage">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="PanelErrorMessage" runat="server" CssClass="modalPanel PanelErrorAndMessages">
                <asp:Label ID="LabelErrorMessage" Text="" runat="server" />
                <table id="Table2" class="DDDetailsTable" cellpadding="6">
                    <tr class="td">
                        <td colspan="2">
                            <asp:LinkButton ID="LinkButtonErrorMessageOk" runat="server" Text="<%$Resources:DynamicData, Ok%>" 
                                OnClick="LinkButtonErrorMessageOK_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderConfirmationMessage" runat="server" BackgroundCssClass="modalBackground"
                TargetControlID="dummy"
                PopupControlID="PanelConfirmationMessage">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="PanelConfirmationMessage" runat="server" CssClass="modalPanel PanelErrorAndMessages">
                <asp:Label ID="LabelConfirmationMessage" Text="" runat="server" />
                <table id="detailsTable" class="DDDetailsTable" cellpadding="6">
                    <tr class="td">
                        <td colspan="2">
                            <asp:LinkButton ID="LinkButtonConfirmationDelete" runat="server" Text="<%$Resources:DynamicData, Delete%>" OnClick="LinkButtonConfirmationDelete_Click" />
                            <asp:LinkButton ID="LinkButtonConfirmationCancel" runat="server" Text="<%$Resources:DynamicData, Cancel%>" CausesValidation="false" OnClick="LinkButtonConfirmationCancel_Click" CommandName="Cancel" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderInformationMessage" runat="server" BackgroundCssClass="modalBackground"
                TargetControlID="dummy"
                PopupControlID="PanelInformationMessage">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="PanelInformationMessage" runat="server" CssClass="modalPanel PanelErrorAndMessages">
                <asp:Label ID="LabelInformationMessage" Text="" runat="server" />
                <table id="Table1" class="DDDetailsTable" cellpadding="6">
                    <tr class="td">
                        <td colspan="2">
                            <asp:LinkButton ID="LinkButtonInformationMessageOK" runat="server" Text="<%$Resources:DynamicData, Ok%>" OnClick="LinkButtonInformationMessageOK_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

