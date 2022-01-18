using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe1
{
    public class InvoiceContext : DbContext
    {
        public InvoiceContext(DbContextOptions opt) : base(opt) { }
        // TOTO: Füge die DbSet<T> Collections hinzu.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: Füge - wenn notwendig - noch Konfigurationen hinzu.
        }

    }
}
