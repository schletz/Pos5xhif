using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SPG_Fachtheorie.Aufgabe2;
using SPG_Fachtheorie.Aufgabe2.Model;
using SPG_Fachtheorie.Aufgabe3RazorPages.Services;

namespace SPG_Fachtheorie.Aufgabe3RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppointmentContext _db;
        private readonly AuthService _authService;
        public IndexModel(AppointmentContext db, AuthService authService)
        {
            _db = db;
            _authService = authService;
        }

        public string CurrentUser => _authService.Username;
        public List<Student> Students { get; set; } = new();
        public List<SelectListItem> UserItems { get; set; } = new();
        [BindProperty]
        public string SelectedUser { get; set; }
        public void OnGet()
        {
            Students = _db.Students.ToList();
            UserItems = Students.Select(s => new SelectListItem
            {
                Text = $"{s.Username} - {s.Lastname} {s.Firstname} (Coach: {(s is Coach ? "Ja" : "Nein")})",
                Value = s.Username
            })
            .ToList();
        }

        public async Task<IActionResult> OnPostLogout()
        {
            await _authService.Logout();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPost()
        {
            await _authService.TryLogin(SelectedUser);
            return RedirectToPage();
        }
    }
}
