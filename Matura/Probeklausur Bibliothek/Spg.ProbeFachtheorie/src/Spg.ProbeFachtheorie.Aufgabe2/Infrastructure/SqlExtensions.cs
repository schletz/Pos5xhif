using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.ProbeFachtheorie.Aufgabe2.Infrastructure
{
    public static class SqlExtensions
    {
        public static void ConfigureSql(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LibraryContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlite(connectionString);
            });
        }
    }
}
