<%@ Control Language="C#" CodeBehind="Phone.ascx.cs" Inherits="DynamicDataWebSite.PhoneField" %>

<asp:HyperLink ID="HyperLinkUrl" runat="server" dir="ltr" NavigateUrl='<%# "tel:"+FieldValueString %>' Text="<%# FieldValueString %>" Target="_blank" />
