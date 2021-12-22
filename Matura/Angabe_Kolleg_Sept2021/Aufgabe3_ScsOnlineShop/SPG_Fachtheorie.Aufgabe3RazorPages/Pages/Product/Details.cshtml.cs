using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace SPG_Fachtheorie.Aufgabe3RazorPages.Pages.Product
{
    public class DetailsModel : PageModel
    {
        private readonly StoreContext _db;
        public Aufgabe2.Model.Product Product { get; private set; }
        public DetailsModel(StoreContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(int productId)
        {
            var product = _db.Products
                .Include(p => p.ProductCategory)
                .FirstOrDefault(p => p.Ean == productId);
            if (product is null) { return NotFound(); }
            Product = product;
            return Page();
        }
    }
}
