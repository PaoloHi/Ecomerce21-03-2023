using Microsoft.AspNetCore.Mvc;
using adminRummet.Datos.Center;
using adminRummet.Models;
using adminRummet.Tools;


//comentando la linea 

namespace adminRummet.Controllers
{
    public class PropiedadesController : Controller
    {
        //Traer información del Center
        PropiedadesCenter _PropiedadesData = new PropiedadesCenter();
        HerramientasImagenes _HerramientasImagenes = new HerramientasImagenes();


        //Tablero de propiedades
        public IActionResult PropiedadesTab(int paginaPropPublicada = 1, int paginaPropNoPublicada = 1, int act = 1)
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
            } else if (act == 2)
            {
                propiedadesModel.txtIdPropNoPublicadaAct = " ";
                propiedadesModel.txtIdPropPublicadaAct = "defaultOpen2";
                propiedadesModel.txtIdPropProcesoAct = " ";
                propiedadesModel.txtIdPropCerradasAct = " ";
            } else if (act == 3)
            {
                propiedadesModel.txtIdPropNoPublicadaAct = " ";
                propiedadesModel.txtIdPropPublicadaAct = " ";
                propiedadesModel.txtIdPropProcesoAct = "defaultOpen2";
                propiedadesModel.txtIdPropCerradasAct = " ";
            } else if (act == 4)
            {
                propiedadesModel.txtIdPropNoPublicadaAct = " ";
                propiedadesModel.txtIdPropPublicadaAct = " ";
                propiedadesModel.txtIdPropProcesoAct = " ";
                propiedadesModel.txtIdPropCerradasAct = "defaultOpen2";
            }

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

            var respuesta = _PropiedadesData.GuardarPropiedad(oPropiedad);

            if (respuesta)
            {
                TempData["SuccessMessage"] = "La propiedad se guardó exitosamente.";

                return RedirectToAction("PropiedadesTab");
            }

            else
                return View();
        }


        //Detalles de propiedad en Publicada 
        public IActionResult DetallesPropiedad(string PROPIEDADID)
        {
            var propiedadesModel = new PropiedadesModelPropiedad();

            var oDatosPropiedad = _PropiedadesData.ObtenerDatosPropiedadSelect(PROPIEDADID);

            propiedadesModel.PropiedadSelect = oDatosPropiedad;
            propiedadesModel.IDPropiedadG = oDatosPropiedad.IDPropiedadG;
            propiedadesModel.Titulo = oDatosPropiedad.Titulo;
            propiedadesModel.Superficie = oDatosPropiedad.Superficie;
            propiedadesModel.SuperficieC = oDatosPropiedad.SuperficieC;
            propiedadesModel.UM = oDatosPropiedad.UM;
            propiedadesModel.UMC = oDatosPropiedad.UMC;
            propiedadesModel.Abrev = oDatosPropiedad.Abrev;
            propiedadesModel.Operacion = oDatosPropiedad.Operacion;
            propiedadesModel.Precio = oDatosPropiedad.Precio;
            propiedadesModel.ISO = oDatosPropiedad.ISO;
            propiedadesModel.Vistas = oDatosPropiedad.Vistas;
            propiedadesModel.Visitas = oDatosPropiedad.Visitas;
            propiedadesModel.FPublicacion = oDatosPropiedad.FPublicacion;
            propiedadesModel.StatusPropiedad = oDatosPropiedad.StatusPropiedad;

            var oListaCaractNum = _PropiedadesData.ListarCaractPropEditarNum(PROPIEDADID);
            propiedadesModel.CaracteristicasNumPropiedadEditar = oListaCaractNum;

            //propiedadesModel.PropiedadGUID = PROPIEDADID;
            var oListaCaracteres = _PropiedadesData.LCaracteristicasD(PROPIEDADID);
            propiedadesModel.ListaCaracteristicasD = oListaCaracteres;


            return View(propiedadesModel);
        }


        //Dettalles propiedad No Publicada
        public IActionResult DetallesPropiedadNP(string PROPIEDADID)
        {
            var propiedadesModel = new
                PropiedadesModelPropiedad();

            var oDatosPropiedad = _PropiedadesData.ObtenerDatosPropiedadSelect(PROPIEDADID);

            propiedadesModel.PropiedadSelect = oDatosPropiedad;
            propiedadesModel.IDPropiedadG = oDatosPropiedad.IDPropiedadG;

            propiedadesModel.Titulo = oDatosPropiedad.Titulo;
            propiedadesModel.Superficie = oDatosPropiedad.Superficie;
            propiedadesModel.SuperficieC = oDatosPropiedad.SuperficieC;
            propiedadesModel.UM = oDatosPropiedad.UM;
            propiedadesModel.UMC = oDatosPropiedad.UMC;
            propiedadesModel.Abrev = oDatosPropiedad.Abrev;
            propiedadesModel.Operacion = oDatosPropiedad.Operacion;
            propiedadesModel.Precio = oDatosPropiedad.Precio;
            propiedadesModel.ISO = oDatosPropiedad.ISO;
            propiedadesModel.FPublicacion = oDatosPropiedad.FPublicacion;

            propiedadesModel.StatusPropiedad = oDatosPropiedad.StatusPropiedad;


            var oListaCaractNum = _PropiedadesData.ListarCaractPropEditarNum(PROPIEDADID);
            propiedadesModel.CaracteristicasNumPropiedadEditar = oListaCaractNum;

            var oListaCaracteres = _PropiedadesData.LCaracteristicasD(PROPIEDADID);
            propiedadesModel.ListaCaracteristicasD = oListaCaracteres;

            // propiedadesModel.PropiedadGUID = PROPIEDADID;

            var oDocumentosLista = _PropiedadesData.ListarDocumentosPropEditar(PROPIEDADID);
            propiedadesModel.DocumentosDetalles = oDocumentosLista;

            return View(propiedadesModel);

        }


        //Dettalles propiedad En Proceso
        public IActionResult DetallesPropiedadPP(string PROPIEDADID)
        {
            var propiedadesModel = new
                PropiedadesModelPropiedad();

            var oDatosPropiedad = _PropiedadesData.ObtenerDatosPropiedadSelect(PROPIEDADID);
            propiedadesModel.PropiedadSelect = oDatosPropiedad;
            propiedadesModel.IDPropiedadG = oDatosPropiedad.IDPropiedadG;

            propiedadesModel.Titulo = oDatosPropiedad.Titulo;
            propiedadesModel.Superficie = oDatosPropiedad.Superficie;
            propiedadesModel.SuperficieC = oDatosPropiedad.SuperficieC;
            propiedadesModel.UM = oDatosPropiedad.UM;
            propiedadesModel.UMC = oDatosPropiedad.UMC;
            propiedadesModel.Abrev = oDatosPropiedad.Abrev;
            propiedadesModel.Operacion = oDatosPropiedad.Operacion;
            propiedadesModel.Precio = oDatosPropiedad.Precio;
            propiedadesModel.ISO = oDatosPropiedad.ISO;
            propiedadesModel.StatusPropiedad = oDatosPropiedad.StatusPropiedad;
            propiedadesModel.FPublicacion = oDatosPropiedad.FPublicacion;


            var oListaCaractNum = _PropiedadesData.ListarCaractPropEditarNum(PROPIEDADID);
            propiedadesModel.CaracteristicasNumPropiedadEditar = oListaCaractNum;

            var oListaCaracteres = _PropiedadesData.LCaracteristicasD(PROPIEDADID);
            propiedadesModel.ListaCaracteristicasD = oListaCaracteres;

        

            return View(propiedadesModel);
        }


        public IActionResult EditarPropiedad(string PROPIEDADID)
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
            var oListaCaractCheck = _PropiedadesData.ListarCaractPropEditarCheck(PROPIEDADID);
            //Envío a la variable del modelo
            propiedadesModel.CaracteristicasCheckPropiedadEditar = oListaCaractCheck;

            // Llamando al metodo para llenar los datos de los elementos num en la part de caracteristicas
            var oListaCaractNum = _PropiedadesData.ListarCaractPropEditarNum(PROPIEDADID);
            // Envío a la variable del modelo
            propiedadesModel.CaracteristicasNumPropiedadEditar = oListaCaractNum;

            // LLamando al metodo para obtener las imagenes de la propiedad
            var oListaImg = _PropiedadesData.ListarImgPropEditar(PROPIEDADID);
            // Envio a la variable del modelo
            propiedadesModel.ImagenesPropiedadEditar = oListaImg;

            // LLamando al metodo para obtener los documentos de la propiedad
            var oListaDoc = _PropiedadesData.ListarDocumentosPropEditar(PROPIEDADID);
            // Envio a la variable del modelo
            propiedadesModel.DocumentosPropiedadEditar = oListaDoc;

            //Llamando al metodo para llenar los datos para tipo de suelo
            var oListaSuelo = _PropiedadesData.ListarSuelo();
            //Envío a la variable del modelo
            propiedadesModel.SueloL = oListaSuelo;

            //Llamando al metodo para llenar los datos para unidades de medida de terreno
            var oListaUM = _PropiedadesData.ListarUMTerreno();
            //Envío a la variable del modelo
            propiedadesModel.UMedidaL = oListaUM;


            // Llamando al metodo para obtener los datos de la propiedad a editar
            var oDatosPropiedad = _PropiedadesData.ObtenerPropiedadEditar(PROPIEDADID);
            // Envio de datos al modelo
            propiedadesModel.PropiedadEditar = oDatosPropiedad;

            propiedadesModel.PropiedadGUID = PROPIEDADID;

            return View(propiedadesModel);
        }

        
        public IActionResult eliminarImagen(int IDimgPropiedad)
        {
            var eliminaImg = _PropiedadesData.EliminarImgPropiedad(IDimgPropiedad);
            //METODO SOLO DEVUELVE LA VISTA
            return RedirectToAction("Propiedades");
        }

        
        public IActionResult eliminarDoc(int IDdocPropiedad)
        {
            var eliminaDoc = _PropiedadesData.EliminarDocPropiedad(IDdocPropiedad);
            //METODO SOLO DEVUELVE LA VISTA
            return RedirectToAction("Propiedades");
        }

        
        public IActionResult GuardarEditarPropiedad(PropiedadesModel oPropiedad)
        {
            var respuesta = _PropiedadesData.GuardarEditarPropiedad(oPropiedad);

            if (respuesta)
                return RedirectToAction("PropiedadesTab");
            else
                return View();

        }

        
        [HttpPost]
        public IActionResult Verificar(string correoPropietario)
        {
            var verificarCorreo = _PropiedadesData.Verificar(correoPropietario);

            if (verificarCorreo.Nombre == null)
            {
                return Json(new { success = false, message = "Error: el correo no está verificado" });
            }

            return Json(new { success = true, verificarCorreo = verificarCorreo });
        }

        
        public IActionResult ListarColonias(string CodigoPostal)
        {
            List<Colonias> lst  = _PropiedadesData.ListarColonias(CodigoPostal);
                    
            return Json(lst);
        }


        [HttpPost]
        public IActionResult VerificarMunicipio(string CODIGOPOSTAL) 
        {
            var verifcarMunicipio = _PropiedadesData.ObtenerMunicipio(CODIGOPOSTAL);

            return Json(new { success = true, MunicipioCorrecto = verifcarMunicipio});

        }

        [HttpPost]
        public IActionResult AprobarDocumento(string IDDOCPROPIEDAD) {

            

            return Json(new { });
        }

        [HttpPost]
        public IActionResult RecahzarDocumento (string IDDOCPROPIEDAD)
        {

            return Json(new { });
        }

     
    }
}
