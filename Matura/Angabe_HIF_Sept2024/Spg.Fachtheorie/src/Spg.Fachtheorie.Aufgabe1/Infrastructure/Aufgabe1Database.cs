using Microsoft.EntityFrameworkCore;

namespace Spg.Fachtheorie.Aufgabe1.Infrastructure
{
    public class Aufgabe1Database : DbContext
    {
        // TODO: DB-Set-Properties
        // ...

        // TODO: Correct Constructor
        public Aufgabe1Database(object fake__PleaseCorrectThis__AndType) // :base(...)
        { }

        public Aufgabe1Database(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: Implementation Fluent API
            // ...
        }
    }
}