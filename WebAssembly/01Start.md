# Anlegen einer .NET 6 Solution mit WebAPI + WebAssembly

Im Allgemeinen braucht eine Webassembly 3 Projekte:
- Eine API, die Daten aus der Datenbank zur Verfügung stellt und Daten zum Schreiben empfängt.
- Ein gemeinsames Projekt, damit die DTO Klassen serialisiert und deserialisiert werden können.
- Die clientseitige Webassembly mit den Blazor Komponenten.

Mit *dotnet new* können die Projekte angelegt und referenziert werden. **Achtung: Dieses
Skript benötigt .NET 6**.

```text
md ScsOnlineShop
cd ScsOnlineShop
md ScsOnlineShop.Api
md ScsOnlineShop.Wasm
md ScsOnlineShop.Shared

cd ScsOnlineShop.Shared
dotnet new classlib

cd ..\ScsOnlineShop.Wasm
dotnet new blazorwasm
dotnet add reference ..\ScsOnlineShop.Shared

cd ..\ScsOnlineShop.Api
dotnet new webapi
dotnet add package Microsoft.AspNetCore.Components.WebAssembly.Server --version 6.*
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 6.*
dotnet add package System.IdentityModel.Tokens.Jwt --version 6.*
dotnet add reference ..\ScsOnlineShop.Wasm
dotnet add reference ..\ScsOnlineShop.Shared

cd ..
dotnet new sln
dotnet sln add ScsOnlineShop.Api
dotnet sln add ScsOnlineShop.Wasm
dotnet sln add ScsOnlineShop.Shared
```
Damit das nullable Feature überall aktiv ist, ändern wir alle *csproj* Dateien und ergänzen die
Optionen *Nullable* und *TreatWarningsAsErrors*.

```xml
<PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <Nullable>enable</Nullable>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
</PropertyGroup>
```

## Konfigurieren des API Projektes

Konfiguriere die Datei *Program.cs* im API Projekt so, dass sie - wenn kein Controller
als Endpoint für eine Adresse vorhanden ist - die Datei *index.html* ausliefert:

** Program.cs **
```c#
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ScsOnlineShop.Api.Services;
using ScsOnlineShop.Application.Infrastructure;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();
// Liefert das verknüpfte Wasm Projekt als Webassembly aus.
// NUGET: Microsoft.AspNetCore.Components.WebAssembly.Server
app.UseBlazorFrameworkFiles();
// Damit Assets (Bilder, ...) in der WASM geladen werden können.
app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();
```

Soll auch eine Datenbank verwendet werden, kann diese in als Service gleich registriert
werden:

```c#
var opt = new DbContextOptionsBuilder()
    .UseSqlite("Data Source=Shop.db")
    .Options;
using (var db = new ShopContext(opt))
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    db.Seed();
}
builder.Services.AddDbContext<ShopContext>(opt =>
    opt.UseSqlite("Data Source=Shop.db")
        .UseLazyLoadingProxies());
        
```