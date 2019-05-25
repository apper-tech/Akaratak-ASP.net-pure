<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="Report.aspx.cs" Inherits="DynamicDataWebSite.Report" %>

<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="asp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="GridView1" />
        </DataControls>
    </asp:DynamicDataManager>

    <h2 class="DDSubHeader"><%= table.DisplayName%></h2>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="DD">
                <asp:QueryableFilterRepeater runat="server" ID="FilterRepeater">
                    <ItemTemplate>
                        <div class="DDFilterContainer">
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("DisplayName") + ":" %>' OnPreRender="Label_PreRender" />
                            <asp:DynamicFilter runat="server" ID="DynamicFilter" />
                        </div>
                    </ItemTemplate>
                </asp:QueryableFilterRepeater>
            </div>
            <div style="clear: both;"></div>
            <h2 class="DDSubSubHeader">التقرير</h2>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server"
                WaitMessageFont-Names="Tahoma" WaitMessageFont-Size="14pt"
                CssClass="Report" Width="936px" PageCountMode="Actual">
            </rsweb:ReportViewer>

            <%-- This is dummy to be able to use the Dynamic Filters --%>
            <asp:GridView ID="GridView1" runat="server" DataSourceID="GridDataSource"
                EnableViewState="false" Visible="false">
            </asp:GridView>

            <asp:EntityDataSource ID="GridDataSource" runat="server" EnableDelete="true"
                OnQueryCreated="GridDataSource_QueryCreated" />
            <asp:QueryExtender TargetControlID="GridDataSource" ID="GridQueryExtender" runat="server">
                <asp:DynamicFilterExpression ControlID="FilterRepeater" />
            </asp:QueryExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
