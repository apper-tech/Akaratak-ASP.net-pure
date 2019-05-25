<%@ Control Language="C#" CodeBehind="User.ascx.cs" Inherits="DynamicDataWebSite.UserField" %>
<asp:PlaceHolder runat="server" ID="userPH">
    <table><tr><td><h4><%= Resources.Account.Contact_info %></h4></td></tr>
        <tr><td><b><%= Resources.Account.Name %>:</b></td><td><asp:Label runat="server" ID="name"></asp:Label></td></tr>
        <tr><td><b><%= Resources.Account.Phone_Number %>:</b></td><td><asp:Label runat="server" ID="phone"></asp:Label></td></tr>
    </table>
</asp:PlaceHolder>
