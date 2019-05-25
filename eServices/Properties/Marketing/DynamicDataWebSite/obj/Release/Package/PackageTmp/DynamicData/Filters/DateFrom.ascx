<%@ Control Language="C#" 
    CodeBehind="DateFrom.ascx.cs" 
    Inherits="DynamicDataWebSite.DateFromFilter" %>

<asp:Label ID="Label1" runat="server" Text='<%= Column.DisplayName %>' AssociatedControlID="txbDateFrom" />&nbsp;
<asp:TextBox 
    ID="txbDateFrom" 
    runat="server" 
    CssClass="DDFilter">
</asp:TextBox>
<ajaxToolkit:CalendarExtender 
    ID="txbDateFrom_CalendarExtender" 
    runat="server" 
    CssClass="yui"
    Enabled="True" 
    TargetControlID="txbDateFrom">
</ajaxToolkit:CalendarExtender>

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
