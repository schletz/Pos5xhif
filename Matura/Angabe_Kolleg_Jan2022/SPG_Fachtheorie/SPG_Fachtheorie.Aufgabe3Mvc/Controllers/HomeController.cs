using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SPG_Fachtheorie.Aufgabe2;
using SPG_Fachtheorie.Aufgabe2.Model;
using SPG_Fachtheorie.Aufgabe3Mvc.Models;
using SPG_Fachtheorie.Aufgabe3Mvc.Services;
using SPG_Fachtheorie.Aufgabe3Mvc.Views.Home;

namespace SPG_Fachtheorie.Aufgabe3Mvc.Controllers;

public class HomeController : Controller
{
    private readonly AppointmentContext _db;
    private readonly AuthService _authService;

    public HomeController(AppointmentContext db, AuthService authService)
    {
        _db = db;
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var students = _db.Students.ToList();
        var userItems = students.Select(s => new SelectListItem
        {
            Text = $"{s.Username} - {s.Lastname} {s.Firstname} (Coach: {(s is Coach ? "Ja" : "Nein")})",
            Value = s.Username
        })
        .ToList();

        return View(new IndexViewModel(
            Students: students,
            UserItems: userItems,
            CurrentUser: _authService.Username,
            SelectedUser: _authService.Username));
    }

    [HttpPost]
    public async Task<IActionResult> Index(string selectedUser)
    {
        await _authService.TryLogin(selectedUser);
        return RedirectToAction();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _authService.Logout();
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
