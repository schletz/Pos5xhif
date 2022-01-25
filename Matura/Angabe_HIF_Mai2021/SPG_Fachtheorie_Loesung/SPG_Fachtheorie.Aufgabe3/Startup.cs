using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace SPG_Fachtheorie.Aufgabe3Mvc
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Includes support for Razor Pages and controllers.
            services.AddDbContext<CovidTestContext>(opt => opt.UseSqlite("Data Source=CovidTest.db"));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
