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

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAntiforgery(o => o.HeaderName = "xsrf-token");
builder.Services.AddDbContext<ShopContext>(opt =>
{
    opt
        .UseSqlite("DataSource=Shop.db")
        .UseLazyLoadingProxies();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
