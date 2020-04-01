// Install-Package SoapCore
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SoapCore;
using SoapServer.Model;
using SoapServer.Services;

namespace SoapServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Beim Starten des Servers prüfen wir, ob die Datenbank existiert. Falls nicht, wird sie
            // auf Basis der Modelklassen erstellt.
            // Aktuell wird eine SQLite Datenbank mit dem Namen Soap.db erstellt. Diese kann z. B.
            // mit DBeaver oder einem anderen SQL Editor angesehen werden.
            // Falls die Modelklassen geändert werden, kann über 
            // db.Database.EnsureDeleted();
            // die vorhandene DB gelöscht werden.
            using (SoapDatabase db = new SoapDatabase())
            {
                // db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }

            // Ein Singleton wird nur 1x instanziert und dann wird diese Instanz für jeden Request
            // benutzt. Wir können daher auch einen State speichern.
            services.AddSingleton<CalcService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Wir weisen hier die Routen zu, unter denen das Service erreichbar ist. Diese Route
            // wird dann im Client gebraucht.
            app.UseSoapEndpoint<CalcService>(path: "/CalcService.svc", binding: new System.ServiceModel.BasicHttpBinding());

        }
    }
}
