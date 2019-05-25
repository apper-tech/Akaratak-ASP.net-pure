<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BreadCrumb.ascx.cs" Inherits="DynamicDataWebSite.ServerControls.BreadCrumb" %>
<%--<link rel="stylesheet" href="/CustomDesign/css/bootstrap.css" />
<link rel="stylesheet" href="/CustomDesign/css/asBreadcrumbs.css" />
<script src="/CustomDesign/js/jquery.js"></script>
<script src="/CustomDesign/js/bootstrap.min.js"></script>
<script src="/CustomDesign/js/jquery-asBreadcrumbs.js"></script>--%>
<style>
    /* The responsive part */

.breadcrumb > li {
    /* With less: .text-overflow(); */
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;    
    color:#27DA93;
    font-size: 25px!important;
}

.breadcrumb > *:nth-child(n+1) {
  display:none;
}
.invisible{display:none;}
/* === For phones =================================== */
@media (max-width: 767px) {
    .breadcrumb  > *:nth-last-child(-n+2) {
        display:inline;
    } 
    .breadcrumb  > * {
        max-width: 60px;
    }
    .breadcrumb > li {
      font-size: 14px!important;
    }
}

/* === For tablets ================================== */
@media (min-width: 768px) and (max-width:991px) {
    .breadcrumb  > *:nth-last-child(-n+4) {
        display:inline;
    } 
    .breadcrumb  > * {
        max-width: 100px;
    }
}

/* === For desktops ================================== */
@media (min-width: 992px) {
    .breadcrumb  > *:nth-last-child(-n+6) {
        display:inline;
    } 
    .breadcrumb  > * {
        max-width: 170px;
    }
}    
</style>
<div id="crump">
    <asp:Repeater runat="server" ID="rep">
        <HeaderTemplate>
           <%-- <ol class="breadcrumb" itemscope itemtype="http://schema.org/BreadcrumbList">--%>
                <ul id="bc1" class="breadcrumb"><span id="ellipses" style="display: inline;"><asp:HyperLink runat="server" NavigateUrl='<%# "~/"+Resources.Route.List %>' ><i class="fa fa-arrow-left" aria-hidden="true"></i></asp:HyperLink></span>
        </HeaderTemplate>
        <ItemTemplate>
            <%if (isActive())
                { %>
          <%--  <li class="active"
                itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem" style="<%= Style %>">
                <span itemprop="name">
                    <asp:Label runat="server" Text='<%# (Container.DataItem as DynamicDataWebSite.Classes.BreadItem).Title %>' ToolTip='<%# (Container.DataItem as DynamicDataWebSite.Classes.BreadItem).Description %>'
                        itemscope itemtype="http://schema.org/Thing" itemprop="item" href='<%# "https://akaratak.com"+(Container.DataItem as DynamicDataWebSite.Classes.BreadItem).Url%>'></asp:Label></span>
                <meta itemprop="position" content="<%= ItemCount %>" />
            </li>--%>
              <li><span tabindex="0"><asp:Label runat="server" Text='<%# (Container.DataItem as DynamicDataWebSite.Classes.BreadItem).Title %>' ToolTip='<%# (Container.DataItem as DynamicDataWebSite.Classes.BreadItem).Description %>'></asp:Label></span></li>
            <%} %>
            <%else
                { %>
            <%--<li
                itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem">
                <asp:HyperLink runat="server" NavigateUrl='<%# (Container.DataItem as DynamicDataWebSite.Classes.BreadItem).Url%>' ToolTip='<%# (Container.DataItem as DynamicDataWebSite.Classes.BreadItem).Description %>'
                    itemscope itemtype="http://schema.org/Thing" itemprop="item" href='<%# "https://akaratak.com"+(Container.DataItem as DynamicDataWebSite.Classes.BreadItem).Url%>'>
                      <span itemprop="name">  <asp:Literal runat="server"  Text='<%#  (Container.DataItem as DynamicDataWebSite.Classes.BreadItem).Title %>'></asp:Literal></span>
                </asp:HyperLink>
                <meta itemprop="position" content="<%= ItemCount %>" />
            </li>--%>
            <li> <asp:HyperLink runat="server" NavigateUrl='<%# "~/"+Resources.Route.List %>' ToolTip='<%# (Container.DataItem as DynamicDataWebSite.Classes.BreadItem).Description %>'><asp:Literal runat="server"  Text='<%#  (Container.DataItem as DynamicDataWebSite.Classes.BreadItem).Title %>'></asp:Literal></asp:HyperLink> <span class="divider"> <span class="accesshide "> </span>
            </li>
            <%} %>
        </ItemTemplate>
        <FooterTemplate>
            <%--</ol>--%>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</div>
<script>
    //jQuery(function ($) {

    //    $('.breadcrumb').asBreadcrumbs({
    //        toggleIconClass: 'fa fa-home'
    //    });

    //});
    $(document).ready(function () {
        toggle_ellipses();
        /***** Responsive breadcrumbs *****/
        $(window).resize(function () {
            toggle_ellipses();
        });
        /***** Responsive breadcrumbs ******/
    });

    function toggle_ellipses() {
        var ellipses1 = $("#ellipses");
        var howlong = $("#bc1 li:hidden").length;
        if ($("#bc1 li:hidden").length > 1) {
            ellipses1.show();
            console.log("length: " + howlong + " => show")
        } else {
            ellipses1.hide();
            console.log("length: " + howlong + " => hide")
        }
    }
</script>
