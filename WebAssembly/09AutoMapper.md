# Automapper für DTO Klassen

## Installieren der Automapper Pakete

Zuerst wird im *Application Projekt* (dort wo auch das Datenmodell definiert wird) eine Referenz
auf 2 Pakete hinzugefügt. Dies kann direkt in der *csproj* Datei oder über den NuGet 
Installer durchgeführt werden.

```xml
<PackageReference Include="AutoMapper" Version="10.*" />
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="8.*" />
```

## Erstellen von Mappings

Im Projekt *Application* erstellen wir nun einen Ordner *Mappings* und darin eine Klasse
*ShopMappingProfile*. Eine Mappingklasse wird von der Klasse Profile (aus Automapper) abgeleitet
und besitzt einen Konstruktor.

Dieser Konstruktor fügt nun mit *CreateMap\<TSource, TDest\>* ein Mapping hinzu. Der erste
Parameter ist der source type, der zweite Typparameter ist der destination type.

```c#
using AutoMapper;
using ScsOnlineShop.Application.Model;
using ScsOnlineShop.Shared.Dto;

namespace ScsOnlineShop.Application.Mappings
{
    public class ShopMappingProfile : Profile
    {
        public ShopMappingProfile()
        {
            // Mapping from Store --> StoreDto
            CreateMap<Store, StoreDto>();
            CreateMap<StoreDto, Store>();
            CreateMap<Offer, OfferDto>();
            CreateMap<Product, ProductDto>();  // Offer contains type product, so we have to add a mapping for product.
            CreateMap<ProductCategory, ProductCategoryDto>();  // Offer contains type product, so we have to add a mapping for product.
        }
    }
}


```
## Registrieren des Services

Damit Automapper über Dependency Injection in der API verwendet werden kann, muss das Service in
der Datei *Program.cs* im API Projekt registriert werden:

```c#
builder.Services.AddAutoMapper(typeof(ShopMappingProfile));
```

Die Klasse *ShopMappingProfile* kann eine beliebige Klasse aus dem Application Projekt sein. Sie
gibt nur an, in welcher Assembly Automapper nach Mappings suchen soll.

Aus Performancegründen ist ein "Live Mapping" ohne Profil nicht mehr möglich. Durch die
Mappingklasse kann nämlich Automapper vorab die entsprechenden Delegates für die Mappings
generieren und muss nicht bei jedem Mappingvorgang über Reflection die Klasse durchsuchen.

## Map und ProjektTo

Nachfolgend ist der StoresController abgebildet, der nun Automapper verwendet. Durch
Dependency Injection wird der Mapper über das IMapper Interface nutzbar gemacht. Danach können
2 Methoden aufgerufen werden:
- **ProjektTo\<TDest\>()** projiziert eine Collection auf eine Collection des Zieltyps. Das ist für
  Datenbankabfragen nützlich.
- **Map\<TDest\>** bildet eine Instanz anhand der Mappingprofiles auf den Zieltyp ab.
  
```c#
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

    [HttpGet]
    public IActionResult GetAllStores() =>
        Ok(_mapper.ProjectTo<StoreDto>(_db.Stores));

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
}
```

## Häufige Fehler

Die Klasse *OfferDto* verwendet z. B. Referenzen auf *ProductDto* und *StoreDto*. Daher müssen auch
für *ProductDto* und *StoreDto* Mappingprofile existieren. Sonst entsteht ein Laufzeitfehler.

Weiters haben unsere DTO Klassen einen Konstruktor. Werden Properties nicht durch den
Konstruktor initialisiert (wie z. B. Collections), brauchen sie einen public setter.

```c#
public class StoreDto
{
    public StoreDto(Guid guid, string name) { /* ... */ }
    public string Guid { get; }
    public string Name { get; set; }
    // Needs public set
    public IReadOnlyList<OfferDto> Offers { get; set; } = new List<OfferDto>();
}
```

Falls ein private setter gewünscht ist, muss das Mapping angepasst werden
(vgl.https://newbedev.com/automapper-mapping-properties-with-private-setters).