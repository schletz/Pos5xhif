using Microsoft.AspNetCore.Components;
using ScsOnlineShop.Shared.Dto;
using ScsOnlineShop.Wasm.Services;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ScsOnlineShop.Wasm.Components
{
    public partial class AddStore
    {
        [Inject]
        public RestService RestService { get; set; } = default!;

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
                try
                {
                    var newStore = await RestService.SendAsync<StoreDto>(HttpMethod.Post, "stores", NewStore);
                    // Das Eingabefeld nach erfolgter Eingabe wieder leeren.
                    NewStore = new(guid: default, name: string.Empty);
                    await OnStoreAddedCallback.InvokeAsync(newStore);
                }
                catch (ApplicationException e)
                {
                    ErrorMessage = e.Message;
                    return;
                }
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