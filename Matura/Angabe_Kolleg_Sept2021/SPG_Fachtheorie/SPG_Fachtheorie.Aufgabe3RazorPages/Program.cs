using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace SPG_Fachtheorie.Aufgabe3RazorPages
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Process currentProcess = Process.GetCurrentProcess();
            Console.WriteLine($"********************************************************************************");
            Console.WriteLine($"* PROCESS ID: {currentProcess.Id}");
            Console.WriteLine($"********************************************************************************");
            using (var db = new Aufgabe2.Infrastructure.StoreContext(new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Store.db")
                .Options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Seed();
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
