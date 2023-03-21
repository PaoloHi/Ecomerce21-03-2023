
            // Script para tab de tablero de brokers
            function openCity(evt, cityName) {
                var i, tabcontent, tablinks;
                tabcontent = document.getElementsByClassName("tabcontent");
                for (i = 0; i < tabcontent.length; i++) {
                    tabcontent[i].style.display = "none";
                }
                tablinks = document.getElementsByClassName("tablinks");
                for (i = 0; i < tablinks.length; i++) {
                    tablinks[i].className = tablinks[i].className.replace(" active", "");
                }
                document.getElementById(cityName).style.display = "block";
                evt.currentTarget.className += " active";
            }

            // Get the element with id="defaultOpen" and click on it
            document.getElementById("defaultOpen").click();




            /*Función 2 para ocultar las tablas*/
            function vertablas(evt, vertablas) {

                var i, tabcontent2, tablinks2;
                tabcontent2 = document.getElementsByClassName("tabcontent2");
                for (i = 0; i < tabcontent2.length; i++) {
                    var juanito = tabcontent2[i].style.display = "none";
                    tabcontent2[i].style.display = "none";
                }
                tablinks2 = document.getElementsByClassName("tablinks2");
                for (i = 0; i < tablinks2.length; i++) {
                    tablinks2[i].className = tablinks2[i].className.replace(" active", "");
                }
                document.getElementById(vertablas).style.display = "block";
                evt.currentTarget.className += " active";

            }

            // Get the element with id="defaultOpen" and click on it
            document.getElementById("defaultOpen2").click();


            /*Función 3 para ocultar contenedores*/
            function vercontenedor(evt, vercontenedor) {
                var i, tabcontent3, tablinks3;
                tabcontent3 = document.getElementsByClassName("tabcontent3");
                for (i = 0; i < tabcontent3.length; i++) {
                    tabcontent3[i].style.display = "none";
                }
                tablinks3 = document.getElementsByClassName("tablinks3");
                for (i = 0; i < tablinks3.length; i++) {
                    tablinks3[i].className = tablinks3[i].className.replace(" active", "");
                }
                document.getElementById(vercontenedor).style.display = "block";
                evt.currentTarget.className += " active";
            }

            // Get the element with id="defaultOpen" and click on it
            document.getElementById("defaultOpen3").click();






            /*Función 4 para ocultar las tablas*/
            function vercontenedor2(evt, vercontenedor2) {
                var i, tabcontent4, tablinks4;
                tabcontent4 = document.getElementsByClassName("tabcontent4");
                for (i = 0; i < tabcontent4.length; i++) {
                    tabcontent4[i].style.display = "none";
                }
                tablinks4 = document.getElementsByClassName("tablinks4");
                for (i = 0; i < tablinks4.length; i++) {
                    tablinks4[i].className = tablinks4[i].className.replace(" active", "");
                }
                document.getElementById(vercontenedor2).style.display = "block";
                evt.currentTarget.className += " active";
            }

            // Get the element with id="defaultOpen" and click on it
            document.getElementById("defaultOpen4").click();


        /*Función 5 para ocultar las tablas*/
        function vercontenedor3(evt, vercontenedor3) {
            var i, tabcontent5, tablinks5;
            tabcontent5 = document.getElementsByClassName("tabcontent5");
            for (i = 0; i < tabcontent5.length; i++) {
                tabcontent5[i].style.display = "none";
            }
            tablinks5 = document.getElementsByClassName("tablinks5");
            for (i = 0; i < tablinks5.length; i++) {
                tablinks5[i].className = tablinks5[i].className.replace(" active", "");
            }
            document.getElementById(vercontenedor3).style.display = "block";
            evt.currentTarget.className += " active";
        }

        // Get the element with id="defaultOpen" and click on it
        document.getElementById("defaultOpen5").click();





//funcion para descargar excel de las propiedades-- >
    function exportTableToExcel(tableID, filename = 'propiedades') {
        var downloadLink;
        var dataType = 'application/vnd.ms-excel';
        var tableSelect = document.getElementById(tableID);
        var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');

        // Specify file name
        filename = filename ? filename + '.xls' : 'excel_data.xls';

        // Create download link element
        downloadLink = document.createElement("a");

        document.body.appendChild(downloadLink);

        if (navigator.msSaveOrOpenBlob) {
            var blob = new Blob(['ufeff', tableHTML], {
            type: dataType
            });
        navigator.msSaveOrOpenBlob(blob, filename);
        } else {
            // Create a link to the file
            downloadLink.href = 'data:' + dataType + ', ' + tableHTML;

        // Setting the file name
        downloadLink.download = filename;

        //triggering the function
        downloadLink.click();
        }
    }

