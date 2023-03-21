//Este script es para el menu lateral aquí encontraras muchas funciones que tengan que ver con el menu lateral




var side_menu = document.getElementById("side_menu");
var btn_open = document.getElementById("btn_open");
var body = document.getElementById("body");
var option_menu = document.getElementById("options")

let list = document.querySelectorAll(".options__menu a ");


var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
})




function open_close_menu(){
    body.classList.toggle("body_move");
    side_menu.classList.toggle("menu__side_move");
}


//este chiquitillo es para el rodondeo en la barra de navegacion 
let smallToggle = 1;

function small() {
    if (smallToggle == 1) {
        option_menu.classList.add("small")
        smallToggle = 0
    } else {
        option_menu.classList.remove("small")
        smallToggle = 1
        console.log("funciono")

    }
}
small();

function activeLink() {
    list.forEach((item) =>
    item.classList.remove('hovered'));
    this.classList.add('hovered') 
}


list.forEach((item)=>
item.addEventListener("click", activeLink))


//Ejecutar funcion en el evento click

document.getElementById("btn_open").addEventListener("click", small);
btn_open.addEventListener("click", open_close_menu);
btn_open.addEventListener("click", activeLink);

//Declaramos variables


//Función para que funcione la barra de progreso

const steps = document.querySelectorAll(".step");
//Evento para mostrar y ocultar el menu
let active = 1;

progressNext.addEventListener("click", () => {
    active++;
    if (active > steps.length) {
        active = steps.length;
    }
    updateProgress();
});

progressPrev.addEventListener("click", () => {
    active--;
    if (active < 1) {
        active = 1;
    }
    updateProgress();
});

const updateProgress = () => {
    steps.forEach((step, i) => {
        if (i < active) {
            step.classList.add("active");
        } else {
            step.classList.remove("active");
        }
    });
    progressBar.style.width = ((active - 1) / (steps.length - 1)) * 100 + "%";
    if (active === 1) {
        progressPrev.disabled = true;
    } else if (active === steps.length) {
        progressNext.disabled = true;
    } else {
        progressPrev.disabled = false;
        progressNext.disabled = false;
    }
};

//funciones para hacer la operación de sacar el porcentaje

function percentage_1() {

    var totalFinal = 0;

    var percent = document.getElementById("percent").value;
    //var total = document.getElementById("total").value;
    var num = document.getElementById("num").value;

    

    //document.getElementById("value1").value = (percent * num) / 100;

    totalFinal = (num - ((percent * num) / 100));
    document.getElementById("value1").value = totalFinal;
    

}
// Función de active de los botones de mesa de control



//*+para fijar el hover en la seccion en la que estemos 

