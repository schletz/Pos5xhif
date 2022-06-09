using Microsoft.AspNetCore.Mvc;

namespace SPG_Fachtheorie.Aufgabe3Mvc.Controllers
{
    public class KonferenzController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
