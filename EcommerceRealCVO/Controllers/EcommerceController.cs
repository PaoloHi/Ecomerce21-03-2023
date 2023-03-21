using EcommerceRealCVO.Datos.Center;
using EcommerceRealCVO.Models;
using EcommerceRealCVO.Tools;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EcommerceRealCVO.Controllers
{
    public class EcommerceController : Controller
    {
        //PLANES
        PlanesCenter _PlanesData = new PlanesCenter();

        //PROPIEDADES
        PropiedadesCenter _PropiedadesData = new PropiedadesCenter();

        //ECOMMERCE 
        EcommerceCenter _EcommerceData = new EcommerceCenter();

        //CONTACTO 
        ContactoCenter _ContactoData = new ContactoCenter();

        //INTEGRACION
        IntegracionCenter _IntegracionData = new IntegracionCenter();

        //FILTROS 
        Filtros _FiltrosTool = new Filtros();

        //Variables de Ubicación 

        string col;
        string mun;
        string edo;
        string pais;
        string? Ac;
        //Lista de los id de las propiedades que aparecen en sugerencias dentro de la propiedad
        List<int> idPropiedades = new List<int>();

        [HttpGet] //Método para el mostrado del formulario
        public IActionResult PlanesEcommerce()
        {

            var planesModel = new PlanesModel();

            var oListaPlanes = _PlanesData.Listar();
            var oListaPlanesCaract = _PlanesData.Listar2();

            planesModel.planesG = oListaPlanes;
            planesModel.planesCaract = oListaPlanesCaract;

            //Envío de datos para la visualización de la alerta

            ViewBag.mssg = TempData["mssg"] as string;
            ViewBag.mssgE = TempData["mssgE"] as string;
            return View(planesModel);
        }

        [HttpGet]
        [Route("propiedades-rummet/{Ac?}/{Gen?}")]
        [Route("propiedades-rummet/{Ac?}/{Tipo?}/{Gen?}")]
        public IActionResult Propiedades(string? Ac, string? Tipo, string? Gen, string? Propiedad)
        {
            //Tipo = 1;
            var propiedadesModel = new PropiedadesModel();
            var ecommerceModel = new EcommerceModel();

            var oListaFiltrosT = _FiltrosTool.ParametrosFiltro(Ac, Tipo, Gen, Propiedad);

            if (Gen != null)
            {
                Gen = Gen.Replace('-', ' ');
            }

            //Console.WriteLine("CUENTA DE LAS PROPIEDADES PROPIEDADES " + idPropiedades.Count());
            //Propiedades sin filtro 
            if (Ac == null && Propiedad == null)
            {
                //Listado de propiedades y datos de propiedades

                var oListaPropiedades = _PropiedadesData.ListarPropiedades();
                propiedadesModel.PropiedadesGL = oListaPropiedades;

                //Listado de las características de las propiedades
                propiedadesModel.LCaracteristicas = _PropiedadesData.ListarCaracteristicas();
            
                //Listado de las imagenes de las propiedades
                var oListaImagenes = _PropiedadesData.ListarImagenesProp();
                //Inserción de imagenes
                // Convercion de la Imagenes en Base64
                var oListaBase64 = new List<base64PropiedadesEcomm>();


                foreach (var datos in oListaImagenes)
                {
                    var path = System.IO.Directory.GetCurrentDirectory() + "\\..\\RealStateGestion\\ImagenesPropiedad\\" + datos.RutaImagenPropiedad.ToString();

                    byte[] imageArray = System.IO.File.ReadAllBytes(path);
                    string base64Image = Convert.ToBase64String(imageArray);
                    string src64 = "data:image/jpg/jpeg/png;base64," + base64Image; // parte que se agregara en src=

                    //txt64.Insert(i, src64); // se codigo base64 en la 

                    //Console.WriteLine(datos.IDpropiedad);
                    oListaBase64.Add(new base64PropiedadesEcomm()
                    {
                        IDpropiedadBase64 = Convert.ToInt32(datos.IDpropiedad),
                        RutaImagen = datos.RutaImagenPropiedad.ToString(),
                        txtBase64 = src64.ToString(),
                    });
                }

                //Enviando a la lista los resultado de las URL en base64
                propiedadesModel.LImagenBase64 = oListaBase64;
            }
            else if (Ac != null)
            {
                //Esta es la lista de propiedades cuando es aplicado el filtro principal
                var oListaPropiedades = _PropiedadesData.ListarPropiedadesB(oListaFiltrosT);
                propiedadesModel.PropiedadesGL = oListaPropiedades;

                //Listado de las características de las propiedades
                propiedadesModel.LCaracteristicas = _PropiedadesData.ListarCaracteristicasB(oListaFiltrosT);

                var oListaImagenes = _PropiedadesData.ListarImagenesPropB(oListaFiltrosT);
                //Inserción de imagenes
                // Convercion de la Imagenes en Base64
                var oListaBase64 = new List<base64PropiedadesEcomm>();
                foreach (var datos in oListaImagenes)
                {
                    var path = System.IO.Directory.GetCurrentDirectory() + "\\..\\RealStateGestion\\ImagenesPropiedad\\" + datos.RutaImagenPropiedad.ToString();

                    byte[] imageArray = System.IO.File.ReadAllBytes(path);
                    string base64Image = Convert.ToBase64String(imageArray);
                    string src64 = "data:image/jpg/jpeg/png;base64," + base64Image; // parte que se agregara en src=

                    //txt64.Insert(i, src64); // se codigo base64 en la 

                    //Console.WriteLine(datos.IDpropiedad);
                    oListaBase64.Add(new base64PropiedadesEcomm()
                    {
                        IDpropiedadBase64 = Convert.ToInt32(datos.IDpropiedad),
                        RutaImagen = datos.RutaImagenPropiedad.ToString(),
                        txtBase64 = src64.ToString(),
                    });


                }

                //Enviando a la lista los resultado de las URL en base64
                propiedadesModel.LImagenBase64 = oListaBase64;

            }

            else if (Propiedad != null)
            {

                //Filtro por el tipo de propiedad 
                //Propiedades sin filtro 
                var oListaPropiedades = _PropiedadesData.ListarPropiedadesTipo(Propiedad);
                propiedadesModel.PropiedadesGL = oListaPropiedades;

                //Listado de las características de las propiedades
                propiedadesModel.LCaracteristicas = _PropiedadesData.ListarCaracteristicas();


                var oListaImagenes = _PropiedadesData.ListarImagenesPropTipo(Propiedad);
                //Inserción de imagenes
                // Convercion de la Imagenes en Base64
                var oListaBase64 = new List<base64PropiedadesEcomm>();


                foreach (var datos in oListaImagenes)
                {
                    var path = System.IO.Directory.GetCurrentDirectory() + "\\..\\RealStateGestion\\ImagenesPropiedad\\" + datos.RutaImagenPropiedad.ToString();

                    byte[] imageArray = System.IO.File.ReadAllBytes(path);
                    string base64Image = Convert.ToBase64String(imageArray);
                    string src64 = "data:image/jpg/jpeg/png;base64," + base64Image; // parte que se agregara en src=

                    //txt64.Insert(i, src64); // se codigo base64 en la 

                    // Console.WriteLine(datos.IDpropiedad);
                    oListaBase64.Add(new base64PropiedadesEcomm()
                    {
                        IDpropiedadBase64 = Convert.ToInt32(datos.IDpropiedad),
                        RutaImagen = datos.RutaImagenPropiedad.ToString(),
                        txtBase64 = src64.ToString(),
                    });


                }

                //Enviando a la lista los resultado de las URL en base64
                propiedadesModel.LImagenBase64 = oListaBase64;


            }

            if (idPropiedades.Count != 0)
            {
                //Listado de propiedades por filtro compuesto
                var oListaPropiedades = _PropiedadesData.ListarPropiedadesFC(idPropiedades);
                propiedadesModel.PropiedadesGL = oListaPropiedades;

                var oListaImagenes = _PropiedadesData.ListarImagenesPropFC(idPropiedades);
                //Inserción de imagenes
                // Convercion de la Imagenes en Base64
                var oListaBase64 = new List<base64PropiedadesEcomm>();


                foreach (var datos in oListaImagenes)
                {
                    var path = System.IO.Directory.GetCurrentDirectory() + "\\..\\RealStateGestion\\ImagenesPropiedad\\" + datos.RutaImagenPropiedad.ToString();

                    byte[] imageArray = System.IO.File.ReadAllBytes(path);
                    string base64Image = Convert.ToBase64String(imageArray);
                    string src64 = "data:image/jpg/jpeg/png;base64," + base64Image; // parte que se agregara en src=

                    //txt64.Insert(i, src64); // se codigo base64 en la 

                    //                    Console.WriteLine(datos.IDpropiedad);
                    oListaBase64.Add(new base64PropiedadesEcomm()
                    {
                        IDpropiedadBase64 = Convert.ToInt32(datos.IDpropiedad),
                        RutaImagen = datos.RutaImagenPropiedad.ToString(),
                        txtBase64 = src64.ToString(),
                    });


                }

                //Enviando a la lista los resultado de las URL en base64
                propiedadesModel.LImagenBase64 = oListaBase64;

            }

            //Listado de tipo de propiedades para el filtrado de propiedades
            propiedadesModel.TPropiedadL = _PropiedadesData.ListarTipoProp();

            propiedadesModel.UMedidaL = _PropiedadesData.ListarUMTerreno();

            //Listado de caracteristicas para el filtrado de propiedades
            propiedadesModel.CaracteristicasGL = _PropiedadesData.ListarCarct("Generales");
            propiedadesModel.CaracteristicasSL = _PropiedadesData.ListarCarct("Servicios");
            propiedadesModel.CaracteristicasEL = _PropiedadesData.ListarCarct("Exteriores");
            propiedadesModel.CaracteristicasAL = _PropiedadesData.ListarCarct("Ambientes");

            return View(propiedadesModel);
        }

        //Método para el envío de información de los filtros 
        [HttpPost]
        public IActionResult FiltroPropiedades(PropiedadesModel oPropiedadL)
        {
            //Lista para guardado de Amenidades seleccionadas 
            List<int> amenidadesSelec = new List<int>();

            //Añadiendo las características Generales
            foreach (var caracteristicaG in oPropiedadL.CaracteristicasGL)
            {
                //Validación si el elemento fue seleccionado de la lista
                if (caracteristicaG.isSelected == true)
                {
                    //Añadiendo a la lista
                    amenidadesSelec.Add(caracteristicaG.IDCaracteristica);
                }
            }
            //Añadiendo las características Servicios
            foreach (var caracteristicaS in oPropiedadL.CaracteristicasSL)
            {
                //Validación si el elemento fue seleccionado de la lista
                if (caracteristicaS.isSelected == true)
                {
                    //Añadiendo a la lista
                    amenidadesSelec.Add(caracteristicaS.IDCaracteristica);
                }
            }
            //Añadiendo las características Exteriores
            foreach (var caracteristicaE in oPropiedadL.CaracteristicasEL)
            {
                //Validación si el elemento fue seleccionado de la lista
                if (caracteristicaE.isSelected == true)
                {
                    //Añadiendo a la lista
                    amenidadesSelec.Add(caracteristicaE.IDCaracteristica);
                }
            }
            //Añadiendo las características Ambientales
            foreach (var caracteristicaA in oPropiedadL.CaracteristicasAL)
            {
                //Validación si el elemento fue seleccionado de la lista
                if (caracteristicaA.isSelected == true)
                {
                    //Añadiendo a la lista
                    amenidadesSelec.Add(caracteristicaA.IDCaracteristica);
                }
            }

            //Obtener ID de propiedades con base a los filtros aplicados

            oPropiedadL.LPropiedadesF = _PropiedadesData.LPropiedadesF(oPropiedadL, amenidadesSelec);



            //Extrayendo los ID de las propiedades dentro de las propiedades sugeridas
            foreach (var IDIMG in oPropiedadL.LPropiedadesF)
            {
                idPropiedades.Add(IDIMG.IDPropiedad);

            }

            Console.WriteLine("CUENTA DE LAS PROPIEDADES FILTRO " + idPropiedades.Count());
            return RedirectToAction("Propiedades", "Ecommerce", new { IDPropiedades = idPropiedades });
        }


        [HttpPost]
        public IActionResult Prueba(EcommerceModel ecommerce)
        {
            //Variables de frase
            string accion = "";
            string municipio = "", colonia = "", estado = "";
            string general = "";
            string tipo = "";

            //Comprobación de variables para envío a URL 
            if (ecommerce.Accion == "Comprar")
            {
                accion = "en-venta";
            }
            else if (ecommerce.Accion == "Rentar")
            {
                accion = "en-renta";

            }
            /*******************************************************************/
            if (ecommerce.Tpropiedad != null)
            {
                tipo = ecommerce.Tpropiedad.ToLower();
            }

            /*******************************************************************/
            //Validar cuando es una ubicación
            if (ecommerce.Municipio != null || ecommerce.CE != null || ecommerce.Colonia != null)
            {
                //Este condicional aplica generalmente al seleccionar alguna colonia de la CDMX
                if (ecommerce.Municipio == ecommerce.CE && ecommerce.Colonia != "undefined")
                {
                    //Obtener Municipio y Estado 
                    var oDatosUbicacion = _EcommerceData.ComprobacionUbi(ecommerce.Colonia, ecommerce.CE);
                    ecommerce.Ubicaciones = oDatosUbicacion;

                    foreach (var datos in oDatosUbicacion)
                    {
                        municipio = datos.Municipio;
                        estado = datos.Estado;

                    }
                    colonia = "en " + ecommerce.Colonia;
                    general = colonia + " " + municipio + " " + estado;
                    general = Regex.Replace(general.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
                    general = general.Replace(' ', '-');
                    general = general.ToLower();
                }

                //Cuando no hay colonia
                if (ecommerce.Colonia == "undefined")
                {
                    //Cuando se coloca el municipio y el estado correctamente
                    municipio = "en " + ecommerce.Municipio;
                    estado = " " + ecommerce.CE;
                    general = municipio + estado;
                    general = Regex.Replace(general.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
                    general = general.Replace(' ', '-');
                    general = general.ToLower();

                }

                //Cuando los 3 elementos vienen completos
                if (ecommerce.Municipio != ecommerce.CE && ecommerce.Colonia != "undefined")
                {
                    general = "en " + ecommerce.Colonia + " " + ecommerce.Municipio + " " + ecommerce.CE;
                    general = Regex.Replace(general.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
                    general = general.Replace(' ', '-');
                    general = general.ToLower();

                }

                //Cuando no hay municipio 
                if (ecommerce.Municipio == "undefined" && ecommerce.Colonia != "undefined")
                {
                    general = "en col " + ecommerce.Colonia + " " + ecommerce.CE;
                    general = Regex.Replace(general.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
                    general = general.Replace(' ', '-');
                    general = general.ToLower();
                }

            }
            else if (ecommerce.UbiCarct != null && (ecommerce.Municipio == null || ecommerce.CE == null || ecommerce.Colonia == null))
            {
                general = "con " + ecommerce.UbiCarct;
                general = Regex.Replace(general.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
                general = general.Replace(' ', '-');
                general = general.ToLower();
            }

            Console.WriteLine("General " + ecommerce.UbiCarct);

            //return View();
            return RedirectToAction("Propiedades", "Ecommerce", new { Ac = accion, Tipo = tipo, Gen = general });

        }

        [Route("contactanos-grupo-cvo")]
        public IActionResult Contacto()
        {
            ViewBag.mssg = TempData["mssg"] as string;
            ViewBag.mssgE = TempData["mssgE"] as string;
            return View();
        }

        //Método para guardar la información de contacto
        [HttpPost]
        public IActionResult GuardarContacto(ContactoModel oContacto)
        {
            // oContacto.Tipo = "Contacto";
            //METODO RECIBE EL OBJETO PARA GUARDARLO EN BD
            if (!ModelState.IsValid)
                return View();

            var respuesta = _ContactoData.Guardar(oContacto);

            if (respuesta)
            {
                TempData["mssg"] = "Nos pondremos en contacto contigo en la fecha y hora acordado.";
                return RedirectToAction("Contacto");
            }

            else
            {
                return RedirectToAction("Contacto");
            }

        }

        //Método de que carga la información de la propiedad seleccionada
        public IActionResult PropiedadesVerMas(string Propiedad)
        {

            //Variables usadas para almacenar los valores de ID y posteriormente traer las imagenes de las propiedades en sugernecias
            var almacenador = "";
            //var idPropiedades = "";

            var propiedadesModel = new PropiedadSeleccionada();
            //Instancia de los datos de la propiedad 
            var oDatosG = _EcommerceData.ObtenerDatosProp(Propiedad);

            //Información general de la propiedad
            //Variables del modelo
            propiedadesModel.Titulo = oDatosG.Titulo;
            propiedadesModel.Descripcion = oDatosG.Descripcion;
            propiedadesModel.ISO = oDatosG.ISO;
            propiedadesModel.Precio = oDatosG.Precio;
            propiedadesModel.Direccion = oDatosG.Direccion;
            propiedadesModel.Calle = oDatosG.Calle;
            propiedadesModel.NoInt = oDatosG.NoInt;
            propiedadesModel.NoExt = oDatosG.NoExt;
            propiedadesModel.Colonia = oDatosG.Colonia;
            propiedadesModel.Municipio = oDatosG.Municipio;
            propiedadesModel.Estado = oDatosG.Estado;
            propiedadesModel.Pais = oDatosG.Pais;
            propiedadesModel.CP = oDatosG.CP;
            propiedadesModel.Superficie = oDatosG.Superficie;
            propiedadesModel.UM = oDatosG.UM;
            propiedadesModel.SuperficieC = oDatosG.SuperficieC;
            propiedadesModel.UMC = oDatosG.UMC;
            propiedadesModel.Tipo = oDatosG.Tipo;
            propiedadesModel.Latitud = oDatosG.Latitud;
            propiedadesModel.Longitud = oDatosG.Longitud;

            //Características número 
            var oListaCaracB = _EcommerceData.ListarCaracteristicasB(Propiedad, "num");
            propiedadesModel.CaracteristicasBas = oListaCaracB;

            //Características check
            var oListaCaracC = _EcommerceData.ListarCaracteristicasB(Propiedad, "check");
            propiedadesModel.CaracteristicasGen = oListaCaracC;

            //Características adicionales
            var oListaCaracA = _EcommerceData.ListarCaracteristicasB(Propiedad, "adicional");
            propiedadesModel.CaracteristicasBasA = oListaCaracA;

            //Fotografías de la propiedad
            //Trayendo los datos correspondientes de las imagenes
            var oListaImgPropiedad = _EcommerceData.ListarImagenProp(Propiedad);
            propiedadesModel.ImgPropiedad = oListaImgPropiedad;

            var count = 0;
            var total = oListaImgPropiedad.Count;
            var primeras = total - 2;

            var oListaBase64 = new List<base64PropiedadDestacada>();
            var oListaBase642 = new List<base64PropiedadSeleccionada>();

            foreach (var datos in oListaImgPropiedad)
            {
                var path = System.IO.Directory.GetCurrentDirectory() + "\\..\\RealStateGestion\\ImagenesPropiedad\\" + datos.RutaImagenPropiedad.ToString();

                byte[] imageArray = System.IO.File.ReadAllBytes(path);
                string base64Image = Convert.ToBase64String(imageArray);
                string src64 = "data:image/jpg/jpeg/png;base64," + base64Image; // parte que se agregara en src=

                //txt64.Insert(i, src64); // se codigo base64 en la 
                count = count + 1;

                if (count <= primeras)
                {
                    oListaBase64.Add(new base64PropiedadDestacada()
                    {
                        //IDpropiedadBase64 = Convert.ToInt32(datos.IDpropiedad),
                        RutaImagen = datos.RutaImagenPropiedad.ToString(),
                        txtBase64 = src64.ToString(),
                    });
                }
                else
                {
                    oListaBase642.Add(new base64PropiedadSeleccionada()
                    {
                        //IDpropiedadBase64 = Convert.ToInt32(datos.IDpropiedad),
                        RutaImagen = datos.RutaImagenPropiedad.ToString(),
                        txtBase64 = src64.ToString(),
                    });
                }

            }

            //Enviando a la lista los resultado de las URL en base64
            propiedadesModel.LImagenBase64S = oListaBase64;
            propiedadesModel.LImagenBase64S2 = oListaBase642;


            /*******************************************************************/

            //Lista de propiedades sugeridas
            var oListaPropiedades = _EcommerceData.ListarPropiedadesS(Propiedad);
            propiedadesModel.PropiedadesSL = oListaPropiedades;

            //Lista de los id de las propiedades que aparecen en sugerencias dentro de la propiedad
            List<int> idPropiedades = new List<int>();

            //Extrayendo los ID de las propiedades dentro de las propiedades sugeridas
            foreach (var IDIMG in oListaPropiedades)
            {
                idPropiedades.Add(IDIMG.IDPropiedad);

            }
            //Variable final con los ID de la spropiedades concatenado para enviar al SP
            var oListaPropiedadesImgS = _EcommerceData.ListarImagenPropS(idPropiedades);

            //Inserción de imagenes
            // Convercion de la Imagenes en Base64
            var oListaBase64Sug = new List<base64PropiedadDestacada>();


            foreach (var datos in oListaPropiedadesImgS)
            {
                var path = System.IO.Directory.GetCurrentDirectory() + "\\..\\RealStateGestion\\ImagenesPropiedad\\" + datos.RutaImagenPropiedad.ToString();

                byte[] imageArray = System.IO.File.ReadAllBytes(path);
                string base64Image = Convert.ToBase64String(imageArray);
                string src64 = "data:image/jpg/jpeg/png;base64," + base64Image; // parte que se agregara en src=

                //txt64.Insert(i, src64); // se codigo base64 en la 

                //Console.WriteLine(datos.IDpropiedad);
                oListaBase64Sug.Add(new base64PropiedadDestacada()
                {
                    IDpropiedadBase64 = Convert.ToInt32(datos.IDPropiedad),
                    RutaImagen = datos.RutaImagenPropiedad.ToString(),
                    txtBase64 = src64.ToString(),
                });

            }

            //Enviando a la lista los resultado de las URL en base64
            propiedadesModel.LImagenBase64Sugerida = oListaBase64Sug;


            return View(propiedadesModel);
        }

        [HttpPost]
        public async Task<IActionResult> MasInformacion([FromBody] PropiedadesModel oMasInformacion)
        {
            var bitrixModel = new BitrixLIModel();

            oMasInformacion.TipoLI = 1;
            var respuesta = _PropiedadesData.LeadInteresadoMI(oMasInformacion);
            if (respuesta)
            {
                Console.WriteLine("Afirmativo");
                var url = "http://bitrix-rummet.com:8080/api/bitrix/lead";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                var body = new
                {
                    nombre = oMasInformacion.NombreMI,
                    apellidoPaterno = oMasInformacion.ApellidoPMI,
                    apellidoMaterno = oMasInformacion.ApellidoMMI,
                    telefono = oMasInformacion.NumeroTMI,
                    correo = oMasInformacion.CorreoMI,
                    tipo = 1,
                    productoId = 110
                };
                var bodyy = JsonConvert.SerializeObject(body);
                request.AddBody(bodyy, "application/json");
                RestResponse response = await client.ExecuteAsync(request);
                var output = response.Content;
                Console.WriteLine(response.Content);

                if (response.IsSuccessful)
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var data = JsonSerializer.Deserialize<BitrixLIModel>(response.Content, options);

                    //Obtener ID de LEAD ingresado
                    var leadIB = _IntegracionData.IDLead(oMasInformacion);

                    if (leadIB != 0)
                    {
                        data.IDRummet = leadIB;
                    }
                    else
                    {
                        data.IDRummet = 0;
                    }

                    data.Accion = "Más información - Exitoso";
                    data.Proveedor = "Bitrix";
                    data.Tipo = 1;
                    var integracionEntrada = _IntegracionData.IntegracionEntrada(data);
                   // TempData["mssg"] = "Nos pondremos en contacto contigo en la fecha y hora acordado.";

                    return Json(new { success = true });

                }
                else
                {
                    Console.WriteLine($"Error al hacer la solicitud: {response.ErrorMessage}");
                }

                TempData["mssg"] = "Nos pondremos en contacto contigo en la fecha y hora acordado.";
                return RedirectToAction("PlanesEcommerce", "Ecommerce");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgendarVisita([FromBody] PropiedadesModel oMasInformacion)
        {
            var bitrixModel = new BitrixLIModel();

            oMasInformacion.TipoLI = 2;
            var respuesta = _PropiedadesData.LeadInteresadoMI(oMasInformacion);
            if (respuesta)
            {
                Console.WriteLine("Afirmativo");
                var url = "http://bitrix-rummet.com:8080/api/bitrix/lead";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                var body = new
                {
                    nombre = oMasInformacion.NombreMI,
                    apellidoPaterno = oMasInformacion.ApellidoPMI,
                    apellidoMaterno = oMasInformacion.ApellidoMMI,
                    telefono = oMasInformacion.NumeroTMI,
                    correo = oMasInformacion.CorreoMI,
                    //fechaHora=oMasInformacion.FechaMI
                    tipo = 2,
                    productoId = 110
                };
                var bodyy = JsonConvert.SerializeObject(body);
                request.AddBody(bodyy, "application/json");
                RestResponse response = await client.ExecuteAsync(request);
                var output = response.Content;
                Console.WriteLine(response.Content);

                if (response.IsSuccessful)
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var data = JsonSerializer.Deserialize<BitrixLIModel>(response.Content, options);

                    //Obtener ID de LEAD ingresado
                    var leadIB = _IntegracionData.IDLead(oMasInformacion);

                    if (leadIB != 0)
                    {
                        data.IDRummet = leadIB;
                    }
                    else
                    {
                        data.IDRummet = 0;
                    }

                    data.Accion = "Agendar Visita - Exitoso";
                    data.Proveedor = "Bitrix";
                    data.Tipo = 2;
                    var integracionEntrada = _IntegracionData.IntegracionEntrada(data);
                  //  TempData["mssg"] = "Nos pondremos en contacto contigo en la fecha y hora acordado.";

                    return Json(new { success = true });

                    Console.WriteLine(data);
                }
                else
                {
                    Console.WriteLine($"Error al hacer la solicitud: {response.ErrorMessage}");
                }

                TempData["mssg"] = "Nos pondremos en contacto contigo en la fecha y hora acordado.";
                return Json(new { success = true });

            }
            return View();
        }

    }

}