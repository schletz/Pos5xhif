using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2;
using SPG_Fachtheorie.Aufgabe2.Model;
using SPG_Fachtheorie.Aufgabe3Mvc.Models;

namespace SPG_Fachtheorie.Aufgabe3Mvc.Controllers;

public class HomeController : Controller
{
    private readonly GradeContext _db;

    public HomeController(GradeContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var students = _db.Students.Include(s => s.Class)
            .OrderBy(s => s.Class.Name).ThenBy(s => s.Lastname)
            .ThenBy(s => s.Firstname).ToList();

        return View(students);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
