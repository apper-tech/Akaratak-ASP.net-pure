<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="RolesManage.aspx.cs" Inherits="DynamicDataWebSite.Account.Role.RolesManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BreadCrumbPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="home-loan">
                    <h3><%= Page.Title %></h3>
                    <div class="loan-point">
                        <%=Resources.Account.Permession %>
                        <asp:DropDownList ID="ddlPermissions" runat="server" AutoPostBack="True"
                            AppendDataBoundItems="true" OnSelectedIndexChanged="ddlPermissions_SelectedIndexChanged" CssClass="in-drop">
                        </asp:DropDownList>
                        <asp:Label ID="lblWarning" runat="server" CssClass="PanelErrorAndMessages"></asp:Label></h4> 
                        <div class="loan-point1">
                            <div class="col-md-6">
                                <h2><%= Resources.Account.User_Revoke %></h2>
                                <asp:CheckBoxList ID="lboxSource" runat="server" TextAlign="Right" CssClass="checkboxlist" Width="280px">
                                </asp:CheckBoxList>
                                <hr />
                                <%=Resources.Account.Count %>
                                <asp:Label ID="lblSourceCount" runat="server" />
                            </div>
                            <div class="col-md-6">
                                <h2><%=Resources.Account.User_Grantee %></h2>
                                <asp:CheckBoxList ID="lboxDestenation" runat="server" TextAlign="Right" CssClass="checkboxlist" Width="280px"></asp:CheckBoxList>
                                <hr />
                                <%=Resources.Account.Count %>
                                <asp:Label ID="lblDestenationCount" runat="server" />
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="bank-bottom1">
                <div class="container">
                    <div class="rolebtn">
                        <ul>
                            <li>
                                <label class="hvr-sweep-to-right">
                                    <asp:Button ID="btnAddSource" runat="server" OnClick="btnAddSource_Click" CausesValidation="False"  CssClass="button2 fontstyled2" /></label></li>
                            <li></li>
                            <li></li>
                            <li></li>
                            <li>
                                <label class="hvr-sweep-to-right">
                                    <asp:Button ID="btnRemoveSource" runat="server" OnClick="btnRemoveSource_Click" CausesValidation="False" CssClass="button2 fontstyled2" /></label></li>
                            <div class="clearfix"></div>
                        </ul>
                    </div>
                </div>
            </div>
            <p>
                <br />
                <br />
                <br />
                <br />
            </p>
            <hr />
            <div class="loan1">
                <label class="hvr-sweep-to-right">
                    <asp:Button ID="btnUpdateUsersPermissions" runat="server" OnClick="btnUpdateUsersPermissions_Click"
                         CssClass="button2 fontstyled4" />
                </label>
                <asp:Label ID="lblResult" runat="server" CssClass="PanelErrorAndMessages"></asp:Label><br />

            </div>
            <div class="clearfix"></div>
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpdateUsersPermissions" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
