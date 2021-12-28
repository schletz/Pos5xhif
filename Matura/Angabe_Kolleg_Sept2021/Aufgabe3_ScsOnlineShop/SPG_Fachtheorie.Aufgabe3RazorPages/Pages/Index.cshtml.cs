using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Model;
using SPG_Fachtheorie.Aufgabe3RazorPages.Services;

namespace SPG_Fachtheorie.Aufgabe3RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly StoreContext _db;
        private readonly AuthService _authService;
        public IndexModel(StoreContext db, AuthService authService)
        {
            _db = db;
            _authService = authService;
        }

        public int? CurrentStore => _authService.CurrentStoreId;
        public List<Store> Stores { get; set; } = new();
        public List<SelectListItem> StoreItems { get; set; } = new();
        [BindProperty]
        public int SelectedStoreItem { get; set; }
        public void OnGet()
        {
            Stores = _db.Stores.ToList();
            StoreItems = Stores.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
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
            await _authService.TryLogin(SelectedStoreItem);
            return RedirectToPage();
        }
    }
}

