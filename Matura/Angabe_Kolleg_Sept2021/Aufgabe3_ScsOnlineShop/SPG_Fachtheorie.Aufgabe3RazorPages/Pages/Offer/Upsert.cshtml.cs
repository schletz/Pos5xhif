using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe3RazorPages.Services;

namespace SPG_Fachtheorie.Aufgabe3RazorPages.Pages.Offer
{
    public class UpsertModel : PageModel
    {
        private readonly StoreContext _db;
        private readonly AuthService _auth;

        public UpsertModel(StoreContext db, AuthService auth)
        {
            _db = db;
            _auth = auth;
        }

        public Aufgabe2.Model.Offer Offer { get; set; }

        public IActionResult OnGet(int productId)
        {
            var currentStore = _auth.CurrentStoreId;
            var offer = _db.Offers
                .Include(o => o.Product)
                .FirstOrDefault(o => o.StoreId == currentStore && o.ProductEan == productId);
            if (offer is null) { return NotFound(); }
            Offer = offer;
            return Page();
        }
    }
}
