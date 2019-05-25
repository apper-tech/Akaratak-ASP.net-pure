<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MapLocation.ascx.cs" Inherits="DynamicDataWebSite.DynamicData.FieldTemplates.MapLocation" %>
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
<asp:PlaceHolder runat="server" ID="MapHolder">
    <link rel="stylesheet" href="/CustomDesign/css/Map.css" type="text/css" />
    <script
        src="https://maps.googleapis.com/maps/api/js"></script>
    <img id="myImg" src="https://maps.googleapis.com/maps/api/staticmap?center=<%= Lat%>,<%= Lng%>&zoom=<%= Zoom%>&size=600x400&maptype=roadmap&markers=color:green|icon:http://www.akaratak.com/customdesign/images/marker.ico|size:big|<%= Lat%>,<%= Lng%>&key=AIzaSyDVx1oYrMkFUTeGbTxeShvP18xme3qB3Fs"
        alt="<%=  GetGlobalResourceObject("Map", "back").ToString()%>" style="width:100%">
    <div id="myModal" class="modal">
        <div class="modal-content" id="googleMap"></div>
        <div id="caption"></div>
    </div>
    <script>
        var myCenter=new google.maps.LatLng(<%= Lat%>,<%= Lng%>);
        var marker;
        var map;
    
        function initialize()
        {
            var mapProp = { center:myCenter,zoom:<%= Zoom%>, mapTypeId:google.maps.MapTypeId.ROADMAP };
            map=new google.maps.Map(document.getElementById("googleMap"),mapProp);
            var image = new google.maps.MarkerImage('/CustomDesign/images/marker.png', null, null, null, new google.maps.Size(35, 50));
            marker=new google.maps.Marker({ position: myCenter, icon: image, map:map, animation:google.maps.Animation.BOUNCE });
            var infowindow = new google.maps.InfoWindow({ content:"<p style='font-family:'Segoe UI';font-size:30px;color:black'><%=  GetGlobalResourceObject("Map", "back").ToString()%></p>"});
            google.maps.event.addListener(marker,'click',function() { infowindow.open(map,marker); map.setCenter(marker.getPosition()); });
            google.maps.event.addDomListener(window, 'resize', function() { map.setCenter(marker.getPosition()); });
            google.maps.event.addListenerOnce(map, 'idle', function(){
                map.setCenter(marker.getPosition());
            });
    }
    function CenterMap()
    {
        map.setCenter(marker.getPosition());
        // alert(map.getCenter());
    }
    //  google.maps.event.addDomListener(window, 'load', initialize);
   

    </script>
    <script>
        var modal = document.getElementById('myModal');
        var img = document.getElementById('myImg');
        var modalImg = document.getElementById("googleMap");
        var captionText = document.getElementById("caption");
        img.onclick = function(){
            modal.style.display = "block";
            modalImg.src = this.src;
            modalImg.alt = this.alt;
            captionText.innerHTML = this.alt;
            initialize();
            CenterMap();
        }

        captionText.onclick = function() {
            modal.style.display = "none";
        }
    </script>
</asp:PlaceHolder>
<asp:PlaceHolder runat="server" Visible="false" ID="DefaultHolder">
    <div class="map">
        <img src="../../CustomDesign/images/mapdefault.png" style="width: 100%; height: 100%" />
        <h1 class="centertext">
            <asp:Literal runat="server" ID="DefaultText"></asp:Literal>
        </h1>
    </div>
</asp:PlaceHolder>
