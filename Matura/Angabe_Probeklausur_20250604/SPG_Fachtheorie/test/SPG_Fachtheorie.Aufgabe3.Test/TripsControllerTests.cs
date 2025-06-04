using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Model;
using SPG_Fachtheorie.Aufgabe3.Commands;
using SPG_Fachtheorie.Aufgabe3.Dtos;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Spg.Fachtheorie.Aufgabe3.API.Test
{
    /// <summary>
    /// Testklasse. Verwende _factory, um die Methoden
    /// _factory.InitializeDatabase, _factory.GetHttpContent<T>, etc. aufzurufen.
    /// Achte immer darauf, _factory.InitializeDatabase aufzurufen, um die Datenbank neu zu erstellen.
    /// </summary>
    [Collection("Sequential")]
    public class TripsControllerTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly TestWebApplicationFactory _factory;

        public TripsControllerTests(TestWebApplicationFactory factory)
        {
            _factory = factory;
        }

        // TODO: Füge hier deine Integration Tests ein, um die Endpunkte zu testen.
    }
}
