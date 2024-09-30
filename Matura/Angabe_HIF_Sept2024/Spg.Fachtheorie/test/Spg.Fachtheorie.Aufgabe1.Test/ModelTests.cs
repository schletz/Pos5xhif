using Spg.Fachtheorie.Aufgabe1.Infrastructure;
using Spg.Fachtheorie.Aufgabe1.Test.Helpers;

namespace Spg.Fachtheorie.Aufgabe1.Test
{
    public class ModelTests
    {
        [Fact]
        public void Should_CreateDatabaseAndSeedWithTestData()
        {
            // TODO: Refactore by implement "using(...)"
            // Arrange
            Aufgabe1Database db = DatabaseUtilities.GetDbInMemory();

            // Act
            DatabaseUtilities.SeedDatabase(db);

            // Assert
            //Assert.Single(db.Products);
        }

        // TODO: More Unit Tests
        // ...
    }
}
