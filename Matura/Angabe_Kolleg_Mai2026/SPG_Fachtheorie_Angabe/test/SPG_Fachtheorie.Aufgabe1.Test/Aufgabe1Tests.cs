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

namespace SPG_Fachtheorie.Aufgabe1.Test;

public class Aufgabe1Tests
{
    /// <summary>
    /// Vorgegebener Test. Prüft, ob die Datenbank überhaupt aus dem Model mit EF Core erzeugt werden kann.
    /// </summary>
    [Fact]
    public void CreateDatabaseTest()
    {
        using var db = GetEmptyDbContext();
        // CREATE TABLE Skript mit dem Debugger angesehen werden (wenn nötig).
        var sqlScript = db.Database.GenerateCreateScript();
        Assert.True(db.Database.CanConnect());
    }

    [Fact]
    public void T01_AddPaymentLogTest()
    {
        // TODO: Add yout implementation
        throw new NotImplementedException();
    }

    [Fact]
    public void T02_PaymentStrategyTypeHasCorrentValue()
    {
        // TODO: Add yout implementation
        throw new NotImplementedException();
    }

    [Fact]
    public void T03_BankAccountIbanIsUniqueTest()
    {
        // TODO: Add yout implementation
        throw new NotImplementedException();
    }
    private AboManagerContext GetEmptyDbContext()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        var options = new DbContextOptionsBuilder()
            .UseSqlite(connection)
            .Options;

        var db = new AboManagerContext(options);
        db.Database.EnsureCreated();
        return db;
    }
}