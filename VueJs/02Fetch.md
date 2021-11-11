# Die erste Page mit AJAX

## Page /Stores anlegen

Nun legen wir im Ordner Pages mit Add - Razor Page eine leere Razor Page (nicht View!) mit dem
Namen *Stores.cshtml* an. Jetzt wird in der Section *Head* auf die Vue.js Library verwiesen:

```html
@page
@model ScsOnlineShop.Webapp.Pages.StoresModel
@section Head {
    <script src=~/lib~/lib/vue3.global.js></script>
}
```

In *Stores.cshtml.cs* wird nun ein sogenannter *PageHandler* mit den Namen *OnGetAll* angelegt.
Razor Pages stellen auch einen Controller dar. Dieser Handler kann mit *?handler=All* aufgerufen
werden. 

```c#
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScsOnlineShop.Application.Infrastructure;
using ScsOnlineShop.Shared.Dto;

namespace ScsOnlineShop.Webapp.Pages
{
    public class StoresModel : PageModel
    {
        private readonly ShopContext _db;

        public StoresModel(ShopContext db)
        {
            _db = db;
        }

        /// <summary>
        /// GET /Stores/All
        /// Endpoint für den AJAX Request. async Task<IActionResult> bei einem async Handler.
        /// </summary>
        public IActionResult OnGetAll()
        {
            var stores = _db.Stores
                .OrderBy(s => s.Name)
                .Select(s => new StoreDto(s.Guid, s.Name)).ToList();
            return new JsonResult(stores);
        }
        /* ... */
    }
}
```

Die DTO Klassen wurden im Projekt *Shared* definiert. Infos über DTO Klassen sind im Thema
Web Assembly unter DTO Klassen zu finden.

Im Browser kann der Handler bereits mit der URL */Stores?handler=All* getestet werden. Somit ist
alles bereit für den ersten Zugriff mit der Fetch API.

## Hot Reload

Damit wir nach einer Änderung im Code nicht das Projekt manuell neu starten können, arbeiten
wir mit *dotnet watch run*. Dafür gehen wir in die Konsole und starten im Ordner *ScsOnlineShop.Webapp*
das Programm:

```text
ScsOnlineShop.Webapp>dotnet watch run
```

> **Hinweis:** Beim neuen Anlegen von Dateien muss der Server manuell neu gestartet werden, da diese
> Datei noch nicht getracked wird.

Nun sieht die gesamte Page *Stores.cshtml* so aus:

```html
@page
@model ScsOnlineShop.Webapp.Pages.StoresModel
@section Head {
    <script src=~/lib~/lib/vue3.global.js></script>
}

<h3>Stores</h3>
<p>Es sind <span id="storesCount">0</span> Stores in der Datenbank.</p>

<ul id="stores">
</ul>
<script>
    fetch("/Stores?handler=All")
    .then(res=>res.json())
    .then(stores=> {
        document.getElementById("storesCount").innerHTML = stores.length;
        const container = document.getElementById("stores");
        for (const store of stores)
        {
            const item = document.createElement("li");
            item.innerHTML = store.name;
            container.appendChild(item);
        }
    })
    .catch(error => alert("Fehler beim Laden der Daten vom Server. Bitte versuchen Sie es erneut."));
</script>
```