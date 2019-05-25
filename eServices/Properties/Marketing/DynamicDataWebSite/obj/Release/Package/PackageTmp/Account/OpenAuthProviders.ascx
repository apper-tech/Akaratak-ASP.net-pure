<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenAuthProviders.ascx.cs" Inherits="DynamicDataWebSite.Account.OpenAuthProviders" %>

<div id="socialLoginList">
    <h4>
        <asp:Literal runat="server" Text="<%$ Resources:Account, Control_OpenAuthProviders_Title %>" /></h4>
    <hr />
    <asp:ListView runat="server" ID="providerDetails" ItemType="System.String"
        SelectMethod="GetProviderNames" ViewStateMode="Disabled">
        <ItemTemplate>
            <p>
                <button type="submit" class="btn btn-default" name="provider" value="<%#: Item %>"
                    title="<%#: String.Format(Convert.ToString(HttpContext.GetGlobalResourceObject("Account", "Control_OpenAuthProviders_SubmitTitleFormat")), Item)  %>">
                    <%#: Item %>
                </button>
            </p>
        </ItemTemplate>
        <EmptyDataTemplate>
            <div>
                <p>There are no external authentication services configured. See <a href="http://go.microsoft.com/fwlink/?LinkId=252803">this article</a> for details on setting up this ASP.NET application to support logging in via external services.</p>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>
</div>
