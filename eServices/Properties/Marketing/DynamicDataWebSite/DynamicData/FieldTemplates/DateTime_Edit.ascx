<%@ Control Language="C#" CodeBehind="DateTime_Edit.ascx.cs" Inherits="DynamicDataWebSite.DateTime_EditField" %>


<asp:TextBox ID="TextBox1" runat="server" CssClass="DDTextBox" Text='<%# FormatFieldValue(FieldValueEditString) %>' 
    Columns="20" TextMode="DateTime" OnPreRender="TextBox1_PreRender"></asp:TextBox>
<asp:Label Text="" runat="server" ID="LableNote" />

<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Dynamic" />
<asp:CustomValidator runat="server" ID="DateValidator" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Dynamic" EnableClientScript="false" Enabled="false" OnServerValidate="DateValidator_ServerValidate" />

