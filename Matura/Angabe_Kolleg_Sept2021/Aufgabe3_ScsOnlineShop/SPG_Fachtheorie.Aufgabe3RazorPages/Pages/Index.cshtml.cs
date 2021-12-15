using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Model;

namespace SPG_Fachtheorie.Aufgabe3RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly StoreContext _db;

        public IndexModel(StoreContext db)
        {
            _db = db;
        }

        public List<Store> Stores { get; set; } = new();

        public void OnGet()
        {
            Stores = _db.Stores.ToList();
        }
    }
}
