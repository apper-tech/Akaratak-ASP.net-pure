<%@ Control Language="C#" CodeBehind="Default_Edit.ascx.cs" Inherits="DynamicDataWebSite.Default_EditEntityTemplate" %>

<%@ Reference Control="~/DynamicData/EntityTemplates/Default.ascx" %>
<div id="direct" runat="server">
<asp:EntityTemplate runat="server" ID="EntityTemplate1">
    <ItemTemplate>
                <asp:Label runat="server" OnInit="Label_Init" OnPreRender="Label_PreRender" />
                <asp:DynamicControl runat="server" ID="DynamicControl" Mode="Edit" OnInit="DynamicControl_Init" 
                    OnPreRender="DynamicControl_PreRender"/>
        <div class="clearfix"></div>
    </ItemTemplate>
</asp:EntityTemplate>
</div>
