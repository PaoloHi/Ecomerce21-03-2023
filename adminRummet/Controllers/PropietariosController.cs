using Microsoft.AspNetCore.Mvc;

namespace adminRummet.Controllers
{
    public class PropietariosController : Controller
    {
        //Tablero del propietario
        public IActionResult TableroPropietario()
        {
            return View();
        }

        //Perfil del propietario
        public IActionResult PerfilPropietario()
        {
            return View();
        }

        //Detalles de propiedad no publicada - Propietario
        public IActionResult PDetallesPropiedadNP()
        {
            return View();
        }

        //Detalles de propiedad publicada - Propietario
        public IActionResult PDetallesPropiedadPP()
        {
            return View();
        }

        //Detalles de propiedad en proceso - Propietario
        public IActionResult PDetallesPropiedadEP()
        {
            return View();
        }
     
    }
}
