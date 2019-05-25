<%@ Page Title="<%$ Resources:Account, Page_TwoFactorAuthenticationSignIn_Name %>" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TwoFactorAuthenticationSignIn.aspx.cs" Inherits="DynamicDataWebSite.Account.TwoFactorAuthenticationSignIn" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <asp:PlaceHolder runat="server" ID="sendcode">
        <section>
            <h4>
                <asp:Literal Text="<%$ Resources:Account, Page_TwoFactorAuthenticationSignIn_SendVerificationCode_Title %>" runat="server" /></h4>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:Literal Text="<%$ Resources:Account, Page_TwoFactorAuthenticationSignIn_SendVerificationCode_Select %>" runat="server" />:
            <asp:DropDownList runat="server" ID="Providers">
            </asp:DropDownList>
                    <br />
                    <asp:Button Text="<%$ Resources:Account, Page_TwoFactorAuthenticationSignIn_SendVerificationCode_Submit %>" ID="ProviderSubmit" OnClick="ProviderSubmit_Click" CssClass="btn btn-default" runat="server" />
                </div>
            </div>
        </section>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="verifycode" Visible="false">
        <section>
            <h4>
                <asp:Literal Text="<%$ Resources:Account, Page_TwoFactorAuthenticationSignIn_EnterVerificationCode_Title %>" runat="server" /></h4>
            <hr />
            <asp:HiddenField ID="SelectedProvider" runat="server" />
            <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                <p class="text-danger">
                    <asp:Literal runat="server" ID="FailureText" />
                </p>
            </asp:PlaceHolder>
            <div class="form-group">
                <asp:Label Text="<%$ Resources:Account, Page_TwoFactorAuthenticationSignIn_EnterVerificationCode_Code %>" runat="server" AssociatedControlID="Code" CssClass="col-md-2 control-label" />
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="Code" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <div class="checkbox">
                        <asp:Label Text="<%$ Resources:Account, Page_TwoFactorAuthenticationSignIn_EnterVerificationCode_RememberBrowser %>" runat="server" />
                        <asp:CheckBox Text="" ID="RememberBrowser" runat="server" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <asp:Button Text="<%$ Resources:Account, Page_TwoFactorAuthenticationSignIn_EnterVerificationCode_Submit %>" ID="CodeSubmit" OnClick="CodeSubmit_Click" CssClass="btn btn-default" runat="server" />
                </div>
            </div>
        </section>
    </asp:PlaceHolder>
</asp:Content>
