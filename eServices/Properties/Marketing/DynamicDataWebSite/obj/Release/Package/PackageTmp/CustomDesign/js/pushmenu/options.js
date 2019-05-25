$(document).ready(function(){
	// HTML markup implementation, overlap mode
	$( '#menu' ).multilevelpushmenu({
	    direction: 'rtl',
	    menuWidth: 250, // '450px', '30em', '25%' will also work
	    menuHeight: '100%',
		collapsed: true,
		containersToPush: [$('#collapse')],
		backItemIcon: 'fa fa-angle-left',
		groupIcon: 'fa fa-angle-right',
		fullCollapse: true,                                        // set to true to fully hide base level holder when collapsed
        swipe: 'both',
        onItemClick: function () {
            // First argument is original event object
            var event = arguments[0],
                // Second argument is menu level object containing clicked item (<div> element)
                $menuLevelHolder = arguments[1],
                // Third argument is clicked item (<li> element)
                $item = arguments[2],
                // Fourth argument is instance settings/options object
                options = arguments[3];

            // You can do some cool stuff here before
            // redirecting to href location
            // like logging the event or even
            // adding some parameters to href, etc...

            // Anchor href
            var itemHref = $item.find('a:first').attr('href');
            // Redirecting the page
           // alert(itemHref);
            if (!itemHref.Contains('#'))
            {
                location.href = itemHref;
            }
            if (itemHref == 'ServerControls/Pushmenu/#')
            {
                Location.href = '/';
            }
            
        }
		
	});
		$('#baseexpand').click(function(){
		        $('#menu').multilevelpushmenu('expand'); 
        });

});