<%@ Page  Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="DynamicDataWebSite.DynamicData.CustomPages.Users.Edit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="FormView1" />
        </DataControls>
    </asp:DynamicDataManager>
   <div class="login-right">
	<div class="container">
    <h3><%= Page.Title %></h3>
            	<div class="login-top">
				<div class="form-info">	
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div dir="ltr">
            <asp:ValidationSummary ID="ValidationSummary" runat="server" EnableClientScript="true"
                HeaderText="<%$Resources:DynamicData, ValidationSummaryHeaderText%>" CssClass="DDValidator" />
            <asp:DynamicValidator runat="server" ID="DetailsViewValidator" ControlToValidate="FormView1" Display="None" CssClass="DDValidator" />
</div>
            <asp:FormView runat="server" ID="FormView1" DataSourceID="DetailsDataSource" DefaultMode="Edit"
                OnItemCommand="FormView1_ItemCommand" OnItemUpdated="FormView1_ItemUpdated" RenderOuterTable="false"
                OnItemUpdating="FormView1_ItemUpdating"
                OnPreRender="FormView1_PreRender"
                OnDataBound="FormView1_DataBound">
                <EditItemTemplate>
                  <asp:DynamicEntity runat="server" Mode="Edit" ID="DynamicEntity"
                            OnLoad="DynamicEntity_Load" />
                    <center>
                           <label class="hvr-sweep-to-right">         
                       <asp:Button runat="server" style="font-size:25px;font-family:'Segoe UI'" ID="LinkButtonUpdate" CommandName="Update" Text="<%$Resources:DynamicData, Confirm %>" OnLoad="LinkButtonUpdate_Load" />
                           </label>
                           <label class="hvr-sweep-to-right">
                            <asp:Button runat="server"  style="font-size:25px;font-family:'Segoe UI'" CommandName="Cancel" Text="<%$Resources:DynamicData, Back %>" CausesValidation="false" />
                           </label>
                        </center>
                             </EditItemTemplate>
                <EmptyDataTemplate>
                    <div class="DDNoItem">
                        <asp:Literal ID="Literal2" runat="server"
                            Text="<%$Resources:DynamicData, ItemDoesNotExist%>" />
                    </div>
                </EmptyDataTemplate>
            </asp:FormView>
            
            <ef:EntityDataSource ID="DetailsDataSource" runat="server" EnableUpdate="true"
                OnQueryCreated="DetailsDataSource_QueryCreated"
                OnUpdating="DetailsDataSource_Updating"
                OnUpdated="DetailsDataSource_Updated" />

            <asp:QueryExtender TargetControlID="DetailsDataSource" ID="DetailsQueryExtender" runat="server">
                <asp:DynamicRouteExpression />
            </asp:QueryExtender>

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
                            <asp:LinkButton ID="LinkButtonConfirmation" runat="server" Text="<%$Resources:DynamicData, Delete%>" OnClick="LinkButtonConfirmation_Click" />
                            <asp:LinkButton ID="LinkButtonConfirmationCancel" runat="server" Text="<%$Resources:DynamicData, Cancel%>" CausesValidation="false" OnClick="LinkButtonConfirmationCancel_Click" />
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
                    </div></div></div></div>
</asp:Content>
