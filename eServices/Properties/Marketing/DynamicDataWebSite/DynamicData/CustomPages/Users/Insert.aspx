<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Insert.aspx.cs" Inherits="DynamicDataWebSite.DynamicData.CustomPages.Users.Insert" %>
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
            <asp:ValidationSummary ID="ValidationSummary" runat="server" EnableClientScript="true"
                HeaderText="<%$Resources:DynamicData, ValidationSummaryHeaderText%>" CssClass="text-danger" />
            <asp:DynamicValidator runat="server" ID="DetailsViewValidator" ControlToValidate="FormView1" Display="None" />

            <asp:FormView runat="server" ID="FormView1" DataSourceID="DetailsDataSource" DefaultMode="Insert"
                OnItemInserting="FormView1_ItemInserting"
                OnItemCommand="FormView1_ItemCommand" OnItemInserted="FormView1_ItemInserted" RenderOuterTable="false"
                OnPreRender="FormView1_PreRender" >
                <InsertItemTemplate>
                        <asp:DynamicEntity runat="server" Mode="Insert" ID="DynamicEntity" OnLoad="DynamicEntity_Load" >
                        </asp:DynamicEntity>   
                    <br />  
                    <center>
                      <label class="hvr-sweep-to-right">
                        <asp:Button runat="server" ID="LinkButtonInsert" CommandName="Insert"  OnLoad="LinkButtonInsert_Load" CssClass="fontstyled2" style="font-size:25px" Text="<%# Resources.RealEstate.Page_User_Insert_Submit %>"></asp:Button>
                        </label>
                        </center>
                </InsertItemTemplate>
            </asp:FormView>   
            <ef:EntityDataSource ID="DetailsDataSource" runat="server" EnableInsert="true"
                OnInserted="DetailsDataSource_Inserted" />
            <div id="msg">
            <input id="dummyErrorMessage" type="button" style="display: none" runat="server" />            
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderErrorMessage" runat="server" BackgroundCssClass="modalBackground"
                TargetControlID="dummyErrorMessage"
                PopupControlID="PanelErrorMessage">
            </ajaxToolkit:ModalPopupExtender>
        <%-- fix popup--%>
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
        </div>
                </ContentTemplate>
    </asp:UpdatePanel>
            </div>
          </div>
        </div>
       </div>
</asp:Content>
