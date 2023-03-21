using Microsoft.AspNetCore.Mvc;
using RealStateGestion.Datos.Center;
using RealStateGestion.Models;
using RealStateGestion.Tools;

namespace RealStateGestion.Controllers
{
    public class PropiedadesController : Controller
    {
        //Traer información del Center
        PropiedadesCenter _PropiedadesData = new PropiedadesCenter();
        HerramientasImagenes _HerramientasImagenes = new HerramientasImagenes();

        //Tablero de propiedades
        public IActionResult Propiedades()
        {
            var propiedadesModel = new PropiedadesModelTablero();

            //Propiedades publicadas
            var oListaPropiedades = _PropiedadesData.ListarPropiedades("Publicada");
            propiedadesModel.PropiedadesL = oListaPropiedades;

            //Imagenes de la propiedad publicada
            propiedadesModel.PropLImagenBase64 = _HerramientasImagenes.ConversionB64Propiedades("Publicada");

            //Propiedades No publicadas
            oListaPropiedades = _PropiedadesData.ListarPropiedades("No publicada");
            propiedadesModel.PropiedadesNL = oListaPropiedades;

            //Imagenes de la propiedad publicada
            propiedadesModel.PropNLImagenBase64 = _HerramientasImagenes.ConversionB64Propiedades("No publicada");

            //Propiedades En proceso
            oListaPropiedades = _PropiedadesData.ListarPropiedades("En proceso");
            propiedadesModel.PropiedadesPL = oListaPropiedades;

            //Imagenes de la propiedad publicada
            propiedadesModel.PropPLImagenBase64 = _HerramientasImagenes.ConversionB64Propiedades("En proceso");
            
            //Propiedades Cerradas
            oListaPropiedades = _PropiedadesData.ListarPropiedades("Cerrada");
            propiedadesModel.PropiedadesCL = oListaPropiedades;

            //Imagenes de la propiedad publicada
            propiedadesModel.PropCLImagenBase64 = _HerramientasImagenes.ConversionB64Propiedades("Cerrada");


            return View(propiedadesModel);
        }

        //Añadir una propiedad
        public IActionResult NuevaPropiedad()
        {
            var propiedadesModel = new PropiedadesModel();

            //Llamando al método Listar, el cual trae los datos de tipo de propiedades
            var oListaTProp = _PropiedadesData.ListarTipoProp();
            //Llenado de la lista a través del modelo 
            propiedadesModel.TPropiedadL = oListaTProp;

            //Llamando al metodo para llenar los datos del tipo de moneda
            var oListaMoneda = _PropiedadesData.ListarMoneda();
            //Envío a la variable del modelo
            propiedadesModel.MonedaL = oListaMoneda;

            //Llamando al metodo para llenar los datos de los check en la parte de caracteristicas
            var oListaCaractCheck = _PropiedadesData.ListarCarct();
            //Envío a la variable del modelo
            propiedadesModel.CaracteristicasL = oListaCaractCheck;

            //Llamando al metodo para llenar los datos para tipo de suelo
            var oListaSuelo = _PropiedadesData.ListarSuelo();
            //Envío a la variable del modelo
            propiedadesModel.SueloL = oListaSuelo;

            //Llamando al metodo para llenar los datos para unidades de medida de terreno
            var oListaUM = _PropiedadesData.ListarUMTerreno();
            //Envío a la variable del modelo
            propiedadesModel.UMedidaL = oListaUM;

            return View(propiedadesModel);
        }

        //Obtener el subtipo de la propiedad y enviarlo a través de un json 
        public JsonResult ObtSubtipoProp(int TipoP)
        {

            //Obteniendo el subtipo de la propiedad, con base al ID enviado del Tipo de propiedad
            var oListaSubtipo = _PropiedadesData.ListarSubtipoProp(TipoP);

            return Json(oListaSubtipo);

        }

        //Guardar una propiedad
        public IActionResult GuardarPropiedad(PropiedadesModel oPropiedad)
        {
            //Método para el guardado de la información 

            //Validación
            //if (!ModelState.IsValid)
            //    return View();

            var respuesta = _PropiedadesData.GuardarPropiedad(oPropiedad);


            if (respuesta)
                return RedirectToAction("Propiedades");
            else
                return View();
        }

        //Detalles de propiedad en Publicada 
        public IActionResult DetallesPropiedad()
        {
            return View();
        }

        //Dettalles propiedad No Publicada
        public IActionResult DetallesPropiedadNP()
        {
            return View();
        }

        //Dettalles propiedad En Proceso
        public IActionResult DetallesPropiedadPP()
        {
            return View();
        }

    }
}
