using EcommerceRealCVO.Datos.Center;
using EcommerceRealCVO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcommerceRealCVO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        EcommerceCenter _EcommerceData = new EcommerceCenter();

        //Para subir los archivos y guardarlos dentro del disco LOCAL 
        private readonly IWebHostEnvironment _enviroment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;

            //Para uso de los archivos
            _enviroment = env;
        }

        //VISTA DEL ECOMMERCE 
        public IActionResult Index()
        {
            var ecommerceModel = new EcommerceModel();
            var rutaImg = new PropiedadesDestacadas();

           
            //Llamando al método Listar, el cual trae los datos de tipo de propiedades
            var oListaTProp = _EcommerceData.ListarTipoProp();
            //Llenado de la lista a través del modelo 
            ecommerceModel.TPropiedadL = oListaTProp;

            //Trayendo datos de propiedades destacadas
            var oListaDProp = _EcommerceData.ListarPropDestacadas();
           
            //Enviando a la lista los resultados de las propiedades destacadas
            ecommerceModel.LPropiedadesDestacadas = oListaDProp;

            //Llenado variable de modelo de ruta 


            // Convercion de la Imagenes en Base64
            var oListaBase64 = new List<base64PropiedadDestacada>();
            foreach (var datos in oListaDProp)
            {
                var path = System.IO.Directory.GetCurrentDirectory() + "\\..\\RealStateGestion\\ImagenesPropiedad\\" + datos.RutaImagen.ToString();

                byte[] imageArray = System.IO.File.ReadAllBytes(path);
                string base64Image = Convert.ToBase64String(imageArray);
                string src64 = "data:image/jpg/jpeg/png;base64," + base64Image; // parte que se agregara en src=

                //txt64.Insert(i, src64); // se codigo base64 en la 

                Console.WriteLine(datos.IDpropiedad);
                oListaBase64.Add(new base64PropiedadDestacada()
                {
                    IDpropiedadBase64 = Convert.ToInt32(datos.IDpropiedad),
                    RutaImagen = datos.RutaImagen.ToString(),
                    txtBase64 = src64.ToString(),
                });


            }

            //Enviando a la lista los resultado de las URL en base64
            ecommerceModel.LImagenBase64 = oListaBase64;


            //Trayendo datos de propiedades destacadas
            var oListaEProp = _EcommerceData.ListarPropExistentes();
            //Enviando a la lista los resultados de las propiedades destacadas
            ecommerceModel.LPropiedadesExistentes = oListaEProp;

            return View(ecommerceModel);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Prueba()
        {
            Console.WriteLine("HEY");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}