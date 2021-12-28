using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Model;
using Microsoft.EntityFrameworkCore;

namespace SPG_Fachtheorie.Aufgabe3RazorPages.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly StoreContext _db;
        public List<ProductCategory> ProductCategories { get; private set; } = new();
        public IndexModel(StoreContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            // SELECT * FROM ProductCategories LEFT JOIN Products ON (...)
            ProductCategories = _db.ProductCategories
                .Include(p => p.Products)
                .OrderBy(p => p.Name).ToList();
        }
    }
}
