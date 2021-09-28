using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe3RazorPages.Services;

namespace SPG_Fachtheorie.Aufgabe3RazorPages
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<AuthService>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddAuthorization();
            services.AddHttpContextAccessor();
            // Includes support for Razor Pages and controllers.
            services.AddDbContext<StoreContext>(opt => opt.UseSqlite("Data Source=Store.db"));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
