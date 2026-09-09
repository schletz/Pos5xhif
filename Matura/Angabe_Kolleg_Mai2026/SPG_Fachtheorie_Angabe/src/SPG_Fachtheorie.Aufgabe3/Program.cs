using Microsoft.Data.Sqlite;
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
        // Configure Datebase with settings from appsettings.json (section ConnectionStrings)
        var databaseName = builder.Configuration.GetConnectionString("Default")
            ?? throw new Exception("Missing ConnectionString:Default in appsettings.json.");
        var connection = new SqliteConnection(databaseName);
        builder.Services.AddDbContextFactory<ReservationContext>(options =>
            options.UseSqlite(connection));

        var app = builder.Build();
        if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Testing"))
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            using (var scope = app.Services.CreateScope())
            using (var service = scope.ServiceProvider.GetRequiredService<ReservationContext>())
            {
                service.Database.EnsureDeleted();
                service.Database.OpenConnection();
                service.Database.EnsureCreated();
                if (app.Environment.IsDevelopment()) service.Seed();
            }
        }

        app.MapControllers();
        app.Run();
    }
}
