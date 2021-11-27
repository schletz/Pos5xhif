using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScsOnlineShop.Application.Infrastructure;
using ScsOnlineShop.Application.Model;
using ScsOnlineShop.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace ScsOnlineShop.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly ShopContext _db;
        private readonly IMapper _mapper;

        public StoresController(ShopContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        /// <summary>
        /// GET /api/stores
        /// Liefert alle Stores und projiziert das Ergebnis auf die Klasse StoreDto.
        /// Natürlich kann auch Automapper verwendet werden, um das zu erledigen.
        /// </summary>
        [HttpGet]
        public IActionResult GetAllStores() =>
            Ok(_mapper.ProjectTo<StoreDto>(_db.Stores));

        [HttpGet("{guid}/offers")]
        public IActionResult GetOffersFromStore(Guid guid)
        {
            var store = _db.Stores.FirstOrDefault(s => s.Guid == guid);
            if (store is null) { return NotFound(); }
            return Ok(_mapper.ProjectTo<OfferDto>(_db.Offers.Where(o => o.StoreId == store.Id)));
        }

        [HttpPost]
        public IActionResult AddStore([FromBody] StoreDto storeDto)
        {
            var store = _mapper.Map<Store>(storeDto);
            try
            {
                _db.Stores.Add(store);
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return BadRequest("Fehler beim Einfügen des Stores.");
            }
            return Ok(_mapper.Map<StoreDto>(store));
        }

        [HttpPut]
        public IActionResult UpdateStore([FromBody] StoreDto storeDto)
        {
            var store = _db.Stores.FirstOrDefault(s => s.Guid == storeDto.Guid);
            if (store is null) { return NotFound(); }
            store.Name = storeDto.Name;
            try
            {
                _db.Stores.Add(store);
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return BadRequest("Fehler beim Aktualisieren des Stores.");
            }
            return NoContent();
        }

        [HttpDelete("{guid}")]
        public IActionResult DeleteStore(Guid guid)
        {
            var store = _db.Stores.FirstOrDefault(s => s.Guid == guid);
            if (store is null) { return NotFound(); }
            try
            {
                _db.Stores.Remove(store);
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return BadRequest("Fehler beim Löschen des Stores.");
            }
            return NoContent();
        }
    }
}