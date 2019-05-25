<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="DynamicDataWebSite.DynamicData.CustomPages.Properties.Details" %>

<asp:Content ID="hd" ContentPlaceHolderID="head" runat="server">
    <meta property="og:title" content="<%= Page.Title %>" />
    <meta property="og:url" content="<%=  HttpContext.Current.Request.Url.AbsoluteUri %>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <!--//DataManager-->
    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="FormView1" />
        </DataControls>
    </asp:DynamicDataManager>
    <!--Print-->
    <div class="print_button">
        <a onclick="window.open('<%= 
            this.Request.Url.OriginalString.Contains("?") ?
            this.Request.Url.OriginalString + "&Print=True" :
            this.Request.Url.OriginalString + "?Print=True" %>'); return false;">
            <asp:Literal runat="server" Visible="false" Text="<%$Resources:DynamicData, Print%>" /></a>
    </div>
    <div class="privacy">
        <div class="container">
            <h3 style="margin-bottom: -10px; padding-bottom: 1cm;"><%= Resources.RealEstate.Page_Details_Title%></h3>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
                HeaderText="<%$Resources:DynamicData, ValidationSummaryHeaderText%>" CssClass="DDValidator" />
            <asp:DynamicValidator runat="server" ID="DetailsViewValidator" ControlToValidate="FormView1" Display="None" CssClass="DDValidator" />

            <asp:FormView runat="server" ID="FormView1" DataSourceID="DetailsDataSource"
                OnItemDeleting="FormView1_ItemDeleting"
                OnItemDeleted="FormView1_ItemDeleted" RenderOuterTable="false" OnDataBound="FormView1_DataBound">
                <ItemTemplate>
                    <div class="container">
                        <div class="buy-single-single">
                            <div class="col-md-9 single-box">
                                <div class=" buying-top">
                                    <asp:DynamicControl runat="server" DataField="Property_Photo" Mode="ReadOnly" />
                                </div>
                                <div class="buy-sin-single" id="direct">
                                    <div class="col-sm-7 buy-sin">
                                        <h4><%= Resources.RealEstate.Column_Other %></h4>
                                        <p>
                                            <span itemprop="description">
                                                <asp:DynamicControl runat="server" DataField="Other_Details" Mode="ReadOnly" />
                                            </span>
                                        </p>
                                        <p>
                                            <hr />
                                        </p>
                                        <h4><%= Resources.RealEstate.Column_Address %></h4>
                                        <p style="font-size: 20px">
                                            <asp:DynamicControl runat="server" DataField="Country" Mode="ReadOnly" />
                                            &nbsp;,&nbsp;<asp:DynamicControl runat="server" DataField="City" Mode="ReadOnly" />
                                            <br />
                                            <asp:DynamicControl runat="server" DataField="Address" Mode="ReadOnly" />
                                        </p>
                                        <p>
                                            <b style="font-size: 15px; color: #333"><%= Resources.RealEstate.Column_Zip %> :</b> &nbsp;
                                            <asp:DynamicControl runat="server" DataField="Zip_Code" Mode="ReadOnly" />
                                        </p>
                                        <p>
                                            <asp:DynamicControl runat="server" DataField="Property_Id_ext" Mode="ReadOnly" />
                                        </p>
                                           <hr />
                                        <div class="right-side">

                                            <asp:DynamicControl runat="server" DataField="User" Mode="ReadOnly" />
                                            <asp:DynamicControl runat="server" DataField="Url_ext" Mode="ReadOnly" />

                                        </div>
                                    </div>
                                    <div class="col-sm-5 middle-side immediate">
                                        <h4><%= Resources.RealEstate.Column_Property_Type %> :
                                            <asp:DynamicControl runat="server" DataField="Property_Type" Mode="ReadOnly" />
                                        </h4>
                                        <a name="#Details"></a>
                                        <table>
                                            <span id="_description15" itemprop="description">
                                                <tr id="bed">
                                                    <td><b><%= Resources.RealEstate.Column_Num_Bedroom %></b></td>
                                                    <td>&nbsp;&nbsp;:&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:DynamicControl runat="server" DataField="Num_Bedrooms" Mode="ReadOnly" />
                                                    </td>
                                                </tr>
                                                <tr id="bath">
                                                    <td><b><%= Resources.RealEstate.Column_Num_Bathroom %></b></td>
                                                    <td>&nbsp;&nbsp;:&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:DynamicControl runat="server" DataField="Num_Bathrooms" Mode="ReadOnly" />
                                                    </td>
                                                </tr>
                                                <tr id="Garage">
                                                    <td><b><%= Resources.RealEstate.Column_Garage %></b></td>
                                                    <td>&nbsp;&nbsp;:&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:DynamicControl runat="server" DataField="Has_Garage" Mode="ReadOnly" />
                                                    </td>
                                                </tr>
                                                <tr id="garden">
                                                    <td><b><%= Resources.RealEstate.Column_Garden %></b></td>
                                                    <td>&nbsp;&nbsp;:&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:DynamicControl runat="server" DataField="Has_Garden" Mode="ReadOnly" />
                                                    </td>
                                                </tr>
                                                <tr id="floor">
                                                    <td><b><%= Resources.RealEstate.Column_Floor %></b></td>
                                                    <td>&nbsp;&nbsp;:&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:DynamicControl runat="server" DataField="Floor" Mode="ReadOnly" />
                                                    </td>
                                                </tr>
                                                <tr id="row">
                                                    <td colspan="3">
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr id="contract">
                                                    <td colspan="2"><b style="color: #27DA93"><%= Resources.RealEstate.Column_Property_Size %>:&nbsp;&nbsp; </b></td>
                                                    <td>
                                                        <asp:DynamicControl runat="server" DataField="Property_Size" Mode="ReadOnly" />
                                                    </td>
                                                </tr>
                                                <span id="_offers12" itemprop="offers" itemscope itemtype="http://schema.org/Offer">
                                                    <span itemprop="price">
                                                        <tr id="sale">
                                                            <td><b><%= Resources.RealEstate.Column_Sale_Price %></b></td>
                                                            <td>&nbsp;&nbsp;:&nbsp;&nbsp;</td>
                                                            <td>
                                                                <asp:DynamicControl runat="server" DataField="Sale_Price" Mode="ReadOnly" />
                                                            </td>
                                                        </tr>
                                                        <tr id="rent">
                                                            <td><b><%= Resources.RealEstate.Column_Rent_Price %></b></td>
                                                            <td>&nbsp;&nbsp;:&nbsp;&nbsp;</td>
                                                            <td>
                                                                <asp:DynamicControl runat="server" DataField="Rent_Price" Mode="ReadOnly" />
                                                            </td>
                                                        </tr>
                                                    </span>
                                                </span>
                                            </span>
                                        </table>
                                     
                                    </div>

                                    <div class="clearfix"></div>
                                </div>
                                <div class="map-buy-single">
                                    <h4><%= Resources.RealEstate.Column_Location %></h4>
                                    <div class="map-buy-single1">
                                        <asp:DynamicControl runat="server" DataField="Location" Mode="ReadOnly" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="single-box-right right-immediate">
                                    <h4><%= Resources.RealEstate.Other_Properties %></h4>
                                    <asp:Repeater runat="server" ID="fetrep">
                                        <ItemTemplate>
                                            <div class="single-box-img ">
                                                <div class="box-img">
                                                    <a href="<%# Eval("Url_ext") %>">
                                                        <img class="img-responsive_def" src="<%# Eval("Property_Photo") %>" alt=""></a>
                                                </div>
                                                <div class="box-text">
                                                    <p><a href="<%# Eval("Url_ext")%>"><%# Eval("Property_Type.Property_Type_Name") %></a></p>
                                                    <a href="<%# Eval("Url_ext") %>" class="in-box"><%= Resources.RealEstate.More %></a>
                                                </div>
                                                <div class="clearfix"></div>
                                            </div>

                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                    <%-- <div class="container">
                       <div class="future">
                        <h3><%= Resources.RealEstate.Other_Projects %></h3>
                        <div class="content-bottom-in">
                            <div class="nbs-flexisel-container">
                                <div class="nbs-flexisel-inner">
                                    <ul id="flexiselDemo2" class="nbs-flexisel-ul" style="left: -506.16px; display: block;">
                                        <asp:Repeater runat="server" ID="futrep">
                                            <ItemTemplate>
                                                <li class="nbs-flexisel-item" style="width: 333px;">
                                                    <div class="project-fur">
                                                        <a href="<%# table.GetActionPath("Details")+"?PropertyID="+Eval("PropertyID") %>">
                                                            <img class="img-responsive" src="<%# Eval("Property_Photo") %>" alt="">
                                                        </a>
                                                        <div class="fur">
                                                            <div class="fur1" style="position: center; text-align: center">
                                                                <span class="fur-money"><%# Eval("Property_Type.Property_Type_Description") %></span>
                                                                <h6 class="fur-name"><a href="<%# table.GetActionPath("Details")+"?PropertyID="+Eval("PropertyID") %>"><%= Resources.RealEstate.Details %></a></h6>
                                                                <span><%# Eval("Address") %></span>
                                                            </div>
                                                            <div class="fur2">
                                                                <span><%# Eval("Property_Size") %> <%= Resources.RealEstate.MSQR %></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>


                                    </ul>
                                </div>
                            </div>
                         <%--   <script type="text/javascript">
						$(window).load(function() {
							$("#flexiselDemo1").flexisel({
								visibleItems: 4,
								animationSpeed: 1000,
								autoPlay: true,
								autoPlaySpeed: 3000,    		
								pauseOnHover: true,
								enableResponsiveBreakpoints: true,
						    	responsiveBreakpoints: { 
						    		portrait: { 
						    			changePoint:480,
						    			visibleItems: 1
						    		}, 
						    		landscape: { 
						    			changePoint:640,
						    			visibleItems: 2
						    		},
						    		tablet: { 
						    			changePoint:768,
						    			visibleItems: 3
						    		}
						    	}
						    });
						    
						});
			</script>--%>
                    <%--<script type="text/javascript" src="../../../CustomDesign/js/jquery.flexisel.js"></script>
                            <script type="text/javascript" src="../../../CustomDesign/js/jquery.blueberry.js"></script>--%>
                        </div>
                    </div></div>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <div class="text-danger">
                        <asp:Literal ID="Literal1" runat="server"
                            Text="<%$Resources:DynamicData, ItemDoesNotExist%>" />
                    </div>
                </EmptyDataTemplate>
            </asp:FormView>
            <!--Source-->
            <ef:EntityDataSource ID="DetailsDataSource" runat="server" EnableDelete="true"
                OnQueryCreated="DetailsDataSource_QueryCreated"
                OnDeleting="DetailsDataSource_Deleting"
                OnDeleted="DetailsDataSource_Deleted" />
            <!--query-->
            <asp:QueryExtender TargetControlID="DetailsDataSource" ID="DetailsQueryExtender" runat="server">
                <asp:DynamicRouteExpression />
            </asp:QueryExtender>
            <script>
                window.onload = function () {
                    location.hash = "#Details";
                };
            </script>
            <div id="insert">
                <div class="clearfix"></div>
                <center>
                <div class="create"><div class="login-top">
                <label style="font-size:26px;position:center">
               <asp:DynamicHyperLink ID="ListHyperLink" runat="server" Action="List">
                    <asp:Literal ID="LiteralShowAllRecordsLinkText" runat="server"
                     />
                </asp:DynamicHyperLink>
            </label></div></div>
           </center>
            </div>
            <div class="confirmInput">
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
