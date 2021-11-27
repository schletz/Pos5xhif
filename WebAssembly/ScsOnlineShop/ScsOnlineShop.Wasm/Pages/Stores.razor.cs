using Microsoft.AspNetCore.Components;
using ScsOnlineShop.Shared.Dto;
using ScsOnlineShop.Wasm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScsOnlineShop.Wasm.Pages
{
    public partial class Stores
    {
        [Inject]
        public RestService RestService { get; set; } = default!;
        public List<StoreDto> StoreList { get; private set; } = new();
        protected override async Task OnInitializedAsync()
        {
            StoreList = await RestService
                .SendAsync<List<StoreDto>>(HttpMethod.Get, "stores") ?? new();
        }
        public void OnStoreAdded(StoreDto storeDto)
        {
            StoreList.Add(storeDto);
        }
    }
}