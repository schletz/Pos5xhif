using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScsOnlineShop.Application.Infrastructure;
using ScsOnlineShop.Application.Model;

namespace ScsOnlineShop.Webapp.Pages;

public class IndexModel : PageModel
{
    private readonly ShopContext _db;
    public List<Store> Stores { get; private set; } = new();
    public IndexModel(ShopContext db)
    {
        _db = db;
    }

    public void OnGet()
    {
        Stores = _db.Stores.OrderBy(s => s.Name).ToList();
    }
}
