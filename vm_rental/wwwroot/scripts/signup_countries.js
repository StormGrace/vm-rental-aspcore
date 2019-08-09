$stateField = $("input[name='state']");
$cityField  = $("input[name='city']");

$country_code = $(".acc-form__country-code");  
 
$countriesData = countriesRepository.getByIndex();

$stateSearchBox = searchBox.createSearchBox($stateField, $countriesData, 'name', 'country_code', 'acc-form__field-state-dropdown', 5, 500);

$stateSearchBox.onFullMatchCallback = setLabel;

function setLabel(data){
    $($country_code).text(data);
}

 
