using SPG_Fachtheorie.Aufgabe2.Model;
using SPG_Fachtheorie.Aufgabe3.Commands;
using SPG_Fachtheorie.Aufgabe3.Dtos;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Spg.Fachtheorie.Aufgabe3.API.Test
{
    [Collection("Sequential")]
    public class ExamsControllerTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly TestWebApplicationFactory _factory;

        public ExamsControllerTests(TestWebApplicationFactory factory)
        {
            _factory = factory;
        }
        
        // TODO: Füge die Nötigen Integration Tests hinzu, um deine Endpunkte lt. Angabe zu testen.

    }
}
