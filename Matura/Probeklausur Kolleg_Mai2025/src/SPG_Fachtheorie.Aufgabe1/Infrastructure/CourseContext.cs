using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe1.Infrastructure
{

    public class CourseContext : DbContext
    {
        // TODO: Füge deine DbSets hinzu
        public CourseContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: Füge hier erweiterte Konfiguration des DbContextes hinzu.
        }

    }
}