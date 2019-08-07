$(document).ready(function(){
    $maxResultLimit = 5; //5 by Default

    $states = [];
    $states_found = [];
    $stateSelected = null;

    $stateField = $("input[name='state']");
    $cityField = $("input[name='city']");
    $statesDropdown = $(".acc-form__field-states-dropdown");
    $country_code = $(".acc-form__country-code");   

    $jqxhr = $.getJSON("../countries.json", function(jsonData){
         $states = jsonData.countries;
    });

    $stateField.focusout(function(){
        $stateInput = $stateField.val();
        $stateRegex = new RegExp("^" + $stateInput + "$", "gi");

       for(i = 0; i < $states_found.length; i++){
             if($states_found[i].country.match($stateRegex)){
                $country_code.text($states_found[i].country_code);
                break;
             }
       }                    
       $($statesDropdown).hide();
    });

    $($stateField).keyup(function(){   
        setTimeout(function(){
            $foundResults = false;
            $stateInput = $stateField.val().trim().replace(/\s\s+/g, ' ');
            
            $states_found = [];
            $($statesDropdown).empty();

            if($stateInput.length > 0){
                $stateRegex = new RegExp("^" + $stateInput, "gi");

                 for(i = 0; i < $states.length; i++){
                     $state = $states[i];

                    if($state.country.match($stateRegex)){
                        $states_found.push($state);
                        $foundResults = true;
                    }
                 }          
                if($foundResults){
                    $($statesDropdown).show();

                    $resultCount = Math.min($states_found.length, $maxResultLimit);

                    for(i = 0; i < $resultCount; i++){
                        $statesDropdown.append("<li>" + $states_found[i].country + "</li>");          
                    }

                    $("li" ).mousedown(function(){
                         $stateSelected = $(this).text();  
                         $stateField.val($stateSelected);
                         console.log($states_found);
                         $country_code.text($states_found[$(this).index()].country_code);
                    });
                
                     if($states_found.length == 1 && $states_found[0].country.match(new RegExp("^" + $stateInput + "$", "gi"))){
                        $($statesDropdown).hide();
                     }
                }
                else{
                    $($statesDropdown).hide();
                }
            }
        }, 200);
    })
});