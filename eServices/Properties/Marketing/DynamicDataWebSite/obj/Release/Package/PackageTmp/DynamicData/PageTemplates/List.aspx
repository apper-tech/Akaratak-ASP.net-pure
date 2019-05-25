<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="List.aspx.cs" Inherits="DynamicDataWebSite.List" %>

<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="asp" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">
    <style>
        .EditLink {
            <%= DynamicDataLibrary.AuthorizationManager.CheckUrlAccessForPrincipal(table.GetActionPath(PageAction.Edit), this.Page.User) ? "" : "display: none;"%>;
        }

        .InsertLink {
            <%= DynamicDataLibrary.AuthorizationManager.CheckUrlAccessForPrincipal(table.GetActionPath(PageAction.Insert), this.Page.User) ? "" : "display: none;"%>;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="GridView1" />
        </DataControls>
    </asp:DynamicDataManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="DD">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
                    HeaderText="<%$Resources:DynamicData, ValidationSummaryHeaderText%>" CssClass="DDValidator" />
                <asp:DynamicValidator runat="server" ID="GridViewValidator" ControlToValidate="GridView1" Display="None" CssClass="DDValidator" />
                <asp:QueryableFilterRepeater runat="server" ID="FilterRepeater" Visible="false" OnDataBinding="FilterRepeater_DataBinding">
                    <ItemTemplate>
                        <div class="DDFilterContainer">
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") + ":"%>' OnPreRender="Label_PreRender" />
                            <asp:DynamicFilter runat="server" ID="DynamicFilter" OnFilterChanged="DynamicFilter_FilterChanged" />
                        </div>
                    </ItemTemplate>
                </asp:QueryableFilterRepeater>
                <br />
            </div>
            <div class="privacy">
                <div class="container">
                    <h3 style="margin-bottom: -10px; padding-bottom: 1cm;"><%= Page.Title%></h3>
                </div>
            </div>
            <div class="grid_overflow">
                <div style="overflow-x: auto;">
                    <asp:GridView ID="GridView1" runat="server" DataSourceID="GridDataSource" EnablePersistedSelection="true"
                        AllowPaging="True" AllowSorting="True" CssClass="table table-bordered"
                        OnRowDeleting="GridView1_RowDeleting" OnRowDeleted="GridView1_RowDeleted"
                        RowStyle-CssClass="table-bordered" AlternatingRowStyle-CssClass="table-bordered" HeaderStyle-CssClass="table-condensed" CellPadding="6" PageSize="10" OnRowCreated="GridView1_RowCreated"
                        OnRowDataBound="GridView1_RowDataBound" EnableViewState="false" OnPageIndexChanging="GridView1_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:DynamicHyperLink runat="server" Action="Edit" Text='<%# this.EditItemCommandText%>'
                                        CssClass="EditLink ActionLink hvr-sweep-to-right more" />
                                    <asp:LinkButton runat="server" CommandName="Delete" Text="<%# this.DeleteItemCommandText%>"
                                        CssClass="InsertLink ActionLink hvr-sweep-to-right more" Visible="<%# this.CouldBeDeleted(Container) %>"
                                        OnClientClick='return confirm("Are you sure you want to delete this item?");' />
                                </ItemTemplate>

                            </asp:TemplateField>

                        </Columns>
                        <EmptyDataTemplate>
                            <hr />
                            <br />
                            <div class="error">
                                <div class="container">
                                    <h4 style="margin-bottom: -10px; padding-bottom: 1cm;">
                                        <asp:Literal ID="Literal1" runat="server"
                                            Text="<%# this.NoRecordsAtList %>" /></h4>
                                </div>
                            </div>
                            <br />
                            <hr />
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
            <ef:EntityDataSource ID="GridDataSource" runat="server" EnableDelete="true"
                OnQueryCreated="GridDataSource_QueryCreated"
                OnDeleting="GridDataSource_Deleting"
                OnDeleted="GridDataSource_Deleted" />

            <asp:QueryExtender TargetControlID="GridDataSource" ID="GridQueryExtender" runat="server">
                <asp:DynamicFilterExpression ControlID="FilterRepeater" />
            </asp:QueryExtender>
            <div id="Message">
                <input id="dummyErrorMessage" type="button" style="display: none" runat="server" />
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderErrorMessage" runat="server" BackgroundCssClass="modalBackground"
                    TargetControlID="dummyErrorMessage"
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
                <script>
                    function del() {
                        //delcall(document.getElementById('<%= LinkButtonConfirmationDelete.ClientID%>'))
                        alert(document.getElementById('<%= LinkButtonConfirmationDelete.ClientID%>').href);
                    }
                </script>
                <input id="dummyConfirmationMessage" type="button" style="display: none" runat="server" />
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderConfirmationMessage" runat="server" BackgroundCssClass="modalBackground"
                    TargetControlID="dummyConfirmationMessage"
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
                <center>
                <div class="create"><div class="login-top">
                <label style="font-size:26px;position:center">
                <asp:DynamicHyperLink ID="InsertHyperLink" runat="server" Action="Insert">
                    <%= this.NewItemLinkText%></asp:DynamicHyperLink>
            </label></div></div>
           </center>
                <input id="dummyInformationMessage" type="button" style="display: none" runat="server" />
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderInformationMessage" runat="server" BackgroundCssClass="modalBackground"
                    TargetControlID="dummyInformationMessage"
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
