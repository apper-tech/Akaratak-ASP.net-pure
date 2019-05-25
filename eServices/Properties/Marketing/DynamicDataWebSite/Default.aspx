<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DynamicDataWebSite._Default" %>

<%@ Register TagName="Icons" TagPrefix="ServerControls"
    Src="~/ServerControls/Icons.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="BreadCrumbCrumb" ContentPlaceHolderID="BreadCrumbPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <div class="content">
        <ServerControls:Icons runat="server" />
       <div class="dealers">
<div class="container">
	<h3><%= Resources.RealEstate.Services %></h3>
	<div class="dealer">
		<div class="dealer-grid">
			<div class="col-sm-4 dealer-grid1">
				<div class="dealer-grid-top">
					<span><img src="CustomDesign/images/star.png" style="width:30px;height:30px" /></span>
					<h6><a href="/Feed"><%= Resources.RealEstate.Direct %> </a></h6>
					<div class="clearfix"> </div>
				</div>
				<p><%= Resources.RealEstate.Direct_Desc %></p>
			</div>
			<div class="col-sm-4 dealer-grid1">
				<div class="dealer-grid-top">
					<span><img src="CustomDesign/images/house.png" style="width:30px;height:30px" /></span>
					<h6><a href="/Dynamic/Properties/ListWithIcons"><%= Resources.RealEstate.View_Property %></a></h6>
					<div class="clearfix"> </div>
				</div>
				<p><%= Resources.RealEstate.View_Property_Desc2%> </p>
			</div>
			<div class="col-sm-4 dealer-grid1">
				<div class="dealer-grid-top">
					<span><img src="CustomDesign/images/develop.png" style="width:30px;height:30px" /></span>  
					<h6><a href="/About"><%= Resources.RealEstate.On_Develop %></a></h6>
					<div class="clearfix"> </div>
				</div>
				<p><%= Resources.RealEstate.On_Develop_Desc%> </p>
			</div>
			<div class="clearfix"> </div>
		</div>
	</div>
</div>
</div>
        </div>
</asp:Content>
