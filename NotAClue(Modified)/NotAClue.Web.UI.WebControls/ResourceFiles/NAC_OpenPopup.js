
function NAC_OpenPopup(popupUrl, returnParameter, returnControl, additionalQueryString, title, width, height, scrollbars) {
    ///<summary>Open a new window in the centre of the screen using the passed in URL.</summary>
    ///<field name="popupUrl" type="string">The popup window's URL as a string.</field>
    ///<field name="title" type="string">The name of the window as a string.</field>
    ///<field name="width" type="Number" integer="true">The width of the the window as an int.</field>
    ///<field name="height" type="Number" integer="true">The height of the the window as an int.</field>
    // pass the id of the return convtrol
    popupUrl = popupUrl + "?" + returnParameter + "=" + returnControl + additionalQueryString;
    var x = 0;
    var y = 0;
    x = (screen.availWidth - 12 - width) / 2;
    y = (screen.availHeight - 48 - height) / 2;

    var features =
        "'screenX=" + x +
        ",screenY=" + y +
        ",width=" + width +
        ",height=" + height +
        ",top=" + y +
        ",left=" + x +
        ",status=no" +
        ",resizable=no" +
        ",scrollbars=" + scrollbars +
        ",toolbar=no" +
        ",location=no" +
        ",modal=yes" +
        ",menubar=no'";

    var NewWindow = window.open(popupUrl, title, features);
    NewWindow.focus();
    return NewWindow;
}