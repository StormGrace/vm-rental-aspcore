$stateField = $("input[name='state']");
$cityField  = $("input[name='city']");

$country_code = $(".acc-form__country-code");  
 
$countriesData = countriesRepository.getByIndex();

$stateSearchBox = searchBox.createSearchBox($stateField, $countriesData, 'name', 5, 500);

 
