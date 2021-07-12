($(function () {
    var emailf, loginf;
    emailf = false;

    updateState();
    //mask email
    const emailRegExpr = /[@.a-z0-9]/;
    var mail = document.getElementById('email');
    mail.addEventListener('keypress', (e) => {
        var mailIn = document.getElementById('email');
        if (!emailRegExpr.test(e.key))
            e.preventDefault();
        else if ((e.key == '@' && mailIn.value.length < 1) ||
            (e.key == '.' && !mailIn.value.includes('@')) ||
            (mailIn.value.includes('@') && e.key == '@'))
            e.preventDefault();
        else if (mailIn.value.length > 1 &&
            mailIn.value.charAt(mailIn.value.length - 1) == '@' &&
            e.key == '.')
            e.preventDefault();
    });

    mail.addEventListener('focusout', (e) => {
        var email = document.getElementById('email');
        var message = document.getElementById('email_mes');
        message.innerHTML = "";
        message.className = "";
        emailf = true;

        if (!(email.value.includes('.') || email.value.includes('@'))) {
            longitude.value = "";
            message.innerHTML = "Некорректный формат email! Скорректируйте поле.";
            message.className = "alert alert-danger py-2";
            emailf = false;
        }
        checkFormState();
    });

    function checkFormState() {
        var button = document.getElementById('confirm_button');
        button.disabled = 
            emailf == true ? false : true;
    }

    function updateState() {

        var email = document.getElementById('email');
        var message = document.getElementById('email_mes');
        message.innerHTML = "";
        message.className = "";
        emailf = true;

        if (!(email.value.includes('.') || email.value.includes('@'))) {
            longitude.value = "";
            message.innerHTML = "Некорректный формат email! Скорректируйте поле.";
            message.className = "alert alert-danger py-2";
            emailf = false;
        }

        checkFormState();
    }

}))(jQuery);