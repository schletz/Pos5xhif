using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SPG_Fachtheorie.Aufgabe2;
using SPG_Fachtheorie.Aufgabe2.Model;

namespace SPG_Fachtheorie.Aufgabe3RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GradeContext _db;
        public IndexModel(GradeContext db)
        {
            _db = db;
        }
        public List<Student> Students { get; set; } = new();
        public void OnGet()
        {
            Students = _db.Students.Include(s=>s.Class)
                .OrderBy(s=>s.Class.Name).ThenBy(s=>s.Lastname)
                .ThenBy(s=>s.Firstname).ToList();
        }
    }
}
