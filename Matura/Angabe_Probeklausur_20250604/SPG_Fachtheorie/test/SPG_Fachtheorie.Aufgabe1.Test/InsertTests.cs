// *************************************************************************************************
// DIESE KLASSE IST NUR FÜR DIE KORREKTUR UND ZUR SELBSTKONTROLLE. HIER MUSS NICHTS GEÄNDERT WERDEN.
// Schreibe deine Unittests in die Testklasse Aufgabe1Test.cs!
// *************************************************************************************************

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe1.Test
{
    /// <summary>
    /// Insert Tests für die erzeugte Datenbank.
    /// Diese Tests versuchen direkt mit einer INSERT Anweisung, Daten in die erstellten Tabellen einzufügen.
    /// Achte auf die Namen der Felder und DbSets.
    /// PK werden mit Id angenommen, FK mit Tablename + Id.
    /// DIESE KLASSE IST NUR FÜR DIE KORREKTUR UND ZUR SELBSTKONTROLLE. HIER MUSS NICHTS GEÄNDERT WERDEN.
    /// Schreibe deine Unittests in die Testklasse Aufgabe1Test.cs!
    /// </summary>
    public class InsertTests
    {
        [Fact]
        public void InsertArticleTest() => InsertRow(
            "INSERT INTO Articles (Number, Name, Price) VALUES (1, 'A', 99.99)");

        // Prüft, ob das value object in Company korrekt definiert wurde.
        // Nur dann werden die Spalten Address_Street, Address_Zip, Address_City direkt in der Tabelle angelegt.
        [Fact]
        public void InsertCompanyTest() => InsertRow(
            "INSERT INTO Companies (Name, Address_Street, Address_Zip, Address_City) VALUES ('A', 'B', 'C', 'D')");

        // Prüft, ob die Vererbung korrekt angelegt wurde.
        // Nur dann sind die Felder Discriminator und die Felder von Employee in der Tabelle Users vorhanden.
        [Fact]
        public void InsertEmployeeTest() => InsertRow(
            "INSERT INTO Companies (Name, Address_Street, Address_Zip, Address_City) VALUES ('A', 'B', 'C', 'D')",
            "INSERT INTO Users (FirstName, LastName, Email, CompanyId, Discriminator) VALUES ('A', 'B', 'C', 1, 'Employee')");

        // Prüft, ob die Vererbung korrekt angelegt wurde.
        // Nur dann sind die Felder Discriminator und die Felder von Customer in der Tabelle Users vorhanden.
        [Fact]
        public void InsertCustomerTest() => InsertRow(
            "INSERT INTO Companies (Name, Address_Street, Address_Zip, Address_City) VALUES ('A', 'B', 'C', 'D')",
            "INSERT INTO Users (FirstName, LastName, Email, Type, Note, Discriminator) VALUES ('A', 'B', 'C', 'B2C', null, 'Customer')");

        // Prüft, ob die Liste von value objects angelegt wurde.
        // Nur dann wird EF Core eine Tabelle Users_Addresses erzeugen, die die Adressen des Customers speichern kann.
        [Fact]
        public void InsertCustomerWithAddressesTest() => InsertRow(
            "INSERT INTO Companies (Name, Address_Street, Address_Zip, Address_City) VALUES ('A', 'B', 'C', 'D')",
            "INSERT INTO Users (FirstName, LastName, Email, Type, Note, Discriminator) VALUES ('A', 'B', 'C', 'B2C', null, 'Customer')",
            "INSERT INTO Users_Addresses (CustomerId, Street, Zip, City) VALUES (1, 'A', 'B', 'C')");

        [Fact]
        public void InsertInvoiceTest() => InsertRow(
            "INSERT INTO Companies (Name, Address_Street, Address_Zip, Address_City) VALUES ('A', 'B', 'C', 'D')",
            "INSERT INTO Users (FirstName, LastName, Email, Type, Note, Discriminator) VALUES ('A', 'B', 'C', 'B2C', null, 'Customer')",
            "INSERT INTO Users (FirstName, LastName, Email, CompanyId, Discriminator) VALUES ('A', 'B', 'C', 1, 'Employee')",
            "INSERT INTO Invoices (Number, Date, CustomerId, EmployeeId) VALUES (1, '2025-05-10T13:00:00', 1, 2)");

        [Fact]
        public void InsertInvoiceItemTest() => InsertRow(
            "INSERT INTO Articles (Number, Name, Price) VALUES (1, 'A', 99.99)",
            "INSERT INTO Companies (Name, Address_Street, Address_Zip, Address_City) VALUES ('A', 'B', 'C', 'D')",
            "INSERT INTO Users (FirstName, LastName, Email, Type, Note, Discriminator) VALUES ('A', 'B', 'C', 'B2C', null, 'Customer')",
            "INSERT INTO Users (FirstName, LastName, Email, CompanyId, Discriminator) VALUES ('A', 'B', 'C', 1, 'Employee')",
            "INSERT INTO Invoices (Number, Date, CustomerId, EmployeeId) VALUES (1, '2025-05-10T13:00:00', 1, 2)",
            "INSERT INTO InvoiceItems (InvoiceId, ArticleNumber, Quantity, Price) VALUES (1, 1, 2, 99.99)");

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

        private void InsertRow(params string[] commandsTexts)
        {
            using var db = GetEmptyDbContext();
            foreach (var commandText in commandsTexts)
            {
                using var command = db.Database.GetDbConnection().CreateCommand();
                command.CommandText = commandText;
                db.Database.OpenConnection();
                try
                {
                    int rows = command.ExecuteNonQuery();
                    Assert.True(rows == 1, $"Query failed: {commandText}");
                }
                catch (SqliteException e)
                {
                    Assert.Fail($"Query failed: {commandText} with error {e.InnerException?.Message ?? e.Message}");
                }
            }
        }
    }
}
