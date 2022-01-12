using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;

namespace SPG_Fachtheorie.Aufgabe3RazorPages.Pages.Product
{
    public class EditModel : PageModel
    {
        private readonly StoreContext _db;

        public EditModel(StoreContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Aufgabe2.Model.Product Product { get; set; }
        public IEnumerable<SelectListItem> CategoryItems =>
            _db.ProductCategories.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

        public IActionResult OnGet(int productId)
        {
            var product = _db.Products
                .Include(p => p.ProductCategory)
                .FirstOrDefault(p => p.Ean == productId);
            if (product is null) { return NotFound(); }
            Product = product;
            return Page();
        }

        public IActionResult OnPost(int productId)
        {
            var dbProduct = _db.Products.FirstOrDefault(p => p.Ean == productId);
            if (dbProduct is null) { return NotFound(); }
            dbProduct.Name = Product.Name;
            dbProduct.ProductCategoryId = Product.ProductCategoryId;
            _db.SaveChanges();
            return RedirectToPage("/Product/Details", new { ProductId = productId });
        }
    }
}
