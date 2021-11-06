using Microsoft.AspNetCore.Components;
using ScsOnlineShop.Dto;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ScsOnlineShop.Wasm.Components
{
    public partial class AddStore
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;

        public string? ErrorMessage { get; private set; }
        public StoreDto NewStore { get; set; } = new(default, string.Empty);

        public async Task HandleValidSubmit()
        {
            var storeDto = new StoreDto(default, NewStore.Name);
            var result = await HttpClient.PostAsJsonAsync("api/store", storeDto);
            if (!result.IsSuccessStatusCode)
            {
                ErrorMessage = $"Fehler beim Senden der Daten. Statuscode {result.StatusCode}";
            }
        }
    }
}