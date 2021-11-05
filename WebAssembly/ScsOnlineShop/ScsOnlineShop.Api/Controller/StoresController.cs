using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScsOnlineShop.Application.Infrastructure;
using ScsOnlineShop.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScsOnlineShop.Api.Controller
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
        [HttpGet]
        public IActionResult GetAllStores()
        {
            return Ok(_db.Stores.Select(s => new StoreDto(s.Guid, s.Name)));
        }

    }
}
