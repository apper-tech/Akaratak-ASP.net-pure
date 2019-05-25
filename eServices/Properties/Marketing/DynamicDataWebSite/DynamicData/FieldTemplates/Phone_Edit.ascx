<%@ Control Language="C#" CodeBehind="Phone_Edit.ascx.cs" Inherits="DynamicDataWebSite.Phone_EditField" %>
<span dir="ltr">
    <asp:TextBox ID="TextBox1" runat="server" Text='<%# FieldValueEditString %>' CssClass="fontstyled2 black" ForeColor="Black" placeholder='<%#Column.DisplayName %>' OnPreRender="TextBox1_PreRender"></asp:TextBox>
</span>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="text-danger" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" CssClass="text-danger" ControlToValidate="TextBox1" Display="Dynamic" ValidationExpression="\+(9[976]\d|8[987530]\d|6[987]\d|5[90]\d|42\d|3[875]\d|2[98654321]\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|4[987654310]|3[9643210]|2[70]|7|1)\d{1,14}$" ErrorMessage="<%# Resources.DynamicData.Phone_MessageFormat %>" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="text-danger" ErrorMessage="<%# Resources.DynamicData.Phone_MessageFormat %>" ControlToValidate="TextBox1" Display="Dynamic" />

