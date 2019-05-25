<%@ Control Language="C#" CodeBehind="Html_Edit.ascx.cs" Inherits="DynamicDataWebSite.Html_EditField" %>

<%--<asp:TextBox ID="TextBox1" runat="server" Text='<%# FieldValueEditString %>' CssClass="DDTextBox" OnPreRender="TextBox1_PreRender"></asp:TextBox>

<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Dynamic" />
--%>

<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<div id="html">
<asp:TextBox ID="TextBox1" runat="server" Text='<%# FieldValueEditString %>' style="font-family:'Segoe UI';width:100%" OnPreRender="TextBox1_PreRender"></asp:TextBox>
<ajaxToolkit:HtmlEditorExtender ID="htmlEditorExtender1"
    TargetControlID="TextBox1" DisplaySourceTab="false"
    runat="server" /></div>
<asp:RequiredFieldValidator
    runat="server"
    ID="RequiredFieldValidator1"
    CssClass="droplist"
    ControlToValidate="TextBox1"
    Display="Dynamic"
    Enabled="false" />

<asp:RegularExpressionValidator
    runat="server"
    ID="RegularExpressionValidator1"
    CssClass="droplist"
    ControlToValidate="TextBox1"
    Display="Dynamic"
    Enabled="false" />

<asp:DynamicValidator
    runat="server"
    ID="DynamicValidator1"
    CssClass="droplist"
    ControlToValidate="TextBox1"
    Display="Dynamic" />