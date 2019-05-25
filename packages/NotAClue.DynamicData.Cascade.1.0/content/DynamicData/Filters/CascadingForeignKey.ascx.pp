<%@ Control Language="C#" CodeBehind="CascadingForeignKey.ascx.cs" Inherits="$rootnamespace$.CascadingForeignKeyFilter" %>

<asp:DropDownList runat="server" ID="DropDownList1" AutoPostBack="True" CssClass="DDFilter"
    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
    <asp:ListItem Text="All" Value="" />
</asp:DropDownList>

