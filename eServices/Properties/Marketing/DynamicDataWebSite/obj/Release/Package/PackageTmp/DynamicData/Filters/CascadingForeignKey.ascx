<%@ Control Language="C#" CodeBehind="CascadingForeignKey.ascx.cs" Inherits="DynamicDataWebSite.CascadingForeignKeyFilter" %>

<asp:DropDownList runat="server" ID="DropDownList1" AutoPostBack="True" CssClass="in-drop"
    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" onchange="scrolltoreslist()">
</asp:DropDownList>

