const countriesRepository = {
      countries: { byIndex: [], byName: [], byCode2: [] },
      
      initialize : function(jsonDataCountries){
        if(jsonDataCountries){        
            for(i = 0; i < jsonDataCountries.length; i++){

                $country = jsonDataCountries[i];    
        
                $countrybyName  = {code2: $country.code2, country_code: $country.country_code, lang: $country.lang, lang_native: $country.lang_native};
                $countrybyCode2 = {name:  $country.name,  country_code: $country.country_code, lang: $country.lang, lang_native: $country.lang_native};

                this.countries.byIndex.push($country);
                this.countries.byName[$country.name] = $countrybyName;
                this.countries.byCode2[$country.code2] = $countrybyCode2;
            }
        }else{ throw "No JSON data Provided for Country Repository."; }
      },
      
      getByIndex : function(){
        return this.countries.byIndex;
      },

      getByName : function(){
        return this.countries.byName;
      },

      getByCode2 : function(){
        return this.countries.byCode2;
      },

      getAll : function(opts){
        if(this.countries !== "undefined" && this.countries.length > 0){
           switch(opts){
              case 'byIndex' : return this.countries.byIndex; break;
              case 'byName'  : return this.countries.byName; break;
              case 'byCode2' : return this.countries.byCode2; break;
           }
        }
        else{throw "No Countries Exist!";}
      },
    };

 //Request the Countries-JSON.
 $jqxhr = $.getJSON("../countries-main.json", function(jsonData){
     countriesRepository.initialize(jsonData.countries);
 });
