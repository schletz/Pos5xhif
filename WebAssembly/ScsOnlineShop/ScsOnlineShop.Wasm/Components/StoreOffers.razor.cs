using Microsoft.AspNetCore.Components;
using ScsOnlineShop.Shared.Dto;
using ScsOnlineShop.Wasm.Services;
using ScsOnlineShop.Wasm.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScsOnlineShop.Wasm.Components
{
    public partial class StoreOffers
    {
        private Guid _loadedStore;
        private List<OfferDto> _offerDtos = new List<OfferDto>();

        [Inject]
        public RestService RestService { get; set; } = default!;
        [Parameter]
        public Guid StoreGuid { get; set; }
        [Parameter]
        public string ProductFilter { get; set; } = string.Empty;

        public IEnumerable<OfferDto> OfferDtos => _offerDtos.Where(o =>
            string.IsNullOrEmpty(ProductFilter) || o.Product.Name.ToLower().Contains(ProductFilter.Trim().ToLower()));
        public OfferDto? EditOffer { get; set; }
        public OfferDto? OfferToDelete { get; set; }
        public bool Loading { get; private set; }
        public ModalDialog DeleteConfirmDialog { get; set; } = default!;

        protected override async Task OnParametersSetAsync()
        {
            // Prevent loading from server if product filter changes.
            if (StoreGuid == _loadedStore) { return; }
            Loading = true;
            try
            {
                var offers = await RestService.SendAsync<List<OfferDto>>(HttpMethod.Get, $"stores/{StoreGuid}/offers");
                if (offers is null) { return; }
                _offerDtos = offers;
            }
            finally
            {
                Loading = false;
                _loadedStore = StoreGuid;
            }
        }

        public async Task DeleteOffer(OfferDto offer)
        {
            OfferToDelete = offer;
            var result = await DeleteConfirmDialog.Show();
            if (!result) { return; }
            Loading = true;
            try
            {
                await RestService.SendAsync<object>(HttpMethod.Delete, $"offers/{offer.Guid}");
                _offerDtos.Remove(offer);
            }
            finally
            {
                Loading = false;
                OfferToDelete = null;
            }
        }
        public async Task SaveOffer()
        {
            if (EditOffer is null) { return; }
            Loading = true;
            try
            {
                await RestService.SendAsync<object>(HttpMethod.Put, $"offers", EditOffer);
                EditOffer = null;
            }
            finally
            {
                Loading = false;
            }
        }
    }
}
