

$("form").on('submit', () => {
    notifier.asyncBlock(new Promise((resolve, reject) => { }), null, null, 'Procesando');
});

jQuery.extend(jQuery.validator.messages, {
    required: "Este campo es requerido.",
    remote: "Please fix this field.",
    email: "Please enter a valid email address.",
    url: "Please enter a valid URL.",
    date: "Please enter a valid date.",
    dateISO: "Please enter a valid date (ISO).",
    number: "Please enter a valid number.",
    digits: "Please enter only digits.",
    creditcard: "Please enter a valid credit card number.",
    equalTo: "Please enter the same value again.",
    accept: "Please enter a value with a valid extension.",
    maxlength: jQuery.validator.format("Este campo admite solo hasta {0} carácteres."),
    minlength: jQuery.validator.format("Este campo necesita al menos {0} carácteres."),
    rangelength: jQuery.validator.format("Please enter a value between {0} and {1} characters long."),
    range: jQuery.validator.format("Please enter a value between {0} and {1}."),
    max: jQuery.validator.format("Please enter a value less than or equal to {0}."),
    min: jQuery.validator.format("Please enter a value greater than or equal to {0}.")
});




const ajaxPostSettings = {
    contentType: "application/json; charset=utf-8",
    type: "POST",
    dataType: 'json',
    timeout: 3000
};

function ajaxPOST(url, data, onSucess, onErrorCallback) {
    try {
        return new Promise((resolve, reject) => {
            $.ajax({
                ...ajaxPostSettings,
                url,
                data: JSON.stringify(data),
                success: (data) => {
                    try {
                        if (onSucess)
                            onSucess(JSON.parse(data.d));
                        resolve(JSON.parse(data.d));
                    } catch (e) {
                        reject(e)
                        notifier.alert(e.message);
                    }
                },
                error: (err) => {
                    if (onErrorCallback)
                        onErrorCallback(err);
                    let data, message;
                    if (err.responseJSON) {
                        data = err.responseJSON.d ? JSON.parse(err.responseJSON.d) : err.responseJSON;
                    } else {
                        if (!err.statusText)
                            data = JSON.parse(JSON.parse(err.responseText).d ? JSON.parse(err.responseText).d : JSON.parse(err.responseText));
                        else
                            data = { message: err.statusText };
                    }
                    message = data.message ? data.message : data.Message;
                    expired = data.expired ? data.expired : false;
                    if (expired) {
                        ModalMensajeExpired(message);
                        reject();
                    }
                    reject(message);
                }
            });
        });
    } catch (e) {
        notifier.alert(e.message);
    }
};


async function getCities(el, elCity) {
    try {
        const selected = el.value;
        if (selected > 0) {
            elCity.prop('disabled', true);
            let cities = await ajaxPOST('/Service.asmx/GetCitiesByProvince', { stateId: selected });
            elCity.html('<option value="0">Seleccione una ciudad</option>');

            cities.forEach(city => {
                elCity.append(`<option value="${city.Id}">${city.Name}</option>`);
            });
            elCity.prop('disabled', false);
        } else {
            elCity.html('<option value="0">Seleccione una provincia primero</option>');
        }
    } catch (e) {
        await Swal.fire({
            title: 'Oops!',
            icon: 'error',
            html:
                `Ocurrio un error al intentar obtener las ciudades de la provincia seleccionada.<br>
                Asegurese de estar conectado a internet.`,
            showCloseButton: true,
            showCancelButton: true,
            focusConfirm: false,
            preConfirm: () => {
                getCities(el, elCity);
            },
            confirmButtonText:
                '<i class="fas fa-sync"></i> Reintentar',
            cancelButtonText:
                'Cancelar'
        });
        //notifier.alert(e.message);
    }
}