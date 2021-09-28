using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Model;
using SPG_Fachtheorie.Aufgabe3Mvc.Models;
using SPG_Fachtheorie.Aufgabe3Mvc.Services;

namespace SPG_Fachtheorie.Aufgabe3Mvc.Controllers
{
    public class HomeController : Controller
    {
        public class IndexViewModel
        {
            public IndexViewModel(List<Store> stores, int storeId, IEnumerable<SelectListItem> storeList)
            {
                Stores = stores;
                StoreId = storeId;
                StoreList = storeList;
            }

            public List<Store> Stores { get; }
            public int StoreId { get; }
            public IEnumerable<SelectListItem> StoreList { get; }

        }

        private readonly StoreContext _db;
        private readonly AuthService _authService;

        public HomeController(StoreContext db, AuthService authService)
        {
            _db = db;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var stores = _db.Stores.ToList();
            var vm = new IndexViewModel(
                stores,
                _authService.CurrentStore,
                stores.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() })
            );
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Login(int storeId)
        {
            await _authService.Login(storeId);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
