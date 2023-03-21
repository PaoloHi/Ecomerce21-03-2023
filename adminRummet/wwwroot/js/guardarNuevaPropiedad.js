console.log("Esta corriendo la linea de guardar Propiedad")

//Alerta Para eleiminar imagenes 
function eliminarimg(IDimg) {
    console.log(IDimg);

    Swal.fire({
        title: '¿Está seguro que desea eliminar esta imagen?',
        text: "Esta acción es irreversible.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            //Función para eliminar
            $.ajax({
                type: 'POST',
                url: '/Propiedades/eliminarImagen',
                data: { IDimgPropiedad: IDimg },
                success: function (result) {
                    Swal.fire(
                        'Imagen Eliminada',
                        'La imagen fue eliminada con éxito.',
                        'success'
                    ).then(function () {
                        //location.reload();
                    })

                },
                error: function () {
                    alert('Failed to receive the Data');
                    console.log('Failed ');
                }
            })

        } else {
            location.reload();
        }

    })
}


$(document).ready(function () {
    $("#TipoPropiedadID").change(function () {
        $.get("/Propiedades/ObtSubtipoProp", { TipoP: $("#TipoPropiedadID").val() }, function (data) {
            // VACIAMOS EL DropDownList
            $("#SubTipoPropiedadID").empty();
            $("#SubTipoPropiedadID").append("<option value> -- Subtipo --</option>")
            // CONSTRUIMOS EL DropDownList A PARTIR DEL RESULTADO Json (data)
            $.each(data, function (index, row) {
                console.log(data);
                $("#SubTipoPropiedadID").append("<option value='" + row.iDsubtipoP + "'>" + row.subtipo + "</option>")
            });
        });
    });
});

$(function () {
    $("#CP, #n_calle, #n_interior, #n_exterior, #colonia, #estado, #alc-muni, #pais").on('change', function () {
        initMap(document.getElementById("txtLat").value, document.getElementById("txtLng").value);
    })
});

$(document).on("change", "#add-new-photo", function () {

    //console.log(this.files);
    var filesImg = this.files;
    var element;
    var activo;
    var supportedImages = ["image/jpeg", "image/png", "image/gif", "image/webp"];
    var seEncontraronElementoNoValidos = false;

    // Limpiando y borrando, por si se agregan nuevas imagenes
    document.getElementById("listaImagenes").innerHTML = '';
    document.getElementById("lista_imgMini").innerHTML = '';
    for (var i = 0; i < filesImg.length; i++) {
        //if (i < 3) {
        //    document.getElementById("imgPrevisualizacion" + i).removeAttribute("src");
        //}
        if (i < 2) {
            document.getElementById("imgDespuesCarousel" + i).removeAttribute("src");
        }
    }

    // Creacion de elementos para el MiniCarousel
    if (filesImg.length < 3) {
        for (var e = 0; e < filesImg.length; e++) {
            if (e == 0) {
                activo = 'active';
            } else {
                activo = '';
            }
            var respaldo = document.getElementById("lista_imgMini").innerHTML;
            document.getElementById("lista_imgMini").innerHTML = respaldo + ['<div class="carousel-item image-contain adaptar-car ', activo, '"> <img class="d-block vista-border-r size-v-image " id="carouselImgProp', e, '"> </div>'].join('');
        }
    } else {
        for (var e = 0; e < 3; e++) {
            if (e == 0) {
                activo = 'active';
            } else {
                activo = '';
            }
            var respaldo = document.getElementById("lista_imgMini").innerHTML;
            document.getElementById("lista_imgMini").innerHTML = respaldo + ['<div class="carousel-item image-contain adaptar-car ', activo, '"><img class="img-size-mp img-fluid d-block " alt="..." id="imgPrevisualizacion', e, '"> </div>'].join('');
        }
    }

    // Creacion de elementos para el Carousel
    for (var j = 2; j < filesImg.length; j++) {

        if (j == 2) {
            activo = 'active';
        } else {
            activo = '';
        }
        var respaldo = document.getElementById("listaImagenes").innerHTML;
        document.getElementById("listaImagenes").innerHTML = respaldo + ['<div class="carousel-item image-contain ejemploooooo ', activo, '"> <img class="d-block vista-border-r size-v-image " id="carouselImgProp', j, '"> </div>'].join('');
    }


    // Agregando las imagenes en los elementos
    for (var i = 0; i < filesImg.length; i++) {
        element = filesImg[i];
        const objectURL = URL.createObjectURL(element);

        if (i == 0) {
            activo = 'active';
        } else {
            activo = '';
        }


        if (i < 2) {
            document.getElementById("imgDespuesCarousel" + i).src = objectURL;
        }
        if (i < 3) {
            document.getElementById("imgPrevisualizacion" + i).src = objectURL;
        }

        if (i > 1 && i < filesImg.length) {
            document.getElementById("carouselImgProp" + i).src = objectURL;
        }
    }
});


$(function () {
    $("#nombre_propiedad, #TipoPropiedadID, #precioProp, #IDMoneda, #descripcion, #CP, #n_calle, #n_interior, #n_exterior, #colonia, #estado, #alc-muni, #pais, #Superficieval, #UnidadSuperficie, #SuperficieContruccion, #UnidadConstruccion, #banosval, #recamarasval, #estacionamientoval, #add-new-photo, #5, #6, #7, #8, #9, #10, #11, #12, #13, #14, #15, #16, #17, #18, #19, #20, #21, #22, #23, #24, #25, #26, #valBodegas, #valClosets, #valElevadores").on('change', function () {
        visualizar();
    })
});
function visualizar() {

    // Datos
    var verNombreProp = document.getElementById('nombre_propiedad').value;
    var verTipoProp = document.getElementById('TipoPropiedadID').value;
    var valPrecio = document.getElementById('precioProp').value;
    var valMoneda = document.getElementById('IDMoneda').value;
    var valDescripcion = document.getElementById('descripcion').value;

    var valCalle = document.getElementById('n_calle').value;
    var valNoInt = document.getElementById('n_interior').value;
    var valNoExt = document.getElementById('n_exterior').value;
    var valColonia = document.getElementById('colonia').value;
    var valMunicipio = document.getElementById('alc-muni').value;
    var valCP = document.getElementById('CP').value;
    var valEstado = document.getElementById('estado').value;
    var valPais = document.getElementById('pais').value;

    var valSuperficie = document.getElementById('Superficieval').value;
    var valUnidadSuperficie = document.getElementById('UnidadSuperficie').value;
    var valConstruccion = document.getElementById('SuperficieContruccion').value;
    var valUnidadConstruccion = document.getElementById('UnidadConstruccion').value;

    var valBanos = document.getElementById('banosval').value;
    var valRecamaras = document.getElementById('recamarasval').value;
    var valEstacionamiento = document.getElementById('estacionamientoval').value;


    // Variables para la obtencion de imagenes
    var imgPropiedad = document.querySelector("#add-new-photo");
    var imagenPrevisualizacion = document.querySelector("#imgPrevisualizacion");

    //var verNombreProp = $("#nombre_propiedad").val();
    //var verTipoProp = $("#TipoPropiedadID").val();
    //var valPrecio = $("#precioProp").val();
    //var valColonia = $("#colonia").val();
    //var valMunicipio = $("#alc-muni").val();
    //var valEstado = $("#estado").val();


    //console.log(verNombreProp, verTipoProp, valPrecio, valColonia, valvalMunicipio, valEstado);

    var valAccesoDiscapacitados = document.getElementById('5');
    var verAD = document.getElementById('verAD');

    if (valAccesoDiscapacitados.checked) {
        verAD.innerHTML = 'Acceso discapacitados';
        verAD.style.visibility = 'visible';
        verAD.style.overflow = 'visible';
        verAD.style.position = 'static';
    } else {
        verAD.style.visibility = 'hidden';
        verAD.style.overflow = 'hidden';
        verAD.style.position = 'absolute';
    }

    var valAlberca = document.getElementById('6');
    var verAL = document.getElementById('verAL');

    if (valAlberca.checked) {
        verAL.innerHTML = 'Alberca';
        verAL.style.visibility = 'visible';
        verAL.style.overflow = 'visible';
        verAL.style.position = 'static';
    } else {
        verAL.style.visibility = 'hidden';
        verAL.style.overflow = 'hidden';
        verAL.style.position = 'absolute';
    }

    var valAmueblado = document.getElementById('7');
    var verAM = document.getElementById('verAM');

    if (valAmueblado.checked) {
        verAM.innerHTML = 'Amueblado';
        verAM.style.visibility = 'visible';
        verAM.style.overflow = 'visible';
        verAM.style.position = 'static';
    } else {
        verAM.style.visibility = 'hidden';
        verAM.style.overflow = 'hidden';
        verAM.style.position = 'absolute';
    }

    var valCaseta = document.getElementById('8');
    var verCA = document.getElementById('verCA');

    if (valCaseta.checked) {
        verCA.innerHTML = 'Caseta de guardia';
        verCA.style.visibility = 'visible';
        verCA.style.overflow = 'visible';
        verCA.style.position = 'static';
    } else {
        verCA.style.visibility = 'hidden';
        verCA.style.overflow = 'hidden';
        verCA.style.position = 'absolute';
    }

    var valChimenea = document.getElementById('9');
    var verCH = document.getElementById('verCH');

    if (valChimenea.checked) {
        verCH.innerHTML = 'Chimenea';
        verCH.style.visibility = 'visible';
        verCH.style.overflow = 'visible';
        verCH.style.position = 'static';
    } else {
        verCH.style.visibility = 'hidden';
        verCH.style.overflow = 'hidden';
        verCH.style.position = 'absolute';
    }

    var valCocinaInt = document.getElementById('10');
    var verCO = document.getElementById('verCO');

    if (valCocinaInt.checked) {
        verCO.innerHTML = 'Cocina integral';
        verCO.style.visibility = 'visible';
        verCO.style.overflow = 'visible';
        verCO.style.position = 'static';
    } else {
        verCO.style.visibility = 'hidden';
        verCO.style.overflow = 'hidden';
        verCO.style.position = 'absolute';
    }

    var valCuartosSer = document.getElementById('11');
    var verCU = document.getElementById('verCU');

    if (valCuartosSer.checked) {
        verCU.innerHTML = 'Cuartos de servicio';
        verCU.style.visibility = 'visible';
        verCU.style.overflow = 'visible';
        verCU.style.position = 'static';
    } else {
        verCU.style.visibility = 'hidden';
        verCU.style.overflow = 'hidden';
        verCU.style.position = 'absolute';
    }

    var valEscuelas = document.getElementById('12');
    var verES = document.getElementById('verES');

    if (valEscuelas.checked) {
        verES.innerHTML = 'Escuelas cercanas';
        verES.style.visibility = 'visible';
        verES.style.overflow = 'visible';
        verES.style.position = 'static';
    } else {
        verES.style.visibility = 'hidden';
        verES.style.overflow = 'hidden';
        verES.style.position = 'absolute';
    }


    var valFrente = document.getElementById('13');
    var verFR = document.getElementById('verFR');

    if (valFrente.checked) {
        verFR.innerHTML = 'Frente a parque';
        verFR.style.visibility = 'visible';
        verFR.style.overflow = 'visible';
        verFR.style.position = 'static';
    } else {
        verFR.style.visibility = 'hidden';
        verFR.style.overflow = 'hidden';
        verFR.style.position = 'absolute';
    }

    var valJacuzzi = document.getElementById('14');
    var verJA = document.getElementById('verJA');

    if (valJacuzzi.checked) {
        verJA.innerHTML = 'Jacuzzi';
        verJA.style.visibility = 'visible';
        verJA.style.overflow = 'visible';
        verJA.style.position = 'static';
    } else {
        verJA.style.visibility = 'hidden';
        verJA.style.overflow = 'hidden';
        verJA.style.position = 'absolute';
    }

    var valMascotas = document.getElementById('15');
    var verMA = document.getElementById('verMA');

    if (valMascotas.checked) {
        verMA.innerHTML = 'Mascotas';
        verMA.style.visibility = 'visible';
        verMA.style.overflow = 'visible';
        verMA.style.position = 'static';
    } else {
        verMA.style.visibility = 'hidden';
        verMA.style.overflow = 'hidden';
        verMA.style.position = 'absolute';
    }

    var valAireAcondicionado = document.getElementById('16');
    var verAI = document.getElementById('verAI');

    if (valAireAcondicionado.checked) {
        verAI.innerHTML = 'Aire acondicionado';
        verAI.style.visibility = 'visible';
        verAI.style.overflow = 'visible';
        verAI.style.position = 'static';
    } else {
        verAI.style.visibility = 'hidden';
        verAI.style.overflow = 'hidden';
        verAI.style.position = 'absolute';
    }

    var valAreaJuegos = document.getElementById('17');
    var verAR = document.getElementById('verAR');

    if (valAreaJuegos.checked) {
        verAR.innerHTML = 'Área de juegos infantiles';
        verAR.style.visibility = 'visible';
        verAR.style.overflow = 'visible';
        verAR.style.position = 'static';
    } else {
        verAR.style.visibility = 'hidden';
        verAR.style.overflow = 'hidden';
        verAR.style.position = 'absolute';
    }

    var valCalefaccion = document.getElementById('18');
    var verCAL = document.getElementById('verCAL');

    if (valCalefaccion.checked) {
        verCAL.innerHTML = 'Calefacción';
        verCAL.style.visibility = 'visible';
        verCAL.style.overflow = 'visible';
        verCAL.style.position = 'static';
    } else {
        verCAL.style.visibility = 'hidden';
        verCAL.style.overflow = 'hidden';
        verCAL.style.position = 'absolute';
    }

    var valGim = document.getElementById('19');
    var verGI = document.getElementById('verGI');

    if (valGim.checked) {
        verGI.innerHTML = 'Gimnasio';
        verGI.style.visibility = 'visible';
        verGI.style.overflow = 'visible';
        verGI.style.position = 'static';
    } else {
        verGI.style.visibility = 'hidden';
        verGI.style.overflow = 'hidden';
        verGI.style.position = 'absolute';
    }

    var valSeguridad = document.getElementById('20');
    var verSE = document.getElementById('verSE');

    if (valSeguridad.checked) {
        verSE.innerHTML = 'Seguridad';
        verSE.style.visibility = 'visible';
        verSE.style.overflow = 'visible';
        verSE.style.position = 'static';
    } else {
        verSE.style.visibility = 'hidden';
        verSE.style.overflow = 'hidden';
        verSE.style.position = 'absolute';
    }

    var valAsador = document.getElementById('21');
    var verASA = document.getElementById('verASA');

    if (valAsador.checked) {
        verASA.innerHTML = 'Asador';
        verASA.style.visibility = 'visible';
        verASA.style.overflow = 'visible';
        verASA.style.position = 'static';
    } else {
        verASA.style.visibility = 'hidden';
        verASA.style.overflow = 'hidden';
        verASA.style.position = 'absolute';
    }

    var valCanchaS = document.getElementById('22');
    var verCANS = document.getElementById('verCANS');

    if (valCanchaS.checked) {
        verCANS.innerHTML = 'Cancha de squash';
        verCANS.style.visibility = 'visible';
        verCANS.style.overflow = 'visible';
        verCANS.style.position = 'static';
    } else {
        verCANS.style.visibility = 'hidden';
        verCANS.style.overflow = 'hidden';
        verCANS.style.position = 'absolute';
    }

    var valCanchaT = document.getElementById('23');
    var verCANT = document.getElementById('verCANT');

    if (valCanchaT.checked) {
        verCANT.innerHTML = 'Cancha de tenis';
        verCANT.style.visibility = 'visible';
        verCANT.style.overflow = 'visible';
        verCANT.style.position = 'static';
    } else {
        verCANT.style.visibility = 'hidden';
        verCANT.style.overflow = 'hidden';
        verCANT.style.position = 'absolute';
    }


    var valJardin = document.getElementById('24');
    var verJAR = document.getElementById('verJAR');

    if (valJardin.checked) {
        verJAR.innerHTML = 'Jardón privado';
        verJAR.style.visibility = 'visible';
        verJAR.style.overflow = 'visible';
        verJAR.style.position = 'static';
    } else {
        verJAR.style.visibility = 'hidden';
        verJAR.style.overflow = 'hidden';
        verJAR.style.position = 'absolute';
    }

    var valCuartoTV = document.getElementById('25');
    var verTV = document.getElementById('verTV');

    if (valCuartoTV.checked) {
        verTV.innerHTML = 'Cuarto de TV';
        verTV.style.visibility = 'visible';
        verTV.style.overflow = 'visible';
        verTV.style.position = 'static';
    } else {
        verTV.style.visibility = 'hidden';
        verTV.style.overflow = 'hidden';
        verTV.style.position = 'absolute';
    }

    var valEstudio = document.getElementById('26');
    var verEST = document.getElementById('verEST');

    if (valEstudio.checked) {
        verEST.innerHTML = 'Estudio';
        verEST.style.visibility = 'visible';
        verEST.style.overflow = 'visible';
        verEST.style.position = 'static';
    } else {
        verEST.style.visibility = 'hidden';
        verEST.style.overflow = 'hidden';
        verEST.style.position = 'absolute';
    }


    var valBodegas = document.getElementById('valBodegas').value;
    var verBodegas = document.getElementById('verBodegas');
    var txtBodegas = '';


    if (valBodegas == 1) {
        txtBodegas = ' Bodega';
    } else {
        txtBodegas = ' Bodegas';
    }

    if (valBodegas == 0) {
        verBodegas.innerHTML = '';
        verBodegas.style.visibility = 'hidden';
        verBodegas.style.overflow = 'hidden';
        verBodegas.style.position = 'absolute';
    }
    else {
        verBodegas.innerHTML = valBodegas + txtBodegas;
        verBodegas.style.visibility = 'visible';
        verBodegas.style.overflow = 'visible';
        verBodegas.style.position = 'static';
    }


    var valClosets = document.getElementById('valClosets').value;
    var verClosets = document.getElementById('verClosets');
    var txtClosets = '';


    if (valClosets == 1) {
        txtClosets = ' Closet';
    } else {
        txtClosets = ' Closets';
    }

    if (valClosets == 0) {
        verClosets.innerHTML = '';
        verClosets.style.visibility = 'hidden';
        verClosets.style.overflow = 'hidden';
        verClosets.style.position = 'absolute';
    }
    else {
        verClosets.innerHTML = valClosets + txtClosets;
        verClosets.style.visibility = 'visible';
        verClosets.style.overflow = 'visible';
        verClosets.style.position = 'static';
    }


    var valElevadores = document.getElementById('valElevadores').value;
    var verElevadores = document.getElementById('verElevadores');
    var txtElevadores = '';


    if (valClosets == 1) {
        txtElevadores = ' Elevador';
    } else {
        txtElevadores = ' Elevadores';
    }

    if (valElevadores == 0) {
        verElevadores.innerHTML = '';
        verElevadores.style.visibility = 'hidden';
        verElevadores.style.overflow = 'hidden';
        verElevadores.style.position = 'absolute';
    }
    else {
        verElevadores.innerHTML = valElevadores + txtElevadores;
        verElevadores.style.visibility = 'visible';
        verElevadores.style.overflow = 'visible';
        verElevadores.style.position = 'static';
    }


    // Agregando nombre de la propiedad en vista
    document.getElementById('verNombre').innerHTML = verNombreProp;
    document.getElementById('verNombre2').innerHTML = verNombreProp;

    // Agregnado Tipo Propiedad en vista
    var verAbrevTipo = '';
    var verTxtProp = '';
    if (verTipoProp == 1) {
        verAbrevTipo = 'Depto';
        verTxtProp = 'Departamento';
    }
    if (verTipoProp == 2) {
        verAbrevTipo = 'Bodega';
        verTxtProp = 'Bodega';
    }
    if (verTipoProp == 3) {
        verAbrevTipo = 'Casa';
        verTxtProp = 'Casa';
    }
    if (verTipoProp == 4) {
        verAbrevTipo = 'Edif';
        verTxtProp = 'Edificio';
    }
    if (verTipoProp == 5) {
        verAbrevTipo = 'Huerta';
        verTxtProp = 'Huerta';
    }
    if (verTipoProp == 6) {
        verAbrevTipo = 'Local';
        verTxtProp = 'Local comercial';
    }
    if (verTipoProp == 7) {
        verAbrevTipo = 'Nave';
        verTxtProp = 'Nave industrial';
    }
    if (verTipoProp == 8) {
        verAbrevTipo = 'Ofic';
        verTxtProp = 'Oficina';
    }
    if (verTipoProp == 9) {
        verAbrevTipo = 'Quinta';
        verTxtProp = 'Quinta';
    }
    if (verTipoProp == 10) {
        verAbrevTipo = 'Rancho';
        verTxtProp = 'Rancho';
    }
    if (verTipoProp == 11) {
        verAbrevTipo = 'Terreno';
        verTxtProp = 'Terreno/Lote';
    }
    if (verTipoProp == 12) {
        verAbrevTipo = 'Villa';
        verTxtProp = 'Villa';
    }
    if (verTipoProp == 13) {
        verAbrevTipo = 'Hosp.';
        verTxtProp = 'Hospital';
    }
    if (verTipoProp == 14) {
        verAbrevTipo = 'Esc.';
        verTxtProp = 'Escuela';
    }



    document.getElementById('verTipoProp').innerHTML = verAbrevTipo;
    document.getElementById('verTipoProp2').innerHTML = verTxtProp;

    // Agregar ver Precio

    var verAbrevMoneda = '';
    if (valMoneda == 1) {
        verAbrevMoneda = 'MXN';
    } if (valMoneda == 2) {
        verAbrevMoneda = 'USD';
    }

    document.getElementById('verPrecio').innerHTML = valPrecio;
    document.getElementById('verPrecio2').innerHTML = '$' + valPrecio + verAbrevMoneda;

    // Agregar ver Ubicacion

    document.getElementById('verUbicacion').innerHTML = valColonia;
    document.getElementById('verEstado').innerHTML = valEstado;

    document.getElementById('verCalle').innerHTML = valCalle;
    document.getElementById('verNoInt').innerHTML = valNoInt;
    document.getElementById('verNoExt').innerHTML = valNoExt;
    document.getElementById('verColonia').innerHTML = valColonia;
    document.getElementById('verCP').innerHTML = valCP;
    document.getElementById('verMunicipio').innerHTML = valMunicipio;
    document.getElementById('verPais').innerHTML = valPais;

    // Agregar Superficie

    var Unidad = '';
    if (valUnidadSuperficie == 1) {
        Unidad = 'm2';
    } if (valUnidadSuperficie == 2) {
        Unidad = 'ha';
    }

    var UnidadCons = '';
    if (valUnidadConstruccion == 1) {
        UnidadCons = 'm2';
    } if (valUnidadConstruccion == 2) {
        UnidadCons = 'ha';
    }

    document.getElementById('verSuperficie').innerHTML = valSuperficie + Unidad;
    document.getElementById('verSuperficie2').innerHTML = valSuperficie + Unidad;

    document.getElementById('verConstruccion').innerHTML = valConstruccion + UnidadCons;

    // Agregando datos de caracteristicas
    var txtBanos = ' Baño';
    var txtEstacionamiento = ' Estacionamiento';
    var txtRecamaras = ' Recamara';
    if (valBanos > 1) {
        txtBanos = ' Baños';
    } if (valEstacionamiento > 1) {
        txtEstacionamiento = ' Estacionamientos';
    }
    if (valRecamaras > 1) {
        txtRecamaras = ' Recamaras';
    }

    document.getElementById('verBanos').innerHTML = valBanos;
    document.getElementById('verBanos2').innerHTML = valBanos + txtBanos;

    document.getElementById('verEstacionamiento').innerHTML = valEstacionamiento;
    document.getElementById('verEstacionamiento2').innerHTML = valEstacionamiento + txtEstacionamiento;

    document.getElementById('verRecamaras').innerHTML = valRecamaras;
    document.getElementById('verRecamaras2').innerHTML = valRecamaras + txtRecamaras;

    // Agregnado Descipcion
    document.getElementById('verDescripcion').innerHTML = valDescripcion;

}




