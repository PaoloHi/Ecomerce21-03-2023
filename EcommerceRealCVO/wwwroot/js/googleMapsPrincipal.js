var placeSearch, autocomplete, autocomplete_textarea;

var idForm = { // Especificacion de id´s de nuestro formaulario
    postal_code: "c.p",
    street_number: "n_exterior",
    route: "n_calle",
    sublocality_level_1: "colonia",
    locality: "alc-muni",
    administrative_area_level_1: "estado",
    country: "pais",
};

var componentForm = { // Variable para datos que deseamos obtener
    postal_code: "short_name",
    street_number: "long_name",
    route: "long_name",
    sublocality_level_1: "long_name",
    locality: "long_name",
    administrative_area_level_1: "long_name",
    country: "long_name",
};

let coloniaf;
let municipiof;
let estadof;

var col, mun, edo, pais;

function initialize() {
    //Inicialización de input para dépositar el dato

    coloniaf = document.querySelector("#Colonia");
    municipiof = document.querySelector("#Municipio");
    estadof = document.querySelector("#CE");

    // Cree el objeto de autocompletado, restringiendo la búsqueda
    autocomplete = new google.maps.places.Autocomplete(
      document.getElementById("input-principal"),
      {
        types: ["locality", "sublocality_level_1"], // busqueda por municipio locality = municipio
        componentRestrictions: { country: "mx" }, // Se especifica que solo abra búsqueda de México
      }
    );

    // Cuando el usuario selecciona una dirección en el menú desplegable,
    // rellena los campos de dirección en el formulario.
    google.maps.event.addListener(autocomplete, "place_changed", function () {
        fillInAddress();
    });
}

function fillInAddress() {

    // Obtener los detalles de lugar el objeto de autocompletado.
    var place = autocomplete.getPlace();
    //console.log(JSON.stringify(place));

    // Recibe cada componente de la dirección de los lugares más detalles
    // y llena el campo correspondiente en el formulario.

    for (var i = 0; i < place.address_components.length; i++) {
        var addressType = place.address_components[i].types[0];
        // alert(addressType);
        if (idForm[addressType]) {
            //console.log("ID DEL FORM " + idForm[addressType])

            var val = place.address_components[i][componentForm[addressType]]; // guardando nuestra informacion en nuestra variable
            //console.log("ESTE ES EL VALOR " + val); 

            var form = idForm[addressType];

            switch (idForm[addressType]) {
                case 'colonia':
                    col = val;
                    break;
                case 'alc-muni':
                    mun = val;
                    break;
                case 'estado':
                    edo = val;
                    break;
                case 'pais':
                    pais = val;
                    break;
            }
                    //console.log(val, addressType);
            //document.getElementById(idForm[addressType]).value = val; // Especificando el campo en donde se mostrara la información
        }
    }

    coloniaf.value = col;
    estadof.value = edo;
    municipiof.value = mun;

    
}



