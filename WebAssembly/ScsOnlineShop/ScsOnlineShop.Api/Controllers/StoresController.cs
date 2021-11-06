using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScsOnlineShop.Application.Infrastructure;
using ScsOnlineShop.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScsOnlineShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly ShopContext _db;

        public StoresController(ShopContext db)
        {
            _db = db;
        }

        /// <summary>
        /// GET /api/stores
        /// Liefert alle Stores und projiziert das Ergebnis auf die Klasse StoreDto.
        /// Natürlich kann auch Automapper verwendet werden, um das zu erledigen.
        /// </summary>
        [HttpGet]
        public IActionResult GetAllStores() =>
            Ok(_db.Stores.Select(s => new StoreDto(s.Guid, s.Name)));
    }
}