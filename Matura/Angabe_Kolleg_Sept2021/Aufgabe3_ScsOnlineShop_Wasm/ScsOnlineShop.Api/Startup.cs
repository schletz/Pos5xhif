using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ScsOnlineShop.Application.Infrastructure;
using ScsOnlineShop.Application.Model;
using ScsOnlineShop.Dto;

namespace ScsOnlineShop.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ShopContext>(opt => opt
                .UseSqlite("DataSource=Shop.db")
                .UseLazyLoadingProxies());
            services
                .AddControllers()
                .AddOData(opt => opt
                    .AddRouteComponents("odata", GetEdmModel())
                    .Select().Filter().OrderBy().SetMaxTop(100).Expand().Count());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }

            // NUGET: Microsoft.AspNetCore.Components.WebAssembly.Server
            app.UseBlazorFrameworkFiles();
            // Damit Assets (Bilder, ...) in der WASM geladen werden können.
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }

        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<StoreDto>("Stores");
            builder.EntitySet<OfferDto>("Offers");
            builder.EntityType<StoreDto>().HasKey(s => s.Guid);
            builder.EntityType<OfferDto>().HasKey(o => o.Guid);
            return builder.GetEdmModel();
        }
    }
}