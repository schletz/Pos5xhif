
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Spg_Hotelmanager.Application.Infrastructure;

namespace Spg_Hotelmanager.Rest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Process currentProcess = Process.GetCurrentProcess();
            Console.WriteLine($"PROCESS ID: {currentProcess.Id}");

            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Hotel.db")
                .Options;

            using (var db = new HotelContext(options))
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
