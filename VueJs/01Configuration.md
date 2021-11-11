# ASP.NET Core Applikation mit Vue.js

## Download von Vue.Js

Zuerst wird wie auf https://v3.vuejs.org/guide/installation.html#release-notes beschrieben
die letzte Version von https://unpkg.com/vue@next geladen und in *ScsOnlineShop.Webapp/wwwroot/lib*
unter dem Namen *vue3.global.js* gespeichert. Wie verwenden für die Entwicklung keine minified
Version. Für das Release sind entsprechende Versionen erhältlich (siehe https://v3.vuejs.org/guide/installation.html#from-cdn-or-without-a-bundler).

> **Achtung:** Achte auf die Version. Die letzte Version ist Vue.js 3, es wird aber oft auf
> die Version 2 verwiesen.

Nun bauen wir im Head Bereich von *ScsOnlineShop.Webapp/Pages/Shares/_Layout.cshtml* mit
*@RenderSection* eine Section mit dem Namen *Head* ein:

```html
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - ScsOnlineShop.Webapp</title>
    @RenderSection("Head", required: false)
    <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
    <link rel="stylesheet" href="~/ScsOnlineShop.Webapp.styles.css" asp-append-version="true" />
</head>
```

In diese Section wird dann - bei Bedarf - das Include für die Vue.js Bibliothek geschrieben. Somit
wird diese Datei nicht auf jeder Seite geladen. Falls das gewünscht ist, kann sie natürlich im
Hauptlayout inkludiert werden.

In der Datei *ScsOnlineShop.Webapp/Program.cs* ergänzen wir mit *AddAntiforgery()* eine
Option, dass der sogenannte anti forgery token aus dem HTTP Header berücksichtig wird. Das
ist wichtig, um POST, PUT und DELETE Requests über die Fetch API an den Server zu senden:

**ASP.NET Core 6 Template: ScsOnlineShop.Webapp/Program.cs** 
```c#
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddAntiforgery(o => o.HeaderName = "xsrf-token");
var app = builder.Build();
/* ... */
```

Falls die Webapplikation noch mit .NET 5 erstellt wurde, muss diese Option in der Klasse *Startup*
erfolgen:

**ASP.NET Core 5 Template: Startup.cs** 
```c#
public void ConfigureServices(IServiceCollection services)
{
    /* ... */
    services.AddAntiforgery(o => o.HeaderName = "xsrf-token");
    /* ... */
}
```

## Datenbank registrieren

Damit wir auf die Datenbank zugreifen können, wird der Kontext *ShopContext* registriert.
Im ASP.NET Core 5 Template erfolgt diese Konfiguration in *Startup/ConfigureServices()*.

**ASP.NET Core 6 Template: ScsOnlineShop.Webapp/Program.cs** 
```c#
using Microsoft.EntityFrameworkCore;
using ScsOnlineShop.Application.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

/* ... */
builder.Services.AddDbContext<ShopContext>(opt =>
{
    opt
        .UseSqlite("DataSource=Shop.db")
        .UseLazyLoadingProxies();
});
var app = builder.Build();
/* ... */
```

Damit unsere Datenbank auch Werte hat ("geseeded" wird), rufen wir in der Datei *Program.cs*
noch die *Seed()* Methode vor *CreateBuilder()* auf. Auch im ASP.NET Core 5 Template steht
dieser Code in der Datei *Program.cs* an erster Stelle der Main Methode.

```c#
using Microsoft.EntityFrameworkCore;
using ScsOnlineShop.Application.Infrastructure;

var opt = new DbContextOptionsBuilder()
    .UseSqlite("DataSource=Shop.db")
    .Options;

using (var db = new ShopContext(opt))
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    db.Seed();
}

var builder = WebApplication.CreateBuilder(args);
/* ... */

```
