using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe3.Cmds;
using SPG_Fachtheorie.Aufgabe3.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe3.Controllers;

[Route("[controller]")]
[ApiController]
public class InventoryItemsController : ControllerBase
{
    private readonly ReservationContext _db;

    public InventoryItemsController(ReservationContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<InventoryItemWithReservationsDto>>> GetInventoryItems([FromQuery] bool withReservations = false)
    {
        // TODO: Add your implementation
        // Hint: Use db directly (without service)
        throw new NotImplementedException();
    }

    [HttpGet("{category}")]
    public async Task<ActionResult<List<InventoryItemDto>>> GetInventoryItemsForCategory(string category)
    {
        // TODO: Add your implementation
        // Hint: Use db directly (without service)
        throw new NotImplementedException();
    }

    [HttpPatch("available/{id}")]
    public async Task<IActionResult> UpdateAvailable(int id, [FromBody] UpdateAvailableCmd cmd)
    {
        // TODO: Add your implementation
        // Hint: Use db directly (without service)
        throw new NotImplementedException();
    }
}