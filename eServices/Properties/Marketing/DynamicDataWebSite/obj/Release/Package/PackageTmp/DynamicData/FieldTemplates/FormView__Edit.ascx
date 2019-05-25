<%@ Control Language="C#" CodeBehind="FormView__Edit.ascx.cs" Inherits="DynamicDataWebSite.FormView_EditField" %>

<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="asp" %>

Taken Whithout modifications from GridView_Edit!

<%--<h2 class="DDSubHeader"><%= table.DisplayName%></h2>--%>

<asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
    <DataControls>
        <asp:DataControlReference ControlID="GridView1" />
    </DataControls>
</asp:DynamicDataManager>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server" />

<div class="DD">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
        HeaderText="<%$Resources:DynamicData, ValidationSummaryHeaderText%>" CssClass="DDValidator" />
    <asp:DynamicValidator runat="server" ID="GridViewValidator" ControlToValidate="FormView1" Display="None" CssClass="DDValidator" />
</div>

<asp:GridView ID="GridView1" runat="server" DataSourceID="GridDataSource" EnablePersistedSelection="True"
    AllowPaging="True" AllowSorting="True" CssClass="DDGridView" 
    RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6" PageSize="15"
    AutoGenerateColumns="true" OnRowCreated="GridView1_RowCreated"
    OnRowDataBound="GridView1_RowDataBound">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="LinkButtonEdit" runat="server" CommandName="Edit" Text="<%$Resources:DynamicData, Update%>" />
                <asp:LinkButton ID="LinkButtonDelete" runat="server" CommandName="Delete" Text="<%$Resources:DynamicData, Delete%>"
                    OnClientClick="<%$Resources:DynamicData, DelConfirm%>" />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="<%$Resources:DynamicData, Update1%>" />
                <asp:LinkButton ID="LinkButtonCancel" runat="server" CommandName="Cancel" Text="<%$Resources:DynamicData, Cancel%>" />
            </EditItemTemplate>
            <InsertItemTemplate>
                <asp:LinkButton ID="LinkButtonCancel" runat="server" CommandName="Cancel" Text="<%$Resources:DynamicData, Cancel%>" />
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
    <asp:FormView ID="FormView1" runat="server" DataSourceID="GridDataSource" RenderOuterTable="false"  DefaultMode="Insert"
        OnItemInserting="FormView1_ItemInserting" OnItemInserted="FormView1_ItemInserted" OnDataBound="FormView1_DataBound">
        <HeaderTemplate>
            <table id="detailsTable" class="DDDetailsTable" cellpadding="6">
        </HeaderTemplate>
        <InsertItemTemplate>
            <asp:DynamicEntity ID="DynamicEntityInsert" runat="server" Mode="Insert" />
            <tr class="td">
                <td colspan="2">
                    <asp:LinkButton ID="LinkButtonInsert" runat="server" CommandName="Insert" Text="<%$Resources:DynamicData, Add %>" />
                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CommandName="Cancel" Text="<%$Resources:DynamicData, Cancel %>"
                        CausesValidation="false" OnClick="LinkButtonCancel_Click" />
                </td>
            </tr>
        </InsertItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:FormView>
</asp:Panel>

<asp:EntityDataSource runat="server" ID="GridDataSource" OnContextCreating="GridDataSource_ContextCreating" >
</asp:EntityDataSource>
