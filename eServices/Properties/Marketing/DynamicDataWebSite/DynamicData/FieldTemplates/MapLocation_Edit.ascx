<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MapLocation_Edit.ascx.cs" Inherits="DynamicDataWebSite.DynamicData.FieldTemplates.MapLocation_Edit" %>
<%/*
Asp.Net Map Location Controller
* Basic map features:

* Location Is GeoLocated via google maps api location services
* In case of location services isn't avalable
* the <div id="err"> will display a message and the coords will be taken from
 behind code (Lat , Lng , Zoom)
* the user input via marker or address will be saved in the hidden field "locargs"
* and will be submitted on response
* the database form of location (lat,lng,zoom)--> (12.22,12.22,8)
* and will be decoded on the edit template page 
*/ %>

<%--Connect To Google API--%>
 <script async defer
    src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBGp-YHqpbM53E2MkXG5kYBkYWvWP1Z2Ag&callback=initMap">
    </script>

<%-- Back to server Location--%>
<asp:HiddenField ID="locargs" runat="server" ClientIDMode="Static" />
<%--the Validators--%>
<%--   <asp:CompareValidator runat="server" ID="comval" ControlToValidate="locargs" ControlToCompare="" Display="Dynamic" CssClass="DDControl DDValidator"></asp:CompareValidator>
    <asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="DDControl DDValidator" ControlToValidate="locargs" Display="Dynamic" />--%>

<%-- The Render table--%>
<hr />
<input id="address" type="text" placeholder="<%= Resources.Map.address %>" style="font-size: 16px; color:black;font-family: 'Segoe UI'; width: 80%" onkeypress="return runScript(event)" />
<label class="hvr-sweep-to-right" style="position: relative; top: -1px; height: 42px" onclick="document.getElementById('srch').click();" >
<input id="srch" class="button2 fontstyled1" type="button" style="position: relative; top: -1px; height: 42px" value="<%= Resources.Map.find_address %>" />
</label>
<input type="button" id="hlp" class="button" data-toggle="modal" data-target="#helppop" value="?" style="font-size: 0px;display:hidden" />
<%--The Map Div--%>
<div id="map-canvas" style="width: 100%; height: 400px"></div>
<%-- Dispaly error msg of Geolocation--%>
<div id="err"></div>
<hr />
<%-- The Help Pop--%>
<div id="helppop" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <table>
                <tr>
                    <td>
                        <img alt="Click out to exit" src="../../CustomDesign/images/help.png" /></td>
                    <td align="left">
                        <h2><%=GetGlobalResourceObject("Map","help").ToString() %></h2>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <h3><%=GetGlobalResourceObject("Map","select").ToString() %></h3>
                        <ol>
                            <li><%=GetGlobalResourceObject("Map","drag").ToString() %> </li>
                            <li><%=GetGlobalResourceObject("Map","search_address").ToString() %></li>
                        </ol>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
<asp:HiddenField runat="server" ID="chkpost" ClientIDMode="Static" Value="no" />
<script type="text/javascript" id="pageload">
    //page load
    function initMap() {
        init_map(null);
    }
    function LoadMap_Edit()
    {
        var f=document.getElementById('chkpost').value;
        if(f=='no')
        {
            document.getElementById('chkpost').value='yes';
            init_map(null);       
        }
        else
        {
            var code= GetArgsInc();
            init_map(code);
        }  
    }
    function GetArgsInc(){
        var s=document.getElementById('locargs').value;
        var onel =s.indexOf(',');
        var twol=s.lastIndexOf(',');
        var lat=s.substring(0,onel);
        var lng=s.substring(onel+1,twol);
        var zoom=s.substring(twol+1);
        return {
            Lat:lat,
            Lng:lng,
            Zoom:zoom
        }
    }
</script>
<script type="text/javascript" id="EnterTrigger">
    function runScript(e) {
        if (e.keyCode == 13) {
            document.getElementById('srch').click();
            return false;
        }
    }
</script>
<script type="text/javascript" id="buttontext">
    // set the ? button's Text
    function help(f) { var b = document.getElementById('hlp'); if (f == "t") b.value = "<%=GetGlobalResourceObject("Map","help_more").ToString() %>"; else b.value = "?"; }
    // set the search button's Text
    function search(f) { var b = document.getElementById('srch'); if (f == "t") b.value = "<%=GetGlobalResourceObject("Map","find_address").ToString() %>"; else b.value = ">"; }  
</script>
<script type="text/javascript" id="globalvar">
    var map;
    var image = new google.maps.MarkerImage('../../CustomDesign/images/marker.png', null, null, null, new google.maps.Size(35, 50));
    var geocoder = new google.maps.Geocoder();
    var markers = [];
</script>
<script type="text/javascript" id="mapinit">
    function init_map(args) {
        //get the lnglat form server on AJAx update panel postback
        var myLatlng; 
        if(args==null){
            myLatlng = new google.maps.LatLng(<%=GetCoords("lat") %>,<%=GetCoords("lng") %>);  
        }
        else { 
            if(args.Lat=="" || args.Lng=="")
            {
                myLatlng = new google.maps.LatLng(<%=GetCoords("lat") %>,<%=GetCoords("lng") %>);  
            }
            else
                myLatlng= new google.maps.LatLng(args.Lat,args.Lng); 
        }
       
        var thezoom=<%=GetCoords("zoom")%>;
        if(args!=null)
        {
            if(args.Zoom!="")
            {
                var x=parseInt(args.Zoom);
                thezoom=x;
            }
        }
        //the map options
        var mapOptions = {zoom:thezoom,center: myLatlng,mapTypeId: google.maps.MapTypeId.ROADMAP,};
        // define the map
        map= new google.maps.Map(document.getElementById('map-canvas'), mapOptions); 
        //define the marker and add it to the array
        var marker = new google.maps.Marker({ position: myLatlng, icon:image,draggable: true, map: map,title:"<%=House%>",});
            markers.push(marker);
        // an info window any thing can be but in here
            var infowindow = new google.maps.InfoWindow({content:"<div class='h1'><%=House%></div>" });
        //show the info window on click
              google.maps.event.addListener(marker, 'click', function() {infowindow.open(map,marker); });
        //get the coords on the drag
              google.maps.event.addListener(marker, 'dragend', function (event) {  CallArgs(event.latLng.lat()+","+event.latLng.lng()+","+map.getZoom()); });
        //keep the map centered while resizing
              google.maps.event.addDomListener(window, 'resize', function() { map.setCenter(myLatlng); });  
        //keep the marker centered while zooming
              google.maps.event.addDomListener(map,'zoom_changed', function() {CallArgs(map.getCenter()+","+map.getZoom());});  
        //keep the marker centered while the map is dragged
              google.maps.event.addListener(map, 'dragend', function() { var l=map.getCenter(); marker.setPosition(l);} );
          }
</script>
<script id="callargs" type="text/javascript">
    //set the values on the hidden feild
    function CallArgs(val) { var d = document.getElementById('locargs'); d.value = val; }
</script>
<script id="geocoder" type="text/javascript">
    document.getElementById('srch').addEventListener('click', function() { geocodeAddress(geocoder,map);});   
    function geocodeAddress(geocoder, resultsMap) {
        var address = document.getElementById('address').value;
        geocoder.geocode({'address': address}, function(results, status) {
            if (status === google.maps.GeocoderStatus.OK) {
                resultsMap.setCenter(results[0].geometry.location);
                CallArgs(results[0].geometry.location+","+resultsMap.getZoom());
                del_all_marks();
                var sub = new google.maps.Marker({ position: results[0].geometry.location, icon:image,draggable: true, map: resultsMap,title:"<%=House%>",});
     google.maps.event.addListener(sub, 'dragend', function (event) { CallArgs(event.latLng.lat()+","+event.latLng.lng()+","+resultsMap.getZoom()) });
     google.maps.event.addDomListener(map,'zoom_changed', function() {var l=map.getCenter(); sub.setPosition(l);});  
     google.maps.event.addListener(sub, 'click', function() {infowindow.open(resultsMap,sub); });
     markers.push(sub);
 } else {
     document.getElementById("err").innerHTML +="<%=GetGlobalResourceObject("Map","geoloc_error").ToString()%>";
    }
  });
    function del_all_marks() { for (var i = 0; i < markers.length; i++) { markers[i].setMap(null); } markers = []; };
}
</script>
<script type="text/javascript" id="deletemarkers">
    //delete all markers
    function DeleteMarkers() { for (var i = 0; i < markers.length; i++) { markers[i].setMap(null); } markers = []; };
</script>
