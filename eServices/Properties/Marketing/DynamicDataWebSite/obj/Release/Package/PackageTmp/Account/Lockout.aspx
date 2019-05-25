<%@ Page Title="<%$ Resources:Account, Page_Lockout_Name %>" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Lockout.aspx.cs" Inherits="DynamicDataWebSite.Account.Lockout" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup>
        <h2><%: Title %>.</h2>
        <h2 class="text-danger">
            <asp:Literal Text="<%$ Resources:Account, Page_Lockout_Message %>" runat="server" /></h2>
    </hgroup>
</asp:Content>
