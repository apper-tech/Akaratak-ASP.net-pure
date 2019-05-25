<%@ Control Language="C#"
    CodeBehind="Range.ascx.cs"
    Inherits="DynamicDataWebSite.RangeFilter" %>
<div style="display: inline-block; min-width: 212px">
    <asp:TextBox
        ID="txbFrom"
        runat="server"
        CssClass="DDFilter">
    </asp:TextBox>
    <asp:Literal runat="server" Text="<%$Resources:DynamicData, To%>" />
    <asp:TextBox
        ID="txbTo"
        runat="server"
        CssClass="DDFilter">
    </asp:TextBox>
</div>
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
