using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Model.Custom;
using Spg.ProbeFachtheorie.Aufgabe2.Infrastructure;
using Spg.ProbeFachtheorie.Aufgabe2.Services;
using Spg.ProbeFachtheorie.Aufgabe3.Infrastructure;
using Spg.ProbeFachtheorie.Aufgabe3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spg.ProbeFachtheorie.Aufgabe3
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
            services.ConfigureSql(Configuration["AppSettings:Database"]);

            /// *** Authentication, Authorization hinzufügen
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.LoginPath = "/";
                    o.AccessDeniedPath = "/NotAuthorized";
                });
            services.AddAuthorization(o =>
            {
                o.AddPolicy("PolicyForGuest", p => p.RequireRole(UserRoles.Guest.ToString(), UserRoles.BackOfficeEmployee.ToString()));
                o.AddPolicy("PolicyForBackOfficeEmployee", p => p.RequireRole(UserRoles.BackOfficeEmployee.ToString()));
            });
            services.AddRazorPages(options =>
            {
               // options.Conventions.AuthorizeFolder("/", "PolicyForGuest");
            });


            services.AddTransient<HttpCookieAuthService>();
            services.AddTransient<DbAuthProvider>();

            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            /// *** Authentication, Authorization aktivieren
            app.UseAuthentication();
            app.UseAuthorization();
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Lax,
            };
            app.UseCookiePolicy(cookiePolicyOptions);
            /// ***

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
