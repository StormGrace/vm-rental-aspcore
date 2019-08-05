$(document).ready(function(){
    $isBusinessByDefault = false;

    $form = $(".acc-form");
    $buttons = $(".acc-type__button");

    $personalButton = $buttons.get(0);
    $businessButton = $buttons.get(1);

    $personalFields = $(".acc-form__field-section[section-type='personal']");
    $businessFields = $(".acc-form__field-section[section-type='business']");
    
    $isBusinessType = false;
  
    $lastSelected = null;

    onTypeChange();


    $($buttons).click(function(){    
        if($(this).is($personalButton)){
            $isBusinessType = false;
            $lastSelected = $personalButton;
        }
        else if($(this).is($businessButton)){
            $isBusinessType = true;
            $lastSelected = $businessButton;
        }
        
        onTypeChange($isBusinessType)

        $('html,body').animate({scrollTop: $(this).offset().top}, 700);
    });

    function onTypeChange($isBusinessType){
            if(!$isBusinessType){
                $($personalButton).addClass("acc-type__button--active");
                $($businessButton).removeClass("acc-type__button--active")
           
                 $form.attr('form-type', 'personal');

                 switchFields($businessFields, $personalFields);

                 $isBusinessType = false;
            
        }else{
                $($businessButton).addClass("acc-type__button--active");
                $($personalButton).removeClass("acc-type__button--active")

                $form.attr('form-type', 'business');

                switchFields($personalFields, $businessFields);

                $isBusinessType = true;
            }
    }

    function switchFields($fieldsToHide, $fieldsToShow){
        $form.fadeOut("fast", function() {
            $fieldsToHide.hide();
            $fieldsToShow.show();
                
            $form.fadeIn("slow");
        });
    }
    

});