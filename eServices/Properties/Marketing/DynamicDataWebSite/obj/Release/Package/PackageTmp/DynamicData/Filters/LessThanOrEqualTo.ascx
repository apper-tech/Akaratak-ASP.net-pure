<%@ Control Language="C#" 
    CodeBehind="LessThanOrEqualTo.ascx.cs" 
    Inherits="DynamicDataWebSite.LessThanOrEqualToFilter" %>

<asp:TextBox 
    ID="txbTo" 
    runat="server" 
    CssClass="DDFilter">
</asp:TextBox>
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
