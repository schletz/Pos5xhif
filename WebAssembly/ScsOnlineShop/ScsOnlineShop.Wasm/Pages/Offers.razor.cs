using Microsoft.AspNetCore.Components;
using ScsOnlineShop.Shared.Dto;
using ScsOnlineShop.Wasm.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScsOnlineShop.Wasm.Pages
{
    public partial class Offers
    {
        [Inject]
        public RestService RestService { get; set; } = default!;
        public IReadOnlyList<StoreDto> StoreDtos { get; private set; } = new List<StoreDto>();
        public StoreDto? ActiveStore { get; set; }
        public bool Loading { get; private set; }
        public string ProductFilter { get; set; } = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            Loading = true;
            try
            {
                var stores = await RestService.SendAsync<List<StoreDto>>(HttpMethod.Get, "stores");
                if (stores is null) { return; }
                StoreDtos = stores.OrderBy(s => s.Name).ToList();
                ActiveStore = StoreDtos.FirstOrDefault();
            }
            finally
            {
                Loading = false;
            }
        }
    }
}
