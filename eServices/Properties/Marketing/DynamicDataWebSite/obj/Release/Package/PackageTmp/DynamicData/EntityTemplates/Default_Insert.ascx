<%@ Control Language="C#" CodeBehind="Default_Insert.ascx.cs" Inherits="DynamicDataWebSite.Default_InsertEntityTemplate" %>

<%@ Reference Control="~/DynamicData/EntityTemplates/Default.ascx" %>
<div id="direct" runat="server">
<asp:EntityTemplate runat="server" ID="EntityTemplate1">
    <ItemTemplate>
                <asp:Label runat="server" OnInit="Label_Init" OnPreRender="Label_PreRender" style="padding-bottom:20px" />
                <asp:DynamicControl runat="server" ID="DynamicControl" Mode="Insert" OnInit="DynamicControl_Init" 
                    OnPreRender="DynamicControl_PreRender"/>
             <div class="clearfix"></div>
    </ItemTemplate>
</asp:EntityTemplate>
    </div>
