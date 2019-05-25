<%@ Control Language="C#" CodeBehind="Text_Edit.ascx.cs" Inherits="DynamicDataWebSite.Text_EditField" %>

<asp:TextBox ID="TextBox1" runat="server" Text='<%# FieldValueEditString %>' CssClass="fontstyled1 black" ForeColor="Black" placeholder='<%#Column.Description %>' OnPreRender="TextBox1_PreRender"></asp:TextBox>

<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="text-danger" ControlToValidate="TextBox1" Display="Dynamic" />
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Dynamic" />

