using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScsOnlineShop.Application.Infrastructure;
using ScsOnlineShop.Shared.Dto;
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
        /// Das Mapping wird ohne named arcuments gemacht, da die Expression für EF Core dies
        /// nicht untetstützt. Deswegen ist auch keine Map() Methode in den Modelklassen
        /// möglich, da ja SQL erstellt wird. Wir müssen daher mit Select projizieren.
        /// </summary>
        [HttpGet]
        public IActionResult Get() =>
            Ok(_db.Offers
                .Select(o => new OfferDto(o.Guid, o.Price,
                    new ProductDto(
                        o.Product.Ean, o.Product.Name,
                        new ProductCategoryDto(o.Product.ProductCategory.Name, o.Product.ProductCategory.Guid)),
                    new StoreDto(o.Store.Guid, o.Store.Name))));

        /// <summary>
        /// GET /api/offers/(GUID)
        /// </summary>
        [HttpGet("{guid}")]
        public IActionResult GetOfferByGuid(Guid guid)
        {
            var offer = _db.Offers.FirstOrDefault(o => o.Guid == guid);
            if (offer is null) { return NotFound(); }
            return Ok(new OfferDto(offer.Guid, offer.Price,
                    new ProductDto(
                        offer.Product.Ean, offer.Product.Name,
                        new ProductCategoryDto(offer.Product.ProductCategory.Name, offer.Product.ProductCategory.Guid)),
                    new StoreDto(offer.Store.Guid, offer.Store.Name)));
        }
    }
}