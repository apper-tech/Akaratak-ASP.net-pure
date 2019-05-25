<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PushMenu_Item.ascx.cs" Inherits="DynamicDataWebSite.ServerControls.PushMenu_Item" %>
<style>
.pushmenu-item-link{
    text-decoration-line: none;
    }
</style>
<asp:HyperLink runat="server" ID="item" CssClass="pushmenu-item-link">
    <i class="<%# Desc %>"></i>
    <asp:Literal runat="server" Text='<%# Title %>'></asp:Literal></asp:HyperLink>
<script type="text/javascript">
    jQuery(document).ready(function ($) {
        setTimeout(function () {
            $('#<%= item.ClientID%>').click(function () {
                    location.href = "<%= Url%>";
                });
            }, 200);
    });
</script>
