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
    public class OffersController : ControllerBase
    {
        private readonly ShopContext _db;

        public OffersController(ShopContext db)
        {
            _db = db;
        }

        /// <summary>
        /// GET /api/offers
        /// </summary>
        [HttpGet]
        public IActionResult Get() =>
            Ok(_db.Offers
                .Select(o => new OfferDto(
                    new ProductDto(o.Product.Ean, o.Product.Name),
                    new StoreDto(o.Store.Guid, o.Store.Name), o.Price, o.Guid)));

        /// <summary>
        /// GET /api/offers/(GUID)
        /// </summary>
        [HttpGet("{guid}")]
        public IActionResult GetOfferByGuid(Guid guid)
        {
            var offer = _db.Offers.FirstOrDefault(o => o.Guid == guid);
            if (offer is null) { return NotFound(); }
            return Ok(new OfferDto(
                    new ProductDto(offer.Product.Ean, offer.Product.Name),
                    new StoreDto(offer.Store.Guid, offer.Store.Name), offer.Price, offer.Guid));
        }
    }
}