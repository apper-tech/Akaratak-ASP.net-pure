<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePicker_Edit.ascx.cs" Inherits="DynamicDataWebSite.DynamicData.FieldTemplates.DatePicker_Edit" %>
<link rel="stylesheet" href="../../CustomDesign/css/jquery.calendars.picker.css">
<script src="../../CustomDesign/js/jquery.plugin.js"></script>
<script src="../../CustomDesign/js/Calender/jquery.calendars.all.js"></script>
<script src="../../CustomDesign/js/Calender/jquery.calendars.picker.ext.js"></script>
<script src="../../CustomDesign/js/Calender/jquery.calendars.ummalqura.js"></script>
<script src="../../CustomDesign/js/Calender/jquery.calendars.ummalqura-ar.js"></script>
<script src="../../CustomDesign/js/jquery-ui.js"></script>
<script src="../../CustomDesign/js/Calender/jquery.calendars.picker-ar.js"></script>
<script src="../../CustomDesign/js/Calender/jquery.calendars.picker.js"></script>
<script>
function showDate(date) {
    var d = document.getElementById('dateargs');
    d.value = date;
}
</script>
<asp:TextBox runat="server" ID="datef" placeholder="<%#Column.Description %>" CssClass="fontstyled2 cal"></asp:TextBox>
<asp:HiddenField ID="dateargs" runat="server" ClientIDMode="Static" />