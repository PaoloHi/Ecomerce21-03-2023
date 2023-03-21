using Microsoft.AspNetCore.Mvc;

namespace adminRummet.Controllers
{
    public class MesaControlController : Controller
    {
        public IActionResult TabMesaControl()
        {
            return View();
        }
    }
}
