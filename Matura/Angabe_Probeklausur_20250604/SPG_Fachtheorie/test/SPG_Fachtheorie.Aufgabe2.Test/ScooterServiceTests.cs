// *************************************************************************************************
// VORGEGEBENE UNITTESTS FÜR AUFGABE 2
// DIESE KLASSE IST NUR FÜR DIE KORREKTUR UND ZUR SELBSTKONTROLLE. HIER MUSS NICHTS GEÄNDERT WERDEN.
// *************************************************************************************************
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe2.Test
{
    public class ScooterServiceTests
    {
        [Fact]
        public void CreateDatabaseSuccessTest()
        {
            using var db = GetEmptyDbContext();
            db.Seed();
            Assert.True(db.Scooters.Count() > 0);
        }

        [Fact]
        public void AddTripTest()
        {
            using var db = GetEmptyDbContext();
            db.Seed();
            var service = new ScooterService(db);
            {
                var ex = Assert.Throws<ScooterServiceException>(() => service.AddTrip(-1, 1, new DateTime(2025, 1, 1)));
                Assert.True(ex.Message == "Invalid user.", @"Failed on Exception for ""Invalid user.""");
            }
            {
                var ex = Assert.Throws<ScooterServiceException>(() => service.AddTrip(1, -1, new DateTime(2025, 1, 1)));
                Assert.True(ex.Message == "Invalid scooter.", @"Failed on Exception for ""Invalid scooter.""");
            }
            {
                var ex = Assert.Throws<ScooterServiceException>(() => service.AddTrip(1, 1, new DateTime(2025, 1, 1)));
                Assert.True(ex.Message == "Scooter is not parked.", @"Failed on Exception for ""Scooter is not parked.""");
            }
            var trip = service.AddTrip(1, 3, new DateTime(2025, 5, 29, 12, 0, 0));
            Assert.True(db.Trips.First(t => t.Id == trip.Id).Id != 0);
        }

        [Fact]
        public void GetTripInfosTest()
        {
            using var db = GetEmptyDbContext();
            db.Seed();
            var service = new ScooterService(db);
            var sql = @"
                select t.Id, t.Begin, t.End, t.ScooterId, s.Model, t.UserId, u.Email, t.End is not null as IsParked
                from Trips t inner join Scooters s on (t.ScooterId = s.Id )
                inner join Users u on (t.UserId = u.Id)
                where t.Begin >= '2025-01-15' and t.Begin <= '2025-02-12'";
            var rows = QueryDatabase(db.Database, sql,
                reader => new
                {
                    Id = reader.GetInt32(0),
                    Begin = reader.GetDateTime(1),
                    End = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2),
                    ScooterId = reader.GetInt32(3),
                    Model = reader.GetString(4),
                    UserId = reader.GetInt32(5),
                    Email = reader.GetString(6),
                    IsParked = reader.GetBoolean(7)
                });

            var tripInfos = service.GetTripInfos(new DateTime(2025, 1, 15), new DateTime(2025, 2, 12));
            Assert.True(tripInfos.Count == rows.Count, $"GetTripInfos row count failed: expected {rows.Count}, got {tripInfos.Count}.");
            foreach (var row in rows)
                Assert.True(tripInfos.Any(t =>
                    t.Id == row.Id && t.UserId == row.UserId && t.ScooterId == row.ScooterId &&
                    t.Begin == row.Begin && t.End == row.End && t.IsParked == row.IsParked),
                    $"GetTripInfos failed at TripId {row.Id}: wrong or missing.");
        }

        [Fact]
        public void CalculateTripLengthTest()
        {
            using var db = GetEmptyDbContext();
            db.Seed();
            var service = new ScooterService(db);
            var sql = @"
                SELECT t.Id, MAX(tl.MileageInMeters) - MIN(tl.MileageInMeters) AS Length
                FROM Trips t
                INNER JOIN TripLogs tl ON t.Id = tl.TripId
                GROUP BY t.Id";
            var rows = QueryDatabase(db.Database, sql,
                reader => new { Id = reader.GetInt32(0), Distance = reader.GetInt32(1) });
            foreach (var row in rows)
            {
                decimal actual = service.CalculateTripLength(row.Id);
                Assert.True(row.Distance == actual, $"CalculateTripLength failed at TripId {row.Id}: expected {row.Distance}, got {actual}");
            }
        }

        [Fact]
        public void CalculatePriceTest()
        {
            using var db = GetEmptyDbContext();
            db.Seed();
            var service = new ScooterService(db);
            string sql = @"
                with tripinfo as (
	                select t.Id, u.FreeKilometers, s.PricePerKilometer,
		                (max(tl.MileageInMeters) - min(tl.MileageInmeters))/1000.0 as Distance
	                from Trips t inner join TripLogs tl on (t.Id = tl.TripId)
		                inner join Users u on (t.UserId  = u.Id)
		                inner join Scooters s on (t.ScooterId = s.Id)
	                group by t.Id, u.FreeKilometers, s.PricePerKilometer
                )
                select ti.Id, max(0, (ti.Distance - ti.FreeKilometers)) * ti.PricePerKilometer as TripCost
                from tripinfo ti";
            var rows = QueryDatabase(db.Database, sql,
                reader => new { Id = reader.GetInt32(0), TripCost = reader.GetDecimal(1) });

            foreach (var row in rows)
            {
                decimal actual = Math.Round(service.CalculatePrice(row.Id), 4);
                Assert.True(Math.Round(row.TripCost, 4) == actual, $"CalculatePriceTest failed at TripId {row.Id}: expected {row.TripCost}, got {actual}");
            }
        }

        /// <summary>
        /// Methode zur Erstellung einer in memory SQLite Datenbank.
        /// Don't touch.
        /// </summary>
        private ScooterContext GetEmptyDbContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder()
                .UseSqlite(connection)
                .Options;

            var db = new ScooterContext(options);
            db.Database.EnsureCreated();
            return db;
        }

        /// <summary>
        /// Methode zum Senden einer SQL Abfrage an die SQLite Datenbank.
        /// Don't touch.
        /// </summary>
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
}