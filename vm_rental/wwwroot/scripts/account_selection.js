$(document).ready(function(){
    $account_type_btns = $(".account-type-btn");

    $sign_up_form_personal = $(".sign-up-container[type='personal']");
    $sign_up_form_business = $(".sign-up-container[type='business']");
 
    $isPersonal = true;
    $isContainerToggled = false;

    $($account_type_btns).click(function(){
        if($(this).hasClass("personal-btn")){
              $isPersonal = true;
              $(this).attr('active', true);
              $(".business-btn").attr('active', false);
              $sign_up_form_personal.attr('active', true);
              $sign_up_form_business.attr('active', false);
                
        }
        else if($(this).hasClass("business-btn")){
              $isPersonal = false;
              $(this).attr('active', true);
              $(".personal-btn").attr('active', false);
              $sign_up_form_personal.attr('active', false);
              $sign_up_form_business.attr('active', true);
        }   
            $('html,body').animate({scrollTop: $(this).offset().top}, 500);
    });
});