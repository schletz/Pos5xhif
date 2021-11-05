using Microsoft.AspNetCore.Components;
using ScsOnlineShop.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace ScsOnlineShop.Wasm.Pages
{
    public partial class Stores
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;
        public StoreDto[] StoreList { get; private set; } = Array.Empty<StoreDto>();

        protected override async Task OnInitializedAsync()
        {
            // using System.Net.Http.Json;
            StoreList = await HttpClient
                .GetFromJsonAsync<StoreDto[]>("/api/stores") ?? Array.Empty<StoreDto>(); ;
        }

    }
}
