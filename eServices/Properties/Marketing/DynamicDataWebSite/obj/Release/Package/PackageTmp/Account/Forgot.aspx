<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Forgot.aspx.cs" Inherits="DynamicDataWebSite.Account.ForgotPassword" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="login-right">
        <div class="container" onload="Scrolldown">
            <h3>
              <%= Page.Title%></h3>
            <div id="ScrollHere" class="login-top" runat="server">
                  <asp:PlaceHolder ID="loginForm" runat="server">
                <div class="clearfix"></div>
                <div class="form-info fontstyled2">
                    <asp:TextBox runat="server" ID="Email" CssClass="fontstyled1 black" ForeColor="Black" />
                   <asp:RequiredFieldValidator runat="server" ControlToValidate="Email" ID="Email_Validator"
                        CssClass="text-danger" />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <label class="hvr-sweep-to-right">
                        <asp:Button ID="submit" runat="server" OnClick="Forgot" CssClass="fontstyled3" />
                    </label>
                    <div class="clearfix"></div><br />
            </div>
                      </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" ID="DisplayEmail" Visible="false">
                	<div class="about-in">
                    <asp:Literal runat="server" ID="Display_Email_Text" />
                </div>
                        <br /><br />
            </asp:PlaceHolder>
        </div>
            <div class="clearfix"></div>
    </div>
        </div>
</asp:Content>
