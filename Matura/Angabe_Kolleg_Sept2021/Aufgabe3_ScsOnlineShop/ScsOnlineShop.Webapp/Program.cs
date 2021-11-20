using Microsoft.EntityFrameworkCore;
using ScsOnlineShop.Application.Infrastructure;

var opt = new DbContextOptionsBuilder()
    .UseSqlite("Data Source=Shop.db")
    .Options;

using (var db = new ShopContext(opt))
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    db.Seed();
}

/* CONFIGURE SERVICES */
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ShopContext>(opt =>
{
    opt.UseSqlite("Data Source=Shop.db")
    .UseLazyLoadingProxies();
});

/* CONFIGURE */
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();
