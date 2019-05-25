<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="Home.aspx.cs" Inherits="DynamicDataWebSite.Home" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server" />

    <h2 class="DDSubHeader">التبويبات الرئيسية</h2>

    <br /><br />

    <asp:GridView ID="Menu1" runat="server" AutoGenerateColumns="false"
        CssClass="DDGridView" RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6" ShowFooter="true">
        <Columns>
            <asp:TemplateField HeaderText="العنوان" SortExpression="TableName">
                <ItemTemplate>
                    <asp:DynamicHyperLink ID="HyperLink1" runat="server"><%# Eval("DisplayName") %></asp:DynamicHyperLink>
                </ItemTemplate>
<%--                <FooterStyle CssClass="td" />
                <FooterTemplate>
                    <a href="/Reports/">التقارير</a>
                </FooterTemplate>--%>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
</asp:Content>


