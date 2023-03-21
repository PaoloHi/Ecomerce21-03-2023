

function rechazar() {
    document.getElementById('motivoRechazo').style.display = 'block';
    document.getElementById('rechazrBtn').style.display = 'none';
    document.getElementById('aprbarBtn').style.display = 'none';
    document.getElementById('modal-header').style.display = 'none';
}


function aprovar() {
    document.getElementById('rechazrBtn').style.display = 'none';
    document.getElementById('aprbarBtn').style.display = 'none';
    document.getElementById('motivoAprovado').style.display = 'block'
    document.getElementById('modal-header').style.display = 'none';
}


function resetModal() {
    document.getElementById('motivoRechazo').style.display = 'none';
    document.getElementById('motivoAprovado').style.display = 'none';
    document.getElementById('aprbarBtn').style.display = 'block';
    document.getElementById('rechazrBtn').style.display = 'block';
    document.getElementById('modal-header').style.display = 'flex';
}
