using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2;
using SPG_Fachtheorie.Aufgabe3RazorPages.Services;

var options = new DbContextOptionsBuilder()
    .UseSqlite("Data Source=Appointment.db")
    .Options;

using (var db = new AppointmentContext(options))
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    db.Seed();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppointmentContext>(opt=>opt.UseSqlite("Data Source=Appointment.db"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<AuthService>();
builder.Services.AddAuthentication(
    Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = "/";
        o.AccessDeniedPath = "/NotAuthorized";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
