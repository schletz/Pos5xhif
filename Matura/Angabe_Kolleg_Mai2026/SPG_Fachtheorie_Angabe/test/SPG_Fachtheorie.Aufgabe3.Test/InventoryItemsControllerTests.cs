using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Model;
using SPG_Fachtheorie.Aufgabe3.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe3.Test;

/// <summary>
/// Testklasse. Verwende _factory, um die Methoden
/// _factory.InitializeDatabase, _factory.GetHttpContent<T>, etc. aufzurufen.
/// Achte immer darauf, _factory.InitializeDatabase aufzurufen, um die Datenbank neu zu erstellen.
/// </summary>
[Collection("Sequential")]
public class InventoryItemsControllerTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly TestWebApplicationFactory _factory;

    public InventoryItemsControllerTests(TestWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetInventoryItemsReturns200Test()
    {
        _factory.InitializeDatabase(db =>
        {
            var category = new Category("Category");
            var item1 = new InventoryItem("123456", "item", category, true);
            var item2 = new InventoryItem("547894", "item2", category, true);
            db.AddRange(category, item1, item2);
            db.SaveChanges();
        });
        var (statusCode, customers) = await _factory.GetHttpContent<List<InventoryItemWithReservationsDto>>("/inventoryItems?withReservations=true");
        Assert.True(statusCode == HttpStatusCode.OK);
        Assert.NotNull(customers);
        Assert.True(customers.Count == 2);
    }

    // TODO: Add your integration tests for PATCH
}
