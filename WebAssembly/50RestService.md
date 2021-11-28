# Serveranfragen besser koordinieren: das Rest Service

Dieses Kapitel betrifft das Web Assembly (*ScsOnlineShop.Wasm*) Projekt.

Bis jetzt wurden Anfragen an den Server immer direkt über den HttpClient durchgeführt.
Mit der Methode *GetFromJsonAsync* gibt es bereits eine komfortable Möglichkeit, Daten
an den Server zu senden:

```c#
public partial class Stores
{
    [Inject]
    public HttpClient HttpClient { get; set; } = default!;

    public List<StoreDto> StoreList { get; private set; } = new();

    protected override async Task OnInitializedAsync()
    {
        // using System.Net.Http.Json;
        StoreList = await HttpClient
            .GetFromJsonAsync<List<StoreDto>>("/api/stores") ?? new();
    }

    public void OnStoreAdded(StoreDto storeDto)
    {
        StoreList.Add(storeDto);
    }
}
```
Gerade in Hinblick auf Authentifizierung, wo ein Token mit den Anfragen mitgesendet werden
muss, möchten wir nun ein eigenes Service schreiben.

Das RestService ist bereits fertig in der Datei [](ScsOnlineShop/ScsOnlineShop.Wasm/Services/RestService.cs)
implementiert. Die wichtigste Methode ist *SendAsync*, die einfach Abfragen an die API sendet:

```c#
// Sendet einen Request an /api/stores und deserialisiert das Ergebnis in
// eine Liste von StoreDto Objekten.
var result1 = await restSrvice.SendAsync<List<StoreDto>>(HttpMethod.Get, "stores");

// Sendet einen POST Request an /api/stores mit einem Request Body.
// NewStore ist ein beliebiges Objekt und wird als JSON als Payload mit dem Request mitgesendet.
var newStore = await RestService.SendAsync<StoreDto>(HttpMethod.Post, "stores", NewStore);
```

## Registrieren des Services in der Webassembly

In der Datei *Program.cs* wird das Service mit *AddSingleton()* registriert.

> **Hinweis:** Singleton Services werden einmal instanziert. Sie werden benötigt, um Zustände
> während der ganzen Applikationslaufzeit zu halten. Verwende sie nur wenn das benötigt wird.
> *AddTransient()* ist oft der bessere Zugang. Dann wird bei jedem Inject Vorgang eine neue
> Instanz erstellt.

```c#
public static async Task Main(string[] args)
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("#app");
    builder.Services.AddSingleton(provider => new RestService($"{builder.HostEnvironment.BaseAddress}api/"));
    await builder.Build().RunAsync();
}
```

Der Ausdruck *$"{builder.HostEnvironment.BaseAddress}api/* hängt an die URL der Webassembly
(z. B. localhost:5432/) einfach den String *api/* an, damit alle Anfragen automatisch an den
API Controller gehen.

Nun wird im *_Imports.razor* die Zeile *@using ScsOnlineShop.Wasm.Services;* hinzugefügt.
Dadurch werden die Services in den Components ohne extra *@using* gefunden.

```
@* Other Usings *@
@using ScsOnlineShop.Wasm
@using ScsOnlineShop.Wasm.Shared;
@using ScsOnlineShop.Wasm.Services;
@using ScsOnlineShop.Wasm.Components;
@using ScsOnlineShop.Shared.Dto;
```
