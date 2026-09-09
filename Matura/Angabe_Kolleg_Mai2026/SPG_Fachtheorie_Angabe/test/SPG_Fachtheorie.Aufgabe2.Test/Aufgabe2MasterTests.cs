// *************************************************************************************************
// VORDEFINIERTE INTEGRATION TESTS ZUR AUTOMATISIERTEN KONTROLLE
// Hier ist nichts auszufüllen oder zu bearbeiten!
// Sie können die Tests zur Kontrolle ausführen, aber hier wird nichts implementiert.
// Bei der Kontrolle der Abgaben wird diese Datei durch die Originalversion ersetzt.
// *************************************************************************************************
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text.Json;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe2.Test;

public class Aufgabe2MasterTests : IDisposable
{
    private readonly ReservationContext _db;
    private readonly ReservationService _service;

    public Aufgabe2MasterTests()
    {
        _db = GetSeededDbContext();
        _service = new ReservationService(_db);
    }

    public void Dispose()
    {
        _db.Dispose();
    }

    [Fact(DisplayName = "Setup: Database Creation")]
    public void T00_CreateDatabase()
    {
        using var db = GetSeededDbContext();
        Assert.True(db.Database.CanConnect());
    }

    [Fact(DisplayName = "Kriterium (3 Pkt): Die Methode getCategoriesWithCount ist korrekt.")]
    public void T01_GetCategoriesWithCountTest()
    {
        var sql = $@"select c.""text"", (select count(*) from inventoryItem i where i.categoryId = c.id) as Count from category c";
        CheckServiceMethodResult(_db, sql, _service.GetCategoriesWithCount, $"GetCategoriesWithCount");
    }

    [Fact(DisplayName = "Kriterium (4 Pkt): Die Methode getPersonsWithOpenReservations ist korrekt.")]
    public void T02_GetPersonsWithOpenReservationsTest()
    {
        var sql = $@"select * from person p where exists(select * from reservation r where r.personId = p.id and r.returnDate is null)";
        CheckServiceMethodResult(_db.Persons, sql, _service.GetPersonsWithOpenReservations, $"GetPersonsWithOpenReservations");
    }

    [Fact(DisplayName = "Kriterium (4 Pkt): Die Methode getInventoryItemsWithoutReservations ist korrekt.")]
    public void T03_GetInventoryItemsWithoutReservationsTest()
    {
        foreach (var year in new int[] { 2025, 2026 })
        {
            var sql = $@"select * from inventoryItem i where not exists(select * from reservation r where r.inventoryItemId = i.id and strftime('%Y', r.loanDate) = '{year}');";
            CheckServiceMethodResult(_db.InventoryItems, sql, () => _service.GetInventoryItemsWithoutReservations(year), $"GetInventoryItemsWithoutReservations({year})");
        }
    }

    [Fact(DisplayName = "Kriterium (1 Pkt): Die Methode addReservation prüft personId korrekt.")]
    public void T04_AddReservationThrowsExceptionWhenPersonIsInvalidTest()
    {
        var ex = Assert.Throws<ReservationException>(() => _service.AddReservation(999, 2, new DateOnly(2026, 1, 5)));
        Assert.Contains("person", ex.Message.ToLower());
    }

    [Fact(DisplayName = "Kriterium (1 Pkt): Die Methode addReservation prüft inventoryItemId korrekt.")]
    public void T05_AddReservationThrowsExceptionWhenItemIsInvalidTest()
    {
        var ex = Assert.Throws<ReservationException>(() => _service.AddReservation(1, 999, new DateOnly(2026, 1, 5)));
        Assert.Contains("item", ex.Message.ToLower());
    }

    [Fact(DisplayName = "Kriterium (1 Pkt): Die Methode addReservation prüft isAvailable korrekt.")]
    public void T06_AddReservationThrowsExceptionWhenItemIsNotAvailableTest()
    {
        var ex = Assert.Throws<ReservationException>(() => _service.AddReservation(1, 1, new DateOnly(2026, 1, 5)));
        Assert.Contains("available", ex.Message.ToLower());
    }

    [Fact(DisplayName = "Kriterium (4 Pkt): Die Methode addReservation erstellt eine korrekte Reservierung in der Datenbank.")]
    public void T07_AddReservationSuccessTest()
    {
        var reservation = _service.AddReservation(1, 2, new DateOnly(2026, 1, 5));
        Assert.True(reservation.Id != 0);
        _db.ChangeTracker.Clear();
        var reservationFromDb = _db.Reservations.Include(r => r.Status).First(r => r.Id == reservation.Id);
        Assert.True(reservation.Status.Text == "Pending");
    }

    private void CheckServiceMethodResult<T>(DbSet<T> set, string sql, Func<List<T>> serviceMethod, string methodName) where T : class =>
        CheckServiceMethodResult(set.FromSqlRaw(sql).ToList(), sql, serviceMethod, methodName);

    private void CheckServiceMethodResult<T>(ReservationContext db, string sql, Func<List<T>> serviceMethod, string methodName) where T : class =>
        CheckServiceMethodResult(db.Database.SqlQueryRaw<T>(sql).ToList(), sql, serviceMethod, methodName);

    private void CheckServiceMethodResult<T>(List<T> rows, string sql, Func<List<T>> serviceMethod, string methodName) where T : class
    {
        var serviceResult = serviceMethod();
        Assert.True(serviceResult.Count == rows.Count, $"{methodName} row count failed: expected {rows.Count}, got {serviceResult.Count}.");
        foreach (var row in rows)
            Assert.True(serviceResult.Any(m => JsonSerializer.Serialize(m) == JsonSerializer.Serialize(row)),
                $"Test failed. Row not found in your result: {JsonSerializer.Serialize(row)}.");
    }

    private ReservationContext GetSeededDbContext()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        var options = new DbContextOptionsBuilder()
            .UseSqlite(connection)
            .Options;

        var db = new ReservationContext(options);
        db.Database.EnsureCreated();
        db.Seed();
        return db;
    }

    private List<T> QueryDatabase<T>(DatabaseFacade database, string commandText, Func<DbDataReader, T> projection)
    {
        using var command = database.GetDbConnection().CreateCommand();
        command.CommandText = commandText;
        database.OpenConnection();
        using var reader = command.ExecuteReader();
        var rows = new List<T>();
        while (reader.Read())
            rows.Add(projection(reader));
        return rows;
    }
}