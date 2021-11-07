using Microsoft.AspNetCore.Components;
using ScsOnlineShop.Shared.Dto;
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

        public List<StoreDto> StoreList { get; private set; } = new();

        protected override async Task OnInitializedAsync()
        {
            // using System.Net.Http.Json;
            StoreList = await HttpClient
                .GetFromJsonAsync<List<StoreDto>>("/api/stores") ?? new();
        }

        public void OnStoreAdded(StoreDto storeDto)
        {
            StoreList.Add(storeDto);
        }
    }
}