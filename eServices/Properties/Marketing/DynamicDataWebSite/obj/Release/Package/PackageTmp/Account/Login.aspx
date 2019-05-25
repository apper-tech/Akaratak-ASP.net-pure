<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DynamicDataWebSite.Account.Login" Async="true" %>

<%@ Register TagName="Checkbox" TagPrefix="ServerControls" Src="~/ServerControls/CheckBox.ascx" %>
<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="login-right">
        <div class="container" onload="Scrolldown">
            <h3>
                <%= Resources.Account.Page_Login_Title%></h3>
            <div id="ScrollHere" class="login-top" runat="server">
                <%--
            enable open auth first	
            	<ul class="login-icons">
					<li><a href="#"><i class="facebook"> </i><span>Facebook</span></a></li>
					<li><a href="#" class="twit"><i class="twitter"></i><span>Twitter</span></a></li>
					<li><a href="#" class="go"><i class="google"></i><span>Google +</span></a></li>
					<li><a href="#" class="in"><i class="linkedin"></i><span>Linkedin</span></a></li>
					
				</ul>--%>
                <div class="clearfix"></div>
                <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                    <p class="text-danger">
                        <asp:Literal runat="server" ID="FailureText" />
                    </p>
                </asp:PlaceHolder>
                <div class="form-info">
                    <!-- /Email-->
                    <asp:TextBox runat="server" ID="Email" CssClass="fontstyled1 black" ForeColor="Black" />
                    <asp:RequiredFieldValidator ID="EmailVal" runat="server" ControlToValidate="Email"
                        CssClass="text-danger" />
                    <!--//Email-->
                    <!--/Password-->
                    <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="fontstyled1 black" ForeColor="Black" />
                    <asp:RequiredFieldValidator runat="server" ID="PasswordVal" ControlToValidate="Password" CssClass="text-danger" />
                    <!--//Password-->
                    <!--/CheckRem-->
                    <ServerControls:Checkbox runat="server" ID="rem"></ServerControls:Checkbox>
                    <!--//CheckRem-->
                    <hr />
                    <!--//CheckRem-->
                    <table>
                        <tr>
                            <td>
                                <label class="hvr-sweep-to-right">
                                    <asp:Button runat="server" OnClick="LogIn" CssClass="fontstyled3" ID="loginBtn" />
                                </label>
                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <a class="forget" href="/Account/Forgot"><%= Resources.Account.Page_Login_ForgotPasswordHyperLink %></a></td>
                        </tr>
                    </table>
                </div>
                <div class="create">
                    <h4>
                        <%= Resources.Account.Page_Login_New %></h4>
                    <a class="hvr-sweep-to-right" style="margin:1em 0 0 0" href="/<%= Resources.Route.Account %>/<%= Resources.Account.Register %>">
                        <%= Resources.Account.Page_Register_Title %></a>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <script>
        window.onloadstart = Scrolldown;
        window.onload = Scrolldown;
        window.onpageshow = Scrolldown;
        function Scrolldown() {
            window.scroll(0, 300);
        }
    </script>
</asp:Content>
