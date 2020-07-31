
Array.prototype.shuffle = function () {
    let counter = this.length, temp, index;
    while (counter > 0) {
        index = (Math.random() * counter--) | 0;
        temp = this[counter];
        this[counter] = this[index];
        this[index] = temp;
    }
}

function randomEx(min, max) {
    return Math.floor(Math.random() * (max - min + 1) + min);
}

// set active nav item
$(function () {
    const current = location.pathname;
    $('ul.metismenu li a').each(function () {
        if (current === '/')
            if ($(this).attr('href') === current)
                $(this).closest('li').addClass('active');
            else
                return;


        if ($(this).attr('href').indexOf(current) !== -1) {
            $(this).closest('li').addClass('active');
        }
    });
});

async function copiarAlPortapapeles(str, alertText) {
    const promise = new Promise(function (resolve, reject) {
        var success = false;
        function listener(e) {
            e.clipboardData.setData("text/plain", str);
            e.preventDefault();
            success = true;
        }
        document.addEventListener("copy", listener);
        document.execCommand("copy");
        document.removeEventListener("copy", listener);
        success ? resolve(true) : reject(false);
    });

    if (await promise())
        notifier.success(alertText);
};


const ajaxPostSettings = {
    contentType: "application/json; charset=utf-8",
    type: "POST",
    dataType: 'json',
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

function throwImageModal(imgSrc, alt) {
    let modal = document.getElementById("modal-image");
    let modalImg = document.getElementById("img-modal");
    let captionText = document.getElementById("caption");
    modal.style.display = "block";
    modalImg.src = imgSrc;
    captionText.innerHTML = alt;
    let span = document.getElementsByClassName("close")[0];
    span.onclick = function () {
        modal.style.display = "none";
    }
}



async function getCities(el,elCity) {
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
        notifier.alert(e.message);
    }
}