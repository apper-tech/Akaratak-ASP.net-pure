<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Url_Edit.ascx.cs" Inherits="DynamicDataWebSite.Url_EditField" %>


<asp:TextBox ID="TextBox1" runat="server" Text='<%# FieldValueEditString %>' Columns="10" TextMode="Url"
    OnPreRender="TextBox1_PreRender"></asp:TextBox>


<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Dynamic" />

