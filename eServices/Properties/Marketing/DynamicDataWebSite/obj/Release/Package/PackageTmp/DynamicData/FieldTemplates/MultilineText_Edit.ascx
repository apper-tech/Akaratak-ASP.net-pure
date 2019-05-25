<%@ Control Language="C#" CodeBehind="MultilineText_Edit.ascx.cs" Inherits="DynamicDataWebSite.MultilineText_EditField" %>
<br />
<asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" style="font-family:'Segoe UI';width:100%" Font-Bold="false" ForeColor="Black" Font-Size="14" Text='<%# FieldValueEditString %>' placeholder='<%#Column.Description %>' Columns="75" Rows="5"
    OnPreRender="TextBox1_PreRender"></asp:TextBox>

<asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />
<asp:DynamicValidator runat="server" id="DynamicValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Dynamic" />

