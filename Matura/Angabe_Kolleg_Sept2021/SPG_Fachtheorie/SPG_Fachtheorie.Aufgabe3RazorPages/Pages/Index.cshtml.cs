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
        [BindProperty]
        public int SelectedStoreId { get; set; }
        public int StoreId => _authService.CurrentStore;
        public List<Store> Stores { get; set; }
        public IEnumerable<SelectListItem> StoreList => Stores
            .Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() });

        public void OnGet()
        {
            Stores = _db.Stores.ToList();
        }

        public async Task<IActionResult> OnPostLogin()
        {
            await _authService.Login(SelectedStoreId);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostLogout()
        {
            await _authService.Logout();
            return RedirectToPage();
        }
    }
}
