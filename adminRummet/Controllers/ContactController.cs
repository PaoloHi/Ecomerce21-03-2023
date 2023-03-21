using Microsoft.AspNetCore.Mvc;

namespace adminRummet.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult TabContactCenter()
        {
            return View();
        }
    }
}
