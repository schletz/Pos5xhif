using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScsOnlineShop.Application.Infrastructure;
using ScsOnlineShop.Shared.Dto;
using Microsoft.EntityFrameworkCore;

namespace ScsOnlineShop.Webapp.Pages
{
    public class StoresModel : PageModel
    {
        private readonly ShopContext _db;

        public StoresModel(ShopContext db)
        {
            _db = db;
        }

        public IActionResult OnGetAll()
        {
            var stores = _db.Stores
                .OrderBy(s => s.Name)
                .Select(s => new
                {
                    s.Guid,
                    s.Name,
                    Offers = s.Offers.Select(o => new
                    {
                        o.Guid,
                        ProductGuid = o.Product.Guid,
                        ProductName = o.Product.Name,
                        o.Price
                    })
                });
            return new JsonResult(stores);
        }
        public void OnGet()
        {
        }

        public IActionResult OnDeleteOffer([FromQuery] Guid offerGuid)
        {
            var offer = _db.Offers.FirstOrDefault(o => o.Guid == offerGuid);
            if (offer is null) { return NotFound(); }
            try
            {
                _db.Offers.Remove(offer);
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return BadRequest("Cannot delete offer.");
            }
            return new NoContentResult();
        }
    }

}