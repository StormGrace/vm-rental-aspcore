var searchBox = {
        data: null,
        inputField: null,
        dropdown: null,
        searchParam: "",
        foundData: [],
        resultLimit: 5,
        inputDelay: 500, //For Performance Optimization.
        isInDelay: false,

        createSearchBox: function(field, repository, searchparam, maxresults, inputdelay){
            $(field).attr('autocomplete', 'off');
            $inputContainer = field.parent();
            $inputContainer.css('position', 'relative');

            inputField = field;
            data = repository;
            searchParam = searchparam;

            isInDelay = false;

            if(typeof(maxresults) !== typeof undefined){
                resultLimit = maxresults;   
            }else{ resultLimit = 5;}

            if(typeof(inputDelay) !== typeof undefined){
               inputDelay = inputdelay;
            }else{ inputDelay = 500;}

            dropdown = searchDropdown.createDropdown(inputField, ".dropdown");
            
            this.search();

            return this;
        },

        search: function(){
           $(inputField).on('keyup', function(e){
            if(isInDelay == false && (e.keyCode >= 65 && e.keyCode <= 90 || e.keyCode == 8 || e.keyCode == 32)){  
               isInDelay = true;
      
               setTimeout(function(){    
                    this.foundData = [];

                    $userInput = $(inputField).val().trim().replace(/\s\s+/g, ' ');
                
                    $partialMatch = new RegExp('^' + $userInput, "gi");
                    $fullMatch    = new RegExp('^' + $userInput + '$', "gi");
                    
                    $isFullMatch = false;

                   if($userInput.length > 0){
                        console.log($userInput);
            
                        for(i = 0; i < this.data.length; i++){
                            $dataItem = data[i];
                            console.log("hey");

                            if($dataItem[searchParam].match($fullMatch)){
                                 this.foundData = [];
                                 this.foundData.push($dataItem[searchParam]);
                                 $isFullMatch = true;
                                 break;
                            }
                            else if($dataItem[searchParam].match($partialMatch)){;
                                this.foundData.push($dataItem[searchParam]);
                            }
                        }        

                        if(this.foundData.length > this.resultLimit){
                            foundData = foundData.slice(0, this.resultLimit);
                        }
                    }

                    isInDelay = false;
                    dropdown.updateDropdown(foundData, $isFullMatch);

                 }, inputDelay);
              }
          });
      },
 };

let searchDropdown = {
        inputElement: null,
        dropdownElement: null,
        data: [],
        dropdownClass: 'dropdown',
        onActiveClass: 'active',
        isHidden: true,

        createDropdown: function(inputelement, dropdownclass){
            inputElement = inputelement;

            if(typeof(dropdownclass) == "String"){
                dropdownClass = dropdownclass;
            }

            $('<ul/>', {"class": this.dropdownClass}).insertAfter(inputElement);

            this.dropdownElement = $(dropdownclass);

            this.hideDropdown();

            return this;
        },

        updateDropdown: function(data, isFullMatch){
            this.data = data;

            //Room for Improvement 
            //(the newly found element might already exist, why delete it?)
            //Probably faster to Empty-All for small element count, instead of increasing the complexity.          
            $(this.dropdownElement).empty(); 
           
            for(i = 0; i < this.data.length; i++)
            {
                $(this.dropdownElement).append('<li>' + this.data[i] + '</li>');
            } 
            
            if((this.data.length == 0 || isFullMatch) && this.isHidden == false)
            {
                this.hideDropdown(); return;              
            }
            else if(this.data.length > 0 && this.isHidden)
            {
                this.showDropdown();
            }

            this.setClickListeners();
       },

       setClickListeners : function(callback){   
          var self = this;
   
          $(this.dropdownElement).children().on("click", function(callback) 
          {
               inputElement.val($(this).text());
               self.hideDropdown();

               if(typeof callback === "function")
               {
                  callback();
               }
           });        
       },   
   
       showDropdown: function(){
           this.isHidden = false;
           $(this.dropdownElement).show();
       },

       hideDropdown: function(){
           this.isHidden = true;
           $(this.dropdownElement).hide();
       },
};