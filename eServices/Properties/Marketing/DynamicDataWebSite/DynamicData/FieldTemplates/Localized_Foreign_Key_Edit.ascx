<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Localized_Foreign_Key_Edit.ascx.cs" Inherits="DynamicDataWebSite.DynamicData.FieldTemplates.Localized_Foreign_Key_Edit" %>
<asp:HyperLink ID="HyperLink1" runat="server"
    
    NavigateUrl="<%# GetNavigateUrl() %>"  />

<asp:DropDownList ID="DropDownList1" runat="server" CssClass="in-drop"    
    OnDataBound="DropDownList1_DataBound">
</asp:DropDownList>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="DDControl DDValidator" ControlToValidate="DropDownList1" Display="Dynamic" Enabled="false" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="DDControl DDValidator" ControlToValidate="DropDownList1" Display="Dynamic" />
<div class="clearfix"> </div>