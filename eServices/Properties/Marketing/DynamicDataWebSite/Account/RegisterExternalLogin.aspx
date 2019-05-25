<%@ Page Title="<%$ Resources:Account, Page_RegisterExternalLogin_Name %>" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterExternalLogin.aspx.cs" Inherits="DynamicDataWebSite.Account.RegisterExternalLogin" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
<h3><%: HttpUtility.HtmlDecode(String.Format(Convert.ToString(HttpContext.GetGlobalResourceObject("Account", "Page_RegisterExternalLogin_Title")), ProviderName)) %></h3>

    <asp:PlaceHolder runat="server">
        <div class="form-horizontal">
            <h4>
                <asp:Literal Text="<%$ Resources:Account, Page_RegisterExternalLogin_AssociationForm %>" runat="server" /></h4>
            <hr />
            <asp:ValidationSummary runat="server" ShowModelStateErrors="true" CssClass="text-danger" />
            <p class="text-info">
                <%= HttpUtility.HtmlDecode(String.Format(Convert.ToString(HttpContext.GetGlobalResourceObject("Account", "Page_RegisterExternalLogin_Description")), ProviderName)) %>
                
                
                
            </p>

            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="email" CssClass="col-md-2 control-label"><asp:Literal Text="<%$ Resources:Account, Page_RegisterExternalLogin_Email %>" runat="server" /></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="email" CssClass="form-control" TextMode="Email" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="email"
                        Display="Dynamic" CssClass="text-danger" ErrorMessage="Email is required" />
                    <asp:ModelErrorMessage runat="server" ModelStateKey="email" CssClass="text-error" />
                </div>
            </div>

            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <asp:Button runat="server" Text="<%$ Resources:Account, Page_RegisterExternalLogin_Submit %>" CssClass="btn btn-default" OnClick="LogIn_Click" />
                </div>
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>
