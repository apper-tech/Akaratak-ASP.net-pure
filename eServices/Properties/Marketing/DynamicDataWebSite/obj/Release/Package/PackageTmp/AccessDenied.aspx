<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="DynamicDataWebSite.AccessDenied" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="alert_area">
        <div style="padding-bottom: 20px">
            <img src="CustomDesign/images/NotAllowed.png" />
        </div>
        <div class="allowed_bg">
            <p>محاولة دخول غير مسموحة </p>
        </div>
        <p style="width: 450px; margin: auto; padding-top: 10px">
            حساب المستخدم الذي تستخدمه غير مصرح له بالدخول
        </p>
    </div>
</asp:Content>
