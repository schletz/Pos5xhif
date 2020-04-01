using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SoapServer
{
    /// <summary>
    /// ASP.NET CORE Server mit SOAP Extensions.
    /// TODO: In Services neue Serviceklassen schreiben. 
    ///       Dann ein Interface erzeugen und in die Library verschieben. Das Service muss das Interface
    ///       implementieren.
    ///       In Startup.cs wir das Service dann unter einer Route registriert.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://*:8080")
                .UseStartup<Startup>()
                .Build();
            host.Run();
        }
    }
}
