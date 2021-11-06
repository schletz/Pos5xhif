# Anlegen von Controllern für CRUD Operationen

Das Projekt im Ordner [ScsOnlineShop](ScsOnlineShop) verwendet folgendes kleines EF Core Klassenmodell:

![](klassenmodell.svg)

## Anpassen des Models

Damit wir z. B. einen Store erstellen können, sollten wir eine GUID als Key definieren. Damit wird
verhindert, dass wir den internen Autoincrement Wert an den Client senden.

Der Wert wird im Konstruktor gesetzt, so dass ein neuer Store sicher einen gültigen GUID Wert
besitzt.

```c#
public class Store
{
    public Store(string name)
    {
        Name = name;
        Guid = Guid.NewGuid();
    }

    protected Store() { }                     // Für EF Core
    public int Id { get; private set; }       // PK ist read-only
    public string Name { get; set; } = default!;
    public virtual ICollection<Offer> Offers { get; } = new List<Offer>();
    public Guid Guid { get; private set; }    // GUID ist read-only
}
```

## Erstellen der DTO Klasse

Damit die Daten über JSON ausgetauscht werden können (wir haben schließlich eine Client/Server
Applikation), definieren wir eine DTO Klasse.

> **Hinweis:** Die DTO Klasse ist bewusst kein C# 9 record, also nicht immutable. Wird ein neuer
> Store erfasst, schreibt das HTML Input Feld den Wert in eine Instanz von StoreDto. Das widerspricht
> natürlich dem Konzept von records.

```c#
public class StoreDto
{
    public StoreDto(Guid guid, string name)
    {
        Guid = guid;
        Name = name;
    }

    public Guid Guid { get; set; }

    [Required(ErrorMessage = "Fehlender Name")]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "Ungültiger Name")]
    public string Name { get; set; }
}
```

Nun wird im API Projekt ein Ordner *Controllers* erstellt, falls er nicht schon vorhanden ist.
Darin erstellen wir die Klasse *StoresController*, um auf Requests mit der URL */api/stores* zu
reagieren.

```c#
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
    public IActionResult GetAllStores()
    {
        return Ok(_db.Stores.Select(s => new StoreDto(s.Guid, s.Name)));
    }
}
```

