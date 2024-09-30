using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Model;

namespace SPG_Fachtheorie.Aufgabe1.Infrastructure
{
    public class DamageContext : DbContext
    {
        // TODO: Add your DbSets

        public DamageContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: Add additional configuration
        }
    }
}