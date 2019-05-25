<%@ Control Language="C#" CodeBehind="Integer_Edit.ascx.cs" Inherits="DynamicDataWebSite.Integer_EditField" %>
<asp:TextBox ID="TextBox1" runat="server" Text='<%# FieldValueEditString %>' placeholder="<%# Column.Description %>" CssClass="fontstyled2 black" ForeColor="Black"
    OnPreRender="TextBox1_PreRender"></asp:TextBox>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="text-danger" ControlToValidate="TextBox1" Display="Dynamic" />
<asp:CompareValidator runat="server" ID="CompareValidator1" CssClass="text-danger" ControlToValidate="TextBox1" Display="Dynamic"
    Operator="DataTypeCheck" Type="Integer" />
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" CssClass="text-danger" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false"  />
<asp:RangeValidator runat="server" ID="RangeValidator1" CssClass="text-danger" ControlToValidate="TextBox1" Type="Integer"
    Enabled="true" EnableClientScript="true" Display="Dynamic" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="text-danger" ControlToValidate="TextBox1" Display="Dynamic" />