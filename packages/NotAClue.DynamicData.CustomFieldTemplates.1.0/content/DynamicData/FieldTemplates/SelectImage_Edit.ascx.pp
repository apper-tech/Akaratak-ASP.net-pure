<%@ Control Language="C#" CodeBehind="SelectImage_Edit.ascx.cs" Inherits="$rootnamespace$.SelectImage_EditField" %>

<asp:RadioButtonList ID="RadioButtonList1" runat="server" 
	RepeatDirection="Horizontal"
	RepeatLayout="Flow" TextAlign="Left">
</asp:RadioButtonList>

<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="RadioButtonList1" Display="Dynamic" Enabled="false" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" ControlToValidate="RadioButtonList1" Display="Dynamic" />
