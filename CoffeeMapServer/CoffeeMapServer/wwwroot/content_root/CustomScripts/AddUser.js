($(function () {
    var emailf = false;
    var loginf=false;

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

    const loginRegExpr = /[a-zA-Z0-9]/;
    var login = document.getElementById('login');
    login.addEventListener('keypress', (e) => {
        var loginIn = document.getElementById('login');
        if (!loginRegExpr.test(e.key))
            e.preventDefault();
    });

    login.addEventListener('focusout', (e) => {
        var loginIn = document.getElementById('login');
        var message = document.getElementById('login_mes');
        message.innerHTML = "";
        message.className = "";
        loginf = true;

        if (loginIn.value.length < 5) {
            loginIn.value = "";
            message.innerHTML = "Некорректный формат Логина! Скорректируйте поле.";
            message.className = "alert alert-danger py-2";
            loginf = false;
        }
        checkFormState();
    });

    function checkFormState() {
        var button = document.getElementById('confirm_button');
        button.disabled = (
            loginf &&
            emailf) == true ? false : true;
    }

}))(jQuery);