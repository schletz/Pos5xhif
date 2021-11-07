# Anlegen von Controllern für CRUD Operationen

Im letzten Kapitel wurden DTO Klassen definiert. Im Controller des Projektes API werden nun die
Modelklassen aus der Datenbank gelesen und projiziert. 

Ein vollständiger Controller für das Einfügen von Stores könnte so aussehen:

```c#
[Route("api/[controller]")]
[ApiController]
public class StoresController : ControllerBase
{
    private readonly ShopContext _db;
    public StoresController(ShopContext db) { _db = db; }

    /// <summary>
    /// GET /api/stores
    /// Liefert alle Stores und projiziert das Ergebnis auf die Klasse StoreDto.
    /// Natürlich kann auch Automapper verwendet werden, um das zu erledigen.
    /// </summary>
    [HttpGet]
    public IActionResult GetAllStores() =>
        Ok(_db.Stores.Select(s => new StoreDto(s.Guid, s.Name)));

    [HttpPost]
    public IActionResult AddStore([FromBody] StoreDto storeDto)
    {
        var store = new Store(name: storeDto.Name);
        try
        {
            _db.Stores.Add(store);
            _db.SaveChanges();
        }
        catch (DbUpdateException)
        {
            return BadRequest("Fehler beim Einfügen des Stores.");
        }
        return Ok(new StoreDto(guid: store.Guid, name: store.Name));
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
```

Bei der Klasse *Offer* ist die Projektion natürlich aufwändiger. Beachte, dass named arguments
oder externe Methoden nicht verwendet werden können, da EF Core eine SQL Expression aus der
LINQ Abfrage erzeugt.

In der POST und PUT Route wird natürlich nur *OfferDtoBase* als Parameter gelesen, während die
GET Route das "volle" Offer (also *OfferDto*) exportiert. Somit wird hier zwischen Senden und
Empfangen unterschieden.

```c#
[Route("api/[controller]")]
[ApiController]
public class OffersController : ControllerBase
{
    private readonly ShopContext _db;
    public OffersController(ShopContext db) { _db = db; }

    [HttpGet]
    public IActionResult Get() =>
        Ok(_db.Offers
            .Select(o => new OfferDto(o.Guid, o.Price,
                new ProductDto(
                    o.Product.Ean, o.Product.Name,
                    new ProductCategoryDto(o.Product.ProductCategory.Name, o.Product.ProductCategory.Guid)),
                new StoreDto(o.Store.Guid, o.Store.Name))));

    [HttpGet("{guid}")]
    public IActionResult GetOfferByGuid(Guid guid) { /* ... */}
    [HttpPost]
    public IActionResult GetOfferByGuid([FromBody] OfferDtoBase storeDto) { /* ... */ }
}
```