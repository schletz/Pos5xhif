using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Linq;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe1.Test
{
    [Collection("Sequential")]
    public class Aufgabe1Test
    {
        private DamageContext GetEmptyDbContext()
        {
            // Database created in C:\Scratch\Aufgabe1_Test\Debug\net8.0\damages.db
            var options = new DbContextOptionsBuilder()
                .UseSqlite(@"Data Source=damages.db")
                .Options;

            var db = new DamageContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        [Fact]
        public void CreateDatabaseTest()
        {
            using var db = GetEmptyDbContext();
            //Assert.True(db.Employees.Count() == 0);
        }

        [Fact]
        public void AddEmployeeSuccessTest()
        {
            // TODO: Remove Exception and add your code here
            throw new NotImplementedException();
        }

        [Fact]
        public void AddDamageWithReportSuccessTest()
        {
            // TODO: Remove Exception and add your code here
            throw new NotImplementedException();
        }

        [Fact]
        public void AddRepairationSuccessTest()
        {
            // TODO: Remove Exception and add your code here
            throw new NotImplementedException();
        }
    }
}