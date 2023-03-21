using Microsoft.AspNetCore.Mvc;
using RealStateGestion.Datos.Center;
using RealStateGestion.Models;
using System.Linq;

namespace RealStateGestion.Controllers
{
    public class PlanesController : Controller
    {

        //Traer 
        PlanesCenter _PlanesData = new PlanesCenter();


        //Método para el mostrado del formulario
        [HttpGet]
        public IActionResult Planes()
        {
            //Creando varaiable para almacenar valores en el modelo
            var planesModel = new PlanesModel();

            //Llamando al método Listar, el cual trae los datos principales de los planes
            var oListaPlanes = _PlanesData.Listar();

            //Llamando al método Listar2, el cual trae las caracteristicas seleccionadas de los planes
            var oListaPlanesCaract = _PlanesData.Listar2();

            //Almacenando dentro de las listas y poder obtenerlas en el front
            //Planes datos generales
            planesModel.planesG = oListaPlanes;
            //Planes características
            planesModel.planesCaract = oListaPlanesCaract;

            //Envío de datos para la visualización de la alerta

            ViewBag.mssg = TempData["mssg"] as string;
            ViewBag.mssgE = TempData["mssgE"] as string;
            return View(planesModel);
        }

        //Para la edición de planes 
        public IActionResult planesAct(int plan)
        {
            var planesModel = new PlanesModel();

            //Enviando el ID del plan al modelo
            planesModel.IDPlan = plan;

            //Para obtener los datos principales del Plan 
            var oListaPlanesG = _PlanesData.Listar();
            //Almacenando dentro de la lista declarada en el modelo
            planesModel.planesG = oListaPlanesG;

            //Esta lista contiene todas las caracteristicas para los planes
            //Llamando al método ListarCaractT, el cual trae todas las características a seleccionar de los planes
            var oListaPlanesCaractG = _PlanesData.ListarCaractG();
            //Almacenando dentro de la lista declarada en el modelo
            planesModel.planesCaractG = oListaPlanesCaractG;

            //Está lista contiene las caracteristicas particulares de cada plan
            var oListaCaractInd = _PlanesData.CaractSelect(plan);
            //Almacenando en la lista declara en el modelo
            planesModel.planesCaractIndv = oListaCaractInd;

            return View(planesModel);
        }
    }
}
