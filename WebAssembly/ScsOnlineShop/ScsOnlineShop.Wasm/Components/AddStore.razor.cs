using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using ScsOnlineShop.Dto;
using System.ComponentModel.DataAnnotations;

namespace ScsOnlineShop.Wasm.Components
{
    public partial class AddStore
    {
        public class StoreModel
        {

            [StringLength(255, MinimumLength = 3)]
            public string Name { get; set; } = string.Empty;
        }
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;
        public StoreModel NewStore { get; set; } = new();
        public string? ErrorMessage { get; private set; }
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
