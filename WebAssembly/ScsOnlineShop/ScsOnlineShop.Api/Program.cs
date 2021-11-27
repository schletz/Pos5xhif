using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ScsOnlineShop.Api.Services;
using ScsOnlineShop.Application.Infrastructure;
using System;

var opt = new DbContextOptionsBuilder()
    .UseSqlite("Data Source=Shop.db")
    .Options;
using (var db = new ShopContext(opt))
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    db.Seed();
}

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

builder.Services.AddControllers();
builder.Services.AddDbContext<ShopContext>(opt =>
    opt.UseSqlite("Data Source=Shop.db")
        .UseLazyLoadingProxies());

var app = builder.Build();
// Liefert das verknüpfte Wasm Projekt als Webassembly aus.
// NUGET: Microsoft.AspNetCore.Components.WebAssembly.Server
app.UseBlazorFrameworkFiles();
// Damit Assets (Bilder, ...) in der WASM geladen werden können.
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();
