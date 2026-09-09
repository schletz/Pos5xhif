// *************************************************************************************************
// VORDEFINIERTE TESTS ZUR AUTOMATISIERTEN KONTROLLE
// *************************************************************************************************
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe1.Test;

public class Aufgabe1MasterTests
{
    [Fact(DisplayName = "Kriterium 00: Initialer Schema-Test: Erstellung der DB")]
    public void T00_CanCreateDatabaseTest()
    {
        using var db = GetEmptyDbContext();
        string createScript = db.Database.GenerateCreateScript();
        Debug.Write(createScript);

        using var command = db.Database.GetDbConnection().CreateCommand();
        command.CommandText = $"SELECT COUNT(*) FROM sqlite_master WHERE type='table';";
        db.Database.OpenConnection();
        var result = (long?)command.ExecuteScalar();
        Assert.True(result >= 3, $"Less than 3 Tables found. Check your DbSets.");
    }

    [Fact(DisplayName = "Kriterium 00: INSERT Test")]
    public void T00_GenerateSchemaTest() => InsertRow(
        "INSERT INTO BankAccount (BankName, AccountHolder, Iban) VALUES ('Bank Austria', 'Max Muster', 'AT611904300234573201')",
        "INSERT INTO Subscription (ProviderName, Description) VALUES ('ChatGPT',NULL)",
        "INSERT INTO Subscription (ProviderName, Description) VALUES ('Google AI', NULL)",
        "INSERT INTO PaymentStrategy (Amount_Value, Amount_Currency, SubscriptionId, BankAccountId, Type, Interval, NextBillingDate) VALUES (30.1, 'EUR', 1, 1, 'PeriodicPayment', 'Monthly', '2026-02-14')",
        "INSERT INTO PaymentStrategy (Amount_Value, Amount_Currency, SubscriptionId, BankAccountId, Type, TransactionDate) VALUES (30.1, 'EUR', 2, 1, 'PeriodicPayment', '2026-01-31')");
    

    // --- Kriterium 01 bis 06: Entity Abbildung ---

    [Fact(DisplayName = "Kriterium 01: Das Entity PaymentStrategy wird korrekt in der erzeugten Datenbank abgebildet.")]
    public void Kriterium01_EntityPaymentStrategyTest()
    {
        using var db = GetEmptyDbContext();
        var entity = db.Model.GetEntityByClassname("PaymentStrategy");
        Assert.Equal("PaymentStrategy", entity.GetTableName());
    }

    [Fact(DisplayName = "Kriterium 02: Das Entity PeriodicPayment wird korrekt in der erzeugten Datenbank abgebildet.")]
    public void Kriterium02_EntityPeriodicPaymentTest()
    {
        using var db = GetEmptyDbContext();
        var entity = db.Model.GetEntityByClassname("PeriodicPayment");
        Assert.NotNull(entity);
        Assert.Equal("PaymentStrategy", entity.GetTableName()); // Table-Per-Hierarchy
    }

    [Fact(DisplayName = "Kriterium 03: Das Entity OneTimePayment wird korrekt in der erzeugten Datenbank abgebildet.")]
    public void Kriterium03_EntityOneTimePaymentTest()
    {
        using var db = GetEmptyDbContext();
        var entity = db.Model.GetEntityByClassname("OneTimePayment");
        Assert.NotNull(entity);
        Assert.Equal("PaymentStrategy", entity.GetTableName()); // Table-Per-Hierarchy
    }

    [Fact(DisplayName = "Kriterium 04: Das Entity PaymentLog wird korrekt in der erzeugten Datenbank abgebildet.")]
    public void Kriterium04_EntityPaymentLogTest()
    {
        using var db = GetEmptyDbContext();
        var entity = db.Model.GetEntityByClassname("PaymentLog");
        Assert.Equal("PaymentLog", entity.GetTableName());
    }

    [Fact(DisplayName = "Kriterium 05: Das Entity BankAccount wird korrekt in der erzeugten Datenbank abgebildet.")]
    public void Kriterium05_EntityBankAccountTest()
    {
        using var db = GetEmptyDbContext();
        var entity = db.Model.GetEntityByClassname("BankAccount");
        Assert.Equal("BankAccount", entity.GetTableName());
    }

    [Fact(DisplayName = "Kriterium 06: Das Entity Subscription wird korrekt in der erzeugten Datenbank abgebildet.")]
    public void Kriterium06_EntitySubscriptionTest()
    {
        using var db = GetEmptyDbContext();
        var entity = db.Model.GetEntityByClassname("Subscription");
        Assert.Equal("Subscription", entity.GetTableName());
    }

    // --- Kriterium 07: Beziehungen ---

    [Fact(DisplayName = "Kriterium 07: Die 1:0..1 Beziehung zwischen Subscription und PaymentStrategy wird korrekt in der Datenbank abgebildet.")]
    public void Kriterium07_SubscriptionAndPaymentStrategyIsOneToOneTest()
    {
        using var db = GetEmptyDbContext();
        var principalEntity = db.Model.GetEntityByClassname("Subscription");
        var dependentEntity = db.Model.GetEntityByClassname("PaymentStrategy");
        var fk = dependentEntity.FindDeclaredNavigation("Subscription")?.ForeignKey;
        Assert.NotNull(fk);
        Assert.True(fk.IsUnique, "Der Foreign Key ist nicht als Unique markiert, folglich ist es keine 1:1 / 1:0..1 Beziehung.");
        Assert.True(fk.DeclaringEntityType == dependentEntity);
        Assert.True(fk.PrincipalEntityType == principalEntity);
    }

    // --- Kriterium 08 bis 09: Discriminator ---

    [Fact(DisplayName = "Kriterium 08: Das Feld PaymentStrategy.type wird als Discriminator verwendet.")]
    public void Kriterium08_PaymentStrategyDiscriminatorTest()
    {
        using var db = GetEmptyDbContext();
        Assert.Equal("Type", db.Model.GetEntityByClassname("PaymentStrategy").GetDiscriminatorPropertyName());
    }

    [Fact(DisplayName = "Kriterium 09: Der Discriminator hat die korrekten Werte (P und O).")]
    public void Kriterium09_PaymentStrategyDiscriminatorValuesTest()
    {
        using var db = GetEmptyDbContext();
        Assert.Equal("P", db.Model.GetEntityByClassname("PeriodicPayment").GetDiscriminatorValue());
        Assert.Equal("O", db.Model.GetEntityByClassname("OneTimePayment").GetDiscriminatorValue());
    }

    // --- Kriterium 10 bis 11: Value Objects ---

    [Fact(DisplayName = "Kriterium 10: PaymentStrategy.Amount wird als value object konfiguriert.")]
    public void Kriterium10_PaymentStrategyAmountIsValueObjectTest()
    {
        using var db = GetEmptyDbContext();
        var entity = db.Model.GetEntityByClassname("PaymentStrategy");
        var amountNav = entity.FindNavigation("Amount");
        Assert.NotNull(amountNav);
        Assert.True(amountNav.TargetEntityType.IsOwned());
    }

    [Fact(DisplayName = "Kriterium 11: PaymentLog.Amount wird als value object konfiguriert.")]
    public void Kriterium11_PaymentLogAmountIsValueObjectTest()
    {
        using var db = GetEmptyDbContext();
        var entity = db.Model.GetEntityByClassname("PaymentLog");
        var amountNav = entity.FindNavigation("Amount");
        Assert.NotNull(amountNav);
        Assert.True(amountNav.TargetEntityType.IsOwned());
    }

    // --- Kriterium 12 bis 14: Konvertierungen, Rich Types und Indizes ---

    [Fact(DisplayName = "Kriterium 12: Die Enum in PeriodicPayment.Interval wird als String gespeichert.")]
    public void Kriterium12_PeriodicPaymentIntervalIsStringTest()
    {
        EnsureConstraint("PaymentStrategy", "Interval", "TEXT");
    }

    [Fact(DisplayName = "Kriterium 13: BankAccount.Iban wird als rich type definiert und als String gespeichert.")]
    public void Kriterium13_BankAccountIbanIsRichTypeTest()
    {
        EnsureConstraint("BankAccount", "Iban", "TEXT");
        using var db = GetEmptyDbContext();
        Assert.Equal("AccountNumber", typeof(BankAccount).GetProperty("Iban")?.PropertyType.Name);
        var converter = db.Model.GetEntityByClassname("BankAccount").GetProperty("Iban").GetValueConverter();
        Assert.NotNull(converter);
    }

    [Fact(DisplayName = "Kriterium 14: BankAccount.Iban wird mit einem unique index definiert.")]
    public void Kriterium14_BankAccountIbanIsUniqueIndexTest()
    {
        using var db = GetEmptyDbContext();
        var index = db.Model.GetEntityByClassname("BankAccount").GetIndexes()
            .FirstOrDefault(i => i.Properties.Any(p => p.Name == "Iban"));
        
        Assert.NotNull(index);
        Assert.True(index.IsUnique);
    }

 
    // --- Hilfsmethoden aus der Vorlage ---

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

    private void InsertRowShouldFail(bool foreignKeyCheck, params string[] commandsTexts) => InsertRow(true, foreignKeyCheck, commandsTexts);
    private void InsertRowShouldFail(params string[] commandsTexts) => InsertRow(true, true, commandsTexts);
    private void InsertRow(params string[] commandsTexts) => InsertRow(false, true, commandsTexts);
    private void InsertRow(bool foreignKeyCheck, params string[] commandsTexts) => InsertRow(false, foreignKeyCheck, commandsTexts);
    
    private void InsertRow(bool shouldFail, bool foreignKeyCheck, params string[] commandsTexts)
    {
        using var db = GetEmptyDbContext();
        bool failed = false;
        using (var command = db.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = $"PRAGMA foreign_keys = {(foreignKeyCheck ? 1 : 0)}";
            db.Database.OpenConnection();
            command.ExecuteNonQuery();
        }

        foreach (var commandText in commandsTexts)
        {
            using var command = db.Database.GetDbConnection().CreateCommand();
            command.CommandText = commandText;
            db.Database.OpenConnection();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqliteException e)
            {
                failed = true;
                if (!shouldFail)
                    Assert.Fail($"Query failed: {commandText} with error {e.InnerException?.Message ?? e.Message}");
            }
        }
        if (shouldFail && !failed)
            Assert.Fail($"Query should fail, but it didn't. {string.Join(Environment.NewLine, commandsTexts)}");
    }

    private void EnsureConstraint(string table, string column, string type, bool isPk = false)
    {
        using var db = GetEmptyDbContext();
        using var cmd = db.Database.GetDbConnection().CreateCommand();
        cmd.CommandText = $"PRAGMA table_info({table})";
        db.Database.OpenConnection();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string columnName = reader.GetString(1);
            string columnType = reader.GetString(2);
            bool columnPk = reader.GetBoolean(5);
            if (columnName.Equals(column, StringComparison.OrdinalIgnoreCase))
            {
                Assert.True(columnType == type, $"Wrong datatype for {table}.{column}. Expected: {type}, given: {columnType}.");
                Assert.True(columnPk == isPk, $"Wrong primary key constraint {table}.{column}. Expected: {isPk}, given: {columnPk}.");
                return;
            }
        }
        Assert.Fail($"Column {table}.{column} not found.");
    }
}

public static class IModelExtensions
{
    public static IEntityType GetEntityByClassname(this IModel model, string name) =>
        model.GetEntityTypes().FirstOrDefault(t => t.ClrType.Name == name) ?? throw new ArgumentException($"Entity {name} not found in Model.");
}