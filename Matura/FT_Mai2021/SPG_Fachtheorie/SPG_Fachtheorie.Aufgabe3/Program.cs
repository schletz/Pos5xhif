using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe3Mvc;
using System;
using System.Diagnostics;

namespace SPG_Fachtheorie.Aufgabe3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Process currentProcess = Process.GetCurrentProcess();
            Console.WriteLine($"********************************************************************************");
            Console.WriteLine($"* PROCESS ID: {currentProcess.Id}");
            Console.WriteLine($"********************************************************************************");
            using (var db = new CovidTestContext(new DbContextOptionsBuilder()
                .UseSqlite("Data Source=CovidTest.db")
                .Options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Import("data.sql");
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
