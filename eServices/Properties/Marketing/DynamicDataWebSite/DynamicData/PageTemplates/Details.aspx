<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="Details.aspx.cs" Inherits="DynamicDataWebSite.Details" %>


<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">
    <style>
		.EditLink {
            <%= DynamicDataLibrary.AuthorizationManager.CheckUrlAccessForPrincipal(table.GetActionPath(PageAction.Edit), this.Page.User) 
                            ? "" 
                            : "display: none;" %>
        }
        .InsertLink {
            <%= DynamicDataLibrary.AuthorizationManager.CheckUrlAccessForPrincipal(table.GetActionPath(PageAction.Insert), this.Page.User) 
                            ? "" 
                            : "display: none;" %>
        }
        <%--Not used in this porject--%>
        .DeleteLink {
            <%= DynamicDataLibrary.AuthorizationManager.CheckUrlAccessForPrincipal(table.GetActionPath(PageAction.Insert).Replace("Insert","Delete"), this.Page.User) 
                            ? "" 
                            : "display: none;" %>
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="FormView1" />
        </DataControls>
    </asp:DynamicDataManager>
    <div class="print_button">
        <a onclick="window.open('<%= 
            this.Request.Url.OriginalString.Contains("?") ?
            this.Request.Url.OriginalString + "&Print=True" :
            this.Request.Url.OriginalString + "?Print=True" %>'); return false;" ><asp:Literal runat="server" Text="<%$Resources:DynamicData, Print%>" /></a>
    </div>
    
    <h2 class="DDSubHeader"><%= this.ItemDetailsTitle %></h2>
    

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
                HeaderText="<%$Resources:DynamicData, ValidationSummaryHeaderText%>" CssClass="DDValidator" />
            <asp:DynamicValidator runat="server" ID="DetailsViewValidator" ControlToValidate="FormView1" Display="None" CssClass="DDValidator" />

            <asp:FormView runat="server" ID="FormView1" DataSourceID="DetailsDataSource"
                OnItemDeleting="FormView1_ItemDeleting"
                OnItemDeleted="FormView1_ItemDeleted" RenderOuterTable="false" OnDataBound="FormView1_DataBound">
                <ItemTemplate>
                    <table id="detailsTable" class="DDDetailsTable" cellpadding="6">
                        <asp:DynamicEntity runat="server" />
                        <tr class="td">
                            <td colspan="2" class="SubmitButtonsContainer">
                                <span class="Submit">
                                <asp:DynamicHyperLink ID="DynamicHyperLink_Edit" runat="server" Action="Edit"
                                    CssClass="EditLink ActionLink" Text="<%# this.EditItemCommandText%>" /></span>

                                <asp:LinkButton ID="DynamicHyperLink_Delete" runat="server" CommandName="Delete"
                                    CssClass="InsertLink ActionLink"
                                    OnClientClick="<%# this.DeleteConfirmationMesssage %>" ><asp:Label CssClass="Submit" Text="<%# this.DeleteItemCommandText%>" runat="server" /></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <div class="DDNoItem">
                        <asp:Literal ID="Literal2" runat="server"
                            Text="<%$Resources:DynamicData, ItemDoesNotExist%>" />
                    </div>
                </EmptyDataTemplate>
            </asp:FormView>
            
            
            <ef:EntityDataSource ID="DetailsDataSource" runat="server" EnableDelete="true"
                 OnQueryCreated="DetailsDataSource_QueryCreated" 
                OnDeleting="DetailsDataSource_Deleting" 
                OnDeleted="DetailsDataSource_Deleted"/>

            <asp:QueryExtender TargetControlID="DetailsDataSource" ID="DetailsQueryExtender" runat="server">
                <asp:DynamicRouteExpression />
            </asp:QueryExtender>

            <br />

            <div class="DDBottomHyperLink">
                <asp:DynamicHyperLink ID="ListHyperLink" runat="server" Action="List">
                    <asp:Literal ID="LiteralShowAllRecordsLinkText" runat="server"
                        Text="<%$Resources:DynamicData, AllRecords%>" />
                </asp:DynamicHyperLink>
            </div>
            

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
            
            <input id="dummyConfirmationMessage" type="button" style="display: none" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderConfirmationMessage" runat="server" BackgroundCssClass="modalBackground"
                TargetControlID="dummyConfirmationMessage"
                PopupControlID="PanelConfirmation">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="PanelConfirmation" runat="server" CssClass="modalPanel">
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
            
            <input id="dummyInformationMessage" type="button" style="display: none" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderInformationMessage" runat="server" BackgroundCssClass="modalBackground"
                TargetControlID="dummyInformationMessage"
                PopupControlID="PanelInformationMessage">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="PanelInformationMessage" runat="server" CssClass="modalPanel">
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

