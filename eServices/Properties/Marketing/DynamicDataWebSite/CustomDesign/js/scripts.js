// navigation slide-in
$(window).load(function() {
  $('.nav_slide_button').click(function() {
    $('.pull').slideToggle();
  });
});
//scroll to result 
function scrolltoreslist()
{
    $('html, body').animate({
        scrollTop: $("#reslist").offset().top
    }, 1000);
}