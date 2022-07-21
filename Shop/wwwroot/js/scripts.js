$(document).ready(function() {

    //toggle header icons
    let pageTheme = false;
    let loggedIn = true;

    $('.header-theme-button').click(function(e) {
        e.preventDefault();
        pageTheme = !pageTheme;
        $(this).toggleClass('active');
    });

    $('.header-login-button').click(function(e) {
        e.preventDefault();
        loggedIn = !loggedIn;
        //$(this).add($(this).closest('div')).toggleClass('active');
    });
    //--

    //toggle authorization windows
    let enterWithCertificate = true;

    $('.enter-with-certificate-button').add('.back-to-auth-popup-button').click(function(e) {
        e.preventDefault();
        enterWithCertificate = !enterWithCertificate;
        $('.authPopup').toggleClass('active');
        $('.authPopupCertificate').toggleClass('active');
    });
    //--

    //toggle password input icon
    let showPassword = false;

    $('.show-password-button').click(function(e) {
        e.preventDefault();   
        showPassword = !showPassword;  
        $(this).toggleClass('active');   
        $(this).closest('.form-item').find('input').attr('type',showPassword ? 'text' : 'password');
    });
    //--

    //the tabs
	$('.the_tabs_item').not('.the_tabs_item.active').slideUp(0);
	
	$('.the_tabs_head a').click(function(e) {
		e.preventDefault();
		$('.the_tabs_item').removeClass('active').slideUp();				
		$('.the_tabs_item:nth-child('+(parseInt($(this).index('.the_tabs_head a'))+1)+')').addClass('active').slideDown();
		
		$('.the_tabs_head a').removeClass('active');
		$(this).addClass('active');
	});
	//-

});