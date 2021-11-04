using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ScsOnlineShop.Application.Infrastructure;
using ScsOnlineShop.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScsOnlineShop.Api.Controllers
{
    public class StoresController : ODataController
    {
        private readonly ShopContext _db;

        public StoresController(ShopContext context)
        {
            _db = context;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_db.Stores.ToList().Select(s => new StoreDto(Guid: s.Guid, Name: s.Name)));
        }

        /// <summary>
        /// key MUSS key heißen, sonst stimmt das Routing Template von OData nicht.
        [EnableQuery]
        public IActionResult Get(Guid key)
        {
            var store = _db.Stores.FirstOrDefault(c => c.Guid == key);
            if (store is null) { return NotFound(); }
            return Ok(new StoreDto(Guid: store.Guid, Name: store.Name));
        }
    }
}