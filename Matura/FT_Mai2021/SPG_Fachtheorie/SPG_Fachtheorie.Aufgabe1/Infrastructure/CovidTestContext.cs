using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe1.Infrastructure
{
    /// <summary>
    /// Datenbankkontext für die Aufgabe 1.
    /// Hinweise:
    ///     Registrieren Sie die verwendeten Tabellen und führen Sie nötige Konfigurationen durch.
    ///     Der Test GenerateDbFromSqlFileTest im Testprojekt muss erfolgreich durchlaufen.
    ///     Schreiben Sie die geforderten Abfragen direkt in den DbContext.
    ///     Verwenden Sie die bereitgestellte Klasse DbContextTests im Testprojekt, um die Implementierung zu testen.
    /// </summary>
    public class CovidTestContext : DbContext
    {

        public CovidTestContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        /// <summary>
        /// Aufgabe 1c - Abfrage einer Person auf Basis seiner Telefonnummer.
        /// Hinweis:
        ///     Geben Sie den ersten gefundenen Datensatz zurück, da theoretisch auch 2 Personen die gleiche Nummer haben können.
        ///     Eine Telefonnummer ist gleich, wenn alle Nummernteile gleich sind.
        ///     Wird keine Person gefunden, ist null zu liefern.
        ///     Tauschen Sie object durch geeignete Datentypen aus.
        /// </summary>
        public object GetPersonByPhoneNumber(object phoneNumber)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Aufgabe 1c - Abfrage von Personen auf Basis der Postleitzahl.
        /// Tauschen Sie object durch geeignete Datentypen aus.
        /// </summary>
        public IQueryable<object> GetPersonByZipCode(string zipCode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Abfrage von Personen auf Basis eines Ortsnamensteils.
        /// Tauschen Sie object durch geeignete Datentypen aus.
        /// </summary>
        public IQueryable<object> GetPersonCityContains(string city)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Eine Statistikabfrage, die die Anzahl pro TestKitType und pro TestResult für einen Zeitraum zurückgibt.
        /// Hinweis: Die übergebenen Zeitwerte sind inklusive zu vergleichen.
        /// Tauschen Sie object durch geeignete Datentypen aus.
        /// </summary>
        public int GetTestCount(object testKitType, object testResult, DateTime timestampFrom, DateTime timestampTo)
        {
            throw new NotImplementedException();
        }
    }

}
