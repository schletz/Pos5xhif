# Anlegen der ersten Komponente

Wird das API Projekt gestartet (mit Visual Studio oder über dotnet run), so ist bereits eine
kleine WASM Applikation sichtbar. Nun wollen wir die Stores auflisten.

Die Entwicklung in Blazor erfolgt über Komponenten. Es sind kleine "Puzzlestücke", die ein
Gesamtbild (die Webapp) bilden.

Alle nachfolgenden Schritte sind im Wasm Projekt durchzuführen.

## Erstellen der Komponente Stores

Für die Auflistung der Stores erstellen wir im Ordner Pages eine Razor Component mit dem Namen
*Stores.razor*. Die erste Direktive (*@pages*) gibt an, dass es sich um eine routingfähige
Komponente handelt. Sie kann mit der URL */stores* erreicht werden.

Der gesamte Code von *Stores.razor* sieht dann so aus:
```html
@page "/stores"
<h3>Stores</h3>
<h4>Alle Stores</h4>
<table class="table table-sm">
    <thead>
        <tr>
            <th>GUID</th>
            <th>Name</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var s in StoreList)
        {
            <tr>
                <td>@s.Guid</td>
                <td>@s.Name</td>
            </tr>
        }
    </tbody>
</table>
```

Nun brauchen wir auch Code hinter der Komponente. Dafür fügen wir im Ordner Pages eine C# Klassendatei
mit dem Namen *Stores.razor.cs* hinzu. 

> **Wichtig:** Ändere die Klasse in eine *partial* class. Die Komponente selbst stellt bereits die
> Klasse Stores dar.

```c#
using Microsoft.AspNetCore.Components;
using ScsOnlineShop.Dto;                 // DTO Projekt
using System;
using System.Net.Http; 
using System.Threading.Tasks;
using System.Net.Http.Json;              // Für GetFromJsonAsync()

namespace ScsOnlineShop.Wasm.Pages
{
    public partial class Stores
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;
        public StoreDto[] StoreList { get; private set; } = Array.Empty<StoreDto>();

        protected override async Task OnInitializedAsync()
        {
            // using System.Net.Http.Json;
            StoreList = await HttpClient
                .GetFromJsonAsync<StoreDto[]>("/api/stores") ?? Array.Empty<StoreDto>(); ;
        }

    }
}
```

Folgende Dinge fallen auf:
- Mit [Inject] können - wie in ASP üblich - Services genutzt werden. Da wir keinen Konstruktor
  definieren können (das macht bereits die Komponente selbst), müssen wir mit einer Annotateion
  arbeiten. Die Zuweisung von *default!* verhindert Warnungen bei aktivierten nullable reference
  types.
- Das Service wird in der Datei Program.cs registriert.
- Die DTO Klassen sind in einem gemeinsamen Projekt. Dadurch können wir auch am Client darauf
  zugreifen und den Datentyp, den die API sendet, wiederherstellen.

## Komponenten verwenden Komponenten: AddStores

Manche Komponenten sollen nicht routingfähig sein, aber ein "Baustein" einer anderen Komponente sein.
Dafür erstellen wir einen Ordner *Components* in der obersten Ebene des Projektes (also auf einer
Stufe wie Pages oder Shared). Damit wir leichteren Zugriff darauf (und auf die DTO Klassen haben),
bearbeiten wir die Datei *_Imports.razor* und ergänzen 2 usings:

```c#
@using ScsOnlineShop.Dto
@using ScsOnlineShop.Wasm.Components
```

Nun fügen wir im Ordner Components eine neue Razor component hinzu und benennen sie AddStore.razor.

```html
<EditForm Model="@NewStore" OnValidSubmit="@HandleValidSubmit">
    <DataAnnotationsValidator />
    <ValidationSummary />
    <InputText id="name" @bind-Value="NewStore.Name" />
    <button type="submit" class="btn-primary">Submit</button>
</EditForm>

@if (!string.IsNullOrEmpty(ErrorMessage))
{
    <p class="text-danger">@ErrorMessage</p>
}
```

Für die Logik erstellen wir wieder eine Klasse *AddStore.razor.cs*:

```c#
using Microsoft.AspNetCore.Components;
using ScsOnlineShop.Dto;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ScsOnlineShop.Wasm.Components
{
    public partial class AddStore
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;

        public string? ErrorMessage { get; private set; }
        public StoreDto NewStore { get; set; } = new(default, string.Empty);

        public async Task HandleValidSubmit()
        {
            var storeDto = new StoreDto(default, NewStore.Name);
            var result = await HttpClient.PostAsJsonAsync("api/store", storeDto);
            if (!result.IsSuccessStatusCode)
            {
                ErrorMessage = $"Fehler beim Senden der Daten. Statuscode {result.StatusCode}";
            }
        }
    }
}
```

Nun kann die neu erstelle Komponente in *Stores.razor* verwendet werden:

```html
@page "/stores"
<h3>Stores</h3>
<h4>Neuer Store</h4>
<div>
    <AddStore />
</div>
<h4>Alle Stores</h4>
@*...*@
```