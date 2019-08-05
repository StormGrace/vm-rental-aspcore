$(document).ready(function () {
    var minChar = 8;
    var maxChar = 30;
    var maxName = 50;
    var maxPhone = 9;

    // Валидация за емайл
    $('input[name=email]').keypress(function (event) {
        var ew = event.which;
        if (ew == 32)
            return true;
        if (ew == 46)
            return true;
        if (48 <= ew && ew <= 57)
            return true;
        if (64 <= ew && ew <= 90)
            return true;
        if (97 <= ew && ew <= 122)
            return true;
        return false;
    });

    //Валидация за потребителско име 
    $('input[name=username]').keypress(function (event) {
        var ew = event.which;
        if (48 <= ew && ew <= 57)
            return true;
        if (64 <= ew && ew <= 90)
            return true;
        if (97 <= ew && ew <= 122)
            return true;
        return false;
    });

    //Валидация за името на фирмата
    $('input[name=firmname]').keypress(function (event) {
        var ew = event.which;
        if (64 <= ew && ew <= 90)
            return true;
        if (97 <= ew && ew <= 122)
            return true;
        return false;
    });

    //Валидация за име 
    $('input[name=firstname]').keypress(function (event) {
        var ew = event.which;
        if (64 <= ew && ew <= 90)
            return true;
        if (97 <= ew && ew <= 122)
            return true;
        return false;

    });

    //Валидация за фамилно име
    $('input[name=lastname]').keypress(function (event) {
        var ew = event.which;
        if (64 <= ew && ew <= 90)
            return true;
        if (97 <= ew && ew <= 122)
            return true;
        return false;

    });

    // Валидация за паролата
    $('input[name=password]').keypress(function (event) {
        var ew = event.which;
        if (32 <= ew && ew <=47)
            return true;
        if (48 <= ew && ew <= 57)
            return true;
        if (65 <= ew && ew <= 90)
            return true;
        if (97 <= ew && ew <= 122)
            return true;
        return false;
    });

    // Валидация за телефон само цифри
    $('input[name=phone]').keypress(function (event) {
        var ew = event.which;
        if (48 <= ew && ew <= 57)
            return true;
        else
            return false;
    });


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


    // добавен метод за емайл
    jQuery.validator.addMethod("validate_email", function (value) {

        if (/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/.test(value)) {
            return true;
        } else {
            return false;
        }
    }, "Please enter a valid Email.");

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
                validate_email: true
            },
            password: {
                validate_password: true,
                minlength: minChar,
                maxlength: maxChar
            },
            username: {
                required: true,
                maxlength: maxName
            },
            firstname: {
                required: true,
                maxlength: maxName
            },
            lastname: {
                required: true,
                maxlength: maxName
            },
            state: {
                required: true,
            },
            city: {
                required: true,
            },
            firmname: {
                required: true,
                maxlength: maxName
            },
            firmemail: {
                validate_email:true
            },
            phone: {
                required: true,
                minlength: maxPhone,
                maxlength: maxPhone
            }
        },

        errorPlacement: function (error, element) {
            var name = $(element).attr("name");
            if (name == "email" || name == "password" || name == "username") {
                error.appendTo(element.parent().next());
            }
            else if (name == "firstname" || name=="lastname" || name=="firmname" || name=="firmemail" || name=="phone" || name=="state" || name=="city") {
                error.appendTo(element.parents().find(".acc-form__field-msg-personal"));
            }
            else {
                error.insertAfter(element);
            }

        },

        messages: {
            password: {
                validate_password: "Password must be at least 8 characters long and contain at least one 0-9, a-z, A-Z and special characters."
            },
            phone: {
                required: "Phone number isn't Specified.",
                minlength: "Phone number must contain 9 characters.",
                maxlength: "Phone number is not correct."
            },
            username: {
                required: "Please enter a username."
            },
            firstname: {
                required: "Please enter firstname."
            },
            lastname: {
                required: "Please enter lastname."
            },
            firmname: {
                required:"Please enter firmname."
            },
            firmemail: {
                required:"Please enter firm-email."
            },
            state: {
                required: "Please enter state."
            },
            city:{
                required:"Please enter city."
            },
            email: {
                required:"Please enter email."
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