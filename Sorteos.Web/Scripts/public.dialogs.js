
const notifier = new AWN({ timeout: 10000, labels: { info: "Info", sucess: "Exito!", warning:"Atención!"} });

function success(message, title,footer) {
    Swal.fire(
        {
            icon: 'success',
            title: title || 'Exito!',
            text: message || 'La acción se completo con éxito',
            footer
        }
    )
}

function warning(message, title, footer) {
    Swal.fire(
        {
            icon: 'warning',
            title: title || 'Atención!',
            text: message || 'Revise los datos ingresados',
            footer
        }
    );
}

function error(message, title, footer) {
    Swal.fire({
        icon: 'error',
        title: title || 'Oops...',
        text: message || 'Algo salio mal..',
        footer
    })
}

