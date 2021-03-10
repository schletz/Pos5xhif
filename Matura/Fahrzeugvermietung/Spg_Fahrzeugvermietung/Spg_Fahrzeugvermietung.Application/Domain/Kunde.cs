using System.Collections.Generic;

namespace Spg_Fahrzeugvermietung.Application.Domain
{
    public class Kunde
    {
        public int Id { get; set; }
        public Name Name { get; set; }
        public string FuehrerscheinNr { get; set; }
        public string Email { get; set; }
        public List<Vermietung> Vermietungen { get; set; } = new List<Vermietung>();
    }
}
