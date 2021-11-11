using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScsOnlineShop.Application.Infrastructure;
using ScsOnlineShop.Shared.Dto;

namespace ScsOnlineShop.Webapp.Pages
{
    public class StoresModel : PageModel
    {
        private readonly ShopContext _db;

        public StoresModel(ShopContext db)
        {
            _db = db;
        }

        /// <summary>
        /// GET /Stores/All
        /// Endpoint für den AJAX Request. async Task<IActionResult> bei einem async Handler.
        /// </summary>
        public IActionResult OnGetAll()
        {
            var stores = _db.Stores
                .OrderBy(s => s.Name)
                .Select(s => new StoreDto(s.Guid, s.Name)).ToList();
            return new JsonResult(stores);
        }
        public void OnGet()
        {
        }
    }
}
