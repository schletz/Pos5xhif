using Spg.ProbeFachtheorie.Aufgabe2.Domain.Model.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.ProbeFachtheorie.Aufgabe2.Domain.Model
{
    public class User : EntityBase
    {
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public UserRoles Role { get; set; }
        public Guid Guid { get; set; }


        public List<ShoppingCart> ShoppingCarts { get; set; } = default!;
        public List<PickupTime> PictupTimes { get; set; } = default!;
    }
}
