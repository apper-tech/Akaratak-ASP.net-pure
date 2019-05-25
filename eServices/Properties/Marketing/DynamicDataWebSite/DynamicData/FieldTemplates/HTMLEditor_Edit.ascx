<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HTMLEditor_Edit.ascx.cs" Inherits="DynamicDataWebSite.DynamicData.FieldTemplates.HTMLEditor_Edit" %>
<script src="../../CustomDesign/js/ckeditor/ckeditor.js"></script>
<asp:TextBox runat="server" ID="text" TextMode="MultiLine"></asp:TextBox>
<script>
    CKEDITOR.replace('<%=text.ClientID%>');
</script>
