using Microsoft.AspNetCore.Mvc.RazorPages;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe3.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EventContext _db;
        public int TicketCount { get; private set; }
        public IndexModel(EventContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            TicketCount = _db.Tickets.Count();
        }
    }
}