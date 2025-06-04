using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;

// Important: Make class public!
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<ScooterContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

        var app = builder.Build();
        if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Testing"))
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            using (var scope = app.Services.CreateScope())
            using (var service = scope.ServiceProvider.GetRequiredService<ScooterContext>())
            {
                service.Database.EnsureDeleted();
                service.Database.EnsureCreated();
                if (app.Environment.IsDevelopment()) service.Seed();
            }
        }

        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
    }
}
