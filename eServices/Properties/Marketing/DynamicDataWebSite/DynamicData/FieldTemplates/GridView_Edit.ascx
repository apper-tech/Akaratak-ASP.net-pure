<%@ Control Language="C#" CodeBehind="GridView_Edit.aspx.cs" Inherits="DynamicDataWebSite.GridView_EditField" %>

<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="asp" %>


<%--<h2 class="DDSubHeader"><%= table.DisplayName%></h2>--%>

<asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
    <DataControls>
        <asp:DataControlReference ControlID="GridView1" />
    </DataControls>
</asp:DynamicDataManager>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server" />

<div class="DD">
    <asp:ValidationSummary ID="ValidationSummaryGridView" runat="server" EnableClientScript="true"
        HeaderText="<%$Resources:DynamicData, ValidationSummaryHeaderText%>" CssClass="DDValidator" />
    <asp:DynamicValidator runat="server" ID="DynamicValidatorGridView" ControlToValidate="GridView1" CssClass="DDValidator" />
    <asp:ValidationSummary ID="ValidationSummaryFormView" runat="server" EnableClientScript="true"
        HeaderText="<%$Resources:DynamicData, ValidationSummaryHeaderText%>" CssClass="DDValidator" />
    <asp:DynamicValidator runat="server" ID="DynamicValidatorFormView" ControlToValidate="FormView1" CssClass="DDValidator" />
</div>

<asp:GridView ID="GridView1" runat="server" DataSourceID="DataSource" EnablePersistedSelection="True"
    AllowPaging="True" AllowSorting="True" CssClass="DDGridView"
    RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6" PageSize="15"
    OnRowDeleting="GridView1_RowDeleting" OnRowDeleted="GridView1_RowDeleted"
    OnRowUpdated="GridView1_RowUpdated" OnRowUpdating="GridView1_RowUpdating"
    AutoGenerateColumns="true" OnRowCreated="GridView1_RowCreated"
    OnRowDataBound="GridView1_RowDataBound">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="LinkButtonEdit" runat="server" CommandName="Edit" CssClass="ActionLink" Text="<%$Resources:DynamicData, Update%>"
                    OnLoad="LinkButtonEdit_Load" />
                <asp:LinkButton ID="LinkButtonDelete" runat="server" CommandName="Delete" CssClass="ActionLink" Text="<%$Resources:DynamicData, Delete%>"
                    OnClientClick="<%$Resources:DynamicData, DelConfirm%>" />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" CssClass="ActionLink" Text="<%$Resources:DynamicData, Update1%>"
                    OnLoad="LinkButtonUpdate_Load" />
                <asp:LinkButton ID="LinkButtonCancel" runat="server" CommandName="Cancel" CssClass="ActionLink" Text="<%$Resources:DynamicData, Cancel%>" />
            </EditItemTemplate>
            <InsertItemTemplate>
                <asp:LinkButton ID="LinkButtonCancel" runat="server" CommandName="Cancel" CssClass="ActionLink" Text="<%$Resources:DynamicData, Cancel%>" />
            </InsertItemTemplate>
        </asp:TemplateField>
    </Columns>
    <HeaderStyle CssClass="th"></HeaderStyle>
    <PagerStyle CssClass="DDFooter" />
    <PagerTemplate>
        <asp:GridViewPager ID="GridViewPager1" runat="server" />
    </PagerTemplate>
    <EmptyDataTemplate>
        <asp:Literal ID="Literal1" runat="server"
            Text="<%$Resources:DynamicData, EmptyRecords%>" />
    </EmptyDataTemplate>
    <RowStyle CssClass="td"></RowStyle>
</asp:GridView>

<asp:LinkButton ID="LinkButtonNew" runat="server" Text="<%$Resources:DynamicData, NewRecord %>" OnClick="LinkButtonNew_Click" />

<asp:Panel ID="NewRecordPanel" runat="server" Visible="false">
    <br />
    <asp:FormView ID="FormView1" runat="server" DataSourceID="DataSource" RenderOuterTable="false" DefaultMode="Insert"
        OnItemInserting="FormView1_ItemInserting" OnItemInserted="FormView1_ItemInserted"
        OnDataBound="FormView1_DataBound"
        OnPreRender="FormView1_PreRender">
        <HeaderTemplate>
            <table id="detailsTable" class="DDDetailsTable" cellpadding="6">
        </HeaderTemplate>
        <InsertItemTemplate>
            <asp:DynamicEntity ID="DynamicEntityInsert" runat="server" Mode="Insert"  OnLoad="DynamicEntity_Load" />
            <tr class="td">
                <td colspan="2">
                    <asp:LinkButton ID="LinkButtonInsert" runat="server" CommandName="Insert" CssClass="ActionLink"
                        Text="<%$Resources:DynamicData, Add %>" OnLoad="LinkButtonInsert_Load" />
                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CommandName="Cancel" CssClass="ActionLink"
                        Text="<%$Resources:DynamicData, Cancel %>"
                        CausesValidation="false" OnClick="LinkButtonCancel_Click" />
                </td>
            </tr>
        </InsertItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:FormView>
</asp:Panel>

<ef:EntityDataSource runat="server" ID="DataSource" OnContextCreating="DataSource_ContextCreating" OnQueryCreated="DataSource_QueryCreated"
    OnUpdating="DataSource_Updating" OnUpdated="DataSource_Updated"
    OnInserting="DataSource_Inserting" OnInserted="DataSource_Inserted" />


<input id="dummyErrorMessage" type="button" style="display: none" runat="server" />
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderErrorMessage" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="dummyErrorMessage"
    PopupControlID="PanelErrorMessage">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="PanelErrorMessage" runat="server" CssClass="modalPanel PanelErrorAndMessages">
    <asp:Label ID="LabelErrorMessage" Text="" runat="server" />
    <table id="Table2" class="DDDetailsTable" cellpadding="6">
        <tr class="td">
            <td>
                <asp:LinkButton ID="LinkButtonErrorMessageOk" runat="server" Text="<%$Resources:DynamicData, Ok%>"
                    OnClick="LinkButtonErrorMessageOK_Click" CssClass="ActionLink"
                    ValidationGroup="NONE" />
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
    <table id="Table1" class="DDDetailsTable" cellpadding="6">
        <tr class="td">
            <td>
                <asp:LinkButton ID="LinkButtonConfirmationOk" runat="server" Text="<%$Resources:DynamicData, Confirm%>" CssClass="ActionLink" OnClick="LinkButtonConfirmationOk_Click"
                    ValidationGroup="NONE" />
                <asp:LinkButton ID="LinkButtonConfirmationCancel" runat="server" Text="<%$Resources:DynamicData, Cancel%>" CssClass="ActionLink" CausesValidation="false" OnClick="LinkButtonConfirmationCancel_Click" CommandName="Cancel"
                    ValidationGroup="NONE" />
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
    <table id="Table3" class="DDDetailsTable" cellpadding="6">
        <tr class="td">
            <td>
                <asp:LinkButton ID="LinkButtonInformationMessageOK" runat="server" Text="<%$Resources:DynamicData, Ok%>" CssClass="ActionLink" OnClick="LinkButtonInformationMessageOK_Click"
                    ValidationGroup="NONE" />
            </td>
        </tr>
    </table>
</asp:Panel>
