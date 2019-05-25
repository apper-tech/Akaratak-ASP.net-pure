<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Feed.aspx.cs" Inherits="DynamicDataWebSite.Feed" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="feedback">
	<div class="container">
		<h3 style="font-family:'Segoe UI'"><%= Resources.RealEstate.Feed_Back_Desc %></h3>
        <div id="scrollhere" runat="server">
		<div class="feedback-top fontstyled2">
				<input class="fontstyled2" type="text" placeholder="<%= Resources.Account.Name %>" required="" maxlength="26"/>
				<input class="fontstyled2" type="text" placeholder="<%= Resources.Account.Page_Register_Email %>" " required="" maxlength="26"/>
				<textarea class="fontstyled2" placeholder="<%= Resources.RealEstate.Opinion %>" requried="" maxlength="50"></textarea>
				 <label class="hvr-sweep-to-right">
	           	<input class="fontstyled2" type="submit" value="<%= Resources.Account.Page_TwoFactorAuthenticationSignIn_SendVerificationCode_Submit %>" onclick="view()" />
	           </label>
            <label id="thanks"></label>
		</div></div>
	</div>
</div>
    <script type="text/javascript">

        function view() {
            document.getElementById("thanks").value = 'شكراً';
        }
    </script>
</asp:Content>
