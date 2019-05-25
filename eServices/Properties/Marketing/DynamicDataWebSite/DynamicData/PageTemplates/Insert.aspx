<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="Insert.aspx.cs" Inherits="DynamicDataWebSite.Insert"
    MaintainScrollPositionOnPostback="true" %>
<%@ Register Src="~/ServerControls/Loader.ascx" TagName="Loader" TagPrefix="ServerControls" %>
<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="FormView1" />
        </DataControls>
    </asp:DynamicDataManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="login-right">
                <div class="container">
                    <h3>
                        <%= Resources.RealEstate.Page_Insert_Title %></h3>
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" EnableClientScript="true"
                        HeaderText="<%$Resources:DynamicData, ValidationSummaryHeaderText%>" CssClass="text-danger" />
                    <asp:DynamicValidator runat="server" ID="DetailsViewValidator" ControlToValidate="FormView1" Display="None" CssClass="DDValidator" />

                   
                        <asp:FormView runat="server" ID="FormView1" DataSourceID="DetailsDataSource" DefaultMode="Insert"
                            OnItemInserting="FormView1_ItemInserting"
                            OnItemCommand="FormView1_ItemCommand" OnItemInserted="FormView1_ItemInserted" RenderOuterTable="false"
                            OnPreRender="FormView1_PreRender">
                            <InsertItemTemplate>
                                <div class="form-info fontstyled1">
                                <asp:DynamicEntity runat="server" Mode="Insert" ID="DynamicEntity" OnLoad="DynamicEntity_Load" />
                                 </div>
                                    <div class="create">
                                <table>
                                    <tr style="position: center">
                                        <td colspan="2">
                                                <asp:LinkButton runat="server" Style="font-size: 30px" ID="LinkButtonInsert" CommandName="Insert" CssClass="hvr-sweep-to-right"
                                                    OnLoad="LinkButtonInsert_Load"><%= NewItemCommandText %></asp:LinkButton>
                                        </td>
                                        <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td colspan="2">
                                                <asp:LinkButton runat="server" CommandName="Cancel" Style="font-size: 30px" CssClass="hvr-sweep-to-right" Text="<%$Resources:DynamicData, Back %>" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </table>
                                       </div>
                                 <div class="clearfix"></div>
                            </InsertItemTemplate>
                        </asp:FormView>
                   <div class="clearfix"></div>
                </div>
               <div class="clearfix"></div>
            </div>  
            <script>
                function pageLoad(sender, args) {
                    //add every client postback control here
                    LoadMap(null);
                    LoadCal('<%= System.Threading.Thread.CurrentThread.CurrentCulture.Name%>');
                }
            </script>
            <ef:EntityDataSource ID="DetailsDataSource" runat="server" EnableInsert="true"
                OnInserting="DetailsDataSource_Inserting"
                OnInserted="DetailsDataSource_Inserted" />
            <ajaxToolkit:ModalPopupExtender ID="MPE001" BackgroundCssClass="maskbg"
                    runat="server" TargetControlID="dummyLink" PopupControlID="divLoader" Drag="false"
                    RepositionMode="None" BehaviorID="bMPLoad" />
                <asp:HyperLink ID="dummyLink" runat="server" CssClass="hidden"
                    NavigateUrl=""></asp:HyperLink>
               <ServerControls:Loader runat="server" />
            <div id="msgpop">
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
</asp:Content>

