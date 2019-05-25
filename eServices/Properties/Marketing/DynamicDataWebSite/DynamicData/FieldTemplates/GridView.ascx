<%@ Control Language="C#" CodeBehind="GridView.ascx.cs" Inherits="DynamicDataWebSite.GridViewField" %>

<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="asp" %>

<asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
    <DataControls>
        <asp:DataControlReference ControlID="GridView1" />
    </DataControls>
</asp:DynamicDataManager>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server" />


<asp:GridView ID="GridView1" runat="server" DataSourceID="GridDataSource" EnablePersistedSelection="true"
    AllowPaging="True" AllowSorting="True" CssClass="DDGridView"
    RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6" PageSize="15"
    AutoGenerateColumns="true" >
    <PagerStyle CssClass="DDFooter" />
    <PagerTemplate>
        <asp:GridViewPager ID="GridViewPager1" runat="server" />
    </PagerTemplate>
    <EmptyDataTemplate>
        <asp:Literal ID="Literal1" runat="server"
            Text="<%$Resources:DynamicData, EmptyRecords%>" />
    </EmptyDataTemplate>
</asp:GridView>

<asp:LinqDataSource runat="server" ID="GridDataSource" EnableDelete="true" OnQueryCreated="GridDataSource_QueryCreated">
</asp:LinqDataSource>

