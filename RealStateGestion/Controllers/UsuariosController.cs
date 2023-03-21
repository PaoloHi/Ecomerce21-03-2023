using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealStateGestion.Datos.Center;
using RealStateGestion.Models;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RealStateGestion.Controllers
{
    public class UsuariosController : Controller
    {
        UsuariosCenter _Usuarios = new UsuariosCenter();

        /******************** INYECCIÓN DE DEPENDENCIA ********************/

        //Para subir los archivos y guardarlos dentro del disco LOCAL 
        private readonly IWebHostEnvironment _enviroment;
        //Para el registro de los usuarios con ayuda de Identity
        private readonly UserManager<IdentityUser> _userManager;

        //Envío dentro del constructor
        public UsuariosController(IWebHostEnvironment env, UserManager<IdentityUser> userManager)   
        {
            //Para uso de los archivos
            _enviroment = env;

            //Para uso de las cuentas de usuario 
            _userManager = userManager;
        }

        /******************** CONSOLA DE USUARIOS ********************/

        //Consola de usuarios llenado de información
        public IActionResult consUsuario()
        {
            var oListaUsuarios = _Usuarios.ListaUsuarios();
            //Envío de datos para la visualización de la alerta

            ViewBag.mssg = TempData["mssg"] as string;
            ViewBag.mssgE = TempData["mssgE"] as string;
            ViewBag.mssgA = TempData["mssgA"] as string;
            return View(oListaUsuarios);
        }


        /******************** CREACIÓN DE USUARIOS ********************/

        //Método de prueba para comparación de usuarios 

        public IActionResult Comprobar()
        {
            var email = "bucky@gmail.com";
            var respuesta = _Usuarios.ValidacionUsuario(email);

            if (respuesta)
            {
                Console.WriteLine("Si existe");
            }
            else
            {
                Console.WriteLine("No existe");
            }

            return View();
        }

        //Creación de un usuario vista
        public IActionResult cUsuario()
        {
            ViewBag.mssg = TempData["mssg"] as string;
            ViewBag.mssgE = TempData["mssgE"] as string;
            return View();
        }

        //Función que envia los datos al centro de datos 
        [HttpPost]
        public async Task<IActionResult> Guardar(UsuariosModel oUsuarios)
        {
            //METODO RECIBE EL OBJETO PARA GUARDARLO EN BD

            //Validación de información del modelo
            if (!ModelState.IsValid)
                return View();


            //Comprobación para saber que tipo de Rol es el usuario 
            var usuarioR = oUsuarios.rolUsuario.ToUpper();
            var broker = "BROKER";
            var brokers = "BROKERS";

            //Tipos de archivo y prefijo 
            string archTipo = "";
            string archPrefijo = "";


            //Inserción a tabla de de inicio de sesión
            var usuario = new AppUsuario { UserName = oUsuarios.email, Email = oUsuarios.email };
            var resultado = await _userManager.CreateAsync(usuario, "Megadeth18$");

            if (resultado.Succeeded)
            {
                //Se realizo la inserción del usuario
                //Envió de correo electrónico

                //Inserción a la tabla de datos usuarios
                var respuesta = _Usuarios.Guardar(oUsuarios);

                if (respuesta)
                {
                    //OBTENCIÓN DE DATOS PARA LA CREACIÓN DE CARPETAS Y NOMBRADO DE LOS ARCHIVOS

                    //Obtener ID del usuario
                    var emailUser = oUsuarios.email;
                    var oDatosA = _Usuarios.ObtenerCarpeta(emailUser);

                    //Obtener fecha y ID de usuario ingresado
                    var IDUsuarioCreado = oDatosA.IDusuario;

                    string fechaString = oDatosA.fechaAlta;

                    //Formateo de fecha para el nombre de la carpeta
                    DateTime fecha = DateTime.Parse(fechaString);
                    var formatoFecha = fecha.ToString("yyMMdd", CultureInfo.InvariantCulture);

                    //Borrado de espacios en nombre de usuario para concatenar en carpeta
                    string nombreUsuario = Regex.Replace(oUsuarios.nombreUsuario, @"\s", "");
                    string apellidoUsuario = Regex.Replace(oUsuarios.apellidoP, @"\s", "");

                    //Nombre a usar dentro de los archivos
                    string nombreApellidoUsuario = nombreUsuario + apellidoUsuario;

                    var carpetaUsuario = "";
                    var pathStringPrincipal = "";
                    var pathString = "";


                    /***********************GUARDADO DE IMAGEN EN CARPETA *********************************/

                    if (oUsuarios.imgPerfil != null)
                    {

                        //Comprobación de tamaño 
                        //Variable obtiene la medida bytes

                        var imgSize = (oUsuarios.imgPerfil).Length;

                        Console.WriteLine(imgSize);

                        //*** POSIBLE CONVERSIÓN 


                        /***********************CÓDIGO LAS IMAGENES DE PERFIL*********************************/
                        //CREACIÓN DE CARPETA DE USUARIO DE PERFIL 
                        //Creación de nombre de carpeta
                        carpetaUsuario = formatoFecha + "_" + IDUsuarioCreado + "_" + nombreUsuario;

                        //Ruta de guardado de la carpeta

                        // Colocando la ruta de la carpeta en donde se guardaran los documentos
                        pathStringPrincipal = System.IO.Path.Combine(_enviroment.ContentRootPath, "ImgPerfil");

                        //Estableciendo ruta y nombre de la carpta a crear
                        pathString = System.IO.Path.Combine(pathStringPrincipal, carpetaUsuario);

                        //Creación de la carpeta
                        System.IO.Directory.CreateDirectory(pathString);


                        /***********************CÓDIGO PARA CAMBIO DE NOMBRE AL ARCHIVO DE IMG PERFIL *********************************/
                        //Guid para el nombre de la imagen del usuario 
                        Guid g = Guid.NewGuid();

                        //Concatenación del guid con la extensión de la imagen de perfil del usuario
                        string nImgPerfilNombre = g + Path.GetExtension(oUsuarios.imgPerfil.FileName);

                        /******************************************************************************/


                        //Validación si trae nombre de archivos 
                        var fileName = System.IO.Path.Combine(pathStringPrincipal, carpetaUsuario, nImgPerfilNombre);

                        var path1 = System.IO.Path.Combine(pathStringPrincipal, carpetaUsuario);

                        //oUsuarios.imgPerfilNombre = oUsuarios.imgPerfil.FileName;
                        oUsuarios.imgPerfilNombre = nImgPerfilNombre;
                        oUsuarios.imgPerfiExt = Path.GetExtension(fileName);
                        oUsuarios.imgPerfilRuta = System.IO.Path.Combine(carpetaUsuario, nImgPerfilNombre);
                        oUsuarios.imgPerfiTam = imgSize;


                        //System.IO.Directory.CreateDirectory(pathString);
                        try
                        {
                            await oUsuarios.imgPerfil.CopyToAsync(new System.IO.FileStream(fileName, System.IO.FileMode.Create));
                            //Guardado de la ruta y elementos en la base de datos
                            _Usuarios.GuardarIMG(oUsuarios);
                        }
                        catch (Exception e)
                        {
                            string error = e.Message;
                        }


                    }

                    /***********************CÓDIGO PARA LOS ARCHIVOS*********************************/
                    //**Validación para ver si los archivos tienen información y crear la carpeta para luego depositar allí los archivos

                    if (oUsuarios.archivo1 != null || oUsuarios.archivo2 != null || oUsuarios.archivo3 != null || oUsuarios.archivo4 != null)
                    {
                        //Creación de la carpeta en dónde se guardaron los archivos del usuario
                        //Creación de nombre de carpeta
                        carpetaUsuario = formatoFecha + "_" + IDUsuarioCreado + "_" + nombreUsuario;

                        //Ruta de guardado de la carpeta
                        // Colocando la ruta de la carpeta en donde se guardaran los documentos
                        pathStringPrincipal = System.IO.Path.Combine(_enviroment.ContentRootPath, "Archivos_prueba");

                        //Estableciendo ruta y nombre de la carpta a crear
                        pathString = System.IO.Path.Combine(pathStringPrincipal, carpetaUsuario);

                        //Creación de la carpeta
                        System.IO.Directory.CreateDirectory(pathString);
                    }
                    if (oUsuarios.archivo1 != null)
                    {
                        //Tipo del archivo 
                        archTipo = "Comprobante";
                        //Prefijo para archivo
                        archPrefijo = "ComprobanteDeDomicilio_";
                        //Envío de información para guardado del archivo
                        Archivos(oUsuarios.archivo1, nombreUsuario, oUsuarios.email, pathStringPrincipal, carpetaUsuario, archPrefijo, archTipo);

                    }
                    if (oUsuarios.archivo2 != null)
                    {
                        //Tipo del archivo 
                        archTipo = "Cuenta";
                        //Prefijo para archivo
                        archPrefijo = "CuentaBancaria_";
                        //Envío de información para guardado del archivo
                        Archivos(oUsuarios.archivo2, nombreUsuario, oUsuarios.email, pathStringPrincipal, carpetaUsuario, archPrefijo, archTipo);


                    }
                    if (oUsuarios.archivo3 != null)
                    {
                        //Tipo del archivo 
                        archTipo = "Constancia";
                        //Prefijo para archivo
                        archPrefijo = "ConstanciaFiscal_";
                        //Envío de información para guardado del archivo
                        Archivos(oUsuarios.archivo3, nombreUsuario, oUsuarios.email, pathStringPrincipal, carpetaUsuario, archPrefijo, archTipo);

                    }
                    if (oUsuarios.archivo4 != null)
                    {
                        //Tipo del archivo 
                        archTipo = "Identificación";
                        //Prefijo para archivo
                        archPrefijo = "IdentificacionOficial_";
                        //Envío de información para guardado del archivo
                        Archivos(oUsuarios.archivo4, nombreUsuario, oUsuarios.email, pathStringPrincipal, carpetaUsuario, archPrefijo, archTipo);

                    }
                    /******************************************************************************/
                    TempData["mssg"] = "El usuario fue registrado con éxito";
                    return RedirectToAction("consUsuario");

                }
                else
                {
                    //REGRESA A LA VISTA CON MENSAJE DE ERROR
                    TempData["mssgE"] = "Error en los datos del usuario, por favor verifique que los datos hayan sido llenados correctamente.";
                    return RedirectToAction("cUsuario");

                }

            }
            else
            {
                //REGRESA A LA VISTA CON UN MENSAJE DE ERROR
                TempData["mssgE"] = "Error en la creación del usuario, verifique que el usuario no exista";
                return RedirectToAction("consUsuario");
            }

            return View();

        }

        /******************** EDICIÓN DE USUARIOS ********************/

        //Edición del usuario redirección a la vista
        public IActionResult edUsuario(int IDUser)
        {
            var usuariosModel = new UsuariosModel();

            //Listado de información general
            var oDatosG = _Usuarios.ObtenerG(IDUser);
            var oDatosB = _Usuarios.ObtenerB(IDUser);
            var oDatosI = _Usuarios.ObtenerImg(IDUser);

            //DatosG
            usuariosModel.IDusuario = oDatosG.IDusuario;
            usuariosModel.nombreUsuario = oDatosG.nombreUsuario;
            usuariosModel.apellidoP = oDatosG.apellidoP;
            usuariosModel.apellidoM = oDatosG.apellidoM;
            usuariosModel.email = oDatosG.email;
            usuariosModel.tel = oDatosG.tel;
            usuariosModel.IDrol = oDatosG.IDrol;
            usuariosModel.rfc = oDatosG.rfc;
            usuariosModel.curp = oDatosG.curp;
            usuariosModel.cel = oDatosG.cel;

            //DatosB
            usuariosModel.clabe = oDatosB.clabe;
            usuariosModel.banco = oDatosB.banco;

            //Datos IMG
            var path = Path.Combine(_enviroment.WebRootPath, "220918_55_BuckyAlejandro", "FONDO_PANTALLA.png");
            var path2 = Path.Combine(_enviroment.ContentRootPath, "220918_55_BuckyAlejandro", "FONDO_PANTALLA.png");

            var AVER = path2;
            //usuariosModel.imgPerfilRuta = oDatosI.imgPerfilRuta;

            usuariosModel.imgPerfilRuta = AVER;
            //DatosA


            //Listado de información bancaria
            ///var oDatosB = _Usuarios.ObtenerB(IDUser);

            //Listado de archivos 

            //var oDatosA = _Usuarios.Listar2(IDUser);

            //Envío de listados a traves del model 
            /*usuariosModel.datosG = oDatosG;
            usuariosModel.datosB = oDatosB;
            usuariosModel.datosA = oDatosA;*/

            //Envío de datos para la visualización de la alerta

            //Envío de alertas

            ViewBag.mssg = TempData["mssg"] as string;
            ViewBag.mssg = TempData["mssgA"] as string;
            ViewBag.mssgE = TempData["mssgE"] as string;
            return View(usuariosModel);

        }

        //Función que contiene los datos para enviar al centro de datos y realizar las consultas
        [HttpPost]
        public IActionResult edUsuario(UsuariosModel oUsuarios)
        {
            if (!ModelState.IsValid)
                return View();

            //Respuesta de datos 
            var respuesta = _Usuarios.Actualizacion(oUsuarios);

            //Respuesta de archivos

            if (respuesta)
            {
                TempData["mssgA"] = "El usuario fue actualizado con éxito";
                return RedirectToAction("consUsuario");
            }

            else
                return View();
        }


        /******************** CONSOLA DE USUARIOS ********************/


        //Función para inactivar usuario
        [HttpPost]
        public IActionResult incUsuario(int IDUser, string statusReg)
        {
            var eliminaUser = _Usuarios.ChangeStatus(IDUser, statusReg);
            //METODO SOLO DEVUELVE LA VISTA
            return RedirectToAction("cUsuario");
        }

        //Función para eliminar usuario 
        [HttpPost]
        public IActionResult eliminaUsuario(int IDUser)
        {
            var eliminaUser = _Usuarios.Eliminar(IDUser);
            //METODO SOLO DEVUELVE LA VISTA
            return RedirectToAction("cUsuario");
        }


        /******************** MÉTODO PARA EL GUARDADO DE ARCHIVOS *********************/
        public async Task Archivos(IFormFile archivo, string usuario, string email, string pathPrincipal, string carpetaUsuario, string archPrefijo, string archTipo)
        {
            //Medida del archivo
            var archSize = (archivo).Length;

            //Extensión del archivo
            string archExt = System.IO.Path.GetExtension(archivo.FileName);

            //Cambio de nombre del archivo, concatenado con los datos del usuario
            var archNombre = archPrefijo + usuario + archExt;

            //Carpeta personalizada y nombre del archivo que estamos guardando
            var archPath = System.IO.Path.Combine(carpetaUsuario, archNombre);

            //Envío al guardado del archivo con la ruta completa, carpeta personalizada y nombre del archivo
            var fileName = System.IO.Path.Combine(pathPrincipal, carpetaUsuario, archNombre);

            try
            {
                //Guardado del archivo
                await archivo.CopyToAsync(new System.IO.FileStream(fileName, System.IO.FileMode.Create));

                //Envío de arreglo para registro en la base de datos
                string[] datosArchivos = { email, archNombre, Convert.ToString(archSize), archExt, archPath, archTipo };
                _Usuarios.GuardarDOC(datosArchivos);

            }
            catch (Exception e)
            {
                string error = e.Message;
            }
        }

    }
}
