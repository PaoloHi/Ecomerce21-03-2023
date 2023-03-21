

btns()
    function btns() {
      const btnOnboarding = document.getElementById('btnOnboarding'),
        btnVenta = document.getElementById('btnVenta'),
        btnEntrega = document.getElementById('btnEntrega'),
        btnPostventa = document.getElementById('btnPostventa');

      const tblOnboarding = document.getElementById('tblOnboarding'),
        tblVenta = document.getElementById('tblVenta'),
        tblEntrega = document.getElementById('tblEntrega'),
        tblPostventa = document.getElementById('tblPostventa');

        // funcion boton Onboarding 
      btnOnboarding.onclick = function () {
        tblOnboarding.classList.add("active")
        tblVenta.classList.remove("active")
        tblEntrega.classList.remove("active")
        tblPostventa.classList.remove("active")
      }

      // funcion boton venta
      btnVenta.onclick = function () {
        tblOnboarding.classList.remove("active")
        tblVenta.classList.add("active")
        tblEntrega.classList.remove("active")
        tblPostventa.classList.remove("active")
      }

      // funcion boton entrega
      btnEntrega.onclick = function () {
        tblOnboarding.classList.remove("active")
        tblVenta.classList.remove("active")
        tblEntrega.classList.add("active")
        tblPostventa.classList.remove("active")
      }

      // funcion boton postventa
      btnPostventa.onclick = function () {
        tblOnboarding.classList.remove("active")
        tblVenta.classList.remove("active")
        tblEntrega.classList.remove("active")
        tblPostventa.classList.add("active")
      }
    }