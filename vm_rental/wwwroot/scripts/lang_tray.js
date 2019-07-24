$( document ).ready(function() {
    $isDropdownActive = false;
    $langBar = $('.lang-bar');
    $langCode2 = $('.code2');
    $dropDownElement = $('.lang-dropdown');
    $languageList = $("li[code2]");
    $selectedLangElement = null;

    $("li[code2]").click(function(){
        onLanguageSelect($(this));
    });

    $(document).mouseup(function (e){
        if(!$(".lang-bar span").is(e.target)){
            setDropdownActivity(false);
        }
    });

    $(".lang-bar span").click(function(){
        setDropdownActivity($isDropdownActive = !$isDropdownActive)
    }) 
});

function onLanguageSelect($targetLanguage){
    $langCode2.attr({
        'title': $targetLanguage.children().html(),
        'target-lang': $targetLanguage.attr('code2')
    });

    $langCode2.html($($targetLanguage).attr('code2'));
}

function setDropdownActivity($isActive){
    $isDropdownActive = $isActive;
    $langBar.attr('is-dropdown-active', $isActive.toString());
}