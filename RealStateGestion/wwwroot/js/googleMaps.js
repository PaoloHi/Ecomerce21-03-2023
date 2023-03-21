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

    function initialize() {
      // Cree el objeto de autocompletado, restringiendo la búsqueda
      autocomplete = new google.maps.places.Autocomplete(
          document.getElementById("n_calle"),
        {
          types: ["route"],
          componentRestrictions: { country: "mx" }, // Se especifica que solo habrá búsqueda de México
        }
      );
      // Cuando el usuario selecciona una dirección en el menú desplegable,
      // rellena los campos de dirección en el formulario.
      google.maps.event.addListener(autocomplete, "place_changed", function () {
        fillInAddress();
      });

      autocomplete_textarea = new google.maps.places.Autocomplete(
        document.getElementById("autocomplete_textarea"),
        { types: ["geocode"] }
      );
      google.maps.event.addListener(
        autocomplete_textarea,
        "place_changed",
        function () {
          fillInAddress_textarea();
        }
      );
    }

    function fillInAddress_textarea() {
      var place = autocomplete_textarea.getPlace();
      console.log(place.formatted_address);
      console.log(JSON.stringify(place));

      // $("#autocomplete_textarea").val(place.formatted_address);
    }

    function fillInAddress() {

      // Obtener los detalles de lugar el objeto de autocompletado.
      var place = autocomplete.getPlace();
      // console.log( JSON.stringify(place));

      // Recibe cada componente de la dirección de los lugares más detalles
      // y llena el campo correspondiente en el formulario.

      for (var i = 0; i < place.address_components.length; i++) {
        var addressType = place.address_components[i].types[0];
        // alert(addressType);
        if (idForm[addressType]) {
          // console.log(idForm[addressType])
          if ( idForm[addressType] != 'c.p'){ // En dado caso de que la direccion entontrada no tenga C.P se ponda en blanco
            document.getElementById('c.p').value = " ";
          }
          var val = place.address_components[i][componentForm[addressType]]; // guardando nuestra informacion en nuestra variable
          // console.log(val); 
          // console.log(val, addressType);
          document.getElementById(idForm[addressType]).value = val; // Especificando el campo en donde se mostrara la información
        }
      }

      // Se verifica que el texto de calle no se el predeterminado de Google Maps
      const veriCalle = (document.getElementById('n_calle').value).includes(',');
      // console.log(veriCalle)
      if (veriCalle == true){
        document.getElementById('n_calle').value = " ";
      }

        var map, vMarker;
        var map2, vMarker2;

      // Se especifica el llamado para mostrar nuestro mapa en el #map con unas cordenadas predeterminadas
      map = new google.maps.Map(document.getElementById("map"), {
        zoom: 19,
        center: new google.maps.LatLng(19.4326296, -99.1331785),
        mapTypeId: google.maps.MapTypeId.ROADMAP,
      });

        map2 = new google.maps.Map(document.getElementById("map2"), {
            zoom: 19,
            center: new google.maps.LatLng(19.4326296, -99.1331785),
            mapTypeId: google.maps.MapTypeId.ROADMAP,
        });

      // Se especifica en donde esta el punto
      vMarker = new google.maps.Marker({
        position: new google.maps.LatLng(19.4326296, -99.1331785),
        draggable: true // Se especifica con draggable de que el usuario podra cambiar la ubicacion del punto
      });

        vMarker2 = new google.maps.Marker({
            position: new google.maps.LatLng(19.4326296, -99.1331785),
            draggable: false // Se especifica con draggable de que el usuario podra cambiar la ubicacion del punto
        });

      // Se especifica en donde se guardaran los valores de longitud y lantitud si el usuario cambio el punto
      google.maps.event.addListener(vMarker, 'dragend', function (evt) {
        $("#txtLat").val(evt.latLng.lat().toFixed(6));
        $("#txtLng").val(evt.latLng.lng().toFixed(6));

        var geocoder = new google.maps.Geocoder();
        var textLat = document.getElementById("txtLat").value;
        var textLng = document.getElementById("txtLng").value;
        var searchLatLng = textLat + ',' + textLng; // texto de busqueda con longitud y latitud
        // console.log(searchLatLng);

        // En caso de que el usario cambie el lugar del punto
        // se mandara una consulata a la API para obtener nuevos datos
        geocoder.geocode({
          "address": searchLatLng // Verifica si la busqueda con logitud y latitud es valida
        }, function (result, status) {
          // console.log(result)
          // alert (status);
          if (status == google.maps.GeocoderStatus.OK) { // Se obtiene todos los datos de lantitud y longitud
            
            for (var i = 0; i < result[0].address_components.length; i++) { // result[0] es es la busqueda exacta del lugar debido a que hay mas datos
              var addressType = result[0].address_components[i].types[0];
              // alert(addressType);
              if (idForm[addressType]) {
                // console.log(idForm[addressType])
                if ( idForm[addressType] != 'c.p'){ // En dado caso de que la direccion entontrada no tenga C.P se ponda en blanco
                  document.getElementById('c.p').value = " ";
                }
                var val = result[0].address_components[i][componentForm[addressType]];
                // console.log(val); 
                // console.log(val, addressType);
                document.getElementById(idForm[addressType]).value = val;
              }
            }

          }
        });

          map.panTo(evt.latLng);
          map2.panTo(evt.latLng);
      });

      map.setCenter(vMarker.position);
        vMarker.setMap(map);

        map2.setCenter(vMarker2.position);
        vMarker2.setMap(map2);

      movePin();

      function movePin() { // funcion para cambiar mapa con los campos ingresados de los inputs
        var geocoder = new google.maps.Geocoder();
        var textCalle = document.getElementById("n_calle").value;
        var textColonia = document.getElementById("colonia").value;
        var textNumInt = document.getElementById("n_interior").value;
        var textNumExt = document.getElementById("n_exterior").value;
        var textAlcMuni = document.getElementById("alc-muni").value;
        var textEstado = document.getElementById("estado").value;
        var textPais = document.getElementById("pais").value;
        var textCP = document.getElementById("c.p").value;
        var direccionCompleta = textCalle + ' ' + textColonia +'' + textNumExt + '' + textAlcMuni + ' ' + textEstado + ' ' + textPais + ' ' + textCP; // varaible con el valor de los inputs juntos
        // console.log(direccionCompleta);

          // Igresando texto de direccion completa en el input de direccion completa
          if (textNumInt == "") {
              var txtInputDireccionCompleta = textCalle + ' ' + textNumExt + ', ' + textColonia + ', ' + textAlcMuni + ', ' + textEstado + ', ' + textPais;
              document.getElementById('direccion_completa').value = txtInputDireccionCompleta;
          } else {
              var txtInputDireccionCompleta = textCalle + ' ' + textNumExt + ' NumInt ' + textNumInt + ', ' + textColonia + ', ' + textAlcMuni + ', ' + textEstado + ', ' + textPais;
              document.getElementById('direccion_completa').value = txtInputDireccionCompleta;
          }

        geocoder.geocode({
          "address": direccionCompleta // verifica si la direccionCompleta es encontrada
        }, function (result, status) {
          // console.log(result)
          // alert (status);
          if (status == google.maps.GeocoderStatus.OK) { // Se obtiene todos los datos de lantitud y longitud
            vMarker.setPosition(new google.maps.LatLng(result[0].geometry.location.lat(), result[0].geometry.location.lng())); // cambia los valores de latitud y longitud por los nuevos encontrados
            map.panTo(new google.maps.LatLng(result[0].geometry.location.lat(), result[0].geometry.location.lng())); // cambia los valores de latitud y longitud por los nuevos encontrados
            $("#txtLat").val(result[0].geometry.location.lat());
            $("#txtLng").val(result[0].geometry.location.lng());
          }
        });
      }
    }

    //ubicación geográfica del usuario,

    function geolocate() {
      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
          var geolocation = new google.maps.LatLng(
            position.coords.latitude,
            position.coords.longitude
          );
          var circle = new google.maps.Circle({
            center: geolocation,
            radius: position.coords.accuracy,
          });
          autocomplete.setBounds(circle.getBounds());
          autocomplete_textarea.setBounds(circle.getBounds());
        });
      }
    }

    // Se escpecifica en donde se mostrara nuestro mapa, center en la ubicacion que predeterminada

//     $(function () {
//       $("#c.p, #n_calle, #n_interior, #n_exterior, #colonia, #estado, #alc-muni, #pais").on('change', function () {
//         alert("Hola")
//         initMap(document.getElementById("txtLat").value, document.getElementById("txtLng").value);
//       })
//     });

    function initMap(latitud, longitud) {
        var map, vMarker;

        var map2, vMarker2;

      map = new google.maps.Map(document.getElementById("map"), {
        zoom: 19,
      });

        map2 = new google.maps.Map(document.getElementById("map2"), {
            zoom: 19,
        });

      // // Se especifica en donde esta el punto
      vMarker = new google.maps.Marker({
        position: new google.maps.LatLng(latitud, longitud),
        draggable: true // Se especifica con draggable de que el usuario podra cambiar la ubicacion del punto
      });

        vMarker2 = new google.maps.Marker({
            position: new google.maps.LatLng(latitud, longitud),
            draggable: false // Se especifica con draggable de que el usuario podra cambiar la ubicacion del punto
        });

      // Se especifica en donde se guardaran los valores de longitud y lantitud si el usuario cambio el punto
      google.maps.event.addListener(vMarker, 'dragend', function (evt) {
        console.log(evt);
        $("#txtLat").val(evt.latLng.lat().toFixed(6));
        $("#txtLng").val(evt.latLng.lng().toFixed(6));

        var geocoder = new google.maps.Geocoder();
        var textLat = document.getElementById("txtLat").value;
        var textLng = document.getElementById("txtLng").value;
        var searchLatLng = textLat + ',' + textLng; // texto de busqueda con longitud y latitud
        // console.log(searchLatLng);

        // En caso de que el usario cambie el lugar del punto
        // se mandara una consulata a la API para obtener nuevos datos
        geocoder.geocode({
          "address": searchLatLng // Verifica si la busqueda con logitud y latitud es valida
        }, function (result, status) {
          // console.log(result)
          // alert (status);
          if (status == google.maps.GeocoderStatus.OK) { // Se obtiene todos los datos de lantitud y longitud
            
            for (var i = 0; i < result[0].address_components.length; i++) { // result[0] es es la busqueda exacta del lugar debido a que hay mas datos
              var addressType = result[0].address_components[i].types[0];
              // alert(addressType);
              if (idForm[addressType]) {
                // console.log(idForm[addressType])
                if ( idForm[addressType] != 'c.p'){ // En dado caso de que la direccion entontrada no tenga C.P se ponda en blanco
                  document.getElementById('c.p').value = " ";
                }
                var val = result[0].address_components[i][componentForm[addressType]];
                // console.log(val); 
                // console.log(val, addressType);
                document.getElementById(idForm[addressType]).value = val;
              }
            }

          }
        });
        
          map.panTo(evt.latLng);
          map2.panTo(evt.latLng);
      });

      map.setCenter(vMarker.position);
        vMarker.setMap(map);

        map2.setCenter(vMarker2.position);
        vMarker2.setMap(map2);

      movePin();

      function movePin() { // funcion para cambiar mapa con los campos ingresados de los inputs
        var geocoder = new google.maps.Geocoder();
        var textCalle = document.getElementById("n_calle").value;
        var textColonia = document.getElementById("colonia").value;
        var textNumInt = document.getElementById("n_interior").value;
        var textNumExt = document.getElementById("n_exterior").value;
        var textAlcMuni = document.getElementById("alc-muni").value;
        var textEstado = document.getElementById("estado").value;
        var textPais = document.getElementById("pais").value;
        var textCP = document.getElementById("c.p").value;
        var direccionCompleta = textCalle + ' ' + textColonia +'' + textNumExt + '' + textAlcMuni + ' ' + textEstado + ' ' + textPais + ' ' + textCP; // varaible con el valor de los inputs juntos
        // console.log(direccionCompleta);

          // Igresando texto de direccion completa en el input de direccion completa
          if (textNumInt == "") {
              var txtInputDireccionCompleta = textCalle + ' ' + textNumExt + ', ' + textColonia + ', ' + textAlcMuni + ', ' + textEstado + ', ' + textPais;
              document.getElementById('direccion_completa').value = txtInputDireccionCompleta;
          } else {
              var txtInputDireccionCompleta = textCalle + ' ' + textNumExt + ' NumInt ' + textNumInt + ', ' + textColonia + ', ' + textAlcMuni + ', ' + textEstado + ', ' + textPais;
              document.getElementById('direccion_completa').value = txtInputDireccionCompleta;
          }

        geocoder.geocode({
          "address": direccionCompleta // verifica si la direccionCompleta es encontrada
        }, function (result, status) {
          // console.log(result)
          // alert (status);
          if (status == google.maps.GeocoderStatus.OK) { // Se obtiene todos los datos de lantitud y longitud
            vMarker.setPosition(new google.maps.LatLng(result[0].geometry.location.lat(), result[0].geometry.location.lng())); // cambia los valores de latitud y longitud por los nuevos encontrados
            map.panTo(new google.maps.LatLng(result[0].geometry.location.lat(), result[0].geometry.location.lng())); // cambia los valores de latitud y longitud por los nuevos encontrados
            $("#txtLat").val(result[0].geometry.location.lat());
            $("#txtLng").val(result[0].geometry.location.lng());
          }
        });
      }
    }