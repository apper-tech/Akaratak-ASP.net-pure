<%@ Page Title="<%$ Resources:Account, Page_ResetPasswordConfirmation_Name %>" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPasswordConfirmation.aspx.cs" Inherits="DynamicDataWebSite.Account.ResetPasswordConfirmation" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <div class="about">	
	<div class="about-head">
		<div class="container">
			 <h3><%: Title %></h3>
       <div class="about-in">	
            <asp:Literal Text="<%$ Resources:Account, Page_ResetPasswordConfirmation_Message_Prefix %>" runat="server" /> <asp:HyperLink ID="login" runat="server" NavigateUrl="~/Account/Login"><asp:Literal Text="<%$ Resources:Account, Page_ResetPasswordConfirmation_Message_ClickHere %>" runat="server" /></asp:HyperLink>
           <h6>
            <asp:Literal Text="<%$ Resources:Account, Page_ResetPasswordConfirmation_Message_Suffix %>" runat="server" /></h6>
        </p>
    </div>
            </div></div></div>
</asp:Content>
