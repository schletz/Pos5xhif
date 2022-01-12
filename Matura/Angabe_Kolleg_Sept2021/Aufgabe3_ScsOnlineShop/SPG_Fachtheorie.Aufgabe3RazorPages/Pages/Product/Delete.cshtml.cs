using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;

namespace SPG_Fachtheorie.Aufgabe3RazorPages.Pages.Product
{
    public class DeleteModel : PageModel
    {
        private readonly StoreContext _db;

        public Aufgabe2.Model.Product Product { get; private set; }
        public DeleteModel(StoreContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(int productId)
        {
            var product = _db.Products
                .Include(p => p.Offers)
                .FirstOrDefault(p => p.Ean == productId);
            if (product is null) { return NotFound(); }
            Product = product;
            return Page();
        }

        public IActionResult OnPostCancel(int productId)
        {
            return RedirectToPage("/Product/Details", new { ProductId = productId });
        }
        public IActionResult OnPostConfirm(int productId)
        {
            var product = _db.Products
                .FirstOrDefault(p => p.Ean == productId);
            if (product is null) { return NotFound(); }
            _db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToPage("/Category/Details", new { CategoryId = product.ProductCategoryId });
        }
    }
}
