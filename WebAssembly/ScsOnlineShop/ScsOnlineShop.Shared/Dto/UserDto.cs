using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScsOnlineShop.Shared.Dto
{
    public record UserDto(string Username, string Role, string Token);
}
