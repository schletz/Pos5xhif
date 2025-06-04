// *************************************************************************************************
// UNITTESTS FÜR AUFGABE 1
// *************************************************************************************************
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Linq;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe1.Test
{
    public class Aufgabe1Test
    {
        /// <summary>
        /// Prüft, ob alle Tabellen in der Datenbank erstellt wurden.
        /// WICHTIG: Der Name der Tabelle wird vom Namen des DbSets im Context bestimmt, er muss wie die unteren Tabellen heißen!
        /// </summary>
        [Fact]
        public void CreateDatabaseTest()
        {
            var tables = new string[] { "Companies", "Users", "Invoices", "InvoiceItems", "Articles" };
            using var db = GetEmptyDbContext();

            foreach (var table in tables)
            {
                using var command = db.Database.GetDbConnection().CreateCommand();
                command.CommandText = $"SELECT 1 FROM sqlite_master WHERE type='table' AND name='{table}';";
                db.Database.OpenConnection();
                var result = (long?)command.ExecuteScalar();
                Assert.True(result == 1, $"Table {table} not found. Check the name of your DbSets.");
            }
        }

        /// <summary>
        /// Prüft, ob das die Enumeration in Customer korrekt gespeichert wird.
        /// </summary>
        [Fact]
        public void PersistEnumSuccessTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Prüft, ob das Property Address in Company als value object korrekt gespeichert werden kann.
        /// </summary>
        [Fact]
        public void PersistValueObjectInCompanySuccessTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Prüft, ob ein Eintrag zur Liste von Adressen in Customer hinzugefügt und wieder
        /// gelesen werden kann.
        /// </summary>
        [Fact]
        public void PersistValueObjectInCustomerSuccessTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Das Entity InvoiceItem soll in der Datenbank gespeichert werden.
        /// Es referenziert auf alle anderen Entities, deswegen reicht dieser eine Test,
        /// um die korrekte Speicherung aller Entities zu prüfen.
        /// </summary>
        [Fact]
        public void PersistInvoiceItemSuccessTest()
        {
            throw new NotImplementedException();
        }

        private InvoiceContext GetEmptyDbContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder()
                .UseSqlite(connection)
                .Options;

            var db = new InvoiceContext(options);
            db.Database.EnsureCreated();
            return db;
        }
    }
}