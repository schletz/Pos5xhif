using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScsOnlineShop.Dto
{
    public record OfferDto(
        Guid Guid,
        int ProductEan,
        decimal Price);
}