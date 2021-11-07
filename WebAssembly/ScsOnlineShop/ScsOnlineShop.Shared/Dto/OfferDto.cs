using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScsOnlineShop.Shared.Dto
{
    /// <summary>
    /// DTO Basisklasse für Offer. Beinhaltet keine Navigation Properties und kann somit für
    /// Eingaben neuer Produkte verwendet werden. Sonst hat man das Problem dass die Klasse
    /// ohne Product und Shop nicht instanziert werden kann.
    /// </summary>
    public class OfferDtoBase
    {
        public OfferDtoBase(Guid guid, int productEan, Guid storeGuid, decimal price)
        {
            Guid = guid;
            ProductEan = productEan;
            StoreGuid = storeGuid;
            Price = price;
        }

        public Guid Guid { get; }
        public int ProductEan { get; set; }
        public Guid StoreGuid { get; set; }
        public decimal Price { get; set; }
    }

    /// <summary>
    /// DTO Klasse für Offer mit Navigations. Da sie nicht gesetzt werden müssen, sind
    /// sie read-only.
    /// </summary>
    public class OfferDto : OfferDtoBase
    {
        public OfferDto(Guid guid, decimal price, ProductDto product, StoreDto store) :
            base(guid, product.Ean, store.Guid, price)
        {
            Product = product;
            Store = store;
        }

        public ProductDto Product { get; }
        public StoreDto Store { get; }
    }
}