using adminRummet.Datos.Center;
using adminRummet.Models;
using adminRummet.Tools;
using Microsoft.AspNetCore.Mvc;

namespace adminRummet.Controllers
{
    public class BrokersController : Controller
    {
        //Consola de Brokers
        public IActionResult ConsolaBrokers()
        {
            return View();
        }

        //Método de nuevo usuario de Rummet
        public IActionResult NuevoBroker()
        {
            return View();
        }

        //Método para guardar los datos de Broker 


        //Edición del Bróker
        public IActionResult EdicionBroker()
        {
            return View();
        }


        //Método para el tablero del Broker - Cuando inicia resumen
        public class BrokerController : Controller
        {
            //Traer información del Center
            PropiedadesCenter _PropiedadesData = new PropiedadesCenter();
            HerramientasImagenes _HerramientasImagenes = new HerramientasImagenes();


            //Tablero de propiedades
            public IActionResult TableroBroker(int paginaPropPublicada = 1, int paginaPropNoPublicada = 1, int act = 1)
            {
                var propiedadesModel = new PropiedadesModelTablero();

                const int cantidadRegistrosPorPagina = 9; //Numero de elementos que se mostraran

                //Propiedades publicadas

                //Propiedades publicadas
                var oListaPropiedades = _PropiedadesData.ListarPropiedades("Aprobada");
                propiedadesModel.PropiedadesL = oListaPropiedades;

                //Imagenes de la propiedad publicada
                propiedadesModel.PropLImagenBase64 = _HerramientasImagenes.ConversionB64Propiedades("Aprobada");

                //Propiedades No publicadas
                oListaPropiedades = _PropiedadesData.ListarPropiedades("En revisión");
                propiedadesModel.PropiedadesNL = oListaPropiedades;

                //Imagenes de la propiedad NO publicada
                propiedadesModel.PropNLImagenBase64 = _HerramientasImagenes.ConversionB64Propiedades("En revisión");

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



                //var oListaPropiedades = _PropiedadesData.ListarPropiedades("Publicada");
                var oListaPropiedadesPublicadas = _PropiedadesData.ListarPropiedades("Aprobada");
                var sElementosPropPublicadas = oListaPropiedadesPublicadas.OrderByDescending(x => x.IDPropiedad).Skip((paginaPropPublicada - 1) * cantidadRegistrosPorPagina).Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistrosPropPublicadas = oListaPropiedadesPublicadas.Count();

                propiedadesModel.PropiedadesL = sElementosPropPublicadas;
                propiedadesModel.PaginaActualPropPublicada = paginaPropPublicada;
                propiedadesModel.TotalDeRegistrosPropPublicada = totalDeRegistrosPropPublicadas;
                propiedadesModel.RegistrosPorPaginaPropPublicada = cantidadRegistrosPorPagina;


                //propiedadesModel.PropiedadesL = oListaPropiedades;

                //Propiedades No publicadas
                //var oListaPropiedades = _PropiedadesData.ListarPropiedades("No publicada");
                var oListaPropiedadesNoPublicadas = _PropiedadesData.ListarPropiedades("En revisión");
                var sElementosPropNoPublicadas = oListaPropiedadesNoPublicadas.OrderByDescending(x => x.IDPropiedad).Skip((paginaPropNoPublicada - 1) * cantidadRegistrosPorPagina).Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistrosNoPublicadas = oListaPropiedadesNoPublicadas.Count();

                propiedadesModel.PropiedadesNL = sElementosPropNoPublicadas;
                propiedadesModel.PaginaActualPropNoPublicada = paginaPropNoPublicada;
                propiedadesModel.TotalDeRegistrosPropNoPublicada = totalDeRegistrosNoPublicadas;
                propiedadesModel.RegistrosPorPaginaPropNoPublicada = cantidadRegistrosPorPagina;


                //propiedadesModel.PropiedadesNL = sElementosPropPublicadas;
                //propiedadesModel.PaginaActual = pagina;
                //propiedadesModel.TotalDeRegistros = totalDeRegistros;
                //propiedadesModel.RegistrosPorPagina = cantidadRegistrosPorPagina;

                //propiedadesModel.PropiedadesNL = oListaPropiedades;


                //Propiedades Cerradas
                oListaPropiedades = _PropiedadesData.ListarPropiedades("Cerrada");
                propiedadesModel.PropiedadesCL = oListaPropiedades;

                if (act == 1)
                {
                    propiedadesModel.txtIdPropNoPublicadaAct = "defaultOpen2";
                    propiedadesModel.txtIdPropPublicadaAct = " ";
                    propiedadesModel.txtIdPropProcesoAct = " ";
                    propiedadesModel.txtIdPropCerradasAct = " ";
                }
                else if (act == 2)
                {
                    propiedadesModel.txtIdPropNoPublicadaAct = " ";
                    propiedadesModel.txtIdPropPublicadaAct = "defaultOpen2";
                    propiedadesModel.txtIdPropProcesoAct = " ";
                    propiedadesModel.txtIdPropCerradasAct = " ";
                }
                else if (act == 3)
                {
                    propiedadesModel.txtIdPropNoPublicadaAct = " ";
                    propiedadesModel.txtIdPropPublicadaAct = " ";
                    propiedadesModel.txtIdPropProcesoAct = "defaultOpen2";
                    propiedadesModel.txtIdPropCerradasAct = " ";
                }
                else if (act == 4)
                {
                    propiedadesModel.txtIdPropNoPublicadaAct = " ";
                    propiedadesModel.txtIdPropPublicadaAct = " ";
                    propiedadesModel.txtIdPropProcesoAct = " ";
                    propiedadesModel.txtIdPropCerradasAct = "defaultOpen2";
                }

                return View(propiedadesModel);
            }


        }


    }
}
