using adminRummet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace adminRummet.Controllers
{
    public class AccesosController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccesosController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager; 
        }

        //Página de inicio de sesión
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Acceso(string? returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            return View();
        }

        //Método para iniciar sesión
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Acceso(AccesoModel accVMODEL, string? returnurl = null) //Recibe los valores
        {
            //variable del rol definitivo
            var rolD = "";
            ViewData["ReturnUrl"] = returnurl;
            //url para regresar a la ráiz del proyecto
            returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(accVMODEL.Email, accVMODEL.Password, accVMODEL.RememberMe, lockoutOnFailure: false);

                if (resultado.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(accVMODEL.Email);
                    //Obtener el rol
                    var roles = await _userManager.GetRolesAsync(user);

                    var roleObjects = new List<IdentityRole>();
                    foreach (var role in roles)
                    {
                        var roleObject = await _roleManager.FindByNameAsync(role);
                        if (roleObject != null)
                        {
                            roleObjects.Add(roleObject);
                        }
                    }

                    foreach (var roleObject in roleObjects)
                    {
                        rolD = roleObject.Name;
                        Console.WriteLine($"Nombre de rol: {roleObject.Name}");
                    }

                    //return RedirectToAction("Index", "Home");
                    //returnurl = Url.Content("~/");

                    switch (rolD)
                    {
                        case "Ejecutivo comercial":
                            returnurl = returnurl ?? @Url.Action("Bienvenida", "Accesos");
                            return RedirectToAction("Bienvenida", "Accesos");
                            break;
                        case "Finanzas":
                            returnurl = returnurl ?? @Url.Action("Bienvenida", "Accesos");
                            return RedirectToAction("Bienvenida", "Accesos");
                            break;
                        case "Aliado":
                            returnurl = returnurl ?? @Url.Action("Bienvenida", "Accesos");
                            return RedirectToAction("Bienvenida", "Accesos");
                            break;
                        case "Mesa de control":
                            returnurl = returnurl ?? @Url.Action("TabMesaControl", "MesaControl");
                            return RedirectToAction("TabMesaControl", "MesaControl");
                            break;
                        case "Propietario":
                            returnurl = returnurl ?? @Url.Action("TableroPropietario", "Propietario");
                            return RedirectToAction("TableroPropietario", "Propietario");
                            break;
                        case "Admin comercial":
                            returnurl = returnurl ?? @Url.Action("Bienvenida", "Accesos");
                            return RedirectToAction("Bienvenida", "Accesos");
                            break;
                        case "Soporte":
                            returnurl = returnurl ?? @Url.Action("Bienvenida", "Accesos");
                            return RedirectToAction("Bienvenida", "Accesos");
                            break;
                        case "Admin root":
                            returnurl = returnurl ?? @Url.Action("Bienvenida", "Accesos");
                            return RedirectToAction("Bienvenida", "Accesos");
                            break;
                        case "Legal":
                            returnurl = returnurl ?? @Url.Action("Bienvenida", "Accesos");
                            return RedirectToAction("Bienvenida", "Accesos");
                            break;
                        case "Contact Center":
                            returnurl = returnurl ?? @Url.Action("TabContactCenter", "Contact");
                            return RedirectToAction("TabContactCenter", "Contact");
                            break;
                        default:
                            ViewData["ReturnUrl"] = returnurl;
                            //url para regresar a la ráiz del proyecto
                            returnurl = returnurl ?? Url.Content("~/");
                            break;
                    }

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


        //Método para cerrar sesión 
        //Salir de la aplicación 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalirAplicacion()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //Para cuando el acceso es Denegado de alguna de las páginas de roles
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Denegado(string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            return View();
        }

        //Cambiar contraseña (usuario autenticado)
        [HttpGet]
        public IActionResult CambiarPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarPassword(CambioPassModel cpViewModel, string email)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByEmailAsync(email);
                if (usuario == null)
                {
                    return RedirectToAction("Error");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);

                var resultado = await _userManager.ResetPasswordAsync(usuario, token, cpViewModel.Password);
                if (resultado.Succeeded)
                {
                    return RedirectToAction("ConfirmacionCambioPassword");
                }
                else
                {
                    return View(cpViewModel);
                }

            }
            return View(cpViewModel);
        }

        //Vista para recalcar que el cambio fue aplicado correctamente
        [HttpGet]
        public IActionResult ConfirmacionCambioPassword()
        {
            return View();
        }

        public IActionResult Bienvenida()
        {
            return View();
        }

  
    }
}
