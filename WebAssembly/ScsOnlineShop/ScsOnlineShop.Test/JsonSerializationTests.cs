using ScsOnlineShop.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ScsOnlineShop.Test
{
    public class JsonSerializationTests
    {
        [Fact]
        public void StoreDtoSerializationSuccessTest()
        {
            var store = new StoreDto(Guid.NewGuid(), "Name");
            var json = System.Text.Json.JsonSerializer.Serialize(store);
            var store2 = System.Text.Json.JsonSerializer.Deserialize<StoreDto>(json)!;
            Assert.True(store.Guid == store2.Guid);
        }

        [Fact]
        public void OfferDtoSerializationSuccessTest()
        {
            var offer = new OfferDto(
                Guid.NewGuid(),
                100,
                new ProductDto(1000000, "Name", new ProductCategoryDto("Name", Guid.NewGuid())),
                new StoreDto(Guid.NewGuid(), "name"));

            var json = System.Text.Json.JsonSerializer.Serialize(offer);
            var offer2 = System.Text.Json.JsonSerializer.Deserialize<OfferDto>(json)!;
            Assert.True(offer.Guid == offer2.Guid);
            Assert.True(offer.Product.Ean == offer2.Product.Ean);
            Assert.True(offer.Store.Guid == offer2.Store.Guid);
            Assert.True(offer.Product.ProductCategory.Guid == offer2.Product.ProductCategory.Guid);
        }
    }
}