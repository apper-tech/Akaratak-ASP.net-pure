<%@ Control Language="C#" CodeBehind="CascadingForeignKey_Edit.ascx.cs" Inherits="$rootnamespace$.CascadingForeignKey_EditField" %>

<asp:DropDownList ID="DropDownList1" runat="server" CssClass="DDDropDown" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
</asp:DropDownList>

<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="DDControl DDValidator" ControlToValidate="DropDownList1" Display="Static" Enabled="false" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="DDControl DDValidator" ControlToValidate="DropDownList1" Display="Static" />

