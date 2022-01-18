using Xunit;
using Microsoft.EntityFrameworkCore;

namespace SPG_Fachtheorie.Aufgabe1.Test;

/// <summary>
/// Unittests für den DBContext.
/// Die Datenbank wird im Ordner SPG_Fachtheorie\SPG_Fachtheorie.Aufgabe1.Test\bin\Debug\net6.0\Invoice.db
/// erzeugt und kann mit SQLite Management Studio oder DBeaver betrachtet werden
/// </summary>
public class InvoiceContextTests
{
    /// <summary>
    /// Prüft, ob die Datenbank mit dem Model im InvoiceContext angelegt werden kann.
    /// </summary>
    [Fact]
    public void CreateDatabaseTest()
    {
        var options = new DbContextOptionsBuilder()
            .UseSqlite("Data Source=Invoice.db")
            .Options;

        using var db = new InvoiceContext(options);
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }
}