<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Confirm.aspx.cs" Inherits="DynamicDataWebSite.Account.Confirm" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
         <div class="about">	
	<div class="about-head">
		<div class="container">
			 <h3><asp:Literal ID="head" runat="server" /></h3>
	
            
        <asp:PlaceHolder runat="server" ID="successPanel" ViewStateMode="Disabled" Visible="true">
				<div class="about-in">	
                    <asp:Literal ID="pre" runat="server" />	
					<h6 ><asp:HyperLink ID="login" runat="server" NavigateUrl="~/Account/Login"> <asp:Literal runat="server" Text="<%$ Resources:Account, Page_Confirm_Here %>" /></asp:HyperLink> </h6>
					<p <asp:Literal ID="suf" runat="server" /></p>
				</div>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="errorPanel" ViewStateMode="Disabled" Visible="false">
            <div class="about-in">	
            <p class="text-danger">
                <asp:Literal runat="server" ID="error" />
            </p>
            </div>
        </asp:PlaceHolder>
  	</div>
	</div>
</asp:Content>
