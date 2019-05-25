<%@ Control Language="C#" CodeBehind="FormView.ascx.cs" Inherits="DynamicDataWebSite.FormViewField" %>

<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="asp" %>

<asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
    <DataControls>
        <asp:DataControlReference ControlID="GridView1" />
    </DataControls>
</asp:DynamicDataManager>
<%-- Tested with view only without edit or delete --%>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
            HeaderText="<%$Resources:DynamicData, ValidationSummaryHeaderText%>" CssClass="DDValidator" />
        <asp:DynamicValidator runat="server" ID="DetailsViewValidator" ControlToValidate="FormView1" Display="None" 
            CssClass="DDValidator" />

        <asp:FormView runat="server" ID="FormView1" DataSourceID="DetailsDataSource"
            OnPreRender="FormView1_PreRender"
            OnItemDeleting="FormView1_ItemDeleting"
            OnItemDeleted="FormView1_ItemDeleted" RenderOuterTable="false" OnDataBound="FormView1_DataBound">
            <ItemTemplate>
                <table id="detailsTable" class="DDDetailsTable" cellpadding="6">
                    <asp:DynamicEntity ID="DynamicEntity1" runat="server" />
                </table>
            </ItemTemplate>
            <EmptyDataTemplate>
                <div class="DDNoItem">
                    <asp:Literal ID="Literal2" runat="server"
                        Text="<%$Resources:DynamicData, ThereIsNoData%>" />
                </div>
            </EmptyDataTemplate>
        </asp:FormView>

        <asp:LinqDataSource ID="DetailsDataSource" runat="server" EnableDelete="true"
            OnQueryCreated="DetailsDataSource_QueryCreated"
            OnDeleting="DetailsDataSource_Deleting"
            OnDeleted="DetailsDataSource_Deleted"  />

        <asp:QueryExtender TargetControlID="DetailsDataSource" ID="DetailsQueryExtender" runat="server">
            <asp:DynamicRouteExpression />
        </asp:QueryExtender>

        <%--<asp:Panel ID="PanelDispInItemDetailsPage" runat="server" Visible="false">
            <br />
            <div class="DDBottomHyperLink">
                استعراض العنصر في صفحته الخاصة:
            <br />
                <asp:DynamicHyperLink ID="ListHyperLink9" runat="server" Action="List">
                    <asp:Literal ID="Literal2" runat="server"
                        Text="<%$Resources:DynamicData, AllRecords%>" />
                </asp:DynamicHyperLink>
            </div>
        </asp:Panel>--%>
        
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
                            OnClick="LinkButtonErrorMessageOK_Click"  CssClass="ActionLink"/>
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
                        <asp:LinkButton ID="LinkButtonConfirmationDelete" runat="server" Text="<%$Resources:DynamicData, Delete%>" CssClass="ActionLink" OnClick="LinkButtonConfirmationDelete_Click" />
                        <asp:LinkButton ID="LinkButtonConfirmationCancel" runat="server" Text="<%$Resources:DynamicData, Cancel%>" CssClass="ActionLink" CausesValidation="false" OnClick="LinkButtonConfirmationCancel_Click" CommandName="Cancel" />
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
                        <asp:LinkButton ID="LinkButtonInformationMessageOK" runat="server" Text="<%$Resources:DynamicData, Ok%>" CssClass="ActionLink" OnClick="LinkButtonInformationMessageOK_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
