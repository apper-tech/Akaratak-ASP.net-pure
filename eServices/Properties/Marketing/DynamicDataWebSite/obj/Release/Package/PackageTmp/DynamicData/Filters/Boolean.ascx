<%@ Control Language="C#" CodeBehind="Boolean.ascx.cs" Inherits="DynamicDataWebSite.BooleanFilter" %>

<asp:DropDownList runat="server" ID="DropDownList1" AutoPostBack="True" CssClass="in-drop"
    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" onchange="scrolltoreslist()">
</asp:DropDownList>

