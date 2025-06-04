using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe1.Infrastructure
{
    public class InvoiceContext : DbContext
    {
        // TODO: Füge hier benötigte DbSets ein.


        public InvoiceContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: Füge hier benötigte Konfigurationen ein.
        }
    }
}