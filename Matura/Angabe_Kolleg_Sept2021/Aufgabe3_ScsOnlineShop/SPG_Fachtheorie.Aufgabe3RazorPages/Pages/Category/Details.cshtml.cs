using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe3RazorPages.Services;

namespace SPG_Fachtheorie.Aufgabe3RazorPages.Pages.Category
{
    public class DetailsModel : PageModel
    {
        private readonly StoreContext _db;
        private readonly AuthService _authService;
        public List<Aufgabe2.Model.Product> Products { get; private set; } = new();
        public int? CurrentStoreId => _authService.CurrentStoreId;
        public DetailsModel(StoreContext db, AuthService authService)
        {
            _db = db;
            _authService = authService;
        }

        public void OnGet(int categoryId)
        {
            Products = _db.Products
                .Include(p => p.Offers)
                .OrderBy(p => p.Name)
                .ToList();
        }
    }
}
