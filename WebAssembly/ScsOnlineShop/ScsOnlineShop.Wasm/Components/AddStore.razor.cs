using Microsoft.AspNetCore.Components;
using ScsOnlineShop.Shared.Dto;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ScsOnlineShop.Wasm.Components
{
    public partial class AddStore
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;

        [Parameter]
        public EventCallback<StoreDto> OnStoreAddedCallback { get; set; }

        public bool Busy { get; private set; }
        public string? ErrorMessage { get; private set; }
        public StoreDto NewStore { get; private set; } = new(guid: default, name: string.Empty);

        public async Task HandleValidSubmit()
        {
            // Spinner aktivieren, damit der User nicht mehrmals speichert.
            Busy = true;
            try
            {
                var result = await HttpClient.PostAsJsonAsync("api/stores", NewStore);
                if (!result.IsSuccessStatusCode)
                {
                    ErrorMessage = result.StatusCode == System.Net.HttpStatusCode.BadRequest
                        ? await result.Content.ReadAsStringAsync()
                        : "Fehler beim Senden der Daten.";
                    return;
                }
                var newStore = await result.Content.ReadFromJsonAsync<StoreDto>();
                await OnStoreAddedCallback.InvokeAsync(newStore);
                // Das Eingabefeld nach erfolgter Eingabe wieder leeren.
                NewStore = new(guid: default, name: string.Empty);
            }
            finally
            {
                // Der Spinner sollte immer - auch im Fehlerfall - wieder deaktiviert werden.
                // Sonst bleibt er "hängen". Hier bietet sich finally an.
                Busy = false;
            }
        }
    }
}