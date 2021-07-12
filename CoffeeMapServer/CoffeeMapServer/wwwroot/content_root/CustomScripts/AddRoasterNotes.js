($(function () {

    $("#contactnumber").mask('+9-(999)-999-99-99');
    $("#contactPersonnumber").mask('+9-(999)-999-99-99');
    $("#hours").mask('AA-AA 00:00-00:00', { 'translation': { A: { pattern: /[а-я]/ } }, 0: { pattern: /[0-9]/ } });
    var titlef, phonef, emailf, priorityf, latitudef, longitudef, hoursf, addressStrf;
    titlef = phonef = emailf = priorityf = latitudef = longitudef = hoursf = addressStrf = false;

    function checkFormState() {
        var button = document.getElementById('confirm_button');
        button.disabled = (titlef &&
            phonef &&
            emailf &&
            priorityf &&
            hoursf &&
            latitudef &&
            longitudef &&
            addressStrf) == true ? false : true;
    }

    //mask contact name
    const contactNameRegExp = /[a-zA-ZА-Яа-я\s]/;
    var contactPerson = document.getElementById('contactPersonName');
    contactPerson.addEventListener('keypress', (e) => {
        if (!contactNameRegExp.test(e.key))
            e.preventDefault();
    });

    document.getElementById("contactnumber").addEventListener('focusout', (e) => {
        var number = document.getElementById("contactnumber");
        var message = document.getElementById('contactnumber_mes');
        message.innerHTML = "";
        message.className = "";
        phonef = true;

        if (number.value.length < 17) {
            message.innerHTML = "Некорректный контактный номер";
            message.className = "alert alert-danger py-2";
            phonef = false;
        }
        checkFormState();
    });

    document.getElementById("hours").addEventListener('focusout', (e) => {
        var hours = document.getElementById("hours");
        var message = document.getElementById('hours_mes');
        message.innerHTML = "";
        message.className = "";
        hoursf = true;

        if (hours.value.length < 17) {
            message.innerHTML = "Некорректный формат часов работы";
            message.className = "alert alert-danger py-2";
            hoursf = false;
        }
        checkFormState();
    });

    //mask title
    const titleRegExp = /[a-zA-ZА-Яа-я0-9\s]/;
    var title = document.getElementById('title');
    title.addEventListener('keypress', (e) => {
        if (!titleRegExp.test(e.key))
            e.preventDefault();
    });

    title.addEventListener('focusout', (e) => {
        var titleIn = document.getElementById('title');
        var message = document.getElementById('title_mes');
        message.innerHTML = "";
        message.className = "";
        titlef = true;

        if (titleIn.value.length < 1) {
            message.innerHTML = "Поле названия обжарщика не может быть пустым.";
            message.className = "alert alert-danger py-2";
            titlef = false;
        }
        checkFormState();
    });

    //web mask
    const webLinkRegExp = /[a-zA-ZА-Яа-я0-9.]/;
    var weblink = document.getElementById('web');
    weblink.addEventListener('keypress', (e) => {
        if (!webLinkRegExp.test(e.key))
            e.preventDefault();
    });

    //vk mask
    var vklink = document.getElementById('vklink');
    vklink.addEventListener('keypress', (e) => {
        if (!webLinkRegExp.test(e.key))
            e.preventDefault();
    });

    //ig mask
    var iglink = document.getElementById('iglink');
    iglink.addEventListener('keypress', (e) => {
        if (!webLinkRegExp.test(e.key))
            e.preventDefault();
    });

    //tg mask
    var tglink = document.getElementById('tglink');
    tglink.addEventListener('keypress', (e) => {
        if (!webLinkRegExp.test(e.key))
            e.preventDefault();
    });

    //mask priority
    const priorityRegExp = /[0-9]/;
    var priority = document.getElementById('priority');
    priority.addEventListener('keypress', (e) => {
        if (!priorityRegExp.test(e.key) || priority.value.length >= 2)
            e.preventDefault();
    });

    priority.addEventListener('focusout', (e) => {
        var priorityIn = document.getElementById('priority');
        var message = document.getElementById('priority_mes');
        message.innerHTML = "";
        message.className = "";
        priorityf = true;

        if (priorityIn.value.length < 1) {
            message.innerHTML = "Поле приоритета обязательно. Вы можете ввести значение от 1 до 99";
            message.className = "alert alert-danger py-2";
            priorityf = false;
        }
        checkFormState();
    });

    //mask coordinates
    const latitudeRegExp = /[-.0-9]/;
    var latitude = document.getElementById('latitude');
    var longitude = document.getElementById('longitude');

    latitude.addEventListener('keypress', (e) => {
        var latitude = document.getElementById('latitude');
        maskCoord(e, latitude);

    });

    longitude.addEventListener('keypress', (e) => {
        var longitude = document.getElementById('longitude');
        maskCoord(e, longitude);
    });

    latitude.addEventListener('focusout', (e) => {
        var latitude = document.getElementById('latitude');
        var message = document.getElementById('latitude_mes');
        message.innerHTML = "";
        message.className = "";
        latitudef = true;

        if (parseFloat(latitude.value) < -85 ||
            parseFloat(latitude.value) > 85 ||
            latitude.value == '' ||
            latitude.value == null) {
            latitude.value = "";
            message.innerHTML = "Неверные координаты. Допустимые пределы широты: -85 до 85!";
            message.className = "alert alert-danger py-2";
            latitudef = false;
        }
        checkFormState();
    });

    longitude.addEventListener('focusout', (e) => {
        var longitude = document.getElementById('longitude');
        var message = document.getElementById('longitude_mes');
        message.innerHTML = "";
        message.className = "";
        longitudef = true;

        if (parseFloat(longitude.value) < -180 ||
            parseFloat(longitude.value) > 180 ||
            longitude.value == '' ||
            longitude.value == null) {
            longitude.value = "";
            message.innerHTML = "Неверные координаты. Допустимые пределы долготы: -180 до 180!";
            message.className = "alert alert-danger py-2";
            longitudef = false;
        }
        checkFormState();
    });

    function maskCoord(e, elem) {
        if (!latitudeRegExp.test(e.key)) {
            e.preventDefault();
            return;
        }
        else if ((elem.value.includes('-') && e.key == '-')
            || (elem.value.includes('.') && e.key == '.')
            || (elem.value.length < 2 && elem.value.includes('-') && e.key == '.')
            || (elem.value.length > 1 && e.key == '-')
            || (elem.value.length < 1 && e.key == '.')
        ) {
            e.preventDefault();
        }
    }


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
            message.innerHTML = "Некорректный формат email! Скорректируйте поле.";
            message.className = "alert alert-danger py-2";
            emailf = false;
        }
        checkFormState();
    });

    const addressRegExpr = /[.,-A-Za-zА-Яа-я0-9\s]/;
    var address = document.getElementById('addressStr');
    address.addEventListener('keypress', (e) => {
        if (!addressRegExpr.test(e.key))
            e.preventDefault();
    });

    address.addEventListener('focusout', (e) => {
        var addressIn = document.getElementById('addressStr');
        var message = document.getElementById('addressStr_mes');
        message.innerHTML = "";
        message.className = "";
        var warn = document.getElementById('address_waring_mes');
        warn.innerHTML = "";
        warn.className = "";
        addressStrf = true;

        if (addressIn.value.length < 10) {
            message.innerHTML = "Слишком короткий адрес. Длина адреса должна быть не менее 10 символов";
            message.className = "alert alert-danger py-2";
            addressStrf = false;
        }
        else {
            var val = addressIn.value;
            $.get("/Admin/CheckAddresses/Check", { address: val })
                .done((data) => {
                    if (data == true) {
                        warn.innerHTML ='Внимание! В базе данных обнаружен адрес с совпадающим значением.' +
                            'Рекомендуется уточнить адрес.';
                        warn.className = "alert alert-warning py-2";
                    }
                });
        }
        checkFormState();
    });
})

)(jQuery);