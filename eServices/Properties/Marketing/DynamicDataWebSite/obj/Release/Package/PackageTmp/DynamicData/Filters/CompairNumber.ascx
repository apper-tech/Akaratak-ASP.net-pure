<%@ Control Language="C#" 
    CodeBehind="CompairNumber.ascx.cs" 
    Inherits="DynamicDataWebSite.CompairNumberFilter" %>
<asp:TextBox 
    ID="txbNumber" 
    runat="server" 
    CssClass="DDFilter">
</asp:TextBox>

<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txbNumber"></ajaxToolkit:FilteredTextBoxExtender>

<asp:Button 
    ID="btnRangeButton" 
    CssClass="DDFilter"
    runat="server" 
    Text="<%$Resources:DynamicData, Filter%>" 
    OnClick="btnRangeButton_Click" />
<asp:Button 
    ID="btnClear" 
    CssClass="DDFilter"
    runat="server" 
    Text="<%$Resources:DynamicData, Clear %>" 
    OnClick="btnRangeButton_Click" />
