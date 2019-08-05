$(document).ready(function () {
    var minChar = 8;
    var maxChar = 30;
    var maxName = 50;
    var maxPhone = 10;

   
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



    $("#sign-up-form-personal").validate({
        errorElement: 'span',
        errorClass: "fielderror",
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
            address: {
                required: true,
            },
            phone: {
                required: true,
                maxlength: maxPhone,
            }

        },

        errorPlacement: function (error, element) {
            var name = $(element).attr("name");
            if (name === "email" || name === "password" || name == "phone") {
                error.appendTo(element.next());
            }
            else {
                error.insertAfter(element);
            }

        },

        messages: {
            password: {
                validate_password: "Password must contain 0-9, a-z, A-Z and _characters only."
            },
            phone: {
                required: "Phone number isn't Specified."
            },
            firstname: {
                required: ""
            },
            lastname: {
                required: ""
            },
            state: {
                required: ""
            },
            city:{
                required:""
            },
            address: {
                required:""
            }
        }
    });

});