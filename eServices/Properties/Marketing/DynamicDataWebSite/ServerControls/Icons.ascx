<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Icons.ascx.cs" Inherits="DynamicDataWebSite.ServerControls.Icons" %>

<asp:Repeater ID="Repeater1" runat="server">
    <HeaderTemplate>
        <div class="content-grid">
            <div class="container" style="text-align:center">
                <h3 style="font-family: 'Segoe UI'"><%= Resources.RealEstate.Site_Welcome %></h3>
    </HeaderTemplate>
    <ItemTemplate>
        <div id="here" class="<%= ClassString %>">
            <a href="<%#Eval("Path") %>" class="mask" style="background-color: #fff;">
                <img class="img-responsive2 zoom-img" src="<%#Eval("IconUrl") %>" alt="">
                <span class="four"><%#Eval("Title") %></span>
            </a>
            <div class="most-1" style="position: center">
                <h5><a href="<%#Eval("Path") %>" style="font-family: 'Segoe UI'"><%#Eval("Title") %></a></h5>
                <p><%#Eval("Desc") %></p>
            </div>
        </div>

    </ItemTemplate>
    <FooterTemplate>
        <div class="clearfix"></div>
        </div>
	</div>

    </FooterTemplate>
</asp:Repeater>
