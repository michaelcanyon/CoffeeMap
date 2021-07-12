($(function () {
    $("#hours").mask('AA-AA 00:00-00:00', { 'translation': { A: { pattern: /[а-я]/ } }, 0: { pattern: /[0-9]/ } });

    var latitudef, longitudef, hoursf, addressStrf;
    latitudef = longitudef = hoursf = addressStrf = false;

    var hours = document.getElementById("hours");
    var message = document.getElementById('hours_mes');
    message.innerHTML = "";
    message.className = "";
    hoursf = true;

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
            longitude.value = "";
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

    function checkFormState() {
        var button = document.getElementById('confirm_button');
        button.disabled = (hoursf &&
            latitudef &&
            longitudef &&
            addressStrf) == true ? false : true;
    }
})

)(jQuery);