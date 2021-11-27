using AutoMapper;
using ScsOnlineShop.Application.Model;
using ScsOnlineShop.Shared.Dto;

namespace ScsOnlineShop.Application.Mappings
{
    public class ShopMappingProfile : Profile
    {
        public ShopMappingProfile()
        {
            CreateMap<Store, StoreDto>();
            CreateMap<StoreDto, Store>();
            CreateMap<Offer, OfferDto>();
            CreateMap<Product, ProductDto>();  // Offer contains type product, so we have to add a mapping for product.
            CreateMap<ProductCategory, ProductCategoryDto>();  // Offer contains type product, so we have to add a mapping for product.
        }
    }
}
