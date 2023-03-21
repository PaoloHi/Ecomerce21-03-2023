var coll = document.getElementsByClassName("collapsible");
var i;

for (i = 0; i < coll.length; i++) {
    coll[i].addEventListener("click", function () {
        this.classList.toggle("activee");
        var content = this.nextElementSibling;
        if (content.style.maxHeight) {
            content.style.maxHeight = null;
        } else {
            content.style.maxHeight = content.scrollHeight + "px";
        }
    });
}

var coll = document.getElementsByClassName("collapsible2");
var i;

for (i = 0; i < coll.length; i++) {
    coll[i].addEventListener("click", function () {
        this.classList.toggle("activee");
        var content = this.nextElementSibling;
        if (content.style.maxHeight) {
            content.style.maxHeight = null;
        } else {
            content.style.maxHeight = content.scrollHeight + "px";
        }
    });
};



/*fucionalidad para mostrar / ocultar las opciones periodicas de pago de renta => */

//ocultar
document.getElementById("comprar").onclick = function () {
    document.getElementById("rentaOpcions").style.display = "none";
    document.getElementById("rentaOpcions2").style.display = "none";
}

//mostrar
document.getElementById("renta").onclick = function () {
    document.getElementById("rentaOpcions").style.display = "flex";
    document.getElementById("rentaOpcions2").style.display = "block";
}

documnet.getElementById("correo") = function () {
    if (true) {
        document.getElementById("datosPropietarioNoExistente").style.display = "none";
    } else {
        document.getElementById("datosPropietarioNoExistente").style.display = "flex";
    }

}
