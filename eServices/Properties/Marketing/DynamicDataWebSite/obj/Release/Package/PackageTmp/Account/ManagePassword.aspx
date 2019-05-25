<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManagePassword.aspx.cs" Inherits="DynamicDataWebSite.Account.ManagePassword" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 <div class="login-right">
	<div class="container">
           <div class="container" onload="Scrolldown">
        <h3> <asp:Literal ID="Header" runat="server" /></h3>
        <!--set new password-->
        <div class="form-info fontstyled2">
        <asp:PlaceHolder runat="server" ID="setPassword" Visible="false">
         <h6> <asp:Literal Text="<%$ Resources:Account, Page_ManagePassword_SetPassword_Text %>" runat="server" /></h6>
                   <asp:ValidationSummary runat="server" ShowModelStateErrors="true" CssClass="text-danger" />
                            <asp:TextBox runat="server" ID="password" TextMode="Password" placeholder="<%$ Resources:Account, Page_ManagePassword_Password %>" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="password"
                                CssClass="text-danger" ErrorMessage="<%$ Resources:Account, Page_ManagePassword_Password_RequiredFieldValidator %>"
                                Display="Dynamic" ValidationGroup="SetPassword" />
                            <asp:ModelErrorMessage runat="server" ModelStateKey="NewPassword" AssociatedControlID="password"
                                CssClass="text-danger" SetFocusOnError="true" />
                            <asp:TextBox runat="server" ID="confirmPassword" TextMode="Password" placeholder="<%$ Resources:Account, Page_ManagePassword_ConfirmPassword %>" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="confirmPassword"
                                CssClass="text-danger" Display="Dynamic" ErrorMessage="<%$ Resources:Account, Page_ManagePassword_ConfirmPassword_RequiredFieldValidator %>"
                                ValidationGroup="SetPassword" />
                            <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="confirmPassword"
                                CssClass="text-danger" Display="Dynamic" ErrorMessage="<%$ Resources:Account, Page_ManagePassword_ConfirmPassword_CompareValidator %>"
                                ValidationGroup="SetPassword" />
                            <label class="hvr-sweep-to-right">
                            <asp:Button runat="server"  CssClass="fontstyled3" Text="<%$ Resources:Account, Page_ManagePassword_Submit_SetPassword %>" ValidationGroup="SetPassword" OnClick="SetPassword_Click" />
                            </label>
                  </asp:PlaceHolder>
        </div>
        <!--change current password-->
        <div class="form-info fontstyled2">
        <asp:PlaceHolder runat="server" ID="changePasswordHolder" Visible="false">
                  <%--  <h6><asp:Literal Text="<%$ Resources:Account, Page_ManagePassword_ChangePassword_Title %>" runat="server" /></h6>--%>
                    <asp:ValidationSummary runat="server" ShowModelStateErrors="true" CssClass="text-danger" />
                       <asp:TextBox runat="server" ID="CurrentPassword" CssClass="fontstyled1 black" TextMode="Password"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="CurrentPassword" ID="valCurrentPassword"
                                CssClass="text-danger" 
                                ValidationGroup="ChangePassword" />             
                            <asp:TextBox runat="server" ID="NewPassword" CssClass="fontstyled1 black" TextMode="Password" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="NewPassword" ID="valNewPassword"
                                CssClass="text-danger" 
                                ValidationGroup="ChangePassword" />
                            <asp:TextBox runat="server" ID="ConfirmNewPassword" CssClass="fontstyled1 black" TextMode="Password"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmNewPassword"
                                CssClass="text-danger" Display="Dynamic" ID="valReqConfirmNewPassword"
                                ValidationGroup="ChangePassword" />
                            <asp:CompareValidator runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" ID="valConfirmNewPassword"
                                CssClass="text-danger" Display="Dynamic"
                                ValidationGroup="ChangePassword" />
            <hr />
                            <label class="hvr-sweep-to-right">
                            <asp:Button runat="server"  CssClass="fontstyled3" ID="btnSubmit" ValidationGroup="ChangePassword" OnClick="ChangePassword_Click" />
         </label>

        </asp:PlaceHolder>
           </div>
   </div>
        </div>
 </div>
</asp:Content>
