<%@ Control Language="C#" 
    CodeBehind="Where.ascx.cs" 
    Inherits="DynamicDataWebSite.WhereFilter" %>

<asp:TextBox 
    ID="TextBox1" 
    runat="server" 
    CssClass="DDFilter">
</asp:TextBox>
<asp:Button 
    ID="Button1" 
    CssClass="DDFilter"
    runat="server" 
    Text="<%$Resources:DynamicData, Filter%>" onclick="Button1_Click" />
<asp:Button 
    ID="btnClear" 
    CssClass="DDFilter"
    runat="server" 
    Text="<%$Resources:DynamicData, Clear %>" 
    OnClick="btnRangeButton_Click" />
