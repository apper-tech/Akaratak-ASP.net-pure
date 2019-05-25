<%@ Control Language="C#" CodeBehind="GridViewPager.ascx.cs" Inherits="DynamicDataWebSite.GridViewPager" 
    EnableViewState="true"%>

<div class="DDPager">
    <span class="DDFloatLeft">
        <asp:ImageButton AlternateText="First page" ToolTip="<%$ Resources:DynamicData, FirstPage %>" ID="ImageButtonFirst" runat="server" ImageUrl="images/PgLast.gif" Width="8" Height="9" CommandName="Page" CommandArgument="First" 
            OnClick="ImageButtonFirst_Click"/>
        &nbsp;
        <asp:ImageButton AlternateText="Previous page" ToolTip="<%$ Resources:DynamicData, PreviousPage %>" ID="ImageButtonPrev" runat="server" ImageUrl="images/PgNext.gif" Width="5" Height="9" CommandName="Page" CommandArgument="Prev" 
            OnClick="ImageButtonPrev_Click"/>
        &nbsp;
        <asp:Label ID="LabelPage" runat="server" Text="<%$ Resources:DynamicData, PageText %>" AssociatedControlID="TextBoxPage" />
        <asp:TextBox ID="TextBoxPage" runat="server" Columns="5" AutoPostBack="true" ontextchanged="TextBoxPage_TextChanged" Width="20px" CssClass="DDControl" />
        
        <asp:Label ID="LabelOf" runat="server" Text="<%$ Resources:DynamicData, Of %>" AssociatedControlID="TextBoxPage" />
        
        <asp:Label ID="LabelNumberOfPages" runat="server" />
        &nbsp;
        <asp:ImageButton AlternateText="Next page" ToolTip="<%$ Resources:DynamicData, NextPage %>" ID="ImageButtonNext" runat="server" ImageUrl="images/PgPrev.gif" Width="5" Height="9" CommandName="Page" CommandArgument="Next" 
            OnClick="ImageButtonNext_Click"/>
        &nbsp;
        <asp:ImageButton AlternateText="Last page" ToolTip="<%$ Resources:DynamicData, LastPage %>" ID="ImageButtonLast" runat="server" ImageUrl="images/PgFirst.gif" Width="8" Height="9" CommandName="Page" CommandArgument="Last" 
            OnClick="ImageButtonLast_Click"/>
    </span>
    <%-- This is commented because it does not work well with first, previous, next and last buttons when 
        ViewState is not enabled on the containted GridView. Actually, ViewStat is set to false to fix a bug related to 
        deleting items when sorting is enabled. --%>
    <%--<span class="DDFloatRight">
        <asp:Label ID="LabelRows" runat="server" Text="<%$ Resources:DynamicData, ResultPerPage %>" AssociatedControlID="DropDownListPageSize" />:
        <asp:DropDownList ID="DropDownListPageSize" runat="server" AutoPostBack="true" CssClass="DDControl" OnSelectedIndexChanged="DropDownListPageSize_SelectedIndexChanged">
            <asp:ListItem Value="5" />
            <asp:ListItem Value="10" />
            <asp:ListItem Value="15" />
            <asp:ListItem Value="20" />
        </asp:DropDownList>
    </span>--%>
</div>

