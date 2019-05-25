<%@ Page Title="<%$ Resources:Account, Page_Manage_Name %>" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="DynamicDataWebSite.Account.Manage" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="contact">
        <div class="container">
            <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
                <p class="text-success"><%: SuccessMessage %></p>
            </asp:PlaceHolder>
            <h3><%= Resources.Account.Page_Manage_Name %></h3>
            <div class="contact-top">
                <div class="col-md-6 contact-top1">
                    <h4>
                        <asp:Label runat="server" ID="FirstName"></asp:Label>
                        <asp:Label runat="server" ID="LastName"></asp:Label></h4>
                    <div class="contact-address">
                        <div class="col-md-6 contact-address1">
                            <h5><%= Resources.Account.Page_ManageDetails_Address %></h5>
                            <p><b>
                                <asp:Label runat="server" ID="Address"></asp:Label></b></p>
                        </div>
                        <div class="col-md-6 contact-address1">
                            <h5><%= Resources.Account.Page_Login_Email %> </h5>
                            <p>
                                <asp:Label runat="server" ID="Email"></asp:Label></p>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="contact-address">
                        <div class="col-md-6 contact-address1">
                            <h5><%= Resources.Account.Page_ManageDetails_PhoneNumber %> </h5>
                            <p>
                                <asp:Label runat="server" ID="Phone"></asp:Label></p>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
                <div class="col-md-6 contact-right">
                    <table style="background-color: #F4F4F4; font-size: 25px; border: 1px solid #E5E5E5; color: #27DA93">
                        <tr>
                            <td><%= Resources.Account.Page_Manage_Password %>&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:HyperLink Style="color: #515865; text-decoration: none;" CssClass="hvr-sweep-to-right" Text="<img style='width:40px' src='/CustomDesign/images/edit.png' alt=''/>" Visible="false" ID="ChangePassword" runat="server" />
                                <asp:HyperLink Style="color: #515865; text-decoration: none;" CssClass="hvr-sweep-to-right" NavigateUrl="<%# Resources.Route.ManagePassword %>" Text="Add" Visible="false" ID="CreatePassword" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="LtDt" />&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                <div runat="server" id="LtEditHy"><a href="<%=this.Get_Details_Link(true) %>" style="color: #515865; text-decoration: none;" class="hvr-sweep-to-right">
                                    <img style="width: 40px" src="/CustomDesign/images/edit.png" alt="" /></a> </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="viewPropLt"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" Text="<%#ViewProp %>"></asp:Literal>
                                <div runat="server" id="LtView"><a href="<%=this.Get_Details_Link(false) %>" style="color: #515865; text-decoration: none;" class="hvr-sweep-to-right">
                                    <img style="width: 40px" src="/CustomDesign/images/eye.png" alt="" /></a> </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="rolelbl" />&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                <a href="<%# RolesLink %>" style="color: #515865; text-decoration: none;" class="hvr-sweep-to-right" runat="server" id="rolehy">
                                    <img runat="server" id="rolehylble" style="width: 40px" src="/CustomDesign/images/mng.png" alt="" /></a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="Literal1" />&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                <a href="<%# UsersListLink %>" style="color: #515865; text-decoration: none;" class="hvr-sweep-to-right" runat="server" id="A1">
                                    <img runat="server" id="Img1" style="width: 40px" src="/CustomDesign/images/eye.png" alt="" /></a>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
</asp:Content>
