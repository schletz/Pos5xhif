# Anlegen einer Solution mit WebAPI + WebAssembly

Im Allgemeinen braucht eine Webassembly 3 Projekte:
- Eine API, die Daten aus der Datenbank zur Verfügung stellt und Daten zum Schreiben empfängt.
- Ein gemeinsames Projekt, damit die DTO Klassen serialisiert und deserialisiert werden können.
- Die clientseitige Webassembly mit den Blazor Komponenten.

Mit *dotnet new* können die Projekte angelegt und referenziert werden:
```text
md ScsOnlineShop
cd ScsOnlineShop
md ScsOnlineShop.Api
md ScsOnlineShop.Wasm
md ScsOnlineShop.Dto

cd ScsOnlineShop.Dto
dotnet new classlib

cd ..\ScsOnlineShop.Wasm
dotnet new blazorwasm
dotnet add reference ..\ScsOnlineShop.Dto

cd ..\ScsOnlineShop.Api
dotnet new webapi
dotnet add reference ..\ScsOnlineShop.Wasm
dotnet add reference ..\ScsOnlineShop.Dto

cd ..
dotnet new sln
dotnet sln add ScsOnlineShop.Api
dotnet sln add ScsOnlineShop.Wasm
dotnet sln add ScsOnlineShop.Dto
```
Damit das nullable Feature überall aktiv ist, ändern wir alle *csproj* Dateien und ergänzen die
Optionen *Nullable* und *TreatWarningsAsErrors*.

```xml
<PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
    <Nullable>enable</Nullable>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
</PropertyGroup>
```

Danach muss im Wasm Projekt die *FetchData* Komponente entfernt werden (verwendet
noch keine nullable reference types) und ggf. anderer Code durch nullable reference
types ersetzt werden (aus *string* wird *string?* gemacht).

## Konfigurieren des API Projektes

Installiere über NuGet im Api Projekt das Paket *Microsoft.AspNetCore.Components.WebAssembly.Server*.
Nun kann die Startup Klasse in *Startup.cs* geändert werden, dass die Webassembly ausgeliefert wird.
Anfragen an */api* werden wie gewohnt an Controller weitergegeben.



```c#
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseWebAssemblyDebugging();
    }
    // Liefert das verknüpfte Wasm Projekt als Webassembly aus.
    // NUGET: Microsoft.AspNetCore.Components.WebAssembly.Server
    app.UseBlazorFrameworkFiles();
    // Damit Assets (Bilder, ...) in der WASM geladen werden können.
    app.UseStaticFiles();

    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapFallbackToFile("index.html");
    });
}
```

Soll auch eine Datenbank verwendet werden, kann diese in *Startup.ConfigureServices()* gleich registriert
werden:

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<ShopContext>(opt =>
    {
        opt
            .UseSqlite("DataSource=Shop.db")
            .UseLazyLoadingProxies();
    });
    services.AddControllers();
}

```