using Xunit;
using Microsoft.EntityFrameworkCore;
using System;

namespace SPG_Fachtheorie.Aufgabe1.Test;

/// <summary>
/// Unittests für den DBContext.
/// Die Datenbank wird im Ordner SPG_Fachtheorie\SPG_Fachtheorie.Aufgabe1.Test\bin\Debug\net6.0\Invoice.db
/// erzeugt und kann mit SQLite Management Studio oder DBeaver betrachtet werden
/// </summary>
[Collection("Sequential")]
public class ApplicationContextTests
{
    private ApplicationContext GetContext(bool deleteDb = true)
    {
        var options = new DbContextOptionsBuilder()
            .UseSqlite("Data Source=Application.db")
            .Options;

        var db = new ApplicationContext(options);
        if (deleteDb)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
        return db;
    }
    /// <summary>
    /// Prüft, ob die Datenbank mit dem Model im InvoiceContext angelegt werden kann.
    /// </summary>
    [Fact]
    public void CreateDatabaseTest()
    {
        using var db = GetContext();
    }

    [Fact]
    public void AddDepartmentSuccessTest()
    {
        using var db = GetContext();
        throw new NotImplementedException();
    }
    [Fact]
    public void AddTaskSuccessTest()
    {
        using var db = GetContext();
        throw new NotImplementedException();
    }
    [Fact]
    public void AddApplicantSuccessTest()
    {
        using var db = GetContext();
        throw new NotImplementedException();
    }
    [Fact]
    public void AddUploadSuccessTest()
    {
        using var db = GetContext();
        throw new NotImplementedException();
    }
    [Fact]
    public void AddApplicantWithApplicantStatusSuccessTest()
    {
        using var db = GetContext();
        throw new NotImplementedException();
    }
}
