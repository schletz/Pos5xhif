using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Spg.Fachtheorie.Aufgabe2.Services;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Spg.Fachtheorie.Aufgabe3.API.Test
{
    public class TestApiFactory : WebApplicationFactory<Program>
    {
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.First(d => d.ServiceType == typeof(DbContextOptions<Aufgabe2Database>));
                services.Remove(descriptor);
                services.AddDbContext<Aufgabe2Database>(options =>
                {
                    options.UseSqlite("DataSource = C:\\Scratch\\Aufgabe3_Tests.db");
                });
            });
            builder.UseEnvironment("Development");
        }
        public void InitializeDatabase(Action<Aufgabe2Database> action)
        {
            using var scope = Services.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<Aufgabe2Database>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            action(db);
        }

        /// <summary>
        /// Send a GET Request and return the deserialized response.
        /// Useful for strongly typed JSON data with DTOs.
        /// </summary>
        public async Task<(HttpStatusCode, T)> GetHttpContent<T>(
            string requestUrl)
            where T : class
        {
            using var client = CreateClient();

            // TODO: Implementation
            //client.DefaultRequestHeaders.Add("role", "admin");

            var response = await client.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            var dataString = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<T>(dataString, _jsonOptions);
            if (data is null) throw new Exception("Deserialization failed");
            return (response.StatusCode, data);
        }

        /// <summary>
        /// Send a GET Request and return the response as a JsonElement. Useful for dynamic JSON data without DTOs.
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public async Task<(HttpStatusCode, JsonElement)> GetHttpContent(string requestUrl)
        {
            using var client = CreateClient();

            // TODO: Implementation
            //client.DefaultRequestHeaders.Add("role", "admin");

            var response = await client.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            var dataString = await response.Content.ReadAsStringAsync();
            var data = JsonDocument.Parse(dataString);
            return (response.StatusCode, data.RootElement);
        }

        /// <summary>
        /// Send a POST Request with a strongly typed payload and return the deserialized response.
        /// </summary>
        public async Task<(HttpStatusCode, JsonElement)> PostHttpContent<TCommand>(
            string requestUrl,
            TCommand payload)
            where TCommand : class
        {
            using var client = CreateClient();

            // TODO: Implementation
            //client.DefaultRequestHeaders.Add("role", "admin");

            var jsonBody = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUrl, jsonBody);
            response.EnsureSuccessStatusCode();
            var dataString = await response.Content.ReadAsStringAsync();
            var data = JsonDocument.Parse(dataString);
            return (response.StatusCode, data.RootElement);
        }

        // TODO: Implement Delete, if needed
    }
}
