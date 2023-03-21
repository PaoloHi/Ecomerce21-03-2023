// este Ajaax es para la alerta que dice ¡¡ Felicidades se guardo correctamente

$(document).ready(function () {
    var successMessage = '@TempData["SuccessMessage"]';
    if (successMessage) {
        Swal.fire(
            'Felicidades',
            successMessage,
            'success'
        );
    }
});






