<%@ Control Language="C#" CodeBehind="ForeignKey_Edit.ascx.cs" Inherits="DynamicDataWebSite.ForeignKey_EditField" %>


<asp:HyperLink ID="HyperLink1" runat="server"
    
    NavigateUrl="<%# GetNavigateUrl() %>"  />

<asp:DropDownList ID="DropDownList1" runat="server" 
    OnDataBound="DropDownList1_DataBound">
</asp:DropDownList>

<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="DDControl DDValidator" ControlToValidate="DropDownList1" Display="Dynamic" Enabled="false" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="DDControl DDValidator" ControlToValidate="DropDownList1" Display="Dynamic" />

