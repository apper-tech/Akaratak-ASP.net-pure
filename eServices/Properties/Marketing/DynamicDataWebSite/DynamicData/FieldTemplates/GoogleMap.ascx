<%@ Control Language="C#" CodeBehind="GoogleMap.ascx.cs" Inherits="DynamicDataWebSite.GoogleMapField" %>

    <style>
      #map {
        width: 500px;
        height: 400px;
      }
    </style>
    <script src="https://maps.googleapis.com/maps/api/js"></script>
    <script>
      function initialize() {
        var mapCanvas = document.getElementById('map');
        var mapOptions = {
          center: new google.maps.LatLng(<%# FieldValueString %>),
          zoom: 8,
          mapTypeId: google.maps.MapTypeId.ROADMAP
        }
        var map = new google.maps.Map(mapCanvas, mapOptions)
      }
      google.maps.event.addDomListener(window, 'load', initialize);
    </script>

    <div id="map"></div>

<asp:Literal runat="server" ID="Literal1" Text="<%# FieldValueString %>" />

