using Microsoft.AspNetCore.Components;
using ScsOnlineShop.Shared.Dto;
using ScsOnlineShop.Wasm.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScsOnlineShop.Wasm.Components
{
    public partial class StoreOffers
    {
        [Inject]
        public RestService RestService { get; set; } = default!;
        [Parameter]
        public Guid StoreGuid { get; set; }
        public IReadOnlyList<OfferDto> OfferDtos { get; private set; } = new List<OfferDto>();
        public bool Loading { get; private set; }
        protected override async Task OnParametersSetAsync()
        {
            Loading = true;
            try
            {
                var offers = await RestService.SendAsync<List<OfferDto>>(HttpMethod.Get, $"stores/{StoreGuid}/offers");
                if (offers is null) { return; }
                OfferDtos = offers;
            }
            finally
            {
                Loading = false;
            }
        }
    }
}
