using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Model;
using Spg.ProbeFachtheorie.Aufgabe2.Infrastructure;
using Spg.ProbeFachtheorie.Aufgabe3.Services;

namespace Spg.ProbeFachtheorie.Aufgabe3.Pages
{
    public class IndexModel : PageModel
    {
        private readonly LibraryContext _db;
        private readonly HttpCookieAuthService _authService;
        public IndexModel(LibraryContext db, HttpCookieAuthService authService)
        {
            _db = db;
            _authService = authService;
        }

        public List<SelectListItem> Users { get; private set; }
        [BindProperty]
        public string UserEmail { get; set; }
        public string CurrentUser { get; private set; }
        public string Message { get; private set; }

        public IActionResult OnGet()
        {
            CurrentUser = _authService.Username();
            Users = _db.Users.Select(u => new SelectListItem
            {
                Value = u.UserName,
                Text = $"{u.EMail} (Role {u.Role})"
            }).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!string.IsNullOrEmpty(UserEmail))
            {
                try
                {
                    await _authService.Login(UserEmail, "1234");
                    return RedirectToPage();
                }
                catch (Exception e)
                {
                    Message = e.Message;
                }
            }
            return Page();
        }
        public async Task<IActionResult> OnPostLogout()
        {
            await _authService.Logout();
            return RedirectToPage();
        }
    }
}
