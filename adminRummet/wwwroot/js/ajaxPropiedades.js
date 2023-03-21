

//Este Ajax es para la parte del verificar el propietario 
$(function () {
    $('#btncorreo').click(function () {
        var url = '/Propiedades/Verificar';
        var correo = $('#email').val(); 
        if (correo !== null) {

            $('#emptyemail').addClass('d-none');

            console.log(correo)
            var data = { correoPropietario: correo };

            $.post(url, data).done(function (data) {
                if (data.success) // verifica el valor devuelto por el controlador
                {
                    console.log(data)
                    console.log(data.verificarCorreo)
                    var oPropietario = data.verificarCorreo;

                    console.log(data.verifCorreo);

                    //$('#datosPropietarioNoExistente').removeClass('d-none');
                    $('#labelNoVerif').addClass('d-none');

                    $('#datosPropietarioNoExistente').removeClass('d-none');
                    $('#labelVerif').removeClass('d-none');

                    $('#nombre').addClass('no-pointer-events');
                    $('#nombre').val(oPropietario.nombre);

                    $('#apellidoPaterno').addClass('no-pointer-events');
                    $('#apellidoPaterno').val(oPropietario.apellidoPaterno);
                    //$('#a_paterno').addClass('disabled')

                    $('#apellidoMaterno').addClass('no-pointer-events');
                    $('#apellidoMaterno').val(oPropietario.apellidoMaterno);
                    //$('#a_materno').addClass('disabled')

                    $('#telefono').addClass('no-pointer-events');
                    $('#telefono').val(oPropietario.telefono)
                    //$('#telefono').addClass('disabled')

                    console.log("se logró el AJAX");
                }
                else {
                    $('#labelVerif').addClass('d-none');

                    $('#nombre').val("");
                    //$('#nombre').prop('required', true);

                    $('#apellidoMaterno').val("");
                    //$('#apellidoMaterno').prop('required', true);

                    $('#apellidoPaterno').val("")
                    //$('#apellidoPaterno').prop('required', true);

                    $('#telefono').val("")
                    //$('#telefono').prop('required', true);

                    $('#datosPropietarioNoExistente').removeClass('d-none');
                    $('#labelNoVerif').removeClass('d-none');
                    console.log(data.message); // muestra el mensaje de error si el proceso no tuvo éxito

                    $('#inpGuardarPropietario').removeClass('d-none');

                }
            }).fail(function () {
                console.log("Error: no se pudo completar el AJAX");

            });


        }
        else {
            $('#emptyemail').removeClass('d-none');

        }

    });
});

// Esta es la parete de litar Colonias
   

$("#CP").on("change", function () {
    console.log("esta corriendo el onchange")

    var url = '/Propiedades/ListarColonias';

    var CP = $('#CP').val() || 10640;

    $("#CP").val(CP)
    console.log(CP)
    if (CP !== null) {
        var data = { CodigoPostal: CP }

        $.post(url, data).done(function (data) {
            console.log(data);

            if (data.legnth === 0) {
                $('#CPInvalid').removeClass('d-none')
            } else (
                $('#CPInvalid').addClass('d-none')
            )
            

            var $select = $('<select>');
            $select.append($('<option>').val('').text('Seleccione una colonia')); // Agrega una opción en blanco
            $.each(data, function (i, item) {
                $select.append($('<option>').val(item.idColonia).text(item.colonia));
                $('#IDcolonia').val(item.idColonia)
            });



            $('#Colonia').html($select.html()); // Actualiza el contenido del dropdownlist existente



            return data
        });
    }
});


$('#Colonia').on('change', function () {
    var selectedValue = $(this).val();

    console.log("esta es la " + selectedValue);
    idColonia = $('#IDColonia').val(selectedValue); 

    coloniaMaps = $('#IDcolonia').val(selectedValue);
});


//esta funcion es para que traiga las colonias relacionadas en editar sin necesidad de un evento 

function verificarMunicipio() {
    

    var url = '/Propiedades/ListarColonias';

    var coloniaGuardada = $('#colonia').val()
    console.log("esta es la colonia guardada" + coloniaGuardada)

    var CP = $('#CP').val() || 00000;

    $("#CP").val(CP)
    console.log(CP)
    if (CP !== null) {
        var data = { CodigoPostal: CP }

        $.post(url, data).done(function (data) {
            console.log(data);

            if (data.legnth === 0) {
                $('#CPInvalid').removeClass('d-none')
            } else (
                $('#CPInvalid').addClass('d-none')
            )


            var $select = $('<select>');
            $select.append($('<option>').val('').text(coloniaGuardada)); // Agrega una opción en blanco
            $.each(data, function (i, item) {
                $select.append($('<option>').val(item.idColonia).text(item.colonia));
                $('#IDcolonia').val(item.idColonia)
            });


            $('#Colonia').html($select.html()); // Actualiza el contenido del dropdownlist existente



            return data
        });
    };
};

// Llamar a la función verificarMunicipio en cualquier momento que sea necesario
verificarMunicipio();



//Esta seccion es para detalles de propiedad




