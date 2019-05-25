<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="DynamicDataWebSite.Account.ResetPassword" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
     <div class="login-right">
        <div class="container" onload="Scrolldown">
            <h3>
         <asp:Literal runat="server" ID="head"></asp:Literal></h3>
               <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
            <div id="ScrollHere" class="login-top" runat="server">
                
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
                  <asp:PlaceHolder ID="loginForm" runat="server">
                <div class="clearfix"></div>
                <div class="form-info fontstyled2">
                <asp:TextBox runat="server" ID="Email" CssClass="fontstyled1 black" />
                <asp:RequiredFieldValidator ID="Email_Validator" runat="server" ControlToValidate="Email"
                    CssClass="text-danger"/>
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="fontstyled1 black" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" ID="Password_Validator"
                    CssClass="text-danger" />
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="fontstyled1 black" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ID="ConfirmPassword_Validator"/>
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="<%$ Resources:Account, Page_ResetPassword_ConfirmPassword_CompareValidator %>" />
                    <asp:PlaceHolder runat="server" ID="PlaceHolder1" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <label class="hvr-sweep-to-right">
                    <asp:Button ID="reset" runat="server" OnClick="Reset_Click"  CssClass="fontstyled3" />
  
                          </label>
                    <div class="clearfix"></div><br />
            </div>
                      </asp:PlaceHolder>
        </div>
            <div class="clearfix"></div>
    </div>
        </div>
</asp:Content>
