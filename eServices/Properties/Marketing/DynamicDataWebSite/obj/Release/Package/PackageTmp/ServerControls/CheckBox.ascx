<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckBox.ascx.cs" Inherits="DynamicDataWebSite.ServerControls.CheckBox" %>
<div id="checkbox">
    <label for="<%= Agree.ClientID %>" style="<%= LabelStyle%>">
    <asp:CheckBox runat="server" ID="Agree" OnCheckedChanged="Agree_CheckedChanged" />
    </label>
    <div class="text-danger">
        <asp:Label runat="server" ID="ValidateCheckboxErrorLt" Visible="false"></asp:Label>
    </div>
  
    <script>
        $(document).ready(function () {
            $('input').iCheck({
                checkboxClass: 'icheckbox_square-green'
            });
            var d = document.getElementById('<%= Agree.ClientID%>');
            d.setAttribute('style', '<%= Style%>' + 'display:none;');
          <%--  if('<%=Required%>'=='True')
            {
                if(document.getElementById('<%=Agree.ClientID%>').checked==false)
                {
                    document.getElementById('<%=ValidateCheckboxErrorLt.ClientID%>').visible=true;
                }
            }--%>
        });
    </script>
</div>
