using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoapServer.Model
{
    /// <summary>
    /// Modelklasse Calculation. Erstellt die Tabelle Calculation mit den angegebenen Spalten.
    /// Id wird automatisch aufgrund des Namens als PK verwendet und Autoincrement eingetragen,
    /// da es ein int Property ist. Das ist das Standardverhalten von EF Core.
    /// </summary>
    public class Calculation
    {
        public int Id { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public int Result { get; set; }
        public string Operation { get; set; }
    }

    /// <summary>
    /// DB Context für den Zugriff auf die erzeugte Datenbank.
    /// </summary>
    public class SoapDatabase : DbContext
    {
        public DbSet<Calculation> Calculations { get; set; }

        /// <summary>
        /// Erzeugt eine SQLite Datenbank mit dem Namen Soap.db. Dafür muss das NuGet Paket
        /// Microsoft.EntityFrameworkCore.Sqlite installiert sein.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=Soap.db");
        }

        /// <summary>
        /// Detaileinstellungen, wie die Tabelle generiert werden soll.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calculation>().HasKey(c => c.Id);
            modelBuilder.Entity<Calculation>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Calculation>().Property(c => c.Operation).HasMaxLength(1);
        }
    }
}
