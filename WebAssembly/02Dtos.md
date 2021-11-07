# DTO Klassen

- Werden im API Projekt über den JSON Serializer serialisiert und als JSON an den Client gesendet.
- Werden im Webassembly Projekt über den JSON Serializer deserialisiert und als typisiertes Objekt
  wiederhergestellt.
- Dienen HTML Formularen als Model zur Speicherung der eingegebenen Daten.

## Wo werden DTO Klassen angelegt?

Beim Anlegen der Solution wurde bereits ein Projekt Shared vom Typ *classlib* angelegt. Auf dieses
Projekt verweisen das API und das Webassembly Projekt. Dadurch ist klar, dass die DTO Klassen
im Projekt *Shared* angelegt werden müssen.

Um Ordnung zu halten, erstellen wir einen Unterordner *Dto* im Projekt *Shared*.

## GUID in den Modelklassen definieren

Die Modelklasse Store besteht im Moment nur aus 2 Properties: *Id* und *Name*. Als ID wird ein
Autoincrement Wert verwendet. **Vorsicht: Sende Autoincrement Werte nie an den Client. Durch
Erhönen des Wertes kann der nächste Datensatz leicht erraten werden!**

Deswegen fügen wir ein Property *Guid* vom Typ *Guid* ein. Es kann als read-only Property definiert
werden. Achte darauf, dass im public Konstruktor der Wert initialisiert wird. Sonst ist die GUID
immer 0 und wird beim Einfügen des 2. Datensatzes eine Exception verursachen.

```c#
public class Store
{
    public Store(string name)
    {
        Name = name;
        Guid = Guid.NewGuid();
    }
    protected Store() { }
    public int Id { get; private set; }
    public string Name { get; set; } = default!;
    public virtual ICollection<Offer> Offers { get; } = new List<Offer>();
    // Muss mit HasAlternateKey() in ShopContext.OnModelCreating() definiert werden.
    public Guid Guid { get; }  
}
```

Read-only Properties werden normalerweise von EF Core nicht berücksichtigt. Mit einer Konfiguration in
*ShopContext.OnModelCreating()* können wir dieses Property allerdings zu einem "alternate Key"
machen (vgl. https://docs.microsoft.com/en-us/ef/core/modeling/keys?tabs=data-annotations#alternate-keys).
Damit wir nicht jede Modelklasse händisch konfigurieren müssen, können wir einfach über Reflection das
Property *Guid* - wenn vorhanden - als alternate key definieren. Natürlich müssen die betroffenen
Properties in allen Modelklassen Guid heißen.

```c#
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    foreach (var entity in modelBuilder.Model.GetEntityTypes())
    {
        var type = entity.ClrType;
        if (type.GetProperty("Guid") is not null)
            modelBuilder.Entity(type).HasAlternateKey("Guid");
    }
    // ...
}
```

## DTO Klasse anlegen

Nun wollen wir in *Shared/Dto* eine Klasse *StoreDto* anlegen. Das ist allerdings schwieriger als
man denkt, wenn wir en Anforderungen des JSON Serializers und der *EditForm* Komponente in Blazor
Rechnung tragen wollen.

```c#
public class StoreDto
{
    public StoreDto(Guid guid, string name)
    {
        Guid = guid;
        Name = name;
    }

    public Guid Guid { get; }

    [Required(ErrorMessage = "Fehlender Name")]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "Ungültiger Name")]
    public string Name { get; set; }
}
```

Wir erkennen folgende Besonderheiten:

- Es gibt einen Konstruktor mit allen Argumenten. Dieser Konstruktor wird automatisch vom
  JSON Serializer verwendet und aufgerufen. Daher darf es nur einen Konstruktor in der DTO
  Klasse geben.
- Das Property *Guid* ist read-only. Da wir dieses Feld nicht mittels HTML Formular bearbeiten
  wollen, kann es so definiert werden.
- Das Property *Name* hat ein offenes set Property. Das ist wichtig, denn das HTML Input Element wird
  den eingegebenen Wert hier hineinschreiben.
- Wir nutzen Annotations aus dem Namespace *System.ComponentModel.DataAnnotations*, um eine Validierung
  zu ermöglichen. Ob Annotations zu DTO Klassen gehören gibt es unterschiedliche Ansichten. Sie
  werden aber die Programmentwicklung wesentlich vereinfachen.
- Die Navigation *Offers* aus dem Model wird nicht exportiert.

## Navigation Properties und DTO Klassen

Schwieriger wird es schon bei der Klasse *Offer*. Wollen wir sie an den Client senden, so sollten
die Navigations zu Product und Store enthalten sein. Nur mit den Schlüsselwerten würde sich keine
vernünftige GUI aufbauen lassen.

```c#
public class Offer
{
    public Offer(Product product, Store store, decimal price) {/* ... */ }
    protected Offer() { }

    public int Id { get; private set; }
    public int ProductEan { get; set; }
    public virtual Product Product { get; set; } = default!;
    public int StoreId { get; set; }
    public virtual Store Store { get; set; } = default!;
    public decimal Price { get; set; }
    public DateTime LastUpdate { get; set; }
    public Guid Guid { get; private set; }
}
```

Unsere DTO Klasse für Offer würde also EAN, Product, Store, Price und Guid beinhalten. Aber es gibt
2 Probleme:

1. *Product* und *Store* sind Modelklassen, sie müssen daher auch in DTO Klassen abgebildet werden.
2. Soll ein neues Offer angelegt werden, liefern die entsprechenden HTML Formularelemente wie
   Dropdowns, ... kein komplettes Product sondern nur den Fremdschlüssel. Das Senden des gesamten
   Product Datensatzes ist für das Anlegen eines Offers auch gar nicht nötig.

Wir lösen daher das Problem mit Vererbung. Zuerst wird die Klasse *OfferDtoBase* definiert. Sie
hat *keine Navigations* und kann verwendet werden, wenn ein neues Offer angelegt werden soll.
Der Client befüllt dann EAN, Store und Preis. Der GUID Wert für das Offer selbst ist dem Client
noch nicht bekannt und daher *default* (also 0).

```c#
public class OfferDtoBase
{
    public OfferDtoBase(Guid guid, int productEan, Guid storeGuid, decimal price)
    {
        Guid = guid;
        ProductEan = productEan;
        StoreGuid = storeGuid;
        Price = price;
    }

    public Guid Guid { get; }
    public int ProductEan { get; set; }
    public Guid StoreGuid { get; set; }
    public decimal Price { get; set; }
}
```

Damit wir aber vom Server z. B. eine vernünftige Liste von Offers an den Client zur Anzeige
schicken können, bauen wir die Navigations in einer abgeleiteten Klasse ein. Beachte, dass
alle Properties hier read-only sind, da sie am Client nicht gesetzt werden.

Beachte den Konstruktor, der die Fremdschlüssel aus den übergebenen Objekten liest und an den
Konstruktor von *OfferDtoBase* weitergibt.

```c#
public class OfferDto : OfferDtoBase
{
    public OfferDto(Guid guid, decimal price, ProductDto product, StoreDto store) :
        base(guid, product.Ean, store.Guid, price)
    {
        Product = product;
        Store = store;
    }

    public ProductDto Product { get; }
    public StoreDto Store { get; }
}
```

