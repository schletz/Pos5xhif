namespace Spg_Fahrzeugvermietung.Application.Domain
{
    public class Transporter : Fahrzeug
    {
        public int MaxNutzlast { get; set; }
        public int Gesamtgewicht { get; set; }
    }
}
