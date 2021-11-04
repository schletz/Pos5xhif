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
    public class OffersController : ODataController
    {
        private readonly ShopContext _db;

        public OffersController(ShopContext context)
        {
            _db = context;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_db.Offers.Select(o => new OfferDto(o.Guid, o.ProductEan, o.Price)).ToList());
        }

        /// <summary>
        /// key MUSS key heißen, sonst stimmt das Routing Template von OData nicht.
        [EnableQuery]
        public IActionResult Get(Guid key)
        {
            var offer = _db.Offers.FirstOrDefault(o => o.Guid == key);
            if (offer is null) { return NotFound(); }
            return Ok(new OfferDto(offer.Guid, offer.ProductEan, offer.Price));
        }
    }
}