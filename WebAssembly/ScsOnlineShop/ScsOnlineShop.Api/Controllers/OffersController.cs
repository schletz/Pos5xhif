using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScsOnlineShop.Application.Infrastructure;
using ScsOnlineShop.Application.Model;
using ScsOnlineShop.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScsOnlineShop.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly ShopContext _db;
        private readonly IMapper _mapper;


        public OffersController(ShopContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        /// <summary>
        /// GET /api/offers
        /// Das Mapping wird ohne named arcuments gemacht, da die Expression für EF Core dies
        /// nicht untetstützt. Deswegen ist auch keine Map() Methode in den Modelklassen
        /// möglich, da ja SQL erstellt wird. Wir müssen daher mit Select projizieren.
        /// </summary>
        [HttpGet]
        public IActionResult Get() =>
            Ok(_mapper.ProjectTo<OfferDto>(_db.Offers));

        /// <summary>
        /// GET /api/offers/(GUID)
        /// </summary>
        [HttpGet("{guid}")]
        public IActionResult GetOfferByGuid(Guid guid)
        {
            var offer = _db.Offers.FirstOrDefault(o => o.Guid == guid);
            if (offer is null) { return NotFound(); }
            return Ok(_mapper.Map<OfferDto>(offer));
        }

        [HttpPut]
        public IActionResult UpdateOffer(OfferDtoBase offerDto)
        {
            var offer = _db.Offers.FirstOrDefault(o => o.Guid == offerDto.Guid);
            if (offer is null) { return NotFound(); }
            offer.Price = offerDto.Price;
            offer.LastUpdate = DateTime.UtcNow;
            _db.SaveChanges();
            return Ok(_mapper.Map<OfferDto>(offer));
        }

        [HttpDelete("{guid}")]
        public IActionResult DeleteOffer(Guid guid)
        {
            var offer = _db.Offers.FirstOrDefault(o => o.Guid == guid);
            if (offer is null) { return NotFound(); }
            _db.Offers.Remove(offer);
            _db.SaveChanges();
            return NoContent();
        }

    }
}