# Authentication und Authorization: Implementierung in der API

> **Hinweis:** Dieses Kapitel setzt Voraus, dass die Grundlagen im
> Dokument [Authentication und Authorization](../AuthenticationPrinciples) bekannt sind. Falls
> nicht, lies das Kapitel bitte vorher durch.

Die nachfolgenden Punkte beziehen sich auf das Projekt *ScsOnlineShop.Api*.

## Generieren eines Secrets

Damit der Server die Authentizität eines JWT prüfen kann, muss zuerst ein Secret generiert
werden. Dies kann z. B. auf https://generate.plus/en/base64 erledigt werden. Soll das Secret
1024 Bit lang sein, sind 128 Bytes zu generieren. Das Secret darf *nicht* als URL Safe Base64
generiert werden, da die *FromBase64String()* Methode sonst eine Exception wirft.

Nun wird das generierte Secret in die Datei *appsettings.json* im API Projekt eingefügt:

```javascript
{
  "Secret": "/4l8fgdxHhEG/ND0si06s3MgUzN+aTLVDw0ekXpc3sp4hTFybGqf88OiFMgr9dGmLjF75leRPSE0GTITjEa0AI3b6ZD6qTh9oSEJmTezzmEEX+vEQbOV46REK3Ii14yHKAtTXbfdN8jmS3eeO4Fz8bV4pRMMrpbphrIIsQRDkH8=",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

## NuGet Pakete

Zuerst werden im API Projekt 2 NuGet Pakete hinzugefügt. Dies kann durch Einfügen der *PackageReference*
Elemente in der *.csproj* Projektdatei geschehen. *Achtung: Verwende Version 5.* bei ASP.NET Core 5 Projekten.*

```xml
<PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="6.*" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="6.*" />
```

## Registrierung des AuthSerivce und Aktivieren der Authentication

In der Datei [AuthService.cs](ScsOnlineShop/ScsOnlineShop.Api/Services/AuthService.cs)
ist das verwendete AuthService bereits fertig implementiert und muss nur mehr angepasst werden.
Es hat zwei Parameter: Das Secret und ein Flag, ob der Server im Development Mode ausgeführt wird.

Bevor es verwendet werden kann, muss zuerst die Serverkonfiguration vorbereitet werden:

In der Klasse *Startup* werden dafür folgende Änderungen eingebaut:
- *IWebHostEnvironment* wird über Dependency Injection zum Konstruktor hinzugefügt.
- *services.AddAuthentication()* wird eingefügt und konfiguriert.
- Das *AuthService* wird registriert und die erforderlichen Parameter werden übergeben.
- Die Middleware wird mit *app.UseAuthentication()* und *app.UseAuthorization()* aktiviert.


**ASP.NET Core 6 (Program.cs)**
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
/* Other Usings */

var builder = WebApplication.CreateBuilder(args);

byte[] key = Convert.FromBase64String(builder.Configuration["Secret"]);
builder.Services.AddTransient<AuthService>(provider => new AuthService(
    builder.Configuration["Secret"],
    builder.Environment.IsDevelopment()));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// OTHER SERVICES ****************
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

}
app.UseStaticFiles();
app.UseBlazorFrameworkFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();

```

## Das Auth Service

Das Herzstück des Services [AuthService.cs](ScsOnlineShop/ScsOnlineShop.Api/Services/AuthService.cs)
ist die Methode *TryLogin()*. Sie generiert den Token, wenn die übermittelten Benutzerdaten
richtig sind.

> **Hinweis:** Die Methode *TryLogin()* muss angepasst werden. Die aktuelle Implementierung
> lässt jedes Passwort und jeden Benutzernamen im Development Mode zu. Im Release Mode lehnt
> sie alle Daten ab.

```c#
public class AuthService
{
    private readonly byte[] _secret;
    private readonly bool _isDevelopment;

    public AuthService(string secret, bool isDevelopment)
    {
        _secret = Convert.FromBase64String(secret);
        _isDevelopment = isDevelopment;

    }
    public (bool success, UserDto? user) TryLogin(string username, string password)
    {
        // TODO: Echte Passwortprüfung gegen die Datenbank, ... einfügen und je nach
        //       Bedarf _isDevelopment prüfen, um jedes Passwort im Development Mode zu erlauben.
        if (!_isDevelopment) { return (false, null); }
        // TODO: Rolle entsprechend des Benutzers setzen.
        var role = "admin";

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            // Payload für den JWT.
            Subject = new ClaimsIdentity(new Claim[]
            {
                // Benutzername als Typ ClaimTypes.Name.
                new Claim(ClaimTypes.Name, username),
                // Rolle des Benutzer als ClaimTypes.DefaultRoleClaimType setzen.
                // ACHTUNG: Muss mit den Annotations der Routen in [Authorize(Roles = "xxx")]
                // übereinstimmen.
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_secret),
                SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return (true, new UserDto(
            Username: username, 
            Role: role, 
            Token: tokenHandler.WriteToken(token)));
    }
}

```

## Controller für den Login Request

Im Verzeichnis *Controllers* wird eine Klasse *UserController* angelegt. Er ruft das AuthService
auf, welches das Passwort prüft. Im Erfolgsfall wird ein Userobjekt (bestehend aus Username,
Rolle und Token) an den Server gesendet. Nun kann der Client an */api/user/login* Benutzername
und Passwort senden und bekommt ein Userobjekt zurück.

```c#
public class UserController : ControllerBase
{

    private readonly AuthService _authService;

    public UserController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto login)
    {
        var (success, user) = _authService.TryLogin(login.Username, login.Password);
        if (!success) { return Unauthorized(); }
        return Ok(user);
    }
}
```

## Absichern anderer Controller

Mit der Annotation *Authorize* kann nun jeder Controller abgesichert werden. Dadurch werden
Requests ohne Token oder mit ungültigem Token mit HTTP 401 (unauthorized) quittiert.

```c#
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class StoresController : ControllerBase
{
    // ...
}
```