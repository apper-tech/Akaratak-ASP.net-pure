<%@ Control Language="C#" CodeBehind="Date_Edit.ascx.cs" Inherits="$rootnamespace$.Date_EditField" %>

<asp:TextBox 
    ID="TextBox1" 
    runat="server" 
    CssClass="DDTextBox" 
    Text="<%# FieldValueEditString %>" 
    Columns="20">
</asp:TextBox>
<ajaxToolkit:CalendarExtender 
    ID="TextBox1_CalendarExtender" 
    runat="server" 
    Enabled="True"
    TargetControlID="TextBox1">
</ajaxToolkit:CalendarExtender>

<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" Enabled="false" />
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" Enabled="false" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" />
<asp:CustomValidator runat="server" ID="DateValidator" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" EnableClientScript="false" Enabled="false" OnServerValidate="DateValidator_ServerValidate" />

