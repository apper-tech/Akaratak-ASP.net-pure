<script type="text/javascript" id="pageload">
//page load
    window.onload = function () {
        tooltip.pop(document.getElementById('address'),'<%=GetGlobalResourceObject("Map","address").ToString() %>');
        ShowPopup();
        user_loc();
        init_map();
    }
function ShowPopup() {
    var content3 = "<%=GetGlobalResourceObject("Map","position").ToString() %>";
    TINY.box.show(content3, 0, 0, 0, 0, 5);
}
</script>
<script type="text/javascript" id="buttontext">
    // set the ? button's Text
    function help(f) { var b = document.getElementById('hlp'); if (f == "t") b.value = "<%=GetGlobalResourceObject("Map","help").ToString() %>"; else b.value = "?"; }
// set the search button's Text
function search(f) { var b = document.getElementById('srch'); if (f == "t") b.value = "<%=GetGlobalResourceObject("Map","find_address").ToString() %>"; else b.value = ">"; }  
</script>
<script type="text/javascript" id="globalvar">
    var gl;
var map;
var image = new google.maps.MarkerImage('/CustomDesign/images/Icons/marker.png', null, null, null, new google.maps.Size(35,50));
var geocoder = new google.maps.Geocoder();
var markers = [];
</script>
<script type="text/javascript" id="mapinit">
    function init_map() {
        var myLatlng; 
        myLatlng = new google.maps.LatLng(<%=Lat %>,<%=Lng %>);  
        //the map options
        var mapOptions = {zoom: 8,center: myLatlng,mapTypeId: google.maps.MapTypeId.ROADMAP,};
        // define the map
        map= new google.maps.Map(document.getElementById('map-canvas'), mapOptions); 
        //define the marker and add it to the array
        var marker = new google.maps.Marker({ position: myLatlng, icon:image,draggable: true, map: map,title:'<%=GetGlobalResourceObject("Map","house").ToString() %>'});
        markers.push(marker);
        // an info window any thing can be but in here
        var infowindow = new google.maps.InfoWindow({content:"<div class='h1'><%=GetGlobalResourceObject("Map","house").ToString() %></div>" });
//show the info window on click
google.maps.event.addListener(marker, 'click', function() {infowindow.open(map,marker); });
//get the coords on the drag
google.maps.event.addListener(marker, 'dragend', function (event) {  CallArgs(event.latLng.lat()+","+event.latLng.lng()+","+map.getZoom()) });
//keep the map centered while resizing
google.maps.event.addDomListener(window, 'resize', function() { map.setCenter(myLatlng); });  
//keep the marker centered while zooming
google.maps.event.addDomListener(map,'zoom_changed', function() {var l=map.getCenter(); marker.setPosition(l);});  
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
            var sub = new google.maps.Marker({ position: results[0].geometry.location, icon:image,draggable: true, map: resultsMap,title:'<%=GetGlobalResourceObject("Map","house").ToString() %>',});
            google.maps.event.addListener(sub, 'dragend', function (event) { CallArgs(event.latLng.lat()+","+event.latLng.lng()+","+resultsMap.getZoom()) });
            google.maps.event.addDomListener(map,'zoom_changed', function() {var l=resultsMap.getCenter(); sub.setPosition(l);});  
            google.maps.event.addListener(sub, 'click', function() {infowindow.open(resultsMap,sub); });
            markers.push(sub);
        } else {
            tooltip.pop(document.getElementById('err'), '<%=GetGlobalResourceObject("Map","geoloc_error").ToString() %>');
        }
    });
  
}
function del_all_marks() { for (var i = 0; i < markers.length; i++) { markers[i].setMap(null); } markers = []; };
</script>
<script type="text/javascript" id="geolocation">
    function user_loc() {
        if (navigator.geolocation) {
            var positionOptions = { enableHighAccuracy: true, timeout: 10 * 1000 };
            navigator.geolocation.getCurrentPosition(function (position) {
                var geolocate = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                map.setCenter(geolocate);
                del_all_marks();
                var marker = new google.maps.Marker({ position: geolocate, icon: image, draggable: true, map: map, title: '<%=GetGlobalResourceObject("Map","house").ToString() %>' });
                markers.push(marker);
                // an info window any thing can be but in here
                var infowindow = new google.maps.InfoWindow({ content: "<div class='h1'><%=GetGlobalResourceObject("Map","house").ToString() %></div>" });
            //show the info window on click
            google.maps.event.addListener(marker, 'click', function () { infowindow.open(map, marker); });
            //get the coords on the drag
            google.maps.event.addListener(marker, 'dragend', function (event) { CallArgs(event.latLng.lat() + "," + event.latLng.lng() + "," + map.getZoom()) });
        }, geolocationError, positionOptions);
    } else {
        tooltip.pop(document.getElementById('err'), '<%=GetGlobalResourceObject("Map","geoloc_support").ToString() %>'); }
}
//if the geolocation error
function geolocationError(positionError) { tooltip.pop(document.getElementById('err'), 'خطأ: '+ positionError.message); }
</script>
