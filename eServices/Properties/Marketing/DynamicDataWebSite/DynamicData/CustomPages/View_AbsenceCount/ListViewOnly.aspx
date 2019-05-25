<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="ListViewOnly.aspx.cs"
    Inherits="DynamicDataWebSite.DynamicData.CustomPages.Absences.ListViewOnly" %>

<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="asp" %>


<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">
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
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("DisplayName") %>' OnPreRender="Label_PreRender" />
                            <asp:DynamicFilter runat="server" ID="DynamicFilter" OnFilterChanged="DynamicFilter_FilterChanged" />
                        </div>
                    </ItemTemplate>
                </asp:QueryableFilterRepeater>
                <br />
            </div>
            <div class="grid_overflow">
                <asp:GridView ID="GridView1" runat="server" DataSourceID="LinqDataSource" EnablePersistedSelection="true"
                    AllowPaging="True" AllowSorting="True" CssClass="DDGridView"
                    RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6" PageSize="15" OnRowCreated="GridView1_RowCreated"
                    OnRowDataBound="GridView1_RowDataBound"
                    EnableViewState="false">
<%--                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                عدد أيام الغياب
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" Text='<%#Eval("ContinuesCount") %>' runat="server" />
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

            <asp:LinqDataSource ID="LinqDataSource" runat="server" OnQueryCreated="LinqDataSource_QueryCreated"></asp:LinqDataSource>

            <asp:QueryExtender TargetControlID="LinqDataSource" ID="GridQueryExtender" runat="server">
                <asp:DynamicFilterExpression ControlID="FilterRepeater" />
            </asp:QueryExtender>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
