using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;

var options = new DbContextOptionsBuilder()
    .UseSqlite("Data Source=languageweek.db")
    .Options;

using (var db = new LanguageweekContext(options))
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    db.Seed();
}

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddDbContext<LanguageweekContext>(opt => opt.UseSqlite("Data Source=languageweek.db"));
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();