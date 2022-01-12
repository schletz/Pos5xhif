using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;

namespace SPG_Fachtheorie.Aufgabe3RazorPages.Pages.Product
{
    public class AddModel : PageModel
    {
        private readonly StoreContext _db;

        public AddModel(StoreContext db)
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

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                _db.Products.Add(Product);
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Der Datensatz konnte nicht eingefügt werden!");
                return Page();
            }
            return RedirectToPage("/Product/Details", new { ProductId = Product.Ean });
        }

    }
}
