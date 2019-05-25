<%@ Control Language="C#" CodeBehind="CascadingForeignKey_Edit.ascx.cs" Inherits="DynamicDataWebSite.CascadingForeignKey_EditField" %>

<asp:DropDownList ID="DropDownList1" runat="server" CssClass="in-drop" OnDataBinding="DropDownList1_DataBinding" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
</asp:DropDownList>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="text-danger" ControlToValidate="DropDownList1" Display="Dynamic" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="DDControl DDValidator" ControlToValidate="DropDownList1" Display="Dynamic" />

<script>
    function HookbtnSelect()
    {
        var args = GetArgs();
        LoadMap(args);
    }
    function GetArgs() {
        var dr = document.getElementById('<%=DropDownList1.ClientID%>');
        var latlng = dr.options[dr.selectedIndex].getAttribute('tooltip');
        var onel = latlng.indexOf('|');
        var lat = latlng.substring(0, onel);
        var lng = latlng.substring(onel + 1, latlng.lenght);
        return {
            Lat: lat,
            Lng: lng,
            Zoom: 5
        }
    }
</script>
