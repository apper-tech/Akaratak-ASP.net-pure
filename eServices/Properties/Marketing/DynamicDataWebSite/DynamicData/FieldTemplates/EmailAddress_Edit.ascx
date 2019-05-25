<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmailAddress_Edit.ascx.cs" Inherits="DynamicDataWebSite.EmailAddress_EditField" %>
<asp:TextBox ID="TextBox1" runat="server" placeholder='<%#Column.DisplayName %>'
    OnPreRender="TextBox1_PreRender"></asp:TextBox>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Dynamic"  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Dynamic" />
