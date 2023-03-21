using EcommerceRealCVO.Datos.Center;
using EcommerceRealCVO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Text.Json;

namespace EcommerceRealCVO.Controllers
{
    public class CuentasController : Controller
    {
        //Inyeccion de dependencias 
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        //CONTACTO 
        ContactoCenter _ContactoData = new ContactoCenter();

        //INTEGRACION
        IntegracionCenter _IntegracionData = new IntegracionCenter();

        //Creación de constructor
        public CuentasController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        OnboardingBroker _OnboardingBroker = new OnboardingBroker();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AlertaProvisional()
        {
            ViewBag.mssg = TempData["mssg"] as string;
            ViewBag.mssgE = TempData["mssgE"] as string;
            return View();
        }


        [HttpGet] //Método para el mostrado del formulario
        public async Task<IActionResult> Registro(string? returnurl = null)
        {

            ViewData["ReturnUrl"] = returnurl;

            RegistroVModel registroVM = new RegistroVModel();

            return View(registroVM);
        }

       // ---------------------------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuieroSaberMas(PlanesModel planesVMODEL)
        {

            if (planesVMODEL.NombreQS != null)
            {
                var oContacto = new ContactoModel();
                //Concato cuando es LB
                oContacto.Nombre = planesVMODEL.NombreQS;
                oContacto.APaterno = planesVMODEL.APaternoQS;
                oContacto.AMaterno = planesVMODEL.AMaternoQS;
                oContacto.Mensaje = planesVMODEL.MensajeQS;
                oContacto.Telefono = planesVMODEL.TelefonoQS;
                oContacto.FechaContacto = planesVMODEL.FechaContactoQS;
                oContacto.Email = planesVMODEL.EmailQS;
                oContacto.Tipo = 11;
                var respuestaLB = _ContactoData.LeadBrokerQC(oContacto);

                if (respuestaLB)
                {
                    var url = "http://bitrix-rummet.com:8080/api/bitrix/broker";
                    var client = new RestClient(url);
                    var request = new RestRequest(url, Method.Post);
                    request.AddHeader("Content-Type", "application/json");
                    var body = new
                    {
                        nombre = oContacto.Nombre,
                        apellidoPaterno = oContacto.APaterno,
                        apellidoMaterno = oContacto.AMaterno,
                        telefono = oContacto.Telefono,
                        correo = oContacto.Email,
                        fechaEntrega=oContacto.FechaContacto,
                        mensaje = oContacto.Mensaje,
                        //tipo = 1,
                        //productoId = 110
                    };
                    var bodyy = JsonConvert.SerializeObject(body);
                    request.AddBody(bodyy, "application/json");
                    RestResponse response = await client.ExecuteAsync(request);
                    var output = response.Content;
                    Console.WriteLine(response.Content);

                    if (response.IsSuccessful)
                    {
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var data = System.Text.Json.JsonSerializer.Deserialize<BitrixLIModel>(response.Content, options);

                        //Obtener ID de LEAD ingresado
                        var leadIB = _IntegracionData.IDLeadB(oContacto);

                        if (leadIB != 0)
                        {
                            data.IDRummet = leadIB;
                        }
                        else
                        {
                            data.IDRummet = 0;
                        }

                        data.Accion = "Quiero saber más - Exitoso";
                        data.Proveedor = "Bitrix";
                        data.Tipo = 11;
                        var integracionEntrada = _IntegracionData.IntegracionEntrada(data);
                        TempData["mssg"] = "Nos pondremos en contacto contigo en la fecha y hora acordado.";

                        TempData["mssg"] = "Nos pondremos en contacto contigo en la fecha y hora acordado.";
                        return RedirectToAction("PlanesEcommerce", "Ecommerce");

                    }
                    else
                    {
                        Console.WriteLine($"Error al hacer la solicitud: {response.ErrorMessage}");
                    }

                   
                }
                else
                {
                    TempData["mssgE"] = "Por favor válida que todos los campos estén correctamente llenados.";
                    return RedirectToAction("PlanesEcommerce", "Ecommerce");
                }

            }
            else
            {

                Console.WriteLine("Hola está mal");
            }


            return RedirectToAction("PlanesEcommerce", "Ecommerce");
        }


        [HttpPost]

        //Registro de LEAD-BROkER a través del botón de planes 
        public async Task<IActionResult> RegistroLBC(PlanesModel planesVMODEL)
        {
            if (planesVMODEL.NombreQS != null)
            {
                var oContacto = new ContactoModel();
                //Concato cuando es LB
                oContacto.Nombre = planesVMODEL.NombreQS;
                oContacto.APaterno = planesVMODEL.APaternoQS;
                oContacto.AMaterno = planesVMODEL.AMaternoQS;
                oContacto.Telefono = planesVMODEL.TelefonoQS;
                oContacto.Email = planesVMODEL.EmailQS;
                oContacto.Tipo = 22;
                var respuestaLB = _ContactoData.LeadBrokerQC(oContacto);

                if (respuestaLB)
                {
                    //Envío a Bitrix
                    var url = "http://bitrix-rummet.com:8080/api/bitrix/broker";
                    var client = new RestClient(url);
                    var request = new RestRequest(url, Method.Post);
                    request.AddHeader("Content-Type", "application/json");
                    var body = new
                    {
                        nombre = oContacto.Nombre,
                        apellidoPaterno = oContacto.APaterno,
                        apellidoMaterno = oContacto.AMaterno,
                        telefono = oContacto.Telefono,
                        correo = oContacto.Email,
                        //fechaHora=oMasInformacion.FechaMI
                        //tipo = 1,
                        //productoId = 110
                    };
                    var bodyy = JsonConvert.SerializeObject(body);
                    request.AddBody(bodyy, "application/json");
                    RestResponse response = await client.ExecuteAsync(request);
                    var output = response.Content;
                    Console.WriteLine(response.Content);

                    if (response.IsSuccessful)
                    {
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var data = System.Text.Json.JsonSerializer.Deserialize<BitrixLIModel>(response.Content, options);

                        //Obtener ID de LEAD ingresado
                        var leadIB = _IntegracionData.IDLeadB(oContacto);

                        if (leadIB != 0)
                        {
                            data.IDRummet = leadIB;
                        }
                        else
                        {
                            data.IDRummet = 0;
                        }

                        data.Accion = "Contratar - Exitoso";
                        data.Proveedor = "Bitrix";
                        data.Tipo = 22;
                        var integracionEntrada = _IntegracionData.IntegracionEntrada(data);

                        TempData["mssg"] = "Nos pondremos en contacto contigo en la fecha y hora acordado.";
                        return RedirectToAction("PlanesEcommerce", "Ecommerce");

                    }
                    else
                    {
                        Console.WriteLine($"Error al hacer la solicitud: {response.ErrorMessage}");
                    }
                }
                else
                {
                    TempData["mssgE"] = "Por favor válida que todos los campos estén correctamente llenados.";
                    return RedirectToAction("PlanesEcommerce", "Ecommerce");
                }

            }
            else
            {

                Console.WriteLine("Hola está mal");
            }


            return RedirectToAction("PlanesEcommerce", "Ecommerce");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //Registro de LEAD-BROkER a través del botón de planes 
        public async Task<IActionResult> RegistroLeadBroker(PlanesModel planesVMODEL, string? returnurl = null)
        {
            //Comprobación de variable para verificar si el usuario viene vacío

            Console.WriteLine("Está vacía");


            /********************** CODIGO PARA LA GENERACIÓN DIRECTA DE USUARIO **********************/
            //Generación de contraseña aleatoria
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@";
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = 7;
            string contraseniaAleatoria = string.Empty;
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria += letra.ToString();
            }


            //Asignación de caracteres especiales 
            string caracteresEspeciales = "%$#/()@";
            int longitudCaracteres = caracteresEspeciales.Length;
            char caracter;
            string caracteresAlt = string.Empty;

            for (int i = 0; i < 2; i++)
            {
                caracter = caracteresEspeciales[rdn.Next(longitudCaracteres)];
                caracteresAlt += caracter.ToString();
            }

            contraseniaAleatoria = contraseniaAleatoria + caracteresAlt + "3434";
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");


            if (ModelState.IsValid)
            {
                var usuario = new AppUsuario { UserName = planesVMODEL.email, Email = planesVMODEL.email };
                var resultado = await _userManager.CreateAsync(usuario, contraseniaAleatoria);

                //Envío a tabla de Identity
                //AppUsuario para el llenado de los campos dentro de la tabla de User en el la base de datos
                if (resultado.Succeeded)
                {

                    var respuesta = _OnboardingBroker.RegistroUsuario(planesVMODEL);
                    // await _signInManager.SignInAsync(usuario, isPersistent: false);

                    //Generación de variable de retorno con URL y vista
                    var urlRetorno = Url.Action("Acceso", "Cuentas", new { userId = usuario.Id }, protocol: HttpContext.Request.Scheme);


                    var contenidoCorreo1 = "<center>¡Hola, " + planesVMODEL.nombreLeadBroker + "!</center> \n " +
                       "<center>Estás a punto de dar un gran paso como asesor inmobiliario, ingresa al siguiente link con tu usuario y contraseña:</center>\n\n" +
                       "<center> <h4> Usuario: " + planesVMODEL.email + " </h4>  \n <h4> Contraseña: " + contraseniaAleatoria + " </h4> \n\n " +
                       "y continua con tu registro <br> <a href=\"" + urlRetorno + "\"> REAL STATE DE CVO </a></center> \n" +
                       "<br><center> Resumen de la compra <br><br> Plan escogido: " + planesVMODEL.nombrePlan + "<br>" +
                       "Costo/Inversión: $3,000 + IVA <br>" +
                       "\nTotal: $3,600 </center>" +
                       "<center> <h2> <a href=\"" + urlRetorno + "\"> PAGAR AHORA </a> </h2> </center>\n" +
                       "Recuerda que estamos para apoyarte, si necesitas ayuda adicional puedes comunicarte con nostros por <span style='color: green; font-size: 18px'> WhatsApp </span> o si " +
                       "prefieres llamarnos al <span style='color: orange; font-size: 18px'> 55 33 55 33</span> en un horario de L-V de 8:00 a 20:00  y sábados de 9:00 a 14:00 \n \n " +
                       "<br> <center>¡Gracias! </center>";


                    await _emailSender.SendEmailAsync(planesVMODEL.email, "Confirmación de correo electrónico", contenidoCorreo1);

                    //Envío de alerta a la parte del fron

                    TempData["mssg"] = "Revisa tu bandeja de correo para continuar con tu compra.";

                    return RedirectToAction("PlanesEcommerce", "Ecommerce");
                    //return LocalRedirect(returnurl);

                }
                else
                {
                    TempData["mssgE"] = "Error el correo ingresado ya ha sido registrado";
                    return RedirectToAction("PlanesEcommerce", "Ecommerce");
                }

                ValidarErrores(resultado);

            }

            return LocalRedirect(returnurl);
        }





        //RegistroLeadBroker Nuevo proceso Bitrix
      


        //Manejador de errores

        private void ValidarErrores(IdentityResult resultado)
        {
            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError(String.Empty, error.Description);
            }
        } // Var privada

        //Método para mostrar el formulario de Inicio de sesión 

        [HttpGet]
        public IActionResult Acceso(string? returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Acceso(AccesoVModel accVMODEL, string? returnurl = null) //Recibe los valores
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(accVMODEL.Email, accVMODEL.Password, accVMODEL.RememberMe, lockoutOnFailure: false);

                if (resultado.Succeeded)
                {
                    //return RedirectToAction("Index", "Home");
                    //returnurl = Url.Content("~/");

                    return LocalRedirect(returnurl);
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Acceso inválido");
                    return View();
                }

            }
            return View(accVMODEL);
        }


        //Salir de la aplicación 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalirAplicacion()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //Olvido de contraseña vista 
        [HttpGet]
        public IActionResult OlvidoPassword()
        {
            return View();
        }

        //Formulario de Olvido de Contraseña
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OlvidoPassword(OlvidoPasswordVModel opVModel) //Recibe los valores e instanciamos el modelo
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByEmailAsync(opVModel.Email);
                if (usuario == null)
                {
                    return RedirectToAction("ConfirmacionOlvidoPassword");

                }

                var codigo = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                var urlRetorno = Url.Action("ResetPassword", "Cuentas", new { userId = usuario.Id, code = codigo }, protocol: HttpContext.Request.Scheme);

                await _emailSender.SendEmailAsync(opVModel.Email, "Recuperar contraseña - Ecommerce Test", "Da clic aquí para recuperar tu contraseña: <a href=\"" + urlRetorno + "\">enlace</a>");

                return RedirectToAction("ConfirmacionOlvidoPassword");
            }
            return View(opVModel);
        }

        //Este método ayuda con la redirección cuando es enviado el correo para cambio de contraseña al usuario
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ConfirmacionOlvidoPassword()
        {
            return View();
        }

        //Método que retornará la vista al usuario cuando de clic en el enlace de olvido de contraseña 
        [HttpGet]
        public IActionResult ResetPassword(string code = null, string userId = null)
        {
            return code == null ? View("Error_codigo") : View();
        }

        //Captura de datos para el cambio de contraseña 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(RecuperaPasswordVModel rpVModel) //Recibe los valores e instanciamos el modelo
        {
            if (ModelState.IsValid)
            {
                //Método para recuperar contraseña a través del Email introducido manualmente
                /*
                var usuario = await _userManager.FindByEmailAsync(rpVModel.Email);
                if (usuario == null)
                {
                    return RedirectToAction("ConfirmacionRecuperaPassword");

                }

                var resultado = await _userManager.ResetPasswordAsync(usuario, rpVModel.Code, rpVModel.Password);

                if (resultado.Succeeded)
                {
                    return RedirectToAction("ConfirmacionRecuperaPassword");
                }
                ValidarErrores(resultado);
                */

                //Método para recuperar la contraseña de manera automática 

                var usuario = await _userManager.FindByIdAsync(rpVModel.UserId);
                var resultado = await _userManager.ResetPasswordAsync(usuario, rpVModel.Code, rpVModel.Password);

                if (resultado.Succeeded)
                {
                    return RedirectToAction("ConfirmacionRecuperaPassword");
                }
                ValidarErrores(resultado);

            }
            return View(rpVModel);
        }


        [HttpGet]
        public IActionResult ConfirmacionRecuperaPassword()
        {
            return View();
        }

        //Vista de confirmación de Email 

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmarEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var usuario = await _userManager.FindByIdAsync(userId);
            if (usuario == null)
            {
                return View("Error");
            }

            var resultado = await _userManager.ConfirmEmailAsync(usuario, code);
            return View(resultado.Succeeded ? "ConfirmarEmail" : "Error");
        }
    }
}
