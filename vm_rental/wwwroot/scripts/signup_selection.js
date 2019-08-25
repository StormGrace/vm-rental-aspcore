    $isBusinessByDefault = false;
    
    $form = $(".acc-form");
    $buttons = $(".acc-type__button");
    
    $checkbox = $(".is-business");

    $personalButton = $buttons.get(0);
    $businessButton = $buttons.get(1);

    $personalFields = $(".acc-form__field-section[section-type='personal']");
    $businessFields = $(".acc-form__field-section[section-type='business']");
    
    $isBusinessType = false;

    onTypeChange();
    
    $($checkbox).click(function(){
      return false;
    });

    $($buttons).click(function(){    
        if($(this).is($personalButton)){
            $isBusinessType = false;
        }
        else if($(this).is($businessButton)){
            $isBusinessType = true;
        }
        
        onTypeChange($isBusinessType)

        $checkbox.prop("checked", $isBusinessType);

        $('html,body').animate({scrollTop: $(this).offset().top}, 700);
    });

    function onTypeChange($isBusinessType){
            if(!$isBusinessType){
                $($personalButton).addClass("acc-type__button--active");
                $($businessButton).removeClass("acc-type__button--active")
           
                 $form.attr('form-type', 'personal');

                 switchFields($businessFields, $personalFields);

                
        }else{
                $($businessButton).addClass("acc-type__button--active");
                $($personalButton).removeClass("acc-type__button--active")
                $checkbox.toggle(true);
                $form.attr('form-type', 'business');

                switchFields($personalFields, $businessFields);
            }
    }

    function switchFields($fieldsToHide, $fieldsToShow){
        $form.fadeOut("fast", function() {
            $fieldsToHide.hide();
            $fieldsToShow.show();

            $form.fadeIn("slow");
        });
    }