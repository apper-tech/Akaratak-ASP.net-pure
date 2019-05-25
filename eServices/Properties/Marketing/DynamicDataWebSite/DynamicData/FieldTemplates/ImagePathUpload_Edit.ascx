<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImagePathUpload_Edit.ascx.cs" Inherits="DynamicDataWebSite.DynamicData.FieldTemplates.ImagePathUpload_Edit" %>
<asp:UpdatePanel runat="server" ID="up1">
    <ContentTemplate>
        <hr />
        <div id="imageUpload" class="price"> 
            <div class="col-sm-4 price-top">
                    <asp:Button runat="server" ID="BtnCancel" Enabled="false" ForeColor="Gray" Text="<%$Resources:DynamicData, Cancel%>" OnClick="BtnCancel_Click" Style="font-family: 'Segoe UI'" />
                </div>
                
                <div class="col-sm-4 price-top" style="text-align:center">
                    <label class="hvr-sweep-to-right">
                        <asp:Button runat="server" CssClass="button2" ID="BtnUpload" OnClick="BtnUpload_Click" Text="<%$Resources:DynamicData, Upload%>" /></label>
                    <asp:Button ID="upClick" runat="server" Style="display: none" />
                </div>
               <div class="col-sm-4 price-top">
                    <asp:FileUpload runat="server" multiple="true" onchange="checkupload('u')" ID="fileupload1" Style="" />
                    <asp:CustomValidator runat="server" ID="ErrVal" CssClass="text-danger" ControlToValidate="fileupload1" OnServerValidate="Unnamed_ServerValidate" Style="display: none" Text="<%$Resources:DynamicData, UploadImage_FileTypeValidator_MessageFormat%>"></asp:CustomValidator>
                </div>
                    <asp:Label Text="<%$Resources:DynamicData, UploadImage_FileTypeValidator_MessageFormat%>" ID="errorLabel" Visible="false" runat="server" CssClass="text-danger" />
        </div>
        <hr />
        <asp:Table runat="server" ID="photoTable">
            <asp:TableRow>
            </asp:TableRow>
        </asp:Table>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="BtnUpload" />
        <asp:AsyncPostBackTrigger ControlID="BtnCancel" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
<br />
<script>
    function checkupload(f) {
        if (f == 'b') {
            openfileDialog();
        }
        else {
            // document.getElementById('<%= errorLabel.ClientID %>').style = "visibility:hidden";
            Upload();
        }
    }
    function Upload() {
        // $("#<%= upClick.ClientID %>").click();
    }
    function openfileDialog() {
        $("#<%= fileupload1.ClientID %>").click();
    }
</script>
