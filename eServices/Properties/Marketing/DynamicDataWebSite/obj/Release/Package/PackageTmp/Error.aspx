<%@ Page Title="خطأ" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="DynamicDataWebSite.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <hr />
    <br />
    <div class="error">
        <div class="container">
            <h4 style="margin-bottom: -10px; padding-bottom: 1cm;">
                <img class="errorimg" src="../../CustomDesign/images/error.png" />
                <asp:Literal ID="Literal1" runat="server"
                    Text="<%$Resources:DynamicData, Error_Generic%>" /></h4>
        </div>
    </div>
    <br />
    <hr />
</asp:Content>
