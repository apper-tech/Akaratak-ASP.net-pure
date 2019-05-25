<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="DynamicDataWebSite.DynamicData.CustomPages.Users.Details" %>


<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">
    <style>
		.EditLink {
            <%= DynamicDataLibrary.AuthorizationManager.CheckUrlAccessForPrincipal(table.GetActionPath(PageAction.Edit), this.Page.User) 
                            ? "" 
                            : "display: none;" %>
        }
        <%--Not used in this porject--%>
        .DeleteLink {
            <%= DynamicDataLibrary.AuthorizationManager.CheckUrlAccessForPrincipal(table.GetActionPath(PageAction.Insert).Replace("Insert","Delete"), this.Page.User) 
                            ? "" 
                            : "display: none;" %>
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="FormView1" />
        </DataControls>
    </asp:DynamicDataManager>
    <div class="print_button">
        <a onclick="window.open('<%= 
            this.Request.Url.OriginalString.Contains("?") ?
            this.Request.Url.OriginalString + "&Print=True" :
            this.Request.Url.OriginalString + "?Print=True" %>'); return false;" ><asp:Literal runat="server" Text="" /></a>
    </div>
        <div class="about">	
	<div class="about-head">
		<div class="container">
			<h3><%= Resources.RealEstate.Page_User_Details_Name %></h3>
            <div class="about-in">
              <%--  <a href="blog_single.html"><img src="images/at.jpg" alt="image" class="img-responsive ">	</a>			
				--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
                HeaderText="<%$Resources:DynamicData, ValidationSummaryHeaderText%>" CssClass="DDValidator" />
            <asp:DynamicValidator runat="server" ID="DetailsViewValidator" ControlToValidate="FormView1" Display="None" CssClass="DDValidator" />

            <asp:FormView runat="server" ID="FormView1" DataSourceID="DetailsDataSource"
                OnItemDeleting="FormView1_ItemDeleting"
                OnItemDeleted="FormView1_ItemDeleted" RenderOuterTable="false" OnDataBound="FormView1_DataBound">
                <ItemTemplate>
                        <h6 style="font-size:60px"><a href="#"><asp:DynamicControl runat="server" DataField="First_Name" />&nbsp;<asp:DynamicControl runat="server" DataField="Last_Name" /></a></h6>
					   <p style="font-size:30px"><%= Resources.RealEstate.Page_User_Details_Lives_In %>&nbsp;&nbsp;&nbsp;<asp:DynamicControl runat="server" DataField="Address" /></p>
				</div>
		</div>
	</div>
	<%--<div class="about-middle">
		<div class="container">		
			<div class="col-md-8 about-mid">
				<h4><%= Resources.RealEstate.Page_User_Details_Phone %>:&nbsp;&nbsp;&nbsp;<asp:DynamicControl runat="server" DataField="Phone_Num" /></h4>
				<h6 style="font-size:30px"><a href="#"><asp:DynamicControl runat="server" DataField="Has_Office" /></a> </h6>
                 <label class="hvr-sweep-to-right">
                                <asp:DynamicHyperLink Font-Size="50px" ID="DynamicHyperLink_Edit" runat="server" Action="Edit"
                                   Text="<%# this.EditItemCommandText%>" /></label>
			</div>
			<div class="about-mid3">
				<p><%= Resources.RealEstate.Page_User_Details_Email %>:</p>
                <p>&nbsp;&nbsp;&nbsp;<asp:DynamicControl style="color:wheat;" runat="server" DataField="Email"  ID="eml" ClientIDMode="Static"/></p>
				<a href='#' class="hvr-sweep-to-right more-in" onclick="Get_Email()"><%= Resources.RealEstate.Page_User_Details_Email_Send %></a>
			
            </div>
			<div class="clearfix"> </div>
		</div>
	</div>--%>
                    <div class="about-middle">
		<div class="container">		
			<div class="col-md-8 about-mid">
				<h4><%= Resources.RealEstate.Page_User_Details_Phone %>:&nbsp;&nbsp;&nbsp;<asp:DynamicControl runat="server" DataField="Phone_Num" /></h4>
				<h6 style="font-size:30px"><a href="#"><asp:DynamicControl runat="server" DataField="Has_Office" /></a> </h6>
                 <label class="hvr-sweep-to-right">
                                <asp:DynamicHyperLink Font-Size="50px" ID="DynamicHyperLink_Edit" runat="server" Action="Edit"
                                   Text="<%# this.EditItemCommandText%>" /></label>
			</div>
			<div class="col-md-4 about-mid1">
				<p><%= Resources.RealEstate.Page_User_Details_Email %>:</p>
                <h4>&nbsp;&nbsp;&nbsp;<asp:DynamicControl style="color:wheat;" runat="server" DataField="Email"  ID="eml" ClientIDMode="Static"/></h4>
				<a href='#' class="hvr-sweep-to-right more-in" onclick="Get_Email()"><%= Resources.RealEstate.Page_User_Details_Email_Send %></a>
			
			</div>
			<div class="clearfix"> </div>
		</div>
	</div>
</div>
                                
                </ItemTemplate>
                <EmptyDataTemplate>
                    <div class="DDNoItem">
                        <asp:Literal ID="Literal2" runat="server"
                            Text="<%$Resources:DynamicData, ItemDoesNotExist%>" />
                    </div>
                </EmptyDataTemplate>
            </asp:FormView>
            
            
            <ef:EntityDataSource ID="DetailsDataSource" runat="server" EnableDelete="true"
                 OnQueryCreated="DetailsDataSource_QueryCreated" 
                OnDeleting="DetailsDataSource_Deleting" 
                OnDeleted="DetailsDataSource_Deleted"/>

            <asp:QueryExtender TargetControlID="DetailsDataSource" ID="DetailsQueryExtender" runat="server">
                <asp:DynamicRouteExpression />
            </asp:QueryExtender>
            <input id="dummyErrorMessage" type="button" style="display: none" runat="server" />            
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderErrorMessage" runat="server" BackgroundCssClass="modalBackground"
                TargetControlID="dummyErrorMessage"
                PopupControlID="PanelErrorMessage">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="PanelErrorMessage" runat="server" CssClass="modalPanel PanelErrorAndMessages">
                <asp:Label ID="LabelErrorMessage" Text="" runat="server" />
                <table id="Table2" class="DDDetailsTable" cellpadding="6">
                    <tr class="td">
                        <td colspan="2">
                            <asp:LinkButton ID="LinkButtonErrorMessageOk" runat="server" Text="<%$Resources:DynamicData, Ok%>" 
                                OnClick="LinkButtonErrorMessageOK_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            
            <input id="dummyConfirmationMessage" type="button" style="display: none" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderConfirmationMessage" runat="server" BackgroundCssClass="modalBackground"
                TargetControlID="dummyConfirmationMessage"
                PopupControlID="PanelConfirmation">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="PanelConfirmation" runat="server" CssClass="modalPanel">
                <asp:Label ID="LabelConfirmationMessage" Text="" runat="server" />

                <table id="detailsTable" class="DDDetailsTable" cellpadding="6">
                    <tr class="td">
                        <td colspan="2">
                            <asp:LinkButton ID="LinkButtonConfirmationDelete" runat="server" Text="<%$Resources:DynamicData, Delete%>" OnClick="LinkButtonConfirmationDelete_Click" />
                            <asp:LinkButton ID="LinkButtonConfirmationCancel" runat="server" Text="<%$Resources:DynamicData, Cancel%>" CausesValidation="false" OnClick="LinkButtonConfirmationCancel_Click" CommandName="Cancel" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            
            <input id="dummyInformationMessage" type="button" style="display: none" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderInformationMessage" runat="server" BackgroundCssClass="modalBackground"
                TargetControlID="dummyInformationMessage"
                PopupControlID="PanelInformationMessage">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="PanelInformationMessage" runat="server" CssClass="modalPanel">
                <asp:Label ID="LabelInformationMessage" Text="" runat="server" />

                <table id="Table1" class="DDDetailsTable" cellpadding="6">
                    <tr class="td">
                        <td colspan="2">
                            <asp:LinkButton ID="LinkButtonInformationMessageOK" runat="server" Text="<%$Resources:DynamicData, Ok%>" OnClick="LinkButtonInformationMessageOK_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
                <script type="text/javascript">
                    function Get_Email()
                    {
                        alert(document.getElementById("eml").value);
                    }
                </script>
		</asp:Content>


