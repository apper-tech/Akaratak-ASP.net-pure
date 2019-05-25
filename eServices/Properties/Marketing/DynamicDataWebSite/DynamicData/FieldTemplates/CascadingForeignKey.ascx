<%@ Control Language="C#" CodeBehind="CascadingForeignKey.ascx.cs" Inherits="DynamicDataWebSite.CascadingForeignKeyField" %>

<asp:HyperLink ID="HyperLink1" runat="server"
    Text="<%# GetDisplayString() %>"
    NavigateUrl="<%# GetNavigateUrl() %>"  />

