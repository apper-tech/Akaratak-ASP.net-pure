<%@ Control Language="C#" CodeBehind="UploadFile_Edit.ascx.cs" Inherits="DynamicDataWebSite.UploadFile_EditField" %>

<asp:PlaceHolder 
    ID="PlaceHolder1" 
    runat="server" 
    Visible="false">
    <asp:Image ID="Image1" runat="server" />&nbsp;
    <asp:Label ID="Label1" runat="server" Text="<%# FieldValueString %>" />
    <asp:HyperLink ID="HyperLink1" runat="server"><%# FieldValueString %></asp:HyperLink>
</asp:PlaceHolder><br />
<asp:FileUpload 
    ID="FileUpload1" 
    runat="server" 
    CssClass="DDTextBox" 
    Width="150px"/>

<asp:CustomValidator 
    ID="CustomValidator1" 
    runat="server" 
    ControlToValidate="FileUpload1" 
    onservervalidate="CustomValidator1_ServerValidate">
</asp:CustomValidator>
