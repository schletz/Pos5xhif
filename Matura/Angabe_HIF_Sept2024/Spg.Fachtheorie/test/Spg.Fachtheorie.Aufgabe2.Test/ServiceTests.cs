using Spg.Fachtheorie.Aufgabe2.Services;
using Spg.Fachtheorie.Aufgabe2.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Fachtheorie.Aufgabe2.Test
{
    public class ServiceTests
    {
        [Fact()]
        public void Should_CreateDatabaseAndSeedWithTestData()
        {
            // TODO: Refactore by implement "using(...)"
            // Arrange
            Aufgabe2Database db = DatabaseUtilities.GetDbInMemory();

            // Act
            DatabaseUtilities.SeedDatabase(db);

            // Assert
            //Assert.Single(db.Products);
        }

        [Fact()]
        public void Should_DoSomethingSuccessfull_When____()
        {
            // Arrange
            Aufgabe2Database db = DatabaseUtilities.GetDbInMemory();
            DatabaseUtilities.SeedDatabase(db);

            // Act

            // Assert
        }

        [Fact()]
        public void Should_ThrowException_When____()
        {
            // Arrange
            Aufgabe2Database db = DatabaseUtilities.GetDbInMemory();
            DatabaseUtilities.SeedDatabase(db);

            // Act

            // Assert
        }

        // TODO: More Unit Tests
        // ...
    }
}
