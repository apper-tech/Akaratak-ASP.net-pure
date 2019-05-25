<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pushmenu.ascx.cs" Inherits="DynamicDataWebSite.ServerControls.SitemapPushmenu" %>
<%@ Register TagName="Item" TagPrefix="Pushmenu" Src="~/ServerControls/Pushmenu/PushMenu_Item.ascx" %>
<div id="menu" class="multilevelpushmenu_menu">
    <nav>
        <h2><i class="fa fa-times"></i><%=Resources.DynamicData.Sitemap %></h2>
        <asp:Repeater runat="server" ID="rep">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <Pushmenu:Item runat="server" Title='<%# CheckMap(Eval("title").ToString()) %>' Desc='<%#CheckMap( Eval("description").ToString()) %>' Url='<%# CheckMap( Eval("Url").ToString() )%>' />
                    <asp:Repeater ID="nestlvl1" DataSource='<%# GetDataSource(Container.DataItem as SiteMapNode,Container.FindControl("nestlvl1") as Repeater) %>' runat="server">
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <Pushmenu:Item runat="server" Title='<%# CheckMap(Eval("title").ToString()) %>' Desc='<%#CheckMap( Eval("description").ToString()) %>' Url='<%# CheckMap( Eval("Url").ToString() )%>' />
                                <asp:Repeater ID="nestlvl2" runat="server" DataSource='<%# GetDataSource(Container.DataItem as SiteMapNode,Container.FindControl("nestlvl2") as Repeater) %>'>
                                    <HeaderTemplate>
                                        <ul>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <Pushmenu:Item runat="server" Title='<%# CheckMap(Eval("title").ToString()) %>' Desc='<%#CheckMap( Eval("description").ToString()) %>' Url='<%# CheckMap( Eval("Url").ToString() )%>' />
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate></ul></FooterTemplate>
                                </asp:Repeater>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>

                        </FooterTemplate>
                    </asp:Repeater>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>

            </FooterTemplate>
        </asp:Repeater>
    </nav>
</div>
