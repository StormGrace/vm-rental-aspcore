$(document).ready(function () {
    var minChar = 8;
    var minName = 4;
    var maxChar = 100;
    var maxName = 20;
    var maxPhone = 9;
    var maxEmail = 254;

  
    //Валидация за име започва с главна буква
    $('input[name = firstname]').on('keydown', function (event) {
        if (this.selectionStart == 0 && event.keyCode >= 65 && event.keyCode <= 90 && !(event.shiftKey) && !(event.ctrlKey) && !(event.metaKey) && !(event.altKey)) {
            var $t = $(this);
            event.preventDefault();
            var char = String.fromCharCode(event.keyCode);
            $t.val(char + $t.val().slice(this.selectionEnd));
            this.setSelectionRange(1, 1);
        }
    });

    //Валидация за име на фирма -> започва с главна буква
    $('input[name = firmname]').on('keydown', function (event) {
        if (this.selectionStart == 0 && event.keyCode >= 65 && event.keyCode <= 90 && !(event.shiftKey) && !(event.ctrlKey) && !(event.metaKey) && !(event.altKey)) {
            var $t = $(this);
            event.preventDefault();
            var char = String.fromCharCode(event.keyCode);
            $t.val(char + $t.val().slice(this.selectionEnd));
            this.setSelectionRange(1, 1);
        }
    });

    //Валидация за фамилно име започва с главна буква
    $('input[name = lastname]').on('keydown', function (event) {
        if (this.selectionStart == 0 && event.keyCode >= 65 && event.keyCode <= 90 && !(event.shiftKey) && !(event.ctrlKey) && !(event.metaKey) && !(event.altKey)) {
            var $t = $(this);
            event.preventDefault();
            var char = String.fromCharCode(event.keyCode);
            $t.val(char + $t.val().slice(this.selectionEnd));
            this.setSelectionRange(1, 1);
        }
    });
    //добавен метод за валидация за емайл
    jQuery.validator.addMethod("usernamevalidation", function (value) {
        if (/^[a-zA-Z0-9]+(?:[ _-][A-Za-z0-9]+)*$/.test(value)) {
           return true;
        }
        else { return false;}
    }, "Only Latin, hyphens and underscores are supported.");


    //добавен метод за валидация за емайл
    jQuery.validator.addMethod("characters_only", function (value) {
        if (/^[a-zA-Z]+$/.test(value)) {
            return true;
        }
        else { return false; }
    }, "Оnly Latin characters.");

    //добавен метод за валидация за емайл
    jQuery.validator.addMethod("numbers_only", function (value) {
        if (/^[0-9]+$/.test(value)) {
            return true;
        }
        else { return false; }
    }, "Phone Number should contain only numbers.");



    // добавен метод за емайл
    jQuery.validator.addMethod("validate_email", function (value) {

        if (/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/.test(value)) {
            return true;
        } else {
            return false;
        }
    }, "Please enter a valid Email Address.");

    // добавен метод за паролата
    jQuery.validator.addMethod("validate_password", function (value) {

        if (/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/.test(value)) {
            return true;
        } else {
            return false;
        }
    }, "Please enter a valid Password.");



    $('form[form-type=personal]').validate({
        errorElement: 'span',
        errorClass: 'has-error',
        ignoreTitle: true,

        rules: {
            email: {
                validate_email: true,
                required: {
                    depends: function () {
                        $(this).val($.trim($(this).val()));
                        return true;
                    }
                },
                maxlength: maxEmail
            },
            password: {
                required: {
                    depends: function () {
                        $(this).val($.trim($(this).val()));
                        return true;
                    }
                },
                validate_password: true,
                minlength: minChar,
                maxlength: maxChar
            },
            username: {
                required: {
                    depends: function () {
                        $(this).val($.trim($(this).val()));
                        return true;
                    }
                },
                maxlength: maxName,
                minlength: minName,
                usernamevalidation: true,

            },
            firstname: {
                required: {
                    depends: function () {
                        $(this).val($.trim($(this).val()));
                        return true;
                    }
                },
                maxlength: maxName,
                characters_only:true
            },
            lastname: {
                required: {
                    depends: function () {
                        $(this).val($.trim($(this).val()));
                        return true;
                    }
                },
                maxlength: maxName,
                 characters_only: true
            },
            state: {
                required: {
                    depends: function () {
                        $(this).val($.trim($(this).val()));
                        return true;
                    }
                },
                characters_only: true
            },
            city: {
                required: {
                    depends: function () {
                        $(this).val($.trim($(this).val()));
                        return true;
                    }
                },
                characters_only: true
            },
            firmname: {
                required: {
                    depends: function () {
                        $(this).val($.trim($(this).val()));
                        return true;
                    }
                },
                maxlength: maxName,
                characters_only: true
            },
            phone: {
                required: {
                    depends: function () {
                        $(this).val($.trim($(this).val()));
                        return true;
                    }
                },
                maxlength: maxPhone,
                numbers_only:true
            }
        },

        errorPlacement: function (error, element) {
            var name = $(element).attr("name");

            if ($(element).hasClass("has-error")) {
                $(element).toggleClass("acc-form__field--error");
                $(element.next()).show();
            } else {
                $(element).removeClass("has-error");
                $(element).removeClass("acc-form__field--error");
                $(element.parent().next()).hide();
            }    



            if (name == "email" || name == "password" || name == "username") {
                error.appendTo(element.parent().next());
            }
            else if (name == "firstname") {
                error.appendTo($(".acc-form__field-msg-firstname"));
            }
            else if (name == "lastname") {
                error.appendTo($(".acc-form__field-msg-lastname"));
            }
            else if (name == "state")
            {
                error.appendTo($(".acc-form__field-msg-state"));
            }
            else if (name == "city")
            {
                error.appendTo($(".acc-form__field-msg-city"));
            }
            else if (name == "phone")
            {
                error.appendTo($(".acc-form__field-msg-phone"));
            }
            else if (name == "firmname")
            {
                error.appendTo($(".acc-form__field-msg-firmname"));
            }
            else {
                error.insertAfter(element);
            }
            
        },

        messages: {
            password: {
                validate_password: "Password must be at least 8 characters long and contain at least one 0-9, a-z, A-Z and special characters.",
                maxlength: "Password is too long.",
                minlength: "Password can't be shorter than 8 characters."
            },
            phone: {
                required: "Please enter your Phone.",
                maxlength: "Phone number must contain 9 characters."
            },
            username: {
                required: "Please enter your Username.",
                minlength: "Username must contain at least {0} characters."
            },
            firstname: {
                required: "Please enter your First Name."
            },
            lastname: {
                required: "Please enter your Last Name."
            },
            firmname: {
                required:"Please enter your Business Name."
            },
            state: {
                required: "Please enter a State."
            },
            city:{
                required:"Please enter a City."
            },
            email: {
                required:"Please enter an Email Address."
            }

        }
    });

    var validator = $('form[form-type=personal]').validate();

    $(".acc-type__button--personal").click(function () {
        validator.resetForm();       //Рестартира формата, но не оправя проблема с дупликирането на еррор съобщенията.
        $(".has-error").remove();    //Премахва зададения клас във кода на ред 152 за да не позволи дупликирането на еррор съобшенията
    });

    $(".acc-type__button--business").click(function () {
        validator.resetForm(); 
        $(".has-error").remove();
    });


});