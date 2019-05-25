<%@ Page Title="<%$ Resources:Account, Page_Login_Name %>" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DynamicDataWebSite.Account.Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <h2><%: Title %>.</h2>

    <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                <div class="form-horizontal">

                    <h4>
                        <asp:Literal runat="server" Text="<%$ Resources:Account, Page_Login_Title %>" /></h4>
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label"><asp:Literal runat="server" Text="<%$ Resources:Account, Page_Login_Email %>"/></asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" ErrorMessage="<%$ Resources:Account, Page_Login_Email_RequiredFieldValidator %>" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label"><asp:Literal runat="server" Text="<%$ Resources:Account, Page_Login_Password %>"/></asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="<%$ Resources:Account, Page_Login_Password_RequiredFieldValidator %>" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <div class="checkbox">
                                <asp:CheckBox runat="server" ID="RememberMe" />
                                <asp:Label runat="server" AssociatedControlID="RememberMe"><asp:Literal runat="server" Text="<%$ Resources:Account, Page_Login_RememberMe %>"/></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="LogIn" Text="<%$ Resources:Account, Page_Login_Submit %>" CssClass="btn btn-default" />
                        </div>
                    </div>
                </div>
                <p>
                    <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled"><asp:Literal runat="server" Text="<%$ Resources:Account, Page_Login_RegisterHyperLink %>"/></asp:HyperLink>
                </p>
                <p>
                    <%-- Enable this once you have account confirmation enabled for password reset functionality--%>
                 <%--   <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink" ViewStateMode="Disabled"><asp:Literal runat="server" Text="<%$ Resources:Account, Page_Login_ForgotPasswordHyperLink %>"/></asp:HyperLink>--%>

                </p>
            </section>
        </div>

        <%-- Hide Open Auth Providers untile configuring the porviders at Startup.Auth.cs! --%>
        <div class="Hidden">
            <div class="col-md-4">
                <section id="socialLoginForm">
                    <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
                </section>
            </div>
        </div>
    </div>
</asp:Content>
