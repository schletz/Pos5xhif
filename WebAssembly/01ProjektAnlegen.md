# Anlegen einer Solution mit WebAPI + WebAssembly

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
```
Damit das nullable Feature überall aktiv ist, ändern wir alle csproj Dateien.

```xml
<PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
    <Nullable>enable</Nullable>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
</PropertyGroup>
```

Danach muss im Wasm Projekt die FetchData Komponente entfernt werden (verwendet
noch keine nullable reference types) und ggf. anderer Code auf nullable reference
types umgestellt werden.

Installiere über NuGet im Api Projekt das Paket *Microsoft.AspNetCore.Components.WebAssembly.Server*.

```c#
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

