//Ejecutar funcion en el evento click
document.getElementById("btn_open").addEventListener("click", open_close_menu);
//Declaramos variables

var side_menu = document.getElementById("side_menu");
var btn_open = document.getElementById("btn_open");
var body = document.getElementById("body");

var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
})

//Evento par amostrar y ocultar el menu

function open_close_menu(){
    body.classList.toggle("body_move");
    side_menu.classList.toggle("menu__side_move");
}




