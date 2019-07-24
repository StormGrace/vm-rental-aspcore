$( document ).ready(function() {
    $isNavTrayActive = false;
    $navBarWrapper = $('.nav-bar-wrapper');
    $navBar = $('.nav-bar');
    $mobileNavBtn = $('.mobile-nav-btn');
    
    $($mobileNavBtn).click(function(){
        setNavTrayActivity($isNavTrayActive = !$isNavTrayActive)
    });
});

function setNavTrayActivity($isActive){
    $isNavTrayActive = $isActive;
    $navBar.attr('is-active', $isActive.toString());
    $isActive ? $mobileNavBtn.text('✕') : $mobileNavBtn.text('☰');
}