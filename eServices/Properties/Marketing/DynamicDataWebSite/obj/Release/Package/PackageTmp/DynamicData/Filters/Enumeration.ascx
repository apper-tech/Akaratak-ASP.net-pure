﻿<%@ Control Language="C#" CodeBehind="Enumeration.ascx.cs" Inherits="DynamicDataWebSite.EnumerationFilter" %>

<asp:DropDownList runat="server" ID="DropDownList1" AutoPostBack="True" CssClass="DDFilter"
    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
  <asp:ListItem Text="الجميع" Value="" />
</asp:DropDownList>

