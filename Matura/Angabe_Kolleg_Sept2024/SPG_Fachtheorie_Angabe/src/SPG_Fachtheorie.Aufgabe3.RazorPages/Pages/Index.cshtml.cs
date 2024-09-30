using Microsoft.AspNetCore.Mvc.RazorPages;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;

namespace SPG_Fachtheorie.Aufgabe3.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly LanguageweekContext _db;

        public IndexModel(LanguageweekContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }
    }
}