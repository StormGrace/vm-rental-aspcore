//Search Box - v1.0
var searchBox = {
  data: [],
  inputField: {},
  dropdown: {},
  searchParam: "",
  returnParam: "",
  foundData: [],
  selected: {},
  resultLimit: 5,
  inputDelay: 500, //For Performance Optimization.
  onFullMatchCallback: null,
  onFocusOutCallback: null,
  isFullMatch: false,

  createSearchBox: function(
    field,
    repository,
    searchparam,
    returnparam,
    dropdownclass,
    maxresults,
    inputdelay
  ){
    $inputContainer = field.parent();
    $inputContainer.css('position', 'relative');

    this.inputField = field;
    this.data = repository;
    this.searchParam = searchparam;
    this.returnParam = returnparam;
    
       isInDelay = false;

    if (typeof maxresults !== typeof undefined) {
      this.resultLimit = maxresults;
    }

    if (typeof inputdelay !== typeof undefined) {
      this.inputDelay = inputdelay;
    }

    this.dropdown = searchDropdown.createDropdown(
      this,
      this.inputField,
      dropdownclass,
    );

    this.onSearch();
    this.onFocusOut();

    return this;
  },

  onSearch: function() {
    var self = this;

    $(this.inputField).on("keyup", function(e) {
      if (isInDelay == false &&
          (!e.ctrlKey && !e.altKey && (e.keyCode >= 65 && e.keyCode <= 90)) ||
           e.keyCode == 8 || e.keyCode == 32 
      ){
        isInDelay = true;

        setTimeout(() =>{
          self.foundData = []; $filteredData = [];

          $userInput = $(self.inputField)
            .val()
            .trim()
            .replace(/\s\s+/g, " ");

          $partialMatch = new RegExp('^' + $userInput, 'gi');
             $fullMatch = new RegExp('^' + $userInput + '$', 'gi');

          if ($userInput.length > 0) {
            for (i = 0; i < self.data.length; i++) {
              $dataItem = self.data[i];
              $filteredItem = $dataItem[self.searchParam];

              if ($fullMatch.test($filteredItem)) {
                self.isFullMatch = true;
                
                self.foundData = []; $filteredData = [];
               
                self.inputField.val($filteredItem);

                self.foundData.push($dataItem);
                 $filteredData.push($filteredItem);

                //console.log("in Search");
                self.selected = $dataItem;
                self.onFullMatch();     
                break;

              } else if ($partialMatch.test($filteredItem)) {
                 self.isFullMatch = false;

                 self.foundData.push($dataItem);
                  $filteredData.push($filteredItem);              
              }
            }

            if ($filteredData.length > self.resultLimit) {
              $filteredData = $filteredData.slice(0, self.resultLimit);
            }
          }
        
          self.dropdown.updateDropdown($filteredData, self.isFullMatch);
          isInDelay = false;

        }, self.inputDelay);
      }
    });
  },

  onFocusOut: function() {
    var self = this;

    $(this.inputField).focusout(function() {
      self.dropdown.hideDropdown();

      self.fireCallback(this.onFocusOutCallback);
    });
  },
 
 onFullMatch: function() {
      this.fireCallback(this.onFullMatchCallback);
  },

 fireCallback: function (callback) {
    if (typeof callback === "function") {
         setTimeout(() => {callback(this.selected[this.returnParam]);}, 0);
     }
  },
};

var searchDropdown = {
  searchBox: {},
  inputElement: {},
  dropdownElement: {},
  data: [],
  dropdownClass: "dropdown",
  onActiveClass: "active",
  isHidden: true,

  createDropdown: function(searchbox, inputelement, dropdownclass) {
    this.searchBox = searchbox;
    this.inputElement = inputelement;

    if (typeof dropdownclass !== typeof undefined) {
      this.dropdownClass = dropdownclass;
    }

    $("<ul/>", { class: this.dropdownClass + " dropdown-search"}).insertAfter(this.inputElement);

    this.dropdownElement = $("." + this.dropdownClass);

    this.hideDropdown();

    return this;
  },

  updateDropdown: function(data, isFullMatch) {
    this.data = data;

    //Room for Optimization
    //(the newly found element might already exist, why delete all to add it again?)
    //Probably faster this way for small results.
    $(this.dropdownElement).empty();

    for (i = 0; i < this.data.length; i++) {
      $(this.dropdownElement).append("<li>" + this.data[i] + "</li>");
    }

    if (this.data.length == 0 || isFullMatch) {
      this.hideDropdown();
      return;
    } else if (this.data.length > 0 && !isFullMatch) {
      this.showDropdown();
    }

    this.setClickListeners();
  },

  setClickListeners: function() {
    var self = this;

    $(this.dropdownElement)
      .children()
      .mousedown(function() {
        self.inputElement.val($(this).text());
        $itemSelected = self.searchBox.foundData[$(this).index()];
        self.searchBox.selected = $itemSelected;
        self.searchBox.onFullMatch();
        self.hideDropdown();
      });
  },

  showDropdown: function() {
    if (this.isHidden == true) {
      this.isHidden = false;
      $(this.dropdownElement).show();
    }
  },

  hideDropdown: function() {
    if (this.isHidden == false) {
      this.isHidden = true;
      $(this.dropdownElement).hide();
    }
  }
};