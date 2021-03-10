using System;

namespace Spg_Fahrzeugvermietung.Application.Domain
{
    public class Vermietung
    {
        public int Id { get; set; }
        public int KundeId { get; set; }
        public Kunde Kunde { get; set; }
        public int FahrzeugId { get; set; }
        public Fahrzeug Fahrzeug { get; set; }
        public DateTime VerliehenAb { get; set; }
        public DateTime VerliehenBis { get; set; }
        public decimal? Rechnungsbetrag { get; set; }
    }
}
