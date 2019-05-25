<%@ Page Title="<%$ Resources:Account, Page_Register_Name %>" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DynamicDataWebSite.Account.Register" %>
<%@ Register TagName="Checkbox" TagPrefix="ServerControls" Src="~/ServerControls/CheckBox.ascx" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="login-right">
        <div class="container">
            <h3>
                <%= Resources.Account.Page_Register_Name %></h3>
            <div class="login-top" id="ScrollHere" runat="server">

                <%--			<ul class="login-icons">
					<li><a href="#"><i class="facebook"> </i><span>Facebook</span></a></li>
					<li><a href="#" class="twit"><i class="twitter"></i><span>Twitter</span></a></li>
					<li><a href="#" class="go"><i class="google"></i><span>Google +</span></a></li>
					<li><a href="#" class="in"><i class="linkedin"></i><span>Linkedin</span></a></li>
					<div class="clearfix"> </div>
				</ul>--%>
                <!--/Err-->
                    <asp:Label AssociatedControlID="ErrorMessage" runat="server" CssClass="text-danger">
                        <asp:Literal runat="server" ID="ErrorMessage" /></asp:Label><!--//Err--><div class="form-info fontstyled1">
                            <!--/Email-->
                            <asp:TextBox runat="server" ID="Email" CssClass="fontstyled1 black" ForeColor="Black" MaxLength="40" />
                            <asp:RegularExpressionValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" ID="EmailRegVal" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator><br />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" ID="EmailVal" />

                            <!--//Email-->
                            <!--/Password-->
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="fontstyled1 black" ForeColor="Black" MaxLength="26" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                                CssClass="text-danger" ID="PasswordVal" />
                            <!--//Password-->
                            <!--/Confirm-->
                            <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="fontstyled1 black" ForeColor="Black" MaxLength="26" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword" ID="ConfirmPasswordVal"
                                CssClass="text-danger" Display="Dynamic"  />
                            <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" ID="ConfirmPasswordCompareVal"
                                CssClass="text-danger" Display="Dynamic" />
                            <!--//Confirm-->
                            <!--/Agree-->
                           <ServerControls:Checkbox runat="server" ID="Agree" Required="true"></ServerControls:Checkbox>
                            <!--//Agree-->
                            <hr />
                            <!--/Submit-->
                            <label class="hvr-sweep-to-right">
                                <asp:Button runat="server" ID="RegBtn" OnClick="CreateUser_Click" CssClass="fontstyled2" />
                            </label>
                            <!--//Submit-->
                            <br />
                            <p><%= Resources.Account.Page_Register_LoginLink %> <a href="/<%= Resources.Route.Account %>/<%= Resources.Account.Login %>"><%=Resources.Account.Login%></a></p>
                        </div>
                </div>
        </div>
    </div>
</asp:Content>
