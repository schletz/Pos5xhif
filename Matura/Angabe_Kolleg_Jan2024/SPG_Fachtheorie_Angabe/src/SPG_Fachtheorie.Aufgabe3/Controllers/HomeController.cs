using Microsoft.AspNetCore.Mvc;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;

namespace SPG_Fachtheorie.Aufgabe3.Controllers
{
    public class HomeController : Controller
    {
        private readonly EventContext _db;

        public HomeController(EventContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}