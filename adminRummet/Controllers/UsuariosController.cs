using adminRummet.Center.Admin;
using adminRummet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace adminRummet.Controllers
{
    public class UsuariosController : Controller
    {
        /******************** INYECCIÓN DE DEPENDENCIAS ********************/

        //Center
        //Usuarios
        UsuariosCenter _Usuarios = new UsuariosCenter();

        //Identity User - para la creación de usuario
        private readonly UserManager<IdentityUser> _userManager;
        //Para los roles de usuario
        private readonly RoleManager<IdentityRole> _roleManager;

        /****************************************/


        /******************** CREACIÓN DE CONSTRUCTOR ********************/
        //Creación de constructor
        public UsuariosController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        /****************************************/

        //Consola usuarios
        public async Task<IActionResult> ConsolaUsuarios()

        {
            var oListaUsuarios = _Usuarios.ListaUsuarios();
            return View(oListaUsuarios);
        }

            //Método de nuevo usuario de Rummet
            public async Task<IActionResult> NuevoUsuario()

        {
            var usuariosModel = new UsuariosModel();
            /*
             * SOLO SE UTILIZA CUANDO LOS ROLES NO EXISTEN
            //Para la creación de los roles
            //Creación de rol usuario Registrado
            await _roleManager.CreateAsync(new IdentityRole("Admin root"));
            await _roleManager.CreateAsync(new IdentityRole("Soporte"));
            await _roleManager.CreateAsync(new IdentityRole("Broker"));
            await _roleManager.CreateAsync(new IdentityRole("Mesa de control"));
            await _roleManager.CreateAsync(new IdentityRole("Contact Center"));
            await _roleManager.CreateAsync(new IdentityRole("Legal"));
            await _roleManager.CreateAsync(new IdentityRole("Finanzas"));
            await _roleManager.CreateAsync(new IdentityRole("Admin comercial"));
            await _roleManager.CreateAsync(new IdentityRole("Ejecutivo comercial"));
            await _roleManager.CreateAsync(new IdentityRole("Propietario"));
            await _roleManager.CreateAsync(new IdentityRole("Aliado"));
            *
            */

            //Llenado de roles
            //Llamando al método Listar, el cual trae los datos de tipo de propiedades
            var oListaRoles = _Usuarios.ListarRoles();
            //Llenado de la lista a través del modelo 
            usuariosModel.Roles = oListaRoles;

            return View(usuariosModel);
        }

        //Función que envia los datos al centro de datos 
        [HttpPost]
        public async Task<IActionResult> Guardar(UsuariosModel oUsuarios)
        {
            //Variable de contraseña global para usuarios NUEVOS
            string passRummet = "RummetVLn=g#dg23";

            //Guardado en la tabla de de inicio de sesión de Rummet
            var usuario = new AppUsuario { UserName = oUsuarios.Correo, Email = oUsuarios.Correo };
            //Variable para crear el usuario y se le envía la información de la variable usuario
            var resultado = await _userManager.CreateAsync(usuario, passRummet);
            if (resultado.Succeeded)
            {
                //Guardando el rol seleccionado y relacionandolo con la tabla correspondiente
                await _userManager.AddToRoleAsync(usuario, oUsuarios.RolS);

                //Guardando la información en la tabla de usuarios nuevos.
                //Inserción a la tabla de datos usuarios
                var respuesta = _Usuarios.Guardar(oUsuarios);

                //Retornar a la consola con el usuario creado :)
                return RedirectToAction("ConsolaUsuarios");

            }
            else
            {
                //Retornar a la vista del nuevo usuario
                Console.WriteLine("El usuario ya existe");
            }

            return View();

        }

    }
}
